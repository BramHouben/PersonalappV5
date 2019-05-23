using System;
using System.Collections.Generic;
using System.Text;
using Dal.Context;
using Dal.Repo;
using Model;

namespace Logic
{
   public class WinkelLogic
    {
        private ItemSqlContext itemSql = new ItemSqlContext();
        private ItemRepo itemRepo = new ItemRepo();
        public List<Item> Vullist()
        {
            return itemSql.Itemsophalen();
        }

        public bool KanItemKopen(int item_id, int user_id)
        {
            return itemRepo.KanItemKopen(item_id, user_id);
        }
    }
}
