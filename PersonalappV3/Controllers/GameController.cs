﻿using Dal.Interfaces;
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
        private KerkLogic kerklogic;
        private GevangenisView gevangenisVM;

        private GevangenisLogic GevangenisLogic;
        private Gevangenis GevangenisModel = new Gevangenis();
        private MisdaadLogic misdaadlogic;
        private UserLogic userlogic;

        public GameController(IMisdaad inMisdaad, IKerk inKerk, IGevangenis inGevangenis, InUser inUser)
        {
            GevangenisLogic = new GevangenisLogic(inGevangenis);
            kerklogic = new KerkLogic(inKerk);
            misdaadlogic = new MisdaadLogic(inMisdaad);
            userlogic = new UserLogic(inUser);
        }

        //  user terugstruren niet ingelogd
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

        public ActionResult Index()
        {
            if (CheckInlog() == false)
            {
                return RedirectToAction("Login", "User");
            }
            int user_id = (int)HttpContext.Session.GetInt32("user_id");
            if (GevangenisLogic.MagUserVrij(user_id) == false)
            {
                return RedirectToAction("Gevangenis", "Game");
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult Gevangenis()
        {
            if (CheckInlog() == false)
            {
                return RedirectToAction("Login", "User");
            }
            gevangenisVM = new GevangenisView();
            int user_id = (int)HttpContext.Session.GetInt32("user_id");

            GevangenisModel.User_id = user_id;
            GevangenisLogic.KrijgenGegevens(GevangenisModel);
            gevangenisVM.Borg = GevangenisModel.Borg;
            gevangenisVM.Tijd_vast = GevangenisModel.Tijd_vast;
            gevangenisVM.Gevangenis_id = GevangenisModel.Gevangenis_id;
            if (gevangenisVM.Gevangenis_id == 0)
            {
                TempData["ZitNietInGevangenis"] = "Je zit momenteel niet in de gevangenis!";
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
            UserIngame userIngame = new UserIngame();
            userIngame.username = HttpContext.Session.GetString("Username");
            userIngame.user_id = (int)HttpContext.Session.GetInt32("user_id");
            string Stringid = form["misdaden"];
            int id = Convert.ToInt32(Stringid);
            userlogic.Krijgendata(userIngame);
            if (misdaadlogic.PlegenMisdaad(id, userIngame) == true)
            {
                misdaadlogic.GeefReward(id, userIngame.user_id);
                misdaadlogic.ZetInDatabase(id, userIngame.user_id);
                TempData["MisdaadGelukt"] = "De misdaad is gelukt!";
                return RedirectToAction("Index", "Game");
            }
            else
            {
                userlogic.HaalLevensEraf(userIngame.user_id);
                misdaadlogic.ZetInGevangenis(id, userIngame.user_id);
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
            if (CheckInlog() == false)
            {
                return RedirectToAction("Login", "User");
            }
            int user_id = (int)HttpContext.Session.GetInt32("user_id");
            if (GevangenisLogic.CheckUserVast(user_id) == true)
            {
                return RedirectToAction("Gevangenis");
            }
            else if (GevangenisLogic.GenoegLevens(user_id) == true)
            {
                TempData["WeinigLevens"] = "Je hebt te weinig levens om een misdaad te plegen. Ga naar het ziekenhuis!";
                return RedirectToAction("Index");
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
            if (CheckInlog() == false)
            {
                return RedirectToAction("Login", "User");
            }
            KerkView KerkVW = new KerkView();
            Kerk kerk = new Kerk();
            int user_id = (int)HttpContext.Session.GetInt32("user_id");
            kerklogic.GetInfoVoorKerk(user_id, kerk);
            DateTime tijdnu = DateTime.Now;
            int result = (int)kerk.Kerk_tijd.Subtract(tijdnu).TotalMinutes;
            if (result < 0)
            {
                result = 0;
            }
            if (kerk.Kerk_id == 0)
            {
                //  opnieuw gegevens vragen fixt het 0 probleem
                kerklogic.GetInfoVoorKerk(user_id, kerk);
            }

            KerkVW.Kerk_tijd = result;
            KerkVW.Levens_user = kerk.User_levens;
            KerkVW.Kerk_id = kerk.Kerk_id;

            return View(KerkVW);
        }

        public IActionResult LevensToevoegen(/*int kerkid*/)
        {
            int user_id = (int)HttpContext.Session.GetInt32("user_id");
            if (kerklogic.MagLevensToevoegen(user_id) == true)
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
    }
}