using System;

using DPI.Interfaces;

namespace DPI.Components
{
	[Serializable]
	public class NamePrice : INamePrice
	{
		string name;
		decimal price;

		public string Name
		{
			get { return name;  }
			set { name = value; }
		}
		public decimal Price
		{
			get { return price; }
			set { price = value; }
		}
		public NamePrice() {}
		public NamePrice(string name, decimal price)
		{
			this.name = name;
			this.price = price;
		}
	}
}