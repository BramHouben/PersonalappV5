using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dal.Interfaces
{
   public interface IMisdaad
    {
        List<Misdaad> VulListMisdaden();
        void ZetInDatabase(int id, int user_id);
        void GeefReward(int id, int user_id);
        void ZetInGevangenis(int id, int user_id);
        int MisdaadPlegen(int id);
        int KrijgXP(int user_id);
        
        void UpdateLevel(int XP, int user_id);
    }
}
