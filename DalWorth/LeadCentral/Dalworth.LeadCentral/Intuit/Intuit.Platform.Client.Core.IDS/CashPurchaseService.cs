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

namespace Intuit.Sb.Cdm.QBO
{
	/// <summary>
	/// Provides Method to perform CRUD operations on CashPurchase Resource of QuickBooks
	/// </summary>
	public class CashPurchaseService : IDSBaseService
	{

		/// <summary>
		/// Adds a CashPurchase under the specified realm. 
		/// </summary>
		/// <param name="context">PlatformSessionContext object with session info filled</param>
		/// <param name="realmId">Users RealmID</param>
		/// <param name="newCashPurchase">CashPurchase object to Add</param>
		/// <returns>Returns an updated version of the CashPurchase with updated IdType and sync token.</returns>
		public CashPurchase AddCashPurchase(PlatformSessionContext context, string realmId, CashPurchase newCashPurchase)
		{
			base.SetServiceTypeProperty(realmId,ref context);
			newCashPurchase = (CashPurchase)base.AddResource(context, realmId, newCashPurchase, IDSResource.cashpurchase);
			return newCashPurchase;
		}

		/// <summary>
        /// Returns a list of all CashPurchases under the specified Realm.
        /// </summary>
        /// <param name="context">PlatformSessionContext object with session info filled</param>
        /// <param name="realmId">Users RealmID</param>
        /// <returns>List of all CashPurchases</returns>
		public List<CashPurchase> FindAll(PlatformSessionContext context, string realmId)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmId: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			IDSResource resource = IDSResource.cashpurchase;
           
            if (context.ServiceType == IntuitServicesType.QBO)
            {
                resource = IDSResource.cashpurchases;
            }
			CashPurchases listOfObjects = (CashPurchases)base.FindAll(context, realmId, resource,typeof(CashPurchases));
			if (listOfObjects != null && listOfObjects.CashPurchase != null)
            {
                return new List<CashPurchase>(listOfObjects.CashPurchase);
            }
            else
            {	
				Logger.WriteToLog(TraceLevel.Info,"CashPurchase not found.");
                return new List<CashPurchase>();
            }
		}

		/// <summary>
        /// Returns a CashPurchase based on the Id string.
        /// </summary>
        /// <param name="context">PlatformSessionContext object with session info filled</param>
        /// <param name="realmId">Users RealmID</param>
        /// <param name="cashpurchaseIdToFind">CashPurchase Id</param>
        /// <returns>CashPurchase object with specified id</returns>
		public CashPurchase FindById(PlatformSessionContext context, string realmId, IdType cashpurchaseIdToFind)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmId: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			CashPurchase cashpurchaseFound = null;
			cashpurchaseFound = (CashPurchase)base.FindById(context, realmId, cashpurchaseIdToFind, IDSResource.cashpurchase, typeof(CashPurchase));
            return cashpurchaseFound;
		}
		
		/// <summary>
        /// Query on CashPurchase object basis on the search criteria and company Id.
        /// </summary>
        /// <param name="context">Session information</param>
        /// <param name="realmId">Company, for which search query to be executed.</param>        
        /// <param name="searchQuery">Criteria for the search
		///</param>
        /// <returns>Returns Search result.</returns>
		
		public List<CashPurchase> GetCashPurchases(PlatformSessionContext context, string realmId, QBQBOCashPurchaseQuery searchQuery)
        {
			Logger.WriteToLog(TraceLevel.Info,"Realm Id: " +realmId);
			
			base.SetServiceTypeProperty(realmId,ref context);
			
			CashPurchases searchCashPurchases = null;

						IDSOperationContext operationContext = new IDSOperationContext(IDSResource.cashpurchases,realmId);
			
			if(searchQuery != null)
			{
            	searchCashPurchases = (CashPurchases)base.GetResourcesForQuery(context,operationContext,searchQuery);
			}
			else
			{
				searchCashPurchases = (CashPurchases)base.GetResources(context, operationContext, typeof(CashPurchases));
			}
                    
            if (searchCashPurchases == null || searchCashPurchases.CashPurchase == null)
            {
				Logger.WriteToLog(TraceLevel.Info, "CashPurchase not found.");
                return null;
            }
            return new List<CashPurchase>(searchCashPurchases.CashPurchase);
        }

		/// <summary>
		/// Updates a CashPurchase under the specified realm. 
		/// </summary>
		/// <param name="context">PlatformSessionContext object with session info filled</param>
		/// <param name="realmId">Users RealmID</param>
		/// <param name="newCashPurchase">CashPurchase object to Update</param>
		/// <returns>Returns an updated version of the CashPurchase with updated IdType and sync token.</returns>
		public CashPurchase UpdateCashPurchase(PlatformSessionContext context, string realmId, CashPurchase newCashPurchase)
		{
			base.SetServiceTypeProperty(realmId,ref context);
			newCashPurchase = (CashPurchase)base.UpdateResource(context, realmId, newCashPurchase, IDSResource.cashpurchase);
			return newCashPurchase;
		}

		/// <summary>
		/// Deletes a CashPurchase under the specified realm. 
		/// </summary>
		/// <param name="context">PlatformSessionContext object with session info filled</param>
		/// <param name="realmId">Users RealmID</param>
		/// <param name="newCashPurchase">CashPurchase object to Delete</param>
		public void DeleteCashPurchase(PlatformSessionContext context, string realmId, CashPurchase newCashPurchase)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmID: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			base.DeleteResource(context, realmId, newCashPurchase, IDSResource.cashpurchase);
		}

	}
}

