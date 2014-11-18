using System;
 
namespace DPI.Interfaces
{
	public class Const
	{
		/*		WebSvcQueue		*/
		public const int MaxAttemps = 25;

		/*		Email		*/
		public const string SMTP_SERVER = "mail.dpiteleconnect.com";
		public const string TECH_SUPPORT_EMAIL = "jason.rieger@dpiteleconnect.com";
		public const int    SMTP_PORT = 25;

		/*		Database		*/
		public const string PROD = "PROD";
		public const string TST  = "TST";
		public const string STG  = "STG";
		public const string DEV  = "DEV";

		public const int CE = 50002; //ConcurrencyException

		// Data refresh interval
		public const int REF_INTERVAL = 1440; // 24 hours

		// Data configuration
		public const int DPI = 24; // DPI's id in the Location table 
		public const string RESALE = "Resale";
		public const string UNEP   = "UNE-P";
		public const string LOCAL_SERVICE   = "Local Service";  

		// Verifone_Transaction

		public const string VF_MONTHLY_PAYMENT      = "Monthly Pa";
		
		//Debit card
		public const string ENROLL					= "ENROLL";

		// Price
		public const string PriceActiveStatus       = "Released"; 

		//FinancialProdTrans
		public const string APPROVED				= "Approved";	// status	

		// Product & tax constants
		public const string PRODUCT_ACTIVE = "active";
		public const short FCC_ChargeType = 300;
		public const decimal FCC_ChargeAmt = 6.5m;
		
		// LD Purchases
		public const string LD_FULLFILL_METHOD = "spFulfill_LongDistance";
		public const string LD_PROD_TYPE = "F";
		public const string LD_PROD_ID = "UNLD";

		//User permission constants.
		public const string PERMISSION_NEW_ORDER = "NewOrder";
		public const string PERMISSION_NEW_PAYMENT = "NewPayment";
		public const string PERMISSION_MONTHLY_PAYMENT = "MonthlyPayment";
		public const string PERMISSION_CUSTOMER_INQUIRY = "CustomerInquiry";
		public const string PERMISSION_PRODUCT_LOOKUP = "ProductLookup";
		public const string PERMISSION_CELL_RECHAREGE = "CellRecharge";
		public const string PERMISSION_INTERNET = "Internet";
		public const string PERMISSION_LD_CALLING_CARD = "LDCallingCard";
		public const string PERMISSION_REVERSAL_VOID = "ReversalVoid";
		public const string PERMISSION_REPORTING = "Reporting";
		public const string PERMISSION_OPERATIONS = "Operations";

		/* Product Level 1 width	*/
		public const int DisplWidth = 642;
		public const int PrintWidth = 644;

		public static string wip = "WIP";
		public static string testWip = "test";
		public const string NEWLINE = "\r\n";
		public const string DEL = ",";
		public const string SUBSYSTEM = "WebOrdering";
		public const string WEBSERVICE_SYSTEM = "WebService";
		public const string GENERAL_ERROR = 
			"An application problem has been encountered. An error log has been generated. Please restart from the first page";
		
		public const string COMP_INDENT = @"&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";

		//	EazyTax directory
		public const string EazyTaxDir = @"C:\EZTax\Data\";

		//Virtual Directory Constants
		public const string VIRTUAL_DIR_BILLVIEW = @"../billview/";
		public const string VIRTUAL_DIR_TEMP = @"../temp/";
		public const string TRAINING_REPORT_TEMP = @"temp/";

		public const string SIGN_OUT_PAGE = "SignOut.aspx";
		public const string ORD_Sumry_SUPPRESS = "SUPPRESS"; // Products starting month 2 order summary option: suppress line item 
		public const string ORD_Sumry_ZERO = "ZERO";		 // Products starting month 2 order summary option : display with zero price

		//PaymentInfo Constants		
		public const decimal PAYMENTINFO_MAX_LOCAL_AMOUNT_PAID = 200;
		public const decimal PAYMENTINFO_MAX_LD_AMOUNT         = 100;
		public const decimal PAYMENTINFO_MAX_AMOUNT_TENEDERED  = 300;

		// YONIX
		public const string YONIX_USERID     = @"web-system"; //User displayed in the VB 6.0 Yonix application for the account information logs. 
		public const string YONIX_DEPARTMENT = @"web-system"; //Department displayed in the VB 6.0 Yonix application for the account information logs.

		// Logon 
		public const string ACCT    = "acct";
		public const string PW      = "pw";
		public const string	TEMPKEY = "tempkey";

		//Internet Constants
		public const string AOL_VENDOR_ID = "121";

		//Web Services Constants
		public const string WS_USERNAME				= "webordering";
		public const string WS_PASSWORD				= "weborderinG!";
		public const string WS_ERRORCODE			= "E";
		public const string WS_DUPLICATE_ERR_CODE	= "D";
		public const string WS_ERRORMESSAGE			= "A System problem has been encountered.";
		public const string WS_ACCT_INACTIVE		= "Inactive account";
		public const string WS_SUCCESSCODE			= "A";
		public const string WS_SUCCESSMESSAGE		= "OK";
		
		// Web services actions
		public const string ORDER_PRODUCT			= "order_product"; // PinSvc.OrderProduct 
		public const string VOID_PRODUCT			= "void_product";  // // PinSvc.OrderProduct
		public const string ACTIVATE_PHONE			= "activate_phone";
		public const string CHECK_ACTIVATION		= "check_activation";
		public const string REPLENISH_SERVICE_PLAN	= "replenish_service_plan";
		public const string GET_SERVICE_PLAN		= "get_service_plan";
		public const string IS_ACTIVE				= "is_active";


		// Web services providers
		public const string SLINGSHOT		  = "slingshot";
		public const string DPI_COLDFUSION	  = "dpi_ws_cf";
		public const string PRESOLUTIONS      = "pre";
		public const string INFINITY_MOBILE   = "infinitymobile";
		public const string PHONETEC			  = "phonetec";

		//Slingshot Constatnts
		public const string DPI_SOURCE		  = "dPi";	

		// Infinity Mobile Constants
		public const string CONTROL_NUMBER	= "_ControlNumber_Text";
		public const string MSL				= "_MSL_Text";
		public const string MSID			= "_MSID_Text";
		public const string MDN				= "_MDN_Text";
		public const string PIN				= "_PIN_Text";

		//PreSolutions Constants
		public const string PRE_SOURCE				= "dpi27";
		public const string PRE_VERSION				= "7";
		public const string PRE_PURCHASE_REQ		= "purchase.request";




	}
}