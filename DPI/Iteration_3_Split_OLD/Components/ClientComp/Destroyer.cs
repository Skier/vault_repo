using System;
using System.Text;
using System.Collections;

using DPI.Interfaces;

namespace DPI.ClientComp
{
	public class Destroyer
	{
		public static void Destroy(object  busObj)
		{
			if (busObj is IDomObj)
				((IDomObj)busObj).delete();
		}
	}
}