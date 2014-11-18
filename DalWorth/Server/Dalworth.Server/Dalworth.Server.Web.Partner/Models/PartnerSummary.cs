using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dalworth.Server.Data;
using Dalworth.Server.Domain;

namespace Dalworth.Server.Web.Partner.Models
{
    public class PartnerSummary
    {
        public PartnerSummary(){}

        #region DateRangePresets

        public Dictionary<string, DateRangePreset> DateRangePresets
        {
            get
            {
                DateTime startOfCurrentMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

                Dictionary<string, DateRangePreset> result = new Dictionary<string, DateRangePreset>();
                result.Add("Month to Date", new DateRangePreset("Month to Date", startOfCurrentMonth, startOfCurrentMonth.AddMonths(1)));
                result.Add("Last Month", new DateRangePreset("Last Month", startOfCurrentMonth.AddMonths(-1), startOfCurrentMonth));
                result.Add("Custom", new DateRangePreset("Custom", null, null));
                return result;
            }
        }

        #endregion

        #region DateRangePreset

        private string m_dateRangePreset;
        public string DateRangePreset
        {
            get
            {
                if (string.IsNullOrEmpty(m_dateRangePreset))
                    return "Month to Date";
                return m_dateRangePreset;
            }
            set { m_dateRangePreset = value; }
        }

        #endregion

        #region DateStart

        [RegularExpression(@"^(?:(?:(?:0?[13578]|1[02])(\/|-|\.)31)\1|(?:(?:0?[13-9]|1[0-2])(\/|-|\.)(?:29|30)\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:0?2(\/|-|\.)29\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:(?:0?[1-9])|(?:1[0-2]))(\/|-|\.)(?:0?[1-9]|1\d|2[0-8])\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$",
            ErrorMessage = "Start Date is not valid")]
        public string DateStart { get; set; }

        #endregion

        #region DateEnd

        [RegularExpression(@"^(?:(?:(?:0?[13578]|1[02])(\/|-|\.)31)\1|(?:(?:0?[13-9]|1[0-2])(\/|-|\.)(?:29|30)\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:0?2(\/|-|\.)29\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:(?:0?[1-9])|(?:1[0-2]))(\/|-|\.)(?:0?[1-9]|1\d|2[0-8])\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$",
            ErrorMessage = "End Date is not valid")]
        public string DateEnd { get; set; }

        #endregion

        #region DateStartNoPreset

        public string DateStartNoPreset
        {
            get
            {
                DateTime? result = null;
                DateRangePreset currentPreset = DateRangePresets[DateRangePreset];

                if (currentPreset.Name != "Custom")
                    result = currentPreset.DateStart;
                else
                {
                    try
                    {
                        result = DateTime.Parse(DateStart);
                    }
                    catch (Exception) { }
                }

                if (result != null)
                    return result.Value.ToString("d");
                return string.Empty;
            }
        }

        #endregion

        #region DateEndNoPreset

        public string DateEndNoPreset
        {
            get
            {
                DateTime? result = null;
                DateRangePreset currentPreset = DateRangePresets[DateRangePreset];

                if (currentPreset.Name != "Custom")
                    result = currentPreset.DateEnd;
                else
                {
                    try
                    {
                        result = DateTime.Parse(DateEnd);
                    }
                    catch (Exception) { }
                }

                if (result != null)
                    return result.Value.ToString("d");
                return string.Empty;
            }
        }

        #endregion

        public List<PartnerSummaryItem> SummaryItems { get; set; }
        public string CurrentPartner { get; set; }


        #region LoadItems

        public void LoadItems()
        {
            DateRangePreset currentPreset = DateRangePresets[DateRangePreset];

            DateTime? start = null;
            DateTime? end = null;

            if (currentPreset.Name != "Custom")
            {
                start = currentPreset.DateStart;
                end = currentPreset.DateEnd;
            }
            else
            {
                try
                {
                    start = DateTime.Parse(DateStart);
                }
                catch (Exception) { }

                try
                {
                    end = DateTime.Parse(DateEnd);
                }
                catch (Exception) { }

            }

            WebUser user = PartnerMembershipProvider.GetCurrentUser();
            int? callSourceId = null;
            if (!user.IsOwner)
                callSourceId = user.OrderSourceId.Value;
            else if (!string.IsNullOrEmpty(CurrentPartner))
                callSourceId = int.Parse(CurrentPartner);

            SummaryItems = PartnerSummaryItem.Find(start, end, callSourceId);
        }

        #endregion        

        #region GetPartners

        public List<SelectListItem> GetPartners()
        {
            List<SelectListItem> result = new List<SelectListItem>();

            SelectListItem allItem = new SelectListItem();
            allItem.Text = "ALL";
            allItem.Value = "";
            result.Add(allItem);

            using (IDbConnection connection = Connection.Instance.GetTemporaryDbConnection())
            {
                connection.Open();
                foreach (OrderSource orderSource in OrderSource.Find(connection).OrderBy(source => source.Name))
                {
                    SelectListItem item = new SelectListItem();
                    item.Text = orderSource.Name;
                    item.Value = orderSource.ID.ToString();
                    result.Add(item);
                }
            }            

            return result;
        }

        #endregion

        #region GetCurrentUserPartnerName

        public string GetCurrentUserPartnerName()
        {
            WebUser user = PartnerMembershipProvider.GetCurrentUser();
            if (!user.OrderSourceId.HasValue)
                return string.Empty;
            using (IDbConnection connection = Connection.Instance.GetTemporaryDbConnection())
            {
                connection.Open();
                return OrderSource.FindByPrimaryKey(user.OrderSourceId.Value, connection).Name;                
            }            

        }

        #endregion
    }
}