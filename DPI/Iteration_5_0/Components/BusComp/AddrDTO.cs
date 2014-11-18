using System;
using DPI.Interfaces;

namespace DPI.Components
{
	[Serializable]
	public class AddrDTO : IAddr
	{
		#region Member Variables

		string streetNum;
		string streetPrefix;
		string street;
		string streetType;
		string streetSuffix;
		string unit;
		string city;
		string zipcode;
		string state;
		string unitType;
		string cLLI;
		string nPANXX;


		#endregion

		#region Properties

		public string StreetNum
		{
			get { return streetNum; }
			set { streetNum = value; }
		}

		public string StreetPrefix				
		{
			get { return streetPrefix; }
			set { streetPrefix = value; }
		}

		public string Street					
		{
			get { return street; }
			set { street = value; }
		}

		public string StreetType				
		{
			get { return streetType; }
			set { streetType = value; }
		}

		public string StreetSuffix				
		{
			get { return streetSuffix; }
			set { streetSuffix = value; }
		}

		public string Unit						
		{
			get { return unit; }
			set { unit = value; }
		}

		public string City						
		{
			get { return city; }
			set { city = value; }
		}

		public string Zipcode					
		{
			get { return zipcode; }
			set { zipcode = value; }
		}

		public string State
		{
			get { return state; }
			set { state = value; }
		}

		public string UnitType
		{
			get { return unitType; }
			set { unitType = value; }
		}

		public string FormattedStreetAddress
		{
			get { return street; }
		}
		public string FormattedCityStateZip
		{
			get { return city; }
		}	
		public string CLLI
		{
			get { return cLLI; }
			set { cLLI = value; }
		}
		public string NPANXX
		{
			get { return nPANXX; }
			set { nPANXX = value; }
		}
	

		#endregion
	}
}
