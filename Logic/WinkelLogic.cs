using System;
using System.Collections.Generic;
using System.Text;
using Dal.Context;
using Dal.Interfaces;
using Dal.Repo;
using Model;

namespace Logic
{
   public class WinkelLogic
    {
        //private ItemSqlContext itemSql = new ItemSqlContext();
        private ItemRepo itemRepo;

        public WinkelLogic(IWinkel inWinkel)
        {
            itemRepo = new ItemRepo(inWinkel);
        }

        public List<Item> Vullist()
        {
            return itemRepo.Itemsophalen();
        }

        public bool KanItemKopen(int item_id, int user_id)
        {

            if(itemRepo.KanItemKopen(item_id, user_id) == true)
            {
                itemRepo.KoopItem(item_id, user_id);
                return true;
            }
            else
            {
                return false;
            }
        }

        //public void KoopItem(int item_id)
        //{
        //    return itemRepo.KoopItem(item_id);
        //}
    }
}
