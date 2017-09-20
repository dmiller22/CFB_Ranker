namespace CFBPredictor
{
    partial class Ranker
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.uxAwayTeam = new System.Windows.Forms.TextBox();
            this.uxHomeTeam = new System.Windows.Forms.TextBox();
            this.uxGetScores = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.uxCompile = new System.Windows.Forms.Button();
            this.uxRankings = new System.Windows.Forms.ListBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.label5 = new System.Windows.Forms.Label();
            this.uxClearRankings = new System.Windows.Forms.Button();
            this.uxTeamName = new System.Windows.Forms.TextBox();
            this.uxGetInfo = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.uxYear = new System.Windows.Forms.DomainUpDown();
            this.uxGetSchedule = new System.Windows.Forms.Button();
            this.uxPredict = new System.Windows.Forms.Button();
            this.uxSimWeek = new System.Windows.Forms.Button();
            this.uxExport = new System.Windows.Forms.Button();
            this.uxWeek = new System.Windows.Forms.DomainUpDown();
            this.uxClose = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.uxFilter = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // uxAwayTeam
            // 
            this.uxAwayTeam.Location = new System.Drawing.Point(12, 39);
            this.uxAwayTeam.Name = "uxAwayTeam";
            this.uxAwayTeam.Size = new System.Drawing.Size(148, 20);
            this.uxAwayTeam.TabIndex = 0;
            // 
            // uxHomeTeam
            // 
            this.uxHomeTeam.Location = new System.Drawing.Point(12, 83);
            this.uxHomeTeam.Name = "uxHomeTeam";
            this.uxHomeTeam.Size = new System.Drawing.Size(148, 20);
            this.uxHomeTeam.TabIndex = 1;
            // 
            // uxGetScores
            // 
            this.uxGetScores.Location = new System.Drawing.Point(86, 298);
            this.uxGetScores.Name = "uxGetScores";
            this.uxGetScores.Size = new System.Drawing.Size(73, 20);
            this.uxGetScores.TabIndex = 4;
            this.uxGetScores.Text = "Get Scores";
            this.uxGetScores.UseVisualStyleBackColor = true;
            this.uxGetScores.Click += new System.EventHandler(this.uxSubmit_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(63, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Away";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(63, 106);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Home";
            // 
            // uxCompile
            // 
            this.uxCompile.Location = new System.Drawing.Point(32, 231);
            this.uxCompile.Name = "uxCompile";
            this.uxCompile.Size = new System.Drawing.Size(100, 23);
            this.uxCompile.TabIndex = 8;
            this.uxCompile.Text = "Compile Rankings";
            this.uxCompile.UseVisualStyleBackColor = true;
            this.uxCompile.Click += new System.EventHandler(this.uxCompile_Click);
            // 
            // uxRankings
            // 
            this.uxRankings.FormattingEnabled = true;
            this.uxRankings.Location = new System.Drawing.Point(323, 106);
            this.uxRankings.Name = "uxRankings";
            this.uxRankings.Size = new System.Drawing.Size(212, 212);
            this.uxRankings.TabIndex = 13;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "collegefootballscores.txt";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog1_FileOk);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(401, 90);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 13);
            this.label5.TabIndex = 22;
            this.label5.Text = "Rankings";
            // 
            // uxClearRankings
            // 
            this.uxClearRankings.Location = new System.Drawing.Point(379, 339);
            this.uxClearRankings.Name = "uxClearRankings";
            this.uxClearRankings.Size = new System.Drawing.Size(97, 23);
            this.uxClearRankings.TabIndex = 27;
            this.uxClearRankings.Text = "Clear Rankings";
            this.uxClearRankings.UseVisualStyleBackColor = true;
            this.uxClearRankings.Click += new System.EventHandler(this.uxClearRankings_Click);
            // 
            // uxTeamName
            // 
            this.uxTeamName.Location = new System.Drawing.Point(15, 342);
            this.uxTeamName.Name = "uxTeamName";
            this.uxTeamName.Size = new System.Drawing.Size(146, 20);
            this.uxTeamName.TabIndex = 28;
            // 
            // uxGetInfo
            // 
            this.uxGetInfo.Location = new System.Drawing.Point(16, 368);
            this.uxGetInfo.Name = "uxGetInfo";
            this.uxGetInfo.Size = new System.Drawing.Size(64, 23);
            this.uxGetInfo.TabIndex = 29;
            this.uxGetInfo.Text = "Team Info";
            this.uxGetInfo.UseVisualStyleBackColor = true;
            this.uxGetInfo.Click += new System.EventHandler(this.uxGetInfo_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(63, 161);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 13);
            this.label4.TabIndex = 31;
            this.label4.Text = "Year";
            // 
            // uxYear
            // 
            this.uxYear.Items.Add("2017");
            this.uxYear.Items.Add("2016");
            this.uxYear.Items.Add("2015");
            this.uxYear.Items.Add("2014");
            this.uxYear.Items.Add("2013");
            this.uxYear.Items.Add("2012");
            this.uxYear.Items.Add("2011");
            this.uxYear.Items.Add("Fake");
            this.uxYear.Location = new System.Drawing.Point(43, 138);
            this.uxYear.Name = "uxYear";
            this.uxYear.Size = new System.Drawing.Size(74, 20);
            this.uxYear.TabIndex = 32;
            this.uxYear.SelectedItemChanged += new System.EventHandler(this.uxYear_SelectedItemChanged);
            // 
            // uxGetSchedule
            // 
            this.uxGetSchedule.Location = new System.Drawing.Point(86, 368);
            this.uxGetSchedule.Name = "uxGetSchedule";
            this.uxGetSchedule.Size = new System.Drawing.Size(65, 23);
            this.uxGetSchedule.TabIndex = 33;
            this.uxGetSchedule.Text = "Schedule";
            this.uxGetSchedule.UseVisualStyleBackColor = true;
            this.uxGetSchedule.Click += new System.EventHandler(this.uxGetSchedule_Click);
            // 
            // uxPredict
            // 
            this.uxPredict.Location = new System.Drawing.Point(166, 56);
            this.uxPredict.Name = "uxPredict";
            this.uxPredict.Size = new System.Drawing.Size(100, 23);
            this.uxPredict.TabIndex = 35;
            this.uxPredict.Text = "Simulate";
            this.uxPredict.UseVisualStyleBackColor = true;
            this.uxPredict.Click += new System.EventHandler(this.uxPredict_Click);
            // 
            // uxSimWeek
            // 
            this.uxSimWeek.Location = new System.Drawing.Point(86, 272);
            this.uxSimWeek.Name = "uxSimWeek";
            this.uxSimWeek.Size = new System.Drawing.Size(73, 20);
            this.uxSimWeek.TabIndex = 37;
            this.uxSimWeek.Text = "Sim Week";
            this.uxSimWeek.UseVisualStyleBackColor = true;
            this.uxSimWeek.Click += new System.EventHandler(this.uxSimWeek_Click);
            // 
            // uxExport
            // 
            this.uxExport.Location = new System.Drawing.Point(379, 368);
            this.uxExport.Name = "uxExport";
            this.uxExport.Size = new System.Drawing.Size(97, 23);
            this.uxExport.TabIndex = 40;
            this.uxExport.Text = "Export Rankings";
            this.uxExport.UseVisualStyleBackColor = true;
            this.uxExport.Click += new System.EventHandler(this.uxExport_Click);
            // 
            // uxWeek
            // 
            this.uxWeek.Items.Add("01");
            this.uxWeek.Items.Add("02");
            this.uxWeek.Items.Add("03");
            this.uxWeek.Items.Add("04");
            this.uxWeek.Items.Add("05");
            this.uxWeek.Items.Add("06");
            this.uxWeek.Items.Add("07");
            this.uxWeek.Items.Add("08");
            this.uxWeek.Items.Add("09");
            this.uxWeek.Items.Add("10");
            this.uxWeek.Items.Add("11");
            this.uxWeek.Items.Add("12");
            this.uxWeek.Items.Add("13");
            this.uxWeek.Items.Add("14");
            this.uxWeek.Items.Add("15");
            this.uxWeek.Items.Add("16");
            this.uxWeek.Items.Add("17");
            this.uxWeek.Items.Add("18");
            this.uxWeek.Items.Add("19");
            this.uxWeek.Items.Add("20");
            this.uxWeek.Location = new System.Drawing.Point(15, 282);
            this.uxWeek.Name = "uxWeek";
            this.uxWeek.Size = new System.Drawing.Size(52, 20);
            this.uxWeek.TabIndex = 42;
            // 
            // uxClose
            // 
            this.uxClose.Location = new System.Drawing.Point(229, 400);
            this.uxClose.Name = "uxClose";
            this.uxClose.Size = new System.Drawing.Size(75, 23);
            this.uxClose.TabIndex = 43;
            this.uxClose.Text = "Close";
            this.uxClose.UseVisualStyleBackColor = true;
            this.uxClose.Click += new System.EventHandler(this.uxClose_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 305);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 13);
            this.label3.TabIndex = 44;
            this.label3.Text = "Week";
            // 
            // uxFilter
            // 
            this.uxFilter.FormattingEnabled = true;
            this.uxFilter.Items.AddRange(new object[] {
            "FBS",
            "FCS",
            "AAC",
            "ACC",
            "Big 12",
            "Big Ten",
            "Conference USA",
            "Independent",
            "MAC",
            "MWC",
            "Pac 12",
            "SEC",
            "Sun Belt",
            "Conference Rank",
            "Strength of Schedule",
            "Offense Rank",
            "Defense Rank"});
            this.uxFilter.Location = new System.Drawing.Point(21, 186);
            this.uxFilter.Name = "uxFilter";
            this.uxFilter.Size = new System.Drawing.Size(121, 21);
            this.uxFilter.TabIndex = 45;
            this.uxFilter.Text = "FBS";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(63, 210);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 13);
            this.label6.TabIndex = 46;
            this.label6.Text = "Filter";
            // 
            // Ranker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(547, 429);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.uxFilter);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.uxClose);
            this.Controls.Add(this.uxWeek);
            this.Controls.Add(this.uxExport);
            this.Controls.Add(this.uxSimWeek);
            this.Controls.Add(this.uxPredict);
            this.Controls.Add(this.uxGetSchedule);
            this.Controls.Add(this.uxYear);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.uxGetInfo);
            this.Controls.Add(this.uxTeamName);
            this.Controls.Add(this.uxClearRankings);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.uxRankings);
            this.Controls.Add(this.uxCompile);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.uxGetScores);
            this.Controls.Add(this.uxHomeTeam);
            this.Controls.Add(this.uxAwayTeam);
            this.Name = "Ranker";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox uxAwayTeam;
        private System.Windows.Forms.TextBox uxHomeTeam;
        private System.Windows.Forms.Button uxGetScores;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button uxCompile;
        private System.Windows.Forms.ListBox uxRankings;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button uxClearRankings;
        private System.Windows.Forms.TextBox uxTeamName;
        private System.Windows.Forms.Button uxGetInfo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DomainUpDown uxYear;
        private System.Windows.Forms.Button uxGetSchedule;
        private System.Windows.Forms.Button uxPredict;
        private System.Windows.Forms.Button uxSimWeek;
        private System.Windows.Forms.Button uxExport;
        private System.Windows.Forms.DomainUpDown uxWeek;
        private System.Windows.Forms.Button uxClose;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox uxFilter;
        private System.Windows.Forms.Label label6;
    }
}

