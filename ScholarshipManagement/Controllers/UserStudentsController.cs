using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjectAlpha.Models;

namespace ProjectAlpha.Controllers
{
    public class UserStudentsController : Controller
    {
        private ModelDbContext db = new ModelDbContext();

        // GET: UserStudents
        public ActionResult Index()
        {
            return View(db.UserStudents.ToList());
        }

        // GET: UserStudents/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserStudent userStudent = db.UserStudents.Find(id);
            if (userStudent == null)
            {
                return HttpNotFound();
            }
            return View(userStudent);
        }

        // GET: UserStudents/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserStudents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserID,UserName,Email,Password")] UserStudent userStudent)
        {
            if (ModelState.IsValid)
            {
                userStudent.UserID = Guid.NewGuid();
                db.UserStudents.Add(userStudent);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(userStudent);
        }

        // GET: UserStudents/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserStudent userStudent = db.UserStudents.Find(id);
            if (userStudent == null)
            {
                return HttpNotFound();
            }
            return View(userStudent);
        }

        // POST: UserStudents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserID,UserName,Email,Password")] UserStudent userStudent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userStudent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userStudent);
        }

        // GET: UserStudents/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserStudent userStudent = db.UserStudents.Find(id);
            if (userStudent == null)
            {
                return HttpNotFound();
            }
            return View(userStudent);
        }

        // POST: UserStudents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            UserStudent userStudent = db.UserStudents.Find(id);
            db.UserStudents.Remove(userStudent);
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
