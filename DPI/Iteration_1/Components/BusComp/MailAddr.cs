//using System;
//using System.Text;
//using System.Collections;
//using DPI.Interfaces;
//
//namespace DPI.Components
//{
//	[Serializable]  
//	public class MailAddr: IMailAddr
//	{
//		/*		Data		*/
//		string streetNum;
//		string streetPrefix;
//		string street;
//		string streetType;
//		string streetSuffix;
//		string unit;
//		string city;
//		string zipcode;
//		string state;
//		string unitType;
//
//		/*		Properties		*/
//		public string UnitType
//		{ 
//			get { return unitType; }
//			set { unitType = value; }
//		}
//		public string StreetNum     
//		{
//			get { return streetNum; }
//			set { streetNum = value; }
//		} 
//		public string StreetPrefix  
//		{
//			get { return streetPrefix; }
//			set { streetPrefix = value; }
//		}
//		public string Street     
//		{
//			get { return street; }
//			set { street = value; }
//
//		}
//		public string StreetType   
//		{
//			get { return streetType; }
//			set { streetType = value; }
//		}
//		public string StreetSuffix 
//		{
//			get { return streetSuffix; }
//			set { streetSuffix = value; }
//		}
//		public string Unit         
//		{
//			get { return unit; }
//			set { unit = value; }
//		}
//		public string City       
//		{
//			get { return city; }
//			set { city = value; }
//		}
//		public string Zipcode     
//		{
//			get { return zipcode; }
//			set { zipcode = value; }
//		}
//		public string State     
//		{
//			get { return state; }
//			set { state = value; }
//		}
//		public string FormattedStreetAddress
//		{
//			get
//			{
//				string sNum = this.streetNum.Trim();
//				string sPrefix = " " + this.streetPrefix.TrimStart();
//				string s = " " + this.street.TrimStart();
//				string sSuffix = " " + this.streetSuffix.TrimStart();
//				string sType = " " + this.streetType.TrimStart();
//				string u = " " + this.unit.TrimStart();
//				string address = sNum + 
//					sPrefix.TrimEnd() + 
//					s.TrimEnd() + 
//					sSuffix.TrimEnd() + 
//					sType.TrimEnd() + 
//					u.TrimEnd();
//				return address.TrimStart();
//			}
//		}
//		public string FormattedCityStateZip
//		{
//			get
//			{
//				string c = this.city.Trim();
//				string s = " " + this.state.TrimStart();
//				string z = "  " + this.zipcode.TrimStart();
//				string cityStateZip = ((c.Length > 0) ? (c + ", ") : c) +
//					s.TrimEnd() + 
//					z.TrimEnd();
//				return cityStateZip.TrimStart();
//			}
//		}
//	}
//}