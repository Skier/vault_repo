using System;
 
namespace DPI.Interfaces
{
	public interface ISummable
	{
		string SumType { get; }
		decimal Amount { get; set; }
	}
}