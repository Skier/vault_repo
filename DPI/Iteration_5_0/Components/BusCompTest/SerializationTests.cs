using System;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using NUnit.Framework;
using DPI.Interfaces;
using DPI.Components;

namespace DPI.ComponentsTests
{
	[TestFixture]
	public class SerializationTest
	{
		int	prdCnt; 
		public static void Main()
		{
			SerializationTest t = new SerializationTest();
			t.serProdInfo();
			t.deserProdInfo();
			t.serProdComp();
			t.deserProdComp();
			Console.Read();
		}
		[Test]
		public void serProdInfo()
		{
			ProdInfo[] prods = ProdInfoCol.GetProd();
			prdCnt = prods.Length;
			IFormatter fmt = new BinaryFormatter();
			Stream stream = new FileStream("ProdInfo.dat", FileMode.Create, FileAccess.Write, FileShare.None);
			fmt.Serialize(stream, prods);
			stream.Close();
		}
		[Test]
		public void deserProdInfo()
		{
			IFormatter fmt = new BinaryFormatter();
			Stream stream = new FileStream("ProdInfo.dat", FileMode.Open, FileAccess.Read, FileShare.Read);
			ProdInfo[] prods = (ProdInfo[])fmt.Deserialize(stream);
			Console.WriteLine(stream.Length);
			stream.Close();	//		Assertion.Assert(prods.Length == prdCnt);

		}
		[Test]
		public void serProdComp()
		{
			ProdComposition[] comps = ProdInfoCol.Comps;
			prdCnt = comps.Length;
			IFormatter fmt = new BinaryFormatter();
			Stream stream = new FileStream("ProdComps.dat", FileMode.Create, FileAccess.Write, FileShare.None);
			fmt.Serialize(stream, comps);
			stream.Close();
		}
		[Test]
		public void deserProdComp()
		{
			IFormatter fmt = new BinaryFormatter();
			Stream stream = new FileStream("ProdComps.dat", FileMode.Open, FileAccess.Read, FileShare.Read);
			ProdComposition[] comps = (ProdComposition[])fmt.Deserialize(stream);
			Console.WriteLine(stream.Length);
			stream.Close();	
			Assertion.Assert(comps.Length == prdCnt);

		}

		[Test]
		public void serDataObject()
		{
			UOW uow = new UOW();
			uow.Service = "Serialize Data Object";
			Product[] prods = Product.getAll(uow);
			uow.close();

			IFormatter fmt = new BinaryFormatter();
			Stream stream = new FileStream("SerDataObj.dat", FileMode.Create, FileAccess.Write, FileShare.None);
			fmt.Serialize(stream, prods);
			stream.Close();
		}
		[Test]
		public void serIMap()
		{
			IMap imap = new IdentityMap();
			UOW uow = new UOW(imap);

			uow.Service = "Serialize IMap";
			Product[] prods = Product.getAll(uow);
			uow.close();

			IFormatter fmt = new BinaryFormatter();
			Stream stream = new FileStream("SerIMap.dat", FileMode.Create, FileAccess.Write, FileShare.None);
			fmt.Serialize(stream, imap);
			stream.Close();
		}
		[Test]
		public void serDataset()
		{
			UOW uow = new UOW();
			uow.Service = "Serialize DataSet";
			SqlConnection cn = Conn.GetConn();
			SqlCommand cmd = cn.CreateCommand();
			cmd.CommandType = CommandType.Text;
			cmd.CommandText = "Select * from dbo.Product";
			
			SqlDataAdapter da = new SqlDataAdapter(cmd);

			dsProduct ds = new dsProduct();
			da.Fill(ds);

			IFormatter fmt = new BinaryFormatter();
			Stream stream = new FileStream("SerDataset.dat", FileMode.Create, FileAccess.Write, FileShare.None);
			fmt.Serialize(stream, ds);
			stream.Close();
			uow.close();
		}
	}
}