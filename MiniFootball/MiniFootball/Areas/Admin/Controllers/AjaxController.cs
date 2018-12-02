using MiniFootball.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MiniFootball.Areas.Admin.Controllers
{
    public class AjaxController : Controller
    {
        Mini_FootballEntities db = new Mini_FootballEntities();
        // GET: Admin/Ajax
        public ActionResult AddPlayer(int player_id, int team_id)
        {

            Player curPlayer = db.Players.Find(player_id);
            Team curTeam = db.Teams.Find(team_id);

            if (curPlayer == null || curTeam == null)
            {
                return HttpNotFound();
            }

            Team_Player newRow = new Team_Player();
            newRow.Player_id = player_id;
            newRow.Team_id = team_id;
            newRow.Submit = 1;
            db.Team_Player.Add(newRow);
            db.SaveChanges();

            List<Player> freePlayers = new List<Player>();

            foreach (var player in db.Players)
            {
                if (!db.Team_Player.Any(tp => tp.Player_id == player.id && tp.Submit == 1))
                {
                    freePlayers.Add(player);
                }
            }

            return Json(new
            {
                status = 200,
                error = "",
                data = freePlayers
            }, JsonRequestBehavior.AllowGet);
        }

        //Add game result
        public ActionResult AddResult(int game_id , int home_score , int away_score)
        {
           Game choosenGame = db.Games.FirstOrDefault(g => g.id == game_id);

            if (choosenGame == null)
            {
                return HttpNotFound();
            }

            if (home_score > away_score)
            {
                choosenGame.WinnerTeamId = choosenGame.Home_team_id;
            }
            else if (home_score < away_score)
            {
                choosenGame.WinnerTeamId = choosenGame.Away_team_id;
            }

            choosenGame.AwayScore = away_score;
            choosenGame.HomeScore = home_score;

            db.SaveChanges();

            return Json(new
            {
                status = 200,
                error = "",
                data = "Data was saved"
            }, JsonRequestBehavior.AllowGet);
        }

        //Accept Offers by offer id
        public ActionResult AcceptOffer(int offer_id)
        {
            Team_Player offer = db.Team_Player.Find(offer_id);

            if(offer != null)
            {
                offer.Submit = 1;
                db.SaveChanges();

                return Json(new
                {
                    status = 200,
                    error = "",
                    data = "Təklif qəbul olundu"
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    status = 505,
                    error = "",
                    data = "Təklif tapilmadi"
                }, JsonRequestBehavior.AllowGet);
            }
          
        }
        //Delete offer by offer_id
        public ActionResult DeleteOffer(int offer_id)
        {
            Team_Player offer = db.Team_Player.Find(offer_id);

            if (offer != null)
            {
                db.Team_Player.Remove(offer);
                db.SaveChanges();

                return Json(new
                {
                    status = 200,
                    error = "",
                    data = "Təklif silindi"
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    status = 505,
                    error = "",
                    data = "Təklif tapilmadi"
                }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}