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

	public class spOrder_AssignIlecWrapper
	{
		/*        Data        */
		int accNumber;
		string ilec;
		string userid;
		UOW uow;
        
		/*        Properties        */
		public int AccNumber
		{
			get { return accNumber;  }
			set { accNumber = value; }
		}
		public string Ilec
		{
			get { return ilec;  }
			set { ilec = value; }
		}
		public string Userid
		{
			get { return userid;  }
			set { userid = value; }
		}
		/*        Constructors			*/
		public spOrder_AssignIlecWrapper(UOW uow)
		{
			if(uow == null)
				throw new ArgumentException("Unit Of Work is required", "Unit Of Work");
            
			this.uow = uow;
		}
		/*        Methods        */
		public int AssignIlec()
		{
			Validate();
			return (new SQL()).insert(this);
		}
		/*		Implementation		*/
		void Validate()
		{
			if ((accNumber == 0) || (userid == null))
				throw new ArgumentException(
					"Account number and  userid are required", "spOrder_AssignIlecWrapper");
			if (userid.Trim().Length == 0)
				throw new ArgumentException(
					"Userid is required", "spOrder_AssignIlecWrapper");
		}
		/*		SQL		*/
		class SQL
		{
			public int insert(spOrder_AssignIlecWrapper rec)
			{
				SqlCommand cmd = getCommand(rec.uow);
				cmd.CommandText = "spOrder_AssignIlec";
				//SqlParameter p = cmd.Parameters.Add("@ret", SqlDbType.Int, 0);
				//p.Direction = ParameterDirection.ReturnValue;
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
					return 0;//(int)cmd.Parameters["@ret"].Value;
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
			void setParam(SqlCommand cmd, spOrder_AssignIlecWrapper rec)
			{
				/*
				  [spOrder_AssignIlec]
				  @Assigned_By VarChar(20),
				  @AccNumber_IN VarChar(8),
				  @ILEC_IN char(3)=NULL
				*/				
				if (rec.userid == null)
					cmd.Parameters.Add("@Assigned_By", SqlDbType.VarChar, 20).Value = DBNull.Value;
				else
				{
					if (rec.userid.Length == 0)
						cmd.Parameters.Add("@Assigned_By", SqlDbType.VarChar, 20).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@Assigned_By", SqlDbType.VarChar, 20).Value = rec.userid;

				}

				cmd.Parameters.Add("@AccNumber_IN", SqlDbType.Int, 0).Value = rec.accNumber;
 
				if (rec.ilec == null)
					cmd.Parameters.Add("@ILEC_IN", SqlDbType.Char, 3).Value = DBNull.Value;
				else
				{
					if (rec.ilec.Length == 0)
						cmd.Parameters.Add("@ILEC_IN", SqlDbType.Char, 3).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@ILEC_IN", SqlDbType.Char, 3).Value = rec.ilec;
				}
 
			}
		}
	}
}




