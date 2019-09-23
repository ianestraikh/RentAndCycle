using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RentAndCycleCodeFirst.Models;

namespace RentAndCycleCodeFirst.Controllers
{
    public class CompanyLocationsController : Controller
    {
        private BikeDbContext db = new BikeDbContext();

        // GET: CompanyLocations
        public ActionResult Index()
        {
            var companyLocations = db.CompanyLocations.Include(c => c.Company);
            return View(companyLocations.ToList());
        }

        // GET: CompanyLocations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyLocation companyLocation = db.CompanyLocations.Find(id);
            if (companyLocation == null)
            {
                return HttpNotFound();
            }
            return View(companyLocation);
        }

        // GET: CompanyLocations/Create
        public ActionResult Create()
        {
            ViewBag.CompanyId = new SelectList(db.Companies, "Id", "Name");
            return View();
        }

        // POST: CompanyLocations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CompanyId,Street,Town,Postcode,Lat,Lon,ImageFilename")] CompanyLocation companyLocation)
        {
            if (ModelState.IsValid)
            {
                db.CompanyLocations.Add(companyLocation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CompanyId = new SelectList(db.Companies, "Id", "Name", companyLocation.CompanyId);
            return View(companyLocation);
        }

        // GET: CompanyLocations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyLocation companyLocation = db.CompanyLocations.Find(id);
            if (companyLocation == null)
            {
                return HttpNotFound();
            }
            ViewBag.CompanyId = new SelectList(db.Companies, "Id", "Name", companyLocation.CompanyId);
            return View(companyLocation);
        }

        // POST: CompanyLocations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CompanyId,Street,Town,Postcode,Lat,Lon,ImageFilename")] CompanyLocation companyLocation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(companyLocation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CompanyId = new SelectList(db.Companies, "Id", "Name", companyLocation.CompanyId);
            return View(companyLocation);
        }

        // GET: CompanyLocations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyLocation companyLocation = db.CompanyLocations.Find(id);
            if (companyLocation == null)
            {
                return HttpNotFound();
            }
            return View(companyLocation);
        }

        // POST: CompanyLocations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CompanyLocation companyLocation = db.CompanyLocations.Find(id);
            db.CompanyLocations.Remove(companyLocation);
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
