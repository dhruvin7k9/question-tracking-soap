using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;


namespace WcfServiceLibQTS
{
    [ServiceContract]
    public interface IQuestionService
    {
        [OperationContract]
        List<Question> GetQuestions();

        [OperationContract]
        Question GetQuestionById(int id);

        [OperationContract]
        Question GetQuestionByTitle(string title);

        [OperationContract]
        Question GetQuestionByTitleForUser(string title, int userId);

        [OperationContract]
        void DeleteQuestion(int id);

        [OperationContract]
        void UpdateQuestion(int id, Question question);

        [OperationContract]
        void CreateQuestion(Question question);

        [OperationContract]
        void AddTopicToQuestion(int questionId, int topicId);

        [OperationContract]
        void RemoveTopicFromQuestion (int questionId);

        [OperationContract]
        List<int> GetTopicsForQuestion(int questionId);

        [OperationContract]
        List<Question> GetQuestionsByUserId (int userId);

        [OperationContract]
        List<Question> GetQuestionsByTagAndUser(int tagId, int userId);

        [OperationContract]
        List<Question> GetQuestionsByPlatformAndUser(int platformId, int userId);

        [OperationContract]
        List<Platform> GetPlatforms();

        [OperationContract]
        Platform GetPlatformById(int id);

        [OperationContract]
        Platform GetPlatformByName(string name);

        [OperationContract]
        List<Topic> GetTopics();

        [OperationContract]
        Topic GetTopicById(int id);

        [OperationContract]
        Topic GetTopicByName(string name);
    }
}
