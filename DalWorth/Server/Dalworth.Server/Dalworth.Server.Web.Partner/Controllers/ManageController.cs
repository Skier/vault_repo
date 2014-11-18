using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;
using Dalworth.Server.Domain;
using Dalworth.Server.Web.Partner.Models;
using AdvertisingSource = Dalworth.Server.Web.Partner.Models.AdvertisingSource;
using Order = Dalworth.Server.Web.Partner.Models.Order;
using TrackingPhone = Dalworth.Server.Web.Partner.Models.TrackingPhone;

namespace Dalworth.Server.Web.Partner.Controllers
{
    public class ManageController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {            
            if (!PartnerMembershipProvider.GetCurrentUser().IsOwner)
                filterContext.Result = RedirectToAction("CallLog", "Home");
        }

        public ActionResult Index()
        {
            return RedirectToAction("TrackingPhones");
        }

        public ActionResult TrackingPhones()
        {            
            return View(new TrackingPhones());
        }

        [HttpGet]
        public ActionResult AddTrackingPhone()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddTrackingPhone(TrackingPhone newPhone)
        {
            if (newPhone != null && ModelState.IsValid)
            {
                string errorText = newPhone.Save();
                if (errorText != string.Empty)
                    ModelState.AddModelError("", errorText);
                else
                    return RedirectToAction("TrackingPhones");
            }

            return View();
        }

        public ActionResult TrackingPhoneDelete(int trackingPhoneId)
        {
            TrackingPhones trackingPhonesModel = new TrackingPhones();
            trackingPhonesModel.DeletePhone(trackingPhoneId);
            return RedirectToAction("TrackingPhones");        
        }

        public ActionResult Partners(bool? showInactive)
        {
            if (showInactive.HasValue && showInactive.Value)
                return View(new Partners(true));
            return View(new Partners());
        }

        [HttpGet]
        public ActionResult AddPartner()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddPartner(Models.Partner partner)
        {
            if (partner != null && ModelState.IsValid)
            {
                partner.Save();
                return RedirectToAction("Partners");
            }

            return View();
        }

        public ActionResult Partner(int? partnerId, bool? activate)
        {
            if (!partnerId.HasValue)
                return RedirectToAction("Partners");

            Session["CurrentPartnerId"] = partnerId.Value;
            Models.Partner partner = new Models.Partner(partnerId.Value);
                        

            if (Request.UrlReferrer != null)
            {
                if (Request.UrlReferrer.AbsoluteUri.Contains("TrackingPhones")
                    || Request.UrlReferrer.AbsoluteUri.Contains("Partners"))
                {
                    Session["PartnerPageReferrer"] = Request.UrlReferrer.AbsoluteUri;                    
                }

                partner.BackLink = (string)Session["PartnerPageReferrer"];
            }
                
            if (activate.HasValue)
                partner.SetActivityStatus(activate.Value);
            return View(partner);
        }

        [HttpGet]
        public ActionResult AssignTrackingPhone()
        {
            return View(new TrackingPhones((int)Session["CurrentPartnerId"], true));
        }

        [HttpPost]
        public ActionResult AssignTrackingPhone(int? trackingPhoneId)
        {
            int partnerId = (int)Session["CurrentPartnerId"];

            if (!trackingPhoneId.HasValue)
                ModelState.AddModelError("", "Please select Tracking Phone to assign");
            else
            {                
                Models.Partner partner = new Models.Partner(partnerId);
                partner.AssignTrackingPhone(trackingPhoneId.Value);
                return RedirectToAction("Partner", new { PartnerId = partnerId });
            }

            return View(new TrackingPhones(partnerId, true));
        }

        [HttpGet]
        public ActionResult AssignAdvertisingSource()
        {
            return View(new AdvertisingSource((int)Session["CurrentPartnerId"]));
        }

        [HttpPost]
        public ActionResult AssignAdvertisingSource(AdvertisingSource advertisingSource)
        {
            int partnerId = (int)Session["CurrentPartnerId"];

            Models.Partner partner = new Models.Partner(partnerId);
            advertisingSource.PartnerId = partnerId;
            string errorText = partner.AssignAdvertisingSource(advertisingSource);
            if (errorText != string.Empty)
            {
                ModelState.AddModelError("", errorText);
                return View(advertisingSource);                
            }
            return RedirectToAction("Partner", new { PartnerId = partnerId });
            
        }

        public ActionResult UnassignTrackingPhone(int? trackingPhoneId)
        {
            if (!trackingPhoneId.HasValue)
                return RedirectToAction("Partners");

            int partnerId = (int)Session["CurrentPartnerId"];
            Models.Partner partner = new Models.Partner(partnerId);
            partner.UnassignTrackingPhone(trackingPhoneId.Value);

            return RedirectToAction("Partner", new { PartnerId = partnerId });
        }

        public ActionResult UnassignAdvertisingSource(int? advertisingSourceId)
        {
            if (!advertisingSourceId.HasValue)
                return RedirectToAction("Partners");

            int partnerId = (int)Session["CurrentPartnerId"];
            Models.Partner partner = new Models.Partner(partnerId);
            partner.UnassignAdvertisingSource(advertisingSourceId.Value);

            return RedirectToAction("Partner", new { PartnerId = partnerId });
        }

        [HttpGet]
        public ActionResult AddPersonalPhone()
        {
            return View(new Phone(0, string.Empty, (int)Session["CurrentPartnerId"]));            
        }
        
        [HttpPost]
        public ActionResult AddPersonalPhone(Phone phone)
        {
            if (phone != null && ModelState.IsValid)
            {
                int partnerId = (int)Session["CurrentPartnerId"];
                Models.Partner partner = new Models.Partner(partnerId);
                partner.AddPersonalPhone(phone);
                return RedirectToAction("Partner", new { PartnerId = partnerId });
            }

            return View(phone);
        }

        public ActionResult DeletePersonalPhone(int? personalPhoneId)
        {
            if (!personalPhoneId.HasValue)
                return RedirectToAction("Partners");

            int partnerId = (int)Session["CurrentPartnerId"];
            Models.Partner partner = new Models.Partner(partnerId);
            partner.DeletePersonalPhone(personalPhoneId.Value);

            return RedirectToAction("Partner", new { PartnerId = partnerId });
        }

        [HttpGet]
        public ActionResult UserDetails(int? userId)
        {
            TempData["UserId"] = userId;
            User user = new User(userId);

            if (Request.UrlReferrer != null)
            {
                if (Request.UrlReferrer.AbsoluteUri.Contains("Partner?PartnerId")
                    || Request.UrlReferrer.AbsoluteUri.Contains("Users"))
                {
                    Session["UserDetailsPageReferrer"] = Request.UrlReferrer.AbsoluteUri;
                }

                user.BackLink = (string)Session["UserDetailsPageReferrer"];
            }

            return View(user);
        }

        [HttpPost]
        public ActionResult UserDetails(User user)
        {            
            if (user != null)
            {
                if (TempData["UserId"] != null)
                    user.Id = (int) TempData["UserId"];

                int? partnerId = null;
                if (Session["CurrentPartnerId"] != null)
                    partnerId = (int) Session["CurrentPartnerId"];

                string errorText = user.Save(partnerId);
                if (errorText != string.Empty)
                {
                    ModelState.AddModelError("", errorText);
                    if (partnerId.HasValue)
                        user.PartnerId = partnerId.Value;
                }
                else
                    return Redirect((string)Session["UserDetailsPageReferrer"]);
            }
            
            return View(user);
        }

        public ActionResult DeleteUserOnPartner(int userId)
        {
            DeleteUser(userId);
            return RedirectToAction("Partner", new { PartnerId = (int)Session["CurrentPartnerId"] });
        }

        public ActionResult DeleteUserOnUsers(int userId)
        {
            DeleteUser(userId);
            return RedirectToAction("Users");
        }

        private void DeleteUser(int userId)
        {
            User user = new User();
            user.Id = userId;
            user.Delete();            
        }

        public ActionResult Users(Users users)
        {
            if (users != null)
                users.Load();            
            return View(users);
        }

        public ActionResult ResetPassword(int userId)
        {
            User user = new User(userId);
            string successMessage = string.Empty;
            if (user.HasPassword)
                successMessage = "Password reset instructions were successfully sent";
            else
                successMessage = "Invitation has been successfully sent";
            user.SendInvitation();
            TempData["SuccessMessage"] = successMessage;
            return RedirectToAction("UserDetails", new { UserId = userId });            
        }
    }
}
