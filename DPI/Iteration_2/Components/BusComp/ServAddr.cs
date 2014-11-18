//using System;
//using System.Text;
//using System.Collections;
//using DPI.Interfaces;
//
//namespace DPI.Components
//{
//	[Serializable]  
//	public class ServAddr: IServAddr
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
//				if (streetNum == null)
//					return "Unknown";
//
//				if (streetNum.Trim().Length == 0)
//					return "Unknown";
//
//				if (street == null)
//					return "Uknown";
//
//				if (street.Trim().Length == 0)
//					return "Unknown";
//
//				StringBuilder sb = new StringBuilder();
//
//				sb.Append(Massage(streetNum, false));
//				sb.Append(Massage(streetPrefix, false));
//			  	sb.Append(Massage(street, false));
//				sb.Append(Massage(streetSuffix, false));
//				sb.Append(Massage(streetType, false));
//				sb.Append(Massage(unit, false));
//
//				return sb.ToString().TrimEnd();				
//			}
//		}
//
//		public string FormattedCityStateZip
//		{
//			get
//			{
//				if (city == null)
//					return "Unknown";
//
//				if (city.Trim().Length == 0)
//					return "Unknown";
//
//				if (state == null)
//					return "Unknown";
//
//				if (state.Trim().Length == 0)
//					return "Unknown";
//
//				StringBuilder sb = new StringBuilder();
//				
//				sb.Append(Massage(city, true));
//				sb.Append(Massage(state, false));
//				sb.Append(Massage(zipcode, false));
//				
//				return sb.ToString().TrimEnd();
//			}
//		}
//		/*		Implementation		*/
//		string Massage(string str, bool comma)
//		{
//			if (str == null)
//				return "";
//			
//			string s = str.TrimStart();
//			if (s.Length == 0)
//				return "";
//
//			if (comma)
//				return s.TrimEnd() + ", ";
//			
//			return s.TrimEnd() + " ";
//		}
//	}
//}