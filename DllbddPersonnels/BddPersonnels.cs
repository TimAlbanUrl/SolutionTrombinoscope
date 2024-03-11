using BddpersonnelContext;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DllbddPersonnels
{
    public class BddPersonnels
    {
        public BddpersonnelDataContext bdd;
        public BddPersonnels(String user, String mdp, String serveurIp,String port) {
            bdd = new BddpersonnelDataContext("User Id=" + user + ";Password=" + mdp + ";Host=" + serveurIp + ";Port="+port+";Database=bddpersonnels;Persist Security Info=True");
        }
        public BddPersonnels()
        {
            bdd = new BddpersonnelDataContext();
        }
        public List<Service> fetchAllServices()
        {
            return bdd.Services.ToList();
        }

        public List<Fonction> fetchAllFonctions()
        {
            return bdd.Fonctions.ToList();
        }

        public void UpdateService(Object obj, String intitule)
        {
            Service serv = (Service)obj;
            serv.Intitule = intitule;
            bdd.SubmitChanges();
        }

        public void UpdateFonction(Object obj, String intitule)
        {
            Fonction fonc = (Fonction)obj;
            fonc.Intitule = intitule;
            bdd.SubmitChanges();
        }

        public void DeleteService(Object obj)
        {
            Service serv = (Service)obj;
            var serv2 = bdd.Services.Single(Service => Service.Id == serv.Id);
            bdd.Services.DeleteOnSubmit(serv2);
            bdd.SubmitChanges();
        }

        public void DeleteFonction(Object obj)
        {
            Fonction fonc = (Fonction)obj;
            var fonc2 = bdd.Fonctions.Single(Fonction => Fonction.Id == fonc.Id);
            bdd.Fonctions.DeleteOnSubmit(fonc2);
            bdd.SubmitChanges();
        }

        public void InsertService(string intitule)
        {
            Service serv = new Service();
            serv.Id = 0;
            serv.Intitule = intitule;
            bdd.Services.InsertOnSubmit(serv);
            bdd.SubmitChanges();
        }
        public void InsertFonction(string intitule)
        {
            Fonction fonc = new Fonction();
            fonc.Id = 0;
            fonc.Intitule = intitule;
            bdd.Fonctions.InsertOnSubmit(fonc);
            bdd.SubmitChanges();
        }
    }
}
