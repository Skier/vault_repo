using System;
using System.Collections.Generic;


namespace SmartSchedule.Domain
{
    public partial class ZipCode
    {
        public ZipCode(){}

        #region Zips

        private static Dictionary<string, ZipCode> m_zips;
        public static Dictionary<string, ZipCode> Zips
        {
            get
            {
                if (m_zips == null)
                {
                    m_zips = new Dictionary<string, ZipCode>();
                    List<ZipCode> zips = Find();
                    foreach (ZipCode zip in zips)
                        m_zips.Add(zip.Zip, zip);
                }

                return m_zips;
            }
        }

        #endregion

        #region GetDistance

        public static double GetDistance(string zip1, string zip2)
        {
            ZipCode zipCode1 = Zips[zip1];
            ZipCode zipCode2 = Zips[zip2];

            return Utils.Distance(zipCode1.Latitude, zipCode1.Longitude, zipCode2.Latitude, zipCode2.Longitude);
        }

        #endregion
    }
}
      