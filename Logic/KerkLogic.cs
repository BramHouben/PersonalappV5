using Dal.Interfaces;
using Models;
using System;

namespace Logic
{
    public class KerkLogic
    {
       
        private IKerk InKerk;

        public KerkLogic(IKerk inkerk)
        {
            InKerk = inkerk;
        }

        public void GetInfoVoorKerk(int user_id, Kerk kerk)
        {
            InKerk.GeefInfoVoorKerk(user_id, kerk);
        }

        public void LevensToevoegen(int user_id)
        {
            InKerk.LevensToevoegen(user_id);
        }

        public bool MagLevensToevoegen(/*int kerkid*/ int user_id)
        {
            DateTime tijdnu = DateTime.Now;
            DateTime tijdVast = InKerk.KrijgTijd(user_id);
            if (tijdVast <= tijdnu)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int KrijgLevensInfo(int user_id)
        {
            return InKerk.KrijgLevensInfo(user_id);
        }
    }
}