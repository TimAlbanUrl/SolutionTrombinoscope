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

        public List<Personnel> fetchAllPersonnel()
        {
            return bdd.Personnels.ToList();
        }

        public void DeletePersonnel(Personnel p)
        {
            bdd.Personnels.DeleteOnSubmit(p);
            bdd.SubmitChanges();
        }
        public void UpdatePersonnel(Personnel p , string nom, string prenom)
        {
            p.Nom = nom;
            p.Prenom = prenom;
            bdd.SubmitChanges();
        }
    }
}
