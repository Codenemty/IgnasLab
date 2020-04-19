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
        public static XList<Player> GetPlayers(string path, Stream stream)
        {
            XList<Player> players = new XList<Player>();
            StreamReader sr;
            if (stream != null)
            {
                sr = new StreamReader(stream);
            }
            else
            {
                sr = new StreamReader(HttpContext.Current.Server.MapPath(path));
            }
            while (!sr.EndOfStream)
            {
                string[] vars = sr.ReadLine().Split(';');//Team Name Surname BYear Height Position GamesCount GoalsCount
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
            sr.Dispose();
            return players;
        }
        /// <summary>
        /// Gets teams from file
        /// </summary>
        /// <param name="path">relative file location</param>
        /// <returns>teams</returns>
        public static XList<Team> GetTeams(string path, Stream stream)
        {
            XList<Team> teams = new XList<Team>();
            StreamReader sr;
            if (stream != null)
            {
                sr = new StreamReader(stream);
            }
            else
            {
                sr = new StreamReader(HttpContext.Current.Server.MapPath(path));
            }

            while (!sr.EndOfStream)
            {
                string[] vars = sr.ReadLine().Split(';');
                string teamName = vars[0];
                int totalGames = int.Parse(vars[1]);
                int wonGames = int.Parse(vars[2]);
                int drawGames = int.Parse(vars[3]);
                Team t = new Team(teamName, totalGames, wonGames, drawGames);
                teams.Add(t);
            }
            sr.Dispose();
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
        /// <summary>
        /// Prints fetched data
        /// </summary>
        /// <param name="panel">Panel which will be filled with tables</param>
        /// <param name="players">fetched player info</param>
        /// <param name="teams">fetched team info</param>
        public static void PrintFetchedData(Panel panel, XList<Player> players, XList<Team> teams)
        {
            //Players
            Table playerTable = new Table();
            playerTable.Caption = "Fetched player data";

            TableHeaderRow th = new TableHeaderRow();
            th.Controls.Add(new TableHeaderCell() { Text = "Team" });
            th.Controls.Add(new TableHeaderCell() { Text = "Name" });
            th.Controls.Add(new TableHeaderCell() { Text = "Surname" });
            th.Controls.Add(new TableHeaderCell() { Text = "BirthYear" });
            th.Controls.Add(new TableHeaderCell() { Text = "Height" });
            th.Controls.Add(new TableHeaderCell() { Text = "Position" });
            th.Controls.Add(new TableHeaderCell() { Text = "GameCount" });
            th.Controls.Add(new TableHeaderCell() { Text = "GoalCount" });
            playerTable.Rows.Add(th);

            foreach (Player player in players)
            {
                TableRow tr = new TableRow();
                tr.Cells.Add(new TableCell() { Text = player.Team });
                tr.Cells.Add(new TableCell() { Text = player.Name });
                tr.Cells.Add(new TableCell() { Text = player.Surname });
                tr.Cells.Add(new TableCell() { Text = player.BirthYear.ToString() });
                tr.Cells.Add(new TableCell() { Text = player.Height.ToString() });
                tr.Cells.Add(new TableCell() { Text = player.Position });
                tr.Cells.Add(new TableCell() { Text = player.GameCount.ToString() });
                tr.Cells.Add(new TableCell() { Text = player.GoalCount.ToString() });
                playerTable.Rows.Add(tr);
            }
            panel.Controls.Add(playerTable);

            //Teams
            Table teamTable = new Table();
            teamTable.Caption = "Fetched team data";

            TableHeaderRow h = new TableHeaderRow();
            h.Controls.Add(new TableHeaderCell() { Text = "Name" });
            h.Controls.Add(new TableHeaderCell() { Text = "Total Played" });
            h.Controls.Add(new TableHeaderCell() { Text = "Wins" });
            h.Controls.Add(new TableHeaderCell() { Text = "Draws" });
            teamTable.Rows.Add(h);

            foreach (Team team in teams)
            {
                TableRow tr = new TableRow();
                tr.Cells.Add(new TableCell() { Text = team.TeamName });
                tr.Cells.Add(new TableCell() { Text = team.TotalGameCount.ToString() });
                tr.Cells.Add(new TableCell() { Text = team.WonGameCount.ToString() });
                tr.Cells.Add(new TableCell() { Text = team.DrawGameCount.ToString() });
                teamTable.Rows.Add(tr);
            }

            panel.Controls.Add(teamTable);
        }
        public static void PrintFetchedDataToFile(XList<Player> players, XList<Team> teams)
        {
            string playerFile = "./App_Data/PLAYER_DATA.txt";
            string teamFile = "./App_Data/TEAM_DATA.txt";
            using(StreamWriter sw = File.CreateText(HttpContext.Current.Server.MapPath(playerFile)))
            {
                sw.WriteLine("Player data");
                sw.WriteLine();
                sw.WriteLine("| {0,20} | {1,15} | {2,15} | {3,5} | {4,8} | {5,8} | {6,5} | {7,5} |", "Team", "Name", "Surname", "Birth", "Height", "Position", "Games", "Goals");
                sw.WriteLine(new string('-', 69));
                foreach (Player player in players)
                {
                    sw.WriteLine("| {0,20} | {1,15} | {2,15} | {3,5} | {4,8} | {5,8} | {6,5} | {7,5} |", player.Team, player.Name, player.Surname, player.BirthYear, player.Height, player.Position, player.GameCount, player.GoalCount);
                }

            }

            using (StreamWriter sw = File.CreateText(HttpContext.Current.Server.MapPath(teamFile)))
            {
                sw.WriteLine("Team data");
                sw.WriteLine();
                sw.WriteLine("| {0,15} | {1,5} | {2,5} | {3,5} |", "Name", "Total", "Wins", "Draws");
                sw.WriteLine(new string('-', 43));
                foreach (Team team in teams)
                {
                    sw.WriteLine("| {0,15} | {1,5} | {2,5} | {3,5} |", team.TeamName, team.TotalGameCount, team.WonGameCount, team.DrawGameCount);
                }
            }
        }
    }
}