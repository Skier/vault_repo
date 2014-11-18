using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using DPI.Interfaces;

namespace DPI.Components
{
//	[Serializable]
	public abstract class SqlGateway
	{
		protected virtual SqlCommand getCommand(DomainObj rec)
		{	
			return makeCommand(rec.Uow);
		}
		protected virtual SqlCommand makeCommand(IUOW uow)
		{
			SqlCommand cmd = uow.Cn.CreateCommand();
			cmd.Transaction = uow.Tran; 
			cmd.CommandType = CommandType.StoredProcedure;
			return cmd;
		}
		protected virtual SqlCommand makeCommand()
		{
			SqlCommand cmd = Conn.GetConn().CreateCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			return cmd;
		}
		public abstract void insert(DomainObj rec);	
		public abstract void delete(DomainObj rec);
		public abstract void update(DomainObj rec);
		protected virtual DomainObj[] execReader(SqlCommand cmd)
		{
			ArrayList ar = new ArrayList();
			SqlDataReader rdr = null;

			try
			{
				rdr = cmd.ExecuteReader();
				while(rdr.Read())
					ar.Add(reader(rdr));

				DomainObj[] doms = new DomainObj[ar.Count];
				ar.CopyTo(doms);
				return doms;
			}
			catch (Exception e)
			{

				if (cmd.Transaction != null)
					if (cmd.Transaction.Connection != null)
						cmd.Transaction.Rollback();
				
				throw e;
			}
			finally
			{
				if (!rdr.IsClosed)
					rdr.Close();
			}
		}
		protected void   execScalar(SqlCommand cmd)
		{
			try
			{
				cmd.ExecuteScalar();
			}
			catch (SqlException se)
			{
				if (cmd.Transaction != null)
					if (cmd.Transaction.Connection != null)
						cmd.Transaction.Rollback();
				
				if (se.Number == Const.CE)
					throw new ConcurrencyException();

				throw se;
			}
			catch (Exception e)
			{
				if (cmd.Transaction != null)
					if (cmd.Transaction.Connection != null)
						cmd.Transaction.Rollback();
				
				throw e;                
			}
		}
		protected int execScalarInt(SqlCommand cmd)
		{
			try
			{
				return (int)cmd.ExecuteScalar();
			}
			catch (SqlException se)
			{
				if (cmd.Transaction != null)
					if (cmd.Transaction.Connection != null)
						cmd.Transaction.Rollback();
				
				if (se.Number == Const.CE)
					throw new ConcurrencyException();

				throw se;
			}
			catch (Exception e)
			{
				if (cmd.Transaction != null)
					if (cmd.Transaction.Connection != null)
						cmd.Transaction.Rollback();
				
				throw e;                
			}
		}
		protected abstract DomainObj reader(SqlDataReader rdr);
	}
}