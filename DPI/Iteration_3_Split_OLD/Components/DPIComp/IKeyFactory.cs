using System;
using DPI.Components;
using DPI.Interfaces;

namespace DPI.ClientComp
{
	public class IKeyFactory //IIDomKey
	{
		public static IDomKey getKey(string part1, string part2) 
		{
			return new Key(part1, part2);
		}
	}
}