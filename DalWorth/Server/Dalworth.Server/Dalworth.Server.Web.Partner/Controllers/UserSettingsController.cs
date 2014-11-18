using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dalworth.Server.Domain;
using Dalworth.Server.Web.Partner.Models;

namespace Dalworth.Server.Web.Partner.Controllers
{
    public class UserSettingsController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("PersonalInfo");
        }

        public ActionResult PersonalInfo()
        {
            return View(new PersonalInfoModel());
        }
    
        [HttpPost]
        public ActionResult PersonalInfo(PersonalInfoModel personalInfo)
        {
            if (personalInfo != null && ModelState.IsValid)
            {                
                string errorText = personalInfo.Save();
                if (errorText != string.Empty)
                    ModelState.AddModelError("", errorText);
                else
                    return RedirectToAction("CallLog", "Home");
            }

            return PersonalInfo();
        }

        public ActionResult ManagePhones()
        {
            WebUser currentUser = PartnerMembershipProvider.GetCurrentUser();
            if (!currentUser.OrderSourceId.HasValue)
                return RedirectToAction("CallLog", "Home");

            return View(new PhoneInfoModel());
        }

        public ActionResult Delete(int id)
        {
            PhoneInfoModel phoneModel = new PhoneInfoModel();
            phoneModel.DeleteOwnPhone(id);
            return RedirectToAction("ManagePhones");
        }

        public ActionResult AddPhone()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult AddPhone(OwnPhone newPhone)
        {
            if (newPhone != null && ModelState.IsValid)
            {
                PhoneInfoModel phoneModel = new PhoneInfoModel();
                string errorText = phoneModel.AddOwnPhone(newPhone.PhoneNumber);
                if (errorText != string.Empty)
                    ModelState.AddModelError("", errorText);
                else
                    return RedirectToAction("ManagePhones");
            }            

            return View();
        }
    }
}
