using System;
using DPI.Interfaces;
 
namespace DPI.Components
{	
	[Serializable]  
	public class AcctInfo : IAcctInfo
	{
		readonly int      accNumber;
		readonly string   phNumber;
		readonly string   firstName;
		readonly string   lastName;

		readonly string   status;
		readonly bool     isActive;  
		readonly decimal  pastDueAmt; 
		readonly decimal  currDueAmt; 
		readonly decimal  custDataBal;
		
		readonly DateTime dueDate;
		readonly DateTime discoDate;

		/*		Properties		*/
		public int		AccNumber	{ get { return accNumber;   }}
		public string	PhNumber	{ get { return phNumber;    }}
		public string	FirstName	{ get { return firstName;   }}
		public string	LastName	{ get { return lastName;    }}
		public string	PhNumFormated
		{ 
			get
			{ 
				if (phNumber == null)
					return null;
				
				if (phNumber.Length < 10)
					return null;

				return  "(" + phNumber.Substring(0, 3) + ") " 
					        + phNumber.Substring(3, 3) 
					  + "-" + phNumber.Substring(6, 4);
			}
		}
		public bool		IsActive	{ get { return isActive;    }}
		public decimal	PastDueAmt  { get { return pastDueAmt;  }}
		public decimal	DueAmt      
		{ 
			get 
			{ 
				if (CurrCharges + PastDueAmt - CustDataBal < 0)
				{
					return 0;
				}
				return (CurrCharges + PastDueAmt - CustDataBal);	
			}
		} 
		public decimal	CustDataBal	{ get { return custDataBal; }}
		public DateTime	DueDate	    { get { return dueDate;     }}
		public DateTime	DiscoDate	{ get { return discoDate;   }}
		public string   Status		{ get { return status;		}}
		public decimal  CurrCharges { get { return currDueAmt;  }}
		public decimal  BalForward  { get { return CustDataBal - PastDueAmt; }}
		/*		Constructors		*/
		public AcctInfo(int accNumber, string phNumber, bool isActive, 
			decimal pastDueAmt,	decimal currDueAmt, decimal custDataBal,
			DateTime dueDate, DateTime discoDate, string status, string firstName, string lastName)	
		{
			this.accNumber   = accNumber;
			this.phNumber    = phNumber;  
			this.isActive    = isActive;  
			this.pastDueAmt  = pastDueAmt; 
			this.currDueAmt  = currDueAmt; 
			this.custDataBal = custDataBal;
			this.dueDate     = dueDate;
			this.discoDate   = discoDate;
			this.status      = status;
			this.firstName	 = firstName;
			this.lastName    = lastName;
		}
	}
}