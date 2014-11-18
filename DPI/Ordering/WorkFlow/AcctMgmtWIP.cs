using System;
using DPI.ClientComp;
using DPI.Interfaces;

namespace DPI.Ordering
{
	[Serializable]
	public class AcctMgmtWip : WIP	
	{		
		#region Const

		public const string HEADER_UPDATE_CUSTOMER = "HEADER_UPDATE_CUSTOMER";
		public const string HEADER_UPDATE_PAYMENT_LOG = "HEADER_UPDATE_PAYMENT_LOG";		
		public const string DEFAULT_CONTROL = "UpdateCustomerCtl.ascx";

		// All WIP indexers are defined in lower case
		public const string WIP_CUSTOMER_FILTER = "wip_customer_filter";
		public const string WIP_PAGE_INDEX = "wip_page_index";
		public const string WIP_ACCNUMBER = "wip_accnumber";
		public const string WIP_CURRENT_CONTROL_URL = "wip_current_control_url";

		#endregion

		#region Member Variables

		// header is used to determine the context of the current process i.e. Update Custer or Update Payment Log etc.
		string _header;
		ICustomerFilter _customerFilter;
		int _pageIndex;
		string _accNumber;
		string _currentControlUrl;

		#endregion
		
		#region Properties

		public override IWipStep FirstStep 
		{ 
			get 
			{ 
				if ( _header == null )
					throw new ArgumentException("Header is not defined.");

				return WorkflowFact.AcctMgmtFirstStep();

//				if ( _header == HEADER_UPDATE_CUSTOMER )
//					return WorkflowFact.UpdateCustomerFirstStep();
//
//				throw new ArgumentException("Header not found.");
			}
		}

		#endregion

		#region Constructors
		
		public AcctMgmtWip(IUser user, string header) : base("User Displayname", "User ClerkId", "User StoreCode")
		{			
			this._header = header;
			CurrStep = (WIP.WipStep)FirstStep;
		}

		#endregion

		#region Protected Methods

		protected override object find(string attr)
		{
			if (attr == null)
				return null;

			switch( attr.ToLower() )
			{
				case WIP_CUSTOMER_FILTER : 
					return _customerFilter;

				case WIP_PAGE_INDEX : 
					return _pageIndex;

				case WIP_ACCNUMBER : 
					return _accNumber;

				case WIP_CURRENT_CONTROL_URL :
					return _currentControlUrl;

				default :
					throw new ArgumentException("No such property: '" + attr + "'"); 
			}
		}

		protected override void load(string attr, object obj)
		{
			if (attr == null)
				return;

			switch( attr.ToLower() )
			{
				case WIP_CUSTOMER_FILTER :
				{
					_customerFilter = (ICustomerFilter)obj;
					break;
				}

				case WIP_PAGE_INDEX :
				{
					_pageIndex = (int)obj;
					break;
				}

				case WIP_ACCNUMBER :
				{
					_accNumber = (string)obj;
					break;
				}

				case WIP_CURRENT_CONTROL_URL :
				{
					_currentControlUrl = (string)obj;
					break;
				}


				default :
					throw new ArgumentException("No such property: '" + attr + "'"); 
			}
		}

		#endregion
	}
}