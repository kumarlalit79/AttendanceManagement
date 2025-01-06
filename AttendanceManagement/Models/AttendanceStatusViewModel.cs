using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AttendanceManagement.Models
{
    public class AttendanceStatusViewModel
    {
        public int AttendanceId { get; set; }
        public string StudentName { get; set; }
        public DateTime AttendanceDate { get; set; }
        public bool IsPresent { get; set; }
    }
}