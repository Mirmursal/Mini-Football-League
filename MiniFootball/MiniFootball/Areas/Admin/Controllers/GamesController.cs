using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MiniFootball.Models;

namespace MiniFootball.Areas.Admin.Controllers
{
    [AuthorizationController]
    public class GamesController : Controller
    {
        private Mini_FootballEntities db = new Mini_FootballEntities();

        // GET: Admin/Games
        public ActionResult Index()
        {
            return View(db.Games.ToList());
        }

        // GET: Admin/Games/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = db.Games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }

        // GET: Admin/Games/Create
        public ActionResult Create()
        {
            ViewBag.Home_team_id = new SelectList(db.Teams, "id", "Name");
            ViewBag.Away_team_id = new SelectList(db.Teams, "id", "Name");
            return View();
        }

        // POST: Admin/Games/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Home_team_id,Away_team_id,Time,HomeScore,AwayScore,WinnerTeamId")] Game game)
        {
            if(game.Home_team_id == game.Away_team_id)
            {
                ViewBag.Message = "Eyni komandalar bir-biri ilə oynaya bilməz";
                return RedirectToAction("Create");
            }

            if (ModelState.IsValid)
            {
                db.Games.Add(game);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Home_team_id = new SelectList(db.Teams, "id", "Name", game.Home_team_id);
            ViewBag.Away_team_id = new SelectList(db.Teams, "id", "Name", game.Away_team_id);
            return View(game);
        }

        // GET: Admin/Games/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = db.Games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            ViewBag.Home_team_id = new SelectList(db.Teams, "id", "Name", game.Home_team_id);
            ViewBag.Away_team_id = new SelectList(db.Teams, "id", "Name", game.Away_team_id);
            return View(game);
        }

        // POST: Admin/Games/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Home_team_id,Away_team_id,Time,HomeScore,AwayScore,WinnerTeamId")] Game game)
        {
            if (ModelState.IsValid)
            {
                db.Entry(game).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Home_team_id = new SelectList(db.Teams, "id", "Name", game.Home_team_id);
            ViewBag.Away_team_id = new SelectList(db.Teams, "id", "Name", game.Away_team_id);
            return View(game);
        }

        // GET: Admin/Games/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = db.Games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }

        //Add game result 
        public ActionResult AddResult()
        {
            List<Game> freeGames = new List<Game>();
            foreach (var games in db.Games)
            {
                if(games.HomeScore == null && games.AwayScore == null && games.WinnerTeamId == null)
                {
                  
                    freeGames.Add(games);
                }
            }

            return View(freeGames);
        }

        // POST: Admin/Games/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Game game = db.Games.Find(id);
            db.Games.Remove(game);
            db.SaveChanges();
            return RedirectToAction("Index");
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
