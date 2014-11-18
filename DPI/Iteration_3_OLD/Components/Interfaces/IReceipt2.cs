using System;
using System.Collections;

namespace DPI.Interfaces
{
	public interface IReceipt2
	{
		int            Id         { get; }
		string         Name       { get; }
		string         CSharpName { get; }
		IReceiptItem[] Items      { get; }
		
		IReceiptItem[] GetItems(IUOW uow);
		IReceiptItem[] FilterItems(ReceiptItemType iType);
		IReceiptItem[] GetFirst(ReceiptItemType iType);
		IReceiptItem[] GetNext(IReceiptItem prev);
		string Conv(IReceiptItem[] lines);
		
	}
	public interface IReceiptItem
	{
		int    Id                   { get; }
		int    ReceiptId            { get; }
		string ItemType             { get; }
		string ItemOrder            { get; }
		string Text                 { get; }
		string OverrideFontFamilty  { get; }
		string OverrideFontStyle    { get; }
		int    OverrideFontSize     { get; }
		string ItemGroup            { get; }
		string HAlignment          { get; }
		string VAlignment          { get; }
		int Compare(object first, object second);
	}
}			