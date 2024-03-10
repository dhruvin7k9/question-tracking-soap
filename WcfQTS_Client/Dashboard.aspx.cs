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
    public class PlatformQuestionTopics
    {
        public Platform Platform { get; set; }
        public Question Question { get; set; }
        public List<string> Topics { get; set; }

        public PlatformQuestionTopics(Platform platform, Question question, List<string> topics)
        {
            Platform = platform;
            Question = question;
            Topics = topics;
        }
    }

    public partial class Dashboard : System.Web.UI.Page
    {
        UserServiceClient _userClient = new UserServiceClient();
        QuestionServiceClient _questionClient = new QuestionServiceClient();
        User user = null;
        Question[] questions;
        protected void Page_Load(object sender, EventArgs e)
        {
            user = Session["CurrentUser"] as User;
            if (user == null) {
                Response.Redirect("Login.aspx");
            }
            else
            {
                questions = _questionClient.GetQuestionsByUserId(user.Id);

                if (!IsPostBack)
                {
                    // Populate dropdowns with filter options
                    PopulatePlatformFilter();

                    // Load questions for the current user
                    LoadQuestionsForUser(questions);
                }
            }
        }

        private void PopulatePlatformFilter()
        {
            ddlPlatformFilter.DataSource = _questionClient.GetPlatforms();
            ddlPlatformFilter.DataTextField = "name";
            ddlPlatformFilter.DataValueField = "Id";
            ddlPlatformFilter.DataBind();
        }

        private Question[] GetQuestionsBySearchTerm(string searchTerm)
        {
            if (questions?.Length > 0)
            {
                // Fetch questions based on search term
                Question[] filteredQuestions = questions.Where(q => q.title.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0).ToArray();
                return filteredQuestions;
            }

            return null;
        }

        protected void LoadQuestionsForUser(Question[] questions)
        {
            if (questions?.Length > 0)
            {
                lblNoResults.Visible = false;

                // Combine questions with topics
                List<PlatformQuestionTopics> questionsWithTopics = new List<PlatformQuestionTopics>();
                foreach (Question question in questions)
                {
                    // Get all the Topics of Question
                    int[] topicIds = _questionClient.GetTopicsForQuestion(question.Id);
                    List<string> topics = new List<string>();
                    foreach (int id in topicIds)
                    {
                        topics.Add(_questionClient.GetTopicById(id).name);
                    }
                    Platform plt = _questionClient.GetPlatformById(question.platformId);

                    PlatformQuestionTopics pqt = new PlatformQuestionTopics(plt, question, topics);
                    questionsWithTopics.Add(pqt);

                }

                // Bind the combined data to the repeater
                rptQuestions.DataSource = questionsWithTopics;
                rptQuestions.DataBind();
            }
            else
            {
                lblNoResults.Visible = true;
            }
        }
        
        protected void BtnLogout_Click(object sender, EventArgs e)
        {
            Session.Remove("CurrentUser");
            Response.Redirect("Login.aspx");
        }

        protected void BtnDeleteQue_Click(object sender, EventArgs e)
        {
            // Get the button that triggered the event
            Button btnDelete = (Button)sender;

            // Get the corresponding question ID
            int questionId = Convert.ToInt32(btnDelete.CommandArgument);

            _questionClient.DeleteQuestion(questionId);


            // Reload questions after deletion
            Response.Redirect("Dashboard.aspx");
        }

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            string searchTerm = inputTextBox.Text;

            if (!string.IsNullOrEmpty(searchTerm))
            {
                // Fetch questions based on search term
                Question[] searchedQuestions = GetQuestionsBySearchTerm(searchTerm);

                LoadQuestionsForUser(searchedQuestions);
            }
        }

        protected void BtnFilter_Click(object sender, EventArgs e)
        {
            // Apply filters
            string platform = ddlPlatformFilter.SelectedValue;
            string difficulty = ddlDifficultyFilter.SelectedValue;
            string remark = ddlRemarkFilter.SelectedValue;

            Question[] filteredQuestions = questions;

            if (questions?.Length > 0)
            {
                if (!string.IsNullOrEmpty(platform))
                {
                    filteredQuestions = filteredQuestions.Where(q => q.platformId == int.Parse(platform)).ToArray();
                }

                if (!string.IsNullOrEmpty(difficulty))
                {
                    filteredQuestions = filteredQuestions.Where(q => q.difficulty == difficulty).ToArray();
                }

                if (!string.IsNullOrEmpty(remark))
                {
                    filteredQuestions = filteredQuestions.Where(q => q.remark == remark).ToArray();
                }
            }
            
            LoadQuestionsForUser(filteredQuestions);
        }
    }
}