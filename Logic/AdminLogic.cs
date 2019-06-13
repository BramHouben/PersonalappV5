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
     
     
        private IAanpassenGegevensUser adminContext;
      

   

        public AdminLogic(IAanpassenGegevensUser iuser)
        {
            adminContext = iuser ;
        }

   

        public bool IsAdminCheck(int userid)
        {
            return adminContext.IsAdminCheck(userid);
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
