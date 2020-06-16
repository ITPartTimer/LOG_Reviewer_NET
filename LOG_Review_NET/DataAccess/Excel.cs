using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Data.OleDb;
using System.Text;
using System.Threading.Tasks;
using LOG_Review_NET.Models;

namespace LOG_Review_NET.DataAccess
{
    public class ExcelExport
    {
        public void WriteLogData(List<LogDataModel> lstLog, string fullPath)
        {
            // initialize text used in OleDbCommand
            string cmdText = "";

            string excelConnString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fullPath + @";Extended Properties=""Excel 8.0;HDR=YES;""";

            using (OleDbConnection eConn = new OleDbConnection(excelConnString))
            {
                try
                {
                    eConn.Open();

                    OleDbCommand eCmd = new OleDbCommand();

                    eCmd.Connection = eConn;

                    foreach (LogDataModel m in lstLog)
                    {
                        // Use parameters to insert into XLS
                        cmdText = "Insert into [Log$] (ID,DT,TYP,APP,MSG,SRC,STK,TRGT) Values(@id,@dt,@typ,@app,@msg,@src,@stk,@trgt)";

                        eCmd.CommandText = cmdText;

                        eCmd.Parameters.AddRange(new OleDbParameter[]
                        {
                                    new OleDbParameter("@id", m.rptID),
                                    new OleDbParameter("@dt", m.dt),
                                    new OleDbParameter("@typ", m.typ),
                                    new OleDbParameter("@app", m.app),
                                    new OleDbParameter("@msg", m.msg),
                                    new OleDbParameter("@src", m.src),
                                    new OleDbParameter("@stk", m.stk),
                                    new OleDbParameter("@trgt", m.trgt),
                        });

                        eCmd.ExecuteNonQuery();

                        // Need to clear Parameters on each pass
                        eCmd.Parameters.Clear();
                    }
                }
                catch (OleDbException)
                {
                    throw;
                }
                catch(Exception)
                {
                    throw;
                }
                finally
                {
                    eConn.Close();
                    eConn.Dispose();
                }
            }
        }
    }
    
}
