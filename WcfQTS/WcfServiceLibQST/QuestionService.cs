using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WcfServiceLibQTS
{
    public class QuestionService : IQuestionService
    {
        readonly string CnnStr = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=QTS;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public List<Question> GetQuestions()
        {
            List<Question> questions = new List<Question>();

            using (SqlConnection cnn = new SqlConnection(CnnStr))
            {
                string query = "SELECT * FROM Question";
                SqlCommand cmd = new SqlCommand(query, cnn);

                cnn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Question question = new Question
                    {
                        Id = reader.GetInt32(0),
                        title = reader.GetString(1),
                        url = reader.GetString(2),
                        difficulty = reader.GetString(3),
                        remark = reader.GetString(4),
                        note = reader.IsDBNull(5) ? string.Empty : reader.GetString(5),
                        platformId = reader.GetInt32(6),
                        userId = reader.GetInt32(7),
                    };

                    questions.Add(question);
                }
            }

            return questions;
        }

        public Question GetQuestionById(int id)
        {
            SqlConnection cnn = new SqlConnection(CnnStr);
            string query = "SELECT * FROM Question WHERE Id=@id";
            SqlCommand cmd = new SqlCommand(query, cnn);
            SqlParameter p = new SqlParameter("@id", id);
            cmd.Parameters.Add(p);

            cnn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            Question question = new Question();
            while (reader.Read())
            {
                question.Id = reader.GetInt32(0);
                question.title = reader.GetString(1);
                question.url = reader.GetString(2);
                question.difficulty = reader.GetString(3);
                question.remark = reader.GetString(4);
                question.note = reader.IsDBNull(5) ? string.Empty : reader.GetString(5);
                question.platformId = reader.GetInt32(6);
                question.userId = reader.GetInt32(7);
            }

            reader.Close();
            cnn.Close();

            return question;
        }

        public Question GetQuestionByTitle(string title)
        {
            SqlConnection cnn = new SqlConnection(CnnStr);
            string query = "SELECT * FROM Question WHERE title=@title";
            SqlCommand cmd = new SqlCommand(query, cnn);
            SqlParameter p = new SqlParameter("@title", title);
            cmd.Parameters.Add(p);

            cnn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            Question question = new Question();
            while (reader.Read())
            {
                question.Id = reader.GetInt32(0);
                question.title = reader.GetString(1);
                question.url = reader.GetString(2);
                question.difficulty = reader.GetString(3);
                question.remark = reader.GetString(4);
                question.note = reader.IsDBNull(5) ? string.Empty : reader.GetString(5);
                question.platformId = reader.GetInt32(6);
                question.userId = reader.GetInt32(7);
            }

            reader.Close();
            cnn.Close();

            return question;
        }

        public Question GetQuestionByTitleForUser(string title, int userId)
        {
            SqlConnection cnn = new SqlConnection(CnnStr);
            string query = "SELECT * FROM Question WHERE title=@title AND userId=@userId";
            SqlCommand cmd = new SqlCommand(query, cnn);
            cmd.Parameters.AddWithValue("@userId", userId);
            cmd.Parameters.AddWithValue("@title", title);

            cnn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            Question question = new Question();
            while (reader.Read())
            {
                question.Id = reader.GetInt32(0);
                question.title = reader.GetString(1);
                question.url = reader.GetString(2);
                question.difficulty = reader.GetString(3);
                question.remark = reader.GetString(4);
                question.note = reader.IsDBNull(5) ? string.Empty : reader.GetString(5);
                question.platformId = reader.GetInt32(6);
                question.userId = reader.GetInt32(7);
            }

            reader.Close();
            cnn.Close();

            return question;
        }

        public void DeleteQuestion(int id)
        {
            using (SqlConnection cnn = new SqlConnection(CnnStr))
            {
                string query = "DELETE FROM Question WHERE Id = @id";
                SqlCommand cmd = new SqlCommand(query, cnn);
                cmd.Parameters.AddWithValue("@id", id);

                cnn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateQuestion(int id, Question question)
        {
            using (SqlConnection cnn = new SqlConnection(CnnStr))
            {
                string query = "UPDATE Question SET title = @title, url = @url, difficulty = @difficulty, remark = @remark, note = @note, platformId = @platformId, userId = @userId WHERE Id = @id";
                SqlCommand cmd = new SqlCommand(query, cnn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@title", question.title);
                cmd.Parameters.AddWithValue("@url", question.url);
                cmd.Parameters.AddWithValue("@difficulty", question.difficulty);
                cmd.Parameters.AddWithValue("@remark", question.remark);
                cmd.Parameters.AddWithValue("@note", string.IsNullOrEmpty(question.note) ? (object)DBNull.Value : question.note);
                cmd.Parameters.AddWithValue("@platformId", question.platformId);
                cmd.Parameters.AddWithValue("@userId", question.userId);

                cnn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void CreateQuestion(Question question)
        {
            using (SqlConnection cnn = new SqlConnection(CnnStr))
            {
                string query = "INSERT INTO Question (title, url, difficulty, remark, note, platformId, userId) VALUES (@title, @url, @difficulty, @remark, @note, @platformId, @userId)";
                SqlCommand cmd = new SqlCommand(query, cnn);
                cmd.Parameters.AddWithValue("@title", question.title);
                cmd.Parameters.AddWithValue("@url", question.url);
                cmd.Parameters.AddWithValue("@difficulty", question.difficulty);
                cmd.Parameters.AddWithValue("@remark", question.remark);
                cmd.Parameters.AddWithValue("@note", string.IsNullOrEmpty(question.note) ? (object)DBNull.Value : question.note);
                cmd.Parameters.AddWithValue("@platformId", question.platformId);
                cmd.Parameters.AddWithValue("@userId", question.userId);

                cnn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void AddTopicToQuestion(int questionId, int topicId)
        {
            using (SqlConnection cnn = new SqlConnection(CnnStr))
            {
                string query = "INSERT INTO QuestionTopic (questionId, topicId) VALUES (@questionId, @topicId)";
                SqlCommand cmd = new SqlCommand(query, cnn);
                cmd.Parameters.AddWithValue("@questionId", questionId);
                cmd.Parameters.AddWithValue("@topicId", topicId);

                cnn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void RemoveTopicFromQuestion(int questionId)
        {
            using (SqlConnection cnn = new SqlConnection(CnnStr))
            {
                string query = "DELETE FROM QuestionTopic WHERE questionId = @questionId";
                SqlCommand cmd = new SqlCommand(query, cnn);
                cmd.Parameters.AddWithValue("@questionId", questionId);

                cnn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<int> GetTopicsForQuestion(int questionId)
        {
            List<int> topicIds = new List<int>();

            using (SqlConnection cnn = new SqlConnection(CnnStr))
            {
                string query = "SELECT topicId FROM QuestionTopic WHERE questionId = @questionId";
                SqlCommand cmd = new SqlCommand(query, cnn);
                cmd.Parameters.AddWithValue("@questionId", questionId);

                cnn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    topicIds.Add(reader.GetInt32(0));
                }
            }

            return topicIds;
        }

        public List<Question> GetQuestionsByUserId(int userId)
        {
            List<Question> questions = new List<Question>();

            using (SqlConnection cnn = new SqlConnection(CnnStr))
            {
                string query = "SELECT * FROM Question WHERE userId = @userId ORDER BY platformId ASC";
                SqlCommand cmd = new SqlCommand(query, cnn);
                cmd.Parameters.AddWithValue("@userId", userId);

                cnn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Question question = new Question
                    {
                        Id = reader.GetInt32(0),
                        title = reader.GetString(1),
                        url = reader.GetString(2),
                        difficulty = reader.GetString(3),
                        remark = reader.GetString(4),
                        note = reader.IsDBNull(5) ? string.Empty : reader.GetString(5),
                        platformId = reader.GetInt32(6),
                        userId = reader.GetInt32(7),
                    };

                    questions.Add(question);
                }
            }

            return questions;
        }

        public List<Question> GetQuestionsByTagAndUser(int tagId, int userId)
        {
            List<Question> questions = new List<Question>();

            using (SqlConnection cnn = new SqlConnection(CnnStr))
            {
                string query = "SELECT q.* FROM Question q INNER JOIN QuestionTopic qt ON q.Id = qt.questionId WHERE qt.topicId = @tagId AND q.userId = @userId";
                SqlCommand cmd = new SqlCommand(query, cnn);
                cmd.Parameters.AddWithValue("@tagId", tagId);
                cmd.Parameters.AddWithValue("@userId", userId);

                cnn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Question question = new Question
                    {
                        Id = reader.GetInt32(0),
                        title = reader.GetString(1),
                        url = reader.GetString(2),
                        difficulty = reader.GetString(3),
                        remark = reader.GetString(4),
                        note = reader.IsDBNull(5) ? string.Empty : reader.GetString(5),
                        platformId = reader.GetInt32(6),
                        userId = reader.GetInt32(7),
                    };

                    questions.Add(question);
                }
            }

            return questions;
        }

        public List<Question> GetQuestionsByPlatformAndUser(int platformId, int userId)
        {
            List<Question> questions = new List<Question>();

            using (SqlConnection cnn = new SqlConnection(CnnStr))
            {
                string query = "SELECT * FROM Question WHERE platformId = @ptId AND userId = @userId";
                SqlCommand cmd = new SqlCommand(query, cnn);
                cmd.Parameters.AddWithValue("@ptId", platformId);
                cmd.Parameters.AddWithValue("@userId", userId);

                cnn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Question question = new Question
                    {
                        Id = reader.GetInt32(0),
                        title = reader.GetString(1),
                        url = reader.GetString(2),
                        difficulty = reader.GetString(3),
                        remark = reader.GetString(4),
                        note = reader.IsDBNull(5) ? string.Empty : reader.GetString(5),
                        platformId = reader.GetInt32(6),
                        userId = reader.GetInt32(7),
                    };

                    questions.Add(question);
                }
            }

            return questions;
        }

        List<Platform> IQuestionService.GetPlatforms()
        {
            List<Platform> platforms = new List<Platform>();

            using (SqlConnection cnn = new SqlConnection(CnnStr))
            {
                string query = "SELECT * FROM Platform";
                SqlCommand cmd = new SqlCommand(query, cnn);

                cnn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Platform platform = new Platform
                    {
                        Id = reader.GetInt32(0),
                        name = reader.GetString(1),
                        url = reader.GetString(2),
                    };

                    platforms.Add(platform);
                }
            }

            return platforms;
        }
        Platform IQuestionService.GetPlatformById(int id)
        {
            SqlConnection cnn = new SqlConnection(CnnStr);
            string query = "SELECT * FROM Platform WHERE Id=@id";
            SqlCommand cmd = new SqlCommand(query, cnn);
            SqlParameter p = new SqlParameter("@id", id);
            cmd.Parameters.Add(p);

            cnn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            Platform platform = new Platform();
            while (reader.Read())
            {
                platform.Id = reader.GetInt32(0);
                platform.name = reader.GetString(1);
                platform.url = reader.GetString(2);
            }

            reader.Close();
            cnn.Close();

            return platform;
        }

        Platform IQuestionService.GetPlatformByName(string name)
        {
            SqlConnection cnn = new SqlConnection(CnnStr);
            string query = "SELECT * FROM Platform WHERE name=@name";
            SqlCommand cmd = new SqlCommand(query, cnn);
            SqlParameter p = new SqlParameter("@name", name);
            cmd.Parameters.Add(p);

            cnn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            Platform platform = new Platform();
            while (reader.Read())
            {
                platform.Id = reader.GetInt32(0);
                platform.name = reader.GetString(1);
                platform.url = reader.GetString(2);
            }

            reader.Close();
            cnn.Close();

            return platform;
        }

        List<Topic> IQuestionService.GetTopics()
        {
            List<Topic> topics = new List<Topic>();

            using (SqlConnection cnn = new SqlConnection(CnnStr))
            {
                string query = "SELECT * FROM Topic";
                SqlCommand cmd = new SqlCommand(query, cnn);

                cnn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Topic topic = new Topic
                    {
                        Id = reader.GetInt32(0),
                        name = reader.GetString(1),
                    };

                    topics.Add(topic);
                }
            }

            return topics;
        }
        Topic IQuestionService.GetTopicById(int id)
        {
            SqlConnection cnn = new SqlConnection(CnnStr);
            string query = "SELECT * FROM Topic WHERE Id=@id";
            SqlCommand cmd = new SqlCommand(query, cnn);
            SqlParameter p = new SqlParameter("@id", id);
            cmd.Parameters.Add(p);

            cnn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            Topic topic = new Topic();
            while (reader.Read())
            {
                topic.Id = reader.GetInt32(0);
                topic.name = reader.GetString(1);
            }

            reader.Close();
            cnn.Close();

            return topic;
        }

        Topic IQuestionService.GetTopicByName(string name)
        {
            SqlConnection cnn = new SqlConnection(CnnStr);
            string query = "SELECT * FROM Topic WHERE name=@name";
            SqlCommand cmd = new SqlCommand(query, cnn);
            SqlParameter p = new SqlParameter("@name", name);
            cmd.Parameters.Add(p);

            cnn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            Topic topic = new Topic();
            while (reader.Read())
            {
                topic.Id = reader.GetInt32(0);
                topic.name = reader.GetString(1);
            }

            reader.Close();
            cnn.Close();

            return topic;
        }
    }

}
