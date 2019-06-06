using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dal.Interfaces;
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
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Winkel()
        {
            WinkelView winkel = new WinkelView();
            winkel.ItemList = winkelLogic.Vullist();

            return View(winkel);
        }

        public IActionResult ItemKopen(int item_id)
        {
            int user_id = (int)HttpContext.Session.GetInt32("user_id");

            if (winkelLogic.KanItemKopen(item_id, user_id) == true)
            {
                //winkelLogic.KoopItem(item_id);
                return RedirectToAction("Winkel");
            }
            else
            {
                return RedirectToAction("Winkel");
            }
        }
    }
}