using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MiniFootball.Filters;
using MiniFootball.Models;
using MiniFootball.ViewModels;
using System.Web.Helpers;

namespace MiniFootball.Controllers
{
    public class HomeController : Controller
    {
        Mini_FootballEntities db = new Mini_FootballEntities();
        public ActionResult Index()
        {
      
            HomeIndexViewModel vm = new HomeIndexViewModel();
            vm.Standings = Tool.StandingGen(db.Games.ToList() , db.Teams.ToList());
            return View(vm);
        }
        //Show old matches
        public ActionResult ShowOldMatches(int? id)
        {
    
            if(id == null)
            {
                return HttpNotFound();
            }

            Team curTeam = db.Teams.Find(id);

            if (curTeam == null)
            {
                return HttpNotFound();
            }

            List<Game> oldMatches = new List<Game>();

            foreach (var game in db.Games)
            {
                if((game.Home_team_id == id && game.AwayScore != null && game.HomeScore != null) || (game.Away_team_id == id && game.AwayScore != null && game.HomeScore != null))
                {
                    oldMatches.Add(game);
                }
            }

            return View(oldMatches);
        }

        //Show next matches
        public ActionResult ShowNextMatches(int? id)
        {

            if (id == null)
            {
                return HttpNotFound();
            }

            Team curTeam = db.Teams.Find(id);

            if (curTeam == null)
            {
                return HttpNotFound();
            }

            List<Game> newMatches = new List<Game>();

            foreach (var game in db.Games)
            {
                if ((game.Home_team_id == id && game.AwayScore == null && game.HomeScore == null) || (game.Away_team_id == id && game.AwayScore == null && game.HomeScore == null))
                {
                    newMatches.Add(game);
                }
            }

            return View(newMatches);
        }
    }
}