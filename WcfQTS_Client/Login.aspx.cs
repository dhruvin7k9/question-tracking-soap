using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WcfQTS_Client.ServiceReferenceUser;

namespace WcfQTS_Client
{
    public partial class Login : System.Web.UI.Page
    {
        UserServiceClient _userClient = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            _userClient = new UserServiceClient();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string userName = TextBox1.Text.Trim();
            string password = TextBox2.Text;
            User user = _userClient.GetUserByUserName(userName);
            if (user != null && user.password == password)
            {
                Session["CurrentUser"] = _userClient.GetUserByUserName(userName);
                Response.Redirect("Dashboard.aspx");
            }
            else
            {
                Label3.Text = "Incorrect Credentials!";
            }  
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Page.Validate("rqrfldv");
            Response.Redirect("Register.aspx");
        }
    }
}