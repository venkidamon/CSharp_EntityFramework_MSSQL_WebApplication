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
    public class RidersController : Controller
    {
        // GET: Riders
        private readonly IRiderRepository repository;
        public RidersController()
        {
            this.repository = new RiderRepository(new RideDbContext());
        }
        public RidersController(IRiderRepository repo)
        {
            this.repository = repo;
        }





        //create
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create([Bind(Include = "RiderName,Email,Password")] Rider rider)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int result = repository.CreateRider(rider);
                    if (result == 1)
                    {
                        return RedirectToAction("Login");
                    }
                    ViewBag.Message = "User already present, please login";
                    return View("Create");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError(string.Empty, "Unable to save changes. Try again, and if the problem persists contact your system administrator.");
            }
            ViewBag.Message = "!Error";
            return View("Create");
        }






        //login
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Validate([Bind(Include = "RiderName,Password")] Rider rider)
        {
            try
            {
               
                    if (repository.RiderExists(rider))
                    {
                        string result = repository.Validate(rider);
                        if (result != null)
                        {
                            Session["riderIdentity"] = rider.RiderName;
                            return RedirectToAction("ActiveRides","Rides");
                        }
                        ViewBag.Message = "Authentication Failed";
                        return View("Login");
                    }
                    ViewBag.Message = "Rider does not exist, please signup.";
                    return View("Login");
                
            }
            catch (DataException)
            {
                ModelState.AddModelError(string.Empty, "Unable to save changes. Try again, and if the problem persists contact your system administrator.");
            }

            ViewBag.Message = "!Error";
            return View("Login");
        }
    }
}