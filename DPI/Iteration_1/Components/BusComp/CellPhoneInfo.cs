using System;
using System.Text;
using System.Collections;

using DPI.Components;
using DPI.Interfaces;
 
namespace DPI.Components
{
	[Serializable]
	public class CellPhoneInfo : ICellPhoneInfo
	{
	#region Data		

		int wirelessProd;
		string pin;
		string zip;
		string newESN;
		string phoneNumber;
		string controlNumber;

	#endregion
	
	#region Properties
		public int WireleesProduct
		{ 
			get { return wirelessProd;} 
			set { wirelessProd = value; }
		}
		public string Pin
		{ 
			get { return pin; } 
			set { pin = value; }
		}
		public string Zip
		{
			get { return zip; } 
			set { zip = value; }
		}
		public string NewESN		
		{
			get { return newESN; } 
			set { newESN = value; }
		}
		public string PhoneNumber
		{ 
			get { return phoneNumber; } 
			set { phoneNumber = value; }
		}
		public string ControlNumber	
		{ 
			get { return controlNumber;  }
			set { controlNumber = value; }
		} 

		public DictionaryEntry[] Entries 
		{ 
			get 
			{ 
				ArrayList ar = new ArrayList();
				
				DictionaryEntry de = new DictionaryEntry();
				
				de.Key = "ControlNumber";
				de.Value = controlNumber;
				ar.Add(de);

				de = new DictionaryEntry();
				de.Key = "PIN";
				de.Value = pin;
				ar.Add(de);

				DictionaryEntry[] entries = new DictionaryEntry[ar.Count];
				ar.CopyTo(entries);

				return entries;
			}	
		}
		public IDomKey IKey { get { return null; } }
		public int Priority { get { return 0; } set {} }
		public RowState RowState	{ get { return RowState.Clean; }}
		public int Ver { get { return 0; }}
		public IUOW Uow { get { return null; } set {} }
	#endregion
	
		#region Constructors
		public CellPhoneInfo() {}
		public CellPhoneInfo(string phoneNumber) 
		{
			this.phoneNumber = phoneNumber;
		}


	#endregion
	#region Properties
		public void RefreshForeignKeys() {}
		public int CompareTo(object o){ return 0; }
		public void add() {}
		public void save() {}
		public void delete(){}
		public void deleteIt(){}
		public void checkExists(){}
		public void removeFromIMap(IUOW uow){}

	#endregion
	}
}