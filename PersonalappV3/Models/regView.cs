
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalappV3.Models
{
    public class RegView
    {
        [Required(ErrorMessage = "Email is verplicht")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Gebruikersnaam is verplicht")]
        [Display(Name ="Gebruikersnaam")]
        
        public string Username { get; set; }
        [Required(ErrorMessage = "Vul een wachtwoord in")]
        [DataType(DataType.Password)]
        [Display(Name = "Wachtwoord")]
        public string WW { get; set; }
    }
}
