using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace IgnasLab
{
    public static class InOut
    {

        public static XList GetPlayers(string path)
        {
            XList players = new XList();

            using(StreamReader sr = new StreamReader(HttpContext.Current.Server.MapPath(path)))
            {
                while (!sr.EndOfStream)
                {
                    string[] vars = sr.ReadLine().Split(' ');//Team Name Surname BYear Height Position GamesCount GoalsCount
                    string team = vars[0];
                    string name = vars[1];
                    string surname = vars[2];
                    int bYear = int.Parse(vars[3]);
                    float height = float.Parse(vars[4]);
                    string position = vars[5];
                    int gamesCount = int.Parse(vars[6]);
                    int goalsCount = int.Parse(vars[7]);
                    Player p = new Player(team, name, surname, bYear, height, position, gamesCount, goalsCount);
                    players.Add(p);
                }
                return players;
            }
        }

        public static List<Team> GetTeams(string path)
        {
            List<Team> teams = new List<Team>();

            using (StreamReader sr = new StreamReader(HttpContext.Current.Server.MapPath(path)))// TeamName TotalGames WonGames DrawGames
            {
                while (!sr.EndOfStream)
                {
                    string[] vars = sr.ReadLine().Split(' ');
                    string teamName = vars[0];
                    int totalGames = int.Parse(vars[1]);
                    int wonGames = int.Parse(vars[2]);
                    int drawGames = int.Parse(vars[3]);
                    Team t = new Team(teamName, totalGames, wonGames, drawGames);
                    teams.Add(t);
                }
            }
                return teams;
        }

        public static void RenderResults(Panel panel, Table defTable, Table midTable, Table atkTable, Table bestTeamTable)
        {
            panel.Controls.Add(defTable);
            panel.Controls.Add(midTable);
            panel.Controls.Add(atkTable);
            panel.Controls.Add(bestTeamTable);
        }






    }
}