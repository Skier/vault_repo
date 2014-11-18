 using System;
using DPI.Interfaces;

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
}