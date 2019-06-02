using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Net;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NationalSavingsBondsSearch
{
    public partial class Form1 : Form
    {
        private string[] arrURLs = new string[9] {
                                                    "http://savings.gov.pk/rs-40000-premium-bonds-draws/",
                                                    "http://savings.gov.pk/rs-40000-draws/",
                                                    "http://savings.gov.pk/rs-25000-draws/",
                                                    "http://savings.gov.pk/rs-15000-draws/",
                                                    "http://savings.gov.pk/rs-7500-draws/",
                                                    "http://savings.gov.pk/rs-1500-draws/",
                                                    "http://savings.gov.pk/rs-750-draws/",
                                                    "http://savings.gov.pk/rs-200-draws/",
                                                    "http://savings.gov.pk/rs-100-draws/"
                                                };
        private List<cBond> MyBonds = new List<cBond>();
        private bool blnDevMode = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            datCheckDrawsAfter.Value = new DateTime(DateTime.Now.AddMonths(-3).Year, DateTime.Now.AddMonths(-3).Month, 1);

            AddToLog("==========================================================================================================================================", "ApplicationStartSeperator");
            AddToLog("Application started");

            if (!Directory.Exists(Application.StartupPath + "\\MyBonds"))
            {
                Directory.CreateDirectory(Application.StartupPath + "\\MyBonds");
                File.Create(Application.StartupPath + "\\MyBonds\\" + "40000PREMIUM.txt");
                File.Create(Application.StartupPath + "\\MyBonds\\" + "40000.txt");
                File.Create(Application.StartupPath + "\\MyBonds\\" + "25000.txt");
                File.Create(Application.StartupPath + "\\MyBonds\\" + "15000.txt");
                File.Create(Application.StartupPath + "\\MyBonds\\" + "7500.txt");
                File.Create(Application.StartupPath + "\\MyBonds\\" + "1500.txt");
                File.Create(Application.StartupPath + "\\MyBonds\\" + "750.txt");
                File.Create(Application.StartupPath + "\\MyBonds\\" + "200.txt");
                File.Create(Application.StartupPath + "\\MyBonds\\" + "100.txt");

                AddToLog("Successfully created MyBonds folder and files.");
            }

            ReadMyBondsFile("40000PREMIUM", "40000");
            ReadMyBondsFile("40000", "40000");
            ReadMyBondsFile("25000", "25000");
            ReadMyBondsFile("15000", "15000");
            ReadMyBondsFile("7500", "7500");
            ReadMyBondsFile("1500", "1500");
            ReadMyBondsFile("750", "750");
            ReadMyBondsFile("200", "200");
            ReadMyBondsFile("100", "100");

            lnkMyBonds.Text = "My Bonds (" + MyBonds.Count.ToString() + ")";
        }

        private void ReadMyBondsFile(string strDenomination, string strDenominationValue)
        {
            string[] arrBonds = null;
            string[] arrDelimiters = new string[4] { Environment.NewLine, " ", ";", "," };
            string strFileName = "";
            int intAddedCount = 0;

            strFileName = Application.StartupPath + "\\MyBonds\\" + strDenomination + ".txt";
            if (File.Exists(strFileName))
            {
                arrBonds = GetFileContents(strFileName).Split(arrDelimiters, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < arrBonds.Length; i++)
                {
                    if (arrBonds[i].IndexOf("-") > 0) //bond range encountered
                    {
                        string[] arrBondRange = arrBonds[i].Split(new char[1] { '-' }, StringSplitOptions.None);
                        int intBondRangeStart = -1;
                        int intBondRangeEnd = -1;
                        if (arrBondRange.Length == 2)
                        {
                            intBondRangeStart = Convert.ToInt32(arrBondRange[0]);
                            intBondRangeEnd = Convert.ToInt32(arrBondRange[1]);
                            if (intBondRangeStart > 0 && intBondRangeEnd > 0)
                            {
                                for (int br = intBondRangeStart; br <= intBondRangeEnd; br++)
                                {
                                    MyBonds.Add(new cBond(br.ToString(), strDenomination));
                                    intAddedCount++;
                                }
                            }
                        }
                    }
                    else //normal bond number
                    {
                        MyBonds.Add(new cBond(arrBonds[i], strDenomination));
                        intAddedCount++;
                    }
                }
            }

            AddToLog("Added " + intAddedCount  + " bonds for " + strDenomination + ".");
        }

        private string GetFileContents(string strFilePath)
        {
            string strReturn = "";
            try
            {
                StreamReader rdrFile = new StreamReader(strFilePath);
                strReturn = rdrFile.ReadToEnd();
                rdrFile.Close();
            }
            catch (Exception ex1)
            {
            }
            return strReturn;
        }

        private void DoDownload()
        {
            lblStatus.Text = "Checking and downloading latest draw results...";
            AddToLog(lblStatus.Text);
            Application.DoEvents();

            WebClient webClient = new WebClient();
            string strFileBody = "";
            string strLink = "";
            string strLinkFileName = "";
            string strDrawListFileName = "";
            string[] arrDrawsListsFiles = null;
            string[] arrDrawFiles = null;
            string strDrawLinkName = "";

            //Download ListPages
            for (int i = 0; i < arrURLs.Length; i++)
            {
                int i1 = arrURLs[i].IndexOf("/rs");
                string strName = arrURLs[i].Substring(i1 + 1, arrURLs[i].Length - i1 - 2);

                if (!Directory.Exists(Application.StartupPath + "\\DrawResults"))
                {
                    Directory.CreateDirectory(Application.StartupPath + "\\DrawResults");
                }
                if (!blnDevMode)
                {
                    lblStatus.Text = "Downloading draw result list files for " + strName.Replace("-", " ") + "...";
                    AddToLog(lblStatus.Text);
                    Application.DoEvents();

                    webClient.DownloadFile(arrURLs[i], Application.StartupPath + "\\DrawResults\\" + strName + ".html");
                }
            }

            //Download Pages under each ListPage
            arrDrawsListsFiles = Directory.GetFiles(Application.StartupPath + "\\DrawResults\\", "*.html");
            List<string> LinksForUniqueChecking = new List<string>();
            for (int i = 0; i < arrDrawsListsFiles.Length; i++)
            {
                int i3 = arrDrawsListsFiles[i].LastIndexOf("\\");
                strDrawListFileName = arrDrawsListsFiles[i].Substring(i3 + 1, arrDrawsListsFiles[i].Length - i3 - 1);
                strDrawListFileName = strDrawListFileName.ToUpper().Replace("-DRAWS.HTML", "").Replace("RS-", "").Replace("-PREMIUM-BONDS", "PREMIUM");

                strFileBody = GetFileContents(arrDrawsListsFiles[i]);

                Regex expression = new Regex(@"http\:\/\/savings\.gov\.pk\/wp\-content\/uploads\/.*?\.txt");
                var results = expression.Matches(strFileBody);
                AddToLog("Found " + results.Count.ToString() + " draw results for " + strDrawListFileName);
                foreach (Match match in results)
                {
                    int i1 = match.Value.IndexOf(".txt");
                    strLink = match.Value.Substring(0, i1 + 4);

                    int i4 = strFileBody.IndexOf(strLink);
                    if (i4>=0)
                    {
                        int i5 = strFileBody.IndexOf(">", i4);
                        if (i5>=0)
                        {
                            int i6 = strFileBody.IndexOf("<", i5);
                            strDrawLinkName = strFileBody.Substring(i5 + 1, i6 - i5 - 1).Trim() + ".txt";
                        }
                    }

                    
                    bool blnUniqueViolated = false;
                    for (int u = 0; u < LinksForUniqueChecking.Count; u++)
                    {
                        if (LinksForUniqueChecking[u] == strLink)
                        {
                            blnUniqueViolated = true;
                            MessageBox.Show("Multiple draw results on the National Bonds website are showing the same draw result file " + strDrawLinkName.Replace(".txt","") + " for denomination " + strDrawListFileName + ". The application will continue but due to this error on the website, the search will not include this draw result.",
                                            "Error found on National Bonds website",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Warning
                                            );
                        }
                    }
                    if (!blnUniqueViolated)
                    {
                        LinksForUniqueChecking.Add(strLink);
                    }
                    

                    int i2 = strLink.LastIndexOf("/");
                    strLinkFileName = "RESULT" + strDrawListFileName + "_" + strDrawLinkName;
                    if (!File.Exists(Application.StartupPath + "\\DrawResults\\" + strLinkFileName))
                    {
                        try
                        {
                            if (!blnDevMode)
                            {
                                webClient.DownloadFile(strLink, Application.StartupPath + "\\DrawResults\\" + strLinkFileName);
                                AddToLog("Successfully downloaded draw result file: " + strLinkFileName);
                            }
                        }
                        catch (Exception ex1)
                        {
                            AddToLog("Failed to download draw result file: " + strLinkFileName + ": " + ex1.Message);
                        }
                    }
                    else
                    {
                        AddToLog("Download ignored (already exists) draw result file: " + strLinkFileName);
                    }
                }
            }
        }

        private void DoCheckMyBonds()
        {
            string strFileBody = "";
            string[] arrDrawFiles = null;
            string[] arrFileBodyTokens = null;
            string strDrawFileNameOriginal = "";
            string strDrawFileDenomination = "";
            string strDrawDate = "";
            string strDrawNo = "";
            int intPrizeAmount = 0;
            DateTime datDrawDate = new DateTime(1900, 1, 1);
            datCheckDrawsAfter.Enabled = false;
            lnkApplyDate.Enabled = false;
            lnkViewResult.Visible = false;
            grdResults.Rows.Clear();
            DateTime datOutput = DateTime.Now;
            string strHTML = "";
            string strHTMLLog = "";
            List<cDrawResultFindOperation> DrawFilesChecked = new List<cDrawResultFindOperation>();

            lblStatus.ForeColor = Color.Black;
            lblStatus.Text = "Checking your " + MyBonds.Count.ToString() + " bonds in draws held on or after " + datCheckDrawsAfter.Value.ToString("dd-MMM-yyyy") + "...";
            AddToLog(lblStatus.Text);
            Application.DoEvents();

            //Find MyBonds under Each File
            arrDrawFiles = Directory.GetFiles(Application.StartupPath + "\\DrawResults\\", "RESULT*.txt");
            strFileBody = "";
            for (int m = 0; m < MyBonds.Count; m++)
            {
                for (int i = 0; i < arrDrawFiles.Length; i++)
                {
                    int i2 = arrDrawFiles[i].LastIndexOf("\\");
                    strDrawFileNameOriginal = arrDrawFiles[i].Substring(i2 + 1, arrDrawFiles[i].Length - i2 - 1);

                    int i3 = strDrawFileNameOriginal.IndexOf("_");
                    strDrawFileDenomination = strDrawFileNameOriginal.Substring(0, i3).Replace("RESULT", "");


                    if (MyBonds[m].Denomination == strDrawFileDenomination)
                    {
                        strDrawDate = strDrawFileNameOriginal.Replace("RESULT" + MyBonds[m].Denomination + "_", "").Replace(".txt", "");

                        try
                        {
                            string[] arrDrawDateTokens = strDrawDate.Split(new string[2] { "-", "_" }, StringSplitOptions.None);
                            if (arrDrawDateTokens.Length == 3)
                            {
                                datDrawDate = new DateTime( Convert.ToInt32(arrDrawDateTokens[2]), Convert.ToInt32(arrDrawDateTokens[1]), Convert.ToInt32(arrDrawDateTokens[0]));
                            }
                        }
                        catch (Exception ex2)
                        {
                            datDrawDate = new DateTime(1900, 1, 1);
                        }

                        if (datDrawDate >= datCheckDrawsAfter.Value)
                        {

                            lblStatus.Text = "Checking " + MyBonds[m].BondNumber + " (Rs. " + MyBonds[m].Denomination + ") in " + strDrawDate;

                            bool blnAlreadyAddedToHTMLLog = false;
                            int intDrawFilesCheckedIndex = -1;
                            for (int l=0;l< DrawFilesChecked.Count;l++)
                            {
                                if (DrawFilesChecked[l].File == arrDrawFiles[i])
                                {
                                    blnAlreadyAddedToHTMLLog = true;
                                    intDrawFilesCheckedIndex = l;
                                    break;
                                }
                            }
                            if (blnAlreadyAddedToHTMLLog == false)
                            {
                                DrawFilesChecked.Add(new cDrawResultFindOperation(datDrawDate.ToString("dd-MMM-yyyy") + " (Rs. " + MyBonds[m].Denomination + ")", arrDrawFiles[i], 0, datDrawDate));
                                intDrawFilesCheckedIndex = DrawFilesChecked.Count - 1;
                            }                            
                            Application.DoEvents();

                            strFileBody = GetFileContents(arrDrawFiles[i]);
                            arrFileBodyTokens = strFileBody.Split(new string[5] { " ", Environment.NewLine, "\t", "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries);
                            for (int j = 0; j < arrFileBodyTokens.Length; j++)
                            {
                                try
                                {
                                    int i4 = strFileBody.IndexOf("Draw No");
                                    if (i4 >= 0)
                                    {
                                        int i5 = strFileBody.IndexOf("\n", i4);
                                        if (i5 >= 0)
                                        {
                                            strDrawNo = strFileBody.Substring(i4 + 8, i5 - i4 - 8);
                                            strDrawNo = strDrawNo.Replace(".", "").Replace(":", "").ToLower().Replace("st", "").Replace("nd", "").Replace("rd", "").Replace("th", "").Trim();
                                        }
                                    }
                                }
                                catch (Exception ex2)
                                {
                                    strDrawNo = "0";
                                }

                                if (arrFileBodyTokens[j].Trim() == MyBonds[m].BondNumber)
                                {
                                    grdResults.Rows.Add(Convert.ToInt32(MyBonds[m].Denomination), Convert.ToInt32(MyBonds[m].BondNumber), datDrawDate, Convert.ToInt32(strDrawNo));
                                    grdResults.Rows[grdResults.Rows.Count - 1].Tag = strDrawFileNameOriginal;

                                    DrawFilesChecked[intDrawFilesCheckedIndex].BondsFound++;
                                }
                            }
                        }

                    }
                }
            }

            if (grdResults.Rows.Count > 0)
            {
                lblStatus.Text = "Your bonds were found in " + grdResults.Rows.Count + " draws.";
                AddToLog(lblStatus.Text);
                lblStatus.ForeColor = Color.Green;

                grdResults.Sort(colDrawDate, ListSortDirection.Descending);
            }
            else
            {
                lblStatus.Text = "Sorry, you bonds were not found in draw.";
                AddToLog(lblStatus.Text);
                lblStatus.ForeColor = Color.Red;
            }

            datCheckDrawsAfter.Enabled = true;
            lnkApplyDate.Enabled = true;

            strHTML += "<html>";
            strHTML += "<body>";
            strHTML += "<h1>National Bonds Results</h1>";
            strHTML += "<h3>Scan was done at " + datOutput.ToString("dd-MMM-yyyy HH:mm tt") + ". " + MyBonds.Count.ToString() + " bonds were searched in all draws held on or after " + datCheckDrawsAfter.Value.ToString("dd-MMM-yyyy") + ".</h3>";
            if (grdResults.Rows.Count == 0)
            {
                strHTML += "<h3 style='color:red;'>Sorry! None of your bonds were found in these draw results</h3>";
            }
            else if (grdResults.Rows.Count == 1)
            {
                strHTML += "<h3 style='color:green;'>" + grdResults.Rows.Count.ToString() + " of your bonds was found in these draw results</h3>";
            }
            else
            {
                strHTML += "<h3 style='color:green;'>" + grdResults.Rows.Count.ToString() + " of your bonds were found in these draw results</h3>";
            }

            strHTML += "<table border='1'>";
            strHTML += "<thead>";
            strHTML += "<tr>";
            for (int c = 0; c < grdResults.Columns.Count; c++)
            {
                strHTML += "<th>" + grdResults.Columns[c].HeaderText + "</th>";
            }
            strHTML += "</tr>";
            strHTML += "</thead>";
            strHTML += "<tbody>";
            for (int r = 0; r < grdResults.Rows.Count; r++)
            {
                strHTML += "<tr>";
                for (int c = 0; c < grdResults.Columns.Count; c++)
                {
                    if (grdResults.Rows[r].Cells[c].Value != null)
                    {
                        if (c==2)
                        {
                            DateTime datTemp = (DateTime)grdResults.Rows[r].Cells[c].Value;
                            strHTML += "<td>" + datTemp.ToString("dd-MMM-yyyy") + "</td>";
                        }
                        else
                        {
                            strHTML += "<td>" + grdResults.Rows[r].Cells[c].Value.ToString() + "</td>";
                        }
                    }
                    else
                    {
                        strHTML += "<td>&nbsp;</td>";
                    }
                }
                strHTML += "</tr>";
            }
            strHTML += "</tbody>";
            strHTML += "</table>";

            strHTML += "<hr>";
            strHTML += "<p>";
            strHTML += "The following draw files were searched:<br>";
            strHTML += "<small>";
            foreach (var x in DrawFilesChecked.OrderByDescending(x => x.DrawDate))
            {
                strHTML += "&nbsp;&nbsp;&nbsp;<a href='" + x.File + "'>" + x.Title + "</a>";

                if (x.BondsFound == 1)
                {
                    strHTML += "&nbsp;&nbsp;&nbsp;<font color='green'>" + x.BondsFound.ToString() + " Bond Won</font>";
                }
                else if (x.BondsFound > 1)
                {
                    strHTML += "&nbsp;&nbsp;&nbsp;<font color='green'>" + x.BondsFound.ToString() + " Bonds Won</font>";
                }
                else
                {
                    strHTML += "";
                }
                strHTML += "<br>";
            }
            for (int fc=0;fc<DrawFilesChecked.Count;fc++)
            {

            }
            strHTML += "</small>";
            strHTML += "</p>";

            strHTML += "</body>";
            strHTML += "</html>";

            if (!Directory.Exists(Application.StartupPath + "\\Outputs")) { Directory.CreateDirectory(Application.StartupPath + "\\Outputs"); }

            StreamWriter wrtOutput = new StreamWriter(Application.StartupPath + "\\Outputs\\" + datOutput.ToString("dd-MMM-yyyy HH-mm tt") + ".html", false);
            wrtOutput.Write(strHTML);
            wrtOutput.Close();

            lnkViewResult.Tag = Application.StartupPath + "\\Outputs\\" + datOutput.ToString("dd-MMM-yyyy HH-mm tt") + ".html";
            lnkViewResult.Visible = true;

            Application.DoEvents();
        }
        
        private string ParseValue(string strInput, string strValueType)
        {
            string strReturn = "";

            try
            {
                if (strValueType == "FirstPrizeAmount")
                {
                    int i1 = strInput.ToLower().IndexOf("first prize");
                    if (i1 < 0)
                    {
                        i1 = strInput.IndexOf("1st prize");
                    }

                    if (i1 >= 0)
                    {
                        int i2 = strInput.IndexOf("Rs", i1);
                        if (i2 >= 0)
                        {
                            int i3 = strInput.ToLower().IndexOf("\n", i2);
                            if (i3 >= 0)
                            {
                                string strAmount = strInput.Substring(i2 + 2, i3 - i2 - 2);
                                strAmount = strAmount.Replace(".", "").Replace(",", "").Replace("/", "").Replace("-", "");

                                strReturn = strAmount;
                            }
                        }
                    }
                }
            }
            catch (Exception ex1)
            {
            }

            return strReturn;
        }

        private void grdResults_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if ( e.RowIndex >= 0 )
            {
                Process prcOpen = new Process();
                prcOpen.StartInfo.FileName = Application.StartupPath + "\\DrawResults\\" + grdResults.Rows[e.RowIndex].Tag.ToString();
                prcOpen.Start();
            }
        }

        private void AddToLog(string strText, string strTextType = "")
        {
            try
            {
                StreamWriter wrtLog = new StreamWriter(Application.StartupPath + "\\Log.txt", true);
                if (strTextType == "")
                {
                    wrtLog.WriteLine(DateTime.Now.ToString("dd-MMM-yyyy HH:mm:ss tt") + " - " + strText);
                }
                else if (strTextType == "ApplicationStartSeperator")
                {
                    wrtLog.WriteLine(strText);
                }
                wrtLog.Close();
            }
            catch (Exception ex)
            {

            }
        }



        private void grdResults_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            DoDownload();
            DoCheckMyBonds();
            
        }

        private void lnkMyBonds_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process prcOpen = new Process();
            prcOpen.StartInfo.FileName = Application.StartupPath + "\\MyBonds";
            prcOpen.Start();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            AddToLog("Application closed by user");
        }

        private void lnkApplyDate_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DoCheckMyBonds();
        }

        private void lnkViewResult_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process prcOpen = new Process();
            prcOpen.StartInfo.FileName = lnkViewResult.Tag.ToString();
            prcOpen.Start();
        }
    }

    public class cBond
    {
        public string BondNumber { get; set; }
        public string Denomination { get; set; }

        public cBond (string strBondNumber, string strDenomination)
        {
            BondNumber = strBondNumber.Trim();
            Denomination = strDenomination.Trim();
        }
    }

    public class cDrawResultFindOperation
    {
        public string Title { get; set; }
        public string File { get; set; }
        public int BondsFound { get; set; }
        public DateTime DrawDate { get; set; }

        public cDrawResultFindOperation(string strTitle, string strFile, int intBondsFound, DateTime datDrawDate)
        {
            Title = strTitle;
            File = strFile;
            BondsFound = intBondsFound;
            DrawDate = datDrawDate;
        }
    }
}
