using System;
using System.Web;
using DPI.Interfaces;
using DPI.Services;

namespace Dpi.Central.Web.Controllers
{
    public enum EditorMode
    {
        Creator,
        Editor
    }

    public abstract class ControllerBase
    {
        #region Constants

        protected const string MAP_KEY = "map_key";
        protected const string RECURRING_PAYMENT_ID_KEY = "recurring_payment_id_key";
        protected const string PAYMENTS_KEY = "payments_key";
        protected const string ACCOUNT_INFO_KEY = "account_info_key";
        protected const string CUST_INFO_EXT2_KEY = "cust_info_ext2_key";
        protected const string EDITOR_MODE_KEY = "EDITOR_MODE_KEY";

        #endregion

        protected ControllerBase() 
        {
        }

        public IAcctInfo RetrieveAccountInfo()
        {
            object value = HttpContext.Current.Session[ACCOUNT_INFO_KEY];

            if (value == null) {
                IAcctInfo acctInfo = CustSvc.GetAcctInfo(Map, AccountNumber);
                value = HttpContext.Current.Session[ACCOUNT_INFO_KEY] = acctInfo;
            }
            
            return (IAcctInfo)value;
        }

        public ICustInfoExt2 RetrieveCustInfoExt2()
        {
            object value = HttpContext.Current.Session[CUST_INFO_EXT2_KEY];

            if (value == null) {
                ICustInfoExt2 custInfo = CustSvc.GetCustInfoExt2(Map, AccountNumber);
                value = HttpContext.Current.Session[CUST_INFO_EXT2_KEY] = custInfo;
            }

            return (ICustInfoExt2)value;
        }

        protected object GetValue(string key)
        {
            object value = HttpContext.Current.Session[key];
            if (value != null) {
                return value;
            }

            throw new ArgumentException("Value with '" + key
                + "' key is empty or does not exist.");
        }

        protected void SetValue(string key, object value)
        {
            HttpContext.Current.Session[key] = value;
        }

        protected IMap Map
        {
            get
            {
                object value = HttpContext.Current.Session[MAP_KEY];
                if (value == null) {
                    value = HttpContext.Current.Session[MAP_KEY] = IMapFactory.getIMap();
                }

                return (IMap) value;
            }
        }

        public int AccountNumber
        {
            get
            {
                HttpContext ctx = HttpContext.Current;

                if (ctx.User != null && ctx.User.Identity.IsAuthenticated) {
                    return Int32.Parse(HttpContext.Current.User.Identity.Name);
                }

                throw new InvalidOperationException("User must be authenticated before getting account number.");
            }
        }
    }
}