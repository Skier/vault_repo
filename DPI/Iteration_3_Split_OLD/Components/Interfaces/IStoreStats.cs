using System;

namespace DPI.Interfaces
{
	public interface IStoreStats
	{
		string  StoreCode       { get; }
		int     CorpId          { get; set; }
		string  StoreNumber     { get; set; }
		int     ActiveCust      { get; set; }
        int     ActiveCustRank  { get; set; }		
		int     MDT_NewCust     { get; set; }
		int     MDT_NewCustRank { get; set; }
		decimal Revenue         { get; }
		int     RevenueRank     { get; set; }
		decimal LdRevenue       { get; set; }
		decimal WirelessRevenue { get; set; }
		decimal LocalRevenue	{ get; set; }
		int		ActualCorpId	{ get; set; }
	}
}