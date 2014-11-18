using System;
 
namespace DPI.Interfaces
{
	public interface INamePrice
	{
		string Name { get; set; }
		decimal Price { get; set; }
	}
}