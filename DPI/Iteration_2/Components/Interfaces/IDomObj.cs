using System;
 
namespace DPI.Interfaces 
{	
	public interface IDomObj
	{
		IDomKey IKey { get; }
		int Priority { get; set; }
		RowState RowState { get; }
		int Ver { get; }
		IUOW Uow { get; set; }
		void RefreshForeignKeys();
		int CompareTo(object o);
		void add();
		void save();
		void delete();
		void deleteIt();
		void checkExists();
		void removeFromIMap(IUOW uow);
	}
	public interface IId
	{
		int Id { get; }
	}
}