using System;
using System.Collections.Generic;
using System.Text;

namespace LOG_Review_NET.Models
{
    public class EmployeesReportsModel
    {
        public int rptID { get; set; }
        public string email { get; set; }
        public string temppath { get; set; }
        public string rootpath { get; set; }
        public string filename { get; set; }
        public string fullpath { get; set; }
    }

    public class LogDataModel
    {
        public int rptID { get; set; }
        public DateTime dt { get; set; }
        public string typ { get; set; }
        public string app { get; set; }
        public string msg { get; set; }
        public string src { get; set; }
        public string stk { get; set; }
        public string trgt { get; set; }
    }
}
