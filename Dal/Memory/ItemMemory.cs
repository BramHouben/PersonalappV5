using Dal.Interfaces;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dal.Context
{
    class ItemMemory : IWinkel
    {

        private static List<Item> items = new List<Item>();

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
            return items;
        }

        public bool KanItemKopen(int item_id, int user_id)
        {
            throw new NotImplementedException();
        }

        public void KoopItem(int item_id, int user_id)
        {
            throw new NotImplementedException();
        }
    }
}
