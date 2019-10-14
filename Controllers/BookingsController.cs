using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using RentAndCycleCodeFirst.Models;

namespace RentAndCycleCodeFirst.Controllers
{
    [Authorize]
    public class BookingsController : Controller
    {
        private BikeDbContext db = new BikeDbContext();

        // GET: Bookings
        public ActionResult Index()
        {
            var bookings = db.Bookings.Include(b => b.CompanyBike);
            return View(bookings.ToList());
        }

        // GET: Bookings/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Booking booking = db.Bookings.Find(id);
        //    if (booking == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(booking);
        //}

        // GET: Bookings/Create
        public ActionResult Create(int? companyBikeId, string sDate, string eDate)
        {
            if (companyBikeId == null || sDate == null || eDate == null)
            {
                return HttpNotFound();
            }

            try
            {
                DateTime startDate = DateTime.ParseExact(sDate, "d/M/yyyy", CultureInfo.InvariantCulture);
                DateTime endDate = DateTime.ParseExact(eDate, "d/M/yyyy", CultureInfo.InvariantCulture);

                if (startDate >= endDate)
                {
                    return HttpNotFound();
                }

                if (startDate <= DateTime.Today)
                {
                    return HttpNotFound();
                }

                // check the num of days of booking, max: 7
                if ((endDate - startDate).TotalDays > 7)
                {
                    return HttpNotFound();
                }

                CompanyBike companyBike = db.CompanyBikes.Find(companyBikeId);
                if (companyBike == null)
                {
                    return HttpNotFound();
                }

                Booking booking = new Booking();
                booking.CompanyBike = companyBike;
                booking.StartDate = startDate;
                booking.EndDate = endDate;
                booking.UserId = User.Identity.GetUserId();

                ViewBag.TotalPrice = companyBike.Price * (endDate - startDate).TotalDays;

                return View(booking);

            }
            catch (System.FormatException e)
            {
                return HttpNotFound();
            }
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,StartDate,EndDate,UserId,CompanyBikeId")] Booking booking)
        {
            //UpdateModel(booking);
            if (ModelState.IsValid)
            {
                // check the num of days of booking, max: 7
                if ((booking.EndDate - booking.StartDate).TotalDays > 7)
                {
                    return HttpNotFound();
                }

                // check if the user creates data under their id
                if (booking.UserId != User.Identity.GetUserId())
                {
                    return HttpNotFound();
                }

                if (booking.StartDate >= booking.EndDate)
                {
                    return HttpNotFound();
                }

                if (booking.StartDate <= DateTime.Today)
                {
                    return HttpNotFound();
                }

                var bookings = db.Bookings;
                foreach (var b in bookings.Where(c => c.CompanyBikeId == booking.CompanyBikeId && c.EndDate > DateTime.Today))
                {
                    if (booking.EndDate >= b.StartDate && booking.StartDate <= b.EndDate)
                    {
                        return HttpNotFound();
                    }
                }
                db.Bookings.Add(booking);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return HttpNotFound();
        }

        // GET: Bookings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,StartDate,EndDate,UserId,CompanyBikeId,Feedback")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                if (booking.Feedback < 1 || booking.Feedback > 5)
                {
                    return View(booking);
                }
                db.Entry(booking).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(booking);
        }

        //// GET: Bookings/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Booking booking = db.Bookings.Find(id);
        //    if (booking == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(booking);
        //}

        //// POST: Bookings/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Booking booking = db.Bookings.Find(id);
        //    db.Bookings.Remove(booking);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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
