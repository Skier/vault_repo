using System;
using System.Collections;

using DPI.Interfaces;

namespace DPI.Components
{
	public class DropDownListItem :	IDropDownListItem	
	{	
		string dDLText;
		string dDLValue;

		public string DDLText 
		{ 
			get { return dDLText;  }
			set { dDLText = value; }
		}
		public string DDLValue 
		{ 
			get { return dDLValue;  }
			set { dDLValue = value; }
		}
		public DropDownListItem(){}
		public DropDownListItem(string dDLText, string dDLValue)
		{
			this.dDLText = dDLText;
			this.dDLValue = dDLValue;
		}
		public int CompareTo(object itm)
		{
			return dDLText.CompareTo(((DropDownListItem)itm).dDLText);
		}
	}
}