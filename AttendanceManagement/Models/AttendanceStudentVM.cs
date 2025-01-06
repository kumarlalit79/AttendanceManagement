using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AttendanceManagement.Models
{
    public class AttendanceStudentVM
    {
        public int attendance_id { get; set; }
        public int Att_student_id { get; set; }
        public Nullable<bool> isPresent { get; set; }
        public Nullable<System.DateTime> attendance_date { get; set; }
        public string marked_by { get; set; }
        public int student_id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Pincode { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Student_Type { get; set; }
        public Nullable<int> FK_Dept_Id { get; set; }
        public System.DateTime DOB { get; set; }
        public Nullable<int> FK_Collage_Id { get; set; }
        public Nullable<int> FK_Year_Id { get; set; }
        public Nullable<int> FK_Sem_Id { get; set; }
        public Nullable<int> FK_Course_Id { get; set; }
        public Nullable<int> FK_Hostel_Id { get; set; }
        public System.DateTime Created_date { get; set; }
        public string Created_by { get; set; }
        public string Password { get; set; }
        public int Id { get; set; }
    }
}