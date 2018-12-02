using MiniFootball.Models;
using MiniFootball.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Helpers;

namespace MiniFootball.Controllers
{
    public class PlayerController : Controller
    {
        Mini_FootballEntities db = new Mini_FootballEntities();
        // GET: Player
        public ActionResult Index()
        {
            return View();
        }
        //Player edit get
        public ActionResult Edit()
        {
            Player player = Session["LoggedPlayer"] as Player;
            if (player == null)
            {
                return HttpNotFound();
            }
            return View(player);
        }
        //Post Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Name,Lastname,Email,Password")] Player player)
        {
            player.Password = Crypto.HashPassword(player.Password);
            if (ModelState.IsValid)
            {
                db.Entry(player).State = EntityState.Modified;
                Session["LoggedPlayer"] = player;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(player);
        }



        public ActionResult Offer()
        {
            if(Session["LoggedPlayer"] == null)
            {
                return HttpNotFound();
            }
            HomePlayerViewModel vm = new HomePlayerViewModel();

            Player curPlayer = Session["loggedPlayer"] as Player;
            var PlayerIsFree = db.Team_Player.Where(tp => tp.Player_id == curPlayer.id);

            if (PlayerIsFree != null)
            {
                int currentTeamId = db.Team_Player.Where(tp => tp.Player_id == curPlayer.id).Select(x => x.Team_id).FirstOrDefault();

                Player player;

                foreach (var item in db.Team_Player)
                {
                    if (item.Team_id == currentTeamId)
                    {
                        player = db.Players.FirstOrDefault(p => p.id == item.Player_id);

                        vm.PlayerList.Add(player);
                    }
                }

                vm.TeamList = db.Teams.ToList();
            }

            vm.Offer = db.Team_Player.FirstOrDefault(tp => tp.Player_id == curPlayer.id && tp.Submit == 0);

            return View(vm);
        }

        //Get Team Informations

        public ActionResult TeamInfo()
        {
            Player curPlayer = Session["LoggedPlayer"] as Player;
            int team_id = db.Team_Player.FirstOrDefault(tp => tp.Player_id == curPlayer.id).Team_id;
          

            PlayerTeaminfoViewModel vm = new PlayerTeaminfoViewModel();
            vm.Team = db.Teams.Find(team_id);

            foreach (var row in db.Team_Player)
            {
                if(row.Team_id == team_id && row.Submit == 1)
                {
                    vm.Playerlist.Add(db.Players.Find(row.Player_id));
                }
            }
           
            return View(vm);
        }
    }
}