using Dal.Interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dal.Memory
{
   public class MisdaadMemory : IMisdaad
    {
        public void GeefReward(int id, int user_id)
        {
            throw new NotImplementedException();
        }

        public int KrijgXP(int user_id)
        {
            throw new NotImplementedException();
        }

        public int MisdaadPlegen(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateLevel(int XP, int user_id)
        {
            throw new NotImplementedException();
        }

        public List<Misdaad> VulListMisdaden()
        {
            throw new NotImplementedException();
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
