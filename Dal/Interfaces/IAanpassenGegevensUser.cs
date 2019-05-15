using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dal.Interfaces
{
    public interface IAanpassenGegevensUser
    {
        void IsAdmin(Admin admin);
        bool IsAdmin2(int userid);
    }
}
