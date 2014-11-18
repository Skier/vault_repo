/*
 * Copyright (c) 2009-2010 Intuit, Inc.
 * All rights reserved. This program and the accompanying materials
 * are made available under the terms of the Eclipse Public License v1.0
 * which accompanies this distribution, and is available at
 * http://www.opensource.org/licenses/eclipse-1.0.php
 * 
 * Contributors:
 *    Intuit Partner Platform – initial contribution
 */

using System;
using System.Collections.Generic;
using Intuit.Sb.Cdm;
using Intuit.Platform.Client.Core;
using System.Diagnostics;
using Intuit.Platform.Client.Core.IDS;

namespace Intuit.Sb.Cdm.Common
{
	/// <summary>
	/// Provides Method to perform CRUD operations on BillPayment Resource of QuickBooks
	/// </summary>
	public class BillPaymentService : IDSBaseService
	{

		/// <summary>
		/// Adds a BillPayment under the specified realm. 
		/// </summary>
		/// <param name="context">PlatformSessionContext object with session info filled</param>
		/// <param name="realmId">Users RealmID</param>
		/// <param name="newBillPayment">BillPayment object to Add</param>
		/// <returns>Returns an updated version of the BillPayment with updated IdType and sync token.</returns>
		public BillPayment AddBillPayment(PlatformSessionContext context, string realmId, BillPayment newBillPayment)
		{
			base.SetServiceTypeProperty(realmId,ref context);
			newBillPayment = (BillPayment)base.AddResource(context, realmId, newBillPayment, IDSResource.billpayment);
			return newBillPayment;
		}

		/// <summary>
        /// Returns a list of all BillPayments under the specified Realm.
        /// </summary>
        /// <param name="context">PlatformSessionContext object with session info filled</param>
        /// <param name="realmId">Users RealmID</param>
        /// <returns>List of all BillPayments</returns>
		public List<BillPayment> FindAll(PlatformSessionContext context, string realmId)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmId: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			IDSResource resource = IDSResource.billpayment;
           
            if (context.ServiceType == IntuitServicesType.QBO)
            {
                resource = IDSResource.billpayments;
            }
			BillPayments listOfObjects = (BillPayments)base.FindAll(context, realmId, resource,typeof(BillPayments));
			if (listOfObjects != null && listOfObjects.BillPayment != null)
            {
                return new List<BillPayment>(listOfObjects.BillPayment);
            }
            else
            {	
				Logger.WriteToLog(TraceLevel.Info,"BillPayment not found.");
                return new List<BillPayment>();
            }
		}

		/// <summary>
        /// Returns a BillPayment based on the Id string.
        /// </summary>
        /// <param name="context">PlatformSessionContext object with session info filled</param>
        /// <param name="realmId">Users RealmID</param>
        /// <param name="billpaymentIdToFind">BillPayment Id</param>
        /// <returns>BillPayment object with specified id</returns>
		public BillPayment FindById(PlatformSessionContext context, string realmId, IdType billpaymentIdToFind)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmId: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			BillPayment billpaymentFound = null;
				
			switch (context.ServiceType)
			{
				case IntuitServicesType.QBD:
					BillPayments billpayments = (BillPayments)base.FindById(context, realmId, billpaymentIdToFind, IDSResource.billpayment, typeof(BillPayments));
					if (billpayments.BillPayment == null || billpayments.BillPayment.Length == 0)
					{
						Logger.WriteToLog(TraceLevel.Info,"BillPayment not found.");
						return null;
					}
					billpaymentFound = billpayments.BillPayment[0];
					break;
				case IntuitServicesType.QBO:
					billpaymentFound = (BillPayment)base.FindById(context, realmId, billpaymentIdToFind, IDSResource.billpayment, typeof(BillPayment));
					break;
			}
            return billpaymentFound;
		}
		
		/// <summary>
        /// Query on BillPayment object basis on the search criteria and company Id.
        /// </summary>
        /// <param name="context">Session information</param>
        /// <param name="realmId">Company, for which search query to be executed.</param>        
        /// <param name="searchQuery">Criteria for the search
		///</param>
        /// <returns>Returns Search result.</returns>
		
		public List<BillPayment> GetBillPayments(PlatformSessionContext context, string realmId, QBQBOBillPaymentQuery searchQuery)
        {
			Logger.WriteToLog(TraceLevel.Info,"Realm Id: " +realmId);
			
			base.SetServiceTypeProperty(realmId,ref context);
			
			BillPayments searchBillPayments = null;

						IDSOperationContext operationContext = new IDSOperationContext(IDSResource.billpayment, realmId);
			if (context.ServiceType == IntuitServicesType.QBO)
            {
                operationContext = new IDSOperationContext(IDSResource.billpayments, realmId);
            }
           
			if(searchQuery != null)
			{
            	searchBillPayments = (BillPayments)base.GetResourcesForQuery(context,operationContext,searchQuery);
			}
			else
			{
				searchBillPayments = (BillPayments)base.GetResources(context, operationContext, typeof(BillPayments));
			}
                    
            if (searchBillPayments == null || searchBillPayments.BillPayment == null)
            {
				Logger.WriteToLog(TraceLevel.Info, "BillPayment not found.");
                return null;
            }
            return new List<BillPayment>(searchBillPayments.BillPayment);
        }

		/// <summary>
		/// Updates a BillPayment under the specified realm. 
		/// </summary>
		/// <param name="context">PlatformSessionContext object with session info filled</param>
		/// <param name="realmId">Users RealmID</param>
		/// <param name="newBillPayment">BillPayment object to Update</param>
		/// <returns>Returns an updated version of the BillPayment with updated IdType and sync token.</returns>
		#warning 'BillPayment Update operation is supported by QBO'
		public BillPayment UpdateBillPayment(PlatformSessionContext context, string realmId, BillPayment newBillPayment)
		{
			base.SetServiceTypeProperty(realmId,ref context);
			newBillPayment = (BillPayment)base.UpdateResource(context, realmId, newBillPayment, IDSResource.billpayment);
			return newBillPayment;
		}

		/// <summary>
		/// Deletes a BillPayment under the specified realm. 
		/// </summary>
		/// <param name="context">PlatformSessionContext object with session info filled</param>
		/// <param name="realmId">Users RealmID</param>
		/// <param name="newBillPayment">BillPayment object to Delete</param>
		public void DeleteBillPayment(PlatformSessionContext context, string realmId, BillPayment newBillPayment)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmID: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			base.DeleteResource(context, realmId, newBillPayment, IDSResource.billpayment);
		}

	}
}

