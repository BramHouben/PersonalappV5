using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalappV3.Models
{
    public class KerkView
    {

        [Display(Name = "Tijd herstel")]
        public DateTime Kerk_tijd { get; set; }
        [Display(Name = "Levens")]
        public int Levens_user { get; set; }

    }
}
