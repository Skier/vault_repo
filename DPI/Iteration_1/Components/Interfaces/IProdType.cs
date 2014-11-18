using System;
namespace DPI.Interfaces
{
	public interface IProdType
	{
		string PrdType { get; }
		string Description { get; }
		bool IsInstallForEachInstance  { get; }
		int OrderDisplaySeq  { get; }
		bool IsFee  { get; }
		bool IsPrompPayDisc  { get; }
		bool IsListed  { get; }
		string AllowsSubcomps  { get; }
		string FulfillMethod  { get; }
	}
}