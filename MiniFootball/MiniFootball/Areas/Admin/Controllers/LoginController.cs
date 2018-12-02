using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MiniFootball.Models;
using System.Web.Helpers;

namespace MiniFootball.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        Mini_FootballEntities db = new Mini_FootballEntities();
        // GET: Admin/Login
        [ActionName("Index")]
        public ActionResult GetLogin()
        {
            return View();
        }

        // POST: Admin/Login

        [ActionName("Index")]
        [HttpPost]
        public ActionResult PostLogin(Admin_settings admin)
        {
            Admin_settings Admin = db.Admin_settings.First();

            if(Admin.Email == admin.Email && Crypto.VerifyHashedPassword(Admin.Password , admin.Password))              
                {
                Session["AdminLogged"] = Admin;
                
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.message = "Email və ya şifrə yanlişdir";
                return RedirectToAction("Index","Login");
            }
        }

        public ActionResult Logout()
        {
            Session["AdminLogged"] = null;
            return RedirectToAction("Index");
        }
    }
}