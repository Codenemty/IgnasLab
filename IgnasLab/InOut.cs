using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IgnasLab
{
    public static class InOut
    {
        /// <summary>
        /// Gets players from file
        /// </summary>
        /// <param name="path"> relative file location</param>
        /// <returns>players</returns>
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
        /// <summary>
        /// Gets teams from file
        /// </summary>
        /// <param name="path">relative file location</param>
        /// <returns>teams</returns>
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
        /// <summary>
        /// Puts all the results on the screen
        /// </summary>
        /// <param name="panel">Panel which will be filled with tables</param>
        /// <param name="defTable"> table of defenders </param>
        /// <param name="midTable">table of midfields</param>
        /// <param name="atkTable">table of attackers</param>
        /// <param name="bestTeamTable">table of the best team</param>
        /// <param name="searchedTeamTable"> table of the searched team</param>
        public static void RenderResults(Panel panel, Table defTable, Table midTable, Table atkTable, Table bestTeamTable, Table searchedTeamTable)
        {
            defTable.Caption = "Best defenders";
            midTable.Caption = "Best midfields";
            atkTable.Caption = "Best attackers";
            bestTeamTable.Caption = "Best Team's players";
            searchedTeamTable.Caption = "Searched team's players";


            panel.Controls.Add(defTable.Rows.Count > 1 ? defTable : ((Control)new Label() { Text = "No defenders found." }));
            panel.Controls.Add(midTable.Rows.Count > 1 ? midTable : ((Control)new Label() { Text = "No midfields found." }));
            panel.Controls.Add(atkTable.Rows.Count > 1 ? atkTable : ((Control)new Label() { Text = "No attackers found." }));
            panel.Controls.Add(bestTeamTable.Rows.Count > 1 ? bestTeamTable : ((Control)new Label() { Text = "There is no best team." }));
            panel.Controls.Add(searchedTeamTable.Rows.Count > 1 ? searchedTeamTable : ((Control)new Label() { Text = "No such team found." }));
        }

    }
}