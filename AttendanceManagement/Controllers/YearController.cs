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
    public class YearController : Controller
    {
        private AttendanceTrackrEntities db = new AttendanceTrackrEntities();

        // GET: Year
        public ActionResult Index()
        {
            return View(db.year_tbl.ToList());
        }

        // GET: Year/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            year_tbl year_tbl = db.year_tbl.Find(id);
            if (year_tbl == null)
            {
                return HttpNotFound();
            }
            return View(year_tbl);
        }

        // GET: Year/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Year/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Year_Id,Year,Created_date,Created_by")] year_tbl year_tbl)
        {
            if (ModelState.IsValid)
            {
                db.year_tbl.Add(year_tbl);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(year_tbl);
        }

        // GET: Year/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            year_tbl year_tbl = db.year_tbl.Find(id);
            if (year_tbl == null)
            {
                return HttpNotFound();
            }
            return View(year_tbl);
        }

        // POST: Year/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Year_Id,Year,Created_date,Created_by")] year_tbl year_tbl)
        {
            if (ModelState.IsValid)
            {
                db.Entry(year_tbl).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(year_tbl);
        }

        // GET: Year/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            year_tbl year_tbl = db.year_tbl.Find(id);
            if (year_tbl == null)
            {
                return HttpNotFound();
            }
            return View(year_tbl);
        }

        // POST: Year/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            year_tbl year_tbl = db.year_tbl.Find(id);
            db.year_tbl.Remove(year_tbl);
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
