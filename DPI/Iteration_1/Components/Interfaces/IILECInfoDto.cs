using System;
 
namespace DPI.Interfaces
{	
	public interface IILECInfoDto
	{
		IILECInfo[] Ilecs  { get; }
		IErrorDto   Errors { get; }
	}
}