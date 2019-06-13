using Model;
using System.Collections.Generic;

namespace Dal.Interfaces
{
    public interface IWinkel
    {
        bool KanItemKopen(int item_id, int user_id);

        void KoopItem(int item_id, int user_id);

        List<Item> Itemsophalen();
    }
}