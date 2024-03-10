using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WcfServiceLibQTS
{
    [DataContract]
    public class QuestionTopic
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int questionId { get; set; }

        [DataMember]
        public int topicId { get; set; }
    }
}
