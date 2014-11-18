using System;
 
namespace DPI.Interfaces
{	
	public interface IChargeDto
	{
		int DlvId	 { get; set; }
		decimal Amt  { get; set; }
	}
}