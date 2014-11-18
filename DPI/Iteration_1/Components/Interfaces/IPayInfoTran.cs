using System;
 
namespace DPI.Interfaces
{	
	public interface IPayInfoTran
	{
		int TranNumber       { get; }
		PayInfoSource Source { get; }
		decimal ComAmount    { get; }
	}
}