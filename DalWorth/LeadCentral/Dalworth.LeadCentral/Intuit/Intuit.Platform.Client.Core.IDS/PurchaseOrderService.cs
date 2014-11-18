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

namespace Intuit.Sb.Cdm.QB
{
	/// <summary>
	/// Provides Method to perform CRUD operations on PurchaseOrder Resource of QuickBooks
	/// </summary>
	public class PurchaseOrderService : IDSBaseService
	{

		/// <summary>
		/// Adds a PurchaseOrder under the specified realm. 
		/// </summary>
		/// <param name="context">PlatformSessionContext object with session info filled</param>
		/// <param name="realmId">Users RealmID</param>
		/// <param name="newPurchaseOrder">PurchaseOrder object to Add</param>
		/// <returns>Returns an updated version of the PurchaseOrder with updated IdType and sync token.</returns>
		public PurchaseOrder AddPurchaseOrder(PlatformSessionContext context, string realmId, PurchaseOrder newPurchaseOrder)
		{
			base.SetServiceTypeProperty(realmId,ref context);
			newPurchaseOrder = (PurchaseOrder)base.AddResource(context, realmId, newPurchaseOrder, IDSResource.purchaseorder);
			return newPurchaseOrder;
		}

		/// <summary>
        /// Returns a list of all PurchaseOrders under the specified Realm.
        /// </summary>
        /// <param name="context">PlatformSessionContext object with session info filled</param>
        /// <param name="realmId">Users RealmID</param>
        /// <returns>List of all PurchaseOrders</returns>
		public List<PurchaseOrder> FindAll(PlatformSessionContext context, string realmId)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmId: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			IDSResource resource = IDSResource.purchaseorder;
           
			PurchaseOrders listOfObjects = (PurchaseOrders)base.FindAll(context, realmId, resource,typeof(PurchaseOrders));
			if (listOfObjects != null && listOfObjects.PurchaseOrder != null)
            {
                return new List<PurchaseOrder>(listOfObjects.PurchaseOrder);
            }
            else
            {	
				Logger.WriteToLog(TraceLevel.Info,"PurchaseOrder not found.");
                return new List<PurchaseOrder>();
            }
		}

		/// <summary>
        /// Returns a PurchaseOrder based on the Id string.
        /// </summary>
        /// <param name="context">PlatformSessionContext object with session info filled</param>
        /// <param name="realmId">Users RealmID</param>
        /// <param name="purchaseorderIdToFind">PurchaseOrder Id</param>
        /// <returns>PurchaseOrder object with specified id</returns>
		public PurchaseOrder FindById(PlatformSessionContext context, string realmId, IdType purchaseorderIdToFind)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmId: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			PurchaseOrder purchaseorderFound = null;
			PurchaseOrders purchaseorders = (PurchaseOrders)base.FindById(context, realmId, purchaseorderIdToFind, IDSResource.purchaseorder, typeof(PurchaseOrders));
			if (purchaseorders.PurchaseOrder == null || purchaseorders.PurchaseOrder.Length == 0)
			{
				Logger.WriteToLog(TraceLevel.Info,"PurchaseOrder not found.");
				return null;
			}
			purchaseorderFound = purchaseorders.PurchaseOrder[0];
            return purchaseorderFound;
		}
		
		/// <summary>
        /// Query on PurchaseOrder object basis on the search criteria and company Id.
        /// </summary>
        /// <param name="context">Session information</param>
        /// <param name="realmId">Company, for which search query to be executed.</param>        
        /// <param name="searchQuery">Criteria for the search
		///</param>
        /// <returns>Returns Search result.</returns>
		
		public List<PurchaseOrder> GetPurchaseOrders(PlatformSessionContext context, string realmId, PurchaseOrderQuery searchQuery)
        {
			Logger.WriteToLog(TraceLevel.Info,"Realm Id: " +realmId);
			
			base.SetServiceTypeProperty(realmId,ref context);
			
			PurchaseOrders searchPurchaseOrders = null;

						IDSOperationContext operationContext = new IDSOperationContext(IDSResource.purchaseorder,realmId);
			
			if(searchQuery != null)
			{
            	searchPurchaseOrders = (PurchaseOrders)base.GetResourcesForQuery(context,operationContext,searchQuery);
			}
			else
			{
				searchPurchaseOrders = (PurchaseOrders)base.GetResources(context, operationContext, typeof(PurchaseOrders));
			}
                    
            if (searchPurchaseOrders == null || searchPurchaseOrders.PurchaseOrder == null)
            {
				Logger.WriteToLog(TraceLevel.Info, "PurchaseOrder not found.");
                return null;
            }
            return new List<PurchaseOrder>(searchPurchaseOrders.PurchaseOrder);
        }

		/// <summary>
		/// Deletes a PurchaseOrder under the specified realm. 
		/// </summary>
		/// <param name="context">PlatformSessionContext object with session info filled</param>
		/// <param name="realmId">Users RealmID</param>
		/// <param name="newPurchaseOrder">PurchaseOrder object to Delete</param>
		public void DeletePurchaseOrder(PlatformSessionContext context, string realmId, PurchaseOrder newPurchaseOrder)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmID: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			base.DeleteResource(context, realmId, newPurchaseOrder, IDSResource.purchaseorder);
		}

	}
}

