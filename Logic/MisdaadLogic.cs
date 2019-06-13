using Dal.Interfaces;
using Model;
using Models;
using System;
using System.Collections.Generic;

namespace Logic
{
    public class MisdaadLogic
    {
        private IMisdaad InMisdaad;

        public MisdaadLogic(IMisdaad imisdaad)
        {
            InMisdaad = imisdaad;
        }

        public List<Misdaad> VulList()
        {
            return InMisdaad.VulListMisdaden();
        }

        public bool PlegenMisdaad(int id, UserIngame user)
        {
            int kans = InMisdaad.MisdaadPlegen(id);
            if (KansBerekenen(kans, user) == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool KansBerekenen(int kans, UserIngame user)
        {
            double Kansfactor = 80;
            bool gelukt = false;
            Random rnd = new Random();
            if (user.itemlist.Count > 0)
            {
                foreach (Item item in user.itemlist)
                {
                    double itemschade = item.Item_schade;
                    itemschade /= 100;
                    Kansfactor -= itemschade;
                }
            }
            for (int i = 0; i < kans; i++)
            {
                int nummer = rnd.Next(1, 100);
                if (nummer >= Kansfactor)
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
            int XP = InMisdaad.KrijgXP(user_id);
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