using CFBPredictor.Models;
using CFBpredictor.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace CFBPredictor.Service
{
    public static class ScoreAdder
    {
        private static string scoresPath = "C:/Users/Danny/GitHub/CollegeFootballRanker/CFBpredictor/scores/";

        /// <summary>
        /// Adds scores posted on the internet to a text file
        /// </summary>
        /// <param name="ncaa">list of all teams</param>
        /// <param name="week">week of scores to add</param>
        /// <returns>the message to display to the user based on the result</returns>
        public static string AddScores(NCAA ncaa, string week)
        {
            WebClient client = new WebClient();
            Team team1 = null, team2 = null;
            string ID, team1ID, team2ID, team1Score = null, team2Score = null;
            string fcsdstring = client.DownloadString("http://stats.washingtonpost.com/cfb/scoreboard.asp?conf=fcs%3A-2&week=" + week);
            string dstring = client.DownloadString("http://stats.washingtonpost.com/cfb/scoreboard.asp?conf=-1&week=" + week);
            string[] sep = new string[] { "team=" };
            string[] sep2 = new string[] { "10%" };
            string[] ScoreString, ScoreString1, ScoreString2;
            string[] firstFCSSep = new string[] { "GAMEZONELINKSTART" };
            string[] parsed = dstring.Split(sep, StringSplitOptions.None);
            string[] fcsGames = fcsdstring.Split(firstFCSSep, StringSplitOptions.None);
            string[,] fcsTeams = new string[200, 3];
            string[] teams;
            List<string> games = new List<string>();

            StreamReader sr = new StreamReader(scoresPath + "collegefootballscores2017.txt");
            using (sr)
            {
                while (!sr.EndOfStream)
                {
                    games.Add(sr.ReadLine());
                }
            }

            string ID1 = "", ID2 = "";

            for (int i = 1; i < parsed.Length; i++)
            {
                int p = parsed[i].IndexOf(@">");
                ID = parsed[i].Substring(0, p);
                string checkID = ID;
                checkID = checkID.Remove(checkID.Length - 1);
                ScoreString = parsed[i].Split(sep2, StringSplitOptions.None);
                string score;
                if (checkID != ID1 && checkID != ID2)
                {
                    try
                    {
                        score = ScoreString[ScoreString.Length - 1].Remove(0, 2);
                    }

                    catch
                    {
                        return "Week has not concluded";
                    }

                    if (Char.IsNumber(score[2]))
                    {
                        score = score.Remove(3, score.Length - 3);
                    }

                    else if (Char.IsNumber(score[1]))
                    {
                        score = score.Remove(2, score.Length - 2);
                    }

                    else
                    {
                        score = score.Remove(1, score.Length - 1);
                    }

                    int index = 0;
                    foreach (char c in ID)
                    {
                        if (!Char.IsNumber(c))
                        {
                            ID = ID.Remove(index);
                        }
                        index++;
                    }

                    if (team1 == null)
                    {
                        foreach (Team fbsteam in ncaa.GetFBS())
                        {
                            if (fbsteam.GetTeamID().Equals(ID))
                            {
                                ID1 = ID;
                                team1 = fbsteam;
                                team1Score = score;
                            }
                        }

                        if (team1 == null)
                        {
                            foreach (Team fcsteam in ncaa.GetFCS())
                            {
                                if (fcsteam.GetTeamID().Equals(ID))
                                {
                                    ID1 = ID;
                                    team1 = fcsteam;
                                    team1Score = score;
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
                                ID2 = ID;
                                team2 = team;
                                team2Score = score;
                                games.Add(team1.GetTeamName() + "-" + team1Score + "-" + team2.GetTeamName() + "-" + team2Score);
                                team1 = null;
                                team2 = null;
                            }
                        }
                    }
                }
            }

            //begin fcs code
            if (week != "18" && week != "19")
            {
                try
                {
                    int ind = 0;
                    string checkID1 = "", checkID2 = "";
                    for (int i = 0; i < fcsGames.Length; i++)
                    {

                        teams = fcsGames[i].Split(sep, StringSplitOptions.None);
                        int p = teams[teams.Length - 2].IndexOf(@">");
                        team1ID = teams[teams.Length - 2].Substring(0, p);
                        int index = 0;
                        foreach (char c in team1ID)
                        {
                            if (!Char.IsNumber(c))
                            {
                                team1ID = team1ID.Remove(index);
                            }
                            index++;
                        }
                        p = teams[teams.Length - 1].IndexOf(@">");
                        team2ID = teams[teams.Length - 1].Substring(0, p);
                        team2ID = team2ID.Remove(team2ID.Length - 1);
                        if (checkID1 != team1ID && checkID2 != team2ID)
                        {
                            if (checkID1 != team2ID && checkID2 != team1ID)
                            {
                                fcsTeams[ind, 0] = teams[0];
                                fcsTeams[ind, 1] = teams[teams.Length - 2];
                                fcsTeams[ind, 2] = teams[teams.Length - 1];
                                checkID1 = team1ID;
                                checkID2 = team2ID;
                                ind++;
                            }
                        }
                    }
                }

                catch
                {
                    return Messages.WEEK_NOT_CONCLUDED_MESSAGE;
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

                        ScoreString1 = fcsTeams[i, 1].Split(sep2, StringSplitOptions.None);
                        ScoreString2 = fcsTeams[i, 2].Split(sep2, StringSplitOptions.None);
                        string score1 = ScoreString1[ScoreString1.Length - 1].Remove(0, 2);
                        string score2 = ScoreString2[ScoreString2.Length - 1].Remove(0, 2);

                        if (Char.IsNumber(score1[2]))
                        {
                            score1 = score1.Remove(3, score1.Length - 3);
                        }

                        else if (Char.IsNumber(score1[1]))
                        {
                            score1 = score1.Remove(2, score1.Length - 2);
                        }

                        else
                        {
                            score1 = score1.Remove(1, score1.Length - 1);
                        }

                        if (Char.IsNumber(score2[2]))
                        {
                            score2 = score2.Remove(3, score2.Length - 3);
                        }

                        else if (Char.IsNumber(score2[1]))
                        {
                            score2 = score2.Remove(2, score2.Length - 2);
                        }

                        else
                        {
                            score2 = score2.Remove(1, score2.Length - 1);
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
                            games.Add(team1.GetTeamName() + "-" + score1 + "-" + team2.GetTeamName() + "-" + score2);
                            team1 = null;
                            team2 = null;
                        }

                        else if (team1 == null || team2 == null)
                        {
                            team1 = null;
                            team2 = null;
                        }
                    }
                }
            }
            //end fcs code

            //write to file here
            StreamWriter sw = new StreamWriter(scoresPath + "collegefootballscores2017.txt");
            using (sw)
            {
                foreach (string s in games)
                {
                    sw.WriteLine(s);
                }
            }

            return Messages.SCORES_ADDED_MESSAGE;
        }
    }
}
