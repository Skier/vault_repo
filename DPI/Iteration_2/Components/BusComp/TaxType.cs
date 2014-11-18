using System;

namespace DPI.Components
{
	public class TaxDescription
	{
		public static string TaxTypeToString(string ttype)
		{
			return  TaxTypeToString(int.Parse(ttype));
		}
		public static string TaxTypeToString(int ttype)
		{
			switch(ttype)
			{
				case 1   : return "Sales Tax";
				case 2   : return "Business and Occupation Tax";
				case 3   : return "Carrier Gross Receipts";
				case 4   : return "District Tax";
				case 5   : return "Excise Tax";
				case 6   : return "Federal Excise Tax";
				case 7   : return "Fed USF A - School";
				case 8   : return "License Tax";
				case 9   : return "P.U.C. Fee";
				case 10  : return "E911 Tax";
				case 11  : return "Service Tax";
				case 12  : return "Special Tax";
				case 13  : return "State Universal Service Fund";
				case 14  : return "Statutory Gross Receipts";
				case 15  : return "Surcharge";
				case 16  : return "Utility Users Tax";
				case 17  : return "Sales Web Hosting";
				case 18  : return "Fed Universal Service Fund";
				case 19  : return "State High Cost Fund";
				case 20  : return "State Deaf and Disabled Fund";
				case 21  : return "CA Teleconnect Fund";
				case 22  : return "Universal Lifeline Telephone Service Charge";
				case 23  : return "Telecommunications Relay Service Surcharge";
				case 24  : return "Telecommunications Infrastructure Maintenance Fee";
				case 25  : return "State Poison Control Fund";
				case 26  : return "Telecommunications Infrastructure Fund";
				case 27  : return "NY MCTD 186c";
				case 28  : return "NY MCTD 184a";
				case 29  : return "Franchise Tax";
				case 30  : return "Utility Users Tax - Business";
				case 31  : return "Fed Telecommunications Relay Service";
				case 32  : return "District Tax (Residential)";
				case 33  : return "Transit Tax";
				case 34  : return "Telecommunications Assistance Service Fund";
				case 35  : return "E911 Tax (Business)";
				case 36  : return "TRS (Business)";
				case 37  : return "Universal Service Fund (Access/Trunk line)";
				case 38  : return "Universal Service Fund (Business Line)";
				case 39  : return "E911 Tax (PBX/Trunk line)";
				case 40  : return "License Tax (Business)";				
				case 42  : return "Sales Tax (Business)";
				case 43  : return "E911 Tax (Residential)";
				case 44  : return "E911 Tax (Wireless)";
				case 45  : return "NY Franchise 184";
				case 46  : return "NY Franchise 184 Usage";
				case 47  : return "NY MCTD 184a Usage";
				case 48  : return "Universal Service Fund (Wireless)";
				case 49  : return "Use Tax";				
				case 50  : return "Sales Tax (Data)";
				case 51  : return "Municipal Right of Way";
				case 52  : return "Municipal Right of Way (Business)";
				case 53  : return "Municipal Right of Way (Private Line)";
				case 54  : return "Utility Users Tax (Wireless)";
				case 55  : return "Fed USF Cellular";
				case 56  : return "Fed USF Paging";
				case 57  : return "Sales Tax (Interstate)";
				case 58  : return "Utility Users Tax PBX Trunk";
				case 59  : return "District Tax Web Hosting";		
				case 60  : return "CA High Cost Fund A";
				case 61  : return "Telecommunications Education Access Fund";
				case 62  : return "Fed TRS Cellular";
				case 63  : return "Fed TRS Paging";
				case 64  : return "Communications Services Tax";
				case 65  : return "Value Added Tax (VAT)";
				case 66  : return "Goods and Services Tax (GST)";
				case 67  : return "Harmonized Sales Tax (HST)";
				case 68  : return "Provincial Sales Tax (PST)";
				case 69  : return "Quebec Sales Tax (QST)";
				case 70  : return "National Contribution Regime (NCR)";
				case 71  : return "Utility Users Tax (Cable Television)";
				case 72  : return "FCC Regulatory Fee (Cable Television)";
				case 73  : return "Franchise Tax (Cable)";
				case 74  : return "Universal Service Fund";
				case 75  : return "Statutory Gross Receipts (Wireless)";
				case 137 : return "Service Provider Tax";
				case 138 : return "Telecommunications Sales Tax";

				case 300  : return "FCC fee";
		
				default  : return "Unknown tax type: " + ttype.ToString(); 
				
		//		default  : throw new ArgumentException("Unknown tax type: " + ttype.ToString()); 
			}
		}

				/*
82  : return "Franchise Tax (Wireless)
83  : return "Reserved
84  : return "Public Education and Government (PEG) Access Fee
85  : return "Communications Service Tax (Satellite)
86  : return "Franchise Tax (Satellite) 
87  : return "Reserved
88  : return "Reserved
89  : return "TRS (Centrex)
90  : return "Utility Users Tax (Cable Television - Business)
91  : return "Utility Users Tax (Centrex)
92  : return "E911 (Centrex)
93  : return "Utility Users Tax (Line)
94  : return "Crime Control District Tax
95  : return "Library District Tax
96  : return "Hospital District Tax
97  : return "Health Services District Tax
98  : return "Emergency Services District Tax
99  : return "Improvement District Tax
100 : return "Development District Tax
101 : return "Transit Web Hosting Tax
102 : return "Ambulance District Tax
103 : return "Fire District Tax
104 : return "Police District Tax
105 : return "Football District Tax
106 : return "Baseball District Tax
107 : return "Crime Control District Web Hosting Tax
108 : return "Library District Web Hosting Tax
109 : return "Hospital District Web Hosting Tax
110 : return "Health Services District Web Hosting Tax
111 : return "Emergency Services District Web Hosting Tax
112 : return "Improvement District Web Hosting Tax
113 : return "Development District Web Hosting Tax
114 : return "Utility Users Tax (Interstate)
115 : return "Utility Users Tax (Telegraph)		 
		
		*/
	}
}