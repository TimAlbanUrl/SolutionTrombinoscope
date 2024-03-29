﻿using BddpersonnelContext;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

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

        public void InsertPersonnel(string prenom, string nom, Service service, Fonction fonction, string telephone,
            BitmapImage image)
        {
            Personnel pers = new Personnel();
            pers.Fonction = fonction;
            pers.Id = 0;
            pers.Service = service;
            pers.Nom = nom;
            pers.Prenom = prenom;
            byte[] data;
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(image));
            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                data = ms.ToArray();
            }
            pers.Photo = data;
            bdd.Personnels.InsertOnSubmit(pers);
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

        public Service GetServiceFromId(int id)
        {
            return bdd.Services.Single(Service => Service.Id == id);
        }

        public Fonction GetFonctionFromId(int id)
        {
            return bdd.Fonctions.Single(Fonction=> Fonction.Id == id);
        }
    }
}
