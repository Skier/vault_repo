using System;
using System.Linq;
using Dalworth.LeadCentral.Domain;

namespace Dalworth.LeadCentral.Web.Partner.Models
{
    public class LeadItem
    {
        private const string UndefinedValue = "";

        public LeadItem(Lead lead)
        {
            Customer = lead.FirstName + " " + lead.LastName;
            DateCreated = lead.DateCreated;
            LeadSource = lead.RelatedLeadSource != null ? lead.RelatedLeadSource.Name : UndefinedValue;
            if (lead.RelatedPhoneCall != null)
            {
                LeadTransport = LeadTransportEnum.PhoneCall;
                PhoneFrom = lead.RelatedPhoneCall.PhoneFrom;
                PhoneTo = lead.RelatedPhoneCall.PhoneTo;
                RecordingUrl = lead.RelatedPhoneCall.RecordingUrl;
            } else if (lead.RelatedSms != null)
            {
                LeadTransport = LeadTransportEnum.PhoneSms;
                PhoneFrom = lead.RelatedSms.PhoneFrom;
                PhoneTo = lead.RelatedSms.PhoneTo;
                RecordingUrl = UndefinedValue;
            }
            else if (lead.RelatedForm != null)
            {
                LeadTransport = LeadTransportEnum.HtmlForm;
                PhoneFrom = lead.Phone;
                PhoneTo = UndefinedValue;
                RecordingUrl = UndefinedValue;
            } else
            {
                LeadTransport = LeadTransportEnum.Direct;
                PhoneFrom = lead.Phone;
                PhoneTo = UndefinedValue;
                RecordingUrl = UndefinedValue;
            }

            LeadStatus = (LeadStatusEnum) lead.LeadStatusId;

            if (lead.RelatedQbInvoices != null && lead.RelatedQbInvoices.Count() > 0)
            {
                decimal amount = 0;
                foreach (var qbInvoice in lead.RelatedQbInvoices)
                {
                    amount += qbInvoice.Amount;
                }

                ClosedAmount = string.Format("{0:C}", amount);
            }

        }

        public string Customer { get; set; }
        public DateTime DateCreated { get; set; }

        public string LeadSource { get; set; }

        public string PhoneFrom { get; set; }
        public string PhoneTo { get; set; }
        public LeadTransportEnum LeadTransport { get; set; }
        public string RecordingUrl { get; set; }

        public LeadStatusEnum LeadStatus { get; set; }
        public string ClosedAmount { get; set; }
    }
}