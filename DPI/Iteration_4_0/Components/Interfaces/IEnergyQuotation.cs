using System;


namespace DPI.Interfaces
{
	public interface IEnergyQuotation
	{
		int ID					{ get; }
		decimal PrepayAmt		{ get; set; }
		int EstimatedUsage		{ get; set; }
		decimal RatePerKwh		{ get; set; }
	}
}