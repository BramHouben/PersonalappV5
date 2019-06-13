using Dal.Memory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using System;

namespace Unittesten
{
    [TestClass]
    public class GebruikerUnit
    {
        private Admin admin = new Admin();
        private UserInlog newuser = new UserInlog();
        private UserMemory usermemory = new UserMemory();
        private AdminMemory adminMemory = new AdminMemory();

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
    "Je mag geen lege strings doorsturen")]
        public void aanmakenUserFoutLegenVelden()
        {
            newuser.username = "";
            newuser.user_id = 4;
            newuser.ww = "test";
            usermemory.InsertenUser(newuser);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
    "Er mag geen dubbele string username of email de database in")]
        public void aanmakenUserFoutDubbeleEmail()
        {
            // email bestaat al
            newuser.username = "test2";
            newuser.email = "test1@test.com";
            usermemory.InsertenUser(newuser);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
 "Er mag geen dubbele string username of email de database in")]
        public void aanmakenUserFoutDubbeleUsername()
        {
            // Username bestaat al
            newuser.username = "test1";
            newuser.email = "nieuwe email@test.com";
            usermemory.InsertenUser(newuser);
        }

        [TestMethod]
        public void aanmakenUsergoed()
        {
            // Dit is een goede manier van registreren
            newuser.email = "UserMemory@test.com";
            newuser.username = "UserMemory";
            newuser.ww = "test";

            usermemory.InsertenUser(newuser);
            Assert.IsTrue(usermemory.userlist.Count == 4);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "Username bestaat niet en kan deze dus niet deleten")]
        public void DeleteUserfout()
        {
            //username id bestaat niet en daarom kan deze user niet verwijderd worden
            newuser.user_id = 0;

            usermemory.DeleteUser(newuser.user_id);
        }

        [TestMethod]
        public void isAdmin()
        {
            admin.admin_id = 1;
            admin.user_id = 5;
            bool Isadmin = adminMemory.IsAdminCheck(admin.user_id);
            Assert.IsTrue(Isadmin);
        }
        [TestMethod]
        public void isGeenAdmin()
        {
           
            admin.user_id = 3;
            bool Isadmin = adminMemory.IsAdminCheck(admin.user_id);
            Assert.IsFalse(Isadmin);
        }

        [TestMethod]
        public void FoutWachtwoord()
        {
            UserInlog foutWW = new UserInlog();
            foutWW.email = "test1@test.com";
            foutWW.username = "test1";
            foutWW.ww = "testfout";

            bool inloggenfout = usermemory.Inloggen(foutWW.username, foutWW.ww);
            Assert.IsTrue(inloggenfout);
        }

        [TestMethod]
        public void GoedeInlog()
        {
            UserInlog goedInlog = new UserInlog();
            goedInlog.email = "test1@test.com";
            goedInlog.username = "test1";
            goedInlog.ww = "Test123!";

            bool inloggengoed = usermemory.Inloggen(goedInlog.username, goedInlog.ww);
            Assert.IsFalse(inloggengoed);
        }
 
    }
}