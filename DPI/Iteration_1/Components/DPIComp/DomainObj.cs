using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using DPI.Interfaces;
 
namespace DPI.Components
{
	[Serializable]
	public abstract class DomainObj : IMapObj, IComparable, IDomObj
	{

	#region Data
		[NonSerialized]
		protected SqlGateway sql;
		protected static Random random = new Random();
		protected RowState rowState;
		protected int ver;
		protected int priority;
		/*	
		  	CustAddr				9,000	
			CustInfo				10,000
			Statement				11,000
			Demand					12,000
			DmdItem					13,000
			Deliverable				14,000
			DlvTax					15,000
			DmdTax					16,000
			Wireless_Transactions	16,500
			PayInfo					17,000
			FinancialProdTrans		18,000
			CardApp					19,000
			WebSvcQueue				50,000
			AgentRegistration		20,000
			AgentIncentive			21,000
			
		*/
		
		[NonSerialized]
		protected UOW uow;
	#endregion
       
	#region Properties
		protected SqlGateway Sql
		{
			get 
			{
				if (sql == null)
					sql = loadSql();
				return sql;
			}
		}
		public abstract IDomKey IKey
		{
			get ;
		}
		public int Priority
		{
			get { return priority; }
			set { priority = value; }
		}
		public virtual RowState RowState
		{
			get { return rowState; }
		}
		public int Ver
		{
			get { return ver; }
		}
		public IUOW Uow
		{
			get { return uow;  }
			set 
			{ 
				uow = (UOW)value; 
				uow.Imap.add(this);
			}
		}
	#endregion
       
	#region PropertiesMethods
		public virtual void RefreshForeignKeys()
		{
		}
		public int CompareTo(object o)
		{
			DomainObj obj = (DomainObj)o;
			
			if (this.priority > obj.priority)  // in asc order
				return 1;
			
			if (this.priority < obj.priority)
				return -1;
			
			return 0;
		}
		public virtual void add()
		{
			if (uow != null)
				if (uow.Imap != null)
				{
					uow.Imap.remove(IKey);
					Sql.insert(this);
					uow.Imap.add(this);
					return;
				}

			Sql.insert(this);
		}
		public virtual void save()
		{
			checkExists();
			Sql.update(this);
		}
		public virtual void delete()
		{
			if (rowState == RowState.Remove)
				return;

			rowState = RowState.Remove;

			if (uow == null)
				deleteIt();
		}
		public virtual void deleteIt()
		{
		//	checkExists();
			Sql.delete(this);

			if (uow == null) 
				return;
 
			if (uow.Imap == null)
				return;

			uow.Imap.remove(IKey);
		}
		public virtual void checkExists()
		{
			throw new ApplicationException("CheckExists() must be overriden in a durived class");
		}	
		public virtual void removeFromIMap(IUOW uow)
		{
			if (uow == null)
				return;

			if (uow.Imap == null)
				return;

			if (!uow.Imap.keyExists(this.IKey))
				return;

			uow.Imap.remove(this.IKey);
		}
		protected virtual void setState()
		{
			CheckForDeletion();

			if (rowState != RowState.New)
				rowState = RowState.Dirty;
		}
		protected virtual void CheckForDeletion()
		{
			if (rowState == RowState.Remove)
				throw new ApplicationException("The object has been scheduled for deletion and cannot be changed");

			if (rowState == RowState.Deleted)
				throw new ApplicationException("The object has been deleted");
		}
		protected virtual void newOnly(string attr)
		{
			if (this.rowState != RowState.New)
				throw new ApplicationException(attr + " can't be changed after a row has been inserted");
		}
		protected static DomainObj[] addToIMap(UOW uow, DomainObj[] objs)
		{
			if (uow == null)
				return objs;

			if (uow.Imap == null)
				return objs;

			uow.Imap.syncIM(objs);

			for (int i = 0; i < objs.Length; i++)
				objs[i].uow = uow;
	
			return objs;
		}

		protected static DomainObj addToIMap(UOW uow, DomainObj obj)
		{
			if (uow == null)
				return obj;

			if (uow.Imap == null)
				return obj;

			uow.Imap.add(obj);
			return obj;
		}
		protected abstract SqlGateway loadSql();
	#endregion      
		
	}
}