using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;

namespace AnthemProviderMgmtSvc
{
    public class GetProviderRequest
    {
        public int ZipCode { get; set; }
        public string ProviderName { get; set; }
        public int SpecId { get; set; }
    }
}