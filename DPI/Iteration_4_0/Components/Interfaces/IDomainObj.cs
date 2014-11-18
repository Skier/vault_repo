using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
  
namespace DPI.Interfaces
{
	public interface IDomainObj : IMapObj, IComparable
	{
//		IDomKey IKey { get ;}
//		int Priority { get; set; }
//		RowState State { get ;}
		int Ver { get ;}
//		IUOW Uow { get; set; }
//		int CompareTo(object o);
//		void add();
//		void save();
//		void delete();
//		void deleteIt();
		void checkExists();
	}
}