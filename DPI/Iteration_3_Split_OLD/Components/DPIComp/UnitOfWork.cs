using System;
using System.Data.SqlClient;
using DPI.Interfaces;

namespace DPI.Components
{
	public class UOW : IUOW //Unit Of Work
	{
		IMap im;
		SqlTransaction tran; 
		SqlConnection cn;
		IPage page;
		
		bool logged = false;
		bool completed = false;
		
		int id;
	//	static Random random = new Random();
		string ws;
		string service;
		DateTime start;
		
		/*		Properties	*/ 
		public int Id { get { return id; }}
		public bool IsCompleted { get { return completed; }}
		public string Service
		{
			get 
			{ 
				if (service == null)
					return "N/A";
			
				return service; 
			}

			set { service = value; }
		}
		public string Workstation
		{
			get { 
				if (ws == null)
					return "N/A";
			
				return ws; 
			}
		}
		public IPage Page
		{
			get { return page; }
			set { page = value; }
		}
		public SqlConnection Cn
		{
			get 
			{ 
				checkCompleted();

				if (cn == null)
				{
					cn = Conn.GetConn();
					ws = cn.WorkstationId;
				}
				return cn;
			}
		}
		public SqlTransaction Tran
		{
			get { return tran; }
		}
		public IMap Imap
		{
			get { return im; }
		}
		/*		Constructors	*/
		public UOW() : this(new IdentityMap())  {}
		public UOW(IMap im) : this(im, "N/A") 	{}
		public UOW(string service) : this(new IdentityMap(), service) {}
		public UOW(IMap im, string service)
		{
			this.service = service;
			this.im = im;

			id =  new Random().Next(1, 10000);
			start = DateTime.Now;	
		}
		/*		Methods		*/
		public void BeginTransaction()
		{
			if (tran == null)
				tran = Cn.BeginTransaction();
		}
		public void Rollback()
		{
			if (tran != null)
				tran.Rollback();
		}
		public void close()
		{
			try
			{
				if (tran != null)
					tran.Dispose();
			}
			finally
			{
				if (cn != null)
					cn.Close();

				completed = true;	
				log();	
			}
		}
		public void commit()
		{
			checkCompleted();

			if (this.im == null)
				throw new ApplicationException("Identity Map is required for commit()");

			if (tran == null)
				tran = Cn.BeginTransaction();
	
			try
			{
				im.save(this);
				tran.Commit();
			}
			catch (Exception e)
			{
				if (tran != null)
					if (tran.Connection != null) // xact has been rolledback already
						tran.Rollback();
				
				Imap.clear();  // data in IMap are no longer current
				throw e;
			}
			finally
			{
				close();
			}
		}
		void log()
		{
			if (logged)
				return;
			
			TimeSpan duration = DateTime.Now - start;
	//		Console.WriteLine("UOW: {3}, {2}: Service: '{0}' took: {1}", Service, 
	//						   duration.ToString(), Workstation, id.ToString());
			logged = true;
		}
		void checkCompleted()
		{
			if (completed)
				cn = null;
//				throw new ApplicationException("UOW " + id.ToString() + " has already completed");
		}
	}
}