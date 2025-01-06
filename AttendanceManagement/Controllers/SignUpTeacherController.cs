using AttendanceManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AttendanceManagement.Controllers
{
    public class SignUpTeacherController : Controller
    {
        // GET: SignUpTeacher
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create()
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

        [HttpPost]
        public ActionResult Create(Teacher_tbl f)
        {
            using(AttendanceTrackrEntities db = new AttendanceTrackrEntities())
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
            }
            return View(f);
        }
    }
}