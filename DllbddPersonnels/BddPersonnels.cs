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
       
    }
}
