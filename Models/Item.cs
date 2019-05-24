using System;

namespace Model
{
    public class Item
    {

        public Item()
        {


        }

        public Item(int item_id, string item_naam, int item_schade, string item_beschrijving, string item_Soort, string item_reputatie, int item_prijs, int min_level, DateTime vervaldatum, bool status)
        {
            Item_id = item_id;
            Item_naam = item_naam;
            Item_schade = item_schade;
            Item_beschrijving = item_beschrijving;
            Item_Soort = item_Soort;
            Item_reputatie = item_reputatie;
            Item_prijs = item_prijs;
            Item_min_level = min_level;
            Vervaldatum = vervaldatum;
            Status = status;
        }

        public int Item_id { get; set; }

        public string Item_naam { get; set; }

        public int Item_schade { get; set; }

        public string Item_beschrijving { get; set; }

        public string Item_Soort { get; set; }

        public string Item_reputatie { get; set; }

        public int Item_prijs { get; set; }

        public int Item_min_level { get; set; }

        public DateTime Vervaldatum { get; set; }

        public bool Status { get; set; }
        //public int item_idReturn()
        //{
        //    return Item_id;

        //}
        //public string item_naamReturn()
        //{
        //    return Item_naam;

        //}

    }
}
