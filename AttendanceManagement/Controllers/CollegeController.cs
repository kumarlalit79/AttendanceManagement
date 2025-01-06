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
    public class CollegeController : Controller
    {
        private AttendanceTrackrEntities db = new AttendanceTrackrEntities();

        // GET: College
        public ActionResult Index()
        {
            return View(db.college_tbl.ToList());
        }

        // GET: College/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            college_tbl college_tbl = db.college_tbl.Find(id);
            if (college_tbl == null)
            {
                return HttpNotFound();
            }
            return View(college_tbl);
        }

        // GET: College/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: College/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Col_id,Name,Address,City,State,Pincode,Created_date,Created_by")] college_tbl college_tbl)
        {
            if (ModelState.IsValid)
            {
                db.college_tbl.Add(college_tbl);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(college_tbl);
        }

        // GET: College/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            college_tbl college_tbl = db.college_tbl.Find(id);
            if (college_tbl == null)
            {
                return HttpNotFound();
            }
            return View(college_tbl);
        }

        // POST: College/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Col_id,Name,Address,City,State,Pincode,Created_date,Created_by")] college_tbl college_tbl)
        {
            if (ModelState.IsValid)
            {
                db.Entry(college_tbl).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(college_tbl);
        }

        // GET: College/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            college_tbl college_tbl = db.college_tbl.Find(id);
            if (college_tbl == null)
            {
                return HttpNotFound();
            }
            return View(college_tbl);
        }

        // POST: College/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            college_tbl college_tbl = db.college_tbl.Find(id);
            db.college_tbl.Remove(college_tbl);
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
