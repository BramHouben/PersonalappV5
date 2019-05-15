using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
  public  class Misdaad
    {
        public Misdaad()
        {

        }
        public Misdaad(int misdaad_id, string misdaad_naam, int misdaad_moelijkheidsgraad, string misdaad_beschrijving)
        {
            Misdaad_id = misdaad_id;
            Misdaad_naam = misdaad_naam;
            Misdaad_moeilijkheidsgraad = misdaad_moelijkheidsgraad;
            Misdaad_beschrijving = misdaad_beschrijving;
        }

       public int Misdaad_id { get; set; }
        public string Misdaad_naam { get; set; }
        public int Misdaad_moeilijkheidsgraad { get; set; }
        public string Misdaad_beschrijving { get; set; }
        //public List<Misdaad> Misdaden { get; set; }
        //public List<Misdaad>Misdaden { get; set; }


    }
}
