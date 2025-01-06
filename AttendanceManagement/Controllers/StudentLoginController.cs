using AttendanceManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AttendanceManagement.Controllers
{
    public class StudentLoginController : Controller
    {
        // GET: StudentLogin
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(student_tbl t)
        {
            using (AttendanceTrackrEntities db = new AttendanceTrackrEntities())
            {
                var credentials = db.student_tbl.Where(x => x.student_id == t.student_id && x.Password == t.Password).FirstOrDefault();
                if (credentials != null)
                {
                    Session["name"] = credentials.Name;
                    return RedirectToAction("Index", "Dashboard");
                }
                else
                {
                    //Session["name"] = credentials.;
                    return RedirectToAction("Create", "StudentSignUp");
                }

            }
            
        }
    }
}