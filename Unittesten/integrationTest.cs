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

        [TestMethod]
        public void ToevoegenItemaanuser()
        {
            // Arrange
            WinkelLogic winkelLogic = new WinkelLogic(new ItemMemory());
            UserLogic userlogic = new UserLogic(new UserMemory());
            Item item = new Item
            {
                Item_id = 1,               
                Item_schade = 15,
                Item_naam = "Luchtbux",
                Item_Soort = "Geweer",
                Item_reputatie = "Luchtbux",
                
            };
            UserIngame User = new UserIngame()
            {
                user_id = 2025,
                ingameGeld = 200,
                itemlist = new List<Item>(),
            };

            // Act
            winkelLogic.KanItemKopen(item.Item_id, User.user_id);
            userlogic.Krijgendata(User);
            // Assert
            CollectionAssert.Contains(User.itemlist, item);
        }
    }
}
