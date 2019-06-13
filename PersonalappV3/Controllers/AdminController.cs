using Dal.Interfaces;
using Logic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Collections.Generic;
using System.Linq;

namespace PersonalappV3.Controllers
{
    public class AdminController : Controller
    {
        private Admin admin = new Admin();
        private AdminLogic adminlogic;

        public AdminController(IAanpassenGegevensUser Iuser)
        {
            adminlogic = new AdminLogic(Iuser);
        }

        public IActionResult AdminIndex()
        {
            return View(adminlogic.KrijgAlleUsers());
        }

        public IActionResult AdminItems()
        {
            return View(adminlogic.KrijgAlleUsersItems());
        }

        public IActionResult InloggenAdmin(UserIngame user)
        {
            admin.user_id = user.user_id;
            adminlogic.InloggenAdmin(admin);

            HttpContext.Session.SetInt32("user_id", admin.user_id);
            HttpContext.Session.SetInt32("Admin", admin.user_id);
            return RedirectToAction("AdminIndex");
        }

        public IActionResult VerwijderUser(int id)
        {
            adminlogic.VerwijderUser(id);
            return RedirectToAction("AdminIndex");
        }

        public List<UserIngame> alledata()
        {
            return adminlogic.KrijgAlleUsers();
        }

        public IActionResult EditUser(int id)
        {
            //UserIngame GeselecteerdeUser = adminlogic.AanpassenUser(id);
            UserIngame User = alledata().Where(s => s.user_id == id).FirstOrDefault();
            return View(User);
        }

        [HttpPost]
        public IActionResult EditUser(UserIngame User)
        {
            adminlogic.EditUser(User);
            return RedirectToAction("AdminIndex");
        }
    }
}