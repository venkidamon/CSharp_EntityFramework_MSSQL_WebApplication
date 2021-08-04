using ProjectAlpha.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectAlpha.Controllers
{
    public class RegistrationController : Controller
    {
        // GET: Registration


        ModelDbContext context = new ModelDbContext();
        StudentPersonalDetail personalObject = new StudentPersonalDetail();
        StudentAcademicDetail academicObject = new StudentAcademicDetail();
        StudentAchievementDetail achievementObject = new StudentAchievementDetail();
        DecisionStatus statusObject = new DecisionStatus();















        /*public ActionResult ex()
        {
            return View();
        }*/
        /*public ActionResult GetPersonalDetails()
        {
            return View();
        }*/
        [HttpGet]
        public ActionResult StudentPersonalDetails(StudentAcademicDetail academicDetails)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                if (academicDetails != null)
                {
                    Session["Academic Details"] = academicDetails;
                }
                var personalObject = (StudentPersonalDetail)Session["Personal Details"];
                if (personalObject != null)
                {
                    return View(personalObject);
                }
            }
            return View();
        }
        /// <summary>
        /// Save the personal details in session.
        /// </summary>
        /// <param name="personalDetails"></param>
        /// <returns>Redirects to StudentAcademicDetail</returns>
        [HttpPost]
        public ActionResult StudentPersonalDetails(StudentPersonalDetail personalDetails)
        {
            if (ModelState.IsValid)
            {
                Session["Personal Details"] = personalDetails;
                string fileName = Path.GetFileNameWithoutExtension(personalDetails.ImageFile.FileName.ToLower());
                string extension = Path.GetExtension(personalDetails.ImageFile.FileName);
                fileName = fileName.Trim() + DateTime.Now.ToString("yyyyMMdd") + extension;
                personalDetails.ImagePath = "~/Content/Image/Data/profilePhoto/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/Content/Image/Data/profilePhoto/"), fileName);
                personalDetails.ImageFile.SaveAs(fileName);

                return RedirectToAction("StudentAcademicDetails");
            }
            else
            {
                return View("StudentPersonalDetails");
            }
        }
        

        /*public ActionResult StudentAcademicDetail()
        {
            return View();
        }*/


        [HttpGet]
        public ActionResult StudentAcademicDetails(StudentAchievementDetail achievementDetails)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                if (achievementDetails != null)
                {
                    Session["Other Details"] = achievementDetails;
                }
                var academicObject = (StudentAcademicDetail)Session["Academic Details"];
                if (academicObject != null)
                {
                    return View(academicObject);
                }
            }
            return View();
        }
        /// <summary>
        /// Save the Academic details
        /// </summary>
        /// <param name="academicDetails"></param>
        /// <returns>Redirect to StudentAchievementDetail</returns>
        [HttpPost]
        public ActionResult StudentAcademicDetails(StudentAcademicDetail academicDetails)
        {
            if (ModelState.IsValid)
            {
                Session["Academic Details"] = academicDetails;
                return RedirectToAction("StudentAchievementDetails");

            }
            else
            {
                return View("StudentAcademicDetails");
            }
        }
        /// <summary>
        /// If already filled save other details in session
        /// </summary>
        /// <returns>Redirects to view</returns>

        /*public ActionResult StudentAchievementDetail()
        {
            return View();
        }*/


        public ActionResult StudentAchievementDetails()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                achievementObject = (StudentAchievementDetail)Session["Other Details"];
                if (achievementObject != null)
                {
                    return View(achievementObject);
                }
            }
            return View();
        }
        /// <summary>
        /// Save other details in session
        /// </summary>
        /// <param name="achievementDetails"></param>
        /// <returns>Redirects to SaveRegistrationDetails</returns>
        [HttpPost]
        public ActionResult StudentAchievementDetails(StudentAchievementDetail achievementDetails)
        {
            if (ModelState.IsValid)
            {
                Session["Other Details"] = achievementDetails;
                string fileName = Path.GetFileNameWithoutExtension(achievementDetails.ImageFile.FileName);
                string extension = Path.GetExtension(achievementDetails.ImageFile.FileName);
                fileName = fileName.Trim() + DateTime.Now.ToString("yyyyMMdd") + extension;
                achievementDetails.ImagePath = "~/Content/Image/Data/submittedProof/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/Content/Image/Data/submittedProof/"), fileName);
                achievementDetails.ImageFile.SaveAs(fileName);


                return RedirectToAction("SaveRegistrationDetails");
            }
            else
            {
                return View("StudentAchievementDetails");
            }
        }
        /// <summary>
        /// Personal,Academic and Other details from session is stored in database along with DecisionID is generated and approval status is stored.
        /// </summary>
        /// <returns>Redirects Student to view the approval status</returns>
        public ActionResult SaveRegistrationDetails()
        {
            Guid userId = new Guid();
            userId = (Guid)Session["UserID"];

            personalObject = (StudentPersonalDetail)Session["Personal Details"];
            System.Guid personalDetailGuid = System.Guid.NewGuid();
            personalObject.PersonalDetailID = personalDetailGuid;
            personalObject.UserID = userId;
            context.StudentPersonalDetails.Add(personalObject);
            context.SaveChanges();

            academicObject = (StudentAcademicDetail)Session["Academic Details"];
            System.Guid academicDetailGuid = System.Guid.NewGuid();
            academicObject.AcademicDetailID = academicDetailGuid;
            academicObject.UserID = userId;
            context.StudentAcademicDetails.Add(academicObject);
            context.SaveChanges();

            achievementObject = (StudentAchievementDetail)Session["Other Details"];
            System.Guid otherDetailGuid = System.Guid.NewGuid();
            achievementObject.AchievementDetailID = otherDetailGuid;
            achievementObject.UserID = userId;
            context.StudentAchievementDetails.Add(achievementObject);
            context.SaveChanges();

            System.Guid decisonStatusGuid = System.Guid.NewGuid();
            statusObject.DecisionStatusID = decisonStatusGuid;
            statusObject.UserID = userId;
            statusObject.ApprovalStatus = "Pending";
            context.DecisionStatuses.Add(statusObject);
            context.SaveChanges();


            return RedirectToAction("UserStatus", "Status");
        }
    }
}