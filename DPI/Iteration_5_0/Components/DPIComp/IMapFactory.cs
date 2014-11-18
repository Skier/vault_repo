using System;
using DPI.Components;
using DPI.Interfaces;

namespace DPI.ClientComp
{
	public class IMapFactory
	{
		public static IMap getIMap() 
		{
			return new IdentityMap();
		}
	}
}