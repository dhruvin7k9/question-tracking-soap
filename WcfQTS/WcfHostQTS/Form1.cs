using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ServiceModel;

namespace WcfHostQTS
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        ServiceHost shUser = null, shQuestion = null;
        private void Form1_Load(object sender, EventArgs e)
        {
            shUser = new ServiceHost(typeof(WcfServiceLibQTS.UserService));
            shQuestion = new ServiceHost(typeof(WcfServiceLibQTS.QuestionService));

            shUser.Open();
            shQuestion.Open();

            label1.Text = "Service Running !";
        }

        private void Form1_Closing(object sender, FormClosingEventArgs e)
        {
            shQuestion.Close();
            shUser.Close();
        }
    }
}
