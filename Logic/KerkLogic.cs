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

        public void LevensToevoegen(int kerkid, int user_id)
        {
            KerkRepo.LevensToevoegen(kerkid, user_id);
        }



        public bool MagLevensToevoegen(int kerkid)
        {
            DateTime tijdnu = DateTime.Now;
          DateTime tijdVast = KerkRepo.KrijgTijd(kerkid);
            if(tijdVast <= tijdnu)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
