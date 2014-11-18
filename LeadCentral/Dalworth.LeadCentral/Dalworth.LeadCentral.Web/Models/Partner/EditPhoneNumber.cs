using System.ComponentModel.DataAnnotations;
using System.Data;
using Dalworth.Common.Data;
using Dalworth.Common.SDK;
using Dalworth.LeadCentral.Domain;
using Dalworth.LeadCentral.Service;
using Dalworth.LeadCentral.Web.Models.Validators;

namespace Dalworth.LeadCentral.Web.Models.Partner
{
    public class EditPhoneNumber
    {
        public int Id { get; set; }

        public int PartnerId { get; set; }

        [Required(ErrorMessage = @"Please enter Phone number")]
        [Phone(ErrorMessage = @"Phone Number must have valid format '214 335 1212'")]
        public string PhoneNumber { get; set; }
        
        [StringLength(200, ErrorMessage = @"Can not be longer than 200 symbols")]
        public string Description { get; set; }

        public EditPhoneNumber ()
        {
        }

        public void Load(int id, IDbConnection connection)
        {
            var phone = PartnerPhoneNumber.FindByPrimaryKey(id, connection);
            Id = phone.Id;
            PartnerId = phone.BusinessPartnerId;
            PhoneNumber = phone.PhoneNumber;
            Description = phone.Description;
        }

        public void Update(IDbConnection connection)
        {
            var phone = new PartnerPhoneNumber();
            phone.Id = Id;
            phone.BusinessPartnerId = PartnerId;
            phone.PhoneNumber = PhoneNumber;
            phone.Description = Description;
            phone.PhoneDigits = StringUtil.ExtractLastSevenDigits(PhoneNumber);

            PartnerPhoneNumber.Save(phone, connection);
        }

        public void Delete(IDbConnection connection)
        {
            try
            {
                var existingPhone = PartnerPhoneNumber.FindByPrimaryKey(Id);
                PartnerPhoneNumber.Delete(existingPhone, connection);
            }
            catch (DataNotFoundException)
            {}
        }
    }
}