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
	public class spCustomerCare_CreateCustomerProductWrapper
	{
		/*        Data        */
		int accNumber;
		int provProdId;
		int orderDetailId;
		string priceRule;
		string priceRuleRev;
		DateTime priceRuleRevExpDate;
		bool isOrderedProduct;
		string billingStatus;
		int supplier;
		int verTransId;
		string provStatus;
		bool isFee;
		string orderType;
		int retval;
		UOW uow;
        
		/*        Properties        */
		public int Id {	get { return retval; }}
		
		public int AccNumber
		{
			get { return accNumber;  }
			set	{ accNumber = value; }
		}

		public int ProvProdId
		{
			get { return provProdId;  }
			set	{ provProdId = value; }
		}
		public int OrderDetailId
		{
			get { return orderDetailId;  }
			set	{ orderDetailId = value; }
		}
		public string PriceRule
		{
			get { return priceRule;  }
			set	{ priceRule = value; }
		}
		public string PriceRuleRev
		{
			get { return priceRuleRev;  }
			set	{ priceRuleRev = value; }
		}
		public DateTime PriceRuleRevExpDate
		{
			get { return priceRuleRevExpDate;  }
			set	{ priceRuleRevExpDate = value; }
		}

		public bool IsOrderedProduct
		{
			get { return isOrderedProduct;  }
			set	{ isOrderedProduct = value; }
		}

		public string BillingStatus
		{
			get { return billingStatus;  }
			set	{ billingStatus = value; }
		}

		public int Supplier
		{
			get { return supplier;  }
			set	{ supplier = value; }
		}

		public int VerTransId
		{
			get { return verTransId;  }
			set	{ verTransId = value; }
		}

		public string ProvStatus
		{
			get { return provStatus;  }
			set	{ provStatus = value; }
		}

		public bool IsFee
		{
			get { return isFee;  }
			set	{ isFee = value; }
		}

		public OrderType OrderType
		{
			get { return (OrderType)Enum.Parse(typeof(OrderType), orderType);  }
			set	{ orderType = value.ToString(); }
		}


		
		
		/*        Constructors			*/
		public spCustomerCare_CreateCustomerProductWrapper(UOW uow)
		{
			if(uow == null)
				throw new ArgumentException("Unit Of Work is required", "spCustomerCare_CreateCustomerProductWrapper");
            
			this.uow = uow;
		}
		/*        Methods        */
		public int CreateCustomerProduct(int ilec)
		{
			if ( ProdInfoCol.GetProd(ProvProdId).IsBillable)
			{
				Supplier = 0;
				if (Price.IsSupplierRequired(uow, this.PriceRule))
					Supplier =  ilec;
			}
			Validate(); 
			retval =  (new SQL()).insert(this);
			return retval;
		}
		/*		Implementation		*/
		void Validate()
		{
			if ((this.orderDetailId == 0) || (this.provProdId == 0) || ( this.accNumber == 0))
				throw new ArgumentException("Orderdetail id, product id, and accnumber are required", "spCustomerCare_CreateCustOrderDetailWrapper");
		}
		/*		SQL		*/
		class SQL
		{
			public int insert(spCustomerCare_CreateCustomerProductWrapper rec)
			{
				SqlCommand cmd = getCommand(rec.uow);
				cmd.CommandText = "spCustomerCare_CreateCustomerProduct";
//				SqlParameter p = cmd.Parameters.Add("@ret", SqlDbType.Int, 0);
//				p.Direction = ParameterDirection.ReturnValue;
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
					Object obj = cmd.Parameters["@retval"].Value;
					if (obj != null)
						return (int)obj;
					else
						return 0;
				}
				catch (SqlException se)
				{
					Console.WriteLine(se.Message);
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
			void setParam(SqlCommand cmd, spCustomerCare_CreateCustomerProductWrapper rec)
			{
				cmd.Parameters.Add("@AccNumber", SqlDbType.Int, 0).Value = rec.accNumber;
				cmd.Parameters.Add("@ProvProdId", SqlDbType.Int, 0).Value = rec.provProdId;
				cmd.Parameters.Add("@OrderDetailId", SqlDbType.Int, 0).Value = rec.orderDetailId;

				if (rec.priceRule!=null && rec.priceRule.Length!=0)
					cmd.Parameters.Add("@PriceRule", SqlDbType.VarChar, 15).Value = rec.priceRule;
				else
					cmd.Parameters.Add("@PriceRule", SqlDbType.VarChar, 15).Value = DBNull.Value;

				if (rec.priceRuleRev!=null && rec.priceRuleRev.Length!=0)
					cmd.Parameters.Add("@PriceRuleRev", SqlDbType.VarChar, 15).Value = rec.priceRuleRev;
				else
					cmd.Parameters.Add("@PriceRuleRev", SqlDbType.VarChar, 15).Value = DBNull.Value; //rec.priceRuleRevExpDate;

				if (rec.priceRuleRevExpDate != DateTime.MinValue)
					cmd.Parameters.Add("@PriceRuleRevExpDate", SqlDbType.DateTime, 0).Value =  rec.priceRuleRevExpDate;
				else
					cmd.Parameters.Add("@PriceRuleRevExpDate", SqlDbType.DateTime, 0).Value = DBNull.Value; //rec.priceRuleRevExpDate;
	
				if(rec.isOrderedProduct)
                    cmd.Parameters.Add("@isOrderedProduct", SqlDbType.Char, 1).Value = "T";
				else
					cmd.Parameters.Add("@isOrderedProduct", SqlDbType.Char, 1).Value = "F";

				cmd.Parameters.Add("@billingStatus", SqlDbType.VarChar, 10).Value = rec.billingStatus;
			
				if (rec.supplier!=0)
					cmd.Parameters.Add("@supplier", SqlDbType.Int, 0).Value = rec.supplier;
				else
					cmd.Parameters.Add("@supplier", SqlDbType.Int, 0).Value = DBNull.Value; //rec.priceRuleRevExpDate;
				
				cmd.Parameters.Add("@verTransId", SqlDbType.Int, 0).Value = rec.verTransId;
				cmd.Parameters.Add("@provStatus", SqlDbType.VarChar, 15).Value = rec.provStatus;
				
				if (rec.isFee)
					cmd.Parameters.Add("@isFee", SqlDbType.Char, 1).Value = "T";
				else
					cmd.Parameters.Add("@isFee", SqlDbType.Char, 1).Value = "F";
	
				cmd.Parameters.Add("@orderType", SqlDbType.VarChar, 15).Value = rec.orderType;
			
				SqlParameter p=cmd.Parameters.Add("@retval", SqlDbType.Int, 0);
				p.Value = rec.retval;
				p.Direction = ParameterDirection.Output;

			}
		}

		bool BoolFromTf(char tf)
		{
			return (tf.Equals("T") || tf.Equals("t"));
		}

		char TfFromBool(bool b)
		{
			if (b) 
				return 'T';
			else
				return 'F';
		}
	}
}


