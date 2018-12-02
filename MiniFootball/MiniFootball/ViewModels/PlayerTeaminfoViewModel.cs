using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MiniFootball.Models;

namespace MiniFootball.ViewModels
{
    public class PlayerTeaminfoViewModel
    {
        public List<Player> Playerlist = new List<Player>();
        public Team Team { get; set; }
    }
}