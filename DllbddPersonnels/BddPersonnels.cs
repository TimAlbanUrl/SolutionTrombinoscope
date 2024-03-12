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

        public List<Service> FetchAllServices()
        {
            return bdd.Services.ToList();
        }

        public List<Fonction> FetchAllFonctions()
        {
            return bdd.Fonctions.ToList();
        }

        public List<Personnel> FetchAllPersonnels()
        {
            return bdd.Personnels.ToList();
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

        public void UpdatePersonnel(Personnel p, string nom, string prenom)
        {
            p.Nom = nom;
            p.Prenom = prenom;
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

        public void DeletePersonnel(Object obj)
        {
            Personnel pers = (Personnel)obj;
            var pers2 = bdd.Personnels.Single(Personnel => Personnel.Id == pers.Id);
            bdd.Personnels.DeleteOnSubmit(pers2);
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

        public List<Personnel> SelectPersonnels(Object servObj, Object foncObj, String nom)
        {
            Service serv;
            Fonction fonc;
            System.Linq.IQueryable<BddpersonnelContext.Personnel> query;
            if (servObj != null)
            {
                serv = (Service)servObj;
                if (foncObj != null)
                {
                    fonc = (Fonction)foncObj;
                    if (nom != "") // either "" or null
                    {
                        query = from pers in bdd.Personnels
                                where pers.IdService == serv.Id
                                where pers.IdFonction == fonc.Id
                                where pers.Nom.Contains(nom)
                                select pers;
                        return query.ToList();
                    }
                    query = from pers in bdd.Personnels
                            where pers.IdService == serv.Id
                            where pers.IdFonction == fonc.Id
                            select pers;
                    return query.ToList();
                }
                if (nom != "") // either "" or null
                {
                    query = from pers in bdd.Personnels
                            where pers.IdService == serv.Id
                            where pers.Nom.Contains(nom)
                            select pers;
                    return query.ToList();
                }
                query = from pers in bdd.Personnels
                        where pers.IdService == serv.Id
                        select pers;
                return query.ToList();
            }
            if (foncObj != null)
            {
                fonc = (Fonction)foncObj;
                if (nom != "") // either "" or null
                {
                    query = from pers in bdd.Personnels
                            where pers.IdFonction == fonc.Id
                            where pers.Nom.Contains(nom)
                            select pers;
                    return query.ToList();
                }
                query = from pers in bdd.Personnels
                        where pers.IdFonction == fonc.Id
                        select pers;
                return query.ToList();
            }
            if (nom != "") // either "" or null
            {
                query = from pers in bdd.Personnels
                        where pers.Nom.Contains(nom)
                        select pers;
                return query.ToList();
            }
            return FetchAllPersonnels();
        }

        public String GetServiceIntituleFromPersonnel(Object obj)
        {
            Personnel pers = (Personnel)obj;
            return bdd.Services.Single(Service => Service.Id == pers.IdService).Intitule;
        }

        public String GetFonctionIntituleFromPersonnel(Object obj)
        {
            Personnel pers = (Personnel)obj;
            return bdd.Fonctions.Single(Fonction=> Fonction.Id == pers.IdFonction).Intitule;
        }
    }
}
