 using System;
using DPI.Interfaces;
using DPI.Components.EPSolutions;

 namespace DPI.Components
 {
	 [Serializable]  
	 public class Receipt : IReceipt
	 {		 
		 protected int accNumber;
		 protected string confNum ;
		 protected int demand;
		 protected string pin;

		 /*		Properties		*/		 
		 public int Demand
		 {
			 get { return demand; }
		 }
		 public int AccNumber 
		 {
			 get {return accNumber; }
		 }
		 public string ConfNum 
		 {
			 get {return confNum; }
			 set { confNum = value; }
		 }
		 public string Pin 
		 {
			 get {return pin; }
			 set { pin = value; }
		 }
		 /*		Contructors		*/
		 public Receipt() {}
		 public Receipt(string confNum)
		 {		
			 this.confNum = confNum;
		 }
		 public Receipt(string confNum, int accNumber) : this (confNum)
		 {		
			 this.accNumber = accNumber;
		 }
		 public Receipt(int demand, string confNum, int accNumber) : this (confNum, accNumber)
		 {		
			 this.demand = demand;
		 }
	 }	 


	 [Serializable]  
	 public class EnergyRcpt : IEnergyRcpt
	 {
		 #region Data
		 bool isEnrolled;
		 bool isPaid;
		 string accNumber;
		 string accName;
		 string eSIID;
		 string pin;
		 string confNum;
		 string errMsg;
		 #endregion

		 #region Properties
		 public bool IsEnrolled				
		 { 
			 get { return isEnrolled;  } 
			 set { isEnrolled = value; }
		 }
		 public bool IsPaid
		 { 
			 get { return isPaid;  } 
			 set { isPaid = value; }
		 }
		 public string AccNumber
		 { 
			 get { return accNumber;  } 
			 set { accNumber = value; }
		 }
		 public string AccName
		 { 
			 get { return accName;  } 
			 set { accName = value; }
		 }
		 public string ESIID
		 { 
			 get { return eSIID;  } 
			 set { eSIID = value; }
		 }
		 public string Pin
		 { 
			 get { return pin;  } 
			 set { pin = value; }
		 }
		 public string ConfNum
		 { 
			 get { return confNum;  } 
			 set { confNum = value; }
		 }
		 public string ErrMsg
		 { 
			 get { return errMsg;  } 
			 set { errMsg = value; }
		 }
		 #endregion

		 #region Contructors
		 public EnergyRcpt() {}
		 public EnergyRcpt(string confNum)
		 {		
			 this.confNum = confNum;
		 }
		 public EnergyRcpt(string confNum, EnrollmentResponse er, PaymentResponse pr) : this (confNum)
		 {		
			 if (er == null)
				 return;

			 if (er.Enrollment == null)
				 return;

			 this.accNumber = er.Enrollment.AccountNumber;
			 this.accName = er.Enrollment.AccountName;
			 this.isEnrolled = true;

			 if (pr == null)
				 return;

			 if (pr.Balance == null)
				 return;

			 this.isPaid = true;
		 }
		 public EnergyRcpt(string confNum, PaymentResponse pr) : this (confNum)
		 {		
			 if (pr == null)
				 return;

			 if (pr.Balance == null)
				 return;

			 this.isPaid = true;
			 this.accName = pr.Balance.AccountName;
			 this.accNumber = pr.Balance.AccountNumber;			 
		 }
		 #endregion

		 #region Implementation

		 #endregion

	 }	 

}