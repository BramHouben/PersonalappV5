using Dal.Memory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using System;

namespace Unittesten
{
    [TestClass]
    public class GebruikerUnit
    {
        private UserInlog newuser = new UserInlog();
        private UserMemory usermemory = new UserMemory();

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
    "lege value mag niet.")]
        public void aanmakenUser()
        {

            usermemory.InsertenUser(newuser);
          

        }
    }
}