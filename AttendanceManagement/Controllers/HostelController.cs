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
    public class HostelController : Controller
    {
        private AttendanceTrackrEntities db = new AttendanceTrackrEntities();

        // GET: Hostel
        public ActionResult Index()
        {
            return View(db.hostel_tbl.ToList());
        }

        // GET: Hostel/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            hostel_tbl hostel_tbl = db.hostel_tbl.Find(id);
            if (hostel_tbl == null)
            {
                return HttpNotFound();
            }
            return View(hostel_tbl);
        }

        // GET: Hostel/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Hostel/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "hostel_id,hostel_name,Created_date,Created_by")] hostel_tbl hostel_tbl)
        {
            if (ModelState.IsValid)
            {
                db.hostel_tbl.Add(hostel_tbl);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(hostel_tbl);
        }

        // GET: Hostel/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            hostel_tbl hostel_tbl = db.hostel_tbl.Find(id);
            if (hostel_tbl == null)
            {
                return HttpNotFound();
            }
            return View(hostel_tbl);
        }

        // POST: Hostel/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "hostel_id,hostel_name,Created_date,Created_by")] hostel_tbl hostel_tbl)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hostel_tbl).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hostel_tbl);
        }

        // GET: Hostel/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            hostel_tbl hostel_tbl = db.hostel_tbl.Find(id);
            if (hostel_tbl == null)
            {
                return HttpNotFound();
            }
            return View(hostel_tbl);
        }

        // POST: Hostel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            hostel_tbl hostel_tbl = db.hostel_tbl.Find(id);
            db.hostel_tbl.Remove(hostel_tbl);
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
