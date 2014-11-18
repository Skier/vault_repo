using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Dalworth.LeadCentral.Domain;
using Dalworth.LeadCentral.Service;
using Dalworth.LeadCentral.Web.Models;
using Dalworth.LeadCentral.Web.Models.User;

namespace Dalworth.LeadCentral.Web.Controllers
{
    public class UsersController : Controller
    {
        //
        // GET: /Users/
        public ActionResult Index(UserList model)
        {
            var allowedRoles = new List<UserRoleEnum>
                                   {
                                       UserRoleEnum.Accountant,
                                       UserRoleEnum.Administrator,
                                       UserRoleEnum.Staff
                                   };
            if (ContextHelper.ActionDenied(allowedRoles))
            {
                Flash.GetCurrent().PushError(string.Format("You havn't access to this resource."));
                return RedirectToAction("Dashboard", "Home");
            }
            using (var connection = CustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
            {
                model.Load(connection);
            }
            return View(model);
        }

        //
        // GET: /Users/Details/5

        public ActionResult Details(int id)
        {
            var allowedRoles = new List<UserRoleEnum>
                                   {
                                       UserRoleEnum.Accountant,
                                       UserRoleEnum.Administrator,
                                       UserRoleEnum.Staff
                                   };
            if (ContextHelper.ActionDenied(allowedRoles))
            {
                Flash.GetCurrent().PushError(string.Format("You havn't access to this resource."));
                return RedirectToAction("Dashboard", "Home");
            }

            using (var connection = CustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
            {
                var model = UserService.FindByPrimaryKey(id, connection);
                if (model != null)
                {
                    model.Activities = ActivityService.FindByUserId(model.Id, connection);
                    return View(model);
                }
            }

            Flash.GetCurrent().PushWarning("Can not find User");
            return RedirectToAction("Index");
        }

        //
        // GET: /Users/Create

        public ActionResult Create(string partnerId)
        {
            var allowedRoles = new List<UserRoleEnum>
                                   {
                                       UserRoleEnum.Administrator,
                                       UserRoleEnum.Staff
                                   };
            if (ContextHelper.ActionDenied(allowedRoles))
            {
                Flash.GetCurrent().PushError(string.Format("You havn't access to this resource."));
                return RedirectToAction("Dashboard", "Home");
            }

            var model = new UserEdit();
            var currentUser = ContextHelper.GetCurrentUser();

            model.InitRoles(currentUser.QbRoleName);
            model.UserRole = UserRoleEnum.BusinessPartner.ToString();
            using (var connection = CustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
            {
                model.Load(connection);
                if (partnerId != null)
                {
                    model.DenyChangePartner = true;
                    model.CurrentPartner = BusinessPartnerService.FindById(int.Parse(partnerId), connection);
                }
            }
            return View(model);
        }
         
        // POST: /Users/Create
        [HttpPost]
        public ActionResult Create(UserEdit model)
        {
            var allowedRoles = new List<UserRoleEnum>
                                   {
                                       UserRoleEnum.Administrator,
                                       UserRoleEnum.Staff
                                   };
            if (ContextHelper.ActionDenied(allowedRoles))
            {
                Flash.GetCurrent().PushError(string.Format("Access Denied."));
                return RedirectToAction("Dashboard", "Home");
            }

            if (!ModelState.IsValid)
            {
                using (var connection = CustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
                {
                    model.Load(connection);
                    if (model.PartnerId > 0)
                    {
                        model.DenyChangePartner = true;
                        model.CurrentPartner = BusinessPartnerService.FindById(model.PartnerId, connection);
                    }
                }
                model.InitRoles(ContextHelper.GetCurrentUser().QbRoleName);
                return View(model);
            }
            
            try
            {
                model.UserScreenName = string.Format("{0} {1}", model.UserFirstName, model.UserLastName);
                model.UserDateCreated = DateTime.Now;
                model.UserIsActive = true;

                var user = ContextHelper.GetCurrentUser();
                if (!user.IsAdmin() && model.UserRole == UserRoleEnum.Administrator.ToString())
                    model.UserRole = UserRoleEnum.Staff.ToString();

                using (var connection = CustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
                {
                        model.CreateUser(connection);
                        ActivityService.Log(string.Format("User {0} was successfully created.", model.UserScreenName), connection);
                }
                Flash.GetCurrent().PushInfo(string.Format("User {0} was successfully created.", model.UserScreenName));
                return RedirectToAction("Details", new { controller = "Users", id = model.UserId });
            }
            catch (UserServiceException ex)
            {
                switch(ex.ErrorCode)
                {
                    case UserServiceException.ErrorCodeEnum.UserExists:
                        Flash.GetCurrent().PushError(string.Format("User {0} already exists.", model.UserScreenName));
                        break;
                    case UserServiceException.ErrorCodeEnum.IntuitError:
                        Flash.GetCurrent().PushError(string.Format("Failed to add {0}.", model.UserScreenName));
                        break;
                    case UserServiceException.ErrorCodeEnum.OtherError:
                        Flash.GetCurrent().PushError(string.Format("Failed to add {0}.", model.UserScreenName));
                        break;
                }
            }
            catch (Exception ex)
            {
                Flash.GetCurrent().PushError(string.Format("User {0} can not be created.", model.UserScreenName));
                using (var connection = CustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
                {
                    ActivityService.Log(string.Format("User {0} can not be created.", model.UserScreenName), connection);
                    ActivityService.Log(string.Format("Error: {0}.", ex.Message), connection);
                }
            }

            using (var connection = CustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
            {
                model.Load(connection);
                if (model.PartnerId > 0)
                {
                    model.DenyChangePartner = true;
                    model.CurrentPartner = BusinessPartnerService.FindById(model.PartnerId, connection);
                }
            }

            model.InitRoles(ContextHelper.GetCurrentUser().QbRoleName);
            return View(model);
        }
        
        //
        // GET: /Users/Edit/5
 
        public ActionResult Edit(int id)
        {
            var allowedRoles = new List<UserRoleEnum>
                                   {
                                       UserRoleEnum.Administrator,
                                       UserRoleEnum.Staff
                                   };
            if (ContextHelper.ActionDenied(allowedRoles))
            {
                Flash.GetCurrent().PushError(string.Format("You havn't access to this resource."));
                return RedirectToAction("Dashboard", "Home");
            }

            try
            {
                var model = new UserEdit();
                using (var connection = CustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
                {
                    model.Load(connection, id);
                }
                model.InitRoles(ContextHelper.GetCurrentUser().QbRoleName);
                return View(model);
            }
            catch (Exception ex)
            {
                Flash.GetCurrent().PushError(string.Format("Can not load User Info."));
                using (var connection = CustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
                {
                    ActivityService.Log(string.Format("Can not load User [{0}].", id), connection);
                    ActivityService.Log(string.Format("Error: {0}.", ex.Message), connection);
                }

                return RedirectToAction("Details", new { id });
            }
        }

        //
        // POST: /Users/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, UserEdit model)
        {
            var allowedRoles = new List<UserRoleEnum>
                                   {
                                       UserRoleEnum.Administrator,
                                       UserRoleEnum.Staff
                                   };
            if (ContextHelper.ActionDenied(allowedRoles))
            {
                Flash.GetCurrent().PushError(string.Format("You havn't access to this resource."));
                return RedirectToAction("Dashboard", "Home");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var user = ContextHelper.GetCurrentUser();
                    if (!user.IsAdmin() && model.UserRole == UserRoleEnum.Administrator.ToString())
                        model.UserRole = UserRoleEnum.Staff.ToString();

                    using (var connection = CustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
                    {
                        model.Update(connection);
                        ActivityService.Log(string.Format("User {0} was successfully updated.", model.UserScreenName), connection);
                    }

                    Flash.GetCurrent().PushInfo(string.Format("User {0} was successfully updated.", model.UserScreenName));
                    return RedirectToAction("Details", new { id });
                }
                catch (Exception ex)
                {
                    Flash.GetCurrent().PushError(string.Format("Can not update User Info."));
                    using (var connection = CustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
                    {
                        ActivityService.Log(string.Format("Can not update User [{0}].", id), connection);
                        ActivityService.Log(string.Format("Error: {0}.", ex.Message), connection);
                    }
                }
            }

            model.InitRoles(ContextHelper.GetCurrentUser().QbRoleName);
            return View(model);
        }

        public ActionResult Activate(int id)
        {
            var allowedRoles = new List<UserRoleEnum>
                                   {
                                       UserRoleEnum.Administrator,
                                       UserRoleEnum.Staff
                                   };
            if (ContextHelper.ActionDenied(allowedRoles))
            {
                Flash.GetCurrent().PushError(string.Format("You havn't access to this resource."));
                return RedirectToAction("Dashboard", "Home");
            }

            try
            {
                using (var connection = CustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
                {
                    var user = UserService.FindByPrimaryKey(id, connection);
                    user.IsActive = true;
                    UserService.Activate(user, connection);
                    ActivityService.Log(string.Format("User [{0}] was successfully activated.", id), connection);
                }
                Flash.GetCurrent().PushInfo(string.Format("User was activated."));
            }
            catch (Exception ex)
            {
                Flash.GetCurrent().PushError(string.Format("Can not activate User."));
                using (var connection = CustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
                {
                    ActivityService.Log(string.Format("Can not activate User [{0}].", id), connection);
                    ActivityService.Log(string.Format("Error: {0}.", ex.Message), connection);
                }
            }
            return RedirectToAction("Details", new { id });
        }

        public ActionResult Deactivate(int id)
        {
            var allowedRoles = new List<UserRoleEnum>
                                   {
                                       UserRoleEnum.Administrator,
                                       UserRoleEnum.Staff
                                   };
            if (ContextHelper.ActionDenied(allowedRoles))
            {
                Flash.GetCurrent().PushError(string.Format("You havn't access to this resource."));
                return RedirectToAction("Dashboard", "Home");
            }

            string userName = null;
            try
            {
                using (var connection = CustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
                {
                    var user = UserService.FindByPrimaryKey(id, connection);
                    userName = user.Name;
                    user.IsActive = false;    
                    UserService.Deactivate(user, connection);
                    ActivityService.Log(string.Format("User [{0}] was successfully deactivated.", id), connection);
                }
                
                Flash.GetCurrent().PushInfo(string.Format("User was deactivated."));
            }
            catch (Exception ex)
            {

                Flash.GetCurrent().PushError(string.Format("Failed to deactivate " + (userName != null?userName:string.Empty) + "user."));
                using (var connection = CustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
                {
                    ActivityService.Log(string.Format("Can not deactivate User [{0}].", id), connection);
                    ActivityService.Log(string.Format("Error: {0}.", ex.Message), connection);
                }
            }
            return RedirectToAction("Details", new { id });
        }

        public ActionResult Reinvite(int id)
        {
            var allowedRoles = new List<UserRoleEnum>
                                   {
                                       UserRoleEnum.Administrator,
                                       UserRoleEnum.Staff
                                   };
            if (ContextHelper.ActionDenied(allowedRoles))
            {
                Flash.GetCurrent().PushError(string.Format("You havn't access to this resource."));
                return RedirectToAction("Dashboard", "Home");
            }

            Domain.User user = null;
            try
            {
                using (var connection = CustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
                {
                    user = UserService.FindByPrimaryKey(id, connection);
                    UserService.SendInvitation(user, "");
                    ActivityService.Log(string.Format("User [{0}] was invited.", user.Id), connection);
                }
                Flash.GetCurrent().PushInfo(string.Format("Invitation email was sent to " + user.Name));
            }
            catch (Exception ex)
            {
                Flash.GetCurrent().PushError(string.Format("Failed to invite " + (user != null? user.Name: " user" )));
                using (var connection = CustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
                {
                    ActivityService.Log(string.Format("Can not reinvite User [{0}].", id), connection);
                    ActivityService.Log(string.Format("Error: {0}.", ex.Message), connection);
                }
            }
            return RedirectToAction("Details", new { id });
        }
    }
}
