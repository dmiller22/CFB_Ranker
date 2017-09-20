using System;
using System.Collections.Generic;
using System.Linq;
using CFBPredictor.Models;

namespace CFBPredictor.Service
{
    public class RankingGenerator
    {
        private NCAA ncaa;
        private List<Team> FinalRankings;
        private List<Team> DivisionI = new List<Team>();
        private List<Team> SOS = new List<Team>();

        private const int MAX_RERANKS = 100; //Number of times it iterates through with different SOS.

        private int reRanks = 0;

        /// <summary>
        /// Constructor that takes in a list of teams and sets that to the
        /// NCAA list of teams
        /// </summary>
        /// <param name="ncaa">list of all division 1 teams</param>
        public RankingGenerator(NCAA ncaa)
        {
            this.ncaa = ncaa;
        }

        /// <summary>
        /// Generates the rankings for each teams and returns a list of teams containing
        /// each team's final rating and ranking
        /// </summary>
        /// <returns>list of teams with their final rating and ranking</returns>
        public List<Team> GenerateRankings()
        {
            CalculateRankings();
            AddToConference();
            ConferenceRankings();
            StrengthOfSchedule();
            foreach (Team team in FinalRankings)
            {
                //team.rating *= 100;
                team.rating *= 0.1;
            }
            return FinalRankings;
        }

        /// <summary>
        /// Calculates each team's opponent records and total points scored/allowed and each team's
        /// initial rating
        /// </summary>
        private void CalculateRankings()
        {
            double low = int.MaxValue;
            foreach (Team team in ncaa.GetFBS())
            {
                foreach (Team opponent in team.GetSchedule())
                {
                    team.IncreaseOpponentWins(opponent.GetWins());
                    team.IncreaseOpponentLosses(opponent.GetLosses());
                    team.IncreaseOpponentTotalPoints(Convert.ToInt32(opponent.GetTotalPoints()));
                    team.IncreaseOpponentTotalPointsAllowed(Convert.ToInt32(opponent.GetTotalPointsAllowed()));
                }
                team.rating += (team.GetPPGvsOppAvg() - team.GetDefensePPGvsOppAvg()) * 5;
                if (team.rating < low)
                {
                    low = team.rating;
                }
            }
            foreach (Team team in ncaa.GetFBS())
            { 
                team.rating += (0 - (low - 1));
                if (team.GetWinPercentage() > 0)
                {
                    team.rating *= Math.Pow(team.GetWinPercentage(), 2);
                }
                else
                {
                    team.rating *= Math.Pow(.001, 2);
                }
                team.rating += 5;

                if (team.GetOpponentWinPercentage() > 0)
                {
                    team.rating *= team.GetOpponentWinPercentage();
                }
                else
                {
                    team.rating *= .001;
                }
                DivisionI.Add(team);
            }

            foreach (Team team in ncaa.GetFCS())
            {
                foreach (Team opponent in team.GetSchedule())
                {
                    team.IncreaseOpponentWins(opponent.GetWins());
                    team.IncreaseOpponentLosses(opponent.GetLosses());
                    team.IncreaseOpponentTotalPoints(Convert.ToInt32(opponent.GetTotalPoints()));
                    team.IncreaseOpponentTotalPointsAllowed(Convert.ToInt32(opponent.GetTotalPointsAllowed()));
                }
                team.rating += (team.GetPPGvsOppAvg() - team.GetDefensePPGvsOppAvg()) * (5 / 2);
                if (team.rating < low)
                {
                    low = team.rating;
                }
            }
            foreach (Team team in ncaa.GetFCS())
            { 
                team.rating += (0 - (low - 1));
                if (team.GetWinPercentage() > 0)
                {
                    team.rating *= Math.Pow(team.GetWinPercentage(), 2);
                }
                else
                {
                    team.rating *= Math.Pow(.001, 2);
                }
                team.rating += 5;

                if (team.GetOpponentWinPercentage() > 0)
                {
                    team.rating *= team.GetOpponentWinPercentage();
                }
                else
                {
                    team.rating *= .001;
                }
                DivisionI.Add(team);
            }
            SortRankings(DivisionI); 
        }  

        /// <summary>
        /// Sorts teams in order based on their rating
        /// </summary>
        /// <param name="Division1">list of teams</param>
        private void SortRankings(List<Team> Division1)
        {
            FinalRankings = Division1.OrderBy(o => o.rating).Reverse().ToList();
            int i = 1;
            foreach (Team team in FinalRankings)
            {
                team.SetRank(i);
                i++;
            }

            if (reRanks < MAX_RERANKS)
            {
                ReRank(FinalRankings);
                return;
            }

            else
            {
                foreach (Team team in Division1)
                {
                    if (team.GetTeamConference() != "FCS")
                    {
                        team.rating *= 100;
                    }
                }
                List<Team> FBSRankings = ncaa.GetFBS().OrderBy(o => o.rating).Reverse().ToList();
                int k = 1;
                foreach (Team team in FBSRankings)
                {
                    team.SetFBSRank(k);
                    k++;
                }
                foreach (Team team in ncaa.GetFCS())
                {
                    team.ClearFBSRank();
                }
            }
        }

        /// <summary>
        /// Each team gets re-ranked using the most recent rankings as the basis for schedule
        /// strength
        /// </summary>
        /// <param name="Rankings">the list of teams with their ranking</param>
        private void ReRank(List<Team> Rankings)
        {
            double low = int.MaxValue;
            foreach (Team team in ncaa.GetFBS())
            {
                team.rating += (team.GetPPGvsOppAvg() - team.GetDefensePPGvsOppAvg()) * 5;
                if (team.rating < low)
                {
                    low = team.rating;
                }

            }

            foreach (Team team in ncaa.GetFCS())
            {
                team.rating += (team.GetPPGvsOppAvg() - team.GetDefensePPGvsOppAvg()) * (5 / 2);
                if (team.rating < low)
                {
                    low = team.rating;
                }
            }

            foreach (Team team in ncaa.GetFBS())
            {
                team.rating += (0 - (low - 1));
                if (team.GetWinPercentage() > 0)
                {
                    team.rating *= Math.Pow(team.GetWinPercentage(), 2);
                }
                else
                {
                    team.rating *= Math.Pow(.0001, 2);
                }
            }

            foreach (Team team in ncaa.GetFCS())
            {
                team.rating += (0 - (low - 1));
                if (team.GetWinPercentage() > 0)
                {
                    team.rating *= Math.Pow(team.GetWinPercentage(), 2);
                }
                else
                {
                    team.rating *= Math.Pow(.0001, 2);
                }
            }

            ReEvaluateSchedule(Rankings);
            reRanks++;
            SortRankings(Rankings);
        }

        /// <summary>
        /// Adjusts each team's rating based on their strength of schedule
        /// </summary>
        /// <param name="Rankings">list of teams being ranked</param>
        private void ReEvaluateSchedule(List<Team> Rankings)
        {
            //Reevaluates SOS based on previous rankings generated
            foreach (Team team in Rankings)
            {
                team.SetStrength(1);
                foreach (Team name in team.GetSchedule())
                {
                    team.SetStrength(team.GetStrength() + name.GetRank());
                }
                team.rating += 5;
                team.rating /= (team.GetStrength() / (team.GetWins() + team.GetLosses()));
            }
        }

        /// <summary>
        /// Adds each team to their corresponding conference
        /// </summary>
        private void AddToConference()
        {
            foreach (Team team in ncaa.GetFBS())
            {
                foreach (Conference conference in ncaa.GetConferences())
                {
                    if (team.GetTeamConference().Equals(conference.GetConferenceName()))
                    {
                        conference.AddMember(team);
                    }

                    else if (conference.GetConferenceName() == "FCS")
                    {
                        foreach (Team fcsTeam in ncaa.GetFCS())
                        {
                            conference.AddMember(fcsTeam);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Calculates the rating for each conference
        /// </summary>
        private void ConferenceRankings()
        {
            foreach (Conference conference in ncaa.GetConferences())
            {
                if (conference.GetConferenceName() == "FCS")
                {
                    foreach (Team team in conference.GetMembers())
                    {
                        conference.rating += 129;
                    }
                }
                else
                {
                    foreach (Team team in conference.GetMembers())
                    {
                        conference.rating += team.GetFBSRank();
                    }
                }
                conference.rating /= conference.GetMembers().Count;
            }
        }

        /// <summary>
        /// Calculates each team's strength of schedule
        /// </summary>
        private void StrengthOfSchedule()
        {
            foreach (Team t in ncaa.GetFBS())
            {
                if ((t.GetWins() + t.GetLosses()) != 0)
                {
                    t.SetStrength(t.GetStrength() / (Convert.ToInt16(t.GetWins() + t.GetLosses())));
                }
                else
                {
                    t.SetStrength(double.MaxValue);
                }
            }
            SOS = ncaa.GetFBS().OrderBy(o => o.GetStrength()).ToList();
            int index = 1;
            foreach (Team team in SOS)
            {
                team.SetSOSRank(index);
                index++;
            }
        }
    }
}
