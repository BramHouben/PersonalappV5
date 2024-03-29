﻿using Dal.Interfaces;
using Logic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonalappV3.Models;

namespace PersonalappV3.Controllers
{
    public class WinkelController : Controller
    {
        private WinkelLogic winkelLogic;

        public WinkelController(IWinkel inWinkel/*, InItem inItem*/)
        {
            winkelLogic = new WinkelLogic(inWinkel/*, inItem*/);
        }

        public bool CheckInlog()
        {
            bool user_id = HttpContext.Session.GetInt32("user_id").HasValue;
            if (user_id == false)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}
        public IActionResult Winkel()
        {
            if (CheckInlog() == false)
            {
                return RedirectToAction("Login", "User");
            }
            WinkelView winkel = new WinkelView();
            winkel.ItemList = winkelLogic.Vullist();

            return View(winkel);
        }

        public IActionResult ItemKopen(int item_id)
        {
            int user_id = (int)HttpContext.Session.GetInt32("user_id");

            if (winkelLogic.KanItemKopen(item_id, user_id) == true)
            {
                TempData["ItemWelBetalen"] = "Je hebt het item gekocht!";
                //winkelLogic.KoopItem(item_id);
                return RedirectToAction("Winkel");
            }
            else
            {
                TempData["ItemNietKopen"] = "Je kunt het item niet betalen!";
                return RedirectToAction("Winkel");
            }
        }
    }
}