using System;
 
namespace DPI.Interfaces
{
	public interface IDomKey 
	{
		int GetHashCode();
		bool Equals(object obj);
	}
}