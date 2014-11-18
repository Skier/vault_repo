using System;

namespace DPI.Interfaces
{
	public interface IPymtMethCtrl
	{
		string CheckName { get; }
		string CheckNum  { get; }
		PaymentType PaymentType { get; }
		bool IsValid { get; }
	}
}