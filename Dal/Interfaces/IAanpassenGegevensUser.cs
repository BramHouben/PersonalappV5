using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dal.Interfaces
{
    public interface IAanpassenGegevensUser
    {
        void IsAdmin(Admin admin);
        bool IsAdminCheck(int userid);
        List<UserIngame> KrijgAlleUsers();
        void VerwijderUser(int user_id);
        //UserIngame AanpassenUser(int id);
        void EditUser(UserIngame user);
        List<UserIngame> KrijgAlleUsersItems();
    }
}
