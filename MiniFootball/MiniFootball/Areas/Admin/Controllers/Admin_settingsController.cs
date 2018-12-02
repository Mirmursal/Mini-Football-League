using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MiniFootball.Models;
using System.Web.Helpers;

namespace MiniFootball.Areas.Admin.Controllers
{
    public class Admin_settingsController : Controller
    {
        private Mini_FootballEntities db = new Mini_FootballEntities();

        // GET: Admin/Admin_settings
        public ActionResult Index()
        {
            return View(db.Admin_settings.ToList());
        }

        // GET: Admin/Admin_settings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin_settings admin_settings = db.Admin_settings.Find(id);
            if (admin_settings == null)
            {
                return HttpNotFound();
            }
            return View(admin_settings);
        }

        // GET: Admin/Admin_settings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin_settings admin_settings = db.Admin_settings.Find(id);
            if (admin_settings == null)
            {
                return HttpNotFound();
            }
            return View(admin_settings);
        }

        // POST: Admin/Admin_settings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Name,Lastname,Email,Password")] Admin_settings admin_settings)
        {
            admin_settings.Password = Crypto.HashPassword(admin_settings.Password);
            if (ModelState.IsValid)
            {
                db.Entry(admin_settings).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(admin_settings);
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
