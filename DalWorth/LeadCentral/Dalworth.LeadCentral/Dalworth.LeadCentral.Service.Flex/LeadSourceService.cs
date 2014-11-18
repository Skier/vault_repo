using Dalworth.LeadCentral.Domain;

namespace Dalworth.LeadCentral.Service.Flex
{
    public class LeadSourceService
    {
        public LeadSource[] GetAll(string ticket)
        {
            return Service.LeadSourceService.GetAll(ticket).ToArray();
        }

        public LeadSource GetByUserId(string ticket, int id)
        {
            return Service.LeadSourceService.GetByUserId(ticket, id);
        }

        public LeadSource[] GetByTrackingPhoneId(string ticket, int id)
        {
            return Service.LeadSourceService.GetByTrackingPhoneId(ticket, id).ToArray();
        }

        public LeadSource Save(string ticket, LeadSource leadSource)
        {
            return Service.LeadSourceService.Save(ticket, leadSource);
        }

        public LeadSource SaveWithPhones(string ticket, LeadSource leadSource, LeadSourcePhone[] ownPhones, TrackingPhone[] companyPhones)
        {
            return Service.LeadSourceService.SaveWithPhones(ticket, leadSource, ownPhones, companyPhones);
        }

        public TrackingPhone AddTrackingPhone(string ticket, LeadSource leadSource, TrackingPhone phone)
        {
            return Service.LeadSourceService.AddTrackingPhone(ticket, leadSource, phone);
        }

        public TrackingPhone RemoveTrackingPhone(string ticket, LeadSource leadSource, TrackingPhone phone)
        {
            return Service.LeadSourceService.RemoveTrackingPhone(ticket, leadSource, phone);
        }
    }
}