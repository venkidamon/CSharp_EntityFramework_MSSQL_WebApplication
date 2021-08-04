using CarPool.Models;
using CarPool.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarPool.Controllers
{
    public class RidesController : Controller
    {
        private IRideRepository repository;
        private RideManagementRepository cuRepository = new RideManagementRepository();
        public RidesController()
        {
            this.repository = new RideRepository(new RideDbContext());
        }

        public RidesController(IRideRepository repo)
        {
            repository = repo;
        }


        
        

        public ActionResult Search(string Search)
        {
            throw new NotImplementedException();
        }

       

    
    


       

      


        [HttpGet]
        public ActionResult Assign(string id)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [ActionName("Assign")]
        public ActionResult AssignRide([Bind(Include = "RideCode")] string id)
        {
            throw new NotImplementedException();
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
            base.Dispose(disposing);
        }












        public ActionResult Create()
        {
            return View();
            //throw new NotImplementedException();
        }

        [HttpPost]
        public ActionResult Create([Bind(Include = "RideCode,Description,RideDate,SeatCount")] Ride Ride)
        {
            throw new NotImplementedException();
        }







        public ActionResult AddRide()
        {
            if(Session["riderIdentity"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                string guid = Guid.NewGuid().ToString();
                Rider rider = new Rider();
                Ride ride = new Ride();
                ride.RideCode = guid;
                ride.CreatedDate = DateTime.Now;
                ride.BookedCount = 0;
                ride.Status = "New";
                return View(ride);
            }
            
        } 
        [HttpPost]
        public ActionResult AddRide(Ride ride)
        {
            repository.AddRide(ride);
            repository.Save();
            return RedirectToAction("index");
        }


        // GET: Rides/Edit/5
        public ActionResult Edit(string id)
        {
            Ride ride = repository.GetRideByCode(id);
            ride.CreatedDate = DateTime.Now;
            return View(ride);
        }

        [HttpPost]
        public ActionResult Edit([Bind(Include = "RideCode,Description,CreatedDate,RideDate,SeatCount,BookedCount,Status")] Ride ride)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    repository.UpdateRide(ride);
                    repository.Save();
                    return RedirectToAction("#");
                }
            }
            catch (DataException)
            {
               
                ModelState.AddModelError(string.Empty, "Unable to save changes. Try again, and if the problem persists contact your system administrator.");
            }
            return View(ride);
        }


        // GET: Rides/Delete/5
        public ActionResult Delete(string id)
        {
            Ride ride = repository.GetRideByCode(id);
            return View(ride);
        }

        // POST: Rides/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id)
        {
            try
            {
                Ride ride = repository.GetRideByCode(id);
                repository.Disable(id);
                repository.Save();
            }
            catch (DataException)
            {

                ModelState.AddModelError(string.Empty, "Unable to save changes. Try again, and if the problem persists contact your system administrator.");
            }
            return RedirectToAction("Index");

        }
        // GET: Rides/Details/5
        public ActionResult Details(string id)
        {
            Ride ride = repository.GetRideByCode(id);
            return View(ride);
        }

        public ActionResult ActiveRides()
        {
            return View(repository.GetActiveRides());
        }


        public ActionResult Index()
        {
            return View(repository.GetActiveRides().ToList());

        }
        [ActionName("search")]
        public ActionResult Index(string search)
        {
            List<Ride> ride = repository.GetActiveRidesByRider(search);
            if(ride != null)
            {
                return View(ride);
            }
            
            
            else
            {
                ViewBag.Message = "No rides found";
                return View();
            }
        }







    }
}