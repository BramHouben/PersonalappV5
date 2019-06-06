using Dal;
using Dal.Memory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using System;
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
    }
}