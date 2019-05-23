using Dal.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dal.Repo
{
    public class ItemRepo
    {
        private readonly IWinkel iwinkel;
        public ItemRepo()
        {
            iwinkel = new MisdaadContext();
        }
        public bool KanItemKopen(int item_id, int user_id)
        {
            return 
        }
    }
}
