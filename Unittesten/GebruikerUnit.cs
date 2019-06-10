using Dal;
using Dal.Memory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Unittesten
{
    [TestClass]
    public class GebruikerUnit
    {
        private UserInlog newuser = new UserInlog();
        private UserMemory usermemory = new UserMemory();

        [TestMethod]
        public void aanmakenUserFout()
        {
            try
            {
                newuser.email = "";
                newuser.username = "UserMemory";
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
        public void DeleteUserGoed()
        {

        }
        [TestMethod]
        public List<Item> itemlistGebruikergoed()
        {

        }
        [TestMethod]
        public bool FoutWachtwoord()
        {
            UserInlog foutWW = new UserInlog();
            foutWW.email = "UserMemory@test.com";
            foutWW.username = "UserMemory";
            foutWW.ww = "testfout";
          bool inloggenfout=  usermemory.Inloggen(foutWW.username, foutWW.ww);
            Assert.IsFalse(inloggenfout);
            return inloggenfout;
        }
        [TestMethod]
        public bool GoedeInlog()
        {
            UserInlog goedInlog = new UserInlog();
            goedInlog.email = "UserMemory@test.com";
            goedInlog.username = "UserMemory";
            goedInlog.ww = "test";
            bool inloggengoed = usermemory.Inloggen(goedInlog.username, goedInlog.ww);
            Assert.IsTrue(inloggengoed);
            return inloggengoed;
        }
        [TestMethod]
        public List<UserInlog> lijstMetUsers()
        {

        }


    }
}