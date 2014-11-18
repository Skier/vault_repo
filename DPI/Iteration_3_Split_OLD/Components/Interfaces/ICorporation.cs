using System;
 
namespace DPI.Interfaces
{
	public interface ICorporation
	{
		string   Name					 { get; }
		string   Address				 { get; }
		string   City					 { get; }
		string   St						 { get; }
		string   Zip					 { get; }
		string   Fax					 { get; }
		string   Contact				 { get; }
		string   Phone					 { get; }
		int      CorpID					 { get; }
		DateTime Date_Created			 { get; }
		bool     RAC_WF					 { get; }
		bool	 RequestClerkId			 { get; }
		int		 ParentId				 { get; set; }                
		bool	 SkipStoreStats			 { get; set; }
		bool	 UsePapentForStoreStats	 { get; set; }
		string   WebOrderingTrainingPage { get; }
		int		 DefaultDebCardProd		 { get; }
		bool	 IsPymtPostReq			 { get; set; }
		string   PymtTypeRule			 { get; }
		bool	 RemLeadZerosFromStoreNum{ get; }
		bool	 AllowLocalConv			 { get; }
 	}
}