using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace AnthemProviderMgmtSvc
{
    [DataContract]
    public class Specializations
    {
        [DataMember]
        public int SpecID { get; set; }
        [DataMember]
        public string Description { get; set; }
    }
}