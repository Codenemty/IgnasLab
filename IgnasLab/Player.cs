using System;

namespace IgnasLab
{
    public class Player : IComparable<Player>, IEquatable<Player>
    {

        public string Team { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int BirthYear { get; set; }
        public float Height { get; set; }
        public string Position { get; set; } //F M A => Forward Midfield Attacker
        public int GameCount { get; set; }
        public int GoalCount { get; set; }
        
        public Player(string team, string name, string surname, int bYear,float height,string position, int gamesCount,int goalsCount)//Team Name Surname BYear Height Position GamesCount GoalsCount
        {
            this.Team = team;
            this.Name = name;
            this.Surname = surname;
            this.BirthYear = bYear;
            this.Height = height;
            this.Position = position;
            this.GameCount = gamesCount;
            this.GoalCount = goalsCount;
        }

        public int CompareTo(Player other)
        {
            if (other == null) return 1;
            if (GoalCount == other.GoalCount)
            {
                return -GameCount.CompareTo(other.GameCount);
            }
            return GoalCount.CompareTo(other.GoalCount);
        }

        public bool Equals(Player other)
        {
            return this.Team == other.Team &&
                   this.Name == other.Name &&
                   this.Surname == other.Surname;
        }
    }
}