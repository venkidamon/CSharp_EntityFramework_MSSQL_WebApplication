using ProjectAlpha.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace ProjectAlpha.Controllers
{
    public class HomeController : Controller
    {
        //*******************************************************************************************//
        
        //object references

        ModelDbContext context = new ModelDbContext();
        HashPassword saltPassword = new HashPassword();
        UserStudent userStudentObject = new UserStudent();
        StudentPersonalDetail personalObject = new StudentPersonalDetail();

        //*******************************************************************************************//



        // GET: Home




        //************************************************************************************************//


        //HOME PAGE



        public ActionResult Index()
        {
            return View();
        }

        //***********************************************************************************************//


        //****************************************************************************************************//


        //SIGNUP PAGE
        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(UserStudent user)
        {

            if (ModelState.IsValid)
            {
                userStudentObject = context.UserStudents.FirstOrDefault(x => x.Email == user.Email);
                if (userStudentObject == null)
                {
                    try
                    {
                        System.Guid guid = System.Guid.NewGuid();
                        user.UserID = guid;
                        /*user.Password = user.Password;*/
                        user.Password = saltPassword.Encryption(user.Password);
                        context.UserStudents.Add(user);
                        context.SaveChanges();
                        
                        return RedirectToAction("Login");
                    }
                    catch (DbEntityValidationException dbEx)
                    {
                        foreach (var validationErrors in dbEx.EntityValidationErrors)
                        {
                            foreach (var validationError in validationErrors.ValidationErrors)
                            {
                                System.Console.WriteLine("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                            }
                        }

                    }

                    return View();
                }
                else
                {
                    /*ModelState.AddModelError("UserAlreadyExists", "User already exists.Please Login");*/
                    ViewBag.Message = "User Already Exists. Please Login";
                    return View("SignUp");
                }
            }
            else
            {
                var errors = ModelState
    .Where(x => x.Value.Errors.Count > 0)
    .Select(x => new { x.Key, x.Value.Errors })
    .ToArray();
                return View("SignUp");
            }
        }



        //********************************************************************************************************//




        //********************************************************************************************************//

        //LOGIN PAGE
        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Login(UserStudent user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    userStudentObject = context.UserStudents.FirstOrDefault(u => u.Email == user.Email);
                    if (userStudentObject == null)
                    {
                        ViewBag.Message = "User does not exist";
                        return View("Login");
                    }
                }
                catch (InvalidOperationException)
                {
                    ModelState.AddModelError("UserExistence", "User does not exist.Please sign up.");
                    return View("SignUp");
                }
              
               
                string key = saltPassword.Decryption(userStudentObject.Password);
                /*  if (user.Password == userObject.Password)*/
                if (user.Password == key)
                {
                    Session["UserID"] = userStudentObject.UserID;
                    personalObject = context.StudentPersonalDetails.SingleOrDefault(p => p.UserID == userStudentObject.UserID);
                    if (personalObject != null)
                    {
                        return RedirectToAction("UserStatus", "Status");
                    }
                    else
                    {
                        return RedirectToAction("StudentPersonalDetails", "Registration");
                    }

                }
                else
                {
                    ModelState.AddModelError("PasswordIncorrect", "Incorrect Password.Enter the Correct Password.");
                    return View("Login");
                }
            }
            else
            {
                var errors = ModelState
    .Where(x => x.Value.Errors.Count > 0)
    .Select(x => new { x.Key, x.Value.Errors })
    .ToArray();
                return View("Login");
            }
        }
        //**********************************************************************************************//




        //**********************************************************************************************//
        public ActionResult AdminLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AdminLogin(UserStudent user)
        {
            Session["AdminID"] = System.Configuration.ConfigurationManager.AppSettings["AdminId"];
            string adminEmailID = System.Configuration.ConfigurationManager.AppSettings["AdminEmailID"];
            string adminPassword = System.Configuration.ConfigurationManager.AppSettings["AdminPassword"];

            if (user.Email == adminEmailID)
            {
                if (user.Password == adminPassword)
                {
                    return RedirectToAction("AdminDashboard", "Status");
                }
                else
                {
                    ModelState.AddModelError("PasswordIncorrect", "Incorrect Password");
                    return View("AdminLogin");
                }
            }
            else
            {
                ModelState.AddModelError("WrongCredentials", "Enter valid credentials");
                return View("AdminLogin");
            }
        }
        //*******************************************************************************************************//
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();



            //TODO: Do book keeping about this Facebook user's activity.
            FormsAuthentication.SignOut();
            Session["fbUser"] = null;

            return RedirectToAction("Index", "Home");
        }
    }
}
