using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Dalworth.LeadCentral.Domain;
using Dalworth.LeadCentral.Service;
using Dalworth.LeadCentral.Web.Models;
using Dalworth.LeadCentral.Web.Models.Partner;

namespace Dalworth.LeadCentral.Web.Controllers
{
    public class PartnersController : Controller
    {
        //
        // GET: /Partners/

        public ActionResult Index(PartnerList model)
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
                return View(model);
            }
        }

        //
        // GET: /Partners/Details/5

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
                var model = BusinessPartnerService.FindById(id, connection);
                if (model != null)
                {
                    model.Campaigns = CampaignService.GetByBusinessPartnerId(id, connection);
                    model.Users = Domain.User.FindActiveByBusinessPartnerId(id, connection);
                    return View(model);
                }

                ActivityService.Log(string.Format("Can not find Partner [{0}].", id), connection);
            }

            Flash.GetCurrent().PushWarning(string.Format("Can not find Partner"));

            return RedirectToAction("Index");
        }

        //
        // GET: /Partners/Create

        public ActionResult Create()
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

            var model = new PartnerEdit();
            using (var connection = CustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
            {
                model.Load(null, connection);
            }
            return View(model);
        } 

        //
        // POST: /Partners/Create

        [HttpPost]
        public ActionResult Create(PartnerEdit model)
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

            if (!ModelState.IsValid)
                return View(model);
            
            try
            {
                using (var connection = CustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
                {
                    model.Update(connection);
                    ActivityService.Log(string.Format("Partner {0} was created.", model.PartnerName), connection);
                }

                Flash.GetCurrent().PushInfo(string.Format("Partner {0} was successfully created.", model.PartnerName));
                return RedirectToAction("Details", new {controller = "Partners", id = model.PartnerId});
            }
            catch (Exception ex)
            {
                Flash.GetCurrent().PushError(string.Format("Can not create Partner"));

                using (var connection = CustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
                {
                    ActivityService.Log(string.Format("Can not create Partner {0}.", model.PartnerName), connection);
                    ActivityService.Log(string.Format("Error: {0}.", ex.Message), connection);
                }

                return View(model);
            }
        }
        
        //
        // GET: /Partners/Edit/5
 
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
                var model = new PartnerEdit();
                using (var connection = CustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
                {
                    model.Load(id, connection);
                    if (model.IsRemoved)
                    {
                        Flash.GetCurrent().PushError(string.Format("Can not edit removed Partner Info."));
                        ActivityService.Log(string.Format("Can not edit removed Partner [{0}].", id),connection);

                        return RedirectToAction("Details", new {id});
                    }
                    return View(model);
                }
            }
            catch(Exception ex)
            {
                Flash.GetCurrent().PushError(string.Format("Can not load Partner Info."));
                using (var connection = CustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
                {
                    ActivityService.Log(string.Format("Can not load Partner [{0}].", id), connection);
                    ActivityService.Log(string.Format("Error: {0}.", ex.Message), connection);
                }

                return RedirectToAction("Details", new { id });
            }
        }

        //
        // POST: /Partners/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, PartnerEdit model)
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
                    using (var connection = CustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
                    {
                        model.Update(connection);
                        ActivityService.Log(string.Format("Partner {0} was updated.", model.PartnerName), connection);
                    }
                    Flash.GetCurrent().PushInfo(string.Format("Partner {0} was successfully updated.", model.PartnerName));

                    return RedirectToAction("Details", new { id });
                }
                catch (Exception ex)
                {
                    Flash.GetCurrent().PushError(string.Format("Can not update Partner Info."));
                    using (var connection = CustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
                    {
                        ActivityService.Log(string.Format("Can not update Partner [{0}].", id), connection);
                        ActivityService.Log(string.Format("Error: {0}.", ex.Message), connection);
                    }
                }
            }

            return View(model);
        }

        public ActionResult Delete(int id)
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
                var model = new PartnerEdit();
                using (var connection = CustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
                {
                    model.Load(null, connection);
                    model.Delete(id, connection);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Flash.GetCurrent().PushError(string.Format("Can not delete Partner."));
                using (var connection = CustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
                {
                    ActivityService.Log(string.Format("Can not delete Partner [{0}].", id), connection);
                    ActivityService.Log(string.Format("Error: {0}.", ex.Message), connection);
                }

                return RedirectToAction("Details", new { id });
            }
        }

        public ActionResult CreatePhoneNumber(int partnerId)
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

            var model = new EditPhoneNumber {PartnerId = partnerId};
            return View(model);
        }

        [HttpPost]
        public ActionResult CreatePhoneNumber(EditPhoneNumber model)
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
                    using (var connection = CustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
                    {
                        model.Update(connection);
                        ActivityService.Log(string.Format("Partner phone {0} was created.", model.PhoneNumber), connection);
                    }

                    Flash.GetCurrent().PushInfo(string.Format("Partner phone {0} was successfully created.",
                                                                  model.PhoneNumber));

                    return RedirectToAction("Details", new { id = model.PartnerId });
                }
                catch (Exception ex)
                {
                    Flash.GetCurrent().PushError(string.Format("Can not create Partner phone."));
                    using (var connection = CustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
                    {
                        ActivityService.Log(string.Format("Can not create phone for Partner [{0}].", model.PartnerId), connection);
                        ActivityService.Log(string.Format("Error: {0}.", ex.Message), connection);
                    }
                }
            }

            return View(model);
        }

        public ActionResult EditPhoneNumber(int id, int partnerId)
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
                var model = new EditPhoneNumber();
                using (var connection = CustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
                {
                    model.Load(id, connection);
                }
                return View(model);
            }
            catch (Exception ex)
            {
                Flash.GetCurrent().PushError(string.Format("Can not load Partner Phone."));
                using (var connection = CustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
                {
                    ActivityService.Log(string.Format("Can not load Partner Phone [{0}].", id), connection);
                    ActivityService.Log(string.Format("Error: {0}.", ex.Message), connection);
                }

                return RedirectToAction("Details", new { id = partnerId });
            }
        }

        [HttpPost]
        public ActionResult EditPhoneNumber(int id, EditPhoneNumber model)
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
                    using (var connection = CustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
                    {
                        model.Update(connection);
                        ActivityService.Log(string.Format("Partner phone {0} was updated.", model.PhoneNumber), connection);
                    }
                    
                    Flash.GetCurrent().PushInfo(string.Format("Partner phone {0} was successfully updated.", model.PhoneNumber));

                    return RedirectToAction("Details", new { id = model.PartnerId });
                }
                catch (Exception ex)
                {
                    Flash.GetCurrent().PushError(string.Format("Can not update Partner phone."));

                    using (var connection = CustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
                    {
                        ActivityService.Log(string.Format("Can not update Partner phone [{0}].", id), connection);
                        ActivityService.Log(string.Format("Error: {0}.", ex.Message), connection);
                    }
                }
            }

            return View(model);
        }

        public ActionResult DeletePhoneNumber(int id, int partnerId)
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
                var model = new EditPhoneNumber();
                using (var connection = CustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
                {
                    model.Load(id, connection);
                    model.Delete(connection);
                    ActivityService.Log(string.Format("Delete Partner Phone [{0}].", id), connection);
                }

                Flash.GetCurrent().PushError(string.Format("Partner Phone was deleted."));
            }
            catch (Exception ex)
            {
                Flash.GetCurrent().PushError(string.Format("Can not load Partner Phone."));
                using (var connection = CustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
                {
                    ActivityService.Log(string.Format("Can not load Partner Phone [{0}].", id), connection);
                    ActivityService.Log(string.Format("Error: {0}.", ex.Message), connection);
                }
            }

            return RedirectToAction("Details", new { id = partnerId });
        }
    }
}
