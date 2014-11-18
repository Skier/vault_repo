using System.Data;
using System.Collections.Generic;
using Dalworth.LeadCentral.Domain;
using Dalworth.LeadCentral.Service;

namespace Dalworth.LeadCentral.Web.Models.Lead
{
    public class LeadEdit
    {
        public LeadEdit()
        {
            
        }

        public void Load(IDbConnection connection)
        {
            InitCampaigns(connection);
            m_businessPartners = BusinessPartnerService.FindActive(connection);
        }

        public Domain.Lead CurrentLead { get; set; }

        #region statuses

        public int LeadStatusId
        {
            get
            {
                return CurrentLead.LeadStatusId;
            }
            set
            {
                CurrentLead.LeadStatusId = value;
            }
        }

        public List<LeadStatus> LeadStatusList
        {
            get
            {
                var result = new List<LeadStatus>
                                 {
                                     new LeadStatus((int) LeadStatusEnum.New, LeadStatusEnum.New.ToString()),
                                     new LeadStatus((int) LeadStatusEnum.Pending, LeadStatusEnum.Pending.ToString()),
                                     new LeadStatus((int) LeadStatusEnum.Cancelled, LeadStatusEnum.Cancelled.ToString()),
                                     new LeadStatus((int) LeadStatusEnum.Converted, LeadStatusEnum.Converted.ToString())
                                 };

                return result;
            }
        }

        #endregion

        #region campaigns

        private List<Domain.Campaign> Campaigns;
        private void InitCampaigns(IDbConnection connection)
        {
            Campaigns = CampaignService.GetAllActive(connection);
        }

        public int CampaignId
        {
            get
            {
                return CurrentLead.CampaignId != null ? CurrentLead.CampaignId.Value : 0 ;
            }
            set
            {
                if (value == 0)
                    CurrentLead.CampaignId = null;
                else
                    CurrentLead.CampaignId = value;
            }
        }

        public List<Domain.Campaign> CampaignList
        {
            get
            {
                var result = new List<Domain.Campaign> {new Domain.Campaign {Id = 0, CampaignName = string.Empty}};
                if (Campaigns != null && Campaigns.Count > 0)
                {
                    foreach (var campaign in Campaigns)
                    {
                        result.Add(campaign);
                    }
                }

                return result;
            }
        }

        #endregion

        #region partners

        private List<BusinessPartner> m_businessPartners;

        public int BusinessPartnerId
        {
            get
            {
                return CurrentLead.BusinessPartnerId != null ? CurrentLead.BusinessPartnerId.Value : 0;
            }
            set
            {
                if (value == 0)
                    CurrentLead.BusinessPartnerId = null;
                else
                    CurrentLead.BusinessPartnerId = value;
            }
        }

        public List<BusinessPartner> BusinessPartnerList
        {
            get
            {
                var result = new List<BusinessPartner> { new BusinessPartner { Id = 0, PartnerName = string.Empty } };
                if (m_businessPartners != null && m_businessPartners.Count > 0)
                {
                    foreach (var partner in m_businessPartners)
                    {
                        result.Add(partner);
                    }
                }

                return result;
            }
        }

        #endregion
    }
}