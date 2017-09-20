using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using CFBPredictor.Models;
using CFBPredictor.Service;
using CFBpredictor.View;

namespace CFBPredictor
{
    public partial class Ranker : Form
    {
        NCAA ncaa;
        List<Team> SOS = new List<Team>();
        
        List<Team> DivisionI = new List<Team>();
        private const string SCORES_PATH = "C:/Users/Danny/GitHub/CFB_Ranker/CFBPredictor/scores/";
        private const string EXCEL_PATH = "C:/Users/Danny/Documents/";
        private int weeksAdded; //Keeps track of how many weeks have been added to prevent duplicate score additions        

        Filter filter;
        
        public Ranker()
        {
            InitializeComponent();
            this.Text = "College Football Rankings";
            uxRankings.DoubleClick += new EventHandler(uxRankings_DoubleClick);
        }

        /// <summary>
        /// Adds proper list of rankings to textbox based on user selected filter.
        /// </summary>
        /// <param name="Rankings"></param>
        private void AddRankings(List<Team> Rankings)
        {
            //Adds the teams and ratings to listbox
            int i = 1;
            List<Conference> confRankings = ncaa.GetConferences().OrderBy(o => o.rating).ToList();

            try
            {
                int rank = 1; //used to rank conferences
      
                if (filter == Filter.NCAADivisionI)
                {
                    foreach (Team team in Rankings)
                    {
                        uxRankings.Items.Add(i + ". " + team.GetTeamName() + " " + team.rating);
                        i++;
                    }
                }

                else if (filter == Filter.FBS)
                {
                    foreach (Team team in Rankings)
                    {
                        if (!team.GetTeamConference().Equals(Names.FCS))
                        {
                            uxRankings.Items.Add(i + ". " + team.GetTeamName() + " " + team.rating);
                            i++;
                        }
                    }
                }

                else if (filter == Filter.FCS)
                {
                    foreach (Team team in Rankings)
                    {
                        if (team.GetTeamConference().Equals(Names.FCS))
                        {
                            uxRankings.Items.Add(i + ". " + team.GetTeamName() + " " + team.rating);
                            i++;
                        }
                    }
                }

                else if (filter == Filter.AAC)
                {
                    foreach (Team team in Rankings)
                    {
                        if (team.GetTeamConference().Equals(Names.AAC))
                        {
                            uxRankings.Items.Add(i + ".(" + team.GetFBSRank() + ") " + team.GetTeamName() + " " + team.rating);
                            i++;
                        }
                    }
                }

                else if (filter == Models.Filter.ACC)
                {
                    foreach (Team team in Rankings)
                    {
                        if (team.GetTeamConference().Equals(Names.ACC))
                        {
                            uxRankings.Items.Add(i + ".(" + team.GetFBSRank() + ") " + team.GetTeamName() + " " + team.rating);
                            i++;
                        }
                    }
                }

                else if (filter == Filter.Big12)
                {
                    foreach (Team team in Rankings)
                    {
                        if (team.GetTeamConference().Equals(Names.Big12))
                        {
                            uxRankings.Items.Add(i + ".(" + team.GetFBSRank() + ") " + team.GetTeamName() + " " + team.rating);
                            i++;
                        }
                    }
                }

                else if (filter == Filter.BigTen)
                {
                    foreach (Team team in Rankings)
                    {
                        if (team.GetTeamConference().Equals(Names.Big10))
                        {
                            uxRankings.Items.Add(i + ".(" + team.GetFBSRank() + ") " + team.GetTeamName() + " " + team.rating);
                            i++;
                        }
                    }
                }

                else if (filter == Models.Filter.CUSA)
                {
                    foreach (Team team in Rankings)
                    {
                        if (team.GetTeamConference().Equals(Names.CUSA))
                        {
                            uxRankings.Items.Add(i + ".(" + team.GetFBSRank() + ") " + team.GetTeamName() + " " + team.rating);
                            i++;
                        }
                    }
                }

                else if (filter == Models.Filter.Independent)
                {
                    foreach (Team team in Rankings)
                    {
                        if (team.GetTeamConference().Equals(Names.Independent))
                        {
                            uxRankings.Items.Add(i + ".(" + team.GetFBSRank() + ") " + team.GetTeamName() + " " + team.rating);
                            i++;
                        }
                    }
                }

                else if (filter == Models.Filter.MAC)
                {
                    foreach (Team team in Rankings)
                    {
                        if (team.GetTeamConference().Equals(Names.MAC))
                        {
                            uxRankings.Items.Add(i + ".(" + team.GetFBSRank() + ") " + team.GetTeamName() + " " + team.rating);
                            i++;
                        }
                    }
                }

                else if (filter == Models.Filter.MWC)
                {
                    foreach (Team team in Rankings)
                    {
                        if (team.GetTeamConference().Equals(Names.MWC))
                        {
                            uxRankings.Items.Add(i + ".(" + team.GetFBSRank() + ") " + team.GetTeamName() + " " + team.rating);
                            i++;
                        }
                    }
                }

                else if (filter == Models.Filter.Pac12)
                {
                    foreach (Team team in Rankings)
                    {
                        if (team.GetTeamConference().Equals(Names.Pac12))
                        {
                            uxRankings.Items.Add(i + ".(" + team.GetFBSRank() + ") " + team.GetTeamName() + " " + team.rating);
                            i++;
                        }
                    }
                }

                else if (filter == Models.Filter.SEC)
                {
                    foreach (Team team in Rankings)
                    {
                        if (team.GetTeamConference().Equals(Names.SEC))
                        {
                            uxRankings.Items.Add(i + ".(" + team.GetFBSRank() + ") " + team.GetTeamName() + " " + team.rating);
                            i++;
                        }
                    }
                }

                else if (filter == Models.Filter.SunBelt)
                {
                    foreach (Team team in Rankings)
                    {
                        if (team.GetTeamConference().Equals(Names.SunBelt))
                        {
                            uxRankings.Items.Add(i + ".(" + team.GetFBSRank() + ") " + team.GetTeamName() + " " + team.rating);
                            i++;
                        }
                    }
                }

                else if (filter == Models.Filter.ConferenceRank)
                {
                    uxRankings.Items.Clear();
                    foreach (Conference conference in confRankings)
                    {
                        uxRankings.Items.Add(rank + ". " + conference.GetConferenceName() + " (" + conference.rating + ")");
                        rank++;
                    }
                }

                else if (filter == Models.Filter.SOS)
                {
                    List<Team> SOSRank = Rankings.OrderBy(o => o.GetStrength()).ToList();
                    foreach (Team team in SOSRank)
                    {
                        if (team.GetTeamConference() != Names.FCS)
                        {
                            uxRankings.Items.Add(rank + ". " + team.GetTeamName());
                            rank++;
                        }
                    }
                }

                else if (filter == Models.Filter.OffenseRank)
                {
                    if (uxRankings.Items.Count == 0)
                    {
                        uxRankings.Items.Clear();
                        double low = int.MaxValue;
                        foreach (Team t in ncaa.GetFBS())
                        {
                            if (t.GetPPGvsOppAvg() < low)
                            {
                                low = t.GetPPGvsOppAvg();
                            }
                        }

                        foreach (Team t in ncaa.GetFBS())
                        {
                            t.SetOppAdjustedPPG(low);
                        }

                        List<Team> Offense = ncaa.GetFBS().OrderBy(o => o.GetOppAdjustedPPG()).Reverse().ToList();
                        foreach (Team t in Offense)
                        {
                            uxRankings.Items.Add(rank + ". " + t.GetTeamName() + " (" + t.GetOppAdjustedPPG() + ")");
                            rank++;
                        }
                    }
                }

                else if (filter == Models.Filter.DefenseRank)
                {
                    if (uxRankings.Items.Count == 0)
                    {
                        uxRankings.Items.Clear();
                        double high = -100;
                        foreach (Team t in ncaa.GetFBS())
                        {
                            if (t.GetDefensePPGvsOppAvg() > high)
                            {
                                high = t.GetDefensePPGvsOppAvg();
                            }
                        }

                        foreach (Team t in ncaa.GetFBS())
                        {
                            t.SetOppAdjustedDPPG(high);
                        }

                        List<Team> Defense = ncaa.GetFBS().OrderBy(o => o.GetOppAdjustedDPPG()).Reverse().ToList();
                        foreach (Team t in Defense)
                        {
                            uxRankings.Items.Add(rank + ". " + t.GetTeamName() + " (" + t.GetOppAdjustedDPPG() + ")");
                            rank++;
                        }
                    }
                }
            }
                
            catch
            {
                ClearRankings();
                return;
            }
        }

        public void ClearRankings()
        {
            if (ncaa != null)
            {
                foreach (Team team in ncaa.GetFBS())
                {
                    team.Clear();
                    team.ClearProjectedPoints();
                }

                foreach (Team team in ncaa.GetFCS())
                {
                    team.Clear();
                }

                foreach (Conference conference in ncaa.GetConferences())
                {
                    conference.rating = 0;
                    conference.ClearMembers();
                }
            }
            uxRankings.Items.Clear();
        }  

        private void DisplayScore(Game game)
        {
            int awayPoints = (int) Math.Round(game.GetAwayTeam().GetPoints());
            int homePoints = (int) Math.Round(game.GetHomeTeam().GetPoints());
            MessageBox.Show(game.GetAwayTeam().GetTeamName() + ": " + awayPoints + "\n" + game.GetHomeTeam().GetTeamName() + ": " + homePoints);
        }

        private void GetInfo(bool DoubleClick)
        {
            if (uxTeamName.Text != "" && !DoubleClick)
            {
                foreach (Team team in ncaa.GetFBS())
                {
                    if (team.GetTeamName().Equals(uxTeamName.Text))
                    {
                        TeamForm tf = new TeamForm(team);
                        tf.Show();
                    }
                }

                foreach (Team team in ncaa.GetFCS())
                {
                    if (team.GetTeamName().Equals(uxTeamName.Text))
                    {
                        TeamForm tf = new TeamForm(team);
                        tf.Show();
                    }
                }
            }

            else if (uxRankings.SelectedItem != null)
            {
                if (!uxFilter.SelectedItem.Equals(Names.SOS))
                {
                    StringBuilder teamstring = new StringBuilder();
                    string line = uxRankings.SelectedItem.ToString();
                    string[] n = line.Split(' ');
                    for (int i = 0; i < n.Length; i++)
                    {
                        if (Char.IsLetter(n[i][0]))
                        {
                            if (i == 1)
                                teamstring.Append(n[i]);
                            else
                                teamstring.Append(" " + n[i]);
                        }
                    }
                    string teamName = teamstring.ToString();

                    foreach (Team team in ncaa.GetFBS())
                    {
                        if (team.GetTeamName().Equals(teamName))
                        {
                            TeamForm tf = new TeamForm(team);
                            tf.Show();
                        }
                    }

                    foreach (Team team in ncaa.GetFCS())
                    {
                        if (team.GetTeamName().Equals(teamName))
                        {
                            TeamForm tf = new TeamForm(team);
                            tf.Show();
                        }
                    }
                }
            }
        }

        private bool SetFilter()
        {
            try
            {
                if (uxFilter.SelectedItem.Equals(Names.Division1))
                    filter = Models.Filter.NCAADivisionI;
                else if (uxFilter.SelectedItem.Equals(Names.FBS))
                    filter = Models.Filter.FBS;
                else if (uxFilter.SelectedItem.Equals(Names.FCS))
                    filter = Models.Filter.FCS;
                else if (uxFilter.SelectedItem.Equals(Names.AAC))
                    filter = Models.Filter.AAC;
                else if (uxFilter.SelectedItem.Equals(Names.ACC))
                    filter = Models.Filter.ACC;
                else if (uxFilter.SelectedItem.Equals(Names.Big12))
                    filter = Models.Filter.Big12;
                else if (uxFilter.SelectedItem.Equals(Names.Big10))
                    filter = Models.Filter.BigTen;
                else if (uxFilter.SelectedItem.Equals(Names.CUSA))
                    filter = Models.Filter.CUSA;
                else if (uxFilter.SelectedItem.Equals(Names.Independent))
                    filter = Models.Filter.Independent;
                else if (uxFilter.SelectedItem.Equals(Names.MAC))
                    filter = Models.Filter.MAC;
                else if (uxFilter.SelectedItem.Equals(Names.MWC))
                    filter = Models.Filter.MWC;
                else if (uxFilter.SelectedItem.Equals(Names.Pac12))
                    filter = Models.Filter.Pac12;
                else if (uxFilter.SelectedItem.Equals(Names.SEC))
                    filter = Models.Filter.SEC;
                else if (uxFilter.SelectedItem.Equals(Names.SunBelt))
                    filter = Models.Filter.SunBelt;
                else if (uxFilter.SelectedItem.Equals(Names.ConferenceRank))
                    filter = Models.Filter.ConferenceRank;
                else if (uxFilter.SelectedItem.Equals(Names.SOS))
                    filter = Models.Filter.SOS;
                else if (uxFilter.SelectedItem.Equals(Names.OffenseRank))
                    filter = Models.Filter.OffenseRank;
                else if (uxFilter.SelectedItem.Equals(Names.DefenseRank))
                    filter = Models.Filter.DefenseRank;

                return true;
            }

            catch
            {
                MessageBox.Show(Messages.SELECT_FILTER_MESSAGE);
                return false;
            }
        }

        private void uxClearRankings_Click(object sender, EventArgs e)
        {
            ClearRankings();
        }

        private void uxCompile_Click(object sender, EventArgs e)
        { 
            if (SetFilter())
            {
                ClearRankings();
                
                if (ScoreCompiler.ReadScores(uxYear.Text, SCORES_PATH))
                {
                    ncaa = ScoreCompiler.ncaa;
                    RankingGenerator rankingGenerator = new RankingGenerator(ncaa);
                    List<Team> FinalRankings = rankingGenerator.GenerateRankings();
                    AddRankings(FinalRankings);
                }
            }
        }

        private void uxExport_Click(object sender, EventArgs e)
        {
            if (uxRankings.Items.Count != 0)
            {
                RankingExporter rankingExporter = new RankingExporter(ncaa, uxYear.SelectedItem.ToString());
                rankingExporter.ExportRankings();
                if (RankingExporter.UploadToDrive(EXCEL_PATH + "Rankings.xlsx"))
                {
                    MessageBox.Show(Messages.EXPORT_COMPLETE_MESSAGE);
                }

                else
                {
                    MessageBox.Show(Messages.DRIVE_UPLOAD_ERROR_MESSAGE);
                }
            }

            else
            {
                MessageBox.Show(Messages.NO_RANKINGS_MESSAGE);
            }
        }

        private void uxGetSchedule_Click(object sender, EventArgs e)
        {
            if (uxTeamName.Text != "")
            {
                foreach (Team team in ncaa.GetFBS())
                {
                    if (team.GetTeamName().Equals(uxTeamName.Text))
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.Append(team.GetFBSRank() + ". " + team.GetTeamName() + " (" + team.GetWins() + "-" + team.GetLosses() + ")  SOS: " + team.GetSOSRank() + "\n_____________________\n\n");
                        for (int i = 0; i < team.GetScheduleSize(); i++)
                        {
                            if (team.GetOpponent(i).GetFBSRank() != 0)
                                sb.Append(team.GetOpponent(i).GetFBSRank() + ". " + team.GetOpponent(i).GetTeamName() + " (" + team.GetOpponent(i).GetWins() + "-" + team.GetOpponent(i).GetLosses() + ")\n");
                            else sb.Append(team.GetOpponent(i).GetTeamName() + " (" + team.GetOpponent(i).GetWins() + "-" + team.GetOpponent(i).GetLosses() + ")\n");
                        }

                        MessageBox.Show(sb.ToString());
                        return;
                    }
                }

                foreach (Team team in ncaa.GetFCS())
                {
                    if (team.GetTeamName().Equals(uxTeamName.Text))
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.Append(team.GetFBSRank() + ". " + team.GetTeamName() + " (" + team.GetWins() + "-" + team.GetLosses() + ")  SOS: " + team.GetSOSRank() + "\n_____________________\n\n");
                        for (int i = 0; i < team.GetScheduleSize(); i++)
                        {
                            sb.Append(team.GetOpponent(i).GetTeamName() + " (" + team.GetOpponent(i).GetWins() + "-" + team.GetOpponent(i).GetLosses() + ")\n");
                        }

                        MessageBox.Show(sb.ToString());
                        return;
                    }
                }
                
            }

            else if (uxRankings.SelectedItem != null)
            {
                StringBuilder teamstring = new StringBuilder();
                string line = uxRankings.SelectedItem.ToString();
                string[] n = line.Split(' ');
                for (int i = 0; i < n.Length; i++)
                {
                    if (Char.IsLetter(n[i][0]))
                    {
                        if (i == 1)
                            teamstring.Append(n[i]);
                        else
                            teamstring.Append(" " + n[i]);
                    }
                }
                string teamName = teamstring.ToString();

                foreach (Team team in ncaa.GetFBS())
                {
                    if (team.GetTeamName().Equals(teamName))
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.Append(team.GetFBSRank() + ". " + team.GetTeamName() + " (" + team.GetWins() + "-" + team.GetLosses() + ")  SOS: " + team.GetSOSRank() + "\n_____________________\n\n");
                        for (int i = 0; i < team.GetScheduleSize(); i++)
                        {
                            if (team.GetOpponent(i).GetFBSRank() != 0)
                                sb.Append(team.GetOpponent(i).GetFBSRank() + ". " + team.GetOpponent(i).GetTeamName() + " (" + team.GetOpponent(i).GetWins() + "-" + team.GetOpponent(i).GetLosses() + ")\n");
                            else sb.Append(team.GetOpponent(i).GetTeamName() + " (" + team.GetOpponent(i).GetWins() + "-" + team.GetOpponent(i).GetLosses() + ")\n");
                        }

                        MessageBox.Show(sb.ToString());
                        return;
                    }
                }

                foreach (Team team in ncaa.GetFCS())
                {
                    if (team.GetTeamName().Equals(teamName))
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.Append(team.GetFBSRank() + ". " + team.GetTeamName() + " (" + team.GetWins() + "-" + team.GetLosses() + ")  SOS: " + team.GetSOSRank() + "\n_____________________\n\n");
                        for (int i = 0; i < team.GetScheduleSize(); i++)
                        {
                            sb.Append(team.GetOpponent(i).GetTeamName() + " (" + team.GetOpponent(i).GetWins() + "-" + team.GetOpponent(i).GetLosses() + ")\n");
                        }

                        MessageBox.Show(sb.ToString());
                        return;
                    }
                }
            }
        }

        private void uxPredict_Click(object sender, EventArgs e)
        {
            GameSimulator gameSimulator = new GameSimulator(ncaa, uxHomeTeam.Text, uxAwayTeam.Text);
            Game simulatedGame = gameSimulator.SimulateGame();
            if (simulatedGame == null)
            {
                MessageBox.Show(Messages.INVALID_TEAM_NAME_MESSAGE);
            }
            else
            {
                DisplayScore(simulatedGame);
                simulatedGame.GetHomeTeam().ClearProjectedPoints();
                simulatedGame.GetAwayTeam().ClearProjectedPoints();
            }       
        }

        private void uxSimWeek_Click(object sender, EventArgs e)
        {
            string week;
            File.Delete(SCORES_PATH + "SimmedWeek.txt");
            try
            {
                week = uxWeek.SelectedItem.ToString();
            }
            catch (NullReferenceException)
            {
                MessageBox.Show(Messages.SELECT_WEEK_MESSAGE);
                return;
            }

            WeekSimulator.SimWeek(ncaa, week);
            WeekSimulator.WriteSimmedScores(ncaa, Convert.ToInt16(week));
        }

        private void uxSubmit_Click(object sender, EventArgs e)
        {
            string week;

            try
            {
                week = uxWeek.SelectedItem.ToString();
                string file = SCORES_PATH + "NumWeeksAdded.txt";
                StreamReader sr = new StreamReader(file);
                weeksAdded = Convert.ToInt16(sr.ReadLine());
                sr.Close();
            }

            catch(IOException)
            {
                MessageBox.Show(Messages.FILE_NOT_FOUND_MESSAGE);
                return;
            }
            catch
            {
                MessageBox.Show(Messages.SELECT_WEEK_MESSAGE);
                return;
            }

            if (Convert.ToInt32(week) > weeksAdded)
            {
                string resultMessage = ScoreAdder.AddScores(ncaa, week);

                if (resultMessage != Messages.WEEK_NOT_CONCLUDED_MESSAGE)
                {
                    StreamWriter sw = new StreamWriter(SCORES_PATH + "NumWeeksAdded.txt");
                    using (sw)
                    {
                        sw.Write(++weeksAdded);
                    }
                }

                MessageBox.Show(resultMessage);
            }

            else
            {
                MessageBox.Show(Messages.WEEK_PREVIOUSLY_ADDED_MESSAGE);
            }
        }

        private void uxGetInfo_Click(object sender, EventArgs e)
        {
            GetInfo(false);
        }

        private void uxYear_SelectedItemChanged(object sender, EventArgs e)
        {

        }

        private void uxRankings_DoubleClick(object sender, EventArgs e)
        {
            GetInfo(true);
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void uxClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
