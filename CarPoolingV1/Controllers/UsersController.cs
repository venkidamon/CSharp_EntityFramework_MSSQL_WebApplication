using CarPool.Models;
using CarPool.Repository;
using System;
using System.Collections.Generic;
using System.Data;
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
            repository = repo;
        }


        //signup or create

        public ActionResult Create()
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
                    int resultValue = repository.CreateUser(user);
                    if (resultValue == 1)
                    {
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        ViewBag.Message = "User already present, please login";
                        return View("Create");
                    }
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError(string.Empty, "Unable to save changes. Try again, and if the problem persists contact your system administrator.");
            }
            ViewBag.Message = "!Error";
            return View("Create");
        }


        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Validate([Bind(Include = "email,password")] User user)
        {
            try
            {
               /* if (ModelState.IsValid)
                {*/
                    if (repository.UserExists(user))
                    {
                        if (repository.Validate(user))
                        {
                            Session["userIdentity"] = user.Email;
                            return RedirectToAction("Index","Rides");
                        }
                        ViewBag.Message = "Error";
                        return View();
                    }
                    ViewBag.Message = "Please SignUp, User does not exist";
               /* }
                else
                {
                    var errors = ModelState.Select(x => x.Value.Errors)
                          .Where(y => y.Count > 0)
                          .ToList();
                }*/
            }
            catch (DataException)
            {
                ModelState.AddModelError(string.Empty, "Unable to save changes. Try again, and if the problem persists contact your system administrator.");
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
            if (disposing)
            {
                repository.Dispose();
            }
            base.Dispose(disposing);
        }


    }
}