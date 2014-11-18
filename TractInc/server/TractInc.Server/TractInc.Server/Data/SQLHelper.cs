using System;
using System.Configuration;

namespace TractInc.Server.Data
{
    public static class SQLHelper
    {
        #region Configuration Values

        public const string TRACTINC_CONNECTION_STRING_KEY = "tractInc4ConnectionString";
/**/
        public const string WALT_CONNECTION_STRING_KEY = "waltConnectionString";
        public const string MAPOPTIX_CONNECTION_STRING_KEY = "mapOptixConnectionString";
/**/
        #endregion

        internal static String GetConnectionString(String key)
        {
            String result = ConfigurationManager.AppSettings[key];
            if (null == result || result.Length == 0)
            {
                throw new ConfigurationErrorsException("Connection string not found");
            }

            return result;
        }

    }
}
