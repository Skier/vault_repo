using System;
using System.Collections;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using DPI.Components;
using DPI.Interfaces;
 
namespace DPI.Components
{
	public class spCustomerCare_CreateCustOrderWrapper
	{
		/*        Data        */
		int accNumber;
		string note;
		string userid;
		int orderNum;
		UOW uow;
        
		/*        Properties        */
		public int OrderNum
		{
			get { return orderNum;  }
		}

		public int AccNumber
		{
			get { return accNumber;  }
			set { accNumber = value; }
		}
		public string Note
		{
			get { return note;  }
			set { note = value; }
		}
		public string Userid
		{
			get { return userid;  }
			set { userid = value; }
		}
		/*        Constructors			*/
		public  spCustomerCare_CreateCustOrderWrapper(UOW uow)
		{
			if(uow == null)
				throw new ArgumentException("Unit Of Work is required", "Unit Of Work");
            
			this.uow = uow;
		}
		/*        Methods        */
		public int CreateCustOrder()
		{
			Validate();
			orderNum =  (new SQL()).insert(this);
			return orderNum;
		}
  		/*		Implementation		*/
		void Validate()
		{
			if ((accNumber == 0) || (userid == null))
				throw new ArgumentException(
					"Account number and  userid are required", "spCustomerCare_CreateCustOrderWrapper");
			if (userid.Trim().Length == 0)
				throw new ArgumentException(
					"Userid is required", "spCustomerCare_CreateCustOrderWrapper");
		}
		/*		SQL		*/
		class SQL
		{
			public int insert(spCustomerCare_CreateCustOrderWrapper rec)
			{
				SqlCommand cmd = getCommand(rec.uow);
				cmd.CommandText = "spCustomerCare_CreateCustOrder";
				SqlParameter p = cmd.Parameters.Add("@ret", SqlDbType.Int, 0);
				p.Direction = ParameterDirection.ReturnValue;
				setParam(cmd, rec);
				return execScalar(cmd);
			}
			SqlCommand getCommand(UOW uow)
			{
				SqlCommand cmd = uow.Cn.CreateCommand();
				cmd.Transaction = uow.Tran; 
				cmd.CommandType = CommandType.StoredProcedure;
				return cmd;
			}
			int execScalar(SqlCommand cmd)
			{
				try
				{
					cmd.ExecuteScalar();  // does not return value properly?!?
					return (int)cmd.Parameters["@ret"].Value;
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
			/*        Implementation        */
			void setParam(SqlCommand cmd, spCustomerCare_CreateCustOrderWrapper rec)
			{
				
				cmd.Parameters.Add("@AccNumber", SqlDbType.Int, 0).Value = rec.accNumber;
 
				if (rec.note == null)
					cmd.Parameters.Add("@Note", SqlDbType.VarChar, 1).Value = DBNull.Value;
				else
				{
					if (rec.Note.Length == 0)
						cmd.Parameters.Add("@Note", SqlDbType.VarChar, 1).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@Note", SqlDbType.VarChar, 1).Value = rec.note;
				}
 
				if (rec.userid == null)
					cmd.Parameters.Add("@Userid", SqlDbType.VarChar, 20).Value = DBNull.Value;
				else
				{
					if (rec.Userid.Length == 0)
						cmd.Parameters.Add("@Userid", SqlDbType.VarChar, 20).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@Userid", SqlDbType.VarChar, 20).Value = rec.userid;
				}
			}
		}
	}
}