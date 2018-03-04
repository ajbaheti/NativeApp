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
    public partial class PplInfo : Form
    {
        public PplInfo(string uname, string type)
        {
            InitializeComponent();

            Form1 f = new Form1();

            if (type.Equals("faculty"))
            {
                string ppl = f.getRestData("/people/faculty/username=" + uname);
                Faculty fac = JToken.Parse(ppl).ToObject<Faculty>();

                pplLabel.Text = fac.name;
                pplTitleLbl.Text = fac.title;
                pictureBox1.Load(fac.imagePath);
                emailLbl.Text = "Email:  "+fac.email;
                phoneLbl.Text = "Phone:  "+fac.phone;
                officeLbl.Text = "Office:  "+fac.office;
            }
            else
            {
                string ppl = f.getRestData("/people/staff/username=" + uname);
                Staff stf = JToken.Parse(ppl).ToObject<Staff>();

                pplLabel.Text = stf.name+",";
                pplTitleLbl.Text = stf.title;
                pictureBox1.Load(stf.imagePath);
                emailLbl.Text = "Email:  " + stf.email;
                phoneLbl.Text = "Phone:  " + stf.phone;
                officeLbl.Text = "Office:  " + stf.office;
            }
        }
    }
}
