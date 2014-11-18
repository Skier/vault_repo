using System;
 
namespace DPI.Interfaces
{	
	/*		Interfaces		*/
	public interface IMapObj: IComparable
	{
		int Priority { get ; }
		IDomKey IKey	{ get; }
		RowState State{ get; }
		void add();
		void save();
		void delete();  // marks for deletion
		void deleteIt(); // actual delete
	}
}