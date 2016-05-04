using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace AnthemProviderMgmtSvc
{
    public class DAL
    {
        DBHelper dbHelper;
        public DAL()
        {
            dbHelper = new DBHelper();
        }

        public List<Specializations>GetSpecializations()
        {
            List<Specializations> specs = new List<Specializations>();
            DataSet ds = dbHelper.ExecuteDataSet("Select * from Specializations;", null, CommandType.Text);
            if(ds!=null && ds.Tables[0] !=null)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    specs.Add(new Specializations { SpecID = Convert.ToInt32(dr["SpecID"]), Description = Convert.ToString(dr["Description"]) });
                }
            }
            return specs;
        }

        public bool AddProvider(Provider provider)
        {
            object[] parameters = GetProviderTransferObject(provider);
            int result = dbHelper.ExecuteNonQuery("Insert into Provider values ({0},{1},{2},{3},{4});", CommandType.Text, parameters);
            if (result>0)
                return true;
            else
            return false;
        }

        private object[] GetProviderTransferObject(Provider provider)
        {
            object[] array = new object[5];
            array[0] = provider.NPID;
            array[1]=provider.ProviderName;
            array[2]=provider.ZipCode;
            array[3]=provider.Status == true?1:0;
            array[4] = provider.Specialty;

            return array;
        }
    }
}