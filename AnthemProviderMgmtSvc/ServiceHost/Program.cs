using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Runtime.Serialization;
using System.Net.Http;
using System.Web;
using Newtonsoft.Json;
using AnthemProviderMgmtSvc;

namespace AnthemPvdrSvcHost
{
    class Program
    {
        static void Main(string[] args)
        {
            using(ServiceHost host = new ServiceHost(typeof(AnthemProviderMgmtSvc.ProviderMgmtSvc)))
            {
                host.Open();
                Console.WriteLine("Host is started:- " + DateTime.Now.ToString());
                //Console.ReadLine();

                using (var response = new HttpClient())
                {      
                    response.BaseAddress = new Uri("http://localhost:58591");
                    var result = response.GetAsync("/ProviderMgmtSvc.svc/Specializations").Result;
                    string jsonString = result.Content.ReadAsStringAsync().Result;
                    Console.WriteLine("Specialtities Received in JSON:- " + DateTime.Now.ToString());

                    List<Specializations> specializations = JsonConvert.DeserializeObject<List<Specializations>>(jsonString);
                }
        }
            }
        }
    }
