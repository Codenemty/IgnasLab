using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace IgnasLab
{
    public static class SoccerExec
    {
        /// <summary>
        /// Runs the tntire algorithm
        /// </summary>
        /// <param name="resultPanel">panel, which will be filled up with</param>
        /// <param name="desiredTeam">searched team's name</param>
        public static void Run(Panel resultPanel, string desiredTeam, string playersPath, string teamsPath, Stream playerStream = null, Stream teamStream = null)
        {
            
            XList<Player> players = InOut.GetPlayers(playersPath, playerStream);
            XList<Team> teams = InOut.GetTeams(teamsPath, teamStream);

            InOut.PrintFetchedData(resultPanel, players, teams);
            InOut.PrintFetchedDataToFile(players, teams);

            XList<Player> defenders = FilterPlayersByPosition(players, "Defender");
            XList<Player> midfields = FilterPlayersByPosition(players, "Midfield");
            XList<Player> attackers = FilterPlayersByPosition(players, "Attacker");

            defenders.Sort();
            midfields.Sort();
            attackers.Sort();

            Team bestTeam = FindBestTeam(teams);
            Team searchedTeam = FindTeam(teams, desiredTeam);

            XList<Player> bestTeamPlayers = FilterPlayersByTeam(players, bestTeam);
            XList<Player> searchedTeamPlayers = FilterPlayersByTeam(players, searchedTeam);

            bestTeamPlayers.Sort();
            searchedTeamPlayers.Sort();

            Table defenderTable = TeamToTable(defenders);
            Table midfieldTable = TeamToTable(midfields);
            Table attackerTable = TeamToTable(attackers);
            Table bestTeamTable = TeamToIndexTable(bestTeamPlayers, players);
            Table searchedTeamTable = TeamToTable(searchedTeamPlayers, true);

            InOut.RenderResults(resultPanel, defenderTable, midfieldTable, attackerTable, bestTeamTable, searchedTeamTable);
        }
        /// <summary>
        /// filters players by position
        /// </summary>
        /// <param name="list">all players</param>
        /// <param name="position">queried player position</param>
        /// <returns>filtered list</returns>
        public static XList<Player> FilterPlayersByPosition(XList<Player> list, string position)
        {
            if (position == "") return list;
            XList<Player> filtered = new XList<Player>();


            foreach (Player player in list)
            {
                if ( player.Position.ToLower() == position.ToLower() ) filtered.Add(player);
            }
            return filtered;
        }
        /// <summary>
        /// filters players by team
        /// </summary>
        /// <param name="list">all players</param>
        /// <param name="team">queried team</param>
        /// <returns>filtered list</returns>
        public static XList<Player> FilterPlayersByTeam(XList<Player> list, Team team)
        {
            if (team == null) return new XList<Player>();
            XList<Player> filtered = new XList<Player>();

            foreach (Player player in list)
            {
                if ( player.Team.ToLower() == team.TeamName.ToLower() ) filtered.Add(player);
            }
            return filtered;
        }
        /// <summary>
        /// Converts XList to Table
        /// </summary>
        /// <param name="list">list</param>
        /// <returns>list converted as table</returns>
        public static Table TeamToIndexTable(XList<Player> BestTeamList, XList<Player> fullList)
        {
            if (BestTeamList == null) return null;
            fullList.Sort();

            Table table = new Table();

            TableHeaderRow header = new TableHeaderRow();
            header.Cells.Add(new TableHeaderCell() { Text = "Name" });
            header.Cells.Add(new TableHeaderCell() { Text = "Surname" });
            header.Cells.Add(new TableHeaderCell() { Text = "Goals" });
            header.Cells.Add(new TableHeaderCell() { Text = "Index in list" });
            
            table.Controls.Add(header);

            foreach (Player player in BestTeamList)
            {
                TableRow row = new TableRow();

                row.Cells.Add(new TableCell() { Text = player.Name });
                row.Cells.Add(new TableCell() { Text = player.Surname });

                row.Cells.Add(new TableCell() { Text = player.GoalCount.ToString() });
                row.Cells.Add(new TableCell() { Text = fullList.IndexOf(player).ToString() });

                table.Rows.Add(row);
            }

            return table;
        }
        public static Table TeamToTable(XList<Player> list, bool OptionRemoveTeam = false) // Name surname team goals games
        {
            if (list == null) return null;
            Table table = new Table();

            TableHeaderRow header = new TableHeaderRow();
            header.Cells.Add(new TableHeaderCell() { Text = "Name" });
            header.Cells.Add(new TableHeaderCell() { Text = "Surname" });
            if (!OptionRemoveTeam)
            {
                header.Cells.Add(new TableHeaderCell() { Text = "Team" });
            }
            header.Cells.Add(new TableHeaderCell() { Text = "Goals" });
            header.Cells.Add(new TableHeaderCell() { Text = "Games" });

            table.Controls.Add(header);
            foreach (Player player in list)
            {
                TableRow row = new TableRow();

                row.Cells.Add(new TableCell() { Text = player.Name });
                row.Cells.Add(new TableCell() { Text = player.Surname });
                if (!OptionRemoveTeam)
                { 
                    row.Cells.Add(new TableCell() { Text = player.Team });
                }
                row.Cells.Add(new TableCell() { Text = player.GoalCount.ToString() });
                row.Cells.Add(new TableCell() { Text = player.GameCount.ToString() });

                table.Rows.Add(row);
            }
               
            
            return table;
        }
        /// <summary>
        /// Finds best team
        /// </summary>
        /// <param name="teams"> team list </param>
        /// <returns> best team </returns>
        public static Team FindBestTeam(XList<Team> teams)
        {
            Team best = null;
            int bestPoints = 0;
            if (teams.Count() == 0) return null;

            foreach (Team team in teams)
            {
                int teamPoints = team.DrawGameCount + team.WonGameCount * 3; //3 per win, 1 per loss
                if (teamPoints >= bestPoints)
                {
                    best = team;
                    bestPoints = teamPoints;
                }
            }
            return best;
        }
        /// <summary>
        /// Finds a team by string
        /// </summary>
        /// <param name="teams"> team list </param>
        /// <param name="teamName"> team lookup </param>
        /// <returns> searched team </returns>
        public static Team FindTeam(XList<Team> teams, string teamName)
        {
            foreach (Team team in teams)
            {
                if (team.TeamName.ToLower() == teamName.ToLower()) return team;
            }
            return null;
        }



    }
}