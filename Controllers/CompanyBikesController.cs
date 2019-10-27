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

        public ActionResult Index(int? id)
        {
            var companyBikes = db.CompanyBikes.Include(c => c.Bike).Include(c => c.CompanyLocation);
            var model = new CompanyBikeViewModel();
            var bookings = db.Bookings.ToList();
            // set up feedback values
            foreach (var companyBike in companyBikes.ToList())
            {
                int count = 0;
                int sum = 0;
                foreach(var b in bookings)
                {
                    if (b.CompanyBikeId == companyBike.Id)
                    {
                        if (b.Feedback != null)
                        {
                            count += 1;
                            sum += b.Feedback ?? 0;
                        }
                    }
                }
                if (count != 0 && sum !=0)
                {
                    companyBike.Rating = Math.Round(sum / (double) count,1);
                }
            }
            if (id != null)
            {
                companyBikes = companyBikes.Where(c => c.CompanyLocationId == id);
                if (companyBikes.Count() == 0)
                {
                    return HttpNotFound();
                }
                model.CompanyBikes = companyBikes.ToList();
                return View(model);
            }
            model.CompanyBikes = companyBikes.ToList();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(CompanyBikeViewModel model)
        {
            if (ModelState.IsValid)
            {
                var notBookedCompanyBikes = new List<CompanyBike>();
                var bookings = db.Bookings.Where(c => c.EndDate > DateTime.Today).ToList();
                foreach (var companyBike in model.CompanyBikes)
                {
                    var isNotBooked = true;
                    foreach (var booking in bookings)
                    {
                        if (companyBike.Id == booking.CompanyBikeId)
                        {
                            if (model.EndDate >= booking.StartDate && model.StartDate <= booking.EndDate)
                            {
                                isNotBooked = false;
                            }
                        }
                    }
                    if (isNotBooked)
                    {
                        notBookedCompanyBikes.Add(companyBike);
                    }
                }
                model.CompanyBikes = notBookedCompanyBikes;
                model.IsValid = true;
            }
            return View(model);
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
        [Authorize(Roles = "Admin")]
        public ActionResult Create(int companyLocationId)
        {
            CompanyLocation companyLocation = db.CompanyLocations.Find(companyLocationId);
            if (companyLocation == null)
            {
                return HttpNotFound();
            }
            ViewBag.BikeId = new SelectList(db.Bikes, "Id", "BikeModel");
            var bike = new CompanyBike();
            bike.CompanyLocationId = companyLocationId;
            return View(bike);
        }

        // POST: CompanyBikes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Count,Price,BikeId,CompanyLocationId")] CompanyBike companyBike)
        {
            if (ModelState.IsValid)
            {
                db.CompanyBikes.Add(companyBike);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BikeId = new SelectList(db.Bikes, "Id", "BikeModel", companyBike.BikeId);
            return View(companyBike);
        }

        // GET: CompanyBikes/Edit/5
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
            if (db.Bookings.Where(b => b.CompanyBikeId == id).ToList().Count > 0)
            {
                ViewBag.Result = "The bike is used in bookings and cannot be deleted.";
                return View(companyBike);
            }
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
