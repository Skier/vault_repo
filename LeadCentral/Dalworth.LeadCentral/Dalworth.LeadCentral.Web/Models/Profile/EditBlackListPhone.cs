using System.ComponentModel.DataAnnotations;
using System.Data;
using Dalworth.Common.SDK;
using Dalworth.LeadCentral.Domain;
using Dalworth.LeadCentral.Service;
using Dalworth.LeadCentral.Web.Models.Validators;

namespace Dalworth.LeadCentral.Web.Models.Profile
{
    public class EditBlackListPhone
    {
        public int Id { get; set; }

        [Required(ErrorMessage = @"Please enter Phone number")]
        [Phone(ErrorMessage = @"Phone Number must have valid format '214 335 1212'")]
        public string PhoneNumber { get; set; }

        [StringLength(200, ErrorMessage = @"Can not be longer than 200 symbols")]
        public string Description { get; set; }

        public void Load(int id, IDbConnection connection)
        {
            var phone = PhoneBlackList.FindByPrimaryKey(id, connection);
            Id = phone.Id;
            PhoneNumber = phone.PhoneNumber;
            Description = phone.Description;
        }

        public void Update(IDbConnection connection)
        {
            var phone = new PhoneBlackList
                            {
                                Id = Id,
                                PhoneNumber = PhoneNumber,
                                Description = Description,
                                PhoneDigits = StringUtil.ExtractLastSevenDigits(PhoneNumber)
                            };

            PhoneBlackList.Save(phone, connection);
        }

        public void Delete(int id, IDbConnection connection)
        {
            var existingPhone = PhoneBlackList.FindByPrimaryKey(id, connection);
            if (existingPhone != null)
                PhoneBlackList.Delete(existingPhone, connection);
        }
    }
}