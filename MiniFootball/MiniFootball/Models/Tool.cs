using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MiniFootball.ViewModels;
namespace MiniFootball.Models
{
    public static class Tool
    {
        public static List<Standing> StandingGen(List<Game> games, List<Team> teams)
        {
            List<Standing> standings = new List<Standing>();
            int wincount;
            int losecount;
            int drawcount;
    
            foreach (Team item in teams)
            {
                wincount = games.Where(g => g.WinnerTeamId == item.id).Count();
                losecount = games.Where(g => (g.Home_team_id == item.id||g.Away_team_id ==item.id)&& (g.WinnerTeamId!=item.id&&g.WinnerTeamId!=null)).Count();
                drawcount = games.Where(g => (g.Home_team_id == item.id || g.Away_team_id == item.id) && g.WinnerTeamId == null&&g.AwayScore!=null).Count();
                    
                Standing std = new Standing();
                std.id = item.id;
                std.TeamName = item.Name;
                std.Win = wincount;
                std.Loss = losecount;
                std.Draw = drawcount;
                std.Point = (wincount * 3) + drawcount;
                std.GameCount = losecount + drawcount + wincount;

                standings.Add(std);
            }

            return standings.OrderByDescending(C => C.Point).ToList();
        }
    }
}