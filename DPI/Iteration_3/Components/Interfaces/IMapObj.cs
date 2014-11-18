using System;
 
namespace DPI.Interfaces
{
	public interface IMapObj : IComparable
	{
		int Priority    { get; }
		IDomKey IKey	{ get; }
		RowState RowState  { get; }

		void add();
		void save();
		void delete();  // marks for deletion
		void deleteIt(); // actual delete
		IUOW Uow	{ get; set; }
		void removeFromIMap(IUOW uow);
	}
}