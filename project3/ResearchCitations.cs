using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace project3
{
    public partial class ResearchCitations : Form
    {
        public ResearchCitations(string name, string type)
        {
            InitializeComponent();

            Form1 f = new Form1();

            if (type.Equals("Area"))
            {                
                // get research by interest information
                string rsrch = f.getRestData("/research/byInterestArea/areaName="+name);
                // need a way to get the JSON form into research object
                ByInterestArea research = JToken.Parse(rsrch).ToObject<ByInterestArea>();

                researchDtlLbl.Text = "Research By Domain Area: " + research.areaName;

                researchListBox.Items.Add("");
                for (int i = 0; i < research.citations.Count; i++)
                {
                    researchListBox.Items.Add("-> "+research.citations[i]);
                    researchListBox.Items.Add("");
                }                    
            }
            else
            {
                // get research by interest information
                string rsrch = f.getRestData("/research/byFaculty/username=" + name);
                // need a way to get the JSON form into research object
                ByFaculty research = JToken.Parse(rsrch).ToObject<ByFaculty>();

                researchDtlLbl.Text = "" + research.facultyName;

                researchListBox.Items.Add("");
                for (int i = 0; i < research.citations.Count; i++)
                {
                    researchListBox.Items.Add("-> " + research.citations[i]);
                    researchListBox.Items.Add("");
                }
            }
        }
    }
}
