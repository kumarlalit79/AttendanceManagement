using AttendanceManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Data.Entity;

namespace AttendanceManagement.Controllers
{
    
    public class DashboardController : Controller
    {
        // GET: Dashboard

        
        public ActionResult Index()
        {
            if (Session["role"] != null && Session["role"].ToString() == "Teacher")
            {
                return RedirectToAction("Index", "TeacherDashboard");
            }
            else if(Session["role"] != null && Session["role"].ToString() == "Student"){
                return RedirectToAction("StudentDashboard", "Dashboard");
            }

            return View();
        }

        [Authorize(Roles= "Teacher")]
        public ActionResult TeacherDashboard()
        {
            List<string> DaysOfWeek = new List<string>
            {
                "Monday" , "Tuesday" , "Wednesday" , "Thursday" , "Friday" , "Saturday"
            };
            ViewBag.DaysOfWeekList = new SelectList(DaysOfWeek);
            return View();
        }

        [Authorize(Roles = "Student")]
        public ActionResult StudentDashboard()
        {
            using (AttendanceTrackrEntities db = new AttendanceTrackrEntities())
            {
                var studentId = (int)Session["studentId"];

                var studentData = db.student_tbl
                    .Include(x => x.department_tbl)
                    .Include(x => x.college_tbl)
                    .Include(x => x.year_tbl)
                    .Include(x => x.semester_tbl)
                    .Include(x => x.course_tbl)
                    .Include(x => x.hostel_tbl)
                    .Where(x => x.student_id == studentId)
                    .ToList();
                return View(studentData);
            }
            
        }

        [Authorize(Roles = "Admin")]
        public ActionResult AdminDashBoard()
        {
            using(AttendanceTrackrEntities db = new AttendanceTrackrEntities())
            {
                var adminId = (int)Session["adminId"];
                var adminData = db.Admins.ToList();
                return View(adminData);
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult CreateStudentPage()
        {
            using (AttendanceTrackrEntities db = new AttendanceTrackrEntities())
            {
                PopulateDropDownLists();
                return View();
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult ViewStudents()
        {
            using (AttendanceTrackrEntities db = new AttendanceTrackrEntities())
            {
                var allStudents = db.student_tbl
                    .Include(x => x.department_tbl)
                    .Include(x => x.college_tbl)
                    .Include(x => x.year_tbl)
                    .Include(x => x.semester_tbl)
                    .Include(x => x.course_tbl)
                    .Include(x => x.hostel_tbl)
                    .ToList();
                return View(allStudents);
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult ViewTeachers()
        {
            using (AttendanceTrackrEntities db = new AttendanceTrackrEntities())
            {
                var teachers = db.Teacher_tbl
                                    .Include(d => d.department_tbl)
                                    .ToList();
                return View(teachers);
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult ChooseDepartment()
        {
            using (AttendanceTrackrEntities db = new AttendanceTrackrEntities())
            {
                List<semester_tbl> semList = new List<semester_tbl>();
                var semAdd = db.semester_tbl.ToList();
                foreach (var item in semAdd)
                {
                    semList.Add(new semester_tbl
                    {
                        Sem_Id = item.Sem_Id,
                        Semester_Name = item.Semester_Name
                    });
                }
                ViewBag.semester = new SelectList(semList, "Sem_Id", "Semester_Name");
                return View();
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult CreateStudent(student_tbl s)
        {
            using (AttendanceTrackrEntities db = new AttendanceTrackrEntities())
            {
                
                s.Created_by = "Gaj";
                s.Created_date = DateTime.Now;
                
                
                db.student_tbl.Add(s);
                db.SaveChanges();

                var studentRole = db.Roles.FirstOrDefault(r => r.Role_Name == "Student");
                if(studentRole != null)
                {
                    UserRole urole = new UserRole();
                    urole.Student_Id = s.student_id;
                    urole.Role_Id = studentRole.Role_Id;

                    db.UserRoles.Add(urole);
                    db.SaveChanges();
                }
                

                PopulateDropDownLists();
                return RedirectToAction("CreateStudentPage", "Dashboard");
            }
        }
        

        [Authorize(Roles = "Admin")]
        public ActionResult CreateTeacherPage()
        {
            using (AttendanceTrackrEntities db = new AttendanceTrackrEntities())
            {
                List<department_tbl> deptList = new List<department_tbl>();
                var AddDept = db.department_tbl.ToList();
                foreach (var item in AddDept)
                {
                    deptList.Add(new department_tbl
                    {
                        DeptId = item.DeptId,
                        DeptName = item.DeptName,
                    });
                }
                ViewBag.DeptList = new SelectList(deptList, "DeptId", "DeptName");
                return View();
            }
        }

        
        [Authorize(Roles = "Admin")]
        public ActionResult ViewAllStudent(int? semesterId, int? departmentId)
        {
            PopulateDropDownLists();
            using (AttendanceTrackrEntities db = new AttendanceTrackrEntities())
            {
                
                int Fkcollegeid = (int)Session["collegeId"];
                var allStudents = db.student_tbl
                    .Include(x => x.department_tbl)
                    .Include(x => x.college_tbl)
                    .Include(x => x.year_tbl)
                    .Include(x => x.semester_tbl)
                    .Include(x => x.course_tbl)
                    .Include(x => x.hostel_tbl);
                    

                if(semesterId.HasValue && semesterId.Value != 0)
                {
                    allStudents = allStudents.Where(x => x.FK_Sem_Id == semesterId.Value);
                }
                if(departmentId.HasValue && departmentId.Value != 0)
                {
                    allStudents = allStudents.Where(x => x.FK_Dept_Id == departmentId.Value);
                }

                var result = allStudents.Select(x => new
                {
                    x.student_id,
                    x.Name,
                    x.Address,
                    x.City,
                    x.State,
                    x.Pincode,
                    x.Mobile,
                    x.Email,
                    x.Student_Type,
                    x.DOB,
                    college_tbl = new { x.college_tbl.Name },
                    department_tbl = new { x.department_tbl.DeptName },
                    semester_tbl = new { x.semester_tbl.Semester_Name }
                }).ToList();

                return View(allStudents);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult CreateTeacher(Teacher_tbl f)
        {
            using (AttendanceTrackrEntities db = new AttendanceTrackrEntities())
            {
                f.Created_date = DateTime.Now;
                f.Created_by = "Gaj";
                var data = db.Teacher_tbl.Add(f);
                int a = db.SaveChanges();
                if (a > 0)
                {
                    ModelState.Clear();
                    TempData["SignUpMsg"] = "Sign Up Successfully, now you can log in";

                }
            }
            using (AttendanceTrackrEntities db = new AttendanceTrackrEntities())
            {
                var deptList = db.department_tbl.ToList();
                ViewBag.DeptList = new SelectList(deptList, "DeptId", "DeptName");
                db.SaveChanges();
            }
            return RedirectToAction("CreateTeacherPage" , "Dashboard");
        }

        private void PopulateDropDownLists()
        {
            using (AttendanceTrackrEntities db = new AttendanceTrackrEntities())
            {
                var stateList = new List<string>()
                {
                    "Andhra Pradesh", "Arunachal Pradesh", "Assam", "Bihar", "Chhattisgarh", "Goa",
            "Gujarat", "Haryana", "Himachal Pradesh", "Jharkhand", "Karnataka", "Kerala",
            "Madhya Pradesh", "Maharashtra", "Manipur", "Meghalaya", "Mizoram", "Nagaland",
            "Odisha", "Punjab", "Rajasthan", "Sikkim", "Tamil Nadu", "Telangana", "Tripura",
            "Uttar Pradesh", "Uttarakhand", "West Bengal"
                };

                ViewBag.stateList = new SelectList(stateList);

                List<college_tbl> collegetbl = new List<college_tbl>();

                var collegeAdd = db.college_tbl.ToList();
                foreach (var item in collegeAdd)
                {
                    collegetbl.Add(new college_tbl
                    {
                        Col_id = item.Col_id,
                        Name = item.Name
                    });
                }
                ViewBag.College = new SelectList(db.college_tbl.ToList(), "Col_id", "Name");

                List<year_tbl> yearList = new List<year_tbl>();
                var yearAdd = db.year_tbl.ToList();
                foreach (var item in yearAdd)
                {
                    yearList.Add(new year_tbl
                    {
                        Year_Id = item.Year_Id,
                        Year = item.Year
                    });
                }
                ViewBag.year = new SelectList(yearAdd, "Year_Id", "Year");

                List<course_tbl> courseList = new List<course_tbl>();
                var courseAdd = db.course_tbl.ToList();
                foreach (var item in courseAdd)
                {
                    courseList.Add(new course_tbl
                    {
                        course_id = item.course_id,
                        course_name = item.course_name
                    });
                }

                ViewBag.Course = new SelectList(courseList, "course_id", "course_name");

                List<hostel_tbl> hostelList = new List<hostel_tbl>();
                var hostelAdd = db.hostel_tbl.ToList();
                foreach (var item in hostelAdd)
                {
                    hostelList.Add(new hostel_tbl
                    {
                        hostel_id = item.hostel_id,
                        hostel_name = item.hostel_name
                    });
                }
                ViewBag.Hostel = new SelectList(hostelList, "hostel_id", "hostel_name");

                List<semester_tbl> semList = new List<semester_tbl>();
                var semAdd = db.semester_tbl.ToList();
                foreach (var item in semAdd)
                {
                    semList.Add(new semester_tbl
                    {
                        Sem_Id = item.Sem_Id,
                        Semester_Name = item.Semester_Name
                    });
                }
                ViewBag.semester = new SelectList(semList, "Sem_Id", "Semester_Name");

                List<department_tbl> deptList = new List<department_tbl>();
                var deptAdd = db.department_tbl.ToList();
                foreach (var item in deptAdd)
                {
                    deptList.Add(new department_tbl
                    {
                        DeptId = item.DeptId,
                        DeptName = item.DeptName
                    });
                }
                ViewBag.department = new SelectList(deptList, "DeptId", "DeptName");

            }
        }
        public ActionResult Logout()
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            TempData["LogoutToast"] = "Logged Out";
            return RedirectToAction("Index" , "LoginTeacher");
        }
        [Authorize(Roles = "Teacher")]
        public ActionResult ViewSchedule()
        {
            using (AttendanceTrackrEntities db = new AttendanceTrackrEntities())
            {

                return View();
            }
            
        }

        [Authorize(Roles = "Teacher")]
        public ActionResult StudentDetails()
        {
            using (AttendanceTrackrEntities db = new AttendanceTrackrEntities())
            {
                var stuDetails = db.student_tbl.ToList();
                return View(stuDetails);
            }

        }
        [Authorize(Roles = "Teacher")]
        public ActionResult AttendanceStatus()
        {
            using (AttendanceTrackrEntities db = new AttendanceTrackrEntities())
            {
                //var attendanceData = db.AttendanceTbls
                //                       .Include(a => a.StudentTbl).ToList();


                var attendanceData = (from att in db.AttendanceTbls
                                      join stu in db.student_tbl on att.student_id equals stu.student_id
                                      select new AttendanceStudentVM
                                      {
                                          // Attendance properties
                                          attendance_id = att.attendance_id,
                                          Att_student_id = att.student_id,
                                          isPresent = att.isPresent,
                                          attendance_date = att.attendance_date,
                                          marked_by = att.marked_by,

                                          // Student properties
                                          student_id = stu.student_id,
                                          Name = stu.Name,
                                          Address = stu.Address,
                                          City = stu.City,
                                          State = stu.State,
                                          Pincode = stu.Pincode,
                                          Mobile = stu.Mobile,
                                          Email = stu.Email,
                                          Student_Type = stu.Student_Type,
                                          FK_Dept_Id = stu.FK_Dept_Id,
                                          DOB = stu.DOB,
                                          FK_Collage_Id = stu.FK_Collage_Id,
                                          FK_Year_Id = stu.FK_Year_Id,
                                          FK_Sem_Id = stu.FK_Sem_Id,
                                          FK_Course_Id = stu.FK_Course_Id,
                                          FK_Hostel_Id = stu.FK_Hostel_Id,
                                          Created_date = stu.Created_date,
                                          Created_by = stu.Created_by,
                                          Password = stu.Password,
                                          Id = stu.Id
                                      }).ToList();

                return View(attendanceData);
            }
            
        }



        [Authorize(Roles = "Teacher")]
        public ActionResult MarkAttendance()
        {
            using (AttendanceTrackrEntities db = new AttendanceTrackrEntities())
            {
                var students = db.student_tbl.ToList();
                return View(students);
            }
        }
        [HttpPost]
        public ActionResult SaveAttendance(int student_id, bool isPresent)
        {
            using (AttendanceTrackrEntities db = new AttendanceTrackrEntities())
            {
                AttendanceTbl attendance = new AttendanceTbl
                {
                    student_id = student_id,
                    attendance_date = DateTime.Now,
                    isPresent = isPresent,
                    marked_by = "Lalit"
                };
                db.AttendanceTbls.Add(attendance);
                db.SaveChanges();
            }

            return RedirectToAction("MarkAttendance");
        }

        [Authorize(Roles = "Student")]
        public ActionResult AuthStudentDetails()
        {
            using (AttendanceTrackrEntities db = new AttendanceTrackrEntities())
            {
                var result = db.student_tbl.ToList();
               
                return View(result);
            }

        }

        [Authorize(Roles = "Student")]
        public ActionResult StudentAttendanceStatus()
        {
            using (AttendanceTrackrEntities db = new AttendanceTrackrEntities())
            {
                //var attendanceData = db.AttendanceTbls
                //                       .Include(a => a.StudentTbl).ToList();

                var studentId = (int)Session["studentId"];
                var attendanceData = (from att in db.AttendanceTbls
                                      join stu in db.student_tbl on att.student_id equals stu.student_id
                                      select new AttendanceStudentVM
                                      {
                                          // Attendance properties
                                          attendance_id = att.attendance_id,
                                          Att_student_id = att.student_id,
                                          isPresent = att.isPresent,
                                          attendance_date = att.attendance_date,
                                          marked_by = att.marked_by,

                                          // Student properties
                                          student_id = stu.student_id,
                                          Name = stu.Name,
                                          Address = stu.Address,
                                          City = stu.City,
                                          State = stu.State,
                                          Pincode = stu.Pincode,
                                          Mobile = stu.Mobile,
                                          Email = stu.Email,
                                          Student_Type = stu.Student_Type,
                                          FK_Dept_Id = stu.FK_Dept_Id,
                                          DOB = stu.DOB,
                                          FK_Collage_Id = stu.FK_Collage_Id,
                                          FK_Year_Id = stu.FK_Year_Id,
                                          FK_Sem_Id = stu.FK_Sem_Id,
                                          FK_Course_Id = stu.FK_Course_Id,
                                          FK_Hostel_Id = stu.FK_Hostel_Id,
                                          Created_date = stu.Created_date,
                                          Created_by = stu.Created_by,
                                          Password = stu.Password,
                                          Id = stu.Id
                                      }).Where(x => x.student_id == studentId).ToList();

                return View(attendanceData);
            }

        }

        
    }
}