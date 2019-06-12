using Dal.Context;
using Dal.Interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic
{
    public class AdminLogic
    {
        //private UserInlog UserInlog;
     
        private IAanpassenGegevensUser adminContext;
      

        //public AdminLogic(IAanpassenGegevensUser iuser)
        //{
        //    AdminRepo = new AdminRepo(iuser);
        //}

        public AdminLogic(IAanpassenGegevensUser iuser)
        {
            adminContext = iuser ;
        }

        //public bool IsAdmin(int user_id, Admin admin)
        //{
        //    return AdminRepo.IsAdmin(user_id,  admin);
        //}

        public bool IsAdmin2(int userid)
        {
            return adminContext.IsAdmin2(userid);
        }

        public List<UserIngame> KrijgAlleUsers()
        {
            return adminContext.KrijgAlleUsers();
        }

        public List<UserIngame> KrijgAlleUsersItems()
        {
            return adminContext.KrijgAlleUsersItems();
        }

        public void InloggenAdmin(Admin admin)
        {
            adminContext.IsAdmin(admin);
        }

        public void VerwijderUser(int user_id)
        {
            adminContext.VerwijderUser(user_id);
        }

        //public UserIngame AanpassenUser(int id)
        //{
        //  return AdminRepo.AanpassenUser(id);
        //}

        public void EditUser(UserIngame user)
        {
            adminContext.EditUser(user);
        }
    }
}
