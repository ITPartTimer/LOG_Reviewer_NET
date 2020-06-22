using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LOG_Review_NET;
using LOG_Review_NET.Models;

namespace LOG_Review_NET.DataAccess
{
    public class SQLData : Helpers
    {
        public List<LogDataModel> Get_Log_Data()
        {
            string logLen = ConfigurationManager.AppSettings.Get("LogLength");

            List<LogDataModel> lst = new List<LogDataModel>();

            SqlCommand cmd = new SqlCommand();
            SqlDataReader rdr = default(SqlDataReader);

            SqlConnection conn = new SqlConnection(STRATIXDataConnString);

            using (conn)
            {
                conn.Open();

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select top " + logLen + " * from Log order by LogID desc";
                cmd.Connection = conn;

                rdr = cmd.ExecuteReader();

                using (rdr)
                {
                    while (rdr.Read())
                    {
                        LogDataModel r = new LogDataModel();

                        r.rptID = Convert.ToInt32(rdr["LogID"]);
                        r.dt = Convert.ToDateTime(rdr["LogDate"]);
                        r.typ = (string)rdr["Typ"];
                        r.app = (string)rdr["AppName"];
                        r.msg = (string)rdr["Msg"];
                        r.src = (string)rdr["Src"];
                        r.stk = (string)rdr["StkTrc"];
                        r.trgt = (string)rdr["Trgt"];

                        lst.Add(r);
                    }
                }
            }

            return lst;
        }
    }
}
