using Logic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using PersonalappV3.Models;

namespace PersonalappV3.Controllers
{
    public class UserController : Controller
    {
        private UserLogic userlogic = new UserLogic();
        //private UserInlog userinlog = new UserInlog();
        private UserIngame IngameUser = new UserIngame();
        private AdminLogic AdminLogic = new AdminLogic();

        // GET: User
        [HttpGet]
        public ActionResult Registratie()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registratie(UserInlog User)
        {
            User.ww = userlogic.Hashwachtwoord(User.ww);
            if (userlogic.InsertenUser(User) == false)
            {
                ModelState.AddModelError("Email", "Email bestaat al");
                ModelState.AddModelError("Username", "Of gebruikersnaam bestaat");
            }
            else
            {
                return RedirectToAction("index", "home");
            }
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserInlog User)
        {
            if (userlogic.Inloggen(User) == true)
            {
                IngameUser.username = User.username;
                userlogic.Krijgendata(IngameUser);
                HttpContext.Session.SetInt32("user_id", IngameUser.user_id);
                HttpContext.Session.SetString("Username", User.username);

                if (AdminLogic.IsAdmin2(IngameUser.user_id))
                {
                    return RedirectToAction("InloggenAdmin", "Admin", new { IngameUser.user_id });
                }
                else
                {
                    userlogic.KijkVoorDagelijkseReward(IngameUser.user_id);

                    return RedirectToAction("index", "Game");
                }
            }
            else
            {
                return View();
            }
        }

        public ActionResult Uitloggen(UserInlog User)
        {
            HttpContext.Session.Clear();

            return RedirectToAction("index", "home");
        }

        public ActionResult Details()
        {
            IngameUser.username = HttpContext.Session.GetString("Username");
            //int id = (int)HttpContext.Session.GetInt32("user_id");
            userlogic.Krijgendata(IngameUser);
            return View(IngameUser);
        }

        // GET: User/Edit/5
        public ActionResult Edit(int user_id)
        {
            user_id = (int)HttpContext.Session.GetInt32("user_id");
            return View();
        }

        // GET: User/Delete/5
        public IActionResult Delete(int id)
        {
            userlogic.DeleteUser(id);
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Clan()
        {
            ClanView clans = new ClanView();
            IngameUser.username = HttpContext.Session.GetString("Username");
           
            userlogic.Krijgendata(IngameUser);
            if (IngameUser.clan_id == 0)
            {
                clans.ClanLijst = userlogic.KrijgenClans(clans.ClanLijst);
                //userlogic.KrijgenClans(clans.ClanLijst);
                return View(clans);
            }
            else
            {
                HttpContext.Session.SetInt32("Clan_id", IngameUser.clan_id);
                return RedirectToAction("Clan_info", "User");
            }
        }
        public IActionResult Clan_info()
        {
            int Clan_id = (int)HttpContext.Session.GetInt32("Clan_id");
            ClanView clans = new ClanView();
            clans.AantalClanLeden = userlogic.AantalClanLeden(Clan_id);
            clans.BerichtenLijst = userlogic.KrijgenBerichten(Clan_id);
            return View(clans);
        }


        [HttpPost]
        public IActionResult InvoerenClan(int Clan)
        {
           int user_id = (int)HttpContext.Session.GetInt32("user_id");
            userlogic.InvoerenClan(Clan, user_id);
            return RedirectToAction("Clan");
        }
       
        public IActionResult BerichtPosten()
        {
      
            return View();
        }

       [HttpPost]
        public IActionResult InvoegenBericht(BerichtView Niewbericht)
        {
            Bericht bericht = new Bericht();


            bericht.Bericht_inhoud = Niewbericht.Bericht_inhoud;
            bericht.Belangrijk_bericht = Niewbericht.Belangrijk_bericht;
            bericht.Bericht_titel = Niewbericht.Bericht_titel;
            //bericht.Belangrijk_bericht = HttpContext.Request.Form["Belangrijk_bericht"];
            int Clan_id = (int)HttpContext.Session.GetInt32("Clan_id");
            int user_id = (int)HttpContext.Session.GetInt32("user_id");
            userlogic.BerichtPosten(Clan_id, user_id, bericht);
            return RedirectToAction("Clan_info", "User");
        }
    }
}