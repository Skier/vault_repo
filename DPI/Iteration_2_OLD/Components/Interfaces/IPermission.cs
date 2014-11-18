using System;

namespace DPI.Interfaces
{
	public interface IPermission 
	{
		string PermsName { get; }
		string Description { get; }
	}
}