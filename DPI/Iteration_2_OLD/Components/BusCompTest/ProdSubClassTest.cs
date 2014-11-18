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
	public class ProdSubClassTest
	{
		int prdCnt;
		public static void Main()
		{
			ProdSubClassTest t = new ProdSubClassTest();
			t.GetSubClass();
			t.GetSubClassViaProdPrice();
			t.serlze();
			t.deser();
			t.serDataObject();
			t.serIMap();
			t.serDataset();
			t.Loops();
			t. DirectAccess();
			Console.ReadLine();

		}
		[Test]
		public void GetSubClass()
		{	
			ProdSubClassInfo[] subs = ProdSubClassCol.GetSubClasses(); 
			Assertion.Assert(subs.Length > 0);
		}
		[Test]
		public void GetSubClassViaProdPrice()
		{	
			UOW uow = new UOW();
			uow.Service = "ProdSubClassTest";
			ProdPrice[] pprice = ProdPrice.getAvaProdTest(uow);

			for (int i = 0; i < pprice.Length; i++)
			{
				if (pprice[i].IsInstallForEachInstance) // checks subclass accesibility
				{}
			}
			uow.close();
		}
		[Test]
		public void serlze()
		{
			ProdInfo[] prods = ProdInfoCol.GetProd();
			prdCnt = prods.Length;
			IFormatter fmt = new BinaryFormatter();
			Stream stream = new FileStream("SerTest.dat", FileMode.Create, FileAccess.Write, FileShare.None);
			fmt.Serialize(stream, prods);
			stream.Close();
		}
		[Test]
		public void deser()
		{
			IFormatter fmt = new BinaryFormatter();
			Stream stream = new FileStream("SerTest.dat", FileMode.Open, FileAccess.Read, FileShare.Read);
			ProdInfo[] prods = (ProdInfo[])fmt.Deserialize(stream);
			stream.Close();
			Assertion.Assert(prods.Length == prdCnt);
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
		[Test]
		public void Loops()
		{
			UOW uow = new UOW();
			uow.Service = "Setup";
			
			SqlConnection cn = Conn.GetConn();
			SqlCommand cmd = cn.CreateCommand();
			cmd.CommandType = CommandType.Text;
			cmd.CommandText = "Select * from dbo.Product";
			
			SqlDataAdapter da = new SqlDataAdapter(cmd);

			dsProduct ds = new dsProduct();
			da.Fill(ds);

			Product[] prods = Product.getAll(uow);
			uow.close();

			string subClass;
			int times = 20000;
			
			uow = new UOW();
			uow.Service = "Accessing DataSet";

			DataRowCollection drc =  ds.Tables[1].Rows;
			for (int j = 0; j < times; j++)
				for (int i = 0; i < drc.Count; i++)
				{
					subClass = (string)drc[i][6]; // subClass
				}
			
			uow.close();

			uow = new UOW();
			uow.Service = "Accessing Data Object";
			
			for (int j = 0; j < times; j++)
				for (int i = 0; i < prods.Length; i++)
					subClass = prods[i].ProdSubClass;
			uow.close();

	}
		[Test]
		public void DirectAccess()
		{
			IMap imap = new IdentityMap(500);
			UOW uow = new UOW(imap);
			uow.Service = "Setup";
			
			SqlConnection cn = Conn.GetConn();
			SqlCommand cmd = cn.CreateCommand();
			cmd.CommandType = CommandType.Text;
			cmd.CommandText = "Select * from dbo.Product";
			
			SqlDataAdapter da = new SqlDataAdapter(cmd);

			dsProduct ds = new dsProduct();
			da.Fill(ds);

			Product[] prods = Product.getAll(uow);
			uow.close();

			string subClass;
			int times = 40000;
			int id = 188;
			DataTable table = ds.Tables[1];
			DataColumn[] keys = new DataColumn[1];
			keys[0] = table.Columns[0];

			table.PrimaryKey = keys;
			
			uow = new UOW();
			uow.Service = "Directly Accessing DataSet";
			DataRow row;

			DataRowCollection drc =  ds.Tables[1].Rows;

			for (int j = 0; j < times; j++)
			{
					row = drc.Find(id);
					subClass = (string)row[6]; // subClass
			}
			
			uow.close();

			uow = new UOW(imap);

			uow.Service = "Directly Accessing Data Object";
			Product prod;

			for (int j = 0; j < times; j++)
			{
				prod = Product.find(uow, id); 
				subClass = prod.ProdSubClass;
			}

			uow.close();
		}
	}
}