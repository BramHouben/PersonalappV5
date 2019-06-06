using System;
using System.Collections.Generic;
using System.Text;
using Models;

namespace Dal.Interfaces
{
    public interface IGevangenis
    {
        void KrijgGegevens(Gevangenis gevangenis);
        bool CheckUserVast(int user_id);
        bool MagUserVrij(int user_id);
        int CheckGeldUser(int user_id);
        void BetalenBorg(int bedragOver, int user_id);
    }
}
