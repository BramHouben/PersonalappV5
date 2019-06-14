using Dal.Interfaces;
using Dal.Memory;
using Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using System;

namespace Unittesten
{
    [TestClass]
    public class GebruikerUnit
    {
        public GebruikerUnit()
        {

        }
        private WinkelLogic winkelLogic = new WinkelLogic(new ItemMemory());
        private Admin admin = new Admin();
        private KerkLogic KerkLogic = new KerkLogic(new KerkMemory());
        private UserInlog newuser = new UserInlog();
        private UserMemory usermemory = new UserMemory();
        private AdminMemory adminMemory = new AdminMemory();
        private UserLogic userLogic = new UserLogic(new UserMemory());

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
    "Je mag geen lege strings doorsturen")]
        public void aanmakenUserFoutLegenVelden()
        {
          
            newuser.username = "";
            newuser.user_id = 4;
            newuser.ww = "test";
            userLogic.InsertenUser(newuser);
        }

        [TestMethod]
        
        public void aanmakenUserFoutDubbeleEmail()
        {
            // email bestaat al
            newuser.username = "test2";
            newuser.email = "test1@test.com";
          bool Check=  userLogic.InsertenUser(newuser);
            Assert.IsFalse(Check);
        }

        [TestMethod]

        public void aanmakenUserFoutDubbeleUsername()
        {
            // Username bestaat al
            newuser.username = "test1";
            newuser.email = "nieuwe email@test.com";
        bool check=    userLogic.InsertenUser(newuser);
            Assert.IsFalse(check);
        }

        [TestMethod]
        public void aanmakenUsergoed()
        {
            // Dit is een goede manier van registreren
            newuser.email = "UserMemory@test.com";
            newuser.username = "UserMemory";
            newuser.ww = "test";

         bool check =    userLogic.InsertenUser(newuser);
            Assert.IsTrue(check);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "Username bestaat niet en kan deze dus niet deleten")]
        public void DeleteUserfout()
        {
            //username id bestaat niet en daarom kan deze user niet verwijderd worden
            newuser.user_id = 0;

            userLogic.DeleteUser(newuser.user_id);
        }

        [TestMethod]
        public void isAdmin()
        {
            admin.admin_id = 1;
            admin.user_id = 5;
            bool Isadmin = adminMemory.IsAdminCheck(admin.user_id);
            Assert.IsTrue(Isadmin);
        }

        [TestMethod]
        public void isGeenAdmin()
        {
            admin.user_id = 3;
            bool Isadmin = adminMemory.IsAdminCheck(admin.user_id);
            Assert.IsFalse(Isadmin);
        }

        [TestMethod]
        public void FoutWachtwoord()
        {
            UserInlog foutWW = new UserInlog();
            foutWW.email = "test1@test.com";
            foutWW.username = "test1";
            foutWW.ww = "testfout";

            bool inloggenfout = userLogic.Inloggen(foutWW);
            Assert.IsFalse(inloggenfout);
        }

        [TestMethod]
        public void GoedeInlog()
        {
            UserInlog goedInlog = new UserInlog();
            goedInlog.email = "test1@test.com";
            goedInlog.username = "test1";
            goedInlog.ww = "Test123!";

            bool inloggengoed = userLogic.Inloggen(goedInlog);
            Assert.IsTrue(inloggengoed);
        }
        [TestMethod]

        public void BerichtPostengoed()
        {
            UserIngame goedInlog = new UserIngame();
            goedInlog.user_id = 2;

            Bericht bericht = new Bericht();
            bericht.Bericht_id = 1;
            bericht.Belangrijk_bericht = false;
            bericht.Bericht_inhoud = "test";
            bericht.Bericht_tijd = DateTime.Now;
            bericht.Bericht_titel = "test";
            bericht.Clan_id = 2;
            userLogic.BerichtPosten(1, goedInlog.user_id, bericht);
            Assert.AreEqual( 1, usermemory.berichten.Count);
        }
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

            bool Inloggengoed = userLogic.Inloggen(User);
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

            bool InloggenFout = userLogic.Inloggen(User);
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

            bool RegistratieGoed = userLogic.InsertenUser(User);
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
            bool RegistratieFout = userLogic.InsertenUser(User);
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
            bool RegistratieFout = userLogic.InsertenUser(User);
            Assert.IsFalse(RegistratieFout);
        }

        [TestMethod]
        public void DagelijkseInlogGoed()
        {
            UserIngame goedInlog = new UserIngame();
            goedInlog.user_id = 1;

            userLogic.KijkVoorDagelijkseReward(1);
        }

        [TestMethod]
        public void DagelijkseInlogFout()
        {
            //
            UserIngame goedInlog = new UserIngame();
            goedInlog.user_id = 2;

            userLogic.KijkVoorDagelijkseReward(1);
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
         "Bericht zonder titel")]
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
            userLogic.BerichtPosten(1, goedInlog.user_id, bericht);
        }
    }
}