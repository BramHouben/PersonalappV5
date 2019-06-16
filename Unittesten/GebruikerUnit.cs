using Dal.Memory;
using Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Unittesten
{
    [TestClass]
  public  class GebruikerUnit
    {
        private Admin admin = new Admin();
        private AdminLogic adminLogic  = new AdminLogic(new AdminMemory());


        [TestMethod]
        
        public void isAdmin()
        {

            admin.admin_id = 1;
            admin.user_id = 5;
            bool Isadmin = adminLogic.IsAdminCheck(admin.user_id);
            Assert.IsTrue(Isadmin);
        }

        [TestMethod]
        public void isGeenAdmin()
        {
            admin.user_id = 3;
            bool Isadmin = adminLogic.IsAdminCheck(admin.user_id);
            Assert.IsFalse(Isadmin);
        }
    }
}
