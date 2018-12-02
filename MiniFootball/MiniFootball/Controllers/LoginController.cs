using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MiniFootball.Models;
using System.Web.Helpers;

namespace MiniFootball.Controllers
{
    public class LoginController : Controller
    {
        Mini_FootballEntities db = new Mini_FootballEntities();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string Email, string Password)
        {
            Player loggedPlayer = db.Players.FirstOrDefault(p => p.Email == Email);

            if(loggedPlayer == null)
            {
                ViewBag.Error = "Email Yanlişdir";
                return View();
            }
            if(Crypto.VerifyHashedPassword(loggedPlayer.Password , Password))
            {
                Session["LoggedPlayer"] = loggedPlayer;

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Error = "Şifrə Yanlişdir";
                return View();
            }

            //Player currentPlayer = db.Players.FirstOrDefault(r => r.Email == Email && r.Password == Password);
            //if (currentPlayer != null)
            //{
            //    Session["LoggedPlayer"] = currentPlayer;

            //    return RedirectToAction("Index", "Home");
            //}
            //else
            //{
            //    ViewBag.Error = "Email və ya şifrə yalnişdir";
            //    return View();
            //}
        }

        public ActionResult LogOut()
        {
            Session["LoggedPlayer"] = null;

            return RedirectToAction("Index", "Home");
        }

        //Register GET
        [ActionName("Register")]
        public ActionResult GetRegister()
        {
            return View();
        }

        //Register POST
        [ActionName("Register")]
        [HttpPost]
        public ActionResult PostRegister(string Name , string Lastname , string Email , string Password , string ConPass)
        {

            Player checkPlayer = db.Players.FirstOrDefault(p => p.Email == Email);

            if(Password != ConPass)
            {
                ViewBag.Error = "Şifrələr uyğun deyil...";
                return View();
            }
            else if(checkPlayer != null)
            {
                ViewBag.Error = "Bu emaillə artiq qeydiyyatdan keçirilib...";
                return View();
            }else
            {
                db.Players.Add(new Player
                {
                    Name = Name,
                    Lastname = Lastname,
                    Email = Email,
                    Password = Crypto.HashPassword(Password)
                });
                db.SaveChanges();
                ViewBag.Success = "Qeydiyyat uğurla tamamlandi...";
                return View();
            }
        }

    }
}