using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WcfServiceLibQTS
{
    [DataContract]
    public class User
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string username { get; set; }

        [DataMember]
        public string password { get; set; }

        [DataMember(IsRequired = false)]
        public string email { get; set; }
    }
}
