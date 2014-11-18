using System.Data;
using System.ComponentModel.DataAnnotations;
using Dalworth.LeadCentral.Domain;
using Dalworth.LeadCentral.Service;
using Dalworth.LeadCentral.Web.Models.Validators;

namespace Dalworth.LeadCentral.Web.Models.Profile
{
    public class CompanyEdit
    {
        public int CurrentCustomerId { get; private set; }

        [Required(ErrorMessage = @"Please enter Company Name")]
        [StringLength(50, ErrorMessage = @"Must be under 50 characters")]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = @"Please enter Contact Person")]
        [StringLength(50, ErrorMessage = @"Must be under 50 characters")]
        public string CustomerContactPerson { get; set; }

        [Required(ErrorMessage = @"Please enter Phone number")]
        [RedirectPhone(ErrorMessage = @"Phone Number must have valid format '2143351212'")]
        public string CustomerPhone { get; set; }

        [Required(ErrorMessage = @"Please enter Email")]
        [Email(ErrorMessage = @"Email must be valid")]
        public string CustomerEmail { get; set; }

        public void Load(int customerId)
        {
            var customer = ContextHelper.GetCurrentCustomer();
            CurrentCustomerId = customer.Id;
            CustomerName = customer.Name;
            CustomerContactPerson = customer.ContactPerson;
            if (customer.Phone != null && customer.Phone.Length > 2)
                CustomerPhone = customer.Phone.Substring(2);
            else
                CustomerPhone = "";
            CustomerEmail = customer.Email;
        }

        public void Update(IDbConnection connection)
        {
            var customer = ContextHelper.GetCurrentCustomer();
            customer.Name = CustomerName;
            customer.ContactPerson = CustomerContactPerson;
            customer.Phone = "+1" + CustomerPhone;
            customer.Email = CustomerEmail;
            customer.IsCompanyProfileInited = true;

            if (TrackingPhoneService.GetAll(connection).Count > 0)
                customer.IsTrackingPhonesInited = true;

            if (CampaignService.GetAll(connection).Count > 0)
                customer.IsCampaignsInited = true;

            Customer.Save(customer);
        }
    }
}