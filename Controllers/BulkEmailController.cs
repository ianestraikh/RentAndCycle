using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;
using RentAndCycleCodeFirst.Models;
using RentAndCycleCodeFirst.Utils;

namespace RentAndCycleCodeFirst.Controllers
{
    public class BulkEmailController : Controller
    {
        public ActionResult GetEmails()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var emails = db.Users.ToList().FindAll(
                delegate (ApplicationUser user)
                {
                    var isAdmin = false;
                    foreach (IdentityUserRole role in user.Roles)
                    {
                        if (db.Roles.Find(role.RoleId).Name.Equals("Admin"))
                        {
                            isAdmin = true;
                        }
                    }
                    return !isAdmin;
                }).Select(u => new
                {
                    email = u.Email
                });
                return Json(emails, JsonRequestBehavior.AllowGet);
            }
        }

        // GET
        public ActionResult Index()
        {
            return View(new BulkEmailViewModel());
        }

        [HttpPost]
        public ActionResult Index(BulkEmailViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string emails = model.ToEmails;
                    string subject = model.Subject;
                    string contents = model.Contents;

                    var toEmails = new List<string>();
                    foreach (string email in emails.Split(';'))
                    {
                        toEmails.Add(email);
                    }

                    EmailSender es = new EmailSender();
                    es.Send("admin@rentandcycle.com", toEmails, subject, contents, model.File.InputStream);

                    ViewBag.Result = "Email has been send.";

                    ModelState.Clear();

                    return View(new BulkEmailViewModel());
                }
                catch
                {
                    return View();
                }
            }

            return View();
        }
    }
}
