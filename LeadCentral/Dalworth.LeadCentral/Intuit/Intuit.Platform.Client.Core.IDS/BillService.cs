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
	/// Provides Method to perform CRUD operations on Bill Resource of QuickBooks
	/// </summary>
	public class BillService : IDSBaseService
	{

		/// <summary>
		/// Adds a Bill under the specified realm. 
		/// </summary>
		/// <param name="context">PlatformSessionContext object with session info filled</param>
		/// <param name="realmId">Users RealmID</param>
		/// <param name="newBill">Bill object to Add</param>
		/// <returns>Returns an updated version of the Bill with updated IdType and sync token.</returns>
		public Bill AddBill(PlatformSessionContext context, string realmId, Bill newBill)
		{
			base.SetServiceTypeProperty(realmId,ref context);
			newBill = (Bill)base.AddResource(context, realmId, newBill, IDSResource.bill);
			return newBill;
		}

		/// <summary>
        /// Returns a list of all Bills under the specified Realm.
        /// </summary>
        /// <param name="context">PlatformSessionContext object with session info filled</param>
        /// <param name="realmId">Users RealmID</param>
        /// <returns>List of all Bills</returns>
		public List<Bill> FindAll(PlatformSessionContext context, string realmId)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmId: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			IDSResource resource = IDSResource.bill;
           
            if (context.ServiceType == IntuitServicesType.QBO)
            {
                resource = IDSResource.bills;
            }
			Bills listOfObjects = (Bills)base.FindAll(context, realmId, resource,typeof(Bills));
			if (listOfObjects != null && listOfObjects.Bill != null)
            {
                return new List<Bill>(listOfObjects.Bill);
            }
            else
            {	
				Logger.WriteToLog(TraceLevel.Info,"Bill not found.");
                return new List<Bill>();
            }
		}

		/// <summary>
        /// Returns a Bill based on the Id string.
        /// </summary>
        /// <param name="context">PlatformSessionContext object with session info filled</param>
        /// <param name="realmId">Users RealmID</param>
        /// <param name="billIdToFind">Bill Id</param>
        /// <returns>Bill object with specified id</returns>
		public Bill FindById(PlatformSessionContext context, string realmId, IdType billIdToFind)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmId: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			Bill billFound = null;
				
			switch (context.ServiceType)
			{
				case IntuitServicesType.QBD:
					Bills bills = (Bills)base.FindById(context, realmId, billIdToFind, IDSResource.bill, typeof(Bills));
					if (bills.Bill == null || bills.Bill.Length == 0)
					{
						Logger.WriteToLog(TraceLevel.Info,"Bill not found.");
						return null;
					}
					billFound = bills.Bill[0];
					break;
				case IntuitServicesType.QBO:
					billFound = (Bill)base.FindById(context, realmId, billIdToFind, IDSResource.bill, typeof(Bill));
					break;
			}
            return billFound;
		}
		
		/// <summary>
        /// Query on Bill object basis on the search criteria and company Id.
        /// </summary>
        /// <param name="context">Session information</param>
        /// <param name="realmId">Company, for which search query to be executed.</param>        
        /// <param name="searchQuery">Criteria for the search
		///</param>
        /// <returns>Returns Search result.</returns>
		
		public List<Bill> GetBills(PlatformSessionContext context, string realmId, QBQBOBillQuery searchQuery)
        {
			Logger.WriteToLog(TraceLevel.Info,"Realm Id: " +realmId);
			
			base.SetServiceTypeProperty(realmId,ref context);
			
			Bills searchBills = null;

						IDSOperationContext operationContext = new IDSOperationContext(IDSResource.bill, realmId);
			if (context.ServiceType == IntuitServicesType.QBO)
            {
                operationContext = new IDSOperationContext(IDSResource.bills, realmId);
            }
           
			if(searchQuery != null)
			{
            	searchBills = (Bills)base.GetResourcesForQuery(context,operationContext,searchQuery);
			}
			else
			{
				searchBills = (Bills)base.GetResources(context, operationContext, typeof(Bills));
			}
                    
            if (searchBills == null || searchBills.Bill == null)
            {
				Logger.WriteToLog(TraceLevel.Info, "Bill not found.");
                return null;
            }
            return new List<Bill>(searchBills.Bill);
        }

		/// <summary>
		/// Updates a Bill under the specified realm. 
		/// </summary>
		/// <param name="context">PlatformSessionContext object with session info filled</param>
		/// <param name="realmId">Users RealmID</param>
		/// <param name="newBill">Bill object to Update</param>
		/// <returns>Returns an updated version of the Bill with updated IdType and sync token.</returns>
		public Bill UpdateBill(PlatformSessionContext context, string realmId, Bill newBill)
		{
			base.SetServiceTypeProperty(realmId,ref context);
			newBill = (Bill)base.UpdateResource(context, realmId, newBill, IDSResource.bill);
			return newBill;
		}

		/// <summary>
		/// Deletes a Bill under the specified realm. 
		/// </summary>
		/// <param name="context">PlatformSessionContext object with session info filled</param>
		/// <param name="realmId">Users RealmID</param>
		/// <param name="newBill">Bill object to Delete</param>
		public void DeleteBill(PlatformSessionContext context, string realmId, Bill newBill)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmID: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			base.DeleteResource(context, realmId, newBill, IDSResource.bill);
		}

		/// <summary>
		/// Reverts a Bill under the specified realm. 
		/// </summary>
		/// <param name="context">PlatformSessionContext object with session info filled</param>
		/// <param name="realmId">Users RealmID</param>
		/// <param name="newBill">Bill object to Revert</param>
		/// <returns>Returns an updated version of the Bill with updated IdType and sync token.</returns>
		#warning 'Bill Revert operation is supported by QB'
		public Bill RevertBill(PlatformSessionContext context, string realmId, Bill newBill)
		{
			base.SetServiceTypeProperty(realmId,ref context);
			newBill = (Bill)base.RevertResource(context, realmId, newBill, IDSResource.bill);
			return newBill;
		}

	}
}

