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
	[Serializable]
	public class ProdAvailabilityExt : ProdAvailability
	{
		decimal priceAmt;
		string pricePriority;


		public decimal PriceAmt { get { return priceAmt; }}
		public string PricePriority { get { return pricePriority; }	}
		public ProdAvailabilityExt(ProdAvailability pa)
		{
			this.id    = pa.Id;
			this.prod  = pa.Prod;
			this.Loc   = pa.Loc;
			this.descr = pa.Descr;
		}
		/*		SQL		*/
		[Serializable]
		class ProdAvailabilityExtSQL : ProdAvailabilitySQL
		{
			public ProdAvailabilityExt[] GetForZip(UOW uow, int loc, int ilec, int dpi, bool isAgentVisible,string orderType)
			{
				SqlCommand cmd = makeCommand(uow);
				
				cmd.CommandText = "dbo.spOrder_GetProductsAvailable2";
	
				cmd.Parameters.Add("@Ilec", SqlDbType.Int, 0).Value = ilec;
				cmd.Parameters.Add("@Dpi", SqlDbType.Int, 0).Value  = dpi;
				cmd.Parameters.Add("@LocId", SqlDbType.Int, 0).Value  = loc;
				cmd.Parameters.Add("@IsAgentVisible", SqlDbType.VarChar, 1).Value  
					= isAgentVisible ? "T" : "F";
				cmd.Parameters.Add("@OrderType", SqlDbType.VarChar, 20).Value  = orderType;
				
				return convert(execReader(cmd));
			}
			ProdAvailabilityExt[] convert(DomainObj[] objs)
			{
				ProdAvailabilityExt[] acls  = new ProdAvailabilityExt[objs.Length];
				objs.CopyTo(acls, 0);
				return  acls;
			}
			protected override DomainObj reader(SqlDataReader rdr)
			{
				ProdAvailabilityExt pae = 
					new ProdAvailabilityExt( (ProdAvailability)base.reader(rdr));

				if (rdr["UnitPrice"] != DBNull.Value)
					pae.priceAmt = (decimal) rdr["UnitPrice"];

				if (rdr["PricePriority"] != DBNull.Value)
					pae.pricePriority = (string)rdr["PricePriority"];

				return pae;
			}
		}
	}
}

//ProdAvailabilityExt