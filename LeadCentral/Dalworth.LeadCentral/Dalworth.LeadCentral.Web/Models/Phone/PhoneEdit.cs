using System.Data;
using System.ComponentModel.DataAnnotations;
using Dalworth.LeadCentral.Service;
using Dalworth.LeadCentral.Web.Models.Validators;

namespace Dalworth.LeadCentral.Web.Models.Phone
{
    public class PhoneEdit
    {
        public int Id { get; set; }
        
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = @"Please enter Phone number")]
        public string FriendlyNumber { get; set; }
        
        public string Description { get; set; }
        
        public bool CallerIdLookup { get; set; }
        
        public bool TranscribeCalls { get; set; }

        [Required(ErrorMessage = @"Please enter Phone number")]
        [RedirectPhone(ErrorMessage = @"Phone Number must have valid format '2143351212'")]
        public string RedirectPhoneNumber { get; set; }

        public void Load(int id, IDbConnection connection)
        {
            var phone = TrackingPhoneService.GetById(id, connection);
            Id = phone.Id;
            PhoneNumber = phone.PhoneNumber;
            FriendlyNumber = phone.FriendlyNumber;
            Description = phone.Description;
            CallerIdLookup = phone.CallerIdLookup;
            TranscribeCalls = phone.TranscribeCalls;
            RedirectPhoneNumber = phone.RedirectPhoneNumber != null && phone.RedirectPhoneNumber.Length > 2 ? phone.RedirectPhoneNumber.Substring(2) : "";
        }

        public void Update(IDbConnection connection)
        {
            var existingPhone = TrackingPhoneService.GetById(Id, connection);
            existingPhone.FriendlyNumber = FriendlyNumber;
            existingPhone.RedirectPhoneNumber = "+1" + RedirectPhoneNumber;
            existingPhone.Description = Description;
            existingPhone.CallerIdLookup = CallerIdLookup;
            existingPhone.TranscribeCalls = TranscribeCalls;

            TrackingPhoneService.Save(existingPhone, connection);
        }
    
    }
}