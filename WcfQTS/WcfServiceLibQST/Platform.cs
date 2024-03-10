using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WcfServiceLibQTS
{
    [DataContract]
    public class Platform
    {
        [DataMember (IsRequired=false)]
        public int Id { get; set; }

        [DataMember (IsRequired=false)]
        public string name;

        [DataMember (IsRequired=false)]
        public string url;
    }
}
