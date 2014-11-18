using System;
 
namespace DPI.Interfaces
{
	public interface ICorp
	{
		int CorpId                 { get; }
		int TotalStores			   { get; }
		IStoreStats[] Stores       { get; }
		// Top stores
		IStoreStats[] Top10Active  { get; }
		IStoreStats[] Top10NewCust { get; }
		IStoreStats[] Top10Revenue { get; }
	}
}