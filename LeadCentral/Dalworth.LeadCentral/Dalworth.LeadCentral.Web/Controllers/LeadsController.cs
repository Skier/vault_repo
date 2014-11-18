using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Dalworth.Common.SDK;
using Dalworth.LeadCentral.Domain;
using Dalworth.LeadCentral.Service;
using Dalworth.LeadCentral.Web.Models;
using Dalworth.LeadCentral.Web.Models.Lead;

namespace Dalworth.LeadCentral.Web.Controllers
{
    public class LeadsController : Controller
    {
        public ActionResult Index(LeadList model)
        {
            var currentUser = ContextHelper.GetCurrentUser();
            using (var connection = CustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
            {
                model.InitLeads(currentUser, connection);
            }
            return View(model);
        }

        public ActionResult LeadDetails(int id)
        {
            Lead model;
            var currentUser = ContextHelper.GetCurrentUser();
            using (var connection = CustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
            {
                model = LeadService.FindByPrimaryKey(id, currentUser, connection);
            }

            if (model == null)
            {
                Flash.GetCurrent().PushError(string.Format("You havn't access to this resource."));
                return RedirectToAction("Index");
            }
            return View(model);
        }

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
                return RedirectToAction("Index");
            }

            var model = new LeadEdit();
            using (var connection = CustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
            {
                model.Load(connection);
            }
            model.CurrentLead = new Lead();
            return View(model);
        } 
        [HttpPost]
        public ActionResult Create(LeadEdit model)
        {
            var allowedRoles = new List<UserRoleEnum>
                                   {
                                       UserRoleEnum.Administrator,
                                       UserRoleEnum.Staff
                                   };

            if (ContextHelper.ActionDenied(allowedRoles))
            {
                Flash.GetCurrent().PushError(string.Format("You havn't access to this resource."));
                return RedirectToAction("Index");
            }

            try
            {
                model.CurrentLead.LeadStatusId = (int)LeadStatusEnum.New;
                Flash.GetCurrent().PushInfo(string.Format("Lead {0} was succesfully created", model.CurrentLead.Name));
                var logMessage = string.Format("Lead {0} was succesfully created", model.CurrentLead.Name);

                using (var connection = CustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
                {
                    LeadService.Create(model.CurrentLead, connection);

                    ActivityService.Log(logMessage, connection);
                }

                return RedirectToAction("LeadDetails", new { model.CurrentLead.Id });
            }
            catch (Exception ex)
            {
                Flash.GetCurrent().PushError(string.Format("Can not create lead {0}.", model.CurrentLead.Name));
                using (var connection = CustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
                {
                    ActivityService.Log(string.Format("Can not create lead {0}.", model.CurrentLead.Name), connection);
                    ActivityService.Log(string.Format("Error: {0}.", ex.Message), connection);
                }

                return View(model);
            }
        }
        
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
                return RedirectToAction("Index");
            }

            var currentUser = ContextHelper.GetCurrentUser();
            var model = new LeadEdit();
            using (var connection = CustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
            {
                model.Load(connection);
                model.CurrentLead = LeadService.FindByPrimaryKey(id,currentUser, connection, false);
            }

            if (model.CurrentLead == null)
            {
                Flash.GetCurrent().PushError(string.Format("You havn't access to this resource."));
                return RedirectToAction("Index");
            }

            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(int id, LeadEdit model)
        {
            var allowedRoles = new List<UserRoleEnum>
                                   {
                                       UserRoleEnum.Administrator,
                                       UserRoleEnum.Staff
                                   };
            if (ContextHelper.ActionDenied(allowedRoles))
            {
                Flash.GetCurrent().PushError(string.Format("You havn't access to this resource."));
                return RedirectToAction("Index");
            }

            try
            {

                string message;
                using (var connection = CustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
                {
                    Lead.Update(model.CurrentLead, connection);
                    message = string.Format("Lead {0} was succesfully updated", model.CurrentLead.LastName);    
                    ActivityService.Log(message, connection);
                }
                Flash.GetCurrent().PushInfo(message);
                return RedirectToAction("LeadDetails", new { model.CurrentLead.Id });
            }
            catch(Exception ex)
            {
                Flash.GetCurrent().PushError(string.Format("Can not update lead {0}.", model.CurrentLead.Name));
                using (var connection = CustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
                {
                    ActivityService.Log(string.Format("Can not update lead {0}.", model.CurrentLead.Name), connection);
                    ActivityService.Log(string.Format("Error: {0}.", ex.Message), connection);
                }

                return View(model);
            }
        }

        public ActionResult MatchToInvoices(int id)
        {
            var allowedRoles = new List<UserRoleEnum>
                                   {
                                       UserRoleEnum.Administrator,
                                       UserRoleEnum.Staff
                                   };
            if (ContextHelper.ActionDenied(allowedRoles))
            {
                Flash.GetCurrent().PushError(string.Format("You havn't access to this resource."));
                return RedirectToAction("Index");
            }

            var model = new MatchInvoice(id);
            try
            {
                Domain.User currentUser = ContextHelper.GetCurrentUser();
                using (var connection = CustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
                {
                    model.RetrieveInvoices(currentUser, connection);
                }

                return View(model);
            }
            catch (Exception ex)
            {
                Flash.GetCurrent().PushError(string.Format("Can not retrieve Quickbooks invoices."));
                using (var connection = CustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
                {
                    ActivityService.Log(string.Format("Can not retrieve Quickbooks invoices for [{0}] Lead.",
                                                      model.LeadId), connection);
                    ActivityService.Log(string.Format("Error: {0}.", ex.Message), connection);

                }
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult GetQbInvoices(MatchInvoice model)
        {
            var allowedRoles = new List<UserRoleEnum>
                                   {
                                       UserRoleEnum.Administrator,
                                       UserRoleEnum.Staff
                                   };
            if (ContextHelper.ActionDenied(allowedRoles))
            {
                Flash.GetCurrent().PushError(string.Format("You havn't access to this resource."));
                return RedirectToAction("Index");
            }

            try
            {
                var currentUser = ContextHelper.GetCurrentUser();

                using (var connection = CustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
                {
                    model.RetrieveInvoices(currentUser, connection);
                }
                return View("MatchToInvoices", model);
            }
            catch (Exception ex)
            {
                Flash.GetCurrent().PushError(string.Format("Can not retrieve Quickbooks invoices."));
                using (var connection = CustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
                {
                    ActivityService.Log(string.Format("Can not retrieve Quickbooks invoices for [{0}] Lead.",
                                                      model.LeadId), connection);
                    ActivityService.Log(string.Format("Error: {0}.", ex.Message), connection);
                }

                return View("MatchToInvoices", model);
            }
        }

        [HttpPost]
        public ActionResult MatchInvoices(string[] matchInvoices, MatchInvoice model)
        {
            var allowedRoles = new List<UserRoleEnum>
                                   {
                                       UserRoleEnum.Administrator,
                                       UserRoleEnum.Staff
                                   };
            if (ContextHelper.ActionDenied(allowedRoles))
            {
                Flash.GetCurrent().PushError(string.Format("You havn't access to this resource."));
                return RedirectToAction("Index");
            }


            try
            {
                var currentUser = ContextHelper.GetCurrentUser();
                using (var connection = CustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
                {
                    if (matchInvoices == null)
                    {
                        Flash.GetCurrent().PushWarning(string.Format("No any Invoice selected"));
                        model.RetrieveInvoices(currentUser, connection);
                        return View("MatchToInvoices", model);
                    }

                    foreach (var item in matchInvoices)
                    {
                        QbInvoiceService.MatchIdsInvoiceToLeadId(item, model.LeadId, connection);
                    }

                    var lead = LeadService.FindByPrimaryKey(model.LeadId, currentUser, connection);
                    if (lead.LeadStatusId != (int) LeadStatusEnum.Converted)
                    {
                        lead.LeadStatusId = (int) LeadStatusEnum.Converted;
                        Lead.Update(lead, connection);
                    }

                    ActivityService.Log(string.Format("Lead [{1}] was succesfully matched to {0} invoices", matchInvoices.Length, model.LeadId), connection);
                }

                Flash.GetCurrent().PushInfo(string.Format("Lead was succesfully matched to {0} invoices", matchInvoices.Length));
            }
            catch (Exception ex)
            {
                Flash.GetCurrent().PushError(string.Format("Can not match Quickbooks invoices."));
                using (var connection = CustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
                {
                    ActivityService.Log(string.Format("Can not match Quickbooks invoices to [{0}] Lead.", model.LeadId), connection);
                    ActivityService.Log(string.Format("Error: {0}.", ex.Message), connection);
                }

                return View("MatchToInvoices", model);
            }

            return RedirectToAction("LeadDetails", new { Id = model.LeadId });
        }

        public ActionResult UnmatchQbInvoice(int id, int leadId)
        {
            var allowedRoles = new List<UserRoleEnum>
                                   {
                                       UserRoleEnum.Administrator,
                                       UserRoleEnum.Staff
                                   };
            if (ContextHelper.ActionDenied(allowedRoles))
            {
                Flash.GetCurrent().PushError(string.Format("You havn't access to this resource."));
                return RedirectToAction("Index");
            }

            try
            {
                using (var connection = CustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
                {
                    QbInvoiceService.UnMatchQbInvoice(id, connection);
                    ActivityService.Log(string.Format("Invoice was succesfully un-matched from Lead [{0}]", id), connection);
                }
                Flash.GetCurrent().PushInfo(string.Format("Invoice was succesfully un-matched from current Lead"));
            }
            catch (Exception ex)
            {
                Flash.GetCurrent().PushError(string.Format("Can not un-match Quickbooks invoice."));
                using (var connection = CustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
                {

                    ActivityService.Log(string.Format("Can not un-match Quickbooks invoices."), connection);
                    ActivityService.Log(string.Format("Error: {0}.", ex.Message), connection);
                }

                return RedirectToAction("LeadDetails", new { Id = leadId });
            }

            

            return RedirectToAction("LeadDetails", new { Id = leadId });
        }

        public ActionResult BlockPhoneNumber(int leadId, string phone)
        {
            var allowedRoles = new List<UserRoleEnum>
                                   {
                                       UserRoleEnum.Administrator,
                                       UserRoleEnum.Staff
                                   };
            if (ContextHelper.ActionDenied(allowedRoles))
            {
                Flash.GetCurrent().PushError(string.Format("You havn't access to this resource."));
                return RedirectToAction("Index");
            }

            try
            {
                var blackPhone = new PhoneBlackList
                                     {
                                         PhoneNumber = phone,
                                         PhoneDigits = StringUtil.ExtractLastSevenDigits(phone),
                                         Description = string.Format("Added by {0} from lead detail screen.",
                                                                     ContextHelper.GetCurrentUser().ScreenName)
                                     };

                using (var connection = CustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
                {

                    PhoneBlackList.Save(blackPhone, connection);
                    ActivityService.Log(string.Format("Add phone number {0} to black list.", phone), connection);
                }

                Flash.GetCurrent().PushInfo(string.Format("Phone number {0} is added to black list.", phone));
            }
            catch (Exception ex)
            {
                Flash.GetCurrent().PushError(string.Format("Can not block phone number."));
                using (var connection = CustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
                {
                    ActivityService.Log(string.Format("Can not block phone number {0}.", phone), connection);
                    ActivityService.Log(string.Format("Error: {0}.", ex.Message), connection);
                }
            }

            return RedirectToAction("LeadDetails", new { Id = leadId });
        }

    }
}
