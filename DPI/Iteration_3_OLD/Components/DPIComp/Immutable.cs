using System;
using DPI.Interfaces;
using DPI.Components;
 
namespace DPI.Components
{
    [Serializable]  
	public abstract class Immutable : IMapObj, IComparable
	{
		[NonSerialized] 
		protected UOW uow; 	
	
	#region Properties
		public int Priority { get {return 0;} }
		public abstract IDomKey  IKey {	get ;}
		public RowState RowState	{ get { return RowState.Clean; }}
		protected string Sql {	get {return null; }	}
		public IUOW Uow 
		{
			get { return uow; }
			set { uow = (UOW)value; }
		}
	#endregion

	#region Methods
		public    void       add() {}
		public    void       save()	{}
		public    void       delete() {}
		public    void       deleteIt() {}
		public    void       checkExists() {}	
		protected SqlGateway loadSql() { return null; }
		public int CompareTo(object obj){return 0; }
		public void removeFromIMap(IUOW uow) {}
	#endregion

	}
}