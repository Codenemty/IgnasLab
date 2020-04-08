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

        }
        internal int CompareTo(Player other)
        {
            if (other == null) return 1;
            if (GoalCount == other.GoalCount)
            {
                return GameCount.CompareTo(other.GameCount);
            }
            return -GoalCount.CompareTo(other.GoalCount);
        }
        public static bool operator >(Player a, Player b) => a.CompareTo(b) == 1;
        public static bool operator <(Player a, Player b) => a.CompareTo(b) == -1;
        public static bool operator >=(Player a, Player b) => a.CompareTo(b) >= 0;
        public static bool operator <=(Player a, Player b) => a.CompareTo(b) <= 0;
    }
}