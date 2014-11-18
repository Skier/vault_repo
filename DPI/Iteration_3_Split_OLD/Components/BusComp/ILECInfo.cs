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
	[Serializable]
	public class ILECInfo : IILECInfo // Read-only
	{
		/*        Data        */
		int orgId;
		string ilecCode;
		string ilecName;
		bool isDefault;
        
		/*        Properties        */
		//	public string Zip
		//	{
		//		get { return zip; }
		//	}
		public int OrgId
		{
			get { return orgId; }
		}
		public string ILECCode
		{
			get { return ilecCode; }
		}
		public string ILECName 
		{
			get { return ilecName; }
		}
		public bool IsDefault
		{
			get { return isDefault; }
		}
		/*        Constructors			*/
		internal ILECInfo() {}
		/*		Static methods		*/
		public static ILECInfo[] getILECs(UOW uow, string zip)
		{
			return new ILECInfoSql().getILECs(uow, zip);
		}
		public static ILECInfo Find(UOW uow, string code)
		{
			return (new ILECInfoSql().findFromCode(uow, code))[0];
		}
		public static ILECInfo Find(UOW uow, int id)
		{
			return (new ILECInfoSql().findFromId(uow, id))[0];
		}
		
		public override string ToString()
		{
			return ilecName;
		}

		/*		Implementation		*/
 
		/*		SQL		*/
		class ILECInfoSql
		{
			public ILECInfo[] getILECs(UOW uow, string zip)
			{
				SqlCommand cmd = makeCommand(uow);
				cmd.Parameters.Add("@Zip", SqlDbType.VarChar, 5).Value = zip;
				cmd.CommandText = "dbo.spOrder_getIlecsAtZip";
				return execReader(cmd);
			}
			public ILECInfo[] findFromCode(UOW uow, string code)
			{
				if (code == null)
				{
					Exception e = new ArgumentNullException("Code", "Must supply an ILEC code.");
					throw e;
				}

				SqlCommand cmd = makeCommand(uow);
				cmd.CommandType=CommandType.Text;
				cmd.CommandText = "select orgid, code, name, 'F' as isDefault from ilecs where code=@code";
				cmd.Parameters.Add("@code", SqlDbType.Char, 3).Value = code;
				return execReader(cmd);
			}
			public ILECInfo[] findFromId(UOW uow, int id)
			{
				SqlCommand cmd = makeCommand(uow);
				cmd.CommandType=CommandType.Text;
				cmd.CommandText = "select orgid, code, name, 'F' as isDefault from ilecs where orgid=@id";
				cmd.Parameters.Add("@id", SqlDbType.Int, 0).Value = id;
				return execReader(cmd);
			}
			/*        Implementation        */
			SqlCommand makeCommand(UOW uow)
			{
				SqlCommand cmd = uow.Cn.CreateCommand();
				cmd.Transaction = uow.Tran; 
				cmd.CommandType = CommandType.StoredProcedure;
				return cmd;
			}
			ILECInfo[] execReader(SqlCommand cmd)
			{
				ArrayList ar = new ArrayList();
				SqlDataReader rdr = cmd.ExecuteReader();

				try
				{
					while(rdr.Read())
						ar.Add(reader(rdr));

					ILECInfo[] recs = new ILECInfo[ar.Count];
					ar.CopyTo(recs);
					return recs;
				}
				catch (Exception e)
				{
					Console.WriteLine(e.Message);
					throw e;
				}
				finally
				{
					if (!rdr.IsClosed)
						rdr.Close();

				}
			}
			protected ILECInfo reader(SqlDataReader rdr)
			{
				ILECInfo rec = new ILECInfo();
                
				if (rdr["orgid"] != DBNull.Value)
					rec.orgId = (int) rdr["orgid"];

				if (rdr["code"] != DBNull.Value)
					rec.ilecCode = (string) rdr["code"];
 
				if (rdr["name"] != DBNull.Value)
					rec.ilecName = (string) rdr["name"];			

				if (rdr["isdefault"] != DBNull.Value)
					rec.isDefault = (string)rdr["isdefault"] == "T" ? true : false;

				return rec;
			}
		}
	}
}