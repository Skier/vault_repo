using System;

namespace DPI.Interfaces
{
	public interface ICardApp : IDomObj
	{
		DateTime AppDate   { get; set; }
		string	 IdType	   { get; set; }
		string	 IdNumber  { get; set; }
		DateTime IdExpDate { get; set; }
		string	 IdState   { get; set; }
		bool	 Approved  { get; set; }
		int		 Dmd	   { get; set; }
		string   CardNum   { get; set; } // Max lenght 19
		string   ExpMonth  { get; set; } // MM
		string   ExpYear   { get; set; } // YY
		string   PrevCard  { get; set; } // The first 4 chars of an existing, non-Purpose card
		string	 Status	   { get; set; }
		bool     Verified  { get; set; }
	}
}