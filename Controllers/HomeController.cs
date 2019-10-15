using RentAndCycleCodeFirst.Models;
using RentAndCycleCodeFirst.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RentAndCycleCodeFirst.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            return View(new SendEmailViewModel());
        }

        [HttpPost]
        public ActionResult Contact(SendEmailViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string fromEmail = model.FromEmail;
                    string subject = model.Subject;
                    string contents = model.Contents;

                    EmailSender es = new EmailSender();
                    var toAdminEmail = "iest0002@student.monash.edu";
                    es.SendAsync(fromEmail, new List<string> { toAdminEmail}, subject, contents);

                    ViewBag.Result = "Email has been send.";

                    ModelState.Clear();

                    return View(new SendEmailViewModel());
                }
                catch
                {
                    return View();
                }
            }

            return View();
        }

        // Return a list of numbers of bikes, that are booked for a day, for a week
        [HttpGet]
        public ActionResult GetNumberOfBikes()
        {
            using (BikeDbContext db = new BikeDbContext())
            {
                var bookings = db.Bookings.Where(b => b.EndDate >= DateTime.Today).ToList();
                int[] nums = new int[7];
                for (int i = 0; i < 7; i++)
                {
                    var count = 0;
                    foreach (var b in bookings)
                    {
                        var date = DateTime.Now.AddDays(i).Date;
                        if (b.StartDate.Date <= date && b.EndDate.Date >= date)
                        {
                            count++;
                        }
                    }
                    nums[i] = count;
                }
                return Json(nums, JsonRequestBehavior.AllowGet);
            }
        }
    }
}