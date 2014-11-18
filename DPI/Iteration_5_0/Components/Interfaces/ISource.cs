using System;

namespace DPI.Interfaces 
{
	public interface ISource
	{
		int Id               { get; }
		string Name          { get; } // concat name & description
		int SortOrder        { get; }
		string Source        { get; }
		string Description   { get; }
		string Status        { get; }
		DateTime StartDate   { get; }
		DateTime EndDate     { get; }
	}
}