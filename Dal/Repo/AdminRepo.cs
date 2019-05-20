using Dal.Context;
using Dal.Interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dal.Repo
{
    public class AdminRepo
    {
        private readonly IAanpassenGegevensUser IAanpassenGegevensUser;
        public AdminRepo()
        {
            IAanpassenGegevensUser = new AdminContext();
        }
        public List<UserIngame> KrijgAlleUsers => IAanpassenGegevensUser.KrijgAlleUsers();

        public void IsAdmin(Admin admin) => IAanpassenGegevensUser.IsAdmin(admin);

        public bool IsAdmin2(int userid) => IAanpassenGegevensUser.IsAdmin2(userid);

        public void VerwijderUser(int user_id) => IAanpassenGegevensUser.VerwijderUser(user_id);

        public UserIngame AanpassenUser(int id) => IAanpassenGegevensUser.AanpassenUser(id);
    }
}
