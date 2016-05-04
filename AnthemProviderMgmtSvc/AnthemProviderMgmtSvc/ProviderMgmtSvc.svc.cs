using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace AnthemProviderMgmtSvc
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ProviderMgmtSvc" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ProviderMgmtSvc.svc or ProviderMgmtSvc.svc.cs at the Solution Explorer and start debugging.
    public class ProviderMgmtSvc : IProviderMgmtSvc
    {
        DAL dataAccess;
        public bool AddProvider(Provider provider)
        {
            dataAccess = new DAL();
            dataAccess.AddProvider(provider);
            return true;

        }

        public List<Specializations> GetSpecialties()
        {
            dataAccess = new DAL();
            List<Specializations> specializations = new List<Specializations>();

            specializations = dataAccess.GetSpecializations();

            return specializations;
        }

        public bool CheckProviderExists(int NPID)
        {
            return true;
        }

        public Provider GetProviderbyNPID(int NPID)
        {
            return new Provider();
        }

        public List<Provider> GetProvidersList(GetProviderRequest request)
        {
            return new List<Provider>();
        }
    }
}
