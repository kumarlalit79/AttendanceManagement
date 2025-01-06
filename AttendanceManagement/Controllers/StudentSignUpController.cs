using AttendanceManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AttendanceManagement.Controllers
{
    public class StudentSignUpController : Controller
    {
        // GET: SignUp
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            PopulateDropDownLists();
            return View(new student_tbl());
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

        [HttpPost]
        public ActionResult Create(student_tbl s)
        {
            using (AttendanceTrackrEntities db = new AttendanceTrackrEntities())
            {
                s.Created_date = DateTime.Now;
                s.Created_by = "Gaj";
                db.student_tbl.Add(s);
                int a = db.SaveChanges();
                if(a > 0)
                {
                    TempData["SignUpMsg"] = "Sign Up Successfully, now you can log in";
                    ModelState.Clear();
                }
                
            }

            PopulateDropDownLists();
            return View(s);
        }
    }
}