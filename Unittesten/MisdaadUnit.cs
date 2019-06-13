using Dal.Memory;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unittesten
{
    [TestClass]
    public class MisdaadUnit
    {
        private MisdaadMemory misdaadmemory = new MisdaadMemory();

        [TestMethod]
        public void GeldGeven()
        {
            misdaadmemory.GeefReward(5, 1);
        }

        [TestMethod]
        public void XpGeven()
        {
            Assert.AreEqual(misdaadmemory.KrijgXP(5), 100);
        }
    }
}