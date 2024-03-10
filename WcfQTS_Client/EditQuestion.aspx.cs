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
    public partial class EditQuestion : System.Web.UI.Page
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
                Page.Validate("questionValidation");
                Response.Redirect("Login.aspx");
            }

            if (!IsPostBack)
            {
                // Get the question Id
                queId = int.Parse(Request.QueryString["Id"]);

                // Populate the platform dropdown
                ddlPlatform.DataSource = _questionClient.GetPlatforms();
                ddlPlatform.DataTextField = "name";
                ddlPlatform.DataValueField = "Id";
                ddlPlatform.DataBind();

                // Populate the topics checkbox
                cblTopics.DataSource = _questionClient.GetTopics();
                cblTopics.DataTextField = "name";
                cblTopics.DataValueField = "Id";
                cblTopics.DataBind();

                // Add CssClass to each ListItem after DataBinding
                foreach (ListItem item in cblTopics.Items)
                {
                    item.Attributes["class"] = "cbl-li";
                }

                // Fill the fields according to the Question ID
                Question question = _questionClient.GetQuestionById(queId);
                txtTitle.Text = question.title;
                txtUrl.Text = question.url;
                txtNote.Text = question.note;

                ddlPlatform.SelectedValue = question.platformId.ToString();
                ddlDifficulty.SelectedValue = question.difficulty;
                ddlRemark.SelectedValue = question.remark;

                int[] queTopics = _questionClient.GetTopicsForQuestion(queId);
                foreach (ListItem item in cblTopics.Items)
                {
                    if (queTopics.Any(id => id == int.Parse(item.Value)))
                    {
                        item.Selected = true;
                    }
                }
            }
        }

        // Method to get selected topics
        private List<Topic> GetSelectedTopics()
        {
            List<Topic> selectedTopics = new List<Topic>();
            foreach (ListItem item in cblTopics.Items)
            {
                if (item.Selected)
                {
                    Topic topic = new Topic();

                    topic.Id = int.Parse(item.Value);
                    topic.name = item.Text;

                    selectedTopics.Add(topic);
                }
            }
            return selectedTopics;
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                queId = int.Parse(Request.QueryString["Id"]);

                string title = txtTitle.Text;

                Question queExists = _questionClient.GetQuestionByTitleForUser(title, user.Id);
                if (queExists != null && queExists.userId == user.Id && queExists.title != title)
                {
                    lblQueExists.Text = "Question with Given Title exists !";
                }
                else
                {
                    string url = txtUrl.Text;
                    string difficulty = ddlDifficulty.SelectedValue;
                    string remark = ddlRemark.SelectedValue;
                    string note = txtNote.Text;
                    int platformId = int.Parse(ddlPlatform.SelectedValue);
                    List<Topic> topics = GetSelectedTopics();

                    // Create a question object using the input data
                    Question question = new Question
                    {
                        title = title,
                        url = url,
                        difficulty = difficulty,
                        remark = remark,
                        note = note,
                        userId = user.Id,
                        platformId = platformId,
                    };

                    // Update a database entry
                    _questionClient.UpdateQuestion(queId, question);

                    // Update QuestioTopic entry by first removing entries and then creating
                    _questionClient.RemoveTopicFromQuestion(queId);

                    for (int i = 0; i < topics.Count; i++)
                    {
                        _questionClient.AddTopicToQuestion(queId, topics[i].Id);
                    }

                    Response.Redirect("Dashboard.aspx");
                }
            }
        }

        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            Page.Validate("questionValidation");
            Response.Redirect("Dashboard.aspx");
        }

        protected void BtnLogout_Click(object sender, EventArgs e)
        {
            Page.Validate("questionValidation");
            Session.Remove("CurrentUser");
            Response.Redirect("Login.aspx");
        }
    }
}