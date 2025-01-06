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
    public class DepartmentController : Controller
    {
        private AttendanceTrackrEntities db = new AttendanceTrackrEntities();

        // GET: Department
        public ActionResult Index()
        {
            return View(db.department_tbl.ToList());
        }

        // GET: Department/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            department_tbl department_tbl = db.department_tbl.Find(id);
            if (department_tbl == null)
            {
                return HttpNotFound();
            }
            return View(department_tbl);
        }

        // GET: Department/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Department/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DeptId,DeptName,Created_date,Created_by")] department_tbl department_tbl)
        {
            if (ModelState.IsValid)
            {
                db.department_tbl.Add(department_tbl);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(department_tbl);
        }

        // GET: Department/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            department_tbl department_tbl = db.department_tbl.Find(id);
            if (department_tbl == null)
            {
                return HttpNotFound();
            }
            return View(department_tbl);
        }

        // POST: Department/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DeptId,DeptName,Created_date,Created_by")] department_tbl department_tbl)
        {
            if (ModelState.IsValid)
            {
                db.Entry(department_tbl).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(department_tbl);
        }

        // GET: Department/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            department_tbl department_tbl = db.department_tbl.Find(id);
            if (department_tbl == null)
            {
                return HttpNotFound();
            }
            return View(department_tbl);
        }

        // POST: Department/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            department_tbl department_tbl = db.department_tbl.Find(id);
            db.department_tbl.Remove(department_tbl);
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
