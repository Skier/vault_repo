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
	public class CustWIP :  Immutable
	{
		const string ook = "CustWIP";
		const string typ = "object";
		public override IDomKey IKey    { get { return KeyFactory.getKey(ook, typ);  }}
		public static   IDomKey IMapKey { get { return new CustWIP().IKey; }}

		/*        Data        */
		public CustInfo custNfo;
		public MailAddr mailAdr;
		public ServAddr servAdr;

		/*		Constructors		*/
		public CustWIP() {}
		public CustWIP(IMap imap, CustInfo ci, MailAddr ma, ServAddr sa)
		{
			//	this.JobProducts = this.AvailZipProds = AvailZipProds;
			this.custNfo=ci;
			this.mailAdr=ma;
			this.servAdr=sa;

			imap.add(this);
		}
	}
}
