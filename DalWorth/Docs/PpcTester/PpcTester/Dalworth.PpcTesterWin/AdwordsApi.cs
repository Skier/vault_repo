using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using com.google.api.adwords.lib;
using com.google.api.adwords.lib.util;
using com.google.api.adwords.v200909;

namespace Dalworth.PpcTesterWin
{
    class AdwordsApi
    {
        
        private CampaignService m_compaingService;
        private AdGroupService m_adgroupService;
        private AdGroupCriterionService m_adgroupCriterionService;


        public AdwordsApi(string clientEmail)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("email", "bfurman1@gmail.com");
            headers.Add("password", "sem40.ru");
            headers.Add("useragent", "Affilia");
            headers.Add("developerToken", "wDt7W1Q_cqY5mHVn0bAL4g");
            headers.Add("clientEmail", clientEmail);

            AdWordsUser user = new AdWordsUser(headers);
            m_compaingService = (CampaignService)user.GetService(AdWordsService.v200909.CampaignService);
            m_adgroupService = (AdGroupService)user.GetService(AdWordsService.v200909.AdGroupService);
            m_adgroupCriterionService = (AdGroupCriterionService)user.GetService(AdWordsService.v200909.AdGroupCriterionService);
        }

        public CampaignPage GetAllGoogleCampaings()
        {
            CampaignSelector selector = new CampaignSelector();
            CampaignPage page = m_compaingService.get(selector);
            return page;
        }

        public AdGroupPage GetAdgroups(long campaignId)
        {

            AdGroupSelector adGroupSelector = new AdGroupSelector()
            {
                campaignId = campaignId,
                campaignIdSpecified = true
            };

            AdGroupPage adGroupPage = m_adgroupService.get(adGroupSelector);

            return adGroupPage;
        }

        public AdGroupCriterionPage GetAdgroupCriterions(long campaignId, long adGroupId, DateTime dateStart, DateTime dateEnd)
        {
            if (dateStart > dateEnd)
                throw new ArgumentException("Invalid dateStart > dateEnd");

            AdGroupCriterionSelector adGroupCriterionSelector = new AdGroupCriterionSelector()
            {
                criterionUse = CriterionUse.BIDDABLE,
                criterionUseSpecified = true
            };

            adGroupCriterionSelector.statsSelector = new StatsSelector()
            {
                dateRange = new DateRange()
                {
                    min = String.Format("{0:yyyyMMdd}", dateStart),
                    max = String.Format("{0:yyyyMMdd}", dateEnd)
                }
            };

            adGroupCriterionSelector.idFilters = new AdGroupCriterionIdFilter[] 
                            {
                                new AdGroupCriterionIdFilter()
                                {
                                     adGroupId = adGroupId,
                                     adGroupIdSpecified = true,
                                     campaignId = campaignId,
                                     campaignIdSpecified = true
                                }
                            };

            AdGroupCriterionPage adGroupCriterionPage = m_adgroupCriterionService.get(adGroupCriterionSelector);

            return adGroupCriterionPage;

        }

        public int MapAdgroupStatus(AdGroupStatus status)
        {
            switch (status)
            {
                case AdGroupStatus.ENABLED:
                    return 1;
                case AdGroupStatus.PAUSED:
                    return 2;
                case AdGroupStatus.DELETED:
                    return 3;
            }
            throw new Exception("Invalid Adgroup Status");
        }

        public int MapCampaingStatus(CampaignStatus status)
        {
            switch (status)
            {
                case com.google.api.adwords.v200909.CampaignStatus.ACTIVE:
                    return 1;
                 
                case com.google.api.adwords.v200909.CampaignStatus.DELETED:
                    return 2;
                   
                case com.google.api.adwords.v200909.CampaignStatus.PAUSED:
                   return 3;
                  
            }

            throw new Exception("Invalid campaing status");
        }

        public int MapKeywordMatchType(KeywordMatchType matchType)
        {
            switch (matchType)
            {
                case KeywordMatchType.BROAD:
                    return 1;
                case KeywordMatchType.PHRASE:
                    return 2;
                case KeywordMatchType.EXACT:
                    return 3;
            }

            throw new Exception("Invalid keyword match type");
        }

        public int MapKeywordStatus(UserStatus status)
        {
            switch (status)
            {
                case UserStatus.ACTIVE:
                    return 1;
                case UserStatus.PAUSED:
                    return 2;
                case UserStatus.DELETED:
                    return 3;
            }

            throw new Exception("Invalid keyword status");
        }

        public int MapSystemServingStatus(SystemServingStatus status)
        {
            switch (status)
            {
                case SystemServingStatus.ELIGIBLE:
                    return 1;
                case SystemServingStatus.RARELY_SERVED:
                    return 2;
            }
            throw new Exception("Invalid system serving status");
        }
    }
}
