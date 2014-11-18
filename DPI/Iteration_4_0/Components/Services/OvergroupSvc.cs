using System;
using System.Reflection;
using System.Data;
using System.Data.SqlClient;

using DPI.Components;
using DPI.Interfaces;

namespace DPI.Services
{
	public class OvergroupSvc
	{
		private Enum_Carrier_Code carrier;
		private string state;
		private bool preorder;
		private Enum_Product_Type productType;

		public const string secureKey = "dpi29dk39d";
		private GravityProxy gravity = null;
		private Profile_Information profile = null;

		public OvergroupSvc(string anILECCode, string aState, bool isPreorder, string aProductType)
		{
			carrier = H2OToDPIILECMap(anILECCode);
			state = aState;
			preorder = isPreorder;
			productType = (Enum_Product_Type)Enum.Parse(typeof(Enum_Product_Type), aProductType);
			gravity = new GravityProxy();
			profile = null;

			//Retrieve profile.
			profile = gravity.LookupProfile(secureKey, carrier, state, preorder, productType);

			if (profile == null) 
			{
				profile = new Profile_Information();
			}
		}

		public OvergroupSvc(IILECInfo anILEC, string aState, bool isPreorder, string productType)
			: this(anILEC.ILECCode, aState, isPreorder, productType)
		{

		}

		/*
		 * After creating a new object of this class, this
		 * function can be called to determine if the object
		 * was successfully created. What happens is that in
		 * some cases where there is no profile setup for a
		 * certain state, then the function for this class
		 * cannot be used.
		 */

		public bool Valid
		{
			get
			{
				if (gravity != null &&
					profile != null &&
					profile.Carrier_Code != Enum_Carrier_Code.NONE) {
					return true;
				} else {
					return false;
				}
			}
		}

		public IAddr2[] ValidateAddress(IAddr2 anAddress)
		{
			//Prepare request.
			ValidateAddress_Request request = new ValidateAddress_Request();
			request.myAddress = ConvertDPIAddressToH2OAddress(anAddress);
			request.myProfile = profile;
			request.SecureKey = secureKey;

			//Send request.
			ValidateAddress_Response response = null;
			response = gravity.ValidateAddress(request);

			IAddr2[] addressList = (IAddr2[])Array.CreateInstance(typeof(IAddr2), response.AddressList.Length);
			if (response.AddressList.Length > 0) {
				for (int i = 0; i < addressList.Length; i++) {
					addressList[i] = ConvertH2OAddressToDPIAddress(response.AddressList[i]);
				}
			}

			return addressList;
		}

		public IAddr2 LookupAddressByTN(string TN)
		{
			//Prepare request.
			ValidateAddressByTN_Request request = new ValidateAddressByTN_Request();
			request.myProfile = profile;
			request.SecureKey = secureKey;
			request.State = state;
			request.TN = TN;

			//Send request.
			ValidateAddress_Response response = null;
			response = gravity.ValidateAddressByTN(request);

			if (response.Valid) {
				/*
				 * Use and return the most recent address for this
				 * telephone number.
				 */
				return ConvertH2OAddressToDPIAddress(response.AddressList[0]);
			} else {
				throw new Exception("Address Validation Error: " + response.Message);
			}
		}

		public string[] AvailableTNs(IAddr2 anAddress, int quantity)
		{
			AvailableTNs_Request request = new AvailableTNs_Request();
			request.myProfile = profile;
			request.SecureKey = secureKey;
			request.myAddress = ConvertDPIAddressToH2OAddress(anAddress);
			request.Quantity = quantity;
			request.Special_Option = Enum_TN_Special_Option.NONE;

			AvailableTNs_Response response = null;
			response = gravity.AvailableTNs(request);

			if (response.Valid) {
				return response.TN_List;
			} else {
				return new string[]{};
			}
		}

		public DateTime EstimateDueDate(IAddr2 aServiceAddr, string TN, DateTime requestedDueDate, string orderType)
		{
			if (requestedDueDate.Date < DateTime.Now.Date) {
				return DateTime.MinValue;
			}

			EstimateDueDate_Request request = new EstimateDueDate_Request();
			request.myProfile = profile;
			request.SecureKey = secureKey;
			request.myOrder = new Order_Information();
			request.myOrder.Order_Options = new Order_Options_Information();
			request.myOrder.LineList = new Line_Information[] {new Line_Information()};
			request.myOrder.LineList[0].TN = TN;
			request.myOrder.LineList[0].ProductList = new Product_Information[] {new Product_Information()};
			request.myOrder.LineList[0].ProductList[0].Code1 = "1FR";
			request.myOrder.LineList[0].ListingList = new Listing_Information[] {new Listing_Information()};
			request.myOrder.Customer = new Customer_Information();
			request.myOrder.Customer.Address = ConvertDPIAddressToH2OAddress(aServiceAddr);
			request.myOrder.Requested_Due_Date = requestedDueDate;
			request.myOrder.Order_Type = (Enum_Order_Type)Enum.Parse(typeof(Enum_Order_Type), orderType);
			request.myOrder.Product_Type = productType;

			EstimateDueDate_Response response = null;
			response = gravity.EstimateDueDate(request);

			if (response.Valid) {
				/*
				 * Return the estimated due date. We also add an
				 * hour for servers that don't have the Daylight
				 * Savings Time patch installed.
				 */
				return response.Due_Date.AddHours(1).Date;
			} else {
				return DateTime.MinValue;
			}
		}

		public static DataTable GetOrders(int customer_id)
		{
			SqlConnection sc = new System.Data.SqlClient.SqlConnection("Initial Catalog=GravityUI;Persist Security Info=False;Password=v9qusEBr;User ID=dpi;Data Source=dev.overgroup.com");

			SqlCommand cmd = new SqlCommand();
			SqlDataAdapter da = new SqlDataAdapter();
			DataSet ds = new DataSet();
			DataTable dt = new DataTable();

			sc.Open();
                       
			cmd.Connection = sc;
			cmd.CommandType = CommandType.Text;
			cmd.CommandText = "SELECT * FROM vOrderWebCentral WHERE Order_ID IN (SELECT Order_ID FROM tblLegacyOrder WHERE Legacy_ID = '" + customer_id.ToString() + "')";

			da.SelectCommand = cmd;
			da.Fill(ds);
			dt = ds.Tables[0];
                       
			sc.Close();

			return dt;
		} 

		/*
		 * Compares two addresses and returns the number of fields
		 * that defer between address1 and address2.
		 */
		public int CompareAddresses(IAddr2 address1, IAddr2 address2)
		{
			int diffs = 0;
			foreach(PropertyInfo property1 in address1.GetType().GetProperties()) {
				foreach(PropertyInfo property2 in address2.GetType().GetProperties()) {
					try {
						//We ignore certain properties.
						if ("IKey,AddressID,AdrStatus,AddrType".IndexOf(property1.Name) == -1 &&
							property1.Name != "FormattedStreetAddress" &&
							property1.Name != "FormattedCityStateZip" &&
							property1.Name == property2.Name &&
							property1.GetValue(address1, null).ToString().ToLower() != property2.GetValue(address2, null).ToString().ToLower()) 
						{
							diffs++;
						}
					} catch {}
				}
			}

			return diffs;
		}

		private Address_Information ConvertDPIAddressToH2OAddress(IAddr2 dPiAddress)
		{
			Address_Information H2OAddress = new Address_Information();
			H2OAddress.LEC_CLLI = dPiAddress.CLLI;
			H2OAddress.LEC_NPANXX = dPiAddress.NPANXX;
			H2OAddress.Street_Number = dPiAddress.StreetNum;
			H2OAddress.Street_Pre_Direction = dPiAddress.StreetPrefix;
			H2OAddress.Street_Name = dPiAddress.Street;
			H2OAddress.Street_Post_Direction = dPiAddress.StreetSuffix;
			H2OAddress.Street_Name_Suffix = dPiAddress.StreetType;
			H2OAddress.Secondary_Number3 = dPiAddress.Unit;
			H2OAddress.Secondary_Designation3 = dPiAddress.UnitType;
			H2OAddress.City = dPiAddress.City;
			H2OAddress.State = dPiAddress.State;
			H2OAddress.Zip = dPiAddress.Zipcode;

			return H2OAddress;
		}

		private IAddr2 ConvertH2OAddressToDPIAddress(Address_Information H2OAddress)
		{
			IAddr2 dPiAddress = new  CustAddress();
			dPiAddress.CLLI = H2OAddress.LEC_CLLI;
			dPiAddress.NPANXX = H2OAddress.LEC_NPANXX;
			dPiAddress.StreetNum = H2OAddress.Street_Number;
			dPiAddress.StreetPrefix = H2OAddress.Street_Pre_Direction;
			dPiAddress.Street = H2OAddress.Street_Name;
			dPiAddress.StreetSuffix = H2OAddress.Street_Post_Direction;
			dPiAddress.StreetType = H2OAddress.Street_Name_Suffix;
			dPiAddress.Unit = H2OAddress.Secondary_Number3;
			dPiAddress.UnitType = H2OAddress.Secondary_Designation3;
			dPiAddress.City = H2OAddress.City;
			dPiAddress.State = H2OAddress.State;
			dPiAddress.Zipcode = H2OAddress.Zip;

			return dPiAddress;
		}

		private Enum_Carrier_Code H2OToDPIILECMap(string anILECCode)
		{
			switch(anILECCode)
			{
				//Southwestern Bell
				case "SWB":
					return Enum_Carrier_Code.SBC;

				//Bell South
				case "BSO": 
					return Enum_Carrier_Code.BELLSOUTH;

				//Non - Assigned
				case "ZZZ": 
					return Enum_Carrier_Code.NONE;

				//U. S. West
				case "USW": 
					return Enum_Carrier_Code.NONE;

				//Bell Atlantic
				case "BAT": 
					return Enum_Carrier_Code.NONE;

				//GTE Telephone
				case "GTE": 
					return Enum_Carrier_Code.NONE;

				//Sprint
				case "INT": 
					return Enum_Carrier_Code.SPRINT;

				//Pacific Bell
				case "PAC": 
					return Enum_Carrier_Code.NONE;

				//Alltel
				case "ALT": 
					return Enum_Carrier_Code.NONE;
		 		
				//S Net
				case "SNT": 
					return Enum_Carrier_Code.NONE;
				
				//CentryTel
				case "CEN": 
					return Enum_Carrier_Code.NONE;
				
				//Valor
				case "VAL": 
					return Enum_Carrier_Code.NONE;
				
				//Ameritech
				case "AMT": 
					return Enum_Carrier_Code.NONE;
				
				default:
					return Enum_Carrier_Code.NONE;
			}
		}
	}
}
