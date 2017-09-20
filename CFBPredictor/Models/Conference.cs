using System.Collections.Generic;

namespace CFBPredictor.Models
{
    public class Conference
    {
        private string conferenceName;
        public double rating = 0;
        private int rank;
        private List<Team> members = new List<Team>();

        /// <summary>
        /// Constructor which sets the conferences name
        /// </summary>
        /// <param name="name">name of the conference</param>
        public Conference(string name)
        {
            conferenceName = name;
        }

        /// <summary>
        /// Gets the name of the conference
        /// </summary>
        /// <returns>the conference name</returns>
        public string GetConferenceName()
        {
            return conferenceName;
        }

        /// <summary>
        /// Gets the conference's rank
        /// </summary>
        /// <returns>the conference's rank</returns>
        public int GetRank()
        {
            return rank;
        }

        /// <summary>
        /// Gets the members of the conference
        /// </summary>
        /// <returns>a list of teams that are members of the conference</returns>
        public List<Team> GetMembers()
        {
            return members;
        }

        /// <summary>
        /// Sets the rank for the conference
        /// </summary>
        /// <param name="rank">the rank for the conference</param>
        public void SetRank(int rank)
        {
            this.rank = rank;
        }

        /// <summary>
        /// Adds a member to the conference
        /// </summary>
        /// <param name="team">the team to add to the conference</param>
        public void AddMember(Team team)
        {
            members.Add(team);
        }

        /// <summary>
        /// Removes all members from the conference
        /// </summary>
        public void ClearMembers()
        {
            members.Clear();
        }
    }
}
