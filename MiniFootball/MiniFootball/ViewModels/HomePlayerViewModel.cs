using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MiniFootball.Models;

namespace MiniFootball.ViewModels
{
    public class HomePlayerViewModel
    {
        public Team_Player Offer { get; set; }
        public List<Team_Player> teamPlayerList = new List<Team_Player>();
        public List<Player> PlayerList = new List<Player>();
        public List<Team> TeamList = new List<Team>();

    }
}