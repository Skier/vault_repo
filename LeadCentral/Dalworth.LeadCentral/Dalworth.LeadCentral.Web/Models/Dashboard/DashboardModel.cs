using System;
using System.Linq;
using System.Data;
using System.Collections.Generic;
using Dalworth.LeadCentral.Domain;
using Dalworth.LeadCentral.Service;

namespace Dalworth.LeadCentral.Web.Models.Dashboard
{
    public class DashboardModel
    {
        private const int LastActivitiesCount = 10;

        public int ActiveCampaigns { get; private set; }
        public decimal CurrentBalance { get; private set; }

        public int TotalLeads { get; private set; }
        public int ConvertedLeads { get; private set; }
        public decimal Amount { get; private set; }

        public string ChartAxisStr { get; private set; }
        public string ChartTotalLeadsStr { get; private set; }
        public string ChartCompletedLeadsStr { get; private set; }
        
        public string SelectedRange { get; set; }

        public List<ActivityLog> LastActivities { get; private set; }

        public List<Range> Ranges 
        { 
            get
            {
                var result = new List<Range>
                                 {
                                     Range.GetRangeByKey("day"),
                                     Range.GetRangeByKey("week"),
                                     Range.GetRangeByKey("month"),
                                     Range.GetRangeByKey("lastmonth"),
                                     Range.GetRangeByKey("year")
                                 };

                return result;
            }
        }
        
        public DashboardModel () {}

        public void Load(Domain.User currentUser, IDbConnection connection)
        {
            LastActivities = ActivityService.FindLast(LastActivitiesCount, connection);
            ActiveCampaigns = CampaignService.GetAllActive(connection).Count;
            CurrentBalance = BillingService.GetCurrentBalance(connection);

            var range = Range.GetRangeByKey(SelectedRange);
            if (range == null)
                SelectedRange = "day";

            range = Range.GetRangeByKey(SelectedRange);

            var statuses = new List<int>
                               {
                                   (int) LeadStatusEnum.Converted, 
                                   (int)LeadStatusEnum.Pending,
                                   (int)LeadStatusEnum.New,
                                   (int)LeadStatusEnum.Cancelled
                               };

            var filter = new LeadFilter
                             {
                                 LeadStatuses = statuses.ToArray(),
                                 DateCreatedFrom = range.StartDate,
                                 DateCreatedTo = range.EndDate
                             };

            statuses.Add((int)LeadStatusEnum.Pending);
            statuses.Add((int)LeadStatusEnum.New);
            statuses.Add((int)LeadStatusEnum.Cancelled);
            filter.LeadStatuses = statuses.ToArray();

            var leads = LeadService.Find(filter, currentUser, connection);
            var chartResult = LeadService.GetChart(SelectedRange, connection);
            
            Amount = leads.Sum(lead => lead.QbInvoices.Sum(qbInvoice => qbInvoice.Amount));
            TotalLeads = leads.Count;
            ConvertedLeads = leads.FindAll(temp => temp.LeadStatusId == (int)LeadStatusEnum.Converted).Count;

            ChartAxisStr = chartResult.AxisStr;
            ChartTotalLeadsStr = chartResult.TotalLeadsStr;
            ChartCompletedLeadsStr = chartResult.CompletedLeadsStr;
        }
    }

    internal class PlotLeadItem
    {
        public string Key { get; set; }
        public string Label { get; set; }
        public decimal Total { get; set; }
        public decimal Completed { get; set; }
    }

    public class Range
    {
        public string Key { get; set; }
        public string Label { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public static Range GetRangeByKey(string key)
        {
            var now = DateTime.Now;
            switch (key)
            {
                case "day":
                    return new Range
                               {
                                   Key = "day",
                                   Label = "Today",
                                   EndDate = DateTime.Now,
                                   StartDate = new DateTime(now.Year, now.Month, now.Day)
                               };
                case "week":
                    return new Range
                               {
                                   Key = "week", 
                                   Label = "Current Week", 
                                   EndDate = DateTime.Now, 
                                   StartDate = now.AddDays(-(int)now.DayOfWeek)
                               };

                case "month":
                    return new Range
                               {
                                   Key = "month", 
                                   Label = "MTD", 
                                   EndDate = DateTime.Now, 
                                   StartDate = new DateTime(now.Year, now.Month, 1)
                               };

                case "lastmonth":
                    return new Range
                    {
                        Key = "lastmonth",
                        Label = "Last month",
                        EndDate = DateTime.Now,
                        StartDate = new DateTime(now.Year, now.AddMonths(-1).Month, 1)
                    };

                case "year":
                    return new Range
                               {
                                   Key = "year", 
                                   Label = "YTD", 
                                   EndDate = DateTime.Now, 
                                   StartDate = new DateTime(now.Year, 1, 1)
                               };
                default:
                    return null;
            }
        }
    }
}