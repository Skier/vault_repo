using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Dalworth.LeadCentral.Domain;
using Dalworth.LeadCentral.Service;
using Dalworth.LeadCentral.Web.Models;
using Dalworth.LeadCentral.Web.Models.Reports;

namespace Dalworth.LeadCentral.Web.Controllers
{
    public class ReportsController : Controller
    {
        //
        // GET: /Reports/

        public ActionResult Index()
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
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        public ActionResult Signup()
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
                return RedirectToAction("Index", "Home");
            }

            
            var model = new SignupModel();
            using (var connection = CustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
            {
                model.Load(connection);
            }
            Session["SignupModel"] = model;
            return View(model);
        }

        [HttpPost]
        public ActionResult Signup(SignupModel model)
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
                return RedirectToAction("Index", "Home");
            }

            using (var connection = CustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
            {
                model.Load(connection);
            }

            Session["SignupModel"] = model;
            return View(model);
        }

        public void SignupExportToCsv()
        {
            var model = (SignupModel)Session["SignupModel"];
            if (model == null || model.Partners == null || model.Partners.Count == 0)
                return;

            var response = HttpContext.Response;
            response.Clear();

            response.Write("Sales Rep, Signup Date, Business Partner, Phone");
            response.Write(Environment.NewLine);

            foreach (var partner in model.Partners)
            {
                var salesRep = partner.SalesRepStr.Replace(",", string.Empty);
                var name = partner.PartnerName.Replace(",", string.Empty);
                var phone = partner.Phone.Replace(",", string.Empty);
                response.Write(string.Format("{0}, {1:d}, {2}, {3}", salesRep, partner.DateCreated, name, phone));
                response.Write(Environment.NewLine);
            }

            response.ContentType = "text/csv";
            response.AppendHeader("Content-Disposition", "attachment; filename=BusinessPartnerSignup.csv");
            response.End();
        }

        public ActionResult IncomingCalls()
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
                return RedirectToAction("Index", "Home");
            }

            var model = new IncomingCallsModel();
            using (var connection = CustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
            {
                model.Load(connection);
            }
            Session["IncomingCallsModel"] = model;
            return View(model);
        }

        [HttpPost]
        public ActionResult IncomingCalls(IncomingCallsModel model)
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
                return RedirectToAction("Index", "Home");
            }

            using (var connection = CustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
            {
                model.Load(connection);
            }
            Session["IncomingCallsModel"] = model;
            return View(model);
        }

        public void IncomingCallsExportToCsv()
        {
            var model = (IncomingCallsModel)Session["IncomingCallsModel"];
            if (model == null || model.Leads == null || model.Leads.Count == 0)
                return;

            var response = HttpContext.Response;
            response.Clear();

            response.Write("Sales Rep, Business Partner, Campaign, Call Date, Customer, Call Status");
            response.Write(Environment.NewLine);

            foreach (var lead in model.Leads)
            {
                var salesRep = string.Empty;
                var businessPartner = string.Empty;
                var campaign = string.Empty;

                var status = lead.IsRealLead ? "Lead" : "Cancel";

                if (lead.RelatedCampaign != null)
                {
                    campaign = lead.RelatedCampaign.CampaignName.Replace(",", string.Empty);
                    if (lead.RelatedCampaign.RelatedBusinessPartner != null)
                    {
                        businessPartner = lead.RelatedCampaign.RelatedBusinessPartner.PartnerName.Replace(",", string.Empty);
                        if (lead.RelatedCampaign.RelatedBusinessPartner.RelatedSalesRep != null)
                        {
                            salesRep = lead.RelatedCampaign.RelatedBusinessPartner.RelatedSalesRep.Name.Replace(",", string.Empty);
                        }
                    }
                }

                response.Write(string.Format("{0}, {1}, {2}, {3:d}, {4}, {5}", 
                    salesRep, businessPartner, campaign, lead.DateCreated, lead.Name, status));
                response.Write(Environment.NewLine);
            }

            response.ContentType = "text/csv";
            response.AppendHeader("Content-Disposition", "attachment; filename=IncomingCalls.csv");
            response.End();
        }

        public ActionResult PartnersRevenue()
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
                return RedirectToAction("Index", "Home");
            }

           
            using (var connection = CustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
            {
                var model = new PartnersRevenueModel();
                model.Load(connection);
                Session["PartnersRevenueModel"] = model;
                return View(model);
            }
            
        }

        [HttpPost]
        public ActionResult PartnersRevenue(PartnersRevenueModel model)
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
                return RedirectToAction("Index", "Home");
            }

            using (var connection = CustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
            {
                model.Load(connection);
            }

            Session["PartnersRevenueModel"] = model;
            return View(model);
        }

        public void PartnersRevenueExportToCsv()
        {
            var model = (PartnersRevenueModel)Session["PartnersRevenueModel"];
            if (model == null || model.Leads == null || model.Leads.Count == 0)
                return;

            var response = HttpContext.Response;
            response.Clear();

            response.Write("Sales Rep, Business Partner, Campaign, Call Date, Invoice Number, Invoice Date, Amount");
            response.Write(Environment.NewLine);

            foreach (var lead in model.Leads)
            {
                var salesRep = string.Empty;
                var businessPartner = string.Empty;
                var campaign = string.Empty;

                if (lead.RelatedCampaign != null)
                {
                    campaign = lead.RelatedCampaign.CampaignName.Replace(",", string.Empty);
                    if (lead.RelatedCampaign.RelatedBusinessPartner != null)
                    {
                        businessPartner = lead.RelatedCampaign.RelatedBusinessPartner.PartnerName.Replace(",", string.Empty);
                        if (lead.RelatedCampaign.RelatedBusinessPartner.RelatedSalesRep != null)
                        {
                            salesRep = lead.RelatedCampaign.RelatedBusinessPartner.RelatedSalesRep.Name.Replace(",", string.Empty);
                        }
                    }
                }

                if (lead.QbInvoices.Count > 0)
                {
                    foreach(var invoice in lead.QbInvoices)
                    {
                        response.Write(string.Format("{0}, {1}, {2}, {3:d}, {4}, {5}, {6}",
                            salesRep, businessPartner, campaign, lead.DateCreated, invoice.IdsInvoiceNumber, invoice.IdsInvoiceDateCreatedStr, invoice.Amount));
                        response.Write(Environment.NewLine);
                    }
                }
            }

            response.ContentType = "text/csv";
            response.AppendHeader("Content-Disposition", "attachment; filename=PartnersRevenue.csv");
            response.End();
        }
    }
}
