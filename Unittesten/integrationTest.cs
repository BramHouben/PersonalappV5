using Dal.Context;
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
    public class integrationTest
    {
        WinkelLogic winkelLogic = new WinkelLogic(new ItemMemory());
        UserLogic userlogic = new UserLogic(new UserMemory());
        KerkLogic KerkLogic = new KerkLogic(new KerkMemory());


        [TestMethod]
        public void KanItemKopen()
        {

            bool KanItemKopen = winkelLogic.KanItemKopen(1, 1);


            Assert.IsTrue(KanItemKopen);
        }
        [TestMethod]
        public void KanItemNietKopen()
        {

            bool KanItemKopen = winkelLogic.KanItemKopen(1, 2);


            Assert.IsFalse(KanItemKopen);
        }

        [TestMethod]
        public void Inloggen()
        {
            UserInlog User = new UserInlog()
            {
                user_id = 1,
                username = "test1",
                email = "test1@test.com",
                ww = "Test123!",
            };

            bool Inloggengoed = userlogic.Inloggen(User);
            // geeft false terug want er 
            Assert.IsTrue(Inloggengoed);

        }

        [TestMethod]
        public void InloggenFout()
        {
            UserInlog User = new UserInlog()
            {
                user_id = 1,
                username = "test1",
                email = "test1@test.com",
                ww = "FoutWachtwoord",
            };

            bool InloggenFout = userlogic.Inloggen(User);
            Assert.IsFalse(InloggenFout);

        }

        [TestMethod]
        public void registratieGoed()
        {
            UserInlog User = new UserInlog()
            {

                username = "NewGebruiker",
                email = "NewGebruiker@test.com",
                ww = "NewGebruiker",
            };

            bool RegistratieGoed = userlogic.InsertenUser(User);
            Assert.IsTrue(RegistratieGoed);
        }

        [TestMethod]
        public void RegistratieFoutZelfdeUsername()
        {
            // unit gebruikersnaam bestaat al
            UserInlog User = new UserInlog()
            {
                user_id = 2,
                username = "test1",
                email = "test2@test.com",
                ww = "Test123!",
            };
            bool RegistratieFout = userlogic.InsertenUser(User);
            Assert.IsFalse(RegistratieFout);
        }
        [TestMethod]
        public void RegistratieFoutZelfdeEmail()
        {
            // unit gebruikersnaam bestaat al
            UserInlog User = new UserInlog()
            {
                user_id = 2,
                username = "test2",
                email = "test1@test.com",
                ww = "Test123!",
            };
            bool RegistratieFout = userlogic.InsertenUser(User);
            Assert.IsFalse(RegistratieFout);
        }
        [TestMethod]
        public void DagelijkseInlogGoed()
        {
            UserIngame goedInlog = new UserIngame();
            goedInlog.user_id = 1;

            userlogic.KijkVoorDagelijkseReward(1);

        }
        [TestMethod]
        public void DagelijkseInlogFout()
        {
            //
            UserIngame goedInlog = new UserIngame();
            goedInlog.user_id = 2;

            userlogic.KijkVoorDagelijkseReward(1);

        }

        [TestMethod]
        public void Geeflevens()
        {
            //
            UserIngame goedInlog = new UserIngame();
            goedInlog.user_id = 1;

            Assert.IsTrue(KerkLogic.MagLevensToevoegen(goedInlog.user_id));

        }
        [TestMethod]
        public void GeefNietlevens()
        {
            //
            UserIngame goedInlog = new UserIngame();
            goedInlog.user_id = 2;

            Assert.IsFalse(KerkLogic.MagLevensToevoegen(goedInlog.user_id));

        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
         "Er mag geen dubbele string username of email de database in")]
        public void BerichtPostenFout()
        {
            
            UserIngame goedInlog = new UserIngame();
            goedInlog.user_id = 2;

            Bericht bericht = new Bericht();
            bericht.Bericht_id = 1;
            bericht.Belangrijk_bericht = false;
            bericht.Bericht_inhoud = "";
            bericht.Bericht_tijd = DateTime.Now;
            bericht.Bericht_titel = "";
            bericht.Clan_id = 2;
            userlogic.BerichtPosten(1, goedInlog.user_id, bericht);

        }
    }
}