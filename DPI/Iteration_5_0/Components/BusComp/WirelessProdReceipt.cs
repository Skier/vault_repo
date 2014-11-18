//using System.Xml;
//using System.Text;
//
//using DPI.Components;
//
//namespace DPI.Components
//{
//	public class WirelessProdReceipt
//	{ 
//	#region Data
//		string header;
//		string success;
//		string failure;
//		string footer;
//		
//		string msl;
//		string pin;
//		string msid;
//		string mdn;
//		string controlNumber;
//
//		bool pass;
//	#endregion
//
//	#region Properties
//		public string MSL 
//		{
//			get { return msl; }
//			set { msl = value; }
//		}
//		public string ControlNumber
//		{
//			get { return controlNumber; }
//			set { controlNumber = value; }
//		}
//		public string PIN
//		{
//			get { return pin; }
//			set { pin = value; }
//		}
//		public string MSID
//		{
//			get { return msid; }
//			set { msid = value; }
//		}
//	#endregion
//
//	#region Constructors
//		public WirelessProdReceipt(string rct, bool pass)
//		{
//			header = success = failure = footer = msl = mdn = pin = msid = controlNumber = string.Empty;
//			this.pass = pass;
//			SetElementsAttrs(new XmlTextReader( XMLUtility.ConvertToXml(rct).OuterXml, XmlNodeType.Element, null));
//		}		
//	#endregion
//
//	#region Methods
//		public virtual string GetReceiptText()
//		{
//			StringBuilder sb = new  StringBuilder();
//
//			sb.Append(Header());
//			sb.Append(SuccessFailure());
//			sb.Append(Footer());
//
//			return SetVariables(sb.ToString());
//		}
//		#endregion
//
//	#region Implementation
//
//		protected virtual string SetVariables(string txt)
//		{
//			StringBuilder sb = new StringBuilder();
//
//			sb.Append(txt);
//
//			sb.Replace("_ControlNumber_Text", "\n\nControl Number: " + controlNumber);
//			sb.Replace("_MSL_Text", "\nMSL: " + msl);
//			sb.Replace("_MSID_Text", "\nMSID: " + msid);
//			sb.Replace("_MDN_Text", "\nMDN: " + mdn);
//
//			return sb.ToString();
//		}
//		protected virtual string Header()
//		{
//			return header + "\n";
//		}
//		protected virtual string SuccessFailure()
//		{
//			if (pass)
//				return success + "\n";
//		
//			return failure + "\n";
//		}
//		protected virtual string Footer()
//		{
//			return footer.Replace("\\n", "\n");
//		}
//		protected virtual void SetElementsAttrs(XmlTextReader xReader)
//		{
//			while (xReader.Read())
//				if (xReader.NodeType == XmlNodeType.Element)
//				{
//					while (xReader.MoveToNextAttribute())
//						switch (xReader.Name.Trim().ToLower())
//						{
//							case "header" :
//							{
//								header = xReader.Value;
//								break;
//							}
//							case "success" :
//							{
//								success = xReader.Value;
//								break;
//							}
//							case "failure" :
//							{
//								failure = xReader.Value;
//								break;
//							}
//							case "footer" :
//							{
//								footer = xReader.Value;
//								break;
//							}
//							default :
//							{
//								DPI_Err_Log.AddLogEntry("WirelessProdReceipt", "", "Uknown Xml attribute: " + xReader.Name);
//								break;
//							}
//						}					
//				}
//		#endregion
//		}
//	}
//}