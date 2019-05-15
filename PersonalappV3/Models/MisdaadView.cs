using Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PersonalappV3.Models
{
    public class MisdaadView
    {
        //public Misdaad Misdaad { get; set; }

        //private int Misdaad_id;
        //public int Misdaad_id { get; set; }
        //public string Misdaad_naam { get; set; }
        [Required(ErrorMessage = "Selecteer een misdaad!")]
        public List<Misdaad> MisdadenList { get; set; }
    }
}