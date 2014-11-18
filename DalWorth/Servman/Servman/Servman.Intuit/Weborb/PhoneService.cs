using Servman.Domain;
using Servman.Service;

namespace Servman.Intuit.Weborb
{
    public class PhoneService
    {
        public PhoneCall[] GetActiveCalls()
        {
            return Service.PhoneService.GetActiveCalls(ContextHelper.GetCurrentCustomer()).ToArray();
        }

        public PhoneCall[] GetCallsByPhoneId(int phoneId)
        {
            return Service.PhoneService.GetCallsByPhoneId(phoneId, ContextHelper.GetCurrentCustomer()).ToArray();
        }

        public void HandleCall(string callSid, User user, string redirectPhoneNumber)
        {
            Service.PhoneService.HandleCall(callSid, user, redirectPhoneNumber, ContextHelper.GetCurrentCustomer());
        }

        public void HandleCall(string callSid, User user)
        {
            Service.PhoneService.HandleCall(callSid, user, user.Phone, ContextHelper.GetCurrentCustomer());
        }

        public Phone[] GetAllPhones()
        {
            return Service.PhoneService.GetAllPhones(ContextHelper.GetCurrentCustomer()).ToArray();
        }

        public Phone[] GetPhonesByBusinessPartnerId(int businessPartnerId)
        {
            return Service.PhoneService.GetPhonesByBusinessPartnerId(businessPartnerId, ContextHelper.GetCurrentCustomer()).ToArray();
        }

        public Phone[] GetPhonesBySalesRepId(int salesRepId)
        {
            return Service.PhoneService.GetPhonesBySalesRepId(salesRepId, ContextHelper.GetCurrentCustomer()).ToArray();
        }

        public Phone[] GetActivePhoneNumbers()
        {
            return Service.PhoneService.GetActivePhoneNumbers().ToArray();
        }

        public Phone[] GetAvailablePhoneNumbers(string areaCode)
        {
            return Service.PhoneService.GetAvailablePhoneNumbers(areaCode).ToArray();
        }

        public Phone[] GetAvailableTollFreePhoneNumbers(string areaCode)
        {
            return Service.PhoneService.GetAvailableTollFreePhoneNumbers(areaCode).ToArray();
        }

        public Phone PurchasePhoneNumber(Phone phone)
        {
            return Service.PhoneService.PurchasePhoneNumber(phone, ContextHelper.GetCurrentCustomer());
        }

        public Phone PurchaseTollFreePhoneNumber(Phone phone)
        {
            return Service.PhoneService.PurchaseTollFreePhoneNumber(phone, ContextHelper.GetCurrentCustomer());
        }

        public Phone SuspendPhoneNumber(Phone phone)
        {
            return Service.PhoneService.SuspendPhoneNumber(phone);
        }

        public Phone ActivatePhoneNumber(Phone phone)
        {
            return Service.PhoneService.ActivatePhoneNumber(phone);
        }

        public void RemovePhoneNumber(Phone phone)
        {
            Service.PhoneService.RemovePhoneNumber(phone);
        }

        public PhoneCallWorkflow[] GetRulesByPhoneId(int phoneId)
        {
            return Service.PhoneService.GetRulesByPhoneId(phoneId, ContextHelper.GetCurrentCustomer()).ToArray();
        }

        public CallWorkflow[] RetrieveWorkflows()
        {
            return Service.PhoneService.RetrieveWorkflows(ContextHelper.GetCurrentCustomer()).ToArray();
        }

        public void CommitWorkflows(CallWorkflow[] workflows)
        {
            Service.PhoneService.CommitWorkflows(workflows, ContextHelper.GetCurrentCustomer());
        }

        public CallWorkflow[] GetAllWorkflows()
        {
            return Service.PhoneService.GetAllWorkflows(ContextHelper.GetCurrentCustomer()).ToArray();
        }

        public PhoneCallWorkflow SavePhoneCallWorkflow(PhoneCallWorkflow phoneCallWorkflow)
        {
            phoneCallWorkflow.UpdateNullableFields();

            return Service.PhoneService.SavePhoneCallWorkflow(phoneCallWorkflow, ContextHelper.GetCurrentCustomer());
        }

        public void RemovePhoneCallWorkflow(PhoneCallWorkflow phoneCallWorkflow)
        {
            Service.PhoneService.RemovePhoneCallWorkflow(phoneCallWorkflow, ContextHelper.GetCurrentCustomer());
        }

        public void UpdatePhoneCallWorkflows(PhoneCallWorkflow[] phoneCallWorkflows)
        {
            foreach(var phoneCallWorkflow in phoneCallWorkflows)
            {
                phoneCallWorkflow.UpdateNullableFields();
            }

            Service.PhoneService.UpdatePhoneCallWorkflows(phoneCallWorkflows, ContextHelper.GetCurrentCustomer());
        }

    }
}