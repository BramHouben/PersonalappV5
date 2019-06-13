using Models;
using System;

namespace Dal.Interfaces
{
    public interface IKerk
    {
        void GeefInfoVoorKerk(int user_id, Kerk kerk);

        void LevensToevoegen(int user_id);

        DateTime KrijgTijd(int kerkid);

        int KrijgLevensInfo(int user_id);
    }
}