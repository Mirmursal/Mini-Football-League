using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MiniFootball.Models;

namespace MiniFootball.ViewModels
{
    public class HomeIndexViewModel
    {
        public List<Standing> Standings = new List<Standing>();
    }
    public class Standing
    {
        public int id { get; set; }
        public string TeamName { get; set; }
        public int Win { get; set; }
        public int Loss { get; set; }
        public int Draw { get; set; }
        public int Point { get; set; }
        public int GameCount { get; set; }

    }
}