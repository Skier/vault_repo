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
	public class spOrder_GetProductsForAccountWrapper
	{
		public static IProdPrice[] GetProducts(UOW uow, int accNumber)
		{
			return new SQL().getProducts(uow, accNumber);
		}
		/*		Implementation		*/
		/*		SQL		*/
		class SQL
		{
			public IProdPrice[] getProducts(UOW uow, int accNumber)
			{
				SqlCommand cmd = getCommand(uow);
				cmd.CommandText = "spOrder_GetProductsForAccount";
				cmd.Parameters.Add("@AcctNum", SqlDbType.Int, 0).Value = accNumber;
				return execReader(cmd);
			}
			/*        Implementation        */
			SqlCommand getCommand(UOW uow)
			{
				SqlCommand cmd = uow.Cn.CreateCommand();
				cmd.Transaction = uow.Tran; 
				cmd.CommandType = CommandType.StoredProcedure;
				return cmd;
			}		
			IProdPrice[] execReader(SqlCommand cmd)
			{
				ArrayList ar = new ArrayList();
				SqlDataReader rdr = cmd.ExecuteReader();

				try
				{
					while(rdr.Read())
						ar.Add(reader(rdr));

					IProdPrice[] prods = new IProdPrice[ar.Count];
					ar.CopyTo(prods);
					return prods;
				}
				catch (Exception e)
				{
					Console.WriteLine(e.Message);

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
			IProdPrice reader(SqlDataReader rdr)
			{
				ProdPrice prod = new ProdPrice();
                
				if (rdr["ProductId"] != DBNull.Value)
					prod.ProdId = (int) rdr["ProductId"];
 
				if (rdr["PriceRule"] != DBNull.Value)
					prod.priceRule = (string) rdr["PriceRule"];
 
				if (rdr["UnitPrice"] != DBNull.Value)
					prod.unitPrice = (decimal)rdr["UnitPrice"];
 
				return prod;
			}
		}
	}
}