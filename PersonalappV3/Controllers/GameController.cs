using Logic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using PersonalappV3.Models;
using System;

namespace PersonalappV3.Controllers
{
    public class GameController : Controller
    {
        private GevangenisView gevangenisVM;
        private WinkelLogic winkelLogic = new WinkelLogic();
        private GevangenisLogic GevangenisLogic = new GevangenisLogic();
        private Gevangenis GevangenisModel = new Gevangenis();
        private MisdaadLogic misdaadlogic = new MisdaadLogic();

        public ActionResult Index()
        {
            return View();
        }

        //public ActionResult Gevangenis()
        //{
        //    return View();
        //}

        [HttpGet]
        public ActionResult Gevangenis()
        {
            gevangenisVM = new GevangenisView();
            int user_id = (int)HttpContext.Session.GetInt32("user_id");

            GevangenisModel.User_id = user_id;
            GevangenisLogic.KrijgenGegevens(GevangenisModel);
            gevangenisVM.Borg = GevangenisModel.Borg;
            gevangenisVM.Tijd_vast = GevangenisModel.Tijd_vast;
            gevangenisVM.Gevangenis_id = GevangenisModel.Gevangenis_id;
            if (gevangenisVM.Gevangenis_id == 0)
            {
                return RedirectToAction("index");
            }
            else
            {
                return View(gevangenisVM);
            }
        }

        [HttpPost]
        public IActionResult PlegenMisdaad(MisdaadView misdaad, IFormCollection form)
        {
            int user_id = (int)HttpContext.Session.GetInt32("user_id");
            string Stringid = form["misdaden"];
            int id = Convert.ToInt32(Stringid);
            if (misdaadlogic.PlegenMisdaad(id) == true)
            {
                misdaadlogic.GeefReward(id, user_id);
                misdaadlogic.ZetInDatabase(id, user_id);
                TempData["MisdaadGelukt"] = "De misdaad is gelukt!";
                return RedirectToAction("Index", "Game");
            }
            else
            {
                misdaadlogic.ZetInGevangenis(id, user_id);
                return RedirectToAction("Gevangenis", "Game");
            }
        }

        public ActionResult BetaalBorg(int geld)
        {
            int user_id = (int)HttpContext.Session.GetInt32("user_id");

            if (GevangenisLogic.BetalenBorg(user_id, geld) == true)
            {
                TempData["BorgBetaald"] = "Borg betaald!";
                ModelState.AddModelError("BorgBetaald", "Je hebt de borg betaald!");
                return RedirectToAction("Index", "Game");
            }
            else
            {
                TempData["Borg"] = "Je kunt de borg niet betalen!";
                ModelState.AddModelError("Borg", "Je kunt de borg niet betalen!");
                return RedirectToAction("Gevangenis");
            }
        }

        public ActionResult MisdaadPlegen()
        {
            int user_id = (int)HttpContext.Session.GetInt32("user_id");
            if (GevangenisLogic.CheckUserVast(user_id) == true)
            {
                return RedirectToAction("Gevangenis");
            }
            else
            {
                MisdaadView model = new MisdaadView();
                model.MisdadenList = misdaadlogic.VulList();

                return View(model);
            }
        }

        public ActionResult Winkel()
        {
            WinkelView winkel = new WinkelView();
            winkel.ItemList = winkelLogic.Vullist();
            return View(winkel);
        }
    }
}