using System;
using Dalworth.LeadCentral.Domain;

namespace Dalworth.LeadCentral.Service.Flex
{
    public class PhoneService
    {
        public PhoneCall[] GetActiveCalls(string ticket)
        {
            return Service.PhoneService.GetActiveCalls(ticket).ToArray();
        }

        public PhoneCall[] GetCallsByPhoneId(string ticket, int phoneId)
        {
            return Service.PhoneService.GetCallsByPhoneId(ticket, phoneId).ToArray();
        }

        public void HandleCall(string ticket, string callSid, User user, string redirectPhoneNumber)
        {
            Service.PhoneService.HandleCall(ticket, callSid, user, redirectPhoneNumber);
        }

        public void HandleCall(string ticket, string callSid, User user)
        {
            Service.PhoneService.HandleCall(ticket, callSid, user, user.Phone);
        }

        public LeadSourcePhone[] GetAllLeadSourcePhones(string ticket)
        {
            return Service.PhoneService.GetAllLeadSourcePhones(ticket).ToArray();
        }

        public LeadSourcePhone[] GetLeadSourcePhones(string ticket, int leadSourceId)
        {
            return Service.PhoneService.GetLeadSourcePhones(ticket, leadSourceId).ToArray();
        }

        public TrackingPhone[] GetTrackingPhonesByLeadSourceId(string ticket, int leadSourceId)
        {
            return Service.PhoneService.GetTrackingPhonesByLeadSourceId(ticket, leadSourceId).ToArray();
        }

        public TrackingPhone[] GetActivePhoneNumbers(string ticket)
        {
            return Service.PhoneService.GetPhoneNumbers(ticket).ToArray();
        }

        public TrackingPhone[] GetAvailablePhoneNumbers(string areaCode)
        {
            return Service.PhoneService.GetAvailablePhoneNumbers(areaCode).ToArray();
        }

        public TrackingPhone[] GetAvailableTollFreePhoneNumbers(string areaCode)
        {
            return Service.PhoneService.GetAvailableTollFreePhoneNumbers(areaCode).ToArray();
        }

        public TrackingPhone PurchasePhoneNumber(string ticket, TrackingPhone phone)
        {
            return Service.PhoneService.PurchasePhoneNumber(ticket, phone);
        }

        public TrackingPhone PurchaseTollFreePhoneNumber(string ticket, TrackingPhone phone)
        {
            return Service.PhoneService.PurchaseTollFreePhoneNumber(ticket, phone);
        }

        public TrackingPhone Save(string ticket, TrackingPhone phone)
        {
            return Service.PhoneService.Save(ticket, phone);
        }

        public TrackingPhone SuspendPhoneNumber(string ticket, TrackingPhone phone)
        {
            return Service.PhoneService.SuspendPhoneNumber(ticket, phone);
        }

        public TrackingPhone ActivatePhoneNumber(string ticket, TrackingPhone phone)
        {
            return Service.PhoneService.ActivatePhoneNumber(ticket, phone);
        }

        public void RemovePhoneNumber(string ticket, TrackingPhone phone)
        {
            Service.PhoneService.RemovePhoneNumber(ticket, phone);
        }

        public PhoneCallWorkflow[] GetRulesByTrackingPhoneId(string ticket, int phoneId)
        {
            return Service.PhoneService.GetRulesByTrackingPhoneId(ticket, phoneId).ToArray();
        }

        public CallWorkflow[] RetrieveWorkflows(string ticket)
        {
            return Service.PhoneService.RetrieveWorkflows(ticket).ToArray();
        }

        public void CommitWorkflows(string ticket, CallWorkflow[] workflows)
        {
            Service.PhoneService.CommitWorkflows(ticket, workflows);
        }

        public CallWorkflow[] GetAllWorkflows(string ticket)
        {
            return Service.PhoneService.GetAllWorkflows(ticket).ToArray();
        }

        public PhoneCallWorkflow SavePhoneCallWorkflow(string ticket, PhoneCallWorkflow phoneCallWorkflow)
        {
            phoneCallWorkflow.UpdateNullableFields();

            return Service.PhoneService.SavePhoneCallWorkflow(ticket, phoneCallWorkflow);
        }

        public void RemovePhoneCallWorkflow(string ticket, PhoneCallWorkflow phoneCallWorkflow)
        {
            Service.PhoneService.RemovePhoneCallWorkflow(ticket, phoneCallWorkflow);
        }

        public void UpdatePhoneCallWorkflows(string ticket, PhoneCallWorkflow[] phoneCallWorkflows)
        {
            foreach(var phoneCallWorkflow in phoneCallWorkflows)
            {
                phoneCallWorkflow.UpdateNullableFields();
            }

            Service.PhoneService.UpdatePhoneCallWorkflows(ticket, phoneCallWorkflows);
        }

        public TrackingPhoneRotation[] GetTrackingPhoneRotations(string ticket, LeadSource[] leadSources, DateTime? startDate, DateTime? endDate)
        {
            return Service.PhoneService.GetTrackingPhoneRotations(ticket, leadSources, startDate, endDate).ToArray();
        }

        public TrackingPhoneRotation[] GetTrackingPhoneRotationsByPhoneCall(string ticket, PhoneCall phoneCall)
        {
            return Service.PhoneService.GetTrackingPhoneRotationsByPhoneCall(ticket, phoneCall).ToArray();
        }

        public TrackingPhoneRotation[] GetTrackingPhoneRotationsByPhoneSms(string ticket, PhoneSms phoneSms)
        {
            return Service.PhoneService.GetTrackingPhoneRotationsByPhoneSms(ticket, phoneSms).ToArray();
        }

        public TrackingPhoneRotation[] GetTrackingPhoneRotationsByLeadForm(string ticket, LeadForm leadForm)
        {
            return Service.PhoneService.GetTrackingPhoneRotationsByLeadForm(ticket, leadForm).ToArray();
        }

    }
}