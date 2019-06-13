using Dal.Interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Dal.Memory
{
    public class AdminMemory : IAanpassenGegevensUser
    {
       

        public void EditUser(UserIngame user)
        {
            throw new NotImplementedException();
        }

        public void IsAdmin(Admin admin)
        {
            throw new NotImplementedException();
        }

        public bool IsAdmin2(int userid)
        {
           
        }
        private bool GetAdminInfo2(int user_id)
        {
          
        }
        public List<UserIngame> KrijgAlleUsers()
        {
            throw new NotImplementedException();
        }

        public List<UserIngame> KrijgAlleUsersItems()
        {
            throw new NotImplementedException();
        }

        public void VerwijderUser(int user_id)
        {
            throw new NotImplementedException();
        }
    }
}
