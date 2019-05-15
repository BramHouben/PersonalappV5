using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
   public class Gevangenis
    {
        public Gevangenis()
        {
        }

        public Gevangenis(int user_id, int gevangenis_id, int borg, DateTime tijd_vast)
        {
            User_id = user_id;
            Gevangenis_id = gevangenis_id;
            Borg = borg;
            Tijd_vast = tijd_vast;
        }

        public int User_id { get; set; }
        public int Gevangenis_id { get; set; }
        public int Borg { get; set; }

        public DateTime Tijd_vast { get; set; }
    }
}
