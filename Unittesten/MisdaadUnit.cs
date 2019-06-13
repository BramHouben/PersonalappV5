using Dal.Memory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Unittesten
{
    [TestClass]
    public  class MisdaadUnit { 
    
        private MisdaadMemory misdaadmemory = new MisdaadMemory();
        

        [TestMethod]
        public void GeldGeven()
        {
            misdaadmemory.GeefReward(5,1);
        }

        [TestMethod]
        public void XpGeven()
        {
         Assert.AreEqual(misdaadmemory.KrijgXP(5),100);
        }
    }
}
