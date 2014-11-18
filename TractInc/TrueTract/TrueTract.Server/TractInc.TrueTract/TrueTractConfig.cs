using System.Configuration;

namespace TractInc.TrueTract
{
    internal class TrueTractConfig
    {
        private const string PDF_EXPORT_DIR = "PdfExportDir";
        private const string PDF_EXPORT_URL = "PdfExportUrl";

        private const string EXCEL_EXPORT_DIR = "PdfExportDir";
        private const string EXCEL_EXPORT_URL = "PdfExportUrl";

        private const string ATTACHMENTS_BASE_DIR = "AttachmentsBaseDir";
        private const string ATTACHMENTS_BASE_URL = "AttachmentsBaseUrl";
        
        public static string PdfExportDir {
            get { return GetAppSettingValue(PDF_EXPORT_DIR); }
        }
        
        public static string PdfExportUrl {
            get { return GetAppSettingValue(PDF_EXPORT_URL); }
        }

        public static string ExcelExportDir
        {
            get { return GetAppSettingValue(EXCEL_EXPORT_DIR); }
        }

        public static string ExcelExportUrl
        {
            get { return GetAppSettingValue(EXCEL_EXPORT_URL); }
        }

        public static string AttachmentsBaseDir {
            get { return GetAppSettingValue(ATTACHMENTS_BASE_DIR); }
        }
        
        public static string AttachmentsBaseUrl {
            get { return GetAppSettingValue(ATTACHMENTS_BASE_URL); }
        }
        
       private static string GetAppSettingValue(string appSettingsKey)
       {
            string result = ConfigurationManager.AppSettings[appSettingsKey];

            if (null == result || result.Length == 0) 
            {
                throw new ConfigurationErrorsException(string.Format("Configuration param {0} not found", appSettingsKey));
            }

            return result;
           
       }
    }
}
