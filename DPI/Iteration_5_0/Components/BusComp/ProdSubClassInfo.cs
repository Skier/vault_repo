using System;
using DPI.Components;
using DPI.Interfaces;

namespace DPI.Components
{
	[Serializable]
	public class ProdSubClassInfo 
	{
		/*        Data        */
		string prodSubClass;
		string prodCode;
		//string pymtAllocSeq;
		bool isInstallForEachInstance;
		//string notes;
		string provHints;
		bool isRestrictedToOneInstance;
		string fulfillMethod;
		bool isFulfillRecurring;
		bool suppressZeroPrice;
		bool suppressOnWebReceipt;
		bool selectionUnselectsCurrent;
        
     
		/*        Properties        */
		public string SubClass
		{
			get { return prodSubClass; }
		}
		public string ProdCode
		{
			get { return prodCode; }
		}
		//public string PymtAllocSeq
		//{
		//	get { return pymtAllocSeq; }
		//}
		public bool IsInstallForEachInstance
		{
			get { return isInstallForEachInstance; }
		}
		//public string Notes
		//{
		//	get { return notes; }
		//}
		public string ProvHints
		{
			get { return provHints; }
		}
		public bool IsRestrictedToOneInstance
		{
			get { return isRestrictedToOneInstance; }
		}
		public string FulfillMethod
		{
			get { return fulfillMethod; }
		}
		public bool IsFulfillRecurring
		{
			get { return isFulfillRecurring; }
		}
		public bool SuppressZeroPrice
		{
			get { return suppressZeroPrice; }
		}
		public bool SuppressOnWebReceipt
		{
			get { return suppressOnWebReceipt; }
		}
		public bool SelectionUnselectsCurrent { get { return selectionUnselectsCurrent; }}
 		public ProdSubClassInfo(ProdSubClass ps)
		{
			this.prodSubClass = ps.SubClass;
			this.prodCode = ps.ProdCode;
		//	this.pymtAllocSeq = ps.PymtAllocSeq;
			this.isInstallForEachInstance = ps.IsInstallForEachInstance;
		//	this.notes= ps.Notes;
			this.provHints = ps.ProvHints;
			this.isRestrictedToOneInstance = ps.IsRestrictedToOneInstance;
			this.fulfillMethod = ps.FulfillMethod;
			this.isFulfillRecurring = ps.IsFulfillRecurring;
			this.suppressZeroPrice = ps.SuppressZeroPrice;
			this.suppressOnWebReceipt = ps.SuppressOnWebReceipt;
			this.selectionUnselectsCurrent = ps.SelectionUnselectsCurrent;
		}
		public ProdSubClassInfo(ProdSubClassInfo ps)
		{
			this.prodSubClass = ps.SubClass;
			this.prodCode = ps.ProdCode;
			//	this.pymtAllocSeq = ps.PymtAllocSeq;
			this.isInstallForEachInstance = ps.IsInstallForEachInstance;
			//	this.notes= ps.Notes;
			this.provHints = ps.ProvHints;
			this.isRestrictedToOneInstance = ps.IsRestrictedToOneInstance;
			this.fulfillMethod = ps.FulfillMethod;
			this.isFulfillRecurring = ps.IsFulfillRecurring;
			this.suppressZeroPrice = ps.SuppressZeroPrice;
			this.suppressOnWebReceipt = ps.SuppressOnWebReceipt;
			this.selectionUnselectsCurrent = ps.SelectionUnselectsCurrent;
		}
		public ProdSubClassInfo Clone()
		{
			return new ProdSubClassInfo(this);
		}
		public static ProdSubClassInfo[] Conv(ProdSubClass[] ps)
		{
			ProdSubClassInfo[] psi = new ProdSubClassInfo[ps.Length];
			for (int i = 0; i < psi.Length; i++)
				psi[i] = new ProdSubClassInfo(ps[i]);

			return psi;
		}
	}
}