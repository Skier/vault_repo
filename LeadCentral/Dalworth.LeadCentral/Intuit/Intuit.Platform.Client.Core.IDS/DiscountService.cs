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
	/// Provides Method to perform CRUD operations on Discount Resource of QuickBooks
	/// </summary>
	public class DiscountService : IDSBaseService
	{

		/// <summary>
		/// Adds a Discount under the specified realm. 
		/// </summary>
		/// <param name="context">PlatformSessionContext object with session info filled</param>
		/// <param name="realmId">Users RealmID</param>
		/// <param name="newDiscount">Discount object to Add</param>
		/// <returns>Returns an updated version of the Discount with updated IdType and sync token.</returns>
		public Discount AddDiscount(PlatformSessionContext context, string realmId, Discount newDiscount)
		{
			base.SetServiceTypeProperty(realmId,ref context);
			newDiscount = (Discount)base.AddResource(context, realmId, newDiscount, IDSResource.discount);
			return newDiscount;
		}

		/// <summary>
        /// Returns a list of all Discounts under the specified Realm.
        /// </summary>
        /// <param name="context">PlatformSessionContext object with session info filled</param>
        /// <param name="realmId">Users RealmID</param>
        /// <returns>List of all Discounts</returns>
		public List<Discount> FindAll(PlatformSessionContext context, string realmId)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmId: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			IDSResource resource = IDSResource.discount;
           
			Discounts listOfObjects = (Discounts)base.FindAll(context, realmId, resource,typeof(Discounts));
			if (listOfObjects != null && listOfObjects.Discount != null)
            {
                return new List<Discount>(listOfObjects.Discount);
            }
            else
            {	
				Logger.WriteToLog(TraceLevel.Info,"Discount not found.");
                return new List<Discount>();
            }
		}

		/// <summary>
        /// Returns a Discount based on the Id string.
        /// </summary>
        /// <param name="context">PlatformSessionContext object with session info filled</param>
        /// <param name="realmId">Users RealmID</param>
        /// <param name="discountIdToFind">Discount Id</param>
        /// <returns>Discount object with specified id</returns>
		public Discount FindById(PlatformSessionContext context, string realmId, IdType discountIdToFind)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmId: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			Discount discountFound = null;
			Discounts discounts = (Discounts)base.FindById(context, realmId, discountIdToFind, IDSResource.discount, typeof(Discounts));
			if (discounts.Discount == null || discounts.Discount.Length == 0)
			{
				Logger.WriteToLog(TraceLevel.Info,"Discount not found.");
				return null;
			}
			discountFound = discounts.Discount[0];
            return discountFound;
		}
		
		/// <summary>
        /// Query on Discount object basis on the search criteria and company Id.
        /// </summary>
        /// <param name="context">Session information</param>
        /// <param name="realmId">Company, for which search query to be executed.</param>        
        /// <param name="searchQuery">Criteria for the search
		///</param>
        /// <returns>Returns Search result.</returns>
		
		public List<Discount> GetDiscounts(PlatformSessionContext context, string realmId, DiscountQuery searchQuery)
        {
			Logger.WriteToLog(TraceLevel.Info,"Realm Id: " +realmId);
			
			base.SetServiceTypeProperty(realmId,ref context);
			
			Discounts searchDiscounts = null;

						IDSOperationContext operationContext = new IDSOperationContext(IDSResource.discount,realmId);
			
			if(searchQuery != null)
			{
            	searchDiscounts = (Discounts)base.GetResourcesForQuery(context,operationContext,searchQuery);
			}
			else
			{
				searchDiscounts = (Discounts)base.GetResources(context, operationContext, typeof(Discounts));
			}
                    
            if (searchDiscounts == null || searchDiscounts.Discount == null)
            {
				Logger.WriteToLog(TraceLevel.Info, "Discount not found.");
                return null;
            }
            return new List<Discount>(searchDiscounts.Discount);
        }

		/// <summary>
		/// Updates a Discount under the specified realm. 
		/// </summary>
		/// <param name="context">PlatformSessionContext object with session info filled</param>
		/// <param name="realmId">Users RealmID</param>
		/// <param name="newDiscount">Discount object to Update</param>
		/// <returns>Returns an updated version of the Discount with updated IdType and sync token.</returns>
		public Discount UpdateDiscount(PlatformSessionContext context, string realmId, Discount newDiscount)
		{
			base.SetServiceTypeProperty(realmId,ref context);
			newDiscount = (Discount)base.UpdateResource(context, realmId, newDiscount, IDSResource.discount);
			return newDiscount;
		}

		/// <summary>
		/// Deletes a Discount under the specified realm. 
		/// </summary>
		/// <param name="context">PlatformSessionContext object with session info filled</param>
		/// <param name="realmId">Users RealmID</param>
		/// <param name="newDiscount">Discount object to Delete</param>
		public void DeleteDiscount(PlatformSessionContext context, string realmId, Discount newDiscount)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmID: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			base.DeleteResource(context, realmId, newDiscount, IDSResource.discount);
		}

		/// <summary>
		/// Reverts a Discount under the specified realm. 
		/// </summary>
		/// <param name="context">PlatformSessionContext object with session info filled</param>
		/// <param name="realmId">Users RealmID</param>
		/// <param name="newDiscount">Discount object to Revert</param>
		/// <returns>Returns an updated version of the Discount with updated IdType and sync token.</returns>
		public Discount RevertDiscount(PlatformSessionContext context, string realmId, Discount newDiscount)
		{
			base.SetServiceTypeProperty(realmId,ref context);
			newDiscount = (Discount)base.RevertResource(context, realmId, newDiscount, IDSResource.discount);
			return newDiscount;
		}

	}
}

