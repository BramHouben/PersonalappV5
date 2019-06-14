using Dal.Memory;
using Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Models;
using System;
using System.Collections.Generic;

namespace Unittesten
{
    [TestClass]
    public class MisdaadUnit
    {
        private MisdaadMemory misdaadmemory = new MisdaadMemory();
        private MisdaadLogic misdaadLogic = new MisdaadLogic(new MisdaadMemory());

        [TestMethod]
        public void KansberekenIsGroot()
        {
            UserIngame user = new UserIngame();
            user.itemlist = new List<Item>();
            user.itemlist.Add(new Item(1, "Luchtbux", 15, "Een Luchtbux om duiven neer te schieten", "Geweer", "Bekend", 100, 1, DateTime.Now, true));
            
            Assert.IsTrue(misdaadLogic.KansBerekenen(100, user));
        }

        [TestMethod]
        public void KansberekenIsKlein()
        {
            UserIngame user = new UserIngame();
            user.itemlist = new List<Item>();
            
            Assert.IsFalse(misdaadLogic.KansBerekenen(0, user));
        }
    }
}