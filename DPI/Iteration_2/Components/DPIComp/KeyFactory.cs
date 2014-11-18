using System;
using DPI.Interfaces;

namespace DPI.Components
{
	public class KeyFactory //IIDomKey
	{
		public static IDomKey getKey(string part1, string part2) 
		{
			return new Key(part1, part2);
		}
	}
}