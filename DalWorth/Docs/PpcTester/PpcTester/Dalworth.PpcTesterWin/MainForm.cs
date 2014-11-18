using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using Dalworth.Server.SDK;
using Dalworth.Server.Domain;
using Dalworth.Server.Domain.Package;
using Dalworth.Server.Data;


using adwords = com.google.api.adwords.v200909;


namespace Dalworth.PpcTesterWin
{
    public partial class MainForm : System.Windows.Forms.Form
    {
        private AdwordsApi m_adwordsApi = null;
        private GoogleTestThread m_googleTestThread;
        public MainForm()
        {
            InitializeComponent();
        }

        #region OnLoad

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            m_dtStatsStart.DateTime = new DateTime(2010, 05, 02);

            List<Company> companies = Company.Find();
            foreach (Company company in companies)
            {
                m_cmbCompany.Properties.Items.Add(new CompanyComboBoxItem(company));
                m_cmbCompanyTesting.Properties.Items.Add(new CompanyComboBoxItem(company));
            }
        }

        #endregion

        #region Import EventHandlers

        private void m_btnUpdateAllCampaigns_Click(object sender, EventArgs e)
        {
            Company selectedCompany = (m_cmbCompany.SelectedItem as CompanyComboBoxItem).Company;
            if (selectedCompany == null)
                return;

            UpdateAllGoogleCampaigns(selectedCompany);

            List<Campaign> campaigns = Campaign.Find(selectedCompany.Id, 1, null);
            m_cmbCompaigns.Properties.Items.Clear();

            foreach (Campaign campaign in campaigns)
            {
                m_cmbCompaigns.Properties.Items.Add(new CampaignComboBoxItem(campaign));
            }

            MessageBox.Show("Success!");
            
        }

        private void m_cmbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            Company selectedCompany = (m_cmbCompany.SelectedItem as CompanyComboBoxItem).Company;
            if (selectedCompany == null)
                return;

            List<Campaign> campaigns = Campaign.Find(selectedCompany.Id, 1, null);
            m_cmbCompaigns.Properties.Items.Clear();

            foreach (Campaign campaign in campaigns)
            {
                m_cmbCompaigns.Properties.Items.Add(new CampaignComboBoxItem(campaign));
            }
           
        }

        private void m_btnUpdateCompaign_Click(object sender, EventArgs e)
        {
            Company selectedCompany = (m_cmbCompany.SelectedItem as CompanyComboBoxItem).Company;
            if (selectedCompany == null)
                return;

            Campaign selectedCampaign = (m_cmbCompaigns.SelectedItem as CampaignComboBoxItem).Campaign;
            if (selectedCampaign == null)
                return;

            UpdateCampaign(selectedCompany, selectedCampaign);

        }

        private void m_btnUpdateCampaignStats_Click(object sender, EventArgs e)
        {
            Company selectedCompany = (m_cmbCompany.SelectedItem as CompanyComboBoxItem).Company;
            if (selectedCompany == null)
                return;

            Campaign selectedCampaign = (m_cmbCompaigns.SelectedItem as CampaignComboBoxItem).Campaign;
            if (selectedCampaign == null)
                return;

            DateTime statsStart = m_dtStatsStart.DateTime;

            UpdateAdgroups(selectedCompany, selectedCampaign, statsStart);
        }

        private void m_cmbCompanyTesting_SelectedIndexChanged(object sender, EventArgs e)
        {
            Company selectedCompany = (m_cmbCompanyTesting.SelectedItem as CompanyComboBoxItem).Company;
            if (selectedCompany == null)
                return;

            List<Campaign> campaigns = Campaign.Find(selectedCompany.Id, 1, null);

            m_cmbCampaignsTesting.Properties.Items.Clear();

            foreach (Campaign campaign in campaigns)
            {
                m_cmbCampaignsTesting.Properties.Items.Add(new CampaignComboBoxItem(campaign));
            }

        }

        #endregion

        #region CampaignUpdate

        private void UpdateAllGoogleCampaigns(Company company)
        {
            if (m_adwordsApi == null)
                m_adwordsApi = new AdwordsApi(company.GoogleClientEmail);

            adwords.CampaignPage campaignPage = m_adwordsApi.GetAllGoogleCampaings();

            foreach (adwords.Campaign googleCampaign in campaignPage.entries)
            {
                try
                {
                    Campaign campaign = Campaign.FindByPrimaryKey(googleCampaign.id, 1);

                    int newStatus = m_adwordsApi.MapCampaingStatus(googleCampaign.status);

                    if (newStatus != campaign.Status)
                    {
                        campaign.Status = newStatus;
                        Campaign.Update(campaign);
                    }
                }
                catch (DataNotFoundException ex)
                {
                    Campaign campaign = new Campaign()
                    {
                        CompanyId = company.Id,
                        Id = googleCampaign.id,
                        SearchEngineId = 1,
                        Name = googleCampaign.name,
                        Status = m_adwordsApi.MapCampaingStatus(googleCampaign.status)
                    };

                    Campaign.Insert(campaign);    
                }       
            }
        }

        private void UpdateCampaign(Company company, Campaign campaign)
        {
            if (m_adwordsApi == null)
                m_adwordsApi = new AdwordsApi(company.GoogleClientEmail);

            adwords.AdGroupPage adgroupPage = m_adwordsApi.GetAdgroups(campaign.Id);

            foreach (adwords.AdGroup adwordsAdgroup in adgroupPage.entries)
            {
                AdGroup adgroup;
                try
                {
                    adgroup = AdGroup.FindByPrimaryKey(adwordsAdgroup.id, 1);

                    int newstatus = m_adwordsApi.MapAdgroupStatus(adwordsAdgroup.status);

                    if (newstatus != adgroup.Status)
                    {
                        adgroup.Status = newstatus;
                        AdGroup.Update(adgroup);
                    }
                }
                catch (DataNotFoundException ex)
                {
                    adgroup = new AdGroup()
                    {
                        CampaignId = campaign.Id,
                        Id = adwordsAdgroup.id,
                        Name = adwordsAdgroup.name,
                        SearchEngineId = 1,
                        Status = m_adwordsApi.MapAdgroupStatus(adwordsAdgroup.status)
                    };
                    AdGroup.Insert(adgroup);
                }
            }
        }

        private void UpdateAdgroups(Company company, Campaign campaign, DateTime statsStartDate)
        {
            DateTime today = DateTime.Now.Date;

            List<AdGroup> adgroups = AdGroup.Find(1, campaign.Id, null);

            List<DateTime> availableStatsDates = KeywordStats.FindAvailableStatsDates(campaign.Id, 1, statsStartDate, null);

            for (DateTime dt = statsStartDate; dt <= today; dt = dt.AddDays(7))
            {
                if (availableStatsDates.Contains(dt))
                    continue;

                foreach (AdGroup adGroup in adgroups)
                {
                    if (adGroup.Status == 1)
                        UpdateAdGroup(company, campaign, adGroup, dt, dt.AddDays(6));
                }
            }
        }

        private void UpdateAdGroup(Company company, Campaign campaign, AdGroup adGroup, DateTime statsDateStart, DateTime statsDateEnd)
        {
            if (m_adwordsApi == null)
                m_adwordsApi = new AdwordsApi(company.GoogleClientEmail);

            DateTime start = statsDateStart;
            DateTime end = statsDateEnd;

            adwords.AdGroupCriterionPage page = m_adwordsApi.GetAdgroupCriterions(campaign.Id, adGroup.Id, start, end);

            if (page == null || page.entries == null)
                return;
        
            foreach (adwords.BiddableAdGroupCriterion adGroupCriterion in page.entries)
            {
               adwords.Stats stats = adGroupCriterion.stats;

               if (adGroupCriterion.criterion.CriterionType != "Keyword")
                   continue;

               adwords.Keyword adwordsKeyword = adGroupCriterion.criterion as adwords.Keyword;

               decimal maxCPC = 0;
               if (((com.google.api.adwords.v200909.ManualCPCAdGroupCriterionBids)(adGroupCriterion.bids)).maxCpc != null)
                 maxCPC = ((com.google.api.adwords.v200909.ManualCPCAdGroupCriterionBids)(adGroupCriterion.bids)).maxCpc.amount.microAmount / 1000000;

               Keyword keyword = null;

               try
               {
                    keyword = Keyword.FindByKeywordString(adwordsKeyword.text, null);
               }
               catch (DataNotFoundException ex)
               {
                   keyword = new Keyword()
                   {
                       KeywordString = adwordsKeyword.text
                   };

                   Keyword.Insert(keyword);
               }

                AdgroupKeyword adgroupKeyword; 

               try
               {
                   adgroupKeyword = AdgroupKeyword.FindByPrimaryKey(1, adwordsKeyword.id);

                   int newStatus = m_adwordsApi.MapKeywordStatus(adGroupCriterion.userStatus);
                   if (adgroupKeyword.Status != newStatus)
                   {
                       adgroupKeyword.Status = newStatus;
                       AdgroupKeyword.Update(adgroupKeyword);
                   }
               }
               catch (DataNotFoundException ex)
               {
                   adgroupKeyword = new AdgroupKeyword()
                   {
                       SearchEngineKeywordId = adwordsKeyword.id,
                       MatchType = m_adwordsApi.MapKeywordMatchType(adwordsKeyword.matchType),
                       SearchEngineId = 1,
                       KeywordId = keyword.Id,
                       Status = m_adwordsApi.MapKeywordStatus(adGroupCriterion.userStatus), 
                       AdGroupId = adGroup.Id
                   };

                   AdgroupKeyword.Insert(adgroupKeyword, null);
               }

               List<KeywordStats> keywordstats = KeywordStats.Find(1, adwordsKeyword.id, statsDateStart, null);

              if (keywordstats.Count == 1)
                  continue;

              KeywordStats keywordStats = new KeywordStats()
               {
                   Clicks = (int)stats.clicks,
                   CTR = (float)stats.ctr,
                   DateStart = start,
                   DateEnd = end,
                   FirstPageCPC = adGroupCriterion.firstPageCpc != null ? adGroupCriterion.firstPageCpc.amount.microAmount / 1000000: 0,
                   Impressions = stats.impressions,
                   IsKeywordAdRelevanceAcceptable = adGroupCriterion.qualityInfo.isKeywordAdRelevanceAcceptable,
                   IsLandingPageLatencyAcceptable = adGroupCriterion.qualityInfo.isLandingPageLatencyAcceptable,
                   IsLandingPageQualityAcceptable = adGroupCriterion.qualityInfo.isLandingPageLatencyAcceptable,
                   QualityScore = adGroupCriterion.qualityInfo.qualityScore,
                   SearchEngineId = 1,
                   SearchEngineKeywordId = adwordsKeyword.id,
                   SystemServingStatus = m_adwordsApi.MapSystemServingStatus(adGroupCriterion.systemServingStatus),
                   MaxCPC = ((adwords.ManualCPCAdGroupCriterionBids)(adGroupCriterion.bids)).maxCpc != null? ((adwords.ManualCPCAdGroupCriterionBids)(adGroupCriterion.bids)).maxCpc.amount.microAmount / 1000000: 0,
                   AveragePosition = (long)stats.averagePosition
               };

             KeywordStats.Insert(keywordStats);

            }
        }
  
      #endregion 

        #region Testing EventHandlers

        private void m_btnTest_Click(object sender, EventArgs e)
        {
            CampaignComboBoxItem m_testCampaign = m_cmbCampaignsTesting.SelectedItem as CampaignComboBoxItem;
            int maxImpressions = int.Parse(m_spinMaxImpressions.Text);

            List<AdgroupKeywordPackage> keywords = AdgroupKeywordPackage.FindByImpressions(m_testCampaign.Campaign.Id, maxImpressions, null);

            m_googleTestThread = new GoogleTestThread(m_testCampaign.Campaign, keywords);

            m_googleTestThread.Start();

        }

        #endregion

        #region Google Testing

        private void StartGoogleCompaign()
        {

        }

        #endregion 

    }

    #region private classes

    class CompanyComboBoxItem
    {
        public Company Company { get; set; }
        public CompanyComboBoxItem(Company company)
        {
            Company = company;
        }

        public override string ToString()
        {
            return Company.Name.Trim();

        }
    }

    class CampaignComboBoxItem
    {
        public Campaign Campaign { get; set; }
        public CampaignComboBoxItem(Campaign campaign)
        {
            Campaign = campaign;
        }

        public override string ToString()
        {
            return Campaign.Name.Trim();

        }
    }

    #endregion
}
