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
	/// Provides Method to perform CRUD operations on SalesOrder Resource of QuickBooks
	/// </summary>
	public class SalesOrderService : IDSBaseService
	{

		/// <summary>
		/// Adds a SalesOrder under the specified realm. 
		/// </summary>
		/// <param name="context">PlatformSessionContext object with session info filled</param>
		/// <param name="realmId">Users RealmID</param>
		/// <param name="newSalesOrder">SalesOrder object to Add</param>
		/// <returns>Returns an updated version of the SalesOrder with updated IdType and sync token.</returns>
		public SalesOrder AddSalesOrder(PlatformSessionContext context, string realmId, SalesOrder newSalesOrder)
		{
			base.SetServiceTypeProperty(realmId,ref context);
			newSalesOrder = (SalesOrder)base.AddResource(context, realmId, newSalesOrder, IDSResource.salesorder);
			return newSalesOrder;
		}

		/// <summary>
        /// Returns a list of all SalesOrders under the specified Realm.
        /// </summary>
        /// <param name="context">PlatformSessionContext object with session info filled</param>
        /// <param name="realmId">Users RealmID</param>
        /// <returns>List of all SalesOrders</returns>
		public List<SalesOrder> FindAll(PlatformSessionContext context, string realmId)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmId: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			IDSResource resource = IDSResource.salesorder;
           
			SalesOrders listOfObjects = (SalesOrders)base.FindAll(context, realmId, resource,typeof(SalesOrders));
			if (listOfObjects != null && listOfObjects.SalesOrder != null)
            {
                return new List<SalesOrder>(listOfObjects.SalesOrder);
            }
            else
            {	
				Logger.WriteToLog(TraceLevel.Info,"SalesOrder not found.");
                return new List<SalesOrder>();
            }
		}

		/// <summary>
        /// Returns a SalesOrder based on the Id string.
        /// </summary>
        /// <param name="context">PlatformSessionContext object with session info filled</param>
        /// <param name="realmId">Users RealmID</param>
        /// <param name="salesorderIdToFind">SalesOrder Id</param>
        /// <returns>SalesOrder object with specified id</returns>
		public SalesOrder FindById(PlatformSessionContext context, string realmId, IdType salesorderIdToFind)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmId: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			SalesOrder salesorderFound = null;
			SalesOrders salesorders = (SalesOrders)base.FindById(context, realmId, salesorderIdToFind, IDSResource.salesorder, typeof(SalesOrders));
			if (salesorders.SalesOrder == null || salesorders.SalesOrder.Length == 0)
			{
				Logger.WriteToLog(TraceLevel.Info,"SalesOrder not found.");
				return null;
			}
			salesorderFound = salesorders.SalesOrder[0];
            return salesorderFound;
		}
		
		/// <summary>
        /// Query on SalesOrder object basis on the search criteria and company Id.
        /// </summary>
        /// <param name="context">Session information</param>
        /// <param name="realmId">Company, for which search query to be executed.</param>        
        /// <param name="searchQuery">Criteria for the search
		///</param>
        /// <returns>Returns Search result.</returns>
		
		public List<SalesOrder> GetSalesOrders(PlatformSessionContext context, string realmId, SalesOrderQuery searchQuery)
        {
			Logger.WriteToLog(TraceLevel.Info,"Realm Id: " +realmId);
			
			base.SetServiceTypeProperty(realmId,ref context);
			
			SalesOrders searchSalesOrders = null;

						IDSOperationContext operationContext = new IDSOperationContext(IDSResource.salesorder,realmId);
			
			if(searchQuery != null)
			{
            	searchSalesOrders = (SalesOrders)base.GetResourcesForQuery(context,operationContext,searchQuery);
			}
			else
			{
				searchSalesOrders = (SalesOrders)base.GetResources(context, operationContext, typeof(SalesOrders));
			}
                    
            if (searchSalesOrders == null || searchSalesOrders.SalesOrder == null)
            {
				Logger.WriteToLog(TraceLevel.Info, "SalesOrder not found.");
                return null;
            }
            return new List<SalesOrder>(searchSalesOrders.SalesOrder);
        }

		/// <summary>
		/// Deletes a SalesOrder under the specified realm. 
		/// </summary>
		/// <param name="context">PlatformSessionContext object with session info filled</param>
		/// <param name="realmId">Users RealmID</param>
		/// <param name="newSalesOrder">SalesOrder object to Delete</param>
		public void DeleteSalesOrder(PlatformSessionContext context, string realmId, SalesOrder newSalesOrder)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmID: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			base.DeleteResource(context, realmId, newSalesOrder, IDSResource.salesorder);
		}

	}
}

