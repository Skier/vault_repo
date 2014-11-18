using System;
  
namespace Dalworth.Server.Domain
{
    public enum MonitoringReadingTypeEnum
    {
        Outside = 1,
        Inside = 2,
        Unaffected = 3,
        Dehumidifier = 4,
        ACUnit = 5
    }


    public partial class MonitoringReadingType
    {
        public MonitoringReadingType(){ }

        #region GetText

        public static string GetText(MonitoringReadingTypeEnum readingType)
        {
            if (readingType == MonitoringReadingTypeEnum.ACUnit)
                return "AC Unit";
            return readingType.ToString();
        }

        #endregion
    }
}
      