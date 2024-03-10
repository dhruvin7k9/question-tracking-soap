using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WcfServiceLibQTS
{
    [DataContract]
    public class Question
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string title { get; set; }

        [DataMember]
        public string url { get; set; }

        [DataMember]
        public string difficulty { get; set; }

        [DataMember]
        public string remark { get; set; }

        [DataMember (IsRequired=false)]
        public string note { get; set; }

        [DataMember]
        public int platformId { get; set; }

        [DataMember]
        public int userId { get; set; }
    }
}
