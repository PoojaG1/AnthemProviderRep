using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using System.Runtime.Serialization;


namespace AnthemProviderMgmtSvc
{
    [DataContract]
    public class Provider
    {
        [DataMember]
        public int ProviderId { get; set; }
        [DataMember]
        public int NPID { get; set; }
        [DataMember]
        public string ProviderName { get; set; }
        [DataMember]
        public int ZipCode { get; set; }
        [DataMember]
        public int Specialty { get; set; }
        [DataMember]
        public bool Status { get; set; }
    }
}