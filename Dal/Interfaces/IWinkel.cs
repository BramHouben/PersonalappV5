using System;
using System.Collections.Generic;
using System.Text;
using Model;

namespace Dal.Interfaces
{
    public interface IWinkel
    {
        bool KanItemKopen(int item_id, int user_id);
        void KoopItem(int item_id, int user_id);
        List<Item> Itemsophalen();
    }
}
