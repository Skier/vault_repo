using System;
using System.Data;
using System.Web;
using Dalworth.LeadCentral.Domain;
using Dalworth.LeadCentral.Service;

namespace Dalworth.LeadCentral.Web
{
    /// <summary>
    /// Summary description for ReceiveLeadForm
    /// </summary>
    public class ReceiveLeadForm : IHttpHandler
    {
        private const int WebAccessDurationMin = 3;

        private const string RealmIdParam = "RealmId";
        private const string LeadSourceIdParam = "LeadSourceId";
        private const string FirstNameParam = "FirstName";
        private const string LastNameParam = "LastName";
        private const string PhoneParam = "Phone";
        private const string MessageParam = "Message";

        private const string ResultUrlParam = "ResultUrl";
        private const string FaultUrlParam = "FaultUrl";

        public void ProcessRequest(HttpContext context)
        {
            var request = context.Request;
            
            var realmId = request[RealmIdParam];
            var leadSourceId = request[LeadSourceIdParam];
            var firstName = request[FirstNameParam];
            var lastName = request[LastNameParam];
            var phone = request[PhoneParam];
            var message = request[MessageParam];
            
            var referralUri = request.UrlReferrer != null ? request.UrlReferrer.AbsoluteUri : "";

            var resultUrl = request[ResultUrlParam];
            var faultUrl = request[FaultUrlParam];

            try
            {
                var servmanCustomer = ServmanCustomerService.FindByRealmId(realmId);

                using (var connection = ServmanCustomerService.GetConnection(servmanCustomer))
                {
                    var leadForm = new LeadForm
                    {
                        LeadSourceId = int.Parse(leadSourceId),
                        FirstName = firstName,
                        LastName = lastName,
                        Phone = phone,
                        Message = message,
                        ReferralUri = referralUri,
                        DateCreated = DateTime.Now
                    };

                    LeadForm.Save(leadForm, connection);
                    SyncWithPhoneRotation(leadForm, servmanCustomer);
                    CreateLead(leadForm, connection);
                }
            }
            catch (Exception)
            {
                context.Response.Redirect(faultUrl);
            }

            context.Response.Redirect(resultUrl);

        }

        private static void CreateLead(LeadForm leadForm, IDbConnection connection)
        {
            var lead = new Lead
            {
                LeadStatusId = (int)LeadStatusEnum.New,
                FirstName = leadForm.FirstName,
                LastName = leadForm.LastName,
                Phone = leadForm.Phone,
                CustomerNotes = leadForm.Message,
                LeadSourceId = leadForm.LeadSourceId,
                DateCreated = DateTime.Now,
                WebFormId = leadForm.Id
            };

            lead.UpdateNullable();

            Lead.Save(lead, connection);
        }

        private static void SyncWithPhoneRotation(LeadForm leadForm, ServmanCustomer servmanCustomer)
        {
            var duration = new TimeSpan(0, WebAccessDurationMin, 0);
            var trackingPhoneRotation = PhoneService.GetLastPhoneRotationByReferral(servmanCustomer, leadForm.ReferralUri);

            if (trackingPhoneRotation == null || trackingPhoneRotation.TimeDisplay.Add(duration) < DateTime.Now)
                return;

            trackingPhoneRotation.PhoneCallId = leadForm.Id;
            trackingPhoneRotation.LeadSourceId = leadForm.LeadSourceId;
            PhoneService.SavePhoneRotation(servmanCustomer, trackingPhoneRotation);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}