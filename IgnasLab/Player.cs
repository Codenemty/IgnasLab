using System;

namespace IgnasLab
{
    public class Player
    {

        public string Team { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int BirthYear { get; set; }
        public float Height { get; set; }
        public char Position { get; set; } //F M D => Forward Midfield Defender
        public int GameCount { get; set; }
        public int GoalCount { get; set; }
        public Player()
        {
            Team = "black people";
        }
        internal int CompareTo(Player other)
        {
            throw new NotImplementedException();
        }
    }
}