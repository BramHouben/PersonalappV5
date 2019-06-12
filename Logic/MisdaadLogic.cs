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
        //private MisdaadRepo MisdaadRepo;
        private IMisdaad InMisdaad;
        public MisdaadLogic(IMisdaad imisdaad)
        {
            InMisdaad = imisdaad;
        }
       
        //private MisdaadContext MisdaadContext = new MisdaadContext();

        public List<Misdaad> VulList()
        {
            return InMisdaad.VulListMisdaden();
        }

        public bool PlegenMisdaad(int id)
        {
            int kans = InMisdaad.MisdaadPlegen(id);
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
            InMisdaad.ZetInGevangenis(id, user_id);
        }

        public void GeefReward(int id, int user_id)
        {
            InMisdaad.GeefReward(id, user_id);
            int XP=  InMisdaad.KrijgXP(user_id);
            double Level = XP /= 100;

            
            int XPInt = (int)Math.Round(Level);
            InMisdaad.UpdateLevel(XPInt, user_id);
        }

        public void ZetInDatabase(int id, int user_id)
        {
            InMisdaad.ZetInDatabase(id, user_id);
        }
    }
}