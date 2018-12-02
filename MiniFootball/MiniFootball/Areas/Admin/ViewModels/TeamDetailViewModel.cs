using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MiniFootball.Models;

namespace MiniFootball.Areas.Admin.ViewModels
{
    public class TeamDetailViewModel
    {
        public Team Team { get; set; }
        public List<Player> PlayerList = new List<Player>();

    }
}