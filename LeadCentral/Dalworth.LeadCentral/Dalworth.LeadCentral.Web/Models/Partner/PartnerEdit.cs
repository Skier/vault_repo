using System;
using System.Data;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Dalworth.Common.SDK;
using Dalworth.LeadCentral.Domain;
using Dalworth.LeadCentral.Service;
using Dalworth.LeadCentral.Web.Models.Validators;

namespace Dalworth.LeadCentral.Web.Models.Partner
{
    public class PartnerEdit
    {
        public PartnerEdit()
        {
            
        }

        private BusinessPartner CurrentPartner { get; set; }
        private BusinessPartner Partner
        {
            get { return CurrentPartner ?? (CurrentPartner = new BusinessPartner{DateCreated = DateTime.Now}); }
        }

        public int PartnerId
        {
            get { return Partner.Id; }
            set { Partner.Id = value; }
        }

        public DateTime DateCreated
        {
            get { return Partner.DateCreated; }
            set { Partner.DateCreated = value; }
        }

        [Required(ErrorMessage = @"Please enter Business Partner's Name")]
        [StringLength(50, ErrorMessage = @"Must be under 50 characters")]
        public string PartnerName
        {
            get { return Partner.PartnerName; }
            set { Partner.PartnerName = value; }
        }

        [Required(ErrorMessage = @"Please enter Email")]
        [Email(ErrorMessage = @"Email must be valid")]
        public string PartnerEmail
        {
            get { return Partner.Email; }
            set { Partner.Email = value; }
        }

        [Required(ErrorMessage = @"Please enter Phone number")]
        [Phone(ErrorMessage = @"Phone Number must have valid format. For Example: '(555) 456 1212'")]
        public string PartnerPhone
        {
            get { return Partner.Phone; }
            set
            {
                Partner.Phone = value;
                Partner.PhoneDigits = StringUtil.ExtractDigits(value);
            }
        }

        [StringLength(250, ErrorMessage = @"Address too long. Should be up to 250 symbols")]
        public string Address
        {
            get { return Partner.Address; }
            set { Partner.Address = value; }
        }

        public bool IsTestPartner
        {
            get { return Partner.IsExcludedFromReports; }
            set { Partner.IsExcludedFromReports = value; }
        }

        public bool IsRemoved
        {
            get { return Partner.IsRemoved; }
        }

        #region salesreps

        private List<Domain.User> SalesReps;
        
            
        

        public int SalesRepId
        {
            get
            {
                return Partner.SalesRepId != null ? Partner.SalesRepId.Value : 0;
            }
            set
            {
                if (value == 0)
                    Partner.SalesRepId = null;
                else
                    Partner.SalesRepId = value;
            }
        }

        public List<Domain.User> SalesRepList
        {
            get
            {
                var result = new List<Domain.User> { new Domain.User { Id = 0, ScreenName = string.Empty } };
                if (SalesReps != null && SalesReps.Count > 0)
                {
                    result.AddRange(SalesReps);
                }

                return result;
            }
        }

        #endregion

        public void Load(int? partnerId, IDbConnection connection)
        {
            SalesReps = UserService.FindStaff(connection);

            if (partnerId.HasValue)
                CurrentPartner = BusinessPartnerService.FindById(partnerId.Value, connection);
        }

        public void Update(IDbConnection connection)
        {
            BusinessPartnerService.Save(CurrentPartner, connection);
        }

        public void Delete(int partnerId, IDbConnection connection )
        {
            var partner = BusinessPartnerService.FindById(partnerId, connection);
            var campaigns = CampaignService.GetByBusinessPartnerId(partnerId, connection);
            foreach (var campaign in campaigns)
            {
                CampaignService.StopCampaign(campaign.Id, connection);
            }
            var users = Domain.User.FindActiveByBusinessPartnerId(partnerId, connection);
            foreach (var user in users)
            {
                user.IsActive = false;
                UserService.Update(user, connection);
            }
            partner.IsRemoved = true;
            BusinessPartnerService.Save(partner, connection);
        }
    }
}