using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CFBPredictor.Models;
using Microsoft.Office.Interop.Excel;
using System.IO;
using System.Net;

namespace CFBPredictor.Service
{
    /// <summary>
    /// Simulates a week of scheduled games, writes the results to an Excel file and 
    /// uploades that file to Google Drive. Also opens that file in Microsoft Excel
    /// </summary>
    public static class WeekSimulator
    {
        private static List<Game> Week = new List<Game>();
        private static string scoresPath = "C:/Users/Danny/GitHub/CollegeFootballRanker/CFBpredictor/scores/";
        private static string excelPath = "C:/Users/Danny/Documents/";

        /// <summary>
        /// Simulates the games scheduled for the week passed in
        /// </summary>
        /// <param name="ncaa">list of all teams</param>
        /// <param name="week">the week to simulate</param>
        /// <returns>a sucess message or the error message if there is one</returns>
        public static string SimWeek(NCAA ncaa, string week)
        {
            WebClient client = new WebClient();
            Team team1 = null, team2 = null;
            string ID, team1ID, team2ID;
            string fcsdstring = client.DownloadString("http://stats.washingtonpost.com/cfb/scoreboard.asp?conf=fcs%3A-2&week=" + week);
            string dstring = client.DownloadString("http://stats.washingtonpost.com/cfb/scoreboard.asp?conf=-1&week=" + week);
            string[] sep = new string[] { "team=" };
            string[] firstFCSSep = new string[] { "GAMEZONELINKSTART" };
            string[] parsed = dstring.Split(sep, StringSplitOptions.None);
            string[] fcsGames = fcsdstring.Split(firstFCSSep, StringSplitOptions.None);
            string[] teams = null;
            string[,] fcsTeams = new string[200, 3];

            for (int i = 1; i < parsed.Length; i++)
            {
                int p = parsed[i].IndexOf(@">");
                ID = parsed[i].Substring(0, p);
                int index = 0;
                foreach (char c in ID)
                {
                    if (!Char.IsNumber(c))
                    {
                        ID = ID.Remove(index);
                    }
                    index++;
                }

                if (i % 2 != 0)
                {
                    foreach (Team fbsteam in ncaa.GetFBS())
                    {
                        if (fbsteam.GetTeamID().Equals(ID))
                        {
                            team1 = fbsteam;
                        }
                    }

                    if (team1 == null)
                    {
                        foreach (Team fcsteam in ncaa.GetFCS())
                        {
                            if (fcsteam.GetTeamID().Equals(ID))
                            {
                                team1 = fcsteam;
                            }

                        }
                    }

                }

                else
                {
                    foreach (Team team in ncaa.GetFBS())
                    {
                        if (team.GetTeamID().Equals(ID))
                        {
                            team2 = team;
                            SimGame(team1, team2);
                            team1 = null;
                            team2 = null;
                        }
                    }
                }
            }

            //begin fcs
            try
            {
                for (int i = 0; i < fcsGames.Length; i++)
                {
                    teams = fcsGames[i].Split(sep, StringSplitOptions.None);
                    for (int j = 0; j < teams.Length; j++)
                    {
                        fcsTeams[i, j] = teams[j];
                    }
                }
            }

            catch
            {
                ClearWeek();
                return "Week has already finished or is in progress.";
            }

            for (int i = 0; i < fcsGames.Length; i++)
            {
                if (fcsTeams[i, 2] != null)
                {
                    int p = fcsTeams[i, 1].IndexOf(@">");
                    team1ID = fcsTeams[i, 1].Substring(0, p);
                    int index = 0;
                    foreach (char c in team1ID)
                    {
                        if (!Char.IsNumber(c))
                        {
                            team1ID = team1ID.Remove(index);
                        }
                        index++;
                    }
                    p = fcsTeams[i, 2].IndexOf(@">");
                    team2ID = fcsTeams[i, 2].Substring(0, p);
                    index = 0;
                    foreach (char c in team2ID)
                    {
                        if (!Char.IsNumber(c))
                        {
                            team2ID = team2ID.Remove(index);
                        }
                        index++;
                    }

                    foreach (Team fcsTeam in ncaa.GetFCS())
                    {
                        if (fcsTeam.GetTeamID().Equals(team1ID))
                        {
                            team1 = fcsTeam;
                        }

                        else if (fcsTeam.GetTeamID().Equals(team2ID))
                        {
                            team2 = fcsTeam;
                        }
                    }
                    if (team1 != null && team2 != null)
                    {
                        SimGame(team1, team2);
                        team1 = null;
                        team2 = null;
                    }
                }
            }
            //end fcs code
            return "Success";
        }

        /// <summary>
        /// Writes the simulated scores to a Microsoft Excel file
        /// </summary>
        /// <param name="ncaa">list of all teams</param>
        /// <param name="week">week that was simulated</param>
        public static void WriteSimmedScores(NCAA ncaa, int week)
        {
            string fileName = scoresPath + "SimmedWeek.txt";
            string xlfileName = excelPath + "SimmedWeek.xlsx";
            StringBuilder scores = new StringBuilder();
            Application xlApp = new Application();
            Workbook xlWorkBook = xlApp.Workbooks.Add();
            Worksheet xlWorkSheet = (Worksheet)xlWorkBook.Worksheets.get_Item(1);
            xlWorkSheet.Columns[1].ColumnWidth = 23;
            xlWorkSheet.Columns[2].ColumnWidth = 4;
            xlWorkSheet.Columns[3].ColumnWidth = 4;
            xlWorkSheet.Columns[4].ColumnWidth = 23;
            xlWorkSheet.Cells[1, 1] = "Week " + Convert.ToString(week);

            List<Team> FBSRankings = ncaa.GetFBS().OrderBy(o => o.rating).Reverse().ToList();
            List<Team> FCSRankings = ncaa.GetFCS().OrderBy(o => o.rating).Reverse().ToList();
            int k = 1;
            foreach (Team team in FBSRankings)
            {
                team.SetFBSRank(k);
                k++;
            }

            foreach (Team team in FCSRankings)
            {
                team.SetFBSRank(FBSRankings.Count + 2);
            }

            int i = 2;
            foreach (Game g in Week)
            {
                scores.Append(g.GetAwayTeam().GetTeamName() + ": " + g.GetAwayScore() + "   ");
                scores.AppendLine(g.GetHomeTeam().GetTeamName() + ": " + g.GetHomeScore());
                if (g.GetAwayTeam().GetFBSRank() <= 25)
                {
                    xlWorkSheet.Cells[i, 1] = "#" + g.GetAwayTeam().GetFBSRank() + " " + g.GetAwayTeam().GetTeamName();
                }
                else
                {
                    xlWorkSheet.Cells[i, 1] = g.GetAwayTeam().GetTeamName();
                }

                xlWorkSheet.Cells[i, 2] = g.GetAwayScore();
                xlWorkSheet.Cells[i, 3] = g.GetHomeScore();
                if (g.GetHomeTeam().GetFBSRank() <= 25)
                {
                    xlWorkSheet.Cells[i, 4] = "#" + g.GetHomeTeam().GetFBSRank() + " " + g.GetHomeTeam().GetTeamName();
                }
                else
                {
                    xlWorkSheet.Cells[i, 4] = g.GetHomeTeam().GetTeamName();
                }
                i++;
            }

            using (StreamWriter sw = new StreamWriter(fileName))
            {
                sw.Write(scores.ToString());
            }

            xlApp.DisplayAlerts = false;
            xlWorkBook.SaveAs("SimmedWeek.xlsx");
            xlWorkBook.Close();

            RankingExporter.UploadToDrive(xlfileName);

            System.Diagnostics.Process.Start(xlfileName);
            scores.Clear();
            Week.Clear();
            foreach (Team t in ncaa.GetFBS())
            {
                t.ClearProjectedPoints();
            }
        }

        /// <summary>
        /// Simulates an individual game
        /// </summary>
        /// <param name="home">the home team in the game</param>
        /// <param name="away">the away team in the game</param>
        public static void SimGame(Team home, Team away)
        {
            away.SetPoints(away.GetPoints() + (away.GetPPG() + home.GetDefensePPGvsOppAvg()));
            away.SetPoints(away.GetPoints() + home.GetDefensePPG() + away.GetPPGvsOppAvg());
            away.SetPoints(away.GetPoints() / 2);
            home.SetPoints(home.GetPoints() + (home.GetPPG() + away.GetDefensePPGvsOppAvg()));
            home.SetPoints(home.GetPoints() + (away.GetDefensePPG() + home.GetPPGvsOppAvg()));
            home.SetPoints(home.GetPoints() / 2);

            Game g = new Game(home, away, home.GetPoints(), away.GetPoints());
            Week.Add(g);
        }

        /// <summary>
        /// Clears all games from the week
        /// </summary>
        public static void ClearWeek()
        {
            Week.Clear();
        }
    }
}
