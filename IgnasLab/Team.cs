using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IgnasLab
{
    public class Team : IComparable<Team>, IEquatable<Team>
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

        public int CompareTo(Team other) => (this.WonGameCount > other.WonGameCount) ? 1 : -1;

        public bool Equals(Team other) => (this.WonGameCount == other.WonGameCount && this.TeamName == other.TeamName);
    }
}