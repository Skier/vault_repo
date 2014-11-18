using System;
using System.Data;
using System.Data.SqlClient;
using NUnit.Framework;
using DPI.Interfaces;
using DPI.Components;

namespace DPI.ComponentsTests
{
	[TestFixture]
	public class ProdFilterTest
	{
		public static void Main()
		{
			ProdFilterTest t = new ProdFilterTest();
			t.ProvCategoryTest();

		}
		[Test]
		public void ProvCategoryTest()
		{	
			UOW uow = new UOW();
			string sql = 
				@"select p.id,
		pt.prodtype,
		prodname, 
		p.description, 
		null as unitprice, 
		loc, 
		taxcode,
		'F' as isrecurring, 
		null as pricerule, 
		0 as priceruleid, 
		isprovisionable,
		isprovviamapping, 
		SC.isinstallforeachinstance, 
		SC.IsRestrictedToOneInstance,
		SC.FulfillMethod, 
		p.prodsubclass,
		startservmon, 
		endservmon, 
		null as exclusiveSupplier, 
		p.eligibilitycriteria, 
		null as pricetype,
		isAgentVisible,
		webdescription 
	from prodavailability pa, prodtype pt,product p
	 left outer join prodsubclass sc on p.prodsubclass=sc.prodsubclass
	where pa.prod = p.ID 
	 and p.prodtype=pt.prodtype
	 and isbillable='T' 
	 and iscomponentonly='F'
 	 and p.status='Active'
	 and p.startdate < getdate()
	 and (p.enddate is null or p.enddate > getdate())
	 and pa.loc=(select top 1 loc from prodavailability where prod=p.id)";

			SqlConnection cn = Conn.GetConn();
			SqlCommand cmd = cn.CreateCommand();
			cmd.CommandType = CommandType.Text;
			cmd.CommandText = sql;
			SqlDataAdapter da = new SqlDataAdapter(cmd);
			//		DPI.Components.dsProductPrice ds = new DPI.Components.dsProductPrice();
			dsProdPrice ds = new dsProdPrice();
			da.Fill(ds);

			Console.WriteLine(
				ds.Tables[1].Rows[0]["TaxCode"].ToString());

		//	Console.WriteLine(
		//		ds.Tables["viewProdPriceTestDataTable"].Rows[0]["TaxCode"].ToString());


  
			/*		try 
					{
						Assertion.Assert(pi.Length > 0);	
					}
					catch (Exception e)
					{
						Console.WriteLine(e.Message);
					}
					finally
					{
						uow.close();
					}
					}
			*/
		}
	}
}