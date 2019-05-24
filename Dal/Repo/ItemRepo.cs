using Dal.Context;
using Dal.Interfaces;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dal.Repo
{
    public class ItemRepo
    {
        private readonly InItem inItem;
        private readonly IWinkel iwinkel;
        public ItemRepo()
        {
            iwinkel = new WinkelSqlContext();
            inItem = new ItemSqlContext();
        }
        public bool KanItemKopen(int item_id, int user_id)
        {
            return iwinkel.KanItemKopen(item_id, user_id);
        }

        public List<Item> Itemsophalen()
        {
            return inItem.Itemsophalen();
        }

        //public void KoopItem(int item_id)
        //{
        //    return iwinkel.KoopItem(item_id);
        //}
    }
}
