using System;
using System.Data;
using System.Collections.Generic;
using Dalworth.LeadCentral.Domain;
using Dalworth.LeadCentral.Service;

namespace Dalworth.LeadCentral.Web.Models.Lead
{
    public class LeadList
    {
        #region statuses

        private const string DefaultLeadStatuses = "New & Pending";

        private string SelectedLeadStatuses;
        public string LeadStatuses
        {
            get
            {
                return string.IsNullOrEmpty(SelectedLeadStatuses) ? DefaultLeadStatuses : SelectedLeadStatuses;
            }
            set
            {
                SelectedLeadStatuses = value;
            }
        }

        public Dictionary<string, List<int>> PredefinedLeadStatuses
        {
            get
            {
                var result = new Dictionary<string, List<int>>
                                 {
                                     {DefaultLeadStatuses, new List<int> {(int) LeadStatusEnum.New, (int) LeadStatusEnum.Pending}},
                                     {"Cancelled", new List<int> {(int) LeadStatusEnum.Cancelled}},
                                     {"Converted", new List<int> {(int) LeadStatusEnum.Converted}}
                                 };

                return result;
            }
        }

        #endregion

        #region salesreps

        private List<Domain.User> SalesReps;

        public int SalesRepId { get; set; }
        public List<Domain.User> SalesRepList
        {
            get
            {
                var result = new List<Domain.User> { new Domain.User { Id = 0, ScreenName = "All" } };
                if (SalesReps != null && SalesReps.Count > 0)
                    result.AddRange(SalesReps);

                return result;
            }
        }

        #endregion

        #region partners

        private List<BusinessPartner> m_partners;

        public int PartnerId { get; set; }
        public List<BusinessPartner> PartnerList
        {
            get
            {
                var result = new List<BusinessPartner> { new BusinessPartner { Id = 0, PartnerName = "All" } };
                if (m_partners != null && m_partners.Count > 0)
                    result.AddRange(m_partners);

                return result;
            }
        }

        #endregion

        #region campaigns

        private List<Domain.Campaign> Campaigns;
        public int CampaignId { get; set; }
        public List<Domain.Campaign> CampaignList
        {
            get
            {
                var result = new List<Domain.Campaign> { new Domain.Campaign { Id = 0, CampaignName = "All" } };
                if (Campaigns != null && Campaigns.Count > 0)
                    result.AddRange(Campaigns);

                return result;
            }
        }

        #endregion

        #region tracking phones

        private List<TrackingPhone> TrackingPhones;

        public int TrackingPhoneId { get; set; }
        public List<TrackingPhone> TrackingPhoneList
        {
            get
            {
                var result = new List<TrackingPhone> { new TrackingPhone { Id = 0, PhoneNumber = "All" } };
                if (TrackingPhones != null && TrackingPhones.Count > 0)
                    result.AddRange(TrackingPhones);

                return result;
            }
        }

        #endregion

        #region dates

        private const string DefaultDateRange = "Last 15 days";

        private string SelectedDateRange;
        public string DateRange
        {
            get
            {
                return string.IsNullOrEmpty(SelectedDateRange) ? DefaultDateRange : SelectedDateRange;
            }
            set { SelectedDateRange = value; }
        }

        public Dictionary<string, DateRange> PredefinedDateRanges
        {
            get
            {
                var result = new Dictionary<string, DateRange>
                                 {
                                     {"Today", new DateRange("Today", DateTime.Now, DateTime.Now)},
                                     {"Yesterday", new DateRange("Yesterday", DateTime.Now.AddDays(-1), null)},
                                     {"Last 7 days", new DateRange(DefaultDateRange, DateTime.Now.AddDays(-7), null)},
                                     {"Last 15 days", new DateRange("Last 15 days", DateTime.Now.AddDays(-15), null)},
                                     {"Last 30 days", new DateRange("Last 30 days", DateTime.Now.AddDays(-30), null)},
                                     {"Custom", new DateRange("Custom", null, null)}
                                 };
                return result;
            }
        }

        #endregion

        public List<Domain.Lead> Leads { get; private set; }

        public void InitLeads(Domain.User currentUser, IDbConnection connection)
        {
            Leads = LeadService.Find(CreateLeadFilter(), currentUser, connection);
            TotalCount = Leads.Count;
            TotalAmount = 0;
            foreach (var lead in Leads)
            {
                TotalAmount += lead.Amount;
            }

            SalesReps = UserService.FindStaff(connection);
            m_partners = BusinessPartnerService.Find(connection);
            Campaigns = CampaignService.GetAllActive(connection);
            TrackingPhones = TrackingPhoneService.GetAll(connection);
        }

        public DateTime? DateCreatedFrom { get; set; }
        public DateTime? DateCreatedTo { get; set; }

        public int TotalCount { get; set; }
        public decimal TotalAmount { get; set; }

        private LeadFilter CreateLeadFilter()
        {
            var leadFilter = new LeadFilter();

            leadFilter.LeadStatuses = PredefinedLeadStatuses[LeadStatuses].ToArray();
            leadFilter.SalesRepId = SalesRepId;
            leadFilter.BusinessPartnerId = PartnerId;
            leadFilter.CampaignId = CampaignId;
            leadFilter.TrackingPhoneId = TrackingPhoneId;

            leadFilter.DateCreatedFrom = DateCreatedFrom;
            leadFilter.DateCreatedTo = DateCreatedTo;

            return leadFilter;
        }
    }

    public class DateRange
    {
        public DateRange(string name, DateTime? dateStart, DateTime? dateEnd)
        {
            Name = name;
            DateFrom = dateStart;
            DateTo = dateEnd;
        }

        public string Name { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }
}