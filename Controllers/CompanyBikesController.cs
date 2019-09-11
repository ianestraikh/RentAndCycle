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
    public class CompanyBikesController : Controller
    {
        private BikeDbContext db = new BikeDbContext();

        // GET: CompanyBikes
        public ActionResult Index()
        {
            var companyBikes = db.CompanyBikes.Include(c => c.Bike).Include(c => c.CompanyLocation);
            return View(companyBikes.ToList());
        }

        // GET: CompanyBikes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyBike companyBike = db.CompanyBikes.Find(id);
            if (companyBike == null)
            {
                return HttpNotFound();
            }
            return View(companyBike);
        }

        // GET: CompanyBikes/Create
        public ActionResult Create()
        {
            ViewBag.BikeId = new SelectList(db.Bikes, "Id", "Model");
            ViewBag.CompanyLocationId = new SelectList(db.CompanyLocations, "Id", "Street");
            return View();
        }

        // POST: CompanyBikes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Count,BikeId,CompanyLocationId")] CompanyBike companyBike)
        {
            if (ModelState.IsValid)
            {
                db.CompanyBikes.Add(companyBike);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BikeId = new SelectList(db.Bikes, "Id", "Model", companyBike.BikeId);
            ViewBag.CompanyLocationId = new SelectList(db.CompanyLocations, "Id", "Street", companyBike.CompanyLocationId);
            return View(companyBike);
        }

        // GET: CompanyBikes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyBike companyBike = db.CompanyBikes.Find(id);
            if (companyBike == null)
            {
                return HttpNotFound();
            }
            ViewBag.BikeId = new SelectList(db.Bikes, "Id", "Model", companyBike.BikeId);
            ViewBag.CompanyLocationId = new SelectList(db.CompanyLocations, "Id", "Street", companyBike.CompanyLocationId);
            return View(companyBike);
        }

        // POST: CompanyBikes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Count,BikeId,CompanyLocationId")] CompanyBike companyBike)
        {
            if (ModelState.IsValid)
            {
                db.Entry(companyBike).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BikeId = new SelectList(db.Bikes, "Id", "Model", companyBike.BikeId);
            ViewBag.CompanyLocationId = new SelectList(db.CompanyLocations, "Id", "Street", companyBike.CompanyLocationId);
            return View(companyBike);
        }

        // GET: CompanyBikes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyBike companyBike = db.CompanyBikes.Find(id);
            if (companyBike == null)
            {
                return HttpNotFound();
            }
            return View(companyBike);
        }

        // POST: CompanyBikes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CompanyBike companyBike = db.CompanyBikes.Find(id);
            db.CompanyBikes.Remove(companyBike);
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
