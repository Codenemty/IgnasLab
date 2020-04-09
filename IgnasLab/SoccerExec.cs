using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace IgnasLab
{
    public static class SoccerExec
    {
        const string playersPath = "./App_Data/players1.txt";
        const string teamsPath = "./App_Data/teams1.txt";
        /// <summary>
        /// Runs the tntire algorithm
        /// </summary>
        /// <param name="resultPanel">panel, which will be filled up with</param>
        /// <param name="desiredTeam">searched team's name</param>
        public static void Run(Panel resultPanel, string desiredTeam)
        {
            
            XList players = InOut.GetPlayers(playersPath);
            List<Team> teams = InOut.GetTeams(teamsPath);

            XList defenders = FilterPlayersByPosition(players, "Defender");
            XList midfields = FilterPlayersByPosition(players, "Midfield");
            XList attackers = FilterPlayersByPosition(players, "Attacker");

            defenders.Sort();
            midfields.Sort();
            attackers.Sort();

            Team bestTeam = FindBestTeam(teams);
            Team searchedTeam = FindTeam(teams, desiredTeam);

            XList bestTeamPlayers = FilterPlayersByTeam(players, bestTeam);
            XList searchedTeamPlayers = FilterPlayersByTeam(players, searchedTeam);

            bestTeamPlayers.Sort();
            searchedTeamPlayers.Sort();

            Table defenderTable = XListToTable(defenders);
            Table midfieldTable = XListToTable(midfields);
            Table attackerTable = XListToTable(attackers);
            Table bestTeamTable = XListToTable(bestTeamPlayers);
            Table searchedTeamTable = XListToTable(searchedTeamPlayers);

            InOut.RenderResults(resultPanel, defenderTable, midfieldTable, attackerTable, bestTeamTable, searchedTeamTable);
        }
        /// <summary>
        /// filters players by position
        /// </summary>
        /// <param name="list">all players</param>
        /// <param name="position">queried player position</param>
        /// <returns>filtered list</returns>
        public static XList FilterPlayersByPosition(XList list, string position)
        {
            if (position == "") return list;
            XList filtered = new XList();

            for (list.Begin(); list.Exist(); list.Next())
            {
                if (list.Get().Position.ToLower() == position.ToLower()) filtered.Add(list.Get());
            }
            return filtered;
        }
        /// <summary>
        /// filters players by team
        /// </summary>
        /// <param name="list">all players</param>
        /// <param name="team">queried team</param>
        /// <returns>filtered list</returns>
        public static XList FilterPlayersByTeam(XList list, Team team)
        {
            XList filtered = new XList();

            for (list.Begin(); list.Exist(); list.Next())
            {
                Player p = list.Get();
                if (p.Team.ToLower() == team.TeamName.ToLower()) filtered.Add(p);
            }
            return filtered;
        }
        /// <summary>
        /// Converts XList to Table
        /// </summary>
        /// <param name="list">list</param>
        /// <returns>list converted as table</returns>
        public static Table XListToTable(XList list) // Name surname team goals games
        {
            if (list == null) return null;
            Table table = new Table();

            TableHeaderRow header = new TableHeaderRow();
            header.Cells.Add(new TableHeaderCell() { Text = "Name" });
            header.Cells.Add(new TableHeaderCell() { Text = "Surname" });
            header.Cells.Add(new TableHeaderCell() { Text = "Team" });
            header.Cells.Add(new TableHeaderCell() { Text = "Goals" });
            header.Cells.Add(new TableHeaderCell() { Text = "Games" });

            table.Controls.Add(header);

            for (list.Begin(); list.Exist(); list.Next())
            {
                Player p = list.Get();
                TableRow row = new TableRow();

                row.Cells.Add(new TableCell() { Text = p.Name });
                row.Cells.Add(new TableCell() { Text = p.Surname });
                row.Cells.Add(new TableCell() { Text = p.Team });
                row.Cells.Add(new TableCell() { Text = p.GoalCount.ToString() });
                row.Cells.Add(new TableCell() { Text = p.GameCount.ToString() });

                table.Rows.Add(row);
            }
            return table;
        }
        /// <summary>
        /// Finds best team
        /// </summary>
        /// <param name="teams"> team list </param>
        /// <returns> best team </returns>
        public static Team FindBestTeam(List<Team> teams)
        {
            Team best = null;
            int bestPoints = 0;
            if (teams.Count == 0) return null;

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
        public static Team FindTeam(List<Team> teams, string teamName)
        {
            foreach (Team team in teams)
            {
                if (team.TeamName.ToLower() == teamName.ToLower()) return team;
            }
            return null;
        }



    }
}