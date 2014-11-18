using System;

namespace DPI.Interfaces
{
	public interface ISlingShotResp
	{
		string			Message		{ get; }
		string			ErrorDetail	{ get; }
		string			ACode		{ get; }
		int				ManId		{ get; }
		int				Code		{ get; }
		string			TranId		{ get; }
		string			Action		{ get; }
		string			Ver			{ get; }
		IPinProduct[]	PinProducts	{ get; }
	}
}