using System.Data;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Dalworth.LeadCentral.Domain;
using Dalworth.LeadCentral.Service;

namespace Dalworth.LeadCentral.Web.Models.Campaign
{
    public class CampaignEdit
    {
        public CampaignEdit()
        {
            CurrentCampaign = new Domain.Campaign();
        }

        public Domain.Campaign CurrentCampaign { get; private set; }

        public BusinessPartner CurrentPartner { get; set; }

        [Required(ErrorMessage = @"Please enter Company Name")]
        [StringLength(50, ErrorMessage = @"Must be under 50 characters")]
        public string CampaignName
        {
            get { return CurrentCampaign != null ? CurrentCampaign.CampaignName : string.Empty; }
            set { if (CurrentCampaign != null) CurrentCampaign.CampaignName = value; }
        }

        #region partners

        private List<BusinessPartner> Partners;
        public bool DenyChangePartner { get; set; }

        public int PartnerId
        {
            get
            {
                return CurrentCampaign.BusinessPartnerId != null ? CurrentCampaign.BusinessPartnerId.Value : 0;
            }
            set
            {
                if (value == 0)
                    CurrentCampaign.BusinessPartnerId = null;
                else
                    CurrentCampaign.BusinessPartnerId = value;
            }
        }

        public List<BusinessPartner> PartnerList
        {
            get
            {
                var result = new List<BusinessPartner> { new BusinessPartner{ Id = 0, PartnerName = string.Empty } };
                if (Partners != null && Partners.Count > 0)
                {
                    foreach (var partner in Partners)
                    {
                        result.Add(partner);
                    }
                }

                return result;
            }
        }

        #endregion

        #region users

        private List<Domain.User> Users;
        public int UserId
        {
            get
            {
                return CurrentCampaign.UserId;
            }
            set
            {
                CurrentCampaign.UserId = value;
            }
        }

        public List<Domain.User> UserList
        {
            get
            {
                var result = new List<Domain.User>();
                if (Users != null && Users.Count > 0)
                {
                    foreach (var user in Users)
                    {
                        result.Add(user);
                    }
                }

                return result;
            }
        }

        #endregion

        public void Load(IDbConnection connection, int? campaignId = null)
        {
            Partners = BusinessPartnerService.FindActive(connection);
            Users = UserService.FindStaff(connection);

            if (campaignId.HasValue)
            {
                CurrentCampaign = CampaignService.LoadCampaign(campaignId.Value, connection);
                DenyChangePartner = CurrentCampaign.RelatedBusinessPartner != null;
            }
        }

        public void Update(IDbConnection connection)
        {
            CampaignService.Save(CurrentCampaign, connection);
        }
    }
}