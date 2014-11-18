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
	/// Provides Method to perform CRUD operations on InventoryAdjustment Resource of QuickBooks
	/// </summary>
	public class InventoryAdjustmentService : IDSBaseService
	{

		/// <summary>
		/// Adds a InventoryAdjustment under the specified realm. 
		/// </summary>
		/// <param name="context">PlatformSessionContext object with session info filled</param>
		/// <param name="realmId">Users RealmID</param>
		/// <param name="newInventoryAdjustment">InventoryAdjustment object to Add</param>
		/// <returns>Returns an updated version of the InventoryAdjustment with updated IdType and sync token.</returns>
		public InventoryAdjustment AddInventoryAdjustment(PlatformSessionContext context, string realmId, InventoryAdjustment newInventoryAdjustment)
		{
			base.SetServiceTypeProperty(realmId,ref context);
			newInventoryAdjustment = (InventoryAdjustment)base.AddResource(context, realmId, newInventoryAdjustment, IDSResource.inventoryadjustment);
			return newInventoryAdjustment;
		}

		/// <summary>
        /// Returns a list of all InventoryAdjustments under the specified Realm.
        /// </summary>
        /// <param name="context">PlatformSessionContext object with session info filled</param>
        /// <param name="realmId">Users RealmID</param>
        /// <returns>List of all InventoryAdjustments</returns>
		public List<InventoryAdjustment> FindAll(PlatformSessionContext context, string realmId)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmId: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			IDSResource resource = IDSResource.inventoryadjustment;
           
			InventoryAdjustments listOfObjects = (InventoryAdjustments)base.FindAll(context, realmId, resource,typeof(InventoryAdjustments));
			if (listOfObjects != null && listOfObjects.InventoryAdjustment != null)
            {
                return new List<InventoryAdjustment>(listOfObjects.InventoryAdjustment);
            }
            else
            {	
				Logger.WriteToLog(TraceLevel.Info,"InventoryAdjustment not found.");
                return new List<InventoryAdjustment>();
            }
		}

		/// <summary>
        /// Returns a InventoryAdjustment based on the Id string.
        /// </summary>
        /// <param name="context">PlatformSessionContext object with session info filled</param>
        /// <param name="realmId">Users RealmID</param>
        /// <param name="inventoryadjustmentIdToFind">InventoryAdjustment Id</param>
        /// <returns>InventoryAdjustment object with specified id</returns>
		public InventoryAdjustment FindById(PlatformSessionContext context, string realmId, IdType inventoryadjustmentIdToFind)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmId: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			InventoryAdjustment inventoryadjustmentFound = null;
			InventoryAdjustments inventoryadjustments = (InventoryAdjustments)base.FindById(context, realmId, inventoryadjustmentIdToFind, IDSResource.inventoryadjustment, typeof(InventoryAdjustments));
			if (inventoryadjustments.InventoryAdjustment == null || inventoryadjustments.InventoryAdjustment.Length == 0)
			{
				Logger.WriteToLog(TraceLevel.Info,"InventoryAdjustment not found.");
				return null;
			}
			inventoryadjustmentFound = inventoryadjustments.InventoryAdjustment[0];
            return inventoryadjustmentFound;
		}
		
		/// <summary>
        /// Query on InventoryAdjustment object basis on the search criteria and company Id.
        /// </summary>
        /// <param name="context">Session information</param>
        /// <param name="realmId">Company, for which search query to be executed.</param>        
        /// <param name="searchQuery">Criteria for the search
		///</param>
        /// <returns>Returns Search result.</returns>
		
		public List<InventoryAdjustment> GetInventoryAdjustments(PlatformSessionContext context, string realmId, InventoryAdjustmentQuery searchQuery)
        {
			Logger.WriteToLog(TraceLevel.Info,"Realm Id: " +realmId);
			
			base.SetServiceTypeProperty(realmId,ref context);
			
			InventoryAdjustments searchInventoryAdjustments = null;

						IDSOperationContext operationContext = new IDSOperationContext(IDSResource.inventoryadjustment,realmId);
			
			if(searchQuery != null)
			{
            	searchInventoryAdjustments = (InventoryAdjustments)base.GetResourcesForQuery(context,operationContext,searchQuery);
			}
			else
			{
				searchInventoryAdjustments = (InventoryAdjustments)base.GetResources(context, operationContext, typeof(InventoryAdjustments));
			}
                    
            if (searchInventoryAdjustments == null || searchInventoryAdjustments.InventoryAdjustment == null)
            {
				Logger.WriteToLog(TraceLevel.Info, "InventoryAdjustment not found.");
                return null;
            }
            return new List<InventoryAdjustment>(searchInventoryAdjustments.InventoryAdjustment);
        }

		/// <summary>
		/// Deletes a InventoryAdjustment under the specified realm. 
		/// </summary>
		/// <param name="context">PlatformSessionContext object with session info filled</param>
		/// <param name="realmId">Users RealmID</param>
		/// <param name="newInventoryAdjustment">InventoryAdjustment object to Delete</param>
		public void DeleteInventoryAdjustment(PlatformSessionContext context, string realmId, InventoryAdjustment newInventoryAdjustment)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmID: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			base.DeleteResource(context, realmId, newInventoryAdjustment, IDSResource.inventoryadjustment);
		}

	}
}

