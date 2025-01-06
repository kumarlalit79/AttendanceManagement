using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AttendanceManagement.Models
{
    public class LoginViewModel
    {
        public int UserId { get; set; }  // For Faculty_Id (Teacher) or Student_Id (Student)
        public string Password { get; set; }
        public string UserType { get; set; }  // "Teacher" or "Student"
    }
}