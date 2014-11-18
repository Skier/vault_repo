using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;
using Dalworth.LeadCentral.Domain;
using Dalworth.LeadCentral.Service;
using Dalworth.LeadCentral.Web.Models;
using Dalworth.LeadCentral.Web.Models.Profile;

namespace Dalworth.LeadCentral.Web.Controllers
{
    public class ProfileController : Controller
    {
        private const string CommitAuthorizeUrlKey = "CommitAuthorizeUrl";

        //
        // GET: /Profile/

        public ActionResult Index()
        {
            var allowedRoles = new List<UserRoleEnum>
                                   {
                                       UserRoleEnum.Administrator
                                   };
            if (ContextHelper.ActionDenied(allowedRoles))
            {
                Flash.GetCurrent().PushError(string.Format("You havn't access to this resource."));
                return RedirectToAction("Dashboard", "Home");
            }

            var model = ContextHelper.GetCurrentCustomer();
            if (model != null)
            {
                return View(model);
            }

            Flash.GetCurrent().PushWarning(string.Format("Can not find Company Info"));

            using (var connection = CustomerService.GetConnection(model))
            {
                ActivityService.Log(string.Format("Can not find Company Info"), connection);
            }

            return RedirectToAction("Dashboard", "Home");
        }

        public ActionResult CompanyEdit()
        {
            var allowedRoles = new List<UserRoleEnum>
                                   {
                                       UserRoleEnum.Administrator
                                   };
            if (ContextHelper.ActionDenied(allowedRoles))
            {
                Flash.GetCurrent().PushError(string.Format("You havn't access to this resource."));
                return RedirectToAction("Dashboard", "Home");
            }

            var customer = ContextHelper.GetCurrentCustomer();
            if (customer != null)
            {
                var model = new CompanyEdit();
                model.Load(customer.Id);

                return View(model);
            }

            Flash.GetCurrent().PushWarning(string.Format("Can not find Company Info"));

            using (var connection = CustomerService.GetConnection(customer))
            {
                ActivityService.Log(string.Format("Can not find Company Info"), connection);
            }

            return RedirectToAction("Dashboard", "Home");
        }

        [HttpPost]
        public ActionResult CompanyEdit(CompanyEdit model)
        {
            var allowedRoles = new List<UserRoleEnum>
                                   {
                                       UserRoleEnum.Administrator
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
                        ActivityService.Log(string.Format("Company {0} profile was updated.", model.CustomerName), connection);
                    }

                    Flash.GetCurrent().PushInfo(string.Format("Company profile was updated."));
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    Flash.GetCurrent().PushError(string.Format("Can not update Company Info"));

                    using (var connection = CustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
                    {
                        ActivityService.Log(string.Format("Can not update Company  {0} [{1}].", model.CustomerName,
                                                          model.CurrentCustomerId), connection);
                        ActivityService.Log(string.Format("Error: {0}.", ex.Message), connection);
                    }

                    return View(model);
                }
            }

            return View(model);
        }

        public ActionResult Balance(TransactionList model)
        {
            var allowedRoles = new List<UserRoleEnum>
                                   {
                                       UserRoleEnum.Administrator
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

        public ActionResult AddFunds()
        {
            var allowedRoles = new List<UserRoleEnum>
                                   {
                                       UserRoleEnum.Administrator
                                   };
            if (ContextHelper.ActionDenied(allowedRoles))
            {
                Flash.GetCurrent().PushError(string.Format("You havn't access to this resource."));
                return RedirectToAction("Dashboard", "Home");
            }

            var model = ContextHelper.GetCurrentCustomer();

            using (var connection = CustomerService.GetConnection(model))
            {
                if (model != null)
                {
                    model.CurrentBalance = BillingService.GetCurrentBalance(connection);
                    return View(model);
                }

                ActivityService.Log(string.Format("Can not find Company profile"), connection);
            }
            Flash.GetCurrent().PushWarning(string.Format("Can not find Company progile"));
            return RedirectToAction("Dashboard", "Home");
        }

        [HttpPost]
        public ActionResult AddFunds(FormCollection collection)
        {
            var allowedRoles = new List<UserRoleEnum>
                                   {
                                       UserRoleEnum.Administrator
                                   };
            if (ContextHelper.ActionDenied(allowedRoles))
            {
                Flash.GetCurrent().PushError(string.Format("You havn't access to this resource."));
                return RedirectToAction("Dashboard", "Home");
            }

            decimal amount;
            if (!decimal.TryParse(collection["Amount"], out amount))
                return RedirectToAction("AddFunds");

            try
            {
                using (var connection = CustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
                {
                    var paymentParams = PaymentService.GetPaymentParams(amount);

                    if (paymentParams.StatusCode == "0")
                    {
                        PaymentService.CreateQbmsTransaction(paymentParams);
                        var redirectUrl = PaymentService.GetPaymentUrl(paymentParams);
                        return Redirect(redirectUrl);
                    }

                    ActivityService.Log(string.Format("Can not Process Payment"), connection);
                    ActivityService.Log(string.Format("Error: {0}.", paymentParams.StatusMessage), connection);
                }

                Flash.GetCurrent().PushError(string.Format("Can not Process Payment"));
                return RedirectToAction("AddFunds");
            }
            catch (Exception ex)
            {
                using (var connection = CustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
                {
                    ActivityService.Log(string.Format("Can not Process Payment"), connection);
                    ActivityService.Log(string.Format("Error: {0}.", ex.Message), connection);
                }

                Flash.GetCurrent().PushError(string.Format("Can not Process Payment"));
                return RedirectToAction("AddFunds");
            }
        }

        public ActionResult ConfirmPayment(string OpId, string OpType, string Status, string StatusCode, string StatusMessage,
            string TxnType, string TxnTimestamp, string MaskedCCN, string AuthCode, decimal Amount, string TxnId)
        {

            var qbmsTransaction = QbmsTransaction.GetByOpId(OpId);
            if (qbmsTransaction == null)
            {
                Flash.GetCurrent().PushError(string.Format("Incorrect Transaction"));
                return RedirectToAction("Index");
            }

            qbmsTransaction.OpType = OpType;
            qbmsTransaction.Status = Status;
            qbmsTransaction.StatusCode = StatusCode;
            qbmsTransaction.StatusMessage = StatusMessage;
            qbmsTransaction.TxnType = TxnType;
            qbmsTransaction.TxnTimestamp = TxnTimestamp;
            qbmsTransaction.MaskedCCN = MaskedCCN;
            qbmsTransaction.AuthCode = AuthCode;
            qbmsTransaction.Amount = Amount;
            qbmsTransaction.TxnId = TxnId;

            var transaction = CustomerService.SaveQbmsTransaction(qbmsTransaction);

            var servmanCustomer = Customer.FindByPrimaryKey(transaction.CustomerId);

            using (var connection = CustomerService.GetConnection(servmanCustomer))
            {
                if (transaction.Status.ToUpper() != "OK")
                {
                    Flash.GetCurrent().PushError(string.Format("Payment was not processed"));
                    ActivityService.Log(string.Format("Payment was not processed"), connection);
                    ActivityService.Log(string.Format("Error: {0}.", transaction.StatusMessage), connection);

                    return RedirectToAction("Index");
                }

                BillingService.CreatePaymentTransaction(connection, transaction.Amount, transaction.Id);
                ActivityService.Log(string.Format("Payment was processed"), connection);
            }

            Flash.GetCurrent().PushInfo(string.Format("Payment success processed"));

            return RedirectToAction("Index");
        }

        public ActionResult Authorize()
        {
            var allowedRoles = new List<UserRoleEnum>
                                   {
                                       UserRoleEnum.Administrator
                                   };
            if (ContextHelper.ActionDenied(allowedRoles))
            {
                Flash.GetCurrent().PushError(string.Format("You havn't access to this resource."));
                return RedirectToAction("Dashboard", "Home");
            }

            CustomerService.SetOAuthConnection(false);

            var customer = ContextHelper.GetCurrentCustomer();
            var oAuthContext = OAuthContext.CreateContext(customer.RealmId);
            oAuthContext.RequestConsumerKeyIfNeeded();

            var grantPageUrl = oAuthContext.GetGrantPageUrl(ConfigurationManager.AppSettings[CommitAuthorizeUrlKey]);

            Session[OAuthContext.OAuthContextKey] = oAuthContext;
            Session[OAuthContext.RealmIdKey] = customer.RealmId;

            return Redirect(grantPageUrl);
        }

        public ActionResult CommitAuthorize()
        {
            var oAuthContext = (OAuthContext)Session[OAuthContext.OAuthContextKey];
            if (oAuthContext == null)
            {
                Flash.GetCurrent().PushError(string.Format("Authorization failed"));
                using (var connection = CustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
                {
                    ActivityService.Log(string.Format("Authorization failed"), connection);
                }
                return RedirectToAction("Index");
            }

            var verifier = oAuthContext.Connector.ParseGrantPageResponseQuery(Request.QueryString.ToString());

            string error;
            if (!oAuthContext.Connector.ValidateVerifier(verifier, out error))
            {
                Flash.GetCurrent().PushError(string.Format("Authorization failed"));
                using (var connection = CustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
                {
                    ActivityService.Log(string.Format("Authorization failed"), connection);
                    ActivityService.Log(string.Format("Error: {0}", error), connection);
                }

                return RedirectToAction("Index");
            }

            oAuthContext.RequestAccessToken(verifier);
            oAuthContext.SetCurrentConnectionActive();

            CustomerService.SetOAuthConnection(true);

            Flash.GetCurrent().PushInfo(string.Format("Authorization success!"));
            using (var connection = CustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
            {
                ActivityService.Log(string.Format("Authorization success!"), connection);
            }

            return RedirectToAction("Index");
        }

        public ActionResult BlackList()
        {
            var allowedRoles = new List<UserRoleEnum>
                                   {
                                       UserRoleEnum.Administrator
                                   };
            if (ContextHelper.ActionDenied(allowedRoles))
            {
                Flash.GetCurrent().PushError(string.Format("You havn't access to this resource."));
                return RedirectToAction("Dashboard", "Home");
            }

            var model = new BlackList();
            using (var connection = CustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
            {
                model.Load(connection);
            }
            return View(model);
        }

        public ActionResult CreateBlackListPhone()
        {
            var allowedRoles = new List<UserRoleEnum>
                                   {
                                       UserRoleEnum.Administrator
                                   };
            if (ContextHelper.ActionDenied(allowedRoles))
            {
                Flash.GetCurrent().PushError(string.Format("You havn't access to this resource."));
                return RedirectToAction("Dashboard", "Home");
            }

            var model = new EditBlackListPhone();
            return View(model);
        }

        [HttpPost]
        public ActionResult CreateBlackListPhone(EditBlackListPhone model)
        {
            var allowedRoles = new List<UserRoleEnum>
                                   {
                                       UserRoleEnum.Administrator
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
                        ActivityService.Log(string.Format("Black List phone {0} was created.", model.PhoneNumber), connection);
                    }

                    Flash.GetCurrent().PushInfo(string.Format("Black List phone {0} was successfully created.", model.PhoneNumber));
                    return RedirectToAction("BlackList");
                }
                catch (Exception ex)
                {
                    Flash.GetCurrent().PushError(string.Format("Can not create Black List phone."));
                    using (var connection = CustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
                    {
                        ActivityService.Log(string.Format("Can not create Black List phone."), connection);
                        ActivityService.Log(string.Format("Error: {0}.", ex.Message),connection);
                    }
                }
            }

            return View(model);
        }

        public ActionResult EditBlackListPhone(int id)
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
                var model = new EditBlackListPhone();
                using (var connection = CustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
                {
                    model.Load(id, connection);
                }
                return View(model);
            }
            catch (Exception ex)
            {
                Flash.GetCurrent().PushError(string.Format("Can not load Black List Phone."));
                using (var connection = CustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
                {
                    ActivityService.Log(string.Format("Can not load Black List Phone [{0}].", id), connection);
                    ActivityService.Log(string.Format("Error: {0}.", ex.Message), connection);
                }

                return RedirectToAction("BlackList");
            }
        }

        [HttpPost]
        public ActionResult EditBlackListPhone(EditBlackListPhone model)
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
                        ActivityService.Log(string.Format("Black List phone {0} was updated.", model.PhoneNumber), connection);
                    }

                    Flash.GetCurrent().PushInfo(string.Format("Black List phone {0} was successfully updated.", model.PhoneNumber));
                    return RedirectToAction("BlackList");
                }
                catch (Exception ex)
                {
                    Flash.GetCurrent().PushError(string.Format("Can not update Black List phone."));
                    using (var connection = CustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
                    {
                        ActivityService.Log(string.Format("Can not update Black List phone [{0}].", model.Id), connection);
                        ActivityService.Log(string.Format("Error: {0}.", ex.Message), connection);
                    }
                }
            }

            return View(model);
        }

        public ActionResult DeleteBlackListPhone(int id)
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
                var model = new EditBlackListPhone();
                using (var connection = CustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
                {
                    model.Delete(id, connection);
                }
            }
            catch (Exception ex)
            {
                Flash.GetCurrent().PushError(string.Format("Can not load Black List Phone."));
                using (var connection = CustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
                {
                    ActivityService.Log(string.Format("Can not load Black List Phone [{0}].", id), connection);
                    ActivityService.Log(string.Format("Error: {0}.", ex.Message), connection);
                }
            }

            return RedirectToAction("BlackList");
        }

        public ActionResult Notifications()
        {
            var allowedRoles = new List<UserRoleEnum>
                                   {
                                       UserRoleEnum.Administrator
                                   };
            if (ContextHelper.ActionDenied(allowedRoles))
            {
                Flash.GetCurrent().PushError(string.Format("You havn't access to this resource."));
                return RedirectToAction("Dashboard", "Home");
            }

            var model = new NotificationList();
            using (var connection = CustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
            {
                model.Load(connection);
            }
            return View(model);
        }

        public ActionResult NotificationSetting()
        {
            var allowedRoles = new List<UserRoleEnum>
                                   {
                                       UserRoleEnum.Administrator
                                   };
            if (ContextHelper.ActionDenied(allowedRoles))
            {
                Flash.GetCurrent().PushError(string.Format("You havn't access to this resource."));
                return RedirectToAction("Dashboard", "Home");
            }

            using (var connection = CustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
            {
                return View(new NotificationSetting(connection));
            }
        }

        [HttpPost]
        public ActionResult NotificationSetting(string[] settings, NotificationSetting model)
        {
            var allowedRoles = new List<UserRoleEnum>
                                   {
                                       UserRoleEnum.Administrator
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
                    model.Update(settings, connection);
                    ActivityService.Log(string.Format("Update Notification settings."), connection);
                }
                Flash.GetCurrent().PushInfo(string.Format("Notification settings was updated successfully."));

            } catch(Exception ex)
            {
                Flash.GetCurrent().PushError(string.Format("Can not update Notification settings."));
                using (var connection = CustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
                {
                    ActivityService.Log(string.Format("Can not update Notification settings."), connection);
                    ActivityService.Log(string.Format("Error: {0}.", ex.Message), connection);
                }
            }

            return RedirectToAction("Notifications");
        }

        public ActionResult InAppCharge(decimal amount, string description)
        {
            var platform = GetIntuitPlatform();

            string errorMessage;
            try
            {
                var requestToken = platform.GetRequestToken(amount, description, out errorMessage);

                Session["IntuitRequestToken"] = requestToken;

                const string autorizeUri = "https://workplace.intuit.com/Account/AuthorizeCharge?dbid={0}&appToken={1}&callBackUrl={2}";
                const string confirmUri = "https://app.theleadcentral.com/Company/InAppConfirmChargeHandler";

                return Redirect(string.Format(autorizeUri, platform.DbId, platform.AppToken, confirmUri));
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                Flash.GetCurrent().PushError(errorMessage);
                return RedirectToAction("Index");
            }
        }

        public ActionResult InAppConfirmChargeHandler(string chargeTokenVerifier)
        {
            var platform = GetIntuitPlatform();

            var requestToken = (string)Session["IntuitRequestToken"];
            Session["IntuitRequestToken"] = null;

            try
            {
                string errorMessage;
                platform.Charge(chargeTokenVerifier, requestToken, out errorMessage);
                if (string.IsNullOrEmpty(errorMessage))
                {
                    Flash.GetCurrent().PushInfo(string.Format("Payment processed"));
                }
                else
                {
                    Flash.GetCurrent().PushError(errorMessage);
                }
            }
            catch (Exception ex)
            {
                Flash.GetCurrent().PushError(ex.Message);
            }

            return RedirectToAction("Index");
        }

        private IntuitPlatformExtension GetIntuitPlatform()
        {
            var customer = ContextHelper.GetCurrentCustomer();
            return new IntuitPlatformExtension(customer.AppDbId, ContextHelper.GetAppToken());
        }
    }
}
