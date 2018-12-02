using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MiniFootball.Models;
using MiniFootball.Areas.Admin.Controllers;
using MiniFootball.Areas.Admin.ViewModels;

namespace MiniFootball.Areas.Admin.Controllers
{
    [AuthorizationController]
    public class TeamsController : Controller
    {
        private Mini_FootballEntities db = new Mini_FootballEntities();

        // GET: Admin/Teams
        public ActionResult Index()
        {
            return View(db.Teams.ToList());
        }

        // GET: Admin/Teams/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Team team = db.Teams.Find(id);
            if (team == null)
            {
                return HttpNotFound();
            }

            TeamDetailViewModel vm = new TeamDetailViewModel();
            vm.Team = team;

            foreach (var player in db.Players)
            {
                foreach (var teamplayer in db.Team_Player)
                {
                    if(player.id == teamplayer.Player_id && teamplayer.Team_id == team.id && teamplayer.Submit == 1)
                    {
                        vm.PlayerList.Add(player);
                    }
                }
            }

            return View(vm);
        }

        // GET: Admin/Teams/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Teams/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Name,City,Logo")] Team team)
        {
            if (ModelState.IsValid)
            {
                db.Teams.Add(team);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(team);
        }

        // GET: Admin/Teams/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Team team = db.Teams.Find(id);
            if (team == null)
            {
                return HttpNotFound();
            }
            return View(team);
        }

        // POST: Admin/Teams/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Name,City,Logo")] Team team)
        {
            if (ModelState.IsValid)
            {
                db.Entry(team).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(team);
        }

        // GET: Admin/Teams/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Team team = db.Teams.Find(id);
            if (team == null)
            {
                return HttpNotFound();
            }
            return View(team);
        }

        // POST: Admin/Teams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            foreach (var row in db.Team_Player)
            {
                if(row.Team_id == id)
                {
                    db.Team_Player.Remove(row);
                }
            }

            foreach (var game in db.Games)
            {
                if(game.Team.id == id || game.Team1.id == id)
                {
                    db.Games.Remove(game);
                }
            }

            Team team = db.Teams.Find(id);
            db.Teams.Remove(team);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        //Remove player from team
        // GET: Admin/Teams/Delete/5
        public ActionResult RemovePlayer(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Player player = db.Players.Find(id);
            if (player == null)
            {
                return HttpNotFound();
            }
            Team_Player row = db.Team_Player.FirstOrDefault(tp => tp.Player_id == id);
            if(row == null)
            {
                return HttpNotFound();
            }
            else
            {
                db.Team_Player.Remove(row);
                db.SaveChanges();
            }
            return RedirectToAction("Details", "Teams", new { id = row.Team_id });
        }

        public ActionResult AddPlayer(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Team team = db.Teams.Find(id);
            if (team == null)
            {
                return HttpNotFound();
            }

             List<Player> freePlayers = new List<Player>();

            foreach (var player in db.Players)
            {
                if (!db.Team_Player.Any(tp=> tp.Player_id == player.id && tp.Submit==1))
                {
                    freePlayers.Add(player);
                }
            }
            ViewBag.CurTeam = team.Name;
            ViewBag.TeamId = team.id;
            return View(freePlayers);
           
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
