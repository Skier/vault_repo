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
				case 145 : return "Tribal Sales Tax";
				case 156 : return "CA PSPE Surcharge";
				case 161 : return "E911 (VoIP)";
				case 162 : return "FUSF (VoIP)";
				case 165 : return "Universal Service Fund (VoIP)";
				case 166 : return "Communications Service Tax (Cable)";
				case 167 : return "Municipal Right of Way (Cable)";
				case 169 : return "FCC Regulatory Fee (Wireline)";
				case 170 : return "FCC Regulatory Fee (Wireless)";
				case 300  : return "FCC fee";
		
				default  : return "Other taxes " + ttype.ToString() + ":"; 				
			}
		}
	}
}