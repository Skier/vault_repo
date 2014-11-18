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
	/// Provides Method to perform CRUD operations on SalesReceipt Resource of QuickBooks
	/// </summary>
	public class SalesReceiptService : IDSBaseService
	{

		/// <summary>
		/// Adds a SalesReceipt under the specified realm. 
		/// </summary>
		/// <param name="context">PlatformSessionContext object with session info filled</param>
		/// <param name="realmId">Users RealmID</param>
		/// <param name="newSalesReceipt">SalesReceipt object to Add</param>
		/// <returns>Returns an updated version of the SalesReceipt with updated IdType and sync token.</returns>
		public SalesReceipt AddSalesReceipt(PlatformSessionContext context, string realmId, SalesReceipt newSalesReceipt)
		{
			base.SetServiceTypeProperty(realmId,ref context);
			newSalesReceipt = (SalesReceipt)base.AddResource(context, realmId, newSalesReceipt, IDSResource.salesreceipt);
			return newSalesReceipt;
		}

		/// <summary>
        /// Returns a list of all SalesReceipts under the specified Realm.
        /// </summary>
        /// <param name="context">PlatformSessionContext object with session info filled</param>
        /// <param name="realmId">Users RealmID</param>
        /// <returns>List of all SalesReceipts</returns>
		public List<SalesReceipt> FindAll(PlatformSessionContext context, string realmId)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmId: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			IDSResource resource = IDSResource.salesreceipt;
           
            if (context.ServiceType == IntuitServicesType.QBO)
            {
                resource = IDSResource.salesreceipts;
            }
			SalesReceipts listOfObjects = (SalesReceipts)base.FindAll(context, realmId, resource,typeof(SalesReceipts));
			if (listOfObjects != null && listOfObjects.SalesReceipt != null)
            {
                return new List<SalesReceipt>(listOfObjects.SalesReceipt);
            }
            else
            {	
				Logger.WriteToLog(TraceLevel.Info,"SalesReceipt not found.");
                return new List<SalesReceipt>();
            }
		}

		/// <summary>
        /// Returns a SalesReceipt based on the Id string.
        /// </summary>
        /// <param name="context">PlatformSessionContext object with session info filled</param>
        /// <param name="realmId">Users RealmID</param>
        /// <param name="salesreceiptIdToFind">SalesReceipt Id</param>
        /// <returns>SalesReceipt object with specified id</returns>
		public SalesReceipt FindById(PlatformSessionContext context, string realmId, IdType salesreceiptIdToFind)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmId: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			SalesReceipt salesreceiptFound = null;
				
			switch (context.ServiceType)
			{
				case IntuitServicesType.QBD:
					SalesReceipts salesreceipts = (SalesReceipts)base.FindById(context, realmId, salesreceiptIdToFind, IDSResource.salesreceipt, typeof(SalesReceipts));
					if (salesreceipts.SalesReceipt == null || salesreceipts.SalesReceipt.Length == 0)
					{
						Logger.WriteToLog(TraceLevel.Info,"SalesReceipt not found.");
						return null;
					}
					salesreceiptFound = salesreceipts.SalesReceipt[0];
					break;
				case IntuitServicesType.QBO:
					salesreceiptFound = (SalesReceipt)base.FindById(context, realmId, salesreceiptIdToFind, IDSResource.salesreceipt, typeof(SalesReceipt));
					break;
			}
            return salesreceiptFound;
		}
		
		/// <summary>
        /// Query on SalesReceipt object basis on the search criteria and company Id.
        /// </summary>
        /// <param name="context">Session information</param>
        /// <param name="realmId">Company, for which search query to be executed.</param>        
        /// <param name="searchQuery">Criteria for the search
		///</param>
        /// <returns>Returns Search result.</returns>
		
		public List<SalesReceipt> GetSalesReceipts(PlatformSessionContext context, string realmId, QBQBOSalesReceiptQuery searchQuery)
        {
			Logger.WriteToLog(TraceLevel.Info,"Realm Id: " +realmId);
			
			base.SetServiceTypeProperty(realmId,ref context);
			
			SalesReceipts searchSalesReceipts = null;

						IDSOperationContext operationContext = new IDSOperationContext(IDSResource.salesreceipt, realmId);
			if (context.ServiceType == IntuitServicesType.QBO)
            {
                operationContext = new IDSOperationContext(IDSResource.salesreceipts, realmId);
            }
           
			if(searchQuery != null)
			{
            	searchSalesReceipts = (SalesReceipts)base.GetResourcesForQuery(context,operationContext,searchQuery);
			}
			else
			{
				searchSalesReceipts = (SalesReceipts)base.GetResources(context, operationContext, typeof(SalesReceipts));
			}
                    
            if (searchSalesReceipts == null || searchSalesReceipts.SalesReceipt == null)
            {
				Logger.WriteToLog(TraceLevel.Info, "SalesReceipt not found.");
                return null;
            }
            return new List<SalesReceipt>(searchSalesReceipts.SalesReceipt);
        }

		/// <summary>
		/// Updates a SalesReceipt under the specified realm. 
		/// </summary>
		/// <param name="context">PlatformSessionContext object with session info filled</param>
		/// <param name="realmId">Users RealmID</param>
		/// <param name="newSalesReceipt">SalesReceipt object to Update</param>
		/// <returns>Returns an updated version of the SalesReceipt with updated IdType and sync token.</returns>
		public SalesReceipt UpdateSalesReceipt(PlatformSessionContext context, string realmId, SalesReceipt newSalesReceipt)
		{
			base.SetServiceTypeProperty(realmId,ref context);
			newSalesReceipt = (SalesReceipt)base.UpdateResource(context, realmId, newSalesReceipt, IDSResource.salesreceipt);
			return newSalesReceipt;
		}

		/// <summary>
		/// Deletes a SalesReceipt under the specified realm. 
		/// </summary>
		/// <param name="context">PlatformSessionContext object with session info filled</param>
		/// <param name="realmId">Users RealmID</param>
		/// <param name="newSalesReceipt">SalesReceipt object to Delete</param>
		public void DeleteSalesReceipt(PlatformSessionContext context, string realmId, SalesReceipt newSalesReceipt)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmID: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			base.DeleteResource(context, realmId, newSalesReceipt, IDSResource.salesreceipt);
		}

		/// <summary>
		/// Reverts a SalesReceipt under the specified realm. 
		/// </summary>
		/// <param name="context">PlatformSessionContext object with session info filled</param>
		/// <param name="realmId">Users RealmID</param>
		/// <param name="newSalesReceipt">SalesReceipt object to Revert</param>
		/// <returns>Returns an updated version of the SalesReceipt with updated IdType and sync token.</returns>
		#warning 'SalesReceipt Revert operation is supported by QB'
		public SalesReceipt RevertSalesReceipt(PlatformSessionContext context, string realmId, SalesReceipt newSalesReceipt)
		{
			base.SetServiceTypeProperty(realmId,ref context);
			newSalesReceipt = (SalesReceipt)base.RevertResource(context, realmId, newSalesReceipt, IDSResource.salesreceipt);
			return newSalesReceipt;
		}

	}
}

