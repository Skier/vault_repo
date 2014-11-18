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
	public class CurCustomer :  Immutable
	{
		const string ook = "CurCustomer";
		const string typ = "struct";
		public override IDomKey IKey    { get { return KeyFactory.getKey(ook, typ);  }}
		public static   IDomKey IMapKey { get { return new CurCustomer().IKey; }}

		/*        Data        */
		public int curAcctNum;
		
		/*		Constructors		*/
		public CurCustomer() {}
		public CurCustomer(IMap imap)
		{
			imap.add(this);
		}
	}
}