using Dal.Memory;
using Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Unittesten
{[TestClass]
  public  class KerkIntergration
    {
        private UserIngame newuser = new UserIngame();
        private KerkLogic kerkLogic = new KerkLogic(new KerkMemory());

        [TestMethod]
        public void MagLevensToevoegenTrue()
        {

            newuser.user_id = 1;

          bool goed= kerkLogic.MagLevensToevoegen(newuser.user_id);
            Assert.IsTrue(goed);
        }
        [TestMethod]
        public void MagLevensToevoegenFalse ()
        {

            newuser.user_id = 2;

            bool fout = kerkLogic.MagLevensToevoegen(newuser.user_id);
            Assert.IsFalse(fout);
        }

        [TestMethod]
        public void krijginfokerk()
        {
            newuser.user_id = 1;
            Kerk kerk = new Kerk();
            kerkLogic.GetInfoVoorKerk(newuser.user_id,kerk);
            Assert.AreEqual(100,kerk.User_levens);
        }
        [TestMethod]
        public void krijginfofout()
        {
            newuser.user_id = 2;
            Kerk kerk = new Kerk();
            kerkLogic.GetInfoVoorKerk(newuser.user_id, kerk);
            Assert.AreNotEqual(100, kerk.User_levens);
        }

    }
}
