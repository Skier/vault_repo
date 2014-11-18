using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Dalworth.LeadCentral.Domain;
using Dalworth.LeadCentral.Service;
using Dalworth.LeadCentral.Web.Models;
using Dalworth.LeadCentral.Web.Models.Campaign;

namespace Dalworth.LeadCentral.Web.Controllers
{
    public class CampaignsController : Controller
    {
        //
        // GET: /Campaigns/

        public ActionResult Index(CampaignList model)
        {
            using (var connection = CustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
            {
                model.Load(connection);
                return View(model);
            }
        }

        //
        // GET: /Campaigns/Details/5

        public ActionResult Details(int id)
        {
            using (var connection = CustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
            {
                var model = CampaignService.LoadCampaign(id, connection);
                if (model == null)
                {
                    Flash.GetCurrent().PushError(string.Format("You havn't access to this resource."));
                    return RedirectToAction("Index");
                }
                return View(model);
            }
            
        }

        //
        // GET: /Campaigns/Create

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
                return RedirectToAction("Index");
            }

            try
            {
                var model = new CampaignEdit();
                using (var connection = CustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
                {
                    model.Load(connection);
                    model.CurrentCampaign.DateCreated = DateTime.Now;
                    model.CurrentCampaign.DateStart = DateTime.Now;
                    model.CurrentCampaign.UserId = ContextHelper.GetCurrentUser().Id;
                    if (partnerId != null)
                    {
                        model.DenyChangePartner = true;
                        model.CurrentPartner = BusinessPartnerService.FindById(int.Parse(partnerId), connection);
                        if (model.CurrentPartner != null && model.CurrentPartner.SalesRepId != null)
                            model.CurrentCampaign.UserId = model.CurrentPartner.SalesRepId.Value;
                    }
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                Flash.GetCurrent().PushError(string.Format("Can not Create new Campaign"));
                Flash.GetCurrent().PushError(string.Format("Error: {0}.", ex.Message));

                return RedirectToAction("Index");
            }
        } 

        //
        // POST: /Campaigns/Create

        [HttpPost]
        public ActionResult Create(CampaignEdit model)
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

            if (ModelState.IsValid)
            {
                try
                {
                    using (var connection = CustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
                    {
                        model.Update(connection);
                        ActivityService.Log(string.Format("Campaign {0} was created", model.CampaignName), connection);
                    }
                    Flash.GetCurrent().PushInfo(string.Format("Campaign {0} was created", model.CampaignName));

                    return RedirectToAction("Details", new { controller = "Campaigns", id = model.CurrentCampaign.Id });
                }
                catch (Exception ex)
                {
                    Flash.GetCurrent().PushError(string.Format("Can not Create new Campaign"));
                    using (var connection = CustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
                    {
                        ActivityService.Log("Can not Create new Campaign", connection);
                        ActivityService.Log(string.Format("Error: {0}.", ex.Message), connection);
                    }
                }
            }

            return View(model);
        }
        
        //
        // GET: /Campaigns/Edit/5
 
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
            var model = new CampaignEdit();
            using (var connection = CustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
            {
                model.Load(connection, id);
                return View(model);
            }
        }

        //
        // POST: /Campaigns/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, CampaignEdit model)
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

            if (ModelState.IsValid)
            {
                try
                {
                    using (var connection = CustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
                    {
                        model.Update(connection);
                        ActivityService.Log(string.Format("Campaign {0} was updated", model.CampaignName), connection);
                    }

                    Flash.GetCurrent().PushInfo(string.Format("Campaign {0} was updated", model.CampaignName));
                    return RedirectToAction("Details", new { controller = "Campaigns", id });
                }
                catch (Exception ex)
                {
                    Flash.GetCurrent().PushError(string.Format("Can not update Campaign {0}", model.CampaignName));

                    using (var connection = CustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
                    {

                        ActivityService.Log(string.Format("Can not update Campaign {0}", model.CampaignName), connection);
                        ActivityService.Log(string.Format("Error: {0}.", ex.Message), connection);
                    }
                }
            }
            return View(model);
        }

        //
        // GET: /Phones/AssignToCampaign/5

        public ActionResult AssignPhone(int id)
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

            var model = new AssignPhone();
            try
            {
                using (var connection = CustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
                {
                    model.LoadUnassigned(id, connection);
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                Flash.GetCurrent().PushError(string.Format("Can not load unassigned Tracking Phones "));
                Flash.GetCurrent().PushError(string.Format("Error: {0}.", ex.Message));
            }

            return RedirectToAction("Index");
        }

        public ActionResult AddPhone(int trackingPhoneId, int campaignId)
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
                    TrackingPhoneService.AssignPhoneToCampaign(trackingPhoneId, campaignId, connection);
                    ActivityService.Log(string.Format("Phone [{0}] was assigned to Campaign [{1}]", trackingPhoneId, campaignId), connection);
                }

                Flash.GetCurrent().PushInfo(string.Format("Tracking Phone was added to current Campaign"));
                return RedirectToAction("Details", new { id = campaignId });

            }catch (Exception ex)
            {
                Flash.GetCurrent().PushError(string.Format("Can not add Tracking Phone to current Campaign"));

                using (var connection = CustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
                {
                    ActivityService.Log(string.Format("Can not assign Phone [{0}] to Campaign [{1}]", trackingPhoneId,
                                                      campaignId), connection);
                    ActivityService.Log(string.Format("Error: {0}.", ex.Message), connection);
                }

                return RedirectToAction("AssignPhone", new { campaignId });
            }
        }

        public ActionResult RemovePhone(int trackingPhoneId, int campaignId)
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
                    TrackingPhoneService.RemovePhoneFromCampaign(trackingPhoneId, campaignId, connection);
                    ActivityService.Log(string.Format("Phone [{0}] was un-assigned from Campaign [{1}]", trackingPhoneId,
                                                      campaignId), connection);
                }

                Flash.GetCurrent().PushInfo(string.Format("Tracking Phone was removed from current Campaign"));
                

                return RedirectToAction("Details", new { id = campaignId });
            }
            catch (Exception ex)
            {
                Flash.GetCurrent().PushError(string.Format("Can not remove Tracking Phone from current Campaign"));
                using (var connection = CustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
                {

                    ActivityService.Log(string.Format("Can not un-assign Phone [{0}] from Campaign [{1}]",
                                                      trackingPhoneId, campaignId), connection);
                    ActivityService.Log(string.Format("Error: {0}.", ex.Message), connection);
                }

                return RedirectToAction("Details", new { id = campaignId });
            }
        }

        public ActionResult Stop(int id)
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
                    var campaign = CampaignService.LoadCampaign(id, connection);
                    if (campaign != null)
                    {
                        CampaignService.StopCampaign(id, connection);

                        Flash.GetCurrent().PushInfo(string.Format("Campaign {0} was stopped.", campaign.CampaignName));
                        ActivityService.Log(string.Format("Campaign {0} was stopped.", campaign.CampaignName), connection);
                    }
                    else
                    {
                        Flash.GetCurrent().PushWarning(string.Format("Can not found Campaign."));
                        ActivityService.Log(string.Format("Campaign [{0}] not found.", id), connection);
                    }
                }

                return RedirectToAction("Details", new { id });
            }
            catch (Exception ex)
            {
                Flash.GetCurrent().PushError(string.Format("Can not Stop current Campaign"));
                using (var connection = CustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
                {
                    ActivityService.Log(string.Format("Can not Stop Campaign [{0}].", id), connection);
                    ActivityService.Log(string.Format("Error: {0}.", ex.Message), connection);
                }

                return RedirectToAction("Details", new { id });
            }
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
                return RedirectToAction("Index");
            }

            try
            {
                using (var connection = CustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
                {
                    var campaign = CampaignService.LoadCampaign(id, connection);
                    if (campaign != null)
                    {
                        if (campaign.DateEnd != null)
                        {
                            campaign.DateEnd = null;
                            CampaignService.Save(campaign, connection);
                            Flash.GetCurrent().PushInfo(string.Format("Campaign {0} was re-activated.",
                                                                      campaign.CampaignName));
                            ActivityService.Log(string.Format("Campaign {0} was re-activated.", campaign.CampaignName), connection);
                        }
                        else
                        {
                            Flash.GetCurrent().PushWarning(
                                string.Format("Can not activate current Campaign. Campaign is active."));
                            ActivityService.Log(string.Format("Can not re-activate Campaign {0}. Campaign is active",
                                                              campaign.CampaignName), connection);
                        }
                    }
                    else
                    {
                        Flash.GetCurrent().PushWarning(string.Format("Can not found Campaign."));
                        ActivityService.Log(string.Format("Campaign [{0}] not found.", id), connection);
                    }
                }

                return RedirectToAction("Details", new { id });
            }
            catch (Exception ex)
            {
                Flash.GetCurrent().PushError(string.Format("Can not re-activate current Campaign"));
                using (var connection = CustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
                {
                    ActivityService.Log(string.Format("Can not re-activate Campaign [{0}].", id), connection);
                    ActivityService.Log(string.Format("Error: {0}.", ex.Message), connection);
                }

                return RedirectToAction("Details", new { id });
            }
        }

    }
}
