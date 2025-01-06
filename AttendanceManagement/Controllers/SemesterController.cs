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
    public class SemesterController : Controller
    {
        private AttendanceTrackrEntities db = new AttendanceTrackrEntities();

        // GET: Semester
        public ActionResult Index()
        {
            return View(db.semester_tbl.ToList());
        }

        // GET: Semester/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            semester_tbl semester_tbl = db.semester_tbl.Find(id);
            if (semester_tbl == null)
            {
                return HttpNotFound();
            }
            return View(semester_tbl);
        }

        // GET: Semester/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Semester/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Sem_Id,Semester_Name,Created_date,Created_by")] semester_tbl semester_tbl)
        {
            if (ModelState.IsValid)
            {
                db.semester_tbl.Add(semester_tbl);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(semester_tbl);
        }

        // GET: Semester/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            semester_tbl semester_tbl = db.semester_tbl.Find(id);
            if (semester_tbl == null)
            {
                return HttpNotFound();
            }
            return View(semester_tbl);
        }

        // POST: Semester/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Sem_Id,Semester_Name,Created_date,Created_by")] semester_tbl semester_tbl)
        {
            if (ModelState.IsValid)
            {
                db.Entry(semester_tbl).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(semester_tbl);
        }

        // GET: Semester/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            semester_tbl semester_tbl = db.semester_tbl.Find(id);
            if (semester_tbl == null)
            {
                return HttpNotFound();
            }
            return View(semester_tbl);
        }

        // POST: Semester/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            semester_tbl semester_tbl = db.semester_tbl.Find(id);
            db.semester_tbl.Remove(semester_tbl);
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
