//using System;
//using System.Text.RegularExpressions;
//using DPI.Interfaces;
// 
//namespace DPI.ClientComp
//{
//	[Serializable]
//	public class PayInfoMP : IPayInfo
//	{
//
//		#region * ------------ Data ------------*
//		DateTime	payDate;
//		decimal     localAmountDue;
//		decimal     localAmountPaid;
//		decimal     ldAmount;
//		decimal     totalAmountDue;
//		decimal     amountTendered;
//		decimal     changeAmount;
//		PaymentType paymentType;
//		//int			id;		
//		IDemand		parDemand;
//		string		status;
//		string		confNumber;
//		string		vFConf;
//		decimal		totalAmountPaid;
//		bool		isConfReq;
//		#endregion
//
//
//		#region *------------- Properties ------*
//		public DateTime PayDate         
//		{ 
//			get	{	return this.payDate;		}
//			set	{	this.payDate = value;		} 
//		}  // Date Time the money was paid
//		public decimal LocalAmountDue  
//		{ 
//			get { return this.localAmountDue;	} 
//			set { this.localAmountDue = value;	} 
//		}  // Amount Due for local service
//		public decimal LocalAmountPaid 
//		{ 
//			get {return this.localAmountPaid;	} 
//			set {this.localAmountPaid = value;	} 
//		}  // Amount paid for local service
//		public decimal TotalAmountPaid 
//		{ 
//			get {return this.totalAmountPaid;	} 
//			set {this.totalAmountPaid = value;	} 
//		}
//		public decimal LdAmount
//		{ 
//			get {return this.ldAmount;			} 
//			set {this.ldAmount = value;			} 
//		}  // Amount paid for "walk up" ld service 		
//		public decimal TotalAmountDue
//		{ 
//			get {return this.localAmountPaid + this.ldAmount;	}      
//		}  // LocalAmountDue + LdAmount
//		public decimal AmountTendered
//		{ 
//			get { return this.amountTendered;	} 
//			set
//			{ 
//				this.amountTendered = value;
//				if (this.amountTendered > this.totalAmountDue)
//				{
//					this.totalAmountPaid = this.totalAmountDue;
//					return;
//				}
//				this.totalAmountPaid = this.amountTendered;
//			} 
//		}  // Amount received from customer
//		public decimal ChangeAmount
//		{ 
//			get 
//			{ 
//				if (this.AmountTendered < this.TotalAmountDue)
//					return 0m;
//
//				return this.AmountTendered - this.TotalAmountDue;	
//			}
//		}  // Amount tendered - localamountpaid - ldamount
//		public PaymentType PaymentType
//		{ 
//			get { return this.paymentType;		} 
//			set { this.paymentType = value;		} 
//		}  // Type of payment (check, cash, etc)
//		public int Id { get {return 0;} }
//		public int DmdId 
//		{ 
//			get { return this.parDemand.Id;			} 
//			set { this.parDemand.Id = value;		} 
//		}
//		public IDemand ParDemand  
//		{ 
//			get { return this.parDemand;		} 
//			set	{ this.parDemand = value;		} 
//		}
//		public string Status 
//		{ 
//			get {return this.status;			} 
//			set { this.status = value;			} 
//		}
//		public string ConfNumber 
//		{ 
//			get { return this.confNumber;		} 
//			set { this.confNumber = value;		} 
//		}
//		public string VFConf 
//		{ 
//			get { return this.vFConf;			} 
//			set {this.vFConf = value;			} 
//		}
//		public bool IsConfReq 
//		{	
//			get {return this.isConfReq;	} 
//			set { isConfReq = value;	} 
//		}
//		#endregion
//
//		#region * ----- Constructors -------*
//		public PayInfoMP(IDemand dmd)
//		{
//			this.parDemand = dmd;
//		}
//		#endregion
//	}
//}