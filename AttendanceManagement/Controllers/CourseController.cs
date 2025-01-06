using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AttendanceManagement.Models;

namespace AttendanceManagement.Controllers
{
    public class CourseController : Controller
    {
        private AttendanceTrackrEntities db = new AttendanceTrackrEntities();

        // GET: Course
        public ActionResult Index()
        {
            return View(db.course_tbl.ToList());
        }

        // GET: Course/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            course_tbl course_tbl = db.course_tbl.Find(id);
            if (course_tbl == null)
            {
                return HttpNotFound();
            }
            return View(course_tbl);
        }

        // GET: Course/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Course/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "course_id,course_name,Created_date,Created_by")] course_tbl course_tbl)
        {
            if (ModelState.IsValid)
            {
                db.course_tbl.Add(course_tbl);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(course_tbl);
        }

        // GET: Course/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            course_tbl course_tbl = db.course_tbl.Find(id);
            if (course_tbl == null)
            {
                return HttpNotFound();
            }
            return View(course_tbl);
        }

        // POST: Course/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "course_id,course_name,Created_date,Created_by")] course_tbl course_tbl)
        {
            if (ModelState.IsValid)
            {
                db.Entry(course_tbl).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(course_tbl);
        }

        // GET: Course/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            course_tbl course_tbl = db.course_tbl.Find(id);
            if (course_tbl == null)
            {
                return HttpNotFound();
            }
            return View(course_tbl);
        }

        // POST: Course/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            course_tbl course_tbl = db.course_tbl.Find(id);
            db.course_tbl.Remove(course_tbl);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
