using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using com.google.api.adwords.lib;
using com.google.api.adwords.lib.util;
using com.google.api.adwords.v200909;
namespace AdwordsApiTester
{
    public class Program
    {

        static void Main(string[] args)
        {
            try
            {

                Dictionary<string, string> headers = new Dictionary<string, string>();

                headers.Add("email", "bfurman1@gmail.com");
                headers.Add("password", "sem40.ru");
                headers.Add("useragent", "Affilia");
                headers.Add("developerToken", "wDt7W1Q_cqY5mHVn0bAL4g");
                headers.Add("clientEmail", "shane@dalworth.com");

                // Create a custom AdWordsUser.
                AdWordsUser user = new AdWordsUser(headers);
                // Remove this line if you want to run this sample against production account.
                //user.UseSandbox();

                CampaignService compaingService = (CampaignService)user.GetService(AdWordsService.v200909.CampaignService);
                AdGroupService adgroupService = (AdGroupService)user.GetService(AdWordsService.v200909.AdGroupService);
                AdGroupCriterionService adgroupCriterionService = (AdGroupCriterionService)user.GetService(AdWordsService.v200909.AdGroupCriterionService);


                CampaignSelector selector = new CampaignSelector();
                //selector.campaignStatuses = new CampaignStatus[] { CampaignStatus.ACTIVE};

                CampaignPage page = compaingService.get(selector);

                foreach (Campaign campaing in page.entries)
                {
                    Console.WriteLine("########################################################################################");
                    Console.WriteLine ("Campaing " + campaing.id + " " + campaing.name+ " " + campaing.status.ToString());
                   
                    AdGroupSelector adGroupSelector = new AdGroupSelector()
                    {
                        campaignId =  campaing.id,
                        campaignIdSpecified = true
                    };


                    AdGroupPage adGroupPage = adgroupService.get(adGroupSelector);
                    if (adGroupPage != null && adGroupPage.entries != null)
                    {
                        foreach (AdGroup adGroup in adGroupPage.entries)
                        {
                            if (adGroup.status != AdGroupStatus.ENABLED)
                                continue;

                            Console.WriteLine("#######" + adGroup.id + " " + adGroup.name + " " + adGroup.status.ToString());

                            AdGroupCriterionSelector adGroupCriterionSelector = new AdGroupCriterionSelector()
                            {
                                criterionUse = CriterionUse.BIDDABLE,
                                 criterionUseSpecified = true
                            };

                             adGroupCriterionSelector.statsSelector = new StatsSelector()
                            {
                                dateRange = new DateRange()
                                {
                                    min = "20100101",
                                    max = "20100501"
                                }
                            };

                            adGroupCriterionSelector.idFilters = new AdGroupCriterionIdFilter[] 
                            {
                                new AdGroupCriterionIdFilter()
                                {
                                     adGroupId = adGroup.id,
                                     adGroupIdSpecified = true,
                                     campaignId = campaing.id,
                                     campaignIdSpecified = true
                                }
                            };

                           
                          
                            AdGroupCriterionPage adGroupCriterionPage = adgroupCriterionService.get(adGroupCriterionSelector);

                            if (adGroupCriterionPage != null && adGroupCriterionPage.entries != null)
                            {
                                foreach (BiddableAdGroupCriterion adGroupCriterion in adGroupCriterionPage.entries)
                                {
                                    Stats stats = adGroupCriterion.stats;
                                    
                                    if (adGroupCriterion.criterion.CriterionType == "Keyword")
                                    {
                                        Keyword keyword = adGroupCriterion.criterion as Keyword;
                                        
                                        Console.WriteLine("##############" + keyword.id + " " + keyword.text + " " + stats.ctr + " QScore: " + adGroupCriterion.qualityInfo.qualityScore);
                                        break;
                                    }    
                                }
                            }
                        }
                    }
                }

                Console.ReadLine();
            }
            catch (Exception e) 
            {
                Console.WriteLine(e);
                Console.ReadLine();
            }


        }
    }
}
