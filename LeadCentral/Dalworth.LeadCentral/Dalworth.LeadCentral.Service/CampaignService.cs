using System;
using System.Collections.Generic;
using System.Data;
using Dalworth.Common.Data;
using Dalworth.LeadCentral.Domain;

namespace Dalworth.LeadCentral.Service
{
    public class CampaignService
    {
        #region Save 

        public static void Save(Campaign campaign, IDbConnection connection)
        {
            var customer = ContextHelper.GetCurrentCustomer();
          
            Campaign.Save(campaign, connection);
            if (customer.IsCampaignsInited)
                return;

            customer.IsCampaignsInited = true;
            Customer.Save(customer);
        }

        #endregion

        #region GetAllActive

        public static List<Campaign> GetAllActive(IDbConnection connection)
        {
            var user = ContextHelper.GetCurrentUser();
            var result = user.IsBusinessPartner 
                ? Campaign.GetAllActive(user.BusinessPartnerId ?? 0, connection) 
                : Campaign.GetAllActive(connection);

            foreach (var campaign in result)
            {
                UpdatePartner(campaign, connection);
            }

            return result;
        }

        #endregion

        #region GetAllActive

        public static List<Campaign> GetAll(IDbConnection connection)
        {
           
            var user = ContextHelper.GetCurrentUser();
            var result = user.IsBusinessPartner
                ? Campaign.GetAll(user.BusinessPartnerId ?? 0, connection)
                : Campaign.GetAll(connection);

            foreach (var campaign in result)
            {
                UpdatePartner(campaign, connection);
            }

            return result;
        }

        #endregion

        #region LoadCampaigns

        public static List<Campaign> LoadCampaigns(CampaignFilter filter, IDbConnection connection)
        {
            var result = new List<Campaign>();
           
            var user = ContextHelper.GetCurrentUser();
            if(user.IsBusinessPartner)
            {
                if (user.BusinessPartnerId == null)
                    return result;
                    
                filter.PartnerId = user.BusinessPartnerId.Value;
            }
                
            result = Campaign.GetCampaigns(filter, connection);

            foreach (var campaign in result)
            {
                UpdateRelated(campaign, connection);
            }

            return result;
            
        }

        #endregion 

        #region LoadCampaign

        public static Campaign LoadCampaign(int campaignId, IDbConnection connection)
        {
            Campaign campaign = null;
            try
            {
                var user = ContextHelper.GetCurrentUser();
                    
                campaign = Campaign.FindByPrimaryKey(campaignId, connection);

                if (user.IsBusinessPartner && user.BusinessPartnerId != campaign.BusinessPartnerId)
                    return null;

                UpdateRelated(campaign, connection);
                
            } catch(DataNotFoundException)
            {
                return null;
            }

            return campaign;
        }

        #endregion

        #region StopCampaign

        public static Campaign StopCampaign(int campaignId, IDbConnection connection)
        {
            var campaign = LoadCampaign(campaignId, connection);
            
            foreach (var phone in campaign.TrackingPhones)
            {
                TrackingPhoneService.RemovePhoneFromCampaign(phone.Id, campaignId, connection);
            }
            campaign.DateEnd = DateTime.Now;

            Save(campaign, connection);

            return campaign;
        }

        #endregion 

        #region GetCampaignByTrackingPhoneAndDate

        public static Campaign GetCampaignByTrackingPhoneAndDate(int trackingPhoneId, DateTime dateTime, IDbConnection connection)
        {
            return Campaign.GetCampaignByTrackingPhoneAndDate(trackingPhoneId, dateTime, connection);
        }

        #endregion 

        #region GetByBusinessPartnerId

        public static List<Campaign> GetByBusinessPartnerId(int id, IDbConnection connection)
        {
            var result = Campaign.GetByBusinessPartnerId(id, connection);
            foreach (var campaign in result)
            {
                UpdateRelated(campaign, connection);
            }

            return result;
        }

        #endregion 

        #region FindByPrimaryKey

        public static Campaign FindByPrimaryKey(int campaignId, IDbConnection connection)
        {
            return Campaign.FindByPrimaryKey(campaignId, connection);
        }

        #endregion 

        #region private

        private static void UpdateRelated(Campaign campaign, IDbConnection connection)
        {
            UpdatePartner(campaign, connection);
            campaign.RelatedUser = User.FindByPrimaryKey(campaign.UserId, connection);
            campaign.TrackingPhones = TrackingPhone.GetByCampaign(campaign.Id, connection);
        }

        private static void UpdatePartner(Campaign campaign, IDbConnection connection)
        {
            if (campaign.BusinessPartnerId != null)
                campaign.RelatedBusinessPartner = BusinessPartner.FindByPrimaryKey(campaign.BusinessPartnerId.Value, connection);
        }

        #endregion
    }
}
