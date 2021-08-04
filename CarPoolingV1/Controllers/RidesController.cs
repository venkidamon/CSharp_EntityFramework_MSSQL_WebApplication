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
        public ActionResult RideDetails(string id)
        {
            if (Session["riderIdentity"] == null)
            {
                return RedirectToAction("Login", "Riders");
            }
            List<RideManagement> rideManagements = cuRepository.GetManagementByRider(id);

            return View(rideManagements);
        }

        public ActionResult Index()
        {
            /* RideDbContext context = new RideDbContext();
             List<RideViewModel> rideViewModel = new List<RideViewModel>();
             var listDetails = (from ride in context.Rides
                                join rider in context.Riders on ride.RiderName equals rider.RiderName
                                select new { ride.RideCode, ride.Description, ride.RideDate, ride.SeatCount, rider.RiderName });
             foreach(var item in listDetails)
             {
                 RideViewModel rideView = new RideViewModel();
                 rideView.RideCode = item.RideCode;
                 rideView.Description = item.Description;
                 rideView.RideDate = item.RideDate;
                 rideView.SeatCount = item.SeatCount;
                 rideView.RiderName = item.RiderName;
                 rideViewModel.Add(rideView);
             }*/


            List<Ride> rideList = repository.GetActiveRides();
            
            return View(rideList);
        }
        [HttpPost]
        public ActionResult Index(string search)
        {
            List<Ride> ride = repository.GetActiveRidesByRider(search);
         
          /*  Ride rideCode = repository.GetRideByCode(search);
            ride.Add(rideCode);*/
            if (ride != null)
            {
                return View(ride);
            }


            else
            {
                ViewBag.Message = "No rides found";
                return View();
            }
        }





        public ActionResult ActiveRides()
        {
            if(Session["riderIdentity"] == null)
            {
                return RedirectToAction("Login", "Riders");
            }
            string name = (string)Session["riderIdentity"];
            List<Ride> rideList = repository.GetActiveRidesByRider(name);
            return View(rideList);
        }
    [HttpPost]
    [ActionName("ActiveRides")]
    public ActionResult Search(string search)
        {
            Ride rideCode = repository.GetRideByCode(search);
            List<Ride> rideList = new List<Ride>();
            List<Ride> dummyList = new List<Ride>();
            rideList.Add(rideCode);
            foreach(var item in rideList)
            {
                if(item == null)
                {
                    return View(dummyList);
                }
            }
            if (rideList != null)
            {
                return View(rideList);
            }
            else
            {
               
                return View();
            }
        }










        // GET: Rides/Details/5
        public ActionResult Details(string id)
        {
         
            Ride ride = repository.GetRideByCode(id);
            return View(ride);
        }








        public ActionResult Create()
        {
            if(Session["riderIdentity"] == null)
            {
                return RedirectToAction("Login", "Riders");
            }
            return View();
            
        }

        [HttpPost]
        public ActionResult Create([Bind(Include = "RideCode,Description,RideDate,SeatCount")] Ride ride)
        {
            ride.CreatedBy = (string)Session["riderIdentity"];
            var data = DateTime.Now;
            ride.CreatedDate = data.Date;
            ride.BookedCount = 0;
            ride.Status = "New";
            ride.RiderName = (string)Session["riderIdentity"];
            repository.AddRide(ride);
            return RedirectToAction("ActiveRides");
        }










        public ActionResult AddRide()
        {
            throw new NotImplementedException();
        }













        public ActionResult Edit(string id)
        {
            if (Session["riderIdentity"] == null)
            {
                return RedirectToAction("Login", "Riders");
            }
            Ride ride = repository.GetRideByCode(id);
            return View(ride);
        }

        [HttpPost]
        public ActionResult Edit([Bind(Include = "RideCode,Description,CreatedDate,RideDate,SeatCount,BookedCount,Status")] Ride ride)
        {
            try
            {
                
                    ride.CreatedBy = (string)Session["riderIdentity"];
                     var date= DateTime.Now;
                    ride.CreatedDate = date.Date;
                    ride.RiderName = (string)Session["riderIdentity"];
                    repository.UpdateRide(ride);
                    return RedirectToAction("ActiveRides");

                
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
            if (Session["riderIdentity"] == null)
            {
                return RedirectToAction("Login", "Riders");
            }
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
            }
            catch (DataException)
            {

                ModelState.AddModelError(string.Empty, "Unable to save changes. Try again, and if the problem persists contact your system administrator.");
            }
            return RedirectToAction("ActiveRides");

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
    }
}