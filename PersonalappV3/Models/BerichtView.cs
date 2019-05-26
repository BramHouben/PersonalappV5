using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalappV3.Models
{
    public class BerichtView
    {
        [Required(ErrorMessage = "Schrijf iets!")]
        [Display(Name = "Inhoud")]
        public string Bericht_inhoud { get; set; }
        [Required(ErrorMessage = "Geef het bericht een titel!")]
        [Display(Name = "Titel")]
        public string Bericht_titel { get; set; }
     
        [Display(Name = "Belangrijk bericht?")]
        public bool Belangrijk_bericht { get; set; }

        public int clan_id { get; set; }
    }
}
