using System;
using System.Collections.Generic;
using System.Text;
using Dal.Context;
using Model;

namespace Logic
{
   public class WinkelLogic
    {
        private ItemSqlContext itemSql = new ItemSqlContext();
        public List<Item> Vullist()
        {
            return itemSql.Itemsophalen();
        }
    }
}
