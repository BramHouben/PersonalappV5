using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Logic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace PersonalappV3.Controllers
{
    public class AdminController : Controller
    {
        private Admin admin = new Admin();
        private AdminLogic adminlogic = new AdminLogic();
        public IActionResult AdminIndex()
        {
            return View();
        }

        public IActionResult InloggenAdmin(UserInlog user)
        {
            admin.user_id = user.user_id;
            adminlogic.InloggenAdmin(admin);

            HttpContext.Session.SetInt32("user_id", admin.user_id);
            return View("AdminIndex");
        }

    }
}