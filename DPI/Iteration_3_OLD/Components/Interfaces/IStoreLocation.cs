using System;

namespace DPI.Interfaces 
{
	public interface IStoreLocation
	{
		string StoreCode			{ get; set; }
		string StoreClass			{ get; set; }
		string Name					{ get; set; }
		string StoreNumber			{ get; set; }
		string Address				{ get; set; }
		string City					{ get; set; }
		string St					{ get; set; }
		string Zip					{ get; set; }
		string Phone				{ get; set; }
		string Fax					{ get; set; }
		string Manager				{ get; set; }
		bool Active					{ get; set; }
		DateTime ActiveDate			{ get; set; }
		string PriceCode			{ get; set; }
		string Wireless_PriceCode	{ get; set; }
		string Notes				{ get; set; }
		string AddLocInf			{ get; set; }
		DateTime TermDate			{ get; set; }
		string Status				{ get; set; }
		string Ilec					{ get; set; }
		int DMA						{ get; set; }
		int CorpID					{ get; set; }
		string Type					{ get; set; }
		int Internet_Channel_ID		{ get; set; }
		bool LocalService			{ get; set; }
		bool Wireless				{ get; set; }
		bool Internet				{ get; set; }
		bool SmartConnect			{ get; set; }
		decimal NET_FlatRate		{ get; set; }
		decimal SC_FlatRate			{ get; set; }
		decimal LS_FlatRate			{ get; set; }
		string Divisional_Manager	{ get; set; }
		int OverrideDebCardProd		{ get;		}
		bool DebitCard				{ get;		}
		string RestProdRule			{ get;		}
		bool IsNarrowPrinter		{ get;		}
		bool Satellite				{ get;		}
		bool ShowSource				{ get;		}
		bool DpiWireless			{ get;		}
	}
}