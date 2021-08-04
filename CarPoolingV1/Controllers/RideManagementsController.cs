using CarPool.Models;
using CarPool.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarPool.Controllers
{
    public class RideManagementsController : Controller
    {
        private RideDbContext db = new RideDbContext();
        private RideManagementRepository repository = new RideManagementRepository();

       

        // GET: RideManagements
        [HttpGet]
        public ActionResult Index()
        {
            //return Booked Rides of the current user
            if (Session["userIdentity"] == null)
            {
                return RedirectToAction("Login", "Users");
            }
            string userDetails = (string)Session["userIdentity"];
            return View(repository.GetBookedRides(userDetails));
        } 
        public ActionResult GetAllRides()
        {
            //return Booked Rides of the current user
            if (Session["userIdentity"] == null)
            {
                return RedirectToAction("Login", "Users");
            }
            string userDetails = (string)Session["userIdentity"];
            return View(db.RideManagements.Where(x=>x.User.Email == userDetails).ToList());
        }

        public ActionResult RideManagement(string id)
        {
            List<RideManagement> rideManagements = repository.GetManagementByRider("id");

            return View(rideManagements);
        }

        // GET: RideManagements/Details/5
        public ActionResult Details(string id)
        {
            throw new NotImplementedException(); ;
        }

        /*// GET: RideManagements/Create
        [HttpGet]
        public ActionResult Create(string id)
        {
            *//*if(Session["userIdentity"] == null)
            {
                return RedirectToAction("Login", "Users");
            }
            string userDetails = (string)Session["userIdentity"];
            User user = db.Users.Find(userDetails);
            Ride ride = db.Rides.Find(id);
            RideManagement rideManagement = new RideManagement();*//*
            return View();
           

        }*/

        public ActionResult CreateRideManagement(string id)
        {
            if (Session["userIdentity"] == null)
            {
                return RedirectToAction("Login", "Users");
            }
            string userDetails = (string)Session["userIdentity"];

            RideManagement rideManagement = new RideManagement();
            rideManagement.RideCode = id;
            rideManagement.Email = userDetails;
            rideManagement.BookedBy = userDetails;
            rideManagement.RideManagementID = Guid.NewGuid();
            rideManagement.BookedDate = DateTime.Now;
            rideManagement.CompletedDate = null;
            
            repository.AddRideManagement(rideManagement);


            
            repository.Dispose();
            return RedirectToAction("MarkBooked", new { id = rideManagement.RideCode});
        }


        // GET: RideManagements/Edit/5
        public ActionResult Edit(string id)
        {
            if (Session["userIdentity"] == null)
            {
                return RedirectToAction("Login", "Users");
            }
            RideManagement rideManagement = db.RideManagements.Find(id);
            if(rideManagement != null)
            {
                return View(rideManagement);
            }
            ViewBag.Message = "No user found";
            return View();
        }
        // POST: RideManagements/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RideCode,Description,CreatedBy,CreatedDate,RideDate,SeatCount,BookedCount,Status(string),")] RideManagement rideManagement)
        {
            db.Entry(rideManagement).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult MarkBooked(string id)
        {
            RideManagement rM = db.RideManagements.FirstOrDefault(z => z.RideCode == id);
            repository.MarkBooked(rM.RideCode, rM.Email);
            return RedirectToAction("Index");
        }
        public ActionResult MarkComplete(Guid id)
        {
            RideManagement rM = db.RideManagements.Find(id);
            repository.MarkCompleteRide(rM.RideCode, rM.Email);
            return RedirectToAction("Index");
        }

       

        // GET: RideManagements/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (Session["userIdentity"] == null)
            {
                return RedirectToAction("Login", "Users");
            }
            RideManagement rideManagement = db.RideManagements.Find(id);
            if(rideManagement != null)
            {
                return View(rideManagement);
            }
            return View();

        }

        // POST: RideManagements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid? id)
        {
            RideManagement rideManagement = db.RideManagements.Find(id);
            if(rideManagement != null)
            {
                db.RideManagements.Remove(rideManagement);
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View();
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