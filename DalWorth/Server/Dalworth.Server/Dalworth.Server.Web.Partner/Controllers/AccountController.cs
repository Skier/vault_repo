using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using Dalworth.Server.Data;
using Dalworth.Server.Domain;
using Dalworth.Server.SDK;
using Dalworth.Server.Web.Partner.Models;

namespace Dalworth.Server.Web.Partner.Controllers
{
    public class AccountController : Controller
    {        
        public IFormsAuthenticationService FormsService { get; set; }
        public IMembershipService MembershipService { get; set; }

        protected override void Initialize(RequestContext requestContext)
        {   
            if (FormsService == null) { FormsService = new FormsAuthenticationService(); }
            if (MembershipService == null) { MembershipService = new AccountMembershipService(); }
            base.Initialize(requestContext);
        }

        // **************************************
        // URL: /Account/LogIn
        // **************************************

        public ActionResult LogIn()
        {
            if (TempData.ContainsKey("SuccessMessage"))
            {
                ViewData["SuccessMessage"] = TempData["SuccessMessage"];
                TempData.Remove("SuccessMessage");
            }

            if (TempData.ContainsKey("FailMessage"))
            {
                ViewData["FailMessage"] = TempData["FailMessage"];
                TempData.Remove("FailMessage");
            }

            return View();
        }

        [HttpPost]
        public ActionResult LogIn(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {   
                if (MembershipService.ValidateUser(model.UserName, model.Password))
                {                                        
                    FormsService.SignIn(model.UserName, model.RememberMe);
                    if (Url.IsLocalUrl(returnUrl))
                        return Redirect(returnUrl);
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Email or password provided is incorrect");
            }
            else
                ModelState.AddModelError("", "Please enter Email and password");            
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        // **************************************
        // URL: /Account/LogOff
        // **************************************

        public ActionResult LogOff()
        {
            FormsService.SignOut();

            return RedirectToAction("Index", "Home");
        }

        public ActionResult ForgotPassword()
        {
            ModelState.Clear();
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(ForgotPasswordModel model)
        {            
            if (ModelState.IsValid)
            {
                WebUser webUser;
                using (IDbConnection connection = Connection.Instance.GetTemporaryDbConnection())
                {
                    connection.Open();
                    webUser = WebUser.FindByLogin(model.EmailAddress, connection);
                }

                if (webUser == null)
                {                    
                    ModelState.AddModelError("", "Sorry, we couldn't find anyone with that email address");
                    return View(model);
                }
                
                PartnerInvitation invitation = new PartnerInvitation(
                    Guid.NewGuid().ToString().Replace("-", string.Empty),
                    webUser.OrderSourceId, webUser.ID, model.EmailAddress, DateTime.Now.AddMinutes(30), false);
                BackgroundJobPending job = new BackgroundJobPending(0,
                    (int)BackgroundJobTypeEnum.PartnerSitePasswordReminder, null, null, null, invitation.InvitationKey);
                using (IDbConnection connection = Connection.Instance.GetTemporaryDbConnection())
                {
                    connection.Open();
                    IDbTransaction transaction = connection.BeginTransaction();
                    try
                    {                        
                        PartnerInvitation.Insert(invitation, connection);
                        BackgroundJobPending.Insert(job, connection);
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }

                TempData["SuccessMessage"] = "Instructions for password reset have been emailed to you";
                return RedirectToAction("LogIn");
            }                

            return View(model);
        }

        [HttpGet]
        public ActionResult SetupPassword(string key)
        {
            ModelState.Clear();
            TempData["InvitationKey"] = key;
            PartnerInvitation invitation;
            RedirectToRouteResult redirectResult = GetInvitation(key, out invitation);
            if (redirectResult != null)
                return redirectResult;

            SetupPasswordModel model = new SetupPasswordModel();
            model.Invitation = invitation;
            return View(model);
        }

        [HttpPost]
        public ActionResult SetupPassword(SetupPasswordModel newPassword)
        {
            PartnerInvitation invitation;
            string key = (string) TempData["InvitationKey"];
            RedirectToRouteResult redirectResult = GetInvitation(key, out invitation);
            TempData["InvitationKey"] = key;
            if (redirectResult != null)
                return redirectResult;
            newPassword.Invitation = invitation;
            bool isPasswordReset = invitation.IsPasswordReset();

            if (ModelState.IsValid)
            {
                using (IDbConnection connection = Connection.Instance.GetTemporaryDbConnection())
                {
                    connection.Open();
                    IDbTransaction transaction = connection.BeginTransaction();

                    try
                    {
                        if (invitation.WebUserId.HasValue)
                        {
                            WebUser user = WebUser.FindByPrimaryKey(invitation.WebUserId.Value, connection);
                            user.PasswordHash = Hash.ComputeHash(newPassword.Password);
                            WebUser.Update(user, connection);
                        }
                        else
                        {
                            WebUser user = new WebUser(0, invitation.OrderSourceId, invitation.Email, 
                                Hash.ComputeHash(newPassword.Password), null, string.Empty, string.Empty,
                                invitation.Email, !invitation.OrderSourceId.HasValue);
                            WebUser.Insert(user, connection);
                        }
                        
                        BackgroundJobPending.DeleteByInvitation(invitation.InvitationKey, connection);
                        PartnerInvitation.Delete(invitation, connection);
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }

                if (isPasswordReset)
                    TempData["SuccessMessage"] = "Your password has been successfully reset";
                else
                    TempData["SuccessMessage"] = "Your password has been successfully set";
                return RedirectToAction("LogIn");                
            }

            return View(newPassword);
        }
        
        private RedirectToRouteResult GetInvitation(string key, out PartnerInvitation invitation)
        {
            invitation = null;

            if (string.IsNullOrEmpty(key))
                return RedirectToAction("LogIn");                
            
            using (IDbConnection connection = Connection.Instance.GetTemporaryDbConnection())
            {
                connection.Open();
                try
                {
                    invitation = PartnerInvitation.FindByPrimaryKey(key);
                }
                catch (DataNotFoundException) { }
            }

            if (invitation == null)
            {
                TempData["FailMessage"] = "Specified invitation key is unknown";
                return RedirectToAction("LogIn");
            }

            if (invitation.ExpirationDate < DateTime.Now)
            {
                TempData["FailMessage"] = "Specified invitation key is expired";
                return RedirectToAction("LogIn");
            }

            return null;
        }
    }
}
