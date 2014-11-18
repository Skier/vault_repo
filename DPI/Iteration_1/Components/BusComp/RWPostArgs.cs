using System;
using System.Collections;
using System.Xml;
using System.Text;

using DPI.Interfaces;
using DPI.Components;

namespace DPI.Components
{	
	public class RWPostArgs : IRWPostArgs
	{
	#region Data

		const string pType = "PaymentType";
		const string dType = "DmdType";
		const string tType = "TranType";
		const string sType = "ServType";

		IUser user;
		IPayInfo payInfo;
		string tranType;
		string servType;
		string pymtType;
		decimal amtPaid; 
		decimal amtTaxes; 
		decimal amtComm;
		string receitId;
	//	bool isConfRequired;

	#endregion

	#region Properties
		public IUser User			{ get { return user;		}}
		public IPayInfo PayInfo		{ get { return payInfo;		}}
		public string TranType      { get { return tranType;	}}
		public string ServType		{ get { return servType;	}}
		public string PymtType		{ get { return pymtType;	}}
		public decimal AmtPaid		{ get { return amtPaid;		}}
		public decimal AmtTaxes		{ get { return amtTaxes;	}}
		public decimal AmtComm		{ get { return amtComm;		}}
		public string ReceitId		{ get { return receitId;	}}
		
	#endregion

	#region Constructors
		public RWPostArgs(UOW uow, IUser user,  IPayInfo payInfo, string receitId)//, bool isConfRequired)
		{
		//	isConfRequired = isConfRequired;
			this.payInfo = payInfo;
			this.user = user;
			this.amtPaid = payInfo.TotalAmountPaid;
			this.amtTaxes = payInfo.ParDemand.OrderSummary(uow).GetTaxAmt(1);
			//this.amtComm = payInfo.GetComAmt(uow, user.LoginStoreCode);
			this.receitId = receitId;
			
			SetupTypes(uow, payInfo);
		}
	#endregion
	
	#region Implementation
		void SetupTypes(UOW uow, IPayInfo payInfo)
		{
			this.pymtType = (CatMappingRule.TransTo (uow, MapDomain.Rentway, MapCategory.PaymentType, (int)payInfo.PaymentType));

			this.tranType =
				CatMappingRule.TransTo
				  (uow, MapDomain.Rentway, MapCategory.DmdType, MapCategory.TranType, payInfo.ParDemand.DmdType);

			this.servType =
				CatMappingRule.TransTo
				  (uow, MapDomain.Rentway, MapCategory.DmdType,	MapCategory.ServType, payInfo.ParDemand.DmdType);
		}		
	#endregion
	}
}