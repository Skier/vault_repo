//using System;
//using System.Collections;
//using System.Data;
//using System.Text;
//using System.Windows.Forms;
//using System.Data.SqlClient;
//using System.Data.SqlTypes;
//using DPI.Components;
//using DPI.Interfaces;
// 
//namespace DPI.Components
//{
//	[Serializable]
//	public class CardApp : ICardApp 
//	{
//		DateTime appDate;
//		string   idType;
//		string   idNumber;
//		DateTime idExpDate;
//		string   idState;
//		bool     approved;
//		int      dmd;
//		string   cardNum;
//		string   expMonth;
//		string   expYear;
//		string   prevCard;
//
//		public DateTime AppDate 
//		{
//			get { return appDate;  }
//			set { appDate = value; }
//		}
//		public string IdState
//		{
//			get { return idState;  }
//			set	{ idState = value; }
//		} 
//		public bool Approved
//		{
//			get { return approved; }
//			set	{approved = value; }
//		}
//		public DateTime IdExpDate
//		{
//			get {return idExpDate;  }
//			set	{idExpDate = value;	}
//		}
//		public int Dmd
//		{
//			get	{ return dmd;  }
//			set	{ dmd = value; }
//		}
//		public string IdType
//		{
//			get	{ return idType;  }
//			set	{ idType = value; }
//		}
//		public string IdNumber
//		{
//			get	{ return idNumber;  }
//			set	{ idNumber = value;	}
//		}
//		public string CardNum
//		{
//			get	{ return cardNum;  }
//			set	{ cardNum = value;	}
//		}
//		public string ExpMonth
//		{
//			get	{ return expMonth;  }
//			set	{ expMonth = value;	}
//		}
//		public string ExpYear
//		{
//			get { return expYear;}
//			set { expYear = value; }
//		}
//		public string PrevCard
//		{
//			get { return prevCard;}
//			set { prevCard = value; }
//		}
//	}
//}