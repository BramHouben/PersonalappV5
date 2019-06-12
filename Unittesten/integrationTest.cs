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


        [TestMethod]
        public void ToevoegenItemaanuser()
        {
            // Arrange
     
            Item item = new Item
            {
                Item_id = 1,               
                Item_schade = 15,
                Item_naam = "Luchtbux",
                Item_Soort = "Geweer",
                Item_reputatie = "Algemeen",
              
                
                
            };
            UserIngame User = new UserIngame()
            {
                user_id = 2025,
                ingameGeld = 800,
                itemlist = new List<Item>(),
            };

            // Act
            winkelLogic.KanItemKopen(item.Item_id, User.user_id);
            userlogic.Krijgendata(User);
            // Assert
            CollectionAssert.Contains(User.itemlist, item);
        }
        [TestMethod]
        public void KanItemKopen()
        {
            Item item = new Item
            {
                Item_id = 1,
                Item_schade = 15,
                Item_naam = "Luchtbux",
                Item_Soort = "Geweer",
                Item_reputatie = "Algemeen",



            };
            UserIngame User = new UserIngame()
            {
                user_id = 2025,
              
                itemlist = new List<Item>(),
            };
            bool KanItemKopen = winkelLogic.KanItemKopen(item.Item_id, User.user_id);


            Assert.IsTrue(KanItemKopen);  
        }
        [TestMethod]
        public void KanItemNietKopen()
        {
            Item item = new Item
            {
                Item_id = 1,
                Item_schade = 15,
                Item_naam = "Luchtbux",
                Item_Soort = "Geweer",
                Item_reputatie = "Algemeen",



            };
            UserIngame User = new UserIngame()
            {
                user_id = 2026,

                itemlist = new List<Item>(),
            };
            bool KanItemKopen = winkelLogic.KanItemKopen(item.Item_id, User.user_id);


            Assert.IsFalse(KanItemKopen);
        }

        [TestMethod]
        public void Inloggen()
        {
            UserInlog User = new UserInlog()
            {
                user_id = 2026,
                username = "Unit",
                email = "testVoorUnit@test.com",
                ww = "Unit",
            };

            bool Inloggengoed = userlogic.Inloggen(User);
            Assert.IsTrue(Inloggengoed);

        }

        [TestMethod]
        public void InloggenFout()
        {
            UserInlog User = new UserInlog()
            {
                user_id = 2026,
                username = "Unit",
                email = "testVoorUnit@test.com",
                ww = "FoutWachtwoord",
            };

            bool Inloggengoed = userlogic.Inloggen(User);
            Assert.IsFalse(Inloggengoed);

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
                user_id = 2026,
                username = "Unit",
                email = "testVoorUnit@test.com",
                ww = "FoutWachtwoord",
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
                user_id = 2026,
                username = "NietUnit",
                email = "testVoorUnit@test.com",
                ww = "FoutWachtwoord",
            };
            bool RegistratieFout = userlogic.InsertenUser(User);
            Assert.IsFalse(RegistratieFout);
        }
    }
    }
