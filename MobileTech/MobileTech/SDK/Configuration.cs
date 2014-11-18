using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Xml;
using System.IO;
using System.Reflection;
using System.Drawing;
using MobileTech.Windows.UI;

namespace MobileTech
{

    public enum ConfigurationKey
    {
        ReplInternetUrl,
        ReplInternetLogin,
        ReplInternetPassword,
        ReplMasterLogin,
        ReplMasterPassword,
        ReplMasterPublisher,
        ReplMasterPublisherDb,
        ReplMasterPublication,
        ReplTransLogin,
        ReplTransPassword,
        ReplTransPublisher,
        ReplTransPublisherDb,
        ReplTransPublication,
        ReplOtherPublication,
        DbLocalFile,
        VCUrl,
        VCPackageVer,
        VCLogin,
        VCPassword,
        HttpUpdateUrl,
        HttpUpdateTemp,
        HttpLogin,
        HttpPassword
    }

    public class Configuration
    {
#if WINCE
        public const string FILE_PATH = @"\MobileTech.WinCE.xml";
#else
        public const string FILE_PATH = @"\MobileTech.Win32.xml";
#endif
        //Global Variables
        private static int _sessionId = 1;
        private static bool _vc = false;
        private static bool _sync = false;
        private static bool _transExportXML = false;
        private static bool _initDB = false;
        private static bool _vcRestart = false;
        private static bool _vcUpdate = false;
        private static string _appNameFullPath = "";
        private static string _connectionString = "";
        private static bool _appStart = true;
        private static int _route = 0;
        private static int _location = 0;
        private static string _dbFullPath;
        private static string _dbLogFullPath;
        private static string _masterConnectionString = "";

        private Configuration()
        {

        }

        public static EventType EventStoreLevel
        {
            get
            {
                return EventType.Exception;
            }
        }

        public static void LoadGlobalConfiguration()
        {
            // Configuration
            XmlDocument xmlConfig;

            //MGS Add string reference here
            string appDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName);
            xmlConfig = new XmlDocument();
            try
            {
                xmlConfig.Load(appDir + FILE_PATH);
            }
            catch (Exception ex)
            {
                throw new MobileTechException(ex);
            }
            try
            {
                string sVC = xmlConfig["application"]["settings"].GetAttribute("VC");
                string sSync = xmlConfig["application"]["settings"].GetAttribute("Sync");
                string sInitDB = xmlConfig["application"]["settings"].GetAttribute("InitDB");
                //This loads the connection string with variables for CE
                //It appends to the full connection string for CE in the program startup
                _connectionString = xmlConfig["application"]["database"].GetAttribute("ConnectionString");
                _masterConnectionString = xmlConfig["application"]["database"].GetAttribute("MasterConnectionString");

                sSync.Trim();
                sVC.Trim();
                sInitDB.Trim();
                if (sVC.ToUpper() == "TRUE")
                {
                    _vc = true;
                }
                else
                {
                    _vc = false;
                }
                if (sSync.ToUpper() == "TRUE")
                {
                    _sync = true;
                }
                else
                {
                    _sync = false;
                }
                if (sInitDB.ToUpper() == "TRUE")
                {
                    _initDB = true;
                }
                else
                {
                    _initDB = false;
                }

            }
            catch (Exception ex)
            {
                throw new MobileTechException(ex);
            }
        }
        public static void LoadImages()
        {
            try
            {
                
                string bitmapDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName) + @"\Bitmaps\";

                GUI.SetImage(ImageKeys.About, bitmapDir + "About.png");
                GUI.SetImage(ImageKeys.AboutFocus, bitmapDir + "AboutFocus.png");
                GUI.SetImage(ImageKeys.AboutDisabled, bitmapDir + "AboutDisabled.png");
                GUI.SetImage(ImageKeys.Add_Small, bitmapDir + "Add_Small.png");
                GUI.SetImage(ImageKeys.Add_SmallFocus, bitmapDir + "Add_SmallFocus.png");
                GUI.SetImage(ImageKeys.Add_SmallDisabled, bitmapDir + "Add_SmallDisabled.png");
                GUI.SetImage(ImageKeys.ARCollection, bitmapDir + "ARCollection.png");
                GUI.SetImage(ImageKeys.ARCollectionFocus, bitmapDir + "ARCollectionFocus.png");
                GUI.SetImage(ImageKeys.ARCollectionDisabled, bitmapDir + "ARCollectionDisabled.png");
                GUI.SetImage(ImageKeys.Back_Small, bitmapDir + "Back_Small.png");
                GUI.SetImage(ImageKeys.Back_SmallFocus, bitmapDir + "Back_SmallFocus.png");
                GUI.SetImage(ImageKeys.Back_SmallDisabled, bitmapDir + "Back_SmallDisabled.png");
                GUI.SetImage(ImageKeys.Close, bitmapDir + "Close.png");
                GUI.SetImage(ImageKeys.CloseFocus, bitmapDir + "CloseFocus.png");
                GUI.SetImage(ImageKeys.CloseDisabled, bitmapDir + "CloseDisabled.png");
                GUI.SetImage(ImageKeys.Cancel_Small, bitmapDir + "Cancel_Small.png");
                GUI.SetImage(ImageKeys.Cancel_SmallFocus, bitmapDir + "Cancel_SmallFocus.png");
                GUI.SetImage(ImageKeys.Cancel_SmallDisabled, bitmapDir + "Cancel_SmallDisabled.png");
                GUI.SetImage(ImageKeys.Continue, bitmapDir + "Continue.png");
                GUI.SetImage(ImageKeys.ContinueFocus, bitmapDir + "ContinueFocus.png");
                GUI.SetImage(ImageKeys.ContinueDisabled, bitmapDir + "ContinueDisabled.png");
                GUI.SetImage(ImageKeys.CustList, bitmapDir + "CustList.png");
                GUI.SetImage(ImageKeys.CustListFocus, bitmapDir + "CustListFocus.png");
                GUI.SetImage(ImageKeys.CustListDisabled, bitmapDir + "CustListDisabled.png");
                GUI.SetImage(ImageKeys.CustomerService, bitmapDir + "CustomerService.png");
                GUI.SetImage(ImageKeys.CustomerServiceFocus, bitmapDir + "CustomerServiceFocus.png");
                GUI.SetImage(ImageKeys.CustomerServiceDisabled, bitmapDir + "CustomerServiceDisabled.png");
                GUI.SetImage(ImageKeys.Detail_Small, bitmapDir + "Detail_Small.png");
                GUI.SetImage(ImageKeys.Detail_SmallFocus, bitmapDir + "Detail_SmallFocus.png");
                GUI.SetImage(ImageKeys.Detail_SmallDisabled, bitmapDir + "Detail_SmallDisabled.png");
                GUI.SetImage(ImageKeys.Done, bitmapDir + "Done.png");
                GUI.SetImage(ImageKeys.DoneFocus, bitmapDir + "DoneFocus.png");
                GUI.SetImage(ImageKeys.DoneDisabled, bitmapDir + "DoneDisabled.png");
                GUI.SetImage(ImageKeys.Done_Small, bitmapDir + "Done_Small.png");
                GUI.SetImage(ImageKeys.Done_SmallFocus, bitmapDir + "Done_SmallFocus.png");
                GUI.SetImage(ImageKeys.Done_SmallDisabled, bitmapDir + "Done_SmallDisabled.png");
                GUI.SetImage(ImageKeys.EndDay, bitmapDir + "EndDay.png");
                GUI.SetImage(ImageKeys.EndDayFocus, bitmapDir + "EndDayFocus.png");
                GUI.SetImage(ImageKeys.EndDayDisabled, bitmapDir + "EndDayDisabled.png");
                GUI.SetImage(ImageKeys.EndDayDone, bitmapDir + "EndDayDone.png");
                GUI.SetImage(ImageKeys.EndDayDoneFocus, bitmapDir + "EndDayDoneFocus.png");
                GUI.SetImage(ImageKeys.EndDayDoneDisabled, bitmapDir + "EndDayDoneDisabled.png");
                GUI.SetImage(ImageKeys.Events, bitmapDir + "Events.png");
                GUI.SetImage(ImageKeys.EventsFocus, bitmapDir + "EventsFocus.png");
                GUI.SetImage(ImageKeys.EventsDisabled, bitmapDir + "EventsDisabled.png");
                GUI.SetImage(ImageKeys.Exit, bitmapDir + "Exit.png");
                GUI.SetImage(ImageKeys.ExitFocus, bitmapDir + "ExitFocus.png");
                GUI.SetImage(ImageKeys.ExitDisabled, bitmapDir + "ExitDisabled.png");
                GUI.SetImage(ImageKeys.House, bitmapDir + "House.png");
                GUI.SetImage(ImageKeys.HouseFocus, bitmapDir + "HouseFocus.png");
                GUI.SetImage(ImageKeys.HouseDisabled, bitmapDir + "HouseDisabled.png");
                GUI.SetImage(ImageKeys.Information, bitmapDir + "Information.png");
                GUI.SetImage(ImageKeys.InformationFocus, bitmapDir + "InformationFocus.png");
                GUI.SetImage(ImageKeys.InformationDisabled, bitmapDir + "InformationDisabled.png");
                GUI.SetImage(ImageKeys.Inventory, bitmapDir + "Inventory.png");
                GUI.SetImage(ImageKeys.InventoryFocus, bitmapDir + "InventoryFocus.png");
                GUI.SetImage(ImageKeys.InventoryDisabled, bitmapDir + "InventoryDisabled.png");
                GUI.SetImage(ImageKeys.Invoice, bitmapDir + "Invoice.png");
                GUI.SetImage(ImageKeys.InvoiceFocus, bitmapDir + "InvoiceFocus.png");
                GUI.SetImage(ImageKeys.InvoiceDisabled, bitmapDir + "InvoiceDisabled.png");
                GUI.SetImage(ImageKeys.Load, bitmapDir + "Load.png");
                GUI.SetImage(ImageKeys.LoadFocus, bitmapDir + "LoadFocus.png");
                GUI.SetImage(ImageKeys.LoadDisabled, bitmapDir + "LoadDisabled.png");
                GUI.SetImage(ImageKeys.LoadRequest, bitmapDir + "LoadRequest.png");
                GUI.SetImage(ImageKeys.LoadRequestFocus, bitmapDir + "LoadRequestFocus.png");
                GUI.SetImage(ImageKeys.LoadRequestDisabled, bitmapDir + "LoadRequestDisabled.png");
                GUI.SetImage(ImageKeys.LoadTransfer, bitmapDir + "LoadTransfer.png");
                GUI.SetImage(ImageKeys.LoadTransferFocus, bitmapDir + "LoadTransferFocus.png");
                GUI.SetImage(ImageKeys.LoadTransferDisabled, bitmapDir + "LoadTransferDisabled.png");
                GUI.SetImage(ImageKeys.Merchandising, bitmapDir + "Merchandising.png");
                GUI.SetImage(ImageKeys.MerchandisingFocus, bitmapDir + "MerchandisingFocus.png");
                GUI.SetImage(ImageKeys.MerchandisingDisabled, bitmapDir + "MerchandisingDisabled.png");
                GUI.SetImage(ImageKeys.MobileTech, bitmapDir + "MobileTech.bmp");
                GUI.SetImage(ImageKeys.Order, bitmapDir + "Order.png");
                GUI.SetImage(ImageKeys.OrderFocus, bitmapDir + "OrderFocus.png");
                GUI.SetImage(ImageKeys.OrderDisabled, bitmapDir + "OrderDisabled.png");
                GUI.SetImage(ImageKeys.PenTarg, bitmapDir + "PenTarg.png");
                GUI.SetImage(ImageKeys.PenTargFocus, bitmapDir + "PenTargFocus.png");
                GUI.SetImage(ImageKeys.PenTargDisabled, bitmapDir + "PenTargDisabled.png");
                GUI.SetImage(ImageKeys.PrintReport, bitmapDir + "PrintReport.png");
                GUI.SetImage(ImageKeys.PrintReportFocus, bitmapDir + "PrintReportFocus.png");
                GUI.SetImage(ImageKeys.PrintReportDisabled, bitmapDir + "PrintReportDisabled.png");
                GUI.SetImage(ImageKeys.ReturnToStock, bitmapDir + "ReturnToStock.png");
                GUI.SetImage(ImageKeys.ReturnToStockFocus, bitmapDir + "ReturnToStockFocus.png");
                GUI.SetImage(ImageKeys.ReturnToStockDisabled, bitmapDir + "ReturnToStockDisabled.png");
                GUI.SetImage(ImageKeys.Review, bitmapDir + "Review.png");
                GUI.SetImage(ImageKeys.ReviewFocus, bitmapDir + "ReviewFocus.png");
                GUI.SetImage(ImageKeys.ReviewDisabled, bitmapDir + "ReviewDisabled.png");
                GUI.SetImage(ImageKeys.ReviewVisits, bitmapDir + "ReviewVisits.png");
                GUI.SetImage(ImageKeys.ReviewVisitsFocus, bitmapDir + "ReviewVisitsFocus.png");
                GUI.SetImage(ImageKeys.ReviewVisitsDisabled, bitmapDir + "ReviewVisitsDisabled.png");
                GUI.SetImage(ImageKeys.Save, bitmapDir + "Save.png");
                GUI.SetImage(ImageKeys.SaveFocus, bitmapDir + "SaveFocus.png");
                GUI.SetImage(ImageKeys.SaveDisabled, bitmapDir + "SaveDisabled.png");
                GUI.SetImage(ImageKeys.Select_Small, bitmapDir + "Select_Small.png");
                GUI.SetImage(ImageKeys.Select_SmallFocus, bitmapDir + "Select_SmallFocus.png");
                GUI.SetImage(ImageKeys.Select_SmallDisabled, bitmapDir + "Select_SmallDisabled.png");
                GUI.SetImage(ImageKeys.Setup, bitmapDir + "Setup.png");
                GUI.SetImage(ImageKeys.SetupFocus, bitmapDir + "SetupFocus.png");
                GUI.SetImage(ImageKeys.SetupDisabled, bitmapDir + "SetupDisabled.png");
                GUI.SetImage(ImageKeys.StartDay, bitmapDir + "StartDay.png");
                GUI.SetImage(ImageKeys.StartDayFocus, bitmapDir + "StartDayFocus.png");
                GUI.SetImage(ImageKeys.StartDayDisabled, bitmapDir + "StartDayDisabled.png");
                GUI.SetImage(ImageKeys.SwitchRoute, bitmapDir + "SwitchRoute.png");
                GUI.SetImage(ImageKeys.SwitchRouteFocus, bitmapDir + "SwitchRouteFocus.png");
                GUI.SetImage(ImageKeys.SwitchRouteDisabled, bitmapDir + "SwitchRouteDisabled.png");
                GUI.SetImage(ImageKeys.SysInfo, bitmapDir + "SysInfo.png");
                GUI.SetImage(ImageKeys.SysInfoFocus, bitmapDir + "SysInfoFocus.png");
                GUI.SetImage(ImageKeys.SysInfoDisabled, bitmapDir + "SysInfoDisabled.png");
                GUI.SetImage(ImageKeys.TCommSetup, bitmapDir + "TCommSetup.png");
                GUI.SetImage(ImageKeys.TCommSetupFocus, bitmapDir + "TCommSetupFocus.png");
                GUI.SetImage(ImageKeys.TCommSetupDisabled, bitmapDir + "TCommSetupDisabled.png");
                GUI.SetImage(ImageKeys.TransmitData, bitmapDir + "TransmitData.png");
                GUI.SetImage(ImageKeys.TransmitDataFocus, bitmapDir + "TransmitDataFocus.png");
                GUI.SetImage(ImageKeys.TransmitDataDisabled, bitmapDir + "TransmitDataDisabled.png");
                GUI.SetImage(ImageKeys.TrkDrvr, bitmapDir + "TrkDrvr.png");
                GUI.SetImage(ImageKeys.TrkDrvrFocus, bitmapDir + "TrkDrvrFocus.png");
                GUI.SetImage(ImageKeys.TrkDrvrDisabled, bitmapDir + "TrkDrvrDisabled.png");
                GUI.SetImage(ImageKeys.Unload, bitmapDir + "Unload.png");
                GUI.SetImage(ImageKeys.UnloadFocus, bitmapDir + "UnloadFocus.png");
                GUI.SetImage(ImageKeys.UnloadDisabled, bitmapDir + "UnloadDisabled.png");
            }
            catch (Exception ex)
            {
                throw new MobileTechException(ex);
            }
        }
        private const String ThreadSlotName = "Configuration";
        public static Configuration Instance
        {
            get
            {
                LocalDataStoreSlot slot = Thread.GetNamedDataSlot(ThreadSlotName);

                if (!(Thread.GetData(slot) is Configuration))
                {
                    Thread.SetData(slot, new Configuration());
                }


                return (Configuration)Thread.GetData(slot);
            }
        }

        public String GetString(ConfigurationKey key)
        {
            return GetString(key.ToString());
        }
        /// <summary>
        /// Session ID
        /// </summary>
        public static int SessionId
        {
            get
            {
                // Load settings
                return _sessionId;
            }
            set
            {
                // Saving settings
                _sessionId = value;
            }
        }
        /// <summary>
        /// Database connection string
        /// </summary>
        public static String ConnectionString
        {
            get
            {
                // Load settings
                return _connectionString;
            }
            set
            {
                // Saving settings
                _connectionString = value;
            }
        }
        public static String MasterConnectionString
        {
            get
            {
                // Load settings
                return _masterConnectionString;
            }
            set
            {
                // Saving settings
                _masterConnectionString = value;
            }
        }        /// <summary>
        /// What is VC ?
        /// </summary>
        public static bool VC
        {
            get
            {
                // Load settings
                return _vc;
            }
            set
            {
                // Saving settings
                _vc = value;
            }
        }

        /// <summary>
        /// What is Sync ?
        /// </summary>
        public static bool Sync
        {
            get
            {
                // Load settings
                return _sync;
            }
            set
            {
                // Saving settings
                _sync = value;
            }
        }
        /// <summary>
        /// What is Sync ?
        /// </summary>
        public static bool TransExportXML
        {
            get
            {
                // Load settings
                return _transExportXML;
            }
            set
            {
                // Saving settings
                _transExportXML = value;
            }
        }
        /// <summary>
        /// What is InitDB ?
        /// </summary>
        public static bool InitDB
        {
            get
            {
                // Load settings
                return _initDB;
            }
            set
            {
                // Saving settings
                _initDB = value;
            }
        }
        /// <summary>
        /// What is VCRestart ?
        /// </summary>
        public static bool VCRestart
        {
            get
            {
                // Load settings
                return _vcRestart;
            }
            set
            {
                // Saving settings
                _vcRestart = value;
            }
        }
        /// <summary>
        /// What is VCUpdate ?
        /// </summary>
        public static bool VCUpdate
        {
            get
            {
                // Load settings
                return _vcUpdate;
            }
            set
            {
                // Saving settings
                _vcUpdate = value;
            }
        }
        /// <summary>
        /// What is AppStart ?
        /// </summary>
        public static bool AppStart
        {
            get
            {
                // Load settings
                return _appStart;
            }
            set
            {
                // Saving settings
                _appStart = value;
            }
        }
        /// <summary>
        /// What is VCUpdate ?
        /// </summary>
        public static string AppNameFullPath
        {
            get
            {
                // Load settings
                return _appNameFullPath;
            }
            set
            {
                // Saving settings
                _appNameFullPath = value;
            }
        }
        /// <summary>
        /// What is DBFullPath ?
        /// </summary>
        public static string DBFullPath
        {
            get
            {
                // Load settings
                return _dbFullPath;
            }
            set
            {
                // Saving settings
                _dbFullPath = value;
            }
        }
        /// <summary>
        /// What is DBFullPath ?
        /// </summary>
        public static string DBLogFullPath
        {
            get
            {
                // Load settings
                return _dbLogFullPath;
            }
            set
            {
                // Saving settings
                _dbLogFullPath = value;
            }
        }
        public static int Route
        {
            get
            {
                // Load settings
                return _route;
            }
            set
            {
                // Saving settings
                _route = value;
            }
        }
        public static int Location
        {
            get
            {
                // Load settings
                return _location;
            }
            set
            {
                // Saving settings
                _location = value;
            }
        }
        #region Temporary

        public String GetString(String name)
        {
            // Configuration
            XmlDocument xmlConfig;

            //MGS Add string reference here
            string appDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName);

            xmlConfig = new XmlDocument();

            xmlConfig.Load(appDir + FILE_PATH);

            //Database Settings
            if (name == "DbLocalFile")
            {
                return xmlConfig["application"]["database"].GetAttribute("DBLocalFile");
            }
            //Replication Settings
            else if (name == "ReplInternetUrl")
            {
                return xmlConfig["application"]["tcomm"]["replication"].GetAttribute("InternetUrl");
            }
            else if (name == "ReplInternetLogin")
            {
                return xmlConfig["application"]["tcomm"]["replication"].GetAttribute("InternetLogin");
            }
            else if (name == "ReplInternetPassword")
            {
                return xmlConfig["application"]["tcomm"]["replication"].GetAttribute("InternetPassword");
            }
            else if (name == "ReplMasterLogin")
            {
                return xmlConfig["application"]["tcomm"]["replication"].GetAttribute("MasterLogin");
            }
            else if (name == "ReplMasterPassword")
            {
                return xmlConfig["application"]["tcomm"]["replication"].GetAttribute("MasterPassword");
            }
            else if (name == "ReplMasterPublisher")
            {
                return xmlConfig["application"]["tcomm"]["replication"].GetAttribute("MasterPublisher");
            }
            else if (name == "ReplMasterPublisherDb")
            {
                return xmlConfig["application"]["tcomm"]["replication"].GetAttribute("MasterPublisherDb");
            }
            else if (name == "ReplMasterPublication")
            {
                return xmlConfig["application"]["tcomm"]["replication"].GetAttribute("MasterPublication");
            }
            else if (name == "ReplTransLogin")
            {
                return xmlConfig["application"]["tcomm"]["replication"].GetAttribute("TransLogin");
            }
            else if (name == "ReplTransPassword")
            {
                return xmlConfig["application"]["tcomm"]["replication"].GetAttribute("TransPassword");
            }
            else if (name == "ReplTransPublisher")
            {
                return xmlConfig["application"]["tcomm"]["replication"].GetAttribute("TransPublisher");
            }
            else if (name == "ReplTransPublisherDb")
            {
                return xmlConfig["application"]["tcomm"]["replication"].GetAttribute("TransPublisherDb");
            }
            else if (name == "ReplTransPublication")
            {
                return xmlConfig["application"]["tcomm"]["replication"].GetAttribute("TransPublication");
            }
            else if (name == "ReplOtherPublication")
            {
                return xmlConfig["application"]["tcomm"]["replication"].GetAttribute("OtherPublication");
            }
            //Version Control Settings
            else if (name == "VCUrl")
            {
                return xmlConfig["application"]["tcomm"]["versioncontrol"].GetAttribute("VCUrl");
            }
            else if (name == "VCPackageVer")
            {
                return xmlConfig["application"]["tcomm"]["versioncontrol"].GetAttribute("VCPackageVer");
            }
            else if (name == "VCLogin")
            {
                return xmlConfig["application"]["tcomm"]["versioncontrol"].GetAttribute("VCLogin");
            }
            else if (name == "VCPassword")
            {
                return xmlConfig["application"]["tcomm"]["versioncontrol"].GetAttribute("VCPassword");
            }
            else if (name == "HttpUpdateUrl")
            {
                return xmlConfig["application"]["tcomm"]["versioncontrol"].GetAttribute("HttpUpdateUrl");
            }
            else if (name == "HttpUpdateTemp")
            {
                return xmlConfig["application"]["tcomm"]["versioncontrol"].GetAttribute("HttpUpdateTemp");
            }
            else if (name == "HttpUpdateLogin")
            {
                return xmlConfig["application"]["tcomm"]["versioncontrol"].GetAttribute("HttpUpdateLogin");
            }
            else if (name == "HttpUpdatePassword")
            {
                return xmlConfig["application"]["tcomm"]["versioncontrol"].GetAttribute("HttpUpdatePassword");
            }
            else
            {
                return null;
            }
        }

        #endregion

    }
}
