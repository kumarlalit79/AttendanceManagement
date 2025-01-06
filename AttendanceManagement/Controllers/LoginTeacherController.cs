using AttendanceManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace AttendanceManagement.Controllers
{
    public class LoginTeacherController : Controller
    {
        // GET: LoginTeacher
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(LoginViewModel login , string returnUrl)
        {
            
            using (AttendanceTrackrEntities db = new AttendanceTrackrEntities())
            {
                //return RedirectToAction("StudentDashboard", "Dashboard");
                List<string> roles = new List<string>();
                if (login.UserType == "Teacher")
                {
                    var TeacherCredentials = db.Teacher_tbl.Where(x => x.Faculty_Id.ToString() == login.UserId.ToString() && x.Password == login.Password).FirstOrDefault();
                    if (TeacherCredentials != null)
                    {
                        FormsAuthentication.SetAuthCookie(login.UserId.ToString(), false);
                        Session["name"] = TeacherCredentials.Name;
                        Session["role"] = "Teacher";
                        roles.Add("Teacher");

                        Session["UserRoles"] = roles.ToArray();

                        if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                        {
                            return Redirect(returnUrl);
                        }
                        TempData["LoggedInToast"] = "Logged in successfully";
                        return RedirectToAction("TeacherDashboard", "Dashboard");
                    }
                }
                
                else if(login.UserType == "Student")
                {
                    var StudentCredentials = db.student_tbl.Where(x => x.student_id == login.UserId && x.Password == login.Password).FirstOrDefault();
                    if (StudentCredentials != null)
                    {
                        FormsAuthentication.SetAuthCookie(login.UserId.ToString(), false);
                        Session["studentId"] = StudentCredentials.student_id;
                        Session["name"] = StudentCredentials.Name;
                        Session["role"] = "Student";
                        roles.Add("Student");

                        Session["UserRoles"] = roles.ToArray();

                        if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                        {
                            return Redirect(returnUrl);
                        }

                        return RedirectToAction("StudentDashboard", "Dashboard");
                    }
                }

                else if (login.UserType == "Admin")
                {
                    var adminCredentials = db.Admins
                        .Where(x => x.Admin_ID == login.UserId && x.Password == login.Password)
                        .FirstOrDefault();
                    if (adminCredentials != null)
                    {
                        FormsAuthentication.SetAuthCookie(login.UserId.ToString(), false);
                        Session["adminId"] = adminCredentials.Admin_ID;
                        Session["role"] = "Admin";
                        roles.Add("Admin");
                        Session["UserRoles"] = roles.ToArray();
                        Session["collegeId"] = adminCredentials.FK_College_Id;

                        return RedirectToAction("AdminDashBoard", "Dashboard");
                    }
                }

                ViewBag.Message = "Invalid";

                
            }
            return View();
        }
    }
}