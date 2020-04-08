using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IgnasLab
{
    public class Team
    {
        public string TeamName { get; set; }
        public int TotalGameCount { get; set; }
        public int WonGameCount { get; set; }
        public int DrawGameCount { get; set; }
        public Team() {}
        public Team(string teamName, int totalGames, int wonGames, int drawGames)
        {
            this.TeamName = teamName;
            this.TotalGameCount = totalGames;
            this.WonGameCount = wonGames;
            this.DrawGameCount = drawGames;
        }
    }
}