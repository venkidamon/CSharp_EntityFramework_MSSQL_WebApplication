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
        private readonly IRiderRepository repository;
        public RidersController()
        {
            this.repository = new RiderRepository(new RideDbContext());
        }
        public RidersController(IRiderRepository repo)
        {
            this.repository = repo;
        }

        public ActionResult Create()
        {
            return View();
        }

        // POST: Riders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "RiderName,Email,Password")] Rider rider)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int result = repository.CreateRider(rider);
                    repository.Save();
                    if(result == 1)
                    {
                        return RedirectToAction("Login");
                    }
                    ViewBag.Message = "User already present";
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

        [HttpPost]
        public ActionResult Validate([Bind(Include = "RiderName,Password")] Rider rider)
        {
            if (ModelState.IsValid)
            {
                string result = repository.Validate(rider);
                if(result != null)
                {
                    Session["riderIdentity"] = rider.RiderName;
                    return RedirectToAction("Index","Rides");
                }
                ViewBag.Message = "Authentication Failed";
                return View("Login");
            }
            ViewBag.Message = "!Error";
            return View("Login");
        }
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Logout()
        {
            throw new NotImplementedException();
        }
    }
}