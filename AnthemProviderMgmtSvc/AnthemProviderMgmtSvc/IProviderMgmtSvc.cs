using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace AnthemProviderMgmtSvc
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IProviderMgmtSvc" in both code and config file together.
    [ServiceContract]
    public interface IProviderMgmtSvc
    {
        [OperationContract]
        [WebInvoke(UriTemplate = "AddProvider", Method = "POST", ResponseFormat = WebMessageFormat.Json)]
        bool AddProvider(Provider provider);

        [OperationContract]
        [WebGet(UriTemplate = "Specializations",ResponseFormat=WebMessageFormat.Json)]
        List<Specializations> GetSpecialties();

        [OperationContract]
        [WebInvoke(UriTemplate = "CheckProviderExists?name={NPID}", Method = "GET",ResponseFormat=WebMessageFormat.Json)]
        bool CheckProviderExists(int NPID);

        [OperationContract]
        [WebInvoke(UriTemplate = "GetProviderbyNPID?name={NPID}", Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        Provider GetProviderbyNPID(int NPID);

        [OperationContract]
        [WebInvoke(UriTemplate = "GetProvidersList", Method = "POST", ResponseFormat = WebMessageFormat.Json)]
        List<Provider> GetProvidersList(GetProviderRequest request);
    }
}
