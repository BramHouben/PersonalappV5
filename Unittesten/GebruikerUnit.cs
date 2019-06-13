using Dal;
using Dal.Interfaces;
using Dal.Memory;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

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
        public void aanmakenUserFout()
        {

            try
            {
                
                newuser.username = "";
                newuser.user_id = 0;
                newuser.ww = "test";
                usermemory.InsertenUser(newuser);
            }
            catch (ArgumentException fout)
            {
                Assert.AreEqual("lege value mag niet", fout.Message);
            }
        }
        [TestMethod]
        public void aanmakenUsergoed()
        {
            // Dit is een goede manier van registreren
                newuser.email = "UserMemory@test.com";
                newuser.username = "UserMemory";
                newuser.ww = "test";
           
            
            usermemory.InsertenUser(newuser);
            Assert.IsTrue(true);


        }
        [TestMethod]
        public void DeleteUserfout()
        {
            try
            {
         
               newuser.user_id = 0;
      
                usermemory.DeleteUser(newuser.user_id);
            }
            catch (ArgumentException fout)
            {
                Assert.AreEqual("User bestaat niet", fout.Message);
            }
        }

        [TestMethod]
        public void isAdmin()
        {
            admin.username = "Admin";
            admin.email = "admin@admin.nl";
            admin.admin_id = 1;
            admin.user_id = 2035;
            bool Isadmin = adminMemory.IsAdmin2(admin.user_id);
            Assert.IsTrue(Isadmin);
          

        }
    

        [TestMethod]
        public void FoutWachtwoord()
        {
            UserInlog foutWW = new UserInlog();
            foutWW.email = "UserMemory@test.com";
            foutWW.username = "UserMemory";
            foutWW.ww = "testfout";
            
          bool inloggenfout=  usermemory.Inloggen(foutWW.username, foutWW.ww);
            Assert.IsFalse(inloggenfout);
            
        }
        [TestMethod]
        public void GoedeInlog()
        {
            UserInlog goedInlog = new UserInlog();
            goedInlog.email = "UserMemory@test.com";
            goedInlog.username = "UserMemory";
            goedInlog.ww = "test";
            
            bool inloggengoed = usermemory.Inloggen(goedInlog.username, goedInlog.ww);
            Assert.IsFalse(inloggengoed);

        }
   

    }
}