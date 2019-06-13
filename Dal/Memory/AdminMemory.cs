using Dal.Interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Dal.Memory
{
    public class AdminMemory : IAanpassenGegevensUser
    {
        public List<Admin> Admins = new List<Admin>();
       public AdminMemory()
        {
            Admin admin = new Admin();
            admin.admin_id = 1;
            admin.user_id = 5;
            admin.username = "Admin";
            admin.email = "admin@admin.nl";
            Admins.Add(admin);
        }

        public void EditUser(UserIngame user)
        {
            throw new NotImplementedException();
        }

        public void IsAdmin(Admin admin)
        {
            throw new NotImplementedException();
        }

        public bool IsAdminCheck(int userid)
        {
            if (Admins.Any(x => x.user_id == userid))
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        private bool GetAdminInfo2(int user_id)
        {
            throw new NotImplementedException();

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
