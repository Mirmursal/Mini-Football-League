using MiniFootball.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MiniFootball.Controllers
{
    public class AjaxController : Controller
    {
        Mini_FootballEntities db = new Mini_FootballEntities();
        // GET: Ajax
        public ActionResult GetOffer(int team_id)
        {





            Team choosenTeam = db.Teams.Find(team_id);
            Player player = Session["LoggedPlayer"] as Player;



            if (choosenTeam == null)
            {
                return HttpNotFound();
            }

            Team_Player check = db.Team_Player.FirstOrDefault(tp=>tp.Player_id == player.id);

            if (check == null)
            {
                db.Team_Player.Add(new Team_Player
                {
                    Team_id = choosenTeam.id,
                    Player_id = player.id,
                    Submit = 0
                });

                db.SaveChanges();

                return Json(new
                {
                    status = 200,
                    error = "",
                    data = choosenTeam.Name + " Komandasina istək göndərdiniz..."
                }, JsonRequestBehavior.AllowGet);
            }else{
                return Json(new
                {
                    status = 505,
                    error = "",
                    data = check.Team.Name + " Komandasina istək göndərilib artiq..."
                }, JsonRequestBehavior.AllowGet);
            }

            
        }
    }
}