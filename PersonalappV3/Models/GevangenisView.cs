using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalappV3.Models
{
    public class GevangenisView
    {
        public int Gevangenis_id { get; set; }
        public DateTime Tijd_vast { get; set; }
        public int Borg { get; set; }

        public string Misdaad_naam { get; set; }
    }
}
