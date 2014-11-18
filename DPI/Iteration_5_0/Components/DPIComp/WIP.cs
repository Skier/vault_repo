using System;
using DPI.Interfaces;
 
namespace  DPI.Components
{
	public abstract class Immutable : IMapObj, IComparable
	{
		/*		Properties		*/
		public int Priority { get {return 0;} }
		public    abstract IDomKey  IKey {	get ;}
		public    RowState State	{ get { return RowState.Clean; }}
		protected string Sql {	get {return null; }	}

		/*		Methods		*/
		public    void       add() {}
		public    void       save()	{}
		public    void       delete() {}
		public    void       deleteIt() {}
		public    void       checkExists() {}	
		protected SqlGateway loadSql() { return null; }
		public int CompareTo(object obj){return 0; }
	}
}