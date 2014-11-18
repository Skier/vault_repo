using System;
using DPI.Components;
using DPI.Interfaces;
using DPI.Services;

namespace Dpi.Central.Web.Controllers
{
    public class AccountSummaryController : ControllerBase
    {
        #region Static Members

        private static AccountSummaryController _instance;

        public static AccountSummaryController Instance
        {
            get
            {
                lock (typeof (AccountSummaryController)) {
                    if (_instance == null) {
                        _instance = new AccountSummaryController();
                    }
                }

                return _instance;
            }
        }

        #endregion

        protected AccountSummaryController() : base()
        {
        }

        public string RetrieveReminderNoticeFileName()
        {
            IPastReminderNotice notice;
            notice = CustSvc.GetReminderNotice(Map, AccountNumber);

            if (notice != null && notice.Filename != null) {
                return notice.Filename.Trim();
            }

            return null;
        }
    }
}