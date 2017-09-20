using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CFBPredictor.Models;

namespace CFBPredictor
{
    public partial class TeamForm : Form
    {
        public TeamForm(Team t)
        {
            InitializeComponent();
            this.Text = t.GetTeamName();
            Image logo = null;
            try
            {
                logo = Image.FromFile("C:/Users/Danny/GitHub/CollegeFootballRanker/CFBpredictor/scores/Logos/" + t.GetTeamName() + ".png");
            }

            catch { }
            TRecord.Text = "(" + t.GetWins() + "-" + t.GetLosses() + ")";
            TRating.Text = t.rating.ToString();
            TRank.Text = t.GetFBSRank().ToString();
            Tppg.Text = t.GetPPG().ToString();
            Tpapg.Text = t.GetDefensePPG().ToString();
            Tppgvsavg.Text = t.GetPPGvsOppAvg().ToString();
            Tpavsavg.Text = t.GetDefensePPGvsOppAvg().ToString();
            Tsos.Text = t.GetSOSRank().ToString();
            if (logo != null)
            {
                TeamLogo.Image = logo;
            }
        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
