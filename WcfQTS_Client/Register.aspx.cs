using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WcfQTS_Client.ServiceReferenceUser;

namespace WcfQTS_Client
{
    public partial class Register : System.Web.UI.Page
    {
        UserServiceClient _userClient = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            _userClient = new UserServiceClient();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string userName = TextBox1.Text.Trim();
            string email = TextBox2.Text.Trim();
            string password = TextBox3.Text;
            User user = _userClient.GetUserByUserName(userName);
            if (user != null && user.username == userName)
            {
                Label3.Text = "User already exists!";
            }
            else
            {
                user.username = userName;
                user.email = email;
                user.password = password;
                _userClient.CreateUser(user);

                Session["CurrentUser"] = _userClient.GetUserByUserName(userName); // need to have user id so getting again
                Response.Redirect("Dashboard.aspx");
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Page.Validate("rqrfldv");
            Response.Redirect("Login.aspx");
        }
    }
}