using Facebook;
using ProjectAlpha.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ProjectAlpha.Controllers
{
    public class FacebookController : Controller
    {
        // GET: Facebook

        UserStudent user = new UserStudent();
        ModelDbContext db = new ModelDbContext();
        HashPassword saltPassword = new HashPassword();
        StudentPersonalDetail personal = new StudentPersonalDetail();
        public ActionResult Index()
        {
            return View();
        }



        private Uri RediredtUri

        {

            get

            {

                var uriBuilder = new UriBuilder(Request.Url);

                uriBuilder.Query = null;

                uriBuilder.Fragment = null;

                uriBuilder.Path = Url.Action("FacebookCallback");

                return uriBuilder.Uri;

            }

        }




        [AllowAnonymous]

        public ActionResult Facebook()

        {

            var fb = new FacebookClient();

            var loginUrl = fb.GetLoginUrl(new

            {




                client_id = "2791152864497215",

                client_secret = "744d96a0f4814e82e1e108a6fd388b6b",

                redirect_uri = RediredtUri.AbsoluteUri,

                response_type = "code",

                scope = "email"



            });

            return Redirect(loginUrl.AbsoluteUri);

        }




        public ActionResult FacebookCallback(string code)

        {
            UserStudent user = new UserStudent();

            var fb = new FacebookClient();

            dynamic result = fb.Post("oauth/access_token", new

            {

                client_id = "2791152864497215",

                client_secret = "744d96a0f4814e82e1e108a6fd388b6b",

                redirect_uri = RediredtUri.AbsoluteUri,

                code = code




            });

            var accessToken = result.access_token;

            Session["AccessToken"] = accessToken;

            fb.AccessToken = accessToken;

            dynamic me = fb.Get("me?fields=link,first_name,currency,last_name,email,gender,locale,timezone,verified,picture,age_range");

            string email = me.email;
            string passs = me.first_name;
            string nname = me.first_name;

            TempData["email"] = me.email;

            TempData["first_name"] = me.first_name;

            TempData["lastname"] = me.last_name;

            TempData["picture"] = me.picture.data.url;



            user.Email = email;
            user.UserName = nname;
            user.Password = saltPassword.Encryption(passs);
            using (ModelDbContext db = new ModelDbContext())
            {
                System.Guid newValue = System.Guid.NewGuid();
                user.UserID = newValue;
                db.UserStudents.Add(user);

                db.SaveChanges();
            }










            FormsAuthentication.SetAuthCookie(email, false);
            string key = saltPassword.Decryption(user.Password);

            if (passs == key)
            {
                Session["UserID"] = user.UserID;
                personal = db.StudentPersonalDetails.SingleOrDefault(p => p.UserID == user.UserID);
                if (personal != null)
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




    }
}