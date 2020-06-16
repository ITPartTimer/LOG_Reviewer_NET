using System;
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
        public List<EmployeesReportsModel> Get_Emp_Reports(string brh, string name)
        {
            List<EmployeesReportsModel> lst = new List<EmployeesReportsModel>();

            SqlCommand cmd = new SqlCommand();
            SqlDataReader rdr = default(SqlDataReader);

            SqlConnection conn = new SqlConnection(STRATIXDataConnString);

            using (conn)
            {
                conn.Open();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "RPT_LKU_proc_Emails_ByBrh_ByRptName";
                cmd.Connection = conn;

                AddParamToSQLCmd(cmd, "@brh", SqlDbType.VarChar, 2, ParameterDirection.Input, brh);
                AddParamToSQLCmd(cmd, "@name", SqlDbType.VarChar, 25, ParameterDirection.Input, name);

                rdr = cmd.ExecuteReader();

                using (rdr)
                {
                    while (rdr.Read())
                    {
                        EmployeesReportsModel r = new EmployeesReportsModel();

                        r.rptID = (int)rdr["RptID"];
                        r.email = (string)rdr["EmpEmail"];
                        r.temppath = (string)rdr["RptTempPath"];
                        r.rootpath = (string)rdr["RptRootPath"];
                        r.filename = (string)rdr["RptFileName"];
                        r.fullpath = (string)rdr["RptFullPath"];

                        lst.Add(r);
                    }
                }
            }

            return lst;
        }

        public List<LogDataModel> Get_Log_Data()
        {
            List<LogDataModel> lst = new List<LogDataModel>();

            SqlCommand cmd = new SqlCommand();
            SqlDataReader rdr = default(SqlDataReader);

            SqlConnection conn = new SqlConnection(STRATIXDataConnString);

            using (conn)
            {
                conn.Open();

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select top 50 * from Log order by LogID desc";
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
                        r.stk = (string)rdr["Stk"];
                        r.trgt = (string)rdr["Trgt"];

                        lst.Add(r);
                    }
                }
            }

            return lst;
        }

       

    }
}
