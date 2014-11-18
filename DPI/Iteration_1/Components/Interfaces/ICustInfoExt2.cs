using System;
using DPI.Interfaces;

namespace DPI.Interfaces
{
	/// <summary>
	/// Summary description for ICustInfoExt2.
	/// </summary>
	public interface ICustInfoExt2 : ICustInfoExt
	{
		DateTime ActivDate	{ get; set; }
	}
}
