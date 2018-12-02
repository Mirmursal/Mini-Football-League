using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MiniFootball.Models;

namespace MiniFootball.Areas.Admin.Controllers
{
    [AuthorizationController]
    public class OffersController : Controller
    {
       
        Mini_FootballEntities db = new Mini_FootballEntities();
        // GET: Admin/Offers
        public ActionResult Index()
        {
            List<Team_Player> Offers = new List<Team_Player>();

            foreach (var item in db.Team_Player)
            {
                if(item.Submit == 0)
                {
                    Offers.Add(item);
                }
            }

            return View(Offers);
        }
    }
}