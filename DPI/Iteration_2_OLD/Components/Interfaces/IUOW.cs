using System;
using System.Data;
using System.Data.SqlClient;
using DPI.Interfaces;
  
namespace DPI.Interfaces
{
	public interface IUOW
	{
		string Service      { get; set; }
		string Workstation  { get; }
		IPage Page          { get; set; }
		SqlConnection Cn    { get; }
		SqlTransaction Tran { get; }
		IMap Imap           { get; }
		int Id              { get; }
		void close();
		void commit();
	}
}