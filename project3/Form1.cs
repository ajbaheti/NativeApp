using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace project3
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            homeAndAbout();
            graduate();
            undergraduate();
            minors();
            employment();
            employmentHistory();
            people();
            researchByInterest();
            researchByFaculty();
            resources();
            news();
            footer();

            mapBrowser.Url = new System.Uri("http://www.ist.rit.edu/api/map");
            contactFormBrowser.Url = new System.Uri("http://www.ist.rit.edu/api/contactForm");
        }

        public string getRestData(string url)
        {
            string baseUri = "http://ist.rit.edu/api";

            // connect to the API
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(baseUri + url);
            try
            {
                WebResponse response = request.GetResponse();

                using (Stream responseStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                    return reader.ReadToEnd();
                }
            }
            catch (WebException we)
            {
                // Something goes wrong, get the error response, then do something with it
                WebResponse err = we.Response;
                using (Stream responseStream = err.GetResponseStream())
                {
                    StreamReader r = new StreamReader(responseStream, Encoding.UTF8);
                    string errorText = r.ReadToEnd();
                    // display or log error
                    Console.WriteLine(errorText);
                }
                throw;
            }
        }

        //function to display ABOUT section
        private void homeAndAbout()
        {
            // get About information
            string jsonAbout = getRestData("/about/");
            // need a way to get the JSON form into an About object
            About about = JToken.Parse(jsonAbout).ToObject<About>();

            // start displaying the About object information on the screen
            richTextBox1.SelectionAlignment = HorizontalAlignment.Center;
            richTextBox1.Text = Environment.NewLine + about.description;
            aboutTitleLabel.Text = about.title;
            aboutQuoteLabel.Text = about.quote + Environment.NewLine + Environment.NewLine + "-" + about.quoteAuthor;
        }

        //function to display GRADUATE section
        private void graduate()
        {
            // get graduate degree information
            string jsonDegrees = getRestData("/degrees/graduate");
            // need a way to get the JSON form into GraduateDegree object
            Degrees degrees = JToken.Parse(jsonDegrees).ToObject<Degrees>();

            string[] grad = new string[4];
            string[] gradTag = new string[3];
            int i = 0;
            gradCertiText.Text = "";

            //display info about individual degree
            foreach (Graduate gd in degrees.graduate)
            {
                if (gd.title != null)
                {
                    grad[i] = gd.title;
                    gradTag[i] = gd.degreeName;

                    if (gd.degreeName.Equals("ist"))
                    {
                        gradTitleLabel.Text = gd.title;
                        gradDescTb.SelectionAlignment = HorizontalAlignment.Center;
                        gradDescTb.Text = Environment.NewLine + gd.description;

                        gradInfoList.Items.Clear();
                        gradInfoList.Items.Add(Environment.NewLine);
                        for (int j = 0; j < gd.concentrations.Count; j++)
                        {
                            gradInfoList.Items.Add(" + "+gd.concentrations[j]);
                            gradInfoList.Items.Add(Environment.NewLine);
                        }
                    }
                }
                else
                {
                    //graduate advance degree certificate info
                    for(int j=0; j<gd.availableCertificates.Count; j++)
                    {
                        gradCertiText.Text += Environment.NewLine;
                        gradCertiText.Text += gd.availableCertificates[j] + Environment.NewLine;
                    }                        
                }         
                i++;
            }

            gradDegreeBtn1.Text = grad[0];
            gradDegreeBtn2.Text = grad[1];
            gradDegreeBtn3.Text = grad[2];
            gradDegreeBtn1.Tag = gradTag[0];
            gradDegreeBtn2.Tag = gradTag[1];
            gradDegreeBtn3.Tag = gradTag[2];            
        }

        //function to display UNDERGRADUATE section
        private void undergraduate()
        {
            // get undergraduate degree information
            string jsonDegrees = getRestData("/degrees/undergraduate");
            // need a way to get the JSON form into GraduateDegree object
            Degrees degrees = JToken.Parse(jsonDegrees).ToObject<Degrees>();

            string[] uGrad = new string[3];
            string[] uGradTag = new string[3];
            int i = 0;

            //individual undergrad degree
            foreach (Undergraduate ugd in degrees.undergraduate)
            {
                uGrad[i] = ugd.title;
                uGradTag[i] = ugd.degreeName;

                if (ugd.degreeName == "wmc")
                {
                    undergradTitleLabel.Text = ugd.title;
                    ugRichText.Text = Environment.NewLine + ugd.description;

                    ugListbox.Items.Clear();
                    ugListbox.Items.Add(Environment.NewLine);
                    for (int j = 0; j < ugd.concentrations.Count; j++)
                    {
                        ugListbox.Items.Add(" + " + ugd.concentrations[j]);
                        ugListbox.Items.Add(Environment.NewLine);
                    }
                }

                i++;
            }

            ugBtn1.Text = uGrad[0];
            ugBtn2.Text = uGrad[1];
            ugBtn3.Text = uGrad[2];
            ugBtn1.Tag = uGradTag[0];
            ugBtn2.Tag = uGradTag[1];
            ugBtn3.Tag = uGradTag[2];
        }

        private void employmentHistory()
        {
            // get employment table information
            string profEmptable = getRestData("/employment/employmentTable");
            // need a way to get the JSON form into employment table object
            Employment profEmpTable = JToken.Parse(profEmptable).ToObject<Employment>();

            employmentTableTitle.Text = profEmpTable.employmentTable.title;

            for (int i=0; i< profEmpTable.employmentTable.professionalEmploymentInformation.Count; i++)
            {
                employmentTable.Rows.Add();
                employmentTable.Rows[i].Cells[0].Value = profEmpTable.employmentTable.professionalEmploymentInformation[i].degree;
                employmentTable.Rows[i].Cells[1].Value = profEmpTable.employmentTable.professionalEmploymentInformation[i].employer;
                employmentTable.Rows[i].Cells[2].Value = profEmpTable.employmentTable.professionalEmploymentInformation[i].city;
                employmentTable.Rows[i].Cells[3].Value = profEmpTable.employmentTable.professionalEmploymentInformation[i].title;
                employmentTable.Rows[i].Cells[4].Value = profEmpTable.employmentTable.professionalEmploymentInformation[i].startDate;
            }

            // get coop table information
            string coopData = getRestData("/employment/coopTable");
            // need a way to get the JSON form into coop table object
            Employment coopTable = JToken.Parse(coopData).ToObject<Employment>();

            coopTableTitleLabel.Text = coopTable.coopTable.title;

            for (int i = 0; i < coopTable.coopTable.coopInformation.Count; i++)
            {
                coop_Table.Rows.Add();
                coop_Table.Rows[i].Cells[0].Value = coopTable.coopTable.coopInformation[i].degree;
                coop_Table.Rows[i].Cells[1].Value = coopTable.coopTable.coopInformation[i].employer;
                coop_Table.Rows[i].Cells[2].Value = coopTable.coopTable.coopInformation[i].city;
                coop_Table.Rows[i].Cells[3].Value = coopTable.coopTable.coopInformation[i].term;
            }
        }

        //function to display MINOR information
        private void minors()
        {
            // get UG minors information
            string minorsJson = getRestData("/minors");
            // need a way to get the JSON form into minors object
            MinorsRoot minors = JToken.Parse(minorsJson).ToObject<MinorsRoot>();

            string[] minorText = new string[8];
            string[] minorTag = new string[8];
            int i = 0;

            //all individual minor info
            foreach (Minors mr in minors.UgMinors)
            {
                minorTag[i] = mr.name;
                minorText[i] = mr.title;

                //show information about WEBDD-MN by default
                if (mr.name.Equals("WEBDD-MN"))
                {
                    minorTitleLabel.Text = mr.title;
                    minorDesc.Text = mr.description;
                    minorNote.Text = "NOTE: " + mr.note;

                    courseListbox.Items.Clear();
                    for (int j = 0; j < mr.courses.Count; j++)
                    {
                        courseListbox.Items.Add("-> " + mr.courses[j]);
                        courseListbox.Items.Add(Environment.NewLine);
                    }

                    //show ISTE-230 course info by default
                    string crsJson = getRestData("/course/courseID=ISTE-105");
                    Course course = JToken.Parse(crsJson).ToObject<Course>();
                    coursenameTitle.Text = course.title;
                    courseDescTb.Text = course.description;
                }
                i++;
            }

            minorBtn1.Text = minorText[0];minorBtn1.Tag = minorTag[0];
            minorBtn2.Text = minorText[1];minorBtn2.Tag = minorTag[1];
            minorBtn3.Text = minorText[2];minorBtn3.Tag = minorTag[2];
            minorBtn4.Text = minorText[3];minorBtn4.Tag = minorTag[3];
            minorBtn5.Text = minorText[4];minorBtn5.Tag = minorTag[4];
            minorBtn6.Text = minorText[5];minorBtn6.Tag = minorTag[5];
            minorBtn7.Text = minorText[6];minorBtn7.Tag = minorTag[6];
            minorBtn8.Text = minorText[7];minorBtn8.Tag = minorTag[7];
        }

        private void minorBtnClick(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;

            if (clickedButton == null) // just to be on the safe side
                return;

            // get UG minor information
            string minorJson = getRestData("/minors/UgMinors/name=" + clickedButton.Tag);
            // need a way to get the JSON form into minor object
            Minors minor = JToken.Parse(minorJson).ToObject<Minors>();

            //display minor content in textbox
            minorTitleLabel.Text = minor.title;
            minorDesc.Text = minor.description;
            if (!minor.note.Equals(""))
                minorNote.Text = "NOTE: " + minor.note;
            else
                minorNote.Text = "";

            coursenameTitle.Text = "";
            courseDescTb.Text = "";

            courseListbox.Items.Clear();
            for (int i = 0; i < minor.courses.Count; i++)
            {
                courseListbox.Items.Add("-> " + minor.courses[i]);
                courseListbox.Items.Add(Environment.NewLine);
            }
        }

        private void ugBtnClick(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;

            if (clickedButton == null) // just to be on the safe side
                return;

            string degree = getRestData("/degrees/undergraduate/degreeName="+clickedButton.Tag);
            Undergraduate degDtl = JToken.Parse(degree).ToObject<Undergraduate>();

            undergradTitleLabel.Text = degDtl.title;
            ugRichText.Text = Environment.NewLine + degDtl.description;

            ugListbox.Items.Clear();
            ugListbox.Items.Add(Environment.NewLine);
            for (int j = 0; j < degDtl.concentrations.Count; j++)
            {
                ugListbox.Items.Add(" + " + degDtl.concentrations[j]);
                ugListbox.Items.Add(Environment.NewLine);
            }
        }

        private void gradDegreeBtnClick(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;

            if (clickedButton == null) // just to be on the safe side
                return;

            string degree = getRestData("/degrees/graduate/degreeName="+clickedButton.Tag);
            Graduate degDtl = JToken.Parse(degree).ToObject<Graduate>();
            
            gradTitleLabel.Text = degDtl.title;
            gradDescTb.Text = Environment.NewLine + degDtl.description;

            gradInfoList.Items.Clear();
            gradInfoList.Items.Add(Environment.NewLine);
            for (int j = 0; j < degDtl.concentrations.Count; j++)
            {
                gradInfoList.Items.Add(" + " + degDtl.concentrations[j]);
                gradInfoList.Items.Add(Environment.NewLine);
            }
        }

        //function to display EMPLOYMENT information
        private void employment()
        {
            // get employment information
            string empInfo = getRestData("/employment");
            // need a way to get the JSON form into employment object
            Employment emp = JToken.Parse(empInfo).ToObject<Employment>();

            //dispay the title of employment
            employmentTitle.Text = emp.introduction.title;

            //employment title and descripton
            employmentInfoLabel.Text = emp.introduction.content[0].title;
            employmentInfoText.Text = emp.introduction.content[0].description;

            //cooperative education title and description
            coopEduInfoLabel.Text = emp.introduction.content[1].title;
            coopEduInfoText.Text = emp.introduction.content[1].description;

            //degree statistics information
            degreeStatLabel.Text = emp.degreeStatistics.title;
            degreeStat1.Text = emp.degreeStatistics.statistics[0].value + Environment.NewLine + Environment.NewLine + emp.degreeStatistics.statistics[0].description;
            degreeStat2.Text = emp.degreeStatistics.statistics[1].value + Environment.NewLine + Environment.NewLine + emp.degreeStatistics.statistics[1].description;
            degreeStat3.Text = emp.degreeStatistics.statistics[2].value + Environment.NewLine + Environment.NewLine + emp.degreeStatistics.statistics[2].description;
            degreeStat4.Text = emp.degreeStatistics.statistics[3].value + Environment.NewLine + Environment.NewLine + emp.degreeStatistics.statistics[3].description;

            //employers list
            employersTitle.Text = emp.employers.title;
            for (int i=0; i< emp.employers.employerNames.Count-1; i++)
                employersList.Items.Add(emp.employers.employerNames[i]);
        }

        //function to display PEOPLE information
        private void people()
        {
            // get employment information
            string ppls = getRestData("/people");
            // need a way to get the JSON form into employment object
            People ppl = JToken.Parse(ppls).ToObject<People>();

            string[] imgUrl = new string[50];
            string[] imgTag = new string[50];
            int i = 0;

            foreach (Faculty f in ppl.faculty)
            {
                imgUrl[i] = f.imagePath;
                imgTag[i] = f.username;
                i++;
            }

            i = 32;
            foreach (Staff s in ppl.staff)
            {
                imgUrl[i] = s.imagePath;
                imgTag[i] = s.username + "**";               
                i++;
            }
            //faculty images
            pictureBox1.Load(imgUrl[0]); pictureBox1.Tag = imgTag[0];
            pictureBox2.Load(imgUrl[1]); pictureBox2.Tag = imgTag[1];
            pictureBox3.Load(imgUrl[2]); pictureBox3.Tag = imgTag[2];
            pictureBox4.Load(imgUrl[3]); pictureBox4.Tag = imgTag[3];
            pictureBox5.Load(imgUrl[4]); pictureBox5.Tag = imgTag[4];
            pictureBox6.Load(imgUrl[5]); pictureBox6.Tag = imgTag[5];
            pictureBox7.Load(imgUrl[6]); pictureBox7.Tag = imgTag[6];
            pictureBox8.Load(imgUrl[7]); pictureBox8.Tag = imgTag[7];
            pictureBox9.Load(imgUrl[8]); pictureBox9.Tag = imgTag[8];
            pictureBox10.Load(imgUrl[9]); pictureBox10.Tag = imgTag[9];
            pictureBox11.Load(imgUrl[10]); pictureBox11.Tag = imgTag[10];
            pictureBox12.Load(imgUrl[11]); pictureBox12.Tag = imgTag[11];
            pictureBox13.Load(imgUrl[12]); pictureBox13.Tag = imgTag[12];
            pictureBox14.Load(imgUrl[13]); pictureBox14.Tag = imgTag[13];
            pictureBox15.Load(imgUrl[14]); pictureBox15.Tag = imgTag[14];
            pictureBox16.Load(imgUrl[15]); pictureBox16.Tag = imgTag[15];
            pictureBox17.Load(imgUrl[16]); pictureBox17.Tag = imgTag[16];
            pictureBox18.Load(imgUrl[17]); pictureBox18.Tag = imgTag[17];
            pictureBox19.Load(imgUrl[18]); pictureBox19.Tag = imgTag[18];
            pictureBox20.Load(imgUrl[19]); pictureBox20.Tag = imgTag[19];
            pictureBox21.Load(imgUrl[20]); pictureBox21.Tag = imgTag[20];
            pictureBox22.Load(imgUrl[21]); pictureBox22.Tag = imgTag[21];
            pictureBox23.Load(imgUrl[22]); pictureBox23.Tag = imgTag[22];
            pictureBox24.Load(imgUrl[23]); pictureBox24.Tag = imgTag[23];
            pictureBox25.Load(imgUrl[24]); pictureBox25.Tag = imgTag[24];
            pictureBox26.Load(imgUrl[25]); pictureBox26.Tag = imgTag[25];
            pictureBox27.Load(imgUrl[26]); pictureBox27.Tag = imgTag[26];
            pictureBox28.Load(imgUrl[27]); pictureBox28.Tag = imgTag[27];
            pictureBox29.Load(imgUrl[28]); pictureBox29.Tag = imgTag[28];
            pictureBox30.Load(imgUrl[29]); pictureBox30.Tag = imgTag[29];
            pictureBox31.Load(imgUrl[30]); pictureBox31.Tag = imgTag[30];
            pictureBox32.Load(imgUrl[31]); pictureBox32.Tag = imgTag[31];
            //staff images
            pictureBox33.Load(imgUrl[32]); pictureBox33.Tag = imgTag[32];
            pictureBox34.Load(imgUrl[33]); pictureBox34.Tag = imgTag[33];
            pictureBox35.Load(imgUrl[34]); pictureBox35.Tag = imgTag[34];
            pictureBox36.Load(imgUrl[35]); pictureBox36.Tag = imgTag[35];
            pictureBox37.Load(imgUrl[36]); pictureBox37.Tag = imgTag[36];
            pictureBox38.Load(imgUrl[37]); pictureBox38.Tag = imgTag[37];
            pictureBox39.Load(imgUrl[38]); pictureBox39.Tag = imgTag[38];
            pictureBox40.Load(imgUrl[39]); pictureBox40.Tag = imgTag[39];
        }

        //function to display RESEARCH BY INTEREST information
        private void researchByInterest()
        {
            // get research by interest information
            string rsrch = getRestData("/research");
            // need a way to get the JSON form into research object
            Research researchIntrst = JToken.Parse(rsrch).ToObject<Research>();

            string[] areas = new string[12];
            int i = 0;

            foreach (ByInterestArea bi in researchIntrst.byInterestArea) {
                areas[i] = bi.areaName;
                i++;    
            }

            rsrchIntrstBtn1.Text = areas[0];
            rsrchIntrstBtn2.Text = areas[1];
            rsrchIntrstBtn3.Text = areas[2];
            rsrchIntrstBtn4.Text = areas[3];
            rsrchIntrstBtn5.Text = areas[4];
            rsrchIntrstBtn6.Text = areas[5];
            rsrchIntrstBtn7.Text = areas[6];
            rsrchIntrstBtn8.Text = areas[7];
            rsrchIntrstBtn9.Text = areas[8];
            rsrchIntrstBtn10.Text = areas[9];
            rsrchIntrstBtn11.Text = areas[10];
            rsrchIntrstBtn12.Text = areas[11];
        }

        private void rsrchIntrstClick(object sender, EventArgs e)
        {
            Button b = sender as Button;

            if (b == null) // just to be on the safe side
                return;

            ResearchCitations rc = new ResearchCitations(b.Text,"Area");
            rc.ShowDialog();
        }

        //function to display RESEARCH BY FACULTY information
        private void researchByFaculty()
        {
            // get research by faculty information
            string rsrch = getRestData("/research");
            // need a way to get the JSON form into research object
            Research researchFac = JToken.Parse(rsrch).ToObject<Research>();

            string[] faculty = new string[21];
            string[] facultyTag = new string[21];

            for (int i = 0; i < researchFac.byFaculty.Count; i++)
            {
                faculty[i] = researchFac.byFaculty[i].facultyName;
                facultyTag[i] = researchFac.byFaculty[i].username;
            }

            rsrchFacBtn20.Text = faculty[0];rsrchFacBtn20.Tag = facultyTag[0];
            rsrchFacBtn19.Text = faculty[1]; rsrchFacBtn19.Tag = facultyTag[1];
            rsrchFacBtn18.Text = faculty[2]; rsrchFacBtn18.Tag = facultyTag[2];
            rsrchFacBtn17.Text = faculty[3]; rsrchFacBtn17.Tag = facultyTag[3];
            rsrchFacBtn16.Text = faculty[4]; rsrchFacBtn16.Tag = facultyTag[4];
            rsrchFacBtn15.Text = faculty[5]; rsrchFacBtn15.Tag = facultyTag[5];
            rsrchFacBtn14.Text = faculty[6]; rsrchFacBtn14.Tag = facultyTag[6];
            rsrchFacBtn13.Text = faculty[7]; rsrchFacBtn13.Tag = facultyTag[7];
            rsrchFacBtn12.Text = faculty[8]; rsrchFacBtn12.Tag = facultyTag[8];
            rsrchFacBtn11.Text = faculty[9]; rsrchFacBtn11.Tag = facultyTag[9];
            rsrchFacBtn10.Text = faculty[10]; rsrchFacBtn10.Tag = facultyTag[10];
            rsrchFacBtn9.Text = faculty[11]; rsrchFacBtn9.Tag = facultyTag[11];
            rsrchFacBtn8.Text = faculty[12]; rsrchFacBtn8.Tag = facultyTag[12];
            rsrchFacBtn7.Text = faculty[13]; rsrchFacBtn7.Tag = facultyTag[13];
            rsrchFacBtn6.Text = faculty[14]; rsrchFacBtn6.Tag = facultyTag[14];
            rsrchFacBtn5.Text = faculty[15]; rsrchFacBtn5.Tag = facultyTag[15];
            rsrchFacBtn4.Text = faculty[16]; rsrchFacBtn4.Tag = facultyTag[16];
            rsrchFacBtn3.Text = faculty[17]; rsrchFacBtn3.Tag = facultyTag[17];
            rsrchFacBtn2.Text = faculty[18]; rsrchFacBtn2.Tag = facultyTag[18];
            rsrchFacBtn1.Text = faculty[19]; rsrchFacBtn1.Tag = facultyTag[19];
        }

        private void rsrchFacultyClick(object sender, EventArgs e)
        {
            Button b = sender as Button;

            if (b == null) // just to be on the safe side
                return;
            string x = (string) b.Tag;
            ResearchCitations rc = new ResearchCitations(x, "Faculty");
            rc.ShowDialog();
        }

        //function to display RESOURCES information
        private void resources()
        {
            // get co-op enrollment information
            string coops = getRestData("/resources");
            // need a way to get the JSON form into research object
            Resources coop = JToken.Parse(coops).ToObject<Resources>();

            CoopEnrollment ce = new CoopEnrollment();
            ce = coop.coopEnrollment;

            rsrcGroupTitle.Text = ce.title;
            rsrcesLabel2.Text = ce.enrollmentInformationContent[0].title;
            rsrcesLabel3.Text = ce.enrollmentInformationContent[1].title;
            rsrcesLabel4.Text = ce.enrollmentInformationContent[2].title;
            rsrcesRichTextBox2.Text = ce.enrollmentInformationContent[0].description;
            rsrcesRichTextBox3.Text = ce.enrollmentInformationContent[1].description;
            rsrcesRichTextBox4.Text = ce.enrollmentInformationContent[2].description;
        }

        //function to display NEWS information
        private void news()
        {
            string oldnews = getRestData("/news");
            News news = JToken.Parse(oldnews).ToObject<News>();

            newsTb.Text = "";
            foreach (Year yr in news.year)
            {
                newsTb.AppendText(Environment.NewLine + yr.title + " ( " + yr.date + " )" + Environment.NewLine);
                newsTb.AppendText(Environment.NewLine + yr.description + Environment.NewLine);
            }

            foreach (Older old in news.older)
            {
                newsTb.AppendText(Environment.NewLine + old.title + " ( " + old.date + " )" + Environment.NewLine);
                newsTb.AppendText(Environment.NewLine + old.description + Environment.NewLine);
            }            
        }

        //function to display FOOTER
        private void footer()
        {
            gradFooter.SelectionAlignment = HorizontalAlignment.Center;
            gradFooter.AppendText(Environment.NewLine + "Copyright @Rochester Institute of Technology. All rights reserved." + Environment.NewLine);
            gradFooter.AppendText("Copyright Infringement | Privacy Statement | Disclaimer | Nondiscrimination");

            newsFooter.SelectionAlignment = HorizontalAlignment.Center;
            newsFooter.AppendText(Environment.NewLine + "Copyright @Rochester Institute of Technology. All rights reserved." + Environment.NewLine);
            newsFooter.AppendText("Copyright Infringement | Privacy Statement | Disclaimer | Nondiscrimination");

            rsrcsFooter.SelectionAlignment = HorizontalAlignment.Center;
            rsrcsFooter.AppendText(Environment.NewLine + "Copyright @Rochester Institute of Technology. All rights reserved." + Environment.NewLine);
            rsrcsFooter.AppendText("Copyright Infringement | Privacy Statement | Disclaimer | Nondiscrimination");

            rspplFooter.SelectionAlignment = HorizontalAlignment.Center;
            rspplFooter.AppendText(Environment.NewLine + "Copyright @Rochester Institute of Technology. All rights reserved." + Environment.NewLine);
            rspplFooter.AppendText("Copyright Infringement | Privacy Statement | Disclaimer | Nondiscrimination");

            rsrchFooter.SelectionAlignment = HorizontalAlignment.Center;
            rsrchFooter.AppendText(Environment.NewLine + "Copyright @Rochester Institute of Technology. All rights reserved." + Environment.NewLine);
            rsrchFooter.AppendText("Copyright Infringement | Privacy Statement | Disclaimer | Nondiscrimination");

            pplFooter.SelectionAlignment = HorizontalAlignment.Center;
            pplFooter.AppendText(Environment.NewLine + "Copyright @Rochester Institute of Technology. All rights reserved." + Environment.NewLine);
            pplFooter.AppendText("Copyright Infringement | Privacy Statement | Disclaimer | Nondiscrimination");

            empFooter.SelectionAlignment = HorizontalAlignment.Center;
            empFooter.AppendText(Environment.NewLine + "Copyright @Rochester Institute of Technology. All rights reserved." + Environment.NewLine);
            empFooter.AppendText("Copyright Infringement | Privacy Statement | Disclaimer | Nondiscrimination");

            minorFooter.SelectionAlignment = HorizontalAlignment.Center;
            minorFooter.AppendText(Environment.NewLine + "Copyright @Rochester Institute of Technology. All rights reserved." + Environment.NewLine);
            minorFooter.AppendText("Copyright Infringement | Privacy Statement | Disclaimer | Nondiscrimination");

            ugFooter.SelectionAlignment = HorizontalAlignment.Center;
            ugFooter.AppendText(Environment.NewLine + "Copyright @Rochester Institute of Technology. All rights reserved." + Environment.NewLine);
            ugFooter.AppendText("Copyright Infringement | Privacy Statement | Disclaimer | Nondiscrimination");

            abtFooter.SelectionAlignment = HorizontalAlignment.Center;
            abtFooter.AppendText(Environment.NewLine + "Copyright @Rochester Institute of Technology. All rights reserved." + Environment.NewLine);
            abtFooter.AppendText("Copyright Infringement | Privacy Statement | Disclaimer | Nondiscrimination");
        }

        private void coopEnrollClick(object sender, EventArgs e)
        {
            rsrcesLabel2.Visible = true;
            rsrcesLabel3.Visible = true;
            rsrcesLabel4.Visible = true;
            rsrcesRichTextBox3.Visible = true;
            rsrcesRichTextBox4.Visible = true;

            // get co-op enrollment information
            string coops = getRestData("/resources");
            // need a way to get the JSON form into research object
            Resources coop = JToken.Parse(coops).ToObject<Resources>();

            CoopEnrollment ce = new CoopEnrollment();
            ce = coop.coopEnrollment;

            rsrcGroupTitle.Text = ce.title;
            rsrcesLabel2.Text = ce.enrollmentInformationContent[0].title;
            rsrcesLabel3.Text = ce.enrollmentInformationContent[1].title;
            rsrcesLabel4.Text = ce.enrollmentInformationContent[2].title;
            rsrcesRichTextBox2.Text = ce.enrollmentInformationContent[0].description;
            rsrcesRichTextBox3.Text = ce.enrollmentInformationContent[1].description;
            rsrcesRichTextBox4.Text = ce.enrollmentInformationContent[2].description;
        }

        private void stdAmbassadorClick(object sender, EventArgs e)
        {
            rsrcesLabel2.Visible = true;
            rsrcesLabel3.Visible = true;
            rsrcesLabel4.Visible = true;
            rsrcesRichTextBox3.Visible = true;
            rsrcesRichTextBox4.Visible = true;

            // get co-op enrollment information
            string stds = getRestData("/resources");
            // need a way to get the JSON form into research object
            Resources ambas = JToken.Parse(stds).ToObject<Resources>();

            StudentAmbassadors sa = new StudentAmbassadors();
            sa = ambas.studentAmbassadors;

            rsrcGroupTitle.Text = sa.title;
            rsrcesLabel2.Text = sa.subSectionContent[0].title;
            rsrcesLabel3.Text = sa.subSectionContent[1].title;
            rsrcesLabel4.Text = sa.subSectionContent[2].title;
            rsrcesRichTextBox2.Text = sa.subSectionContent[0].description;
            rsrcesRichTextBox3.Text = sa.subSectionContent[1].description;
            rsrcesRichTextBox4.Text = sa.subSectionContent[2].description;
        }

        private void advisingClick(object sender, EventArgs e)
        {
            rsrcesLabel2.Visible = true;
            rsrcesLabel3.Visible = true;
            rsrcesLabel4.Visible = true;
            rsrcesRichTextBox3.Visible = true;
            rsrcesRichTextBox4.Visible = true;

            // get advising information
            string advis = getRestData("/resources");
            // need a way to get the JSON form into research object
            Resources ambas = JToken.Parse(advis).ToObject<Resources>();

            StudentServices ss = new StudentServices();
            ss = ambas.studentServices;

            rsrcGroupTitle.Text = ss.title;
            rsrcesLabel2.Text = ss.academicAdvisors.title;
            rsrcesLabel3.Text = ss.facultyAdvisors.title;
            rsrcesLabel4.Text = ss.professonalAdvisors.title;
            rsrcesRichTextBox2.Text = ss.academicAdvisors.description;
            rsrcesRichTextBox3.Text = ss.facultyAdvisors.description;
            string x = ss.professonalAdvisors.advisorInformation[0].name + "--" + ss.professonalAdvisors.advisorInformation[0].department + "--" + ss.professonalAdvisors.advisorInformation[0].email + Environment.NewLine;
            x += ss.professonalAdvisors.advisorInformation[1].name + "--" + ss.professonalAdvisors.advisorInformation[1].department + "--" + ss.professonalAdvisors.advisorInformation[1].email + Environment.NewLine;
            x += ss.professonalAdvisors.advisorInformation[2].name + "--" + ss.professonalAdvisors.advisorInformation[2].department + "--" + ss.professonalAdvisors.advisorInformation[2].email + Environment.NewLine;
            rsrcesRichTextBox4.Text = x;
        }

        private void studyAbroadClick(object sender, EventArgs e)
        {
            rsrcesLabel2.Visible = true;
            rsrcesLabel3.Visible = true;
            rsrcesLabel4.Visible = true;
            rsrcesRichTextBox3.Visible = true;
            rsrcesRichTextBox4.Visible = true;

            // get advising information
            string abrd = getRestData("/resources");
            // need a way to get the JSON form into research object
            Resources abroad = JToken.Parse(abrd).ToObject<Resources>();

            StudyAbroad sa = new StudyAbroad();
            sa = abroad.studyAbroad;

            rsrcGroupTitle.Text = sa.title;
            rsrcesLabel2.Text = sa.title;
            rsrcesLabel3.Text = sa.places[0].nameOfPlace;
            rsrcesLabel4.Text = sa.places[1].nameOfPlace;
            rsrcesRichTextBox2.Text = sa.description;
            rsrcesRichTextBox3.Text = sa.places[0].description;
            rsrcesRichTextBox4.Text = sa.places[0].description;
        }

        private void tutorClick(object sender, EventArgs e)
        {
            string abrd = getRestData("/resources");
            // need a way to get the JSON form into research object
            Resources abroad = JToken.Parse(abrd).ToObject<Resources>();

            TutorsAndLabInformation tl = new TutorsAndLabInformation();
            tl = abroad.tutorsAndLabInformation;

            rsrcGroupTitle.Text = tl.title;
            rsrcesLabel2.Visible = false;
            rsrcesLabel3.Visible = false;
            rsrcesLabel4.Visible = false;
            rsrcesRichTextBox2.Text = tl.description;
            rsrcesRichTextBox3.Visible = false;
            rsrcesRichTextBox4.Visible = false;
        }

        private void formsClick(object sender, EventArgs e)
        {
            rsrcesLabel2.Visible = true;
            rsrcesLabel3.Visible = true;
            rsrcesLabel4.Visible = true;
            rsrcesRichTextBox3.Visible = true;
            rsrcesRichTextBox4.Visible = true;

            string form = getRestData("/resources");
            // need a way to get the JSON form into research object
            Resources forms = JToken.Parse(form).ToObject<Resources>();

            Forms f = new Forms();
            f = forms.forms;

            rsrcGroupTitle.Text = "Forms";
            rsrcesLabel2.Text = "Graduate Forms";
            rsrcesLabel3.Text = "Undergraduate Forms";
            rsrcesLabel4.Visible = false;
            rsrcesRichTextBox2.Text = f.graduateForms[0].formName + Environment.NewLine;
            rsrcesRichTextBox2.Text += f.graduateForms[1].formName + Environment.NewLine;
            rsrcesRichTextBox2.Text += f.graduateForms[2].formName + Environment.NewLine;
            rsrcesRichTextBox2.Text += f.graduateForms[3].formName;
            rsrcesRichTextBox3.Text = f.undergraduateForms[0].formName;
            rsrcesRichTextBox4.Visible = false;
        }

        private void minorCourseIndex(object sender, EventArgs e)
        {
            string curItem = courseListbox.SelectedItem.ToString();
            curItem = curItem.Replace("-> ", "");
            if (!curItem.Equals(Environment.NewLine))
            {
                courseDescTb.Text = curItem;

                // get course information
                string crsJson = getRestData("/course/courseID="+curItem);
                Course course = JToken.Parse(crsJson).ToObject<Course>();

                coursenameTitle.Text = course.title;
                courseDescTb.Text = course.description;

            }
        }

        private void btnMouseEnter(object sender, EventArgs e)
        {
            Button b = sender as Button;

            if (b == null) // just to be on the safe side
                return;

            b.Size = new Size(b.Size.Width + 40, b.Size.Height + 40);
            b.Location = new Point(b.Location.X - (40 / 2), b.Location.Y - (40 / 2));
            b.ForeColor = System.Drawing.Color.DarkTurquoise;
        }

        private void btnMouseLeave(object sender, EventArgs e)
        {
            Button b = sender as Button;

            if (b == null) // just to be on the safe side
                return;

            b.Size = new Size(b.Size.Width - 40, b.Size.Height - 40);
            b.Location = new Point(b.Location.X + (40 / 2), b.Location.Y + (40 / 2));
            b.ForeColor = System.Drawing.Color.White;
        }

        private void minorMouseEnter(object sender, EventArgs e)
        {
            Button b = sender as Button;

            if (b == null) // just to be on the safe side
                return;

            b.Size = new Size(b.Size.Width + 30, b.Size.Height + 30);
            b.Location = new Point(b.Location.X - (30 / 2), b.Location.Y - (30 / 2));
            b.ForeColor = System.Drawing.Color.Orange;
        }

        private void minorMouseLeave(object sender, EventArgs e)
        {
            Button b = sender as Button;

            if (b == null) // just to be on the safe side
                return;

            b.Size = new Size(b.Size.Width - 30, b.Size.Height - 30);
            b.Location = new Point(b.Location.X + (30 / 2), b.Location.Y + (30 / 2));
            b.ForeColor = System.Drawing.Color.White;
        }

        private void employmentInfoEnter(object sender, EventArgs e)
        {
            Button b = sender as Button;

            if (b == null) // just to be on the safe side
                return;

            b.Size = new Size(b.Size.Width + 25, b.Size.Height + 25);
            b.Location = new Point(b.Location.X - (25 / 2), b.Location.Y - (25 / 2));
            b.ForeColor = System.Drawing.Color.White;
        }

        private void employmentInfoLeave(object sender, EventArgs e)
        {
            Button b = sender as Button;

            if (b == null) // just to be on the safe side
                return;

            b.Size = new Size(b.Size.Width - 25, b.Size.Height - 25);
            b.Location = new Point(b.Location.X + (25 / 2), b.Location.Y + (25 / 2));
            b.ForeColor = System.Drawing.Color.Black;
        }

        private void rsrchMouseEnter(object sender, EventArgs e)
        {
            Button b = sender as Button;

            if (b == null) // just to be on the safe side
                return;

            b.Size = new Size(b.Size.Width + 50, b.Size.Height + 50);
            b.Location = new Point(b.Location.X - (50 / 2), b.Location.Y - (50 / 2));
            b.Font = new Font(b.Font.FontFamily,14, FontStyle.Bold);
        }

        private void rsrchMouseLeave(object sender, EventArgs e)
        {
            Button b = sender as Button;

            if (b == null) // just to be on the safe side
                return;

            b.Size = new Size(b.Size.Width - 50, b.Size.Height - 50);
            b.Location = new Point(b.Location.X + (50 / 2), b.Location.Y + (50 / 2));
            b.Font = new Font(b.Font.FontFamily, 10, FontStyle.Bold);
        }

        private void imageMouseEnter(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;

            if (pb == null)
                return;

            pb.Size = new Size(pb.Size.Width + 25, pb.Size.Height + 25);
            pb.Location = new Point(pb.Location.X - (25 / 2), pb.Location.Y - (25 / 2));
        }

        private void imageMouseLeave(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;

            if (pb == null)
                return;

            pb.Size = new Size(pb.Size.Width - 25, pb.Size.Height - 25);
            pb.Location = new Point(pb.Location.X + (25 / 2), pb.Location.Y + (25 / 2));
        }

        private void pplImageClick(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;

            if (pb == null)
                return;

            string x = (string)pb.Tag;
            string y = "";
            if (x.Contains("**"))
            {
                x = x.Replace("**", "");
                y = "staff";
            }
            else
            {
                y = "faculty";
            }

            PplInfo pi = new PplInfo(x,y);
            pi.ShowDialog();
        }
    }
}
