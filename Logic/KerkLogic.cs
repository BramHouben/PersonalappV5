using System;
using System.Collections.Generic;
using System.Text;
using Dal.Interfaces;
using Dal.Repo;
using Models;

namespace Logic
{
    public class KerkLogic
    {
        private KerkRepo KerkRepo;
        public KerkLogic(IKerk inkerk)
        {
            KerkRepo = new KerkRepo(inkerk);
        }
        
        public void GetInfoVoorKerk(int user_id, Kerk kerk)
        {
            KerkRepo.GeefInfoVoorKerk(user_id,kerk);
        }

        public void LevensToevoegen(int user_id)
        {
            KerkRepo.LevensToevoegen(user_id);
        }



        public bool MagLevensToevoegen(/*int kerkid*/ int user_id)
        {
            DateTime tijdnu = DateTime.Now;
          DateTime tijdVast = KerkRepo.KrijgTijd(user_id);
            if(tijdVast <= tijdnu)
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
          return  KerkRepo.KrijgLevensInfo(user_id);
        }
    }
}
