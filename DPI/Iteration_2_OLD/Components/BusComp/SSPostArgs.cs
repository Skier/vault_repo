using System;
using System.Collections;
using System.Xml;
using System.Text;

using DPI.Interfaces;
using DPI.Components;

namespace DPI.Components
{	
	public class SSPostArgs : ISSPostArgs
	{
	#region Data

		string	source;
		string  pwd;
		string	reason;
		string	confNum;
		int		tranId;
		string	retailer;
		string	storeId;
		string	upc;
		decimal price;
		string	aCode;
		int	    manId;
		int		areaCode;
		int		prefix;

	#endregion

	#region Properties
		public string	Source		{ get { return source;	 }}
		public string	Pwd			{ get { return pwd;		 }}
		public string	Reason		{ get { return reason;	 }}
		public string	ConfNum		{ get { return confNum;  }}
		public int		TranId		{ get { return tranId;	 }} 
		public string	Retailer	{ get { return retailer; }} 
		public string	StoreId		{ get { return storeId;	 }}
		public string	Upc			{ get { return upc;		 }}
		public decimal	Price		{ get { return price;	 }}
		public string	ACode		{ get { return aCode;	 }}
		public int		ManId		{ get { return manId;	 }}
		public int		AreaCode	{ get { return areaCode; }}
		public int		Prefix		{ get { return prefix;	 }}
	#endregion

	#region Constructors
		public SSPostArgs()
		{
			this.source = Const.DPI_SOURCE;
			this.pwd = System.Configuration.ConfigurationSettings.AppSettings["SlingShotWebSvcPwd"];
		}
		public SSPostArgs(IUser user) : this()
		{			
			this.retailer = GetCorpName(user);
			this.storeId = user.LoginStoreCode;			
		}
		public SSPostArgs(IUser user,  IPayInfo pi, IWireless_Products wp) : this(user)
		{			
			this.tranId = pi.Id;
			this.upc	= wp.UniProdName;
			this.price	= wp.Price;	
		}
		public SSPostArgs(IUser user, int tranId, string confNum, string aCode, 
			int manId, string reason) : this(user)
		{
			this.tranId		= tranId;
			this.confNum	= confNum;
			this.aCode		= aCode;
			this.manId		= manId;
			this.reason		= reason;
		}
		public SSPostArgs(int areaCode, int prefix) : this()
		{			
			this.areaCode = areaCode;
			this.prefix = prefix;
		}

	#endregion
	
	#region Implementation
		string GetCorpName(IUser user)
		{
			return StoreStatsCol.GetCorporation(user.LoginStoreCode).Name;
		}		
	#endregion
	}
}