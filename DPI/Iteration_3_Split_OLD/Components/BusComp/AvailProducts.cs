//using System;
//using System.Collections;
//using System.Data;
//using System.Text;
//using System.Windows.Forms;
//using System.Data.SqlClient;
//using System.Data.SqlTypes;
//using DPI.Interfaces;
//
//namespace DPI.Components
//{
//	[Serializable]  
//	public class JobProds :  Immutable
//	{
//		const string ook = "JobProds";
//		const string typ = "array";
//		public override IDomKey IKey    { get { return KeyFactory.getKey(ook, typ);  }}
//		public static   IDomKey IMapKey { get { return new JobProds().IKey; }}
//
//		/*        Data        */
//		public ProdPrice BasicService;
//	//	public ProdPrice[] AvailZipProds; // products at the zip
//		public ProdPrice[] JobProducts;   //
//		public string zipcode;
//		public int zip;
//		public string IlecCode; 
//		public int ilecId;
//
//		/*		Constructors		*/
//		public JobProds() {}
//		public JobProds(IMap imap, ProdPrice[] AvailZipProds, int zip, string ilecCode)
//		{
//		//	this.JobProducts = this.AvailZipProds = AvailZipProds;
//			this.JobProducts = AvailZipProds;
//			this.zip = zip; 
//			this.IlecCode = ilecCode;
//			imap.add(this);
//		} 
//	}
//}