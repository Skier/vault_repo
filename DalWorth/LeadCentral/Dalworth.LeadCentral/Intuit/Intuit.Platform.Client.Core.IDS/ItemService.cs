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
	/// Provides Method to perform CRUD operations on Item Resource of QuickBooks
	/// </summary>
	public class ItemService : IDSBaseService
	{

		/// <summary>
		/// Adds a Item under the specified realm. 
		/// </summary>
		/// <param name="context">PlatformSessionContext object with session info filled</param>
		/// <param name="realmId">Users RealmID</param>
		/// <param name="newItem">Item object to Add</param>
		/// <returns>Returns an updated version of the Item with updated IdType and sync token.</returns>
		public Item AddItem(PlatformSessionContext context, string realmId, Item newItem)
		{
			base.SetServiceTypeProperty(realmId,ref context);
			newItem = (Item)base.AddResource(context, realmId, newItem, IDSResource.item);
			return newItem;
		}

		/// <summary>
        /// Returns a list of all Items under the specified Realm.
        /// </summary>
        /// <param name="context">PlatformSessionContext object with session info filled</param>
        /// <param name="realmId">Users RealmID</param>
        /// <returns>List of all Items</returns>
		public List<Item> FindAll(PlatformSessionContext context, string realmId)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmId: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			IDSResource resource = IDSResource.item;
           
            if (context.ServiceType == IntuitServicesType.QBO)
            {
                resource = IDSResource.items;
            }
			Items listOfObjects = (Items)base.FindAll(context, realmId, resource,typeof(Items));
			if (listOfObjects != null && listOfObjects.Item != null)
            {
                return new List<Item>(listOfObjects.Item);
            }
            else
            {	
				Logger.WriteToLog(TraceLevel.Info,"Item not found.");
                return new List<Item>();
            }
		}

		/// <summary>
        /// Returns a Item based on the Id string.
        /// </summary>
        /// <param name="context">PlatformSessionContext object with session info filled</param>
        /// <param name="realmId">Users RealmID</param>
        /// <param name="itemIdToFind">Item Id</param>
        /// <returns>Item object with specified id</returns>
		public Item FindById(PlatformSessionContext context, string realmId, IdType itemIdToFind)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmId: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			Item itemFound = null;
				
			switch (context.ServiceType)
			{
				case IntuitServicesType.QBD:
					Items items = (Items)base.FindById(context, realmId, itemIdToFind, IDSResource.item, typeof(Items));
					if (items.Item == null || items.Item.Length == 0)
					{
						Logger.WriteToLog(TraceLevel.Info,"Item not found.");
						return null;
					}
					itemFound = items.Item[0];
					break;
				case IntuitServicesType.QBO:
					itemFound = (Item)base.FindById(context, realmId, itemIdToFind, IDSResource.item, typeof(Item));
					break;
			}
            return itemFound;
		}
		
		/// <summary>
        /// Query on Item object basis on the search criteria and company Id.
        /// </summary>
        /// <param name="context">Session information</param>
        /// <param name="realmId">Company, for which search query to be executed.</param>        
        /// <param name="searchQuery">Criteria for the search
		///</param>
        /// <returns>Returns Search result.</returns>
		
		public List<Item> GetItems(PlatformSessionContext context, string realmId, QBQBOItemQuery searchQuery)
        {
			Logger.WriteToLog(TraceLevel.Info,"Realm Id: " +realmId);
			
			base.SetServiceTypeProperty(realmId,ref context);
			
			Items searchItems = null;

						IDSOperationContext operationContext = new IDSOperationContext(IDSResource.item, realmId);
			if (context.ServiceType == IntuitServicesType.QBO)
            {
                operationContext = new IDSOperationContext(IDSResource.items, realmId);
            }
           
			if(searchQuery != null)
			{
            	searchItems = (Items)base.GetResourcesForQuery(context,operationContext,searchQuery);
			}
			else
			{
				searchItems = (Items)base.GetResources(context, operationContext, typeof(Items));
			}
                    
            if (searchItems == null || searchItems.Item == null)
            {
				Logger.WriteToLog(TraceLevel.Info, "Item not found.");
                return null;
            }
            return new List<Item>(searchItems.Item);
        }

		/// <summary>
		/// Updates a Item under the specified realm. 
		/// </summary>
		/// <param name="context">PlatformSessionContext object with session info filled</param>
		/// <param name="realmId">Users RealmID</param>
		/// <param name="newItem">Item object to Update</param>
		/// <returns>Returns an updated version of the Item with updated IdType and sync token.</returns>
		public Item UpdateItem(PlatformSessionContext context, string realmId, Item newItem)
		{
			base.SetServiceTypeProperty(realmId,ref context);
			newItem = (Item)base.UpdateResource(context, realmId, newItem, IDSResource.item);
			return newItem;
		}

		/// <summary>
		/// Deletes a Item under the specified realm. 
		/// </summary>
		/// <param name="context">PlatformSessionContext object with session info filled</param>
		/// <param name="realmId">Users RealmID</param>
		/// <param name="newItem">Item object to Delete</param>
		public void DeleteItem(PlatformSessionContext context, string realmId, Item newItem)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmID: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			base.DeleteResource(context, realmId, newItem, IDSResource.item);
		}

		/// <summary>
		/// Reverts a Item under the specified realm. 
		/// </summary>
		/// <param name="context">PlatformSessionContext object with session info filled</param>
		/// <param name="realmId">Users RealmID</param>
		/// <param name="newItem">Item object to Revert</param>
		/// <returns>Returns an updated version of the Item with updated IdType and sync token.</returns>
		#warning 'Item Revert operation is supported by QB'
		public Item RevertItem(PlatformSessionContext context, string realmId, Item newItem)
		{
			base.SetServiceTypeProperty(realmId,ref context);
			newItem = (Item)base.RevertResource(context, realmId, newItem, IDSResource.item);
			return newItem;
		}

	}
}

