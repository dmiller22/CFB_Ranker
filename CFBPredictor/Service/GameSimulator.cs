using CFBPredictor.Models;

namespace CFBPredictor.Service
{
    public class GameSimulator
    {
        private NCAA ncaa;
        Team homeTeam, awayTeam;
        private string homeTeamName, awayTeamName;

        /// <summary>
        /// Constructor which sets the NCAA object as well as the names of both teams
        /// that played in the game
        /// </summary>
        /// <param name="ncaa">list of all teams in division 1</param>
        /// <param name="homeTeamName">name of the home team in the game</param>
        /// <param name="awayTeamName">name of the away team in the game</param>
        public GameSimulator(NCAA ncaa, string homeTeamName, string awayTeamName)
        {
            this.ncaa = ncaa;
            this.homeTeamName = homeTeamName;
            this.awayTeamName = awayTeamName;
        }

        /// <summary>
        /// Predicts the game result between the two teams
        /// </summary>
        /// <returns>the Game object if the team names are valid, null otherwise</returns>
        public Game SimulateGame()
        {
            if (VerifyTeams())
            {
                awayTeam.SetPoints(awayTeam.GetPoints() + (awayTeam.GetPPG() + homeTeam.GetDefensePPGvsOppAvg()));
                awayTeam.SetPoints(awayTeam.GetPoints() + homeTeam.GetDefensePPG() + awayTeam.GetPPGvsOppAvg());
                awayTeam.SetPoints(awayTeam.GetPoints() / 2);
                homeTeam.SetPoints(homeTeam.GetPoints() + (homeTeam.GetPPG() + awayTeam.GetDefensePPGvsOppAvg()));
                homeTeam.SetPoints(homeTeam.GetPoints() + (awayTeam.GetDefensePPG() + homeTeam.GetPPGvsOppAvg()));
                homeTeam.SetPoints(homeTeam.GetPoints() / 2);
                Game game = new Game(homeTeam, awayTeam, homeTeam.GetPoints(), awayTeam.GetPoints());
                return game;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Verifies that both team names passed in are valid team names
        /// </summary>
        /// <returns>true if both teams are valid, false otherwise</returns>
        private bool VerifyTeams()
        {
            bool home = false;
            bool away = false;
            foreach (Team team in ncaa.GetFBS())
            {
                if (team.GetTeamName().Equals(homeTeamName))
                {
                    homeTeam = team;
                    home = true;
                }
                else if (team.GetTeamName().Equals(awayTeamName))
                {
                    awayTeam = team;
                    away = true;
                }
            }
            return home && away;
        }
    }
}
