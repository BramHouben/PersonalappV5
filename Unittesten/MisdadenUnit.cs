using Dal.Memory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Unittesten
{
    [TestClass]
   public class MisdadenUnit
    {

        private UserInlog newuser = new UserInlog();
        private MisdaadMemory misdaadMemory = new MisdaadMemory();

        [TestMethod]
        public bool MisdaadMislukt()
        {

            misdaadMemory.MisdaadPlegen(1);
        }
        [TestMethod]

        public bool MisdaadGelukt()
        {

        }
        [TestMethod]
        public int GeldGeven()
        {

        }
        [TestMethod]
        public List<Misdaad> VulListMisdaden()
        {

        }
        [TestMethod]
        public int XpGeven()
        {

        }

    }
}
