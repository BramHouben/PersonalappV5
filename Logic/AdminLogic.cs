using Dal.Interfaces;
using Dal.Repo;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic
{
    public class AdminLogic
    {
        //private UserInlog UserInlog;
        private AdminRepo AdminRepo = new AdminRepo();
        //public bool IsAdmin(int user_id, Admin admin)
        //{
        //    return AdminRepo.IsAdmin(user_id,  admin);
        //}

        public bool IsAdmin2(int userid)
        {
            return AdminRepo.IsAdmin2(userid);
        }

        public List<UserIngame> KrijgAlleUsers()
        {
            return AdminRepo.KrijgAlleUsers;
        }

        public void InloggenAdmin(Admin admin)
        {
            AdminRepo.IsAdmin(admin);
        }

        public void VerwijderUser(int user_id)
        {
            AdminRepo.VerwijderUser(user_id);
        }
    }
}
