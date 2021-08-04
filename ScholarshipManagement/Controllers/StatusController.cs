using ProjectAlpha.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectAlpha.Controllers
{
    public class StatusController : Controller
    {
        // GET: Status
        ModelDbContext context = new ModelDbContext();

        public ActionResult UserStatus()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {


                Guid userid = (Guid)Session["UserID"];



                List<ViewModelUserStatus> viewModels = new List<ViewModelUserStatus>();
                var ListDetails = (from per in context.StudentPersonalDetails
                                   join sta in context.DecisionStatuses on per.UserID equals sta.UserID
                                   join usr in context.UserStudents on per.UserID equals usr.UserID
                                   where (sta.UserID == userid)
                                   select new { per.FullName, sta.ApprovalStatus, per.UserID, usr.Email }).ToList();
                foreach (var item in ListDetails)
                {
                    ViewModelUserStatus viewModel = new ViewModelUserStatus();
                    viewModel.FullName = item.FullName;
                    viewModel.ApprovalStatus = item.ApprovalStatus;
                    viewModel.UserID = item.UserID;
                    viewModel.EmailID = item.Email;
                    viewModels.Add(viewModel);
                }

                return View(viewModels);
            }
        }

        public ActionResult AdminDashboard()
        {
            if (Session["AdminId"] == null)
            {
                return RedirectToAction("AdminLogin", "Home");
            }
            else
            {
                List<ViewModelAdminDashboard> viewModelAdmins = new List<ViewModelAdminDashboard>();
                var ListDetails = (from per in context.StudentPersonalDetails
                                   join sta in context.DecisionStatuses on per.UserID equals sta.UserID
                                   join aca in context.StudentAcademicDetails on per.UserID equals aca.UserID
                                   select new { per.FullName, per.AnnualIncome, aca.SSLCPercentage, sta.ApprovalStatus, per.UserID }).ToList();
                foreach (var item in ListDetails)
                {
                    ViewModelAdminDashboard viewModelAdmin = new ViewModelAdminDashboard();
                    viewModelAdmin.FullName = item.FullName;
                    viewModelAdmin.ApprovalStatus = item.ApprovalStatus;
                    viewModelAdmin.AnnualIncome = item.AnnualIncome;
                    viewModelAdmin.SSLCPercentage = item.SSLCPercentage;
                    viewModelAdmin.UserID = item.UserID;
                    viewModelAdmins.Add(viewModelAdmin);
                    if (viewModelAdmin.ApprovalStatus != "Pending")
                    {
                        viewModelAdmins.Remove(viewModelAdmin);
                    }
                }

                return View(viewModelAdmins);
            }
        }
        /* public ActionResult DisplayDetails()
         {
             return View();
         }*/
        public ActionResult DisplayDetails(Guid id)
        {

            if (Session["AdminId"] == null)
            {
                return RedirectToAction("AdminLogin", "Home");
            }
            else
            {
                StudentPersonalDetail personalDetail = new StudentPersonalDetail();
                personalDetail = context.StudentPersonalDetails.SingleOrDefault(a => a.UserID == id);
                DecisionStatus decisionStatus = new DecisionStatus();
                decisionStatus = context.DecisionStatuses.SingleOrDefault(a => a.UserID == id);
                StudentAcademicDetail academicDetail = new StudentAcademicDetail();
                academicDetail = context.StudentAcademicDetails.SingleOrDefault(a => a.UserID == id);
                StudentAchievementDetail achievementDetail = new StudentAchievementDetail();
                achievementDetail = context.StudentAchievementDetails.SingleOrDefault(a => a.UserID == id);


                var viewModel = new ViewModelDisplayDetail()
                {

                    PersonalDetail = personalDetail,
                    DecisionStatus = decisionStatus,
                    AcademicDetail = academicDetail,
                    AchievementDetail = achievementDetail

                };

                return View(viewModel);


            }

        }


        public ActionResult Approve(Guid id)
        {
            if (Session["AdminId"] == null)
            {
                return RedirectToAction("AdminLogin", "Home");
            }
            else
            {

                DecisionStatus decisionStatus = new DecisionStatus();
                ViewModelAdminView adminView = new ViewModelAdminView();
                var status = context.DecisionStatuses.FirstOrDefault(x => x.UserID == id);
                status.ApprovalStatus = "Approved";
                decisionStatus.ApprovalStatus = status.ApprovalStatus;
                context.SaveChanges();

                return RedirectToAction("AdminDashboard");
            }
        }

        public ActionResult Reject(Guid id)
        {

            if (Session["AdminId"] == null)
            {
                return RedirectToAction("AdminLogin", "Home");
            }
            else
            {
                DecisionStatus decisionStatus = new DecisionStatus();
                ViewModelAdminView adminView = new ViewModelAdminView();
                var status = context.DecisionStatuses.FirstOrDefault(x => x.UserID == id);
                status.ApprovalStatus = "Rejected";
                decisionStatus.ApprovalStatus = status.ApprovalStatus;
                context.SaveChanges();
                /* string fullPath = Request.MapPath("~/image/ImageData/" + );
                 string ProfilefullPath = Request.MapPath("~/image/ImageData/" + photoName);
                 if (System.IO.File.Exists(fullPath)||System.IO.File.Exists(ProfilefullPath))
                 {

                     System.IO.File.Delete(fullPath);
                     System.IO.File.Delete(ProfilefullPath);
                 }*/
                return RedirectToAction("AdminDashboard");
            }
        }
        public ActionResult Info(Guid id, string Status)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                if (Status == "Pending")
                {
                    ViewBag.Message = "Your application is still under process";
                    return View();
                }
                else if (Status == "Approved")
                {
                    ViewBag.Message = "Your application is approved";
                    return View();
                }
                else if (Status == "Rejected")
                {

                    ViewBag.Message = "Youre application is Rejected.Please go through our eligibility criteria";
                    return View("InfoReject");
                }

                else
                {
                    return RedirectToAction("Error", "Shared");
                }
            }
        }
    }
}