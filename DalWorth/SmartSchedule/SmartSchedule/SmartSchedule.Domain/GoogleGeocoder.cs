using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Web;

namespace SmartSchedule.Domain
{
    [DataContract]
    public struct Coordinate
    {
        #region Coordinate

        public Coordinate(float latitude, float longitude, bool isValid, string errorText)
        {
            m_latitude = latitude;
            m_longitude = longitude;
            m_isValid = isValid;
            m_errorText = errorText;
        }

        #endregion

        #region Latitude

        private float m_latitude;   
        [DataMember]
        public float Latitude
        {
            get { return m_latitude; }
            set { m_latitude = value; }
        }

        #endregion

        #region Longitude

        private float m_longitude;
        [DataMember]
        public float Longitude
        {
            get { return m_longitude; }
            set { m_longitude = value; }
        }

        #endregion

        #region IsValid

        private bool m_isValid;
        [DataMember]
        public bool IsValid
        {
            get { return m_isValid; }
            set { m_isValid = value; }
        }

        #endregion

        #region ErrorText

        private string m_errorText;
        [DataMember]
        public string ErrorText
        {
            get { return m_errorText; }
            set { m_errorText = value; }
        }

        #endregion
    }

    public class GoogleGeocoder
    {
        public static Coordinate GeocodeZip(string zip)
        {
            return Geocode(zip + ", USA");
        }

        public static Coordinate Geocode(string address)
        {
            for (int i = 1; i <= 5; i++)
            {
                try
                {
                    return GeocodeTry(address);
                }
                catch (Exception ex)
                {
                    if (i == 5)
                        throw;

                    Host.Trace("Geocoding Error", ex.Message);
                    Host.Trace("Geocoding", "Sleep 1 sec");
                    Thread.Sleep(1000);
                    continue;
                }
            }

            return new Coordinate(0, 0, false, "Unknown geocoding error");
        }

        private static Coordinate GeocodeTry(string address)
        {
            byte[] buffer = new byte[8192];

            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(
                string.Format("http://maps.google.com/maps/geo?q={0}&output=csv&oe=utf8&sensor=false&key=your_api_key&gl=us",
                HttpUtility.UrlEncode(address)));                        

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                Stream stream = response.GetResponseStream();
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                if (bytesRead != 0)
                {
                    string result = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    string[] splitResults = result.Split(new char[1] {','});

                    string resultCode = splitResults[0];

                    Coordinate resultCoordinate = new Coordinate(0, 0, false, string.Empty);

                    if (resultCode == "200")
                    {
                        resultCoordinate.IsValid = true;
                        resultCoordinate.ErrorText = string.Empty;
                        resultCoordinate.Latitude = float.Parse(splitResults[2]);
                        resultCoordinate.Longitude = float.Parse(splitResults[3]);

                        //Distance from Dalworth office
                        if (Utils.Distance(32.8216690, -97.0880650, resultCoordinate.Latitude,
                                           resultCoordinate.Longitude) > 150)
                        {
                            resultCoordinate.IsValid = false;
                            resultCoordinate.ErrorText = "Address is incorrect (too far)";
                            resultCoordinate.Latitude = 0;
                            resultCoordinate.Longitude = 0;
                        }
                    }
                    else if (resultCode == "602")
                        resultCoordinate.ErrorText = "Address is incorrect";
                    else if (resultCode == "620")
                    {
                        resultCoordinate.ErrorText = "Google service denied, too many queries";
                        throw new Exception(resultCoordinate.ErrorText);
                    }                        
                    else
                        resultCoordinate.ErrorText = "Unknown Geocoding error, result code " + resultCode;

                    return resultCoordinate;
                }

                throw new Exception("Unable to read HttpResponse");
            }
        }
    }
}
