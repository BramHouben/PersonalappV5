using Dal.Context;
using Dal.Interfaces;
using Dal.Repo;
using Models;
using System;
using System.Collections.Generic;

namespace Logic
{
    public class MisdaadLogic
    {
        private MisdaadRepo MisdaadRepo;
        public MisdaadLogic(IMisdaad imisdaad)
        {
            MisdaadRepo = new MisdaadRepo(imisdaad);
        }
       
        //private MisdaadContext MisdaadContext = new MisdaadContext();

        public List<Misdaad> VulList()
        {
            return MisdaadRepo.VulListMisdaden();
        }

        public bool PlegenMisdaad(int id)
        {
            int kans = MisdaadRepo.MisdaadPlegen(id);
            if (KansBerekenen(kans) == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool KansBerekenen(int kans)
        {
            bool gelukt = false;
            Random rnd = new Random();
            for (int i = 0; i < kans; i++)
            {
                int nummer = rnd.Next(1, 100);
                if (nummer >= 80)
                {
                    gelukt = true;
                    break;
                }
            }
            return gelukt;
        }

        public void ZetInGevangenis(int id, int user_id)
        {
            MisdaadRepo.ZetInGevangenis(id, user_id);
        }

        public void GeefReward(int id, int user_id)
        {
            MisdaadRepo.GeefReward(id, user_id);
        }

        public void ZetInDatabase(int id, int user_id)
        {
            MisdaadRepo.ZetInDatabase(id, user_id);
        }
    }
}