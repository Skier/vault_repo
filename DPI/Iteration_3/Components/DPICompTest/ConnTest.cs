using System;
using System.Data;
using System.Data.SqlClient;
using DPI.Interfaces;

namespace DPI.Components
{
	public class ConnTest
	{
		public static void Main()
		{
			ConnTest t = new ConnTest();

			for (int i = 0; i < 10; i++)
			{
				Console.WriteLine("Round " + (i + 1).ToString());
				t.test();
			}
			Console.Read();
		}		
		public void test()
		{
			SqlConnection cn = new SqlConnection(conString());
			
			cn.Open(); 
			ActivateYonixRole(cn);
			cn.Close();
		}
		public static void  ActivateYonixRole(SqlConnection cn)
		{
			SqlCommand cmd = cn.CreateCommand();
			cmd.CommandText = "sp_setapprole 'Yonix', 'dpitcash1', 'none'";
			
			try 
			{
				cmd.ExecuteScalar();
				Console.WriteLine("Works fine.");
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}
		public static string conString()
		{	
			return	@"User ID=amittelman;" 
				+	@"Data Source=SQLDBSDEV;"
				+	@"Persist Security Info=True;min pool size=1;max pool size=1;"
				+	@"Password=password;Initial Catalog=YONIXDPISTG;";
		}
	}
}