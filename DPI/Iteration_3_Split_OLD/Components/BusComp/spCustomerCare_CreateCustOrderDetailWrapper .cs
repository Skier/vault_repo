using System;
using System.Collections;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using DPI.Interfaces;
 
namespace DPI.Components
{
	public class spCustomerCare_CreateCustOrderDetailWrapper
	{
		/*        Data        */
		int orderId;
		int dpiProdId;
		int qty;
		int id;
		UOW uow;
        
		/*        Properties        */
		public int Id {	get { return id; }}
		public int OrderId
		{
			get { return orderId;  }
			set	{ orderId = value; }
		}
		public int DpiProdId
		{
			get { return dpiProdId;  }
			set	{ dpiProdId = value; }
		}
		public int Qty
		{
			get { return qty;  }
			set	{ qty = value; }
		}
        
		/*        Constructors			*/
		public spCustomerCare_CreateCustOrderDetailWrapper(UOW uow)
		{
			if(uow == null)
				throw new ArgumentException("Unit Of Work is required", "spCustomerCare_CreateCustOrderDetailWrapper");
            
			this.uow = uow;
		}
		/*        Methods        */
		public int CreateOrderDetail()
		{
			Validate();
			id =  (new SQL()).insert(this);
			return id;
		}
		/*		Implementation		*/
		void Validate()
		{
			if ((orderId == 0) || (dpiProdId == 0) || ( qty == 0))
				throw new ArgumentException("Order id, product id, and quantity are required", "spCustomerCare_CreateCustOrderDetailWrapper");
		}
		/*		SQL		*/
		class SQL
		{
			public int insert(spCustomerCare_CreateCustOrderDetailWrapper rec)
			{
				SqlCommand cmd = getCommand(rec.uow);
				cmd.CommandText = "spCustomerCare_CreateCustOrderDetail";
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
			void setParam(SqlCommand cmd, spCustomerCare_CreateCustOrderDetailWrapper rec)
			{
				cmd.Parameters.Add("@OrderId", SqlDbType.Int, 0).Value = rec.orderId;
				cmd.Parameters.Add("@DpiProdId", SqlDbType.Int, 0).Value = rec.dpiProdId;
				cmd.Parameters.Add("@Qty", SqlDbType.Int, 0).Value = rec.qty;
			}
		}
	}
}