using System;

namespace CFBPredictor.Models
{
    public class Game
    {
        private Team homeTeam;
        private Team awayTeam;
        private int homeScore;
        private int awayScore;

        /// <summary>
        /// Sets the home and away teams for the game
        /// </summary>
        /// <param name="home">the home team for the game</param>
        /// <param name="away">the away team for the game</param>
        public Game(Team home, Team away)
        {
            homeTeam = home;
            awayTeam = away;
        }

        /// <summary>
        /// Sets the home and away teams and their scores for the game
        /// </summary>
        /// <param name="home">the home team for the game</param>
        /// <param name="away">the away team for the game</param>
        /// <param name="homeScore">the home team's score for the game</param>
        /// <param name="awayScore">the away team's score for the game</param>
        public Game(Team home, Team away, double homeScore, double awayScore)
        {
            homeTeam = home;
            awayTeam = away;
            try
            {
                this.homeScore = Convert.ToInt32(Math.Round(homeScore));
                this.awayScore = Convert.ToInt32(Math.Round(awayScore));
            }

            catch { }
        }

        /// <summary>
        /// Gets the home team for the game
        /// </summary>
        /// <returns>the home team</returns>
        public Team GetHomeTeam()
        {
            return homeTeam;
        }

        /// <summary>
        /// Gets the away team for the game
        /// </summary>
        /// <returns>the away team</returns>
        public Team GetAwayTeam()
        {
            return awayTeam;
        }

        /// <summary>
        /// Gets the home team's score for the game
        /// </summary>
        /// <returns>the home team's score</returns>
        public int GetHomeScore()
        {
            return homeScore;
        }

        /// <summary>
        /// Gets the away team's score for the game
        /// </summary>
        /// <returns>the away team's score</returns>
        public int GetAwayScore()
        {
            return awayScore;
        }
    }
}
