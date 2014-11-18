

using System;


namespace DPI.Interfaces
{
	public interface IEnergyItems
	{
		decimal PrepayAmount			{ get; set; }
		IKeyVal[] Items					{ get; set; }
		INamePrice[] PricedItems		{ get; set; }
	}
}