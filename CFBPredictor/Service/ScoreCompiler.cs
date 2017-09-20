using CFBPredictor.Models;
using System;
using System.IO;

namespace CFBPredictor.Service
{ 
    public static class ScoreCompiler
    {
        public static NCAA ncaa = new NCAA();

        /// <summary>
        /// Reads scores from the file and creates a string array for each game
        /// </summary>
        /// <param name="year">year of scores to read</param>
        /// <param name="scoresPath">path of the files containing the scores</param>
        /// <returns>true if the scores are read successfully, false otherwise</returns>
        public static bool ReadScores(string year, string scoresPath)
        {
            string[] scoreline;
            string file;

            try
            {
                file = scoresPath + "collegefootballscores" + year + ".txt";
            }

            catch
            {
                return false;
            }

            try
            {
                StreamReader sr = new StreamReader(file);
                while (!sr.EndOfStream)
                {
                    string score = sr.ReadLine();
                    scoreline = score.Split('-');
                    CompileScores(scoreline);
                }

                sr.Close();
                return true;
            }


            catch (IOException)
            {
                return false;
            }
        }

        /// <summary>
        /// Takes the scoreline and adds the result to the proper teams' stats.
        /// Increases each team's wins or losses and points scored and points allowed
        /// </summary>
        /// <param name="scoreline">the score for one game</param>
        private static void CompileScores(string[] scoreline)
        {
            foreach (Team team in ncaa.GetFBS())
            {
                if (scoreline[0].Equals(team.GetTeamName()))
                {
                    team.IncreaseTotalPoints(Convert.ToInt32(scoreline[1]));
                    team.IncreaseTotalPointsAllowed(Convert.ToInt32(scoreline[3]));
                    if (Convert.ToInt32(scoreline[1]) > Convert.ToInt32(scoreline[3]))
                    {
                        team.IncreaseWins();
                    }
                    else
                    {
                        team.IncreaseLosses();
                    }

                    foreach (Team opponent in ncaa.GetFBS())
                    {
                        if (scoreline[2].Equals(opponent.GetTeamName()))
                        {
                            team.AddOpponent(opponent);
                        }
                    }

                    foreach (Team opponent in ncaa.GetFCS())
                    {
                        if (scoreline[2].Equals(opponent.GetTeamName()))
                        {
                            team.AddOpponent(opponent);
                        }
                    }
                }

                else if (scoreline[2].Equals(team.GetTeamName()))
                {
                    team.IncreaseTotalPoints(Convert.ToInt32(scoreline[3]));
                    team.IncreaseTotalPointsAllowed(Convert.ToInt32(scoreline[1]));
                    if (Convert.ToInt32(scoreline[3]) > Convert.ToInt32(scoreline[1]))
                    {
                        team.IncreaseWins();
                    }
                    else
                    {
                        team.IncreaseLosses();
                    }
                    foreach (Team opponent in ncaa.GetFBS())
                    {
                        if (scoreline[0].Equals(opponent.GetTeamName()))
                        {
                            team.AddOpponent(opponent);
                        }
                    }

                    foreach (Team opponent in ncaa.GetFCS())
                    {
                        if (scoreline[0].Equals(opponent.GetTeamName()))
                        {
                            team.AddOpponent(opponent);
                        }
                    }
                }
            }

            foreach (Team team in ncaa.GetFCS())
            {
                if (scoreline[0].Equals(team.GetTeamName()))
                {
                    team.IncreaseTotalPoints(Convert.ToInt32(scoreline[1]));
                    team.IncreaseTotalPointsAllowed(Convert.ToInt32(scoreline[3]));
                    if (Convert.ToInt32(scoreline[1]) > Convert.ToInt32(scoreline[3]))
                    {
                        team.IncreaseWins();
                    }
                    else
                    {
                        team.IncreaseLosses();
                    }
                    foreach (Team opponent in ncaa.GetFBS())
                    {
                        if (scoreline[2].Equals(opponent.GetTeamName()))
                        {
                            team.AddOpponent(opponent);
                        }
                    }

                    foreach (Team opponent in ncaa.GetFCS())
                    {
                        if (scoreline[2].Equals(opponent.GetTeamName()))
                        {
                            team.AddOpponent(opponent);
                        }
                    }
                }

                else if (scoreline[2].Equals(team.GetTeamName()))
                {
                    team.IncreaseTotalPoints(Convert.ToInt32(scoreline[3]));
                    team.IncreaseTotalPointsAllowed(Convert.ToInt32(scoreline[1]));
                    if (Convert.ToInt32(scoreline[3]) > Convert.ToInt32(scoreline[1]))
                    {
                        team.IncreaseWins();
                    }
                    else
                    {
                        team.IncreaseLosses();
                    }
                    foreach (Team opponent in ncaa.GetFBS())
                    {
                        if (scoreline[0].Equals(opponent.GetTeamName()))
                        {
                            team.AddOpponent(opponent);
                        }
                    }

                    foreach (Team opponent in ncaa.GetFCS())
                    {
                        if (scoreline[0].Equals(opponent.GetTeamName()))
                        {
                            team.AddOpponent(opponent);
                        }
                    }
                }
            }
        }
    }
}
