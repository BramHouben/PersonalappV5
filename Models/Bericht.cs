using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
  public  class Bericht
    {
        public Bericht() { }
        public Bericht(int bericht_id, string bericht_inhoud, DateTime bericht_tijd, int user_id, string bericht_titel, int clan_id, bool belangrijk_bericht)
        {
            Bericht_id = bericht_id;
            Bericht_inhoud = bericht_inhoud;
            Bericht_tijd = bericht_tijd;
            User_id = user_id;
            Bericht_titel = bericht_titel;
            Clan_id = clan_id;
            Belangrijk_bericht = belangrijk_bericht;
        }

        public int Bericht_id { get; set; }

        public string Bericht_inhoud { get; set; }

        public DateTime Bericht_tijd { get; set; }
        public int User_id { get; set; }

        public string Bericht_titel { get; set; }
        public int Clan_id { get; set; }

        public bool Belangrijk_bericht { get; set; }
    }
}
