using Dal.Interfaces;
using Model;
using System.Collections.Generic;

namespace Logic
{
    public class WinkelLogic
    {
        private IWinkel InWinkel;

        public WinkelLogic(IWinkel inWinkel)
        {
            InWinkel = inWinkel;
        }

        public List<Item> Vullist()
        {
            return InWinkel.Itemsophalen();
        }

        public bool KanItemKopen(int item_id, int user_id)
        {
            if (InWinkel.KanItemKopen(item_id, user_id) == true)
            {
                InWinkel.KoopItem(item_id, user_id);

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}