using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MiniFootball.Areas.Admin.Controllers
{
    [AuthorizationController]
    public class HomeController : Controller
    {
        // GET: Admin/Home
        public ActionResult Index()
        {
            if (Session["AdminLogged"] != null)
            {
                return View();
            }
            else
            {
               return RedirectToAction("Index", "Login");
            }

            
        }
    }
}