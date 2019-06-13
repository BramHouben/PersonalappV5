using Dal.Memory;
using Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Unittesten
{
    [TestClass]
   public class Misdadenintegration
    {
       private MisdaadLogic misdaadLogic = new MisdaadLogic(new MisdaadMemory());
        private UserIngame newuser = new UserIngame();
      

        [TestMethod]
        public void MisdaadGelukt()
        {
            newuser.level = 1;
            newuser.xp = 100;
            newuser.ingameGeld = 500;
            newuser.itemlist = new List<Item>();
            newuser.itemlist.Add(new  Item(1, "Luchtbux", 15, "Een Luchtbux om duiven neer te schieten", "Geweer", "Bekend", 100, 1, DateTime.Now, true));
          Assert.IsTrue(  misdaadLogic.PlegenMisdaad(1, newuser));         
        }
        [TestMethod]

        public void MisdaadMislukt()
        {
            newuser.level = 1;
            newuser.xp = 100;
            newuser.ingameGeld = 500;
            newuser.itemlist = new List<Item>();
            // 0 procent kans
            Assert.IsFalse(misdaadLogic.KansBerekenen(0, newuser));
        }
       

    }
}
