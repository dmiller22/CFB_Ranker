using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CFBPredictor.Models;
using System.Net.Mail;
using System.Net;
using System.IO;
using Google.Apis.Drive.v3;
using Google.Apis.Auth.OAuth2;
using Microsoft.Office.Interop.Excel;
using Google.Apis.Services;
using System.Threading;
using Google.Apis.Util.Store;

namespace CFBPredictor.Service
{
    /// <summary>
    /// Exports rankings to a text and excel file and sends those files as an
    /// attachment in an email
    /// </summary>
    public class RankingExporter
    {
        private NCAA ncaa;
        private string year;
        private string scoresPath = "C:/Users/Danny/GitHub/CollegeFootballRanker/CFBpredictor/scores/";
        private string excelPath = "C:/Users/Danny/Documents/";
        private string auth = "C:/Users/resources/auth.txt";
        private static string[] scopes = { DriveService.Scope.Drive };

        /// <summary>
        /// Constructor that sets the NCAA object and the year to export
        /// </summary>
        /// <param name="ncaa">list of all teams in division 1</param>
        /// <param name="year">year of rankings to export</param>
        public RankingExporter(NCAA ncaa, string year)
        {
            this.ncaa = ncaa;
            this.year = year;
        }

        /// <summary>
        /// Takes the rankings and writes them to a text file and sends that file 
        /// as an attachment in an email. Also writes them to an excel file and
        /// attaches that to the email as well
        /// </summary>
        public void ExportRankings()
        {
            StringBuilder text = new StringBuilder();
            int i = 1;
            List<Team> FBSRankings = ncaa.GetFBS().OrderBy(o => o.rating).Reverse().ToList();
            List<Team> FCSRankings = ncaa.GetFCS().OrderBy(o => o.rating).Reverse().ToList();

            Application xlApp = new Application();
            Workbook xlWorkBook = xlApp.Workbooks.Add();
            Worksheet FBSRankSheet = (Worksheet)xlWorkBook.Worksheets.get_Item(1);
            Worksheet FCSRankSheet = (Worksheet)xlWorkBook.Worksheets.get_Item(2);
            Worksheet SOSRankSheet = (Worksheet)xlWorkBook.Worksheets.get_Item(3);
            FBSRankSheet.Columns[1].ColumnWidth = 5;
            FBSRankSheet.Columns[2].ColumnWidth = 20;
            FBSRankSheet.Cells[1, 2] = "Rankings (" + DateTime.Today.Date.Month + "/" + DateTime.Today.Date.Day + "/" + DateTime.Today.Date.Year + ")";
            int rank_index = 1;
            foreach (Team team in FBSRankings)
            {
                FBSRankSheet.Cells[rank_index + 1, 1] = rank_index;
                FBSRankSheet.Cells[rank_index + 1, 2] = team.GetTeamName();
                FBSRankSheet.Cells[rank_index + 1, 3] = String.Format("{0:0.000}", team.rating);
                rank_index++;
            }

            FCSRankSheet.Columns[1].ColumnWidth = 5;
            FCSRankSheet.Columns[2].ColumnWidth = 20;
            FCSRankSheet.Cells[1, 2] = "Rankings (" + DateTime.Today.Date.Month + "/" + DateTime.Today.Date.Day + "/" + DateTime.Today.Date.Year + ")";
            rank_index = 1;
            foreach (Team team in FCSRankings)
            {
                FCSRankSheet.Cells[rank_index + 1, 1] = rank_index;
                FCSRankSheet.Cells[rank_index + 1, 2] = team.GetTeamName();
                FCSRankSheet.Cells[rank_index + 1, 3] = String.Format("{0:0.000}", team.rating);
                rank_index++;
            }

            SOSRankSheet.Columns[1].ColumnWidth = 5;
            SOSRankSheet.Columns[2].ColumnWidth = 20;
            SOSRankSheet.Cells[1, 2] = "Strength of Schedule (" + DateTime.Today.Date.Month + "/" + DateTime.Today.Date.Day + "/" + DateTime.Today.Date.Year + ")";
            rank_index = 1;
            List<Team> SOS = ncaa.GetFBS().OrderBy(o => o.GetStrength()).ToList();
            foreach (Team team in SOS)
            {
                SOSRankSheet.Cells[rank_index + 1, 1] = rank_index;
                SOSRankSheet.Cells[rank_index + 1, 2] = team.GetTeamName();
                rank_index++;
            }

            xlApp.DisplayAlerts = false;
            FBSRankSheet.Name = "FBS Rankings";
            FCSRankSheet.Name = "FCS Rankings";
            SOSRankSheet.Name = "SOS Rank";
            xlWorkBook.SaveAs("Rankings.xlsx");
            xlWorkBook.Close();

            foreach (Team team in FBSRankings)
            {

                if (team.GetFBSRank() < 100 && team.GetTeamName().Length < 4)
                {
                    text.Append(i + ". " + team.GetTeamName() + "\t\t\t\t" + team.rating + System.Environment.NewLine);
                    i++;
                }

                else if (team.GetFBSRank() < 100 && team.GetTeamName().Length < 12)
                {
                    text.Append(i + ". " + team.GetTeamName() + "\t\t\t" + team.rating + System.Environment.NewLine);
                    i++;
                }

                else if (team.GetFBSRank() < 100 && team.GetTeamName().Length < 20)
                {
                    text.Append(i + ". " + team.GetTeamName() + "\t\t" + team.rating + System.Environment.NewLine);
                    i++;
                }

                else if (team.GetFBSRank() < 10 && team.GetTeamName().Length < 13)
                {
                    text.Append(i + ". " + team.GetTeamName() + "\t\t\t" + team.rating + System.Environment.NewLine);
                    i++;
                }

                else if (team.GetFBSRank() < 100 && team.GetTeamName().Length < 12)
                {
                    text.Append(i + ". " + team.GetTeamName() + "\t\t\t" + team.rating + System.Environment.NewLine);
                    i++;
                }

                else if (team.GetFBSRank() > 99 && team.GetTeamName().Length < 11)
                {
                    text.Append(i + ". " + team.GetTeamName() + "\t\t\t" + team.rating + System.Environment.NewLine);
                    i++;
                }

                else if (team.GetFBSRank() > 99 && team.GetTeamName().Length < 20)
                {
                    text.Append(i + ". " + team.GetTeamName() + "\t\t" + team.rating + System.Environment.NewLine);
                    i++;
                }

                else
                {
                    text.Append(i + ". " + team.GetTeamName() + "\t" + team.rating + System.Environment.NewLine);
                    i++;
                }
            }

            using (StreamWriter sw = new StreamWriter(scoresPath + "Rankings" + year + ".txt"))
            {
                sw.Write(text);
            }

            StreamReader sr = new StreamReader(auth);
            string password = sr.ReadLine();
            sr.Close();
            password = password.ToLower();
            password = password.Remove(10, 3);
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            mail.From = new MailAddress("dmiller22kc@gmail.com");
            mail.To.Add("dmiller22kc@gmail.com");
            mail.Subject = "Rankings";
            mail.Body = "This weeks CFB rankings";

            Attachment txtfile, xlfile;
            txtfile = new Attachment(scoresPath + "Rankings" + year + ".txt");
            xlfile = new Attachment(excelPath + "Rankings.xlsx");
            mail.Attachments.Add(txtfile);
            mail.Attachments.Add(xlfile);

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("dmiller22kc@gmail.com", password),
                Timeout = 2000000,
            };
            smtp.Send(mail);
            txtfile.Dispose();
            xlfile.Dispose();
        }

        /// <summary>
        /// Uploads the file at the path passed in to Google Drive
        /// </summary>
        /// <param name="uploadFile">the path of the file to upload</param>
        /// <returns>true if the upload is successful, false otherwise</returns>
        public static bool UploadToDrive(string uploadFile)
        {
            string fileName = uploadFile.Split('.')[0];
            string[] dirs = fileName.Split('/');
            fileName = dirs[dirs.Length - 1];

            if (File.Exists(uploadFile))
            {
                UserCredential credential;

                using (var stream = new FileStream("C:/Users/resources/client_secret.json", FileMode.Open, FileAccess.Read))
                {
                    string credPath = System.Environment.GetFolderPath(
                        System.Environment.SpecialFolder.Personal);
                    credPath = Path.Combine(credPath, ".credentials/drive-dotnet-quickstart.json");

                    credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                        GoogleClientSecrets.Load(stream).Secrets,
                        scopes,
                        "user",
                        CancellationToken.None,
                        new FileDataStore(credPath, true)).Result;
                }

                var service = new DriveService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = "App Name",
                });

                string folderId;
                if (fileName == "Rankings")
                {
                    folderId = "0B6PEje_okeyZenFIRjM1VEZiejQ";
                }
                else
                {
                    folderId = "0B6PEje_okeyZbGFlVTU4YjJNMkU";
                }
                Google.Apis.Drive.v3.Data.File driveRanking = new Google.Apis.Drive.v3.Data.File();
                driveRanking.Name = fileName + "(" + DateTime.Today.ToString("d") + ")";
                driveRanking.MimeType = "application/vnd.google-apps.spreadsheet";
                driveRanking.Parents = new List<string> { folderId };
                FilesResource.CreateMediaUpload request;
                try
                {
                    using (var stream = new FileStream(uploadFile, FileMode.Open))
                    {
                        request = service.Files.Create(driveRanking, stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                        request.Fields = "id";
                        request.Upload();
                    }
                }

                catch(Exception e)
                {
                    Console.WriteLine("Error: " + e.Message);
                    return false;
                }

                return true;
            }

            else
            {
                Console.WriteLine("File does not exist: " + uploadFile);
                return false;
            }
        }
    }
}
