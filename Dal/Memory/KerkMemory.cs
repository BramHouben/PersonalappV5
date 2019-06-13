using Dal.Interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dal.Memory
{
    public class KerkMemory : IKerk
    {
        public void GeefInfoVoorKerk(int user_id, Kerk kerk)
        {
            throw new NotImplementedException();
        }

        public int KrijgLevensInfo(int user_id)
        {
            throw new NotImplementedException();
        }

        public DateTime KrijgTijd(int kerkid)
        {
            DateTime tijd = DateTime.Now;
            if (kerkid ==1)
            {
                tijd = new DateTime(2019, 1, 1);
            }
      
            return tijd;
        }

        public void LevensToevoegen(int user_id)
        {
            throw new NotImplementedException();
        }
    }
}
