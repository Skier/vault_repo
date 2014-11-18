using System;
 
namespace DPI.Interfaces
{	
	public interface IILECInfo
	{
		int    OrgId     {get; }
		string ILECCode  {get; } 
		string ILECName  {get; }
		bool   IsDefault {get; }
	}
}