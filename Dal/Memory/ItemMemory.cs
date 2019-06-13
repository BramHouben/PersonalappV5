using Dal.Interfaces;
using Model;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dal.Context
{
    public class ItemMemory : IWinkel
    {
        public List<Item> ItemList = new List<Item>();
        public List<Item> WinkelList = new List<Item>();
        private UserIngame WinkelgebruikerGoed = new UserIngame();
        private UserIngame WinkelGebruikerFout = new UserIngame();
        public List<UserIngame> geburikersWinkel = new List<UserIngame>();
        public ItemMemory()
        {
            // 2 verschillende users toevoegen lukte niet via de contructor
            WinkelgebruikerGoed.ingameGeld = 500;
            WinkelgebruikerGoed.user_id = 1;
            WinkelgebruikerGoed.itemlist = new List<Item>();
            geburikersWinkel.Add(WinkelgebruikerGoed);
            WinkelGebruikerFout.ingameGeld = 10;
            WinkelGebruikerFout.user_id = 2;
            WinkelGebruikerFout.itemlist = new List<Item>();
            geburikersWinkel.Add(WinkelGebruikerFout);
         

            DateTime vervaldatum = new DateTime(2020, 4, 12);
            //ItemList.Add(new Item(1, "Luchtbux", 15, "Een Luchtbux om duiven neer te schieten", "Geweer", "Bekend", 100, 1, vervaldatum, true));
            WinkelList.Add(new Item(1, "Luchtbux", 15, "Een Luchtbux om duiven neer te schieten", "Geweer", "Bekend", 100, 1, vervaldatum, true));
        }

        //private static List<Item> items = new List<Item>();

        //public ItemMemory()
        //{
        //    if (items.Count == 0)
        //    {
        //        items.Add(new Item(1, "Geweer"));
        //        items.Add(new Item(2, "vork"));
        //    }
        //}

        public List<Item> Itemsophalen()
        {
            return ItemList;
        }

        public bool KanItemKopen(int item_id, int user_id)
        {
        
            UserIngame gebruiker = geburikersWinkel.FirstOrDefault(x => x.user_id.Equals(user_id));
            int usergeld = gebruiker.ingameGeld;
            Item varItem = WinkelList.FirstOrDefault(x => x.Item_id.Equals(item_id));
           int prijsItem=  varItem.Item_prijs;
            if (usergeld >= prijsItem)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public void KoopItem(int item_id, int user_id)
        {
            Item item = WinkelList.FirstOrDefault(x => x.Item_id.Equals(item_id));
            UserIngame gebruiker = geburikersWinkel.FirstOrDefault(x => x.user_id.Equals(user_id));
            gebruiker.itemlist.Add(item);
        }
    }
}