using Dal.Interfaces;
using Logic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using PersonalappV3.Models;
using System;

namespace PersonalappV3.Controllers
{

    public class GameController : Controller
    {
        private KerkLogic kerklogic;
        private GevangenisView gevangenisVM;
        //private WinkelLogic winkelLogic = new WinkelLogic();
        private GevangenisLogic GevangenisLogic;
        private Gevangenis GevangenisModel = new Gevangenis();
        private MisdaadLogic misdaadlogic;
        private UserLogic userlogic;

        public GameController(IMisdaad inMisdaad, IKerk inKerk ,IGevangenis inGevangenis, InUser inUser)
        {
            GevangenisLogic = new GevangenisLogic(inGevangenis);
            kerklogic = new KerkLogic(inKerk);
            misdaadlogic = new MisdaadLogic(inMisdaad);
            userlogic = new UserLogic(inUser);
        }

        public ActionResult Index()
        {
            int user_id = (int)HttpContext.Session.GetInt32("user_id");
            if (GevangenisLogic.MagUserVrij(user_id) == false)
            {
                return RedirectToAction("Gevangenis", "Game");
            }
            else { 
            return View();
        }
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
                userlogic.HaalLevensEraf(user_id);
                misdaadlogic.ZetInGevangenis(id, user_id);
                return RedirectToAction("Gevangenis", "Game");
            }
        }

        public ActionResult BetaalBorg()
        {
            int user_id = (int)HttpContext.Session.GetInt32("user_id");
            
            if (GevangenisLogic.BetalenBorg(user_id/*, geld*/) == true)
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

        public IActionResult Kerk()
        {
            KerkView KerkVW = new KerkView();
            Kerk kerk = new Kerk();
            int user_id = (int)HttpContext.Session.GetInt32("user_id");
            kerklogic.GetInfoVoorKerk(user_id, kerk);
            DateTime tijdnu = DateTime.Now;
            var result = (int)kerk.Kerk_tijd.Subtract(tijdnu).TotalMinutes;
            if (kerk.Kerk_id == 0)
            {
                //opnieuw gegevens vragen 
                kerklogic.GetInfoVoorKerk(user_id, kerk);

            }
            //else
            //{
            KerkVW.Kerk_tijd = result;
                KerkVW.Levens_user = kerk.User_levens;
                KerkVW.Kerk_id = kerk.Kerk_id;
            //}

            return View(KerkVW);
        }

        public IActionResult LevensToevoegen(/*int kerkid*/)
        {
            int user_id = (int)HttpContext.Session.GetInt32("user_id");
            if(kerklogic.MagLevensToevoegen(user_id) == true)
            {
                kerklogic.LevensToevoegen(user_id);
                TempData.Remove("GeentijdZiekenhuis");
                return RedirectToAction("Kerk", "Game");
                
            }
            else
            {
                TempData["GeentijdZiekenhuis"] = "Wacht tot de timer is afgelopen!";
                return RedirectToAction("Kerk", "Game");
            }
            
        }
        //public IActionResult Winkel()
        //{
        //    WinkelView winkel = new WinkelView();
        //    winkel.ItemList = winkelLogic.Vullist();
        //    winkel.Geld = 500;
       
        //    return View(winkel);
        //}

        //public IActionResult ItemKopen(int item_id)
        //{
        //    int user_id = (int)HttpContext.Session.GetInt32("user_id");

        //    if (winkelLogic.KanItemKopen(item_id, user_id) == true)
        //    {
        //        //winkelLogic.KoopItem(item_id);
        //        return RedirectToAction("Winkel");
        //    }
        //    else
        //    {
        //        return RedirectToAction("Winkel");
        //    }
        //}
    
    }
}