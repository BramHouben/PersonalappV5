using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalappV3.Models
{
    public class ClanView
    {
        [Required(ErrorMessage = "Kies een Clan!")]
        [Display(Name = "Clans")]
        public List<Clan> ClanLijst { get; set; }

        public List<Bericht> BerichtenLijst { get; set; }

        public int AantalClanLeden { get; set; }
    }
}
