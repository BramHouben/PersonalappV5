using System;
using System.Collections.Generic;
using System.Text;
using Models;

namespace Dal.Interfaces
{
    public interface IKerk
    {
        void GeefInfoVoorKerk(int user_id, Kerk kerk);
        void LevensToevoegen(int kerkid, int user_id);
    }
}
