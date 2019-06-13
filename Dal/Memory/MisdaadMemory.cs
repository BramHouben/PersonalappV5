using Dal.Interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dal.Memory
{
   public class MisdaadMemory : IMisdaad
    {
        public List<Gevangenis> Gevangenis = new List<Gevangenis>();
        public List<Misdaad> misdaden = new List<Misdaad>();
        private UserIngame userIngame = new UserIngame();
        public MisdaadMemory()
        {
            misdaden.Add(new Misdaad(1,"Snoep stelen van een kind",5,"Het kind let niet op en je steelt zijn snoep"));
        }

        public void GeefReward(int id, int user_id)
        {
            userIngame.xp += 50;
            userIngame.ingameGeld += 100;
        }

        public int KrijgXP(int user_id)
        {
            return 100;
        }

        public int MisdaadPlegen(int id)
        {
            return 100;
        }

        public void UpdateLevel(int XP, int user_id)
        {
            throw new NotImplementedException();
        }

        public List<Misdaad> VulListMisdaden()
        {
            misdaden.Add(new Misdaad(2, "Auto stelen", 1, "Je steelt een auto in de parkeergarage"));
            return misdaden;
        }

        public void ZetInDatabase(int id, int user_id)
        {
            throw new NotImplementedException();
        }

        public void ZetInGevangenis(int id, int user_id)
        {
            throw new NotImplementedException();
        }
    }
}
