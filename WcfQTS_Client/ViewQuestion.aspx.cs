using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WcfQTS_Client.ServiceReferenceQuestion;
using WcfQTS_Client.ServiceReferenceUser;

namespace WcfQTS_Client
{
    public partial class ViewQuestion : System.Web.UI.Page
    {
        UserServiceClient _userClient = new UserServiceClient();
        QuestionServiceClient _questionClient = new QuestionServiceClient();
        User user = null;
        int queId;

        protected void Page_Load(object sender, EventArgs e)
        {
            user = Session["CurrentUser"] as User;
            if (user == null)
            {
                Response.Redirect("Login.aspx");
            }
            if (!IsPostBack)
            {
                // Get the question Id
                queId = int.Parse(Request.QueryString["Id"]);

                // Load question details
                LoadQuestionDetails();
            }
        }

        private void LoadQuestionDetails()
        {
            Question question = _questionClient.GetQuestionById(queId);

            // Populate the labels with question details
            lblTitleValue.Text = question.title;
            hlUrlValue.Text = question.url;
            hlUrlValue.NavigateUrl = question.url;
            lblDifficultyValue.Text = question.difficulty;
            lblDifficultyValue.CssClass = question.difficulty;
            lblRemarkValue.Text = question.remark;
            if (!string.IsNullOrEmpty(question.note))
                lblNoteValue.Text = question.note;
            else
                lblNoteValue.Text = "No note found ...";

            // Populate platform label
            Platform platform = _questionClient.GetPlatformById(question.platformId);
            hlPlatformValue.Text = platform.name;
            hlPlatformValue.NavigateUrl = platform.url;

            // Populate the topics
            int[] topics = _questionClient.GetTopicsForQuestion(queId);
            foreach (var topic in topics)
            {
                lblTopicsValue.Text += _questionClient.GetTopicById(topic).name + ", ";
            }

            // Remove the trailing comma
            lblTopicsValue.Text = lblTopicsValue.Text.TrimEnd(',', ' ');
        }

        protected void BtnEdit_Click(object sender, EventArgs e)
        {
            queId = int.Parse(Request.QueryString["Id"]);
            Response.Redirect($"EditQuestion.aspx?Id={queId}");
        }

        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            queId = int.Parse(Request.QueryString["Id"]);
            _questionClient.DeleteQuestion(queId);
            Response.Redirect("Dashboard.aspx");
        }

        protected void BtnGoBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Dashboard.aspx");
        }

        protected void BtnLogout_Click(object sender, EventArgs e)
        {
            Session.Remove("CurrentUser");
            Response.Redirect("Login.aspx");
        }
    }
}