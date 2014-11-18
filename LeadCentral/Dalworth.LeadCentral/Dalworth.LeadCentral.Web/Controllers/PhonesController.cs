using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Dalworth.LeadCentral.Domain;
using Dalworth.LeadCentral.Service;
using Dalworth.LeadCentral.Web.Models;
using Dalworth.LeadCentral.Web.Models.Phone;

namespace Dalworth.LeadCentral.Web.Controllers
{
    public class PhonesController : Controller
    {
        //
        // GET: /Phones/

        public ActionResult Index()
        {
            var model = new PhoneList();
            using (var connection = CustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
            {
                model.LoadActive(connection);
            }
            return View(model);
        }

        //
        // GET: /Phones/Details/5

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
                return RedirectToAction("Index");
            }

            TrackingPhone model = null;
            try
            {
                using (var connection = CustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
                {
                    model = TrackingPhoneService.GetById(id, connection);
                }
            } catch(Exception ex)
            {
                Flash.GetCurrent().PushError(string.Format("Can not get TrackingPhone {0}.", id));

                using (var connection = CustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
                {
                    ActivityService.Log(string.Format("Can not get TrackingPhone {0}.", id), connection);
                    ActivityService.Log(string.Format("Error: {0}.", ex.Message), connection);
                }
            }
            if (model != null)
                return View(model);
           
            return RedirectToAction("Index");
        }

        public ActionResult StartPurchase(string returnController, string returnAction, int returnId)
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

            Session["PurchasePhoneReturnController"] = returnController;
            Session["PurchasePhoneReturnAction"] = returnAction;

            if(returnId == 0)
                Session["PurchasePhoneReturnId"] = null;
            else
                Session["PurchasePhoneReturnId"] = returnId;

            return RedirectToAction("SelectArea");
        }

        public ActionResult SelectArea()
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

            return View();
        }

        //
        // POST: /Phones/GetAvailableNumbers
        public ActionResult SelectAvailablePhone(string area, bool? isTollFree)
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

            if (area == null || isTollFree == null)
                return RedirectToAction("Index");

            Session["PurchasePhoneSelectedArea"] = area;
            Session["PurchasePhoneIsTollFree"] = isTollFree;

            var model = new AvailableNumbers();

            try
            {
                model.LoadAvailableNumbers(area, isTollFree.Value);
                return View(model);
            }
            catch (Exception ex)
            {
                Flash.GetCurrent().PushError(string.Format("Can not load Phone Numbers for areaCode {0}.", area));

                using (var connection = CustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
                {
                    ActivityService.Log(string.Format("Can not load Phone Numbers for areaCode {0}.", area), connection);
                    ActivityService.Log(string.Format("Error: {0}.", ex.Message), connection);
                }
                return View(model);
            }
        }

        //
        // GET: /Phones/Purchase

        public ActionResult Purchase(string phoneNo, string friendlyNo, string redirectNo, bool isTollfree)
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
                var trackingPhone = new TrackingPhone
                                        {
                                            PhoneNumber = phoneNo,
                                            FriendlyNumber = friendlyNo,
                                            RedirectPhoneNumber = redirectNo,
                                            IsTollFree = isTollfree
                                        };

                using (var connection = CustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
                {
                    TrackingPhoneService.PurchasePhoneNumber(trackingPhone, connection);
                    ActivityService.Log(string.Format("TrackingPhone {0} was purchased.", trackingPhone.FriendlyNumber), connection);
                }

                var returnController = (string) Session["PurchasePhoneReturnController"];
                if (returnController != null)
                    Session["PurchasePhoneReturnController"] = null;
                else
                    returnController = "Phones";

                var returnAction = (string)Session["PurchasePhoneReturnAction"];
                if (returnAction != null)
                    Session["PurchasePhoneReturnAction"] = null;
                else
                    returnAction = "Index";

                Flash.GetCurrent().PushInfo(string.Format("TrackingPhone {0} was created.", trackingPhone.FriendlyNumber));
                

                var returnId = (int?)Session["PurchasePhoneReturnId"];
                if (returnId != null)
                {
                    Session["PurchasePhoneReturnId"] = null;
                    return RedirectToAction(returnAction, returnController, new { id = returnId });
                } 

                return RedirectToAction(returnAction, returnController);
            }
            catch (Exception ex)
            {
                Flash.GetCurrent().PushError(string.Format("Can not purchase Phone Numbers {0}.", friendlyNo));

                using (var connection = CustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
                {
                    ActivityService.Log(string.Format("Can not purchase Phone Numbers {0}.", friendlyNo), connection);
                    ActivityService.Log(string.Format("Error: {0}.", ex.Message), connection);
                }

                var area = (string)Session["PurchasePhoneSelectedArea"];
                var isTollFree = (bool)Session["PurchasePhoneIsTollFree"];

                return RedirectToAction("SelectAvailablePhone", "Phones", new { area, isTollFree});
            }
        }

        //
        // GET: /Phones/Edit/5
 
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

            var model = new PhoneEdit();
            try
            {
                using (var connection = CustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
                {
                    model.Load(id, connection);
                }
                return View(model);
            }
            catch (Exception ex)
            {
                Flash.GetCurrent().PushError(string.Format("Can not get TrackingPhone {0}.", id));
                using (var connection = CustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
                {
                    ActivityService.Log(string.Format("Can not get TrackingPhone {0}.", id), connection);
                    ActivityService.Log(string.Format("Error: {0}.", ex.Message), connection);
                }

                return RedirectToAction("Index");
            }
        }

        //
        // POST: /Phones/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, PhoneEdit model)
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
                        ActivityService.Log(string.Format("TrackingPhone {0} was updated.", model.FriendlyNumber), connection);
                    }
                    Flash.GetCurrent().PushInfo(string.Format("TrackingPhone {0} was updated.", model.FriendlyNumber));
                    

                    return RedirectToAction("Details", "Phones", new { id = model.Id });
                }
                catch (Exception ex)
                {
                    Flash.GetCurrent().PushError(string.Format("Can not update TrackingPhone {0}.", model.FriendlyNumber));
                    using (var connection = CustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
                    {
                        ActivityService.Log(string.Format("Can not update TrackingPhone {0}.", model.FriendlyNumber), connection);
                        ActivityService.Log(string.Format("Error: {0}.", ex.Message), connection);
                    }

                    return View(model);
                }
            }

            return View(model);
        }

        public ActionResult Suspend(int id)
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
                TrackingPhone phone;
                using (var connection = CustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
                {
                    phone = TrackingPhoneService.GetById(id, connection);
                    TrackingPhoneService.SuspendPhoneNumber(phone, connection);
                    ActivityService.Log(string.Format("TrackingPhone {0} was suspended.", phone.FriendlyNumber), connection);
                }

                Flash.GetCurrent().PushInfo(string.Format("TrackingPhone {0} was suspended.", phone.FriendlyNumber));
                return RedirectToAction("Details", "Phones", new { id = phone.Id });
            }
            catch (Exception ex)
            {
                Flash.GetCurrent().PushError(string.Format("Can not suspend TrackingPhone."));

                using (var connection = CustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
                {
                    ActivityService.Log(string.Format("Can not suspend TrackingPhone [{0}].", id), connection);
                    ActivityService.Log(string.Format("Error: {0}.", ex.Message), connection);
                }
            }

            return RedirectToAction("Details", "Phones", new { id });
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
                TrackingPhone phone;
                using (var connection = CustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
                {
                    phone = TrackingPhoneService.GetById(id, connection);
                    TrackingPhoneService.ActivatePhoneNumber(phone, connection);
                    ActivityService.Log(string.Format("TrackingPhone {0} was activated.", phone.FriendlyNumber), connection);
                }

                Flash.GetCurrent().PushInfo(string.Format("TrackingPhone {0} was activated.", phone.FriendlyNumber));
                return RedirectToAction("Details", "Phones", new { id = phone.Id });
            }
            catch (Exception ex)
            {
                Flash.GetCurrent().PushError(string.Format("Can not activate TrackingPhone"));
                using (var connection = CustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
                {
                    ActivityService.Log(string.Format("Can not activate TrackingPhone [{0}].", id), connection);
                    ActivityService.Log(string.Format("Error: {0}.", ex.Message), connection);
                }
            }

            return RedirectToAction("Details", "Phones", new { id });
        }

        public ActionResult Release(int id)
        {
            var allowedRoles = new List<UserRoleEnum>
                                   {
                                       UserRoleEnum.Administrator
                                   };
            if (ContextHelper.ActionDenied(allowedRoles))
            {
                Flash.GetCurrent().PushError(string.Format("You havn't access to this resource."));
                return RedirectToAction("Index");
            }

            try
            {
                TrackingPhone phone;
                using (var connection = CustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
                {
                    phone = TrackingPhoneService.GetById(id, connection);
                    TrackingPhoneService.RemovePhoneNumber(phone, connection);
                    ActivityService.Log(string.Format("TrackingPhone {0} was released.", phone.FriendlyNumber), connection);
                }

                Flash.GetCurrent().PushInfo(string.Format("TrackingPhone {0} was released.", phone.FriendlyNumber));
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Flash.GetCurrent().PushError(string.Format("Can not release Tracking Phone."));
                using (var connection = CustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
                {
                    ActivityService.Log(string.Format("Can not release TrackingPhone [{0}].", id), connection);
                    ActivityService.Log(string.Format("Error: {0}.", ex.Message), connection);
                }

                return RedirectToAction("Details", "Phones", new { id });
            }
        }

        public ActionResult TransmitTwilioPhones()
        {
            var allowedRoles = new List<UserRoleEnum>
                                   {
                                       UserRoleEnum.Administrator
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
                    TrackingPhoneService.TransmitTwilioPhones(connection);
                    ActivityService.Log(string.Format("Transmit all existing TrackingPhones to current application."), connection);
                }

                Flash.GetCurrent().PushInfo(
                        string.Format("All Phone Numbers was transmitted to current application."));

            }
            catch (Exception ex)
            {
                Flash.GetCurrent().PushError(string.Format("Can not transmit Phone numbers."));
                using (var connection = CustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
                {
                    ActivityService.Log(string.Format("Can not transmit TrackingPhones."), connection);
                    ActivityService.Log(string.Format("Error: {0}.", ex.Message), connection);
                }
            }

            return RedirectToAction("Index");
        }

    }
}
