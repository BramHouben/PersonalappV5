using Dal.Context;
using Models;
using System;
using System.Collections.Generic;

namespace Logic
{
    public class MisdaadLogic
    {
        private MisdaadContext MisdaadContext = new MisdaadContext();

        public List<Misdaad> VulList()
        {
            return MisdaadContext.VulListMisdaden();
        }

        public bool PlegenMisdaad(int id)
        {
            int kans = MisdaadContext.MisdaadPlegen(id);
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
            MisdaadContext.ZetInGevangenis(id, user_id);
        }

        public void GeefReward(int id, int user_id)
        {
            MisdaadContext.GeefReward(id, user_id);
        }

        public void ZetInDatabase(int id, int user_id)
        {
            MisdaadContext.ZetInDatabase(id, user_id);
        }
    }
}