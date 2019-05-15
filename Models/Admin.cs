using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Admin : UserInlog
    {
        public Admin()
        {

        }
        public Admin(int admin_id, bool magaanpassen)
        {
            this.admin_id = admin_id;
            Magaanpassen = magaanpassen;
        }

        public int admin_id { get; set; }

        public bool Magaanpassen { get; set; }



        //private string admin_naam;

        //public Admin(/*int user_id*/ string email, string username, string hash) : base(/*user_id*/ email, username, hash)
        //{
        //}
    }
}
