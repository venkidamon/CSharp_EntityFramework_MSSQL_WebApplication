using CarPool.Models;
using CarPool.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CarPool.Controllers
{
    public class UsersController : Controller
    {
        // GET: Users

        private readonly IUserRepository repository;
        public UsersController()
        {
            this.repository = new UserRepository(new RideDbContext());
        }
        public UsersController(IUserRepository repo)
        {
            this.repository = repo;
        }
    

        public ActionResult Create()
        {

            return View();
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create([Bind(Include = "email,password,ConfirmPassword")] User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int result = repository.CreateUser(user);
                    repository.Save();
                    if (result == 1)
                    {
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        ViewBag.Message = "User already present";
                        return View("Create");
                    }

                }
            }
            catch (DataException er)
            {
                ModelState.AddModelError(string.Empty, "Unable to save changes. Try again, and if the problem persists contact your system administrator." + er);
            }
            ViewBag.Message = "!Error";
            return View("Create");
            
        }

        [HttpPost]
        public ActionResult Validate([Bind(Include = "email,password")] User user)
        {
            if (ModelState.IsValid)
            {
                bool result = repository.Validate(user);
                if (result)
                {
                    Session["userIdentity"] = user.Email;
                    return RedirectToAction("ActiveRides","Rides");
                }
                ViewBag.Message = "Authentication Failed";
                return View("Login");
            }
            ViewBag.Message = "!Error";
            return View("Login");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }


       

        protected override void Dispose(bool disposing)
        {
            
            if (disposing && repository != null)
                repository.Dispose();
            base.Dispose(disposing);
        }
    }
}