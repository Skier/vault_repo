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
	/// Provides Method to perform CRUD operations on ItemReceipt Resource of QuickBooks
	/// </summary>
	public class ItemReceiptService : IDSBaseService
	{

		/// <summary>
		/// Adds a ItemReceipt under the specified realm. 
		/// </summary>
		/// <param name="context">PlatformSessionContext object with session info filled</param>
		/// <param name="realmId">Users RealmID</param>
		/// <param name="newItemReceipt">ItemReceipt object to Add</param>
		/// <returns>Returns an updated version of the ItemReceipt with updated IdType and sync token.</returns>
		public ItemReceipt AddItemReceipt(PlatformSessionContext context, string realmId, ItemReceipt newItemReceipt)
		{
			base.SetServiceTypeProperty(realmId,ref context);
			newItemReceipt = (ItemReceipt)base.AddResource(context, realmId, newItemReceipt, IDSResource.itemreceipt);
			return newItemReceipt;
		}

		/// <summary>
        /// Returns a list of all ItemReceipts under the specified Realm.
        /// </summary>
        /// <param name="context">PlatformSessionContext object with session info filled</param>
        /// <param name="realmId">Users RealmID</param>
        /// <returns>List of all ItemReceipts</returns>
		public List<ItemReceipt> FindAll(PlatformSessionContext context, string realmId)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmId: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			IDSResource resource = IDSResource.itemreceipt;
           
			ItemReceipts listOfObjects = (ItemReceipts)base.FindAll(context, realmId, resource,typeof(ItemReceipts));
			if (listOfObjects != null && listOfObjects.ItemReceipt != null)
            {
                return new List<ItemReceipt>(listOfObjects.ItemReceipt);
            }
            else
            {	
				Logger.WriteToLog(TraceLevel.Info,"ItemReceipt not found.");
                return new List<ItemReceipt>();
            }
		}

		/// <summary>
        /// Returns a ItemReceipt based on the Id string.
        /// </summary>
        /// <param name="context">PlatformSessionContext object with session info filled</param>
        /// <param name="realmId">Users RealmID</param>
        /// <param name="itemreceiptIdToFind">ItemReceipt Id</param>
        /// <returns>ItemReceipt object with specified id</returns>
		public ItemReceipt FindById(PlatformSessionContext context, string realmId, IdType itemreceiptIdToFind)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmId: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			ItemReceipt itemreceiptFound = null;
			ItemReceipts itemreceipts = (ItemReceipts)base.FindById(context, realmId, itemreceiptIdToFind, IDSResource.itemreceipt, typeof(ItemReceipts));
			if (itemreceipts.ItemReceipt == null || itemreceipts.ItemReceipt.Length == 0)
			{
				Logger.WriteToLog(TraceLevel.Info,"ItemReceipt not found.");
				return null;
			}
			itemreceiptFound = itemreceipts.ItemReceipt[0];
            return itemreceiptFound;
		}
		
		/// <summary>
        /// Query on ItemReceipt object basis on the search criteria and company Id.
        /// </summary>
        /// <param name="context">Session information</param>
        /// <param name="realmId">Company, for which search query to be executed.</param>        
        /// <param name="searchQuery">Criteria for the search
		///</param>
        /// <returns>Returns Search result.</returns>
		
		public List<ItemReceipt> GetItemReceipts(PlatformSessionContext context, string realmId, ItemReceiptQuery searchQuery)
        {
			Logger.WriteToLog(TraceLevel.Info,"Realm Id: " +realmId);
			
			base.SetServiceTypeProperty(realmId,ref context);
			
			ItemReceipts searchItemReceipts = null;

						IDSOperationContext operationContext = new IDSOperationContext(IDSResource.itemreceipt,realmId);
			
			if(searchQuery != null)
			{
            	searchItemReceipts = (ItemReceipts)base.GetResourcesForQuery(context,operationContext,searchQuery);
			}
			else
			{
				searchItemReceipts = (ItemReceipts)base.GetResources(context, operationContext, typeof(ItemReceipts));
			}
                    
            if (searchItemReceipts == null || searchItemReceipts.ItemReceipt == null)
            {
				Logger.WriteToLog(TraceLevel.Info, "ItemReceipt not found.");
                return null;
            }
            return new List<ItemReceipt>(searchItemReceipts.ItemReceipt);
        }

		/// <summary>
		/// Deletes a ItemReceipt under the specified realm. 
		/// </summary>
		/// <param name="context">PlatformSessionContext object with session info filled</param>
		/// <param name="realmId">Users RealmID</param>
		/// <param name="newItemReceipt">ItemReceipt object to Delete</param>
		public void DeleteItemReceipt(PlatformSessionContext context, string realmId, ItemReceipt newItemReceipt)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmID: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			base.DeleteResource(context, realmId, newItemReceipt, IDSResource.itemreceipt);
		}

	}
}

