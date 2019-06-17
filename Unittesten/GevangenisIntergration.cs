using Dal.Memory;
using Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Unittesten
{
    [TestClass]
  public  class GevangenisIntergration
    {
        private UserIngame newuser = new UserIngame();
        private GevangenisLogic gevangenisLogic = new GevangenisLogic(new GevangenisMemory());

        [TestMethod]
        public void CheckVast()
        {
            Gevangenis gevangenis = new Gevangenis();
            gevangenis.User_id = 1;

            gevangenisLogic.KrijgenGegevens(gevangenis);

            Assert.AreEqual(5, gevangenis.Gevangenis_id);

        }
        [TestMethod]
        public void CheckNietvast()
        {
            Gevangenis gevangenis = new Gevangenis();
            gevangenis.User_id = 2;

            gevangenisLogic.KrijgenGegevens(gevangenis);

            Assert.AreEqual(0,gevangenis.Gevangenis_id);

        }

    }
}
