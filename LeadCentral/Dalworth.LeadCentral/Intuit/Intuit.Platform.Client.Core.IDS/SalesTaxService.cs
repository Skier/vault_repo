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
	/// Provides Method to perform CRUD operations on SalesTax Resource of QuickBooks
	/// </summary>
	public class SalesTaxService : IDSBaseService
	{

		/// <summary>
		/// Adds a SalesTax under the specified realm. 
		/// </summary>
		/// <param name="context">PlatformSessionContext object with session info filled</param>
		/// <param name="realmId">Users RealmID</param>
		/// <param name="newSalesTax">SalesTax object to Add</param>
		/// <returns>Returns an updated version of the SalesTax with updated IdType and sync token.</returns>
		public SalesTax AddSalesTax(PlatformSessionContext context, string realmId, SalesTax newSalesTax)
		{
			base.SetServiceTypeProperty(realmId,ref context);
			newSalesTax = (SalesTax)base.AddResource(context, realmId, newSalesTax, IDSResource.salestax);
			return newSalesTax;
		}

		/// <summary>
        /// Returns a list of all SalesTaxes under the specified Realm.
        /// </summary>
        /// <param name="context">PlatformSessionContext object with session info filled</param>
        /// <param name="realmId">Users RealmID</param>
        /// <returns>List of all SalesTaxes</returns>
		public List<SalesTax> FindAll(PlatformSessionContext context, string realmId)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmId: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			IDSResource resource = IDSResource.salestax;
           
			SalesTaxes listOfObjects = (SalesTaxes)base.FindAll(context, realmId, resource,typeof(SalesTaxes));
			if (listOfObjects != null && listOfObjects.SalesTax != null)
            {
                return new List<SalesTax>(listOfObjects.SalesTax);
            }
            else
            {	
				Logger.WriteToLog(TraceLevel.Info,"SalesTax not found.");
                return new List<SalesTax>();
            }
		}

		/// <summary>
        /// Returns a SalesTax based on the Id string.
        /// </summary>
        /// <param name="context">PlatformSessionContext object with session info filled</param>
        /// <param name="realmId">Users RealmID</param>
        /// <param name="salestaxIdToFind">SalesTax Id</param>
        /// <returns>SalesTax object with specified id</returns>
		public SalesTax FindById(PlatformSessionContext context, string realmId, IdType salestaxIdToFind)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmId: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			SalesTax salestaxFound = null;
			SalesTaxes salestaxes = (SalesTaxes)base.FindById(context, realmId, salestaxIdToFind, IDSResource.salestax, typeof(SalesTaxes));
			if (salestaxes.SalesTax == null || salestaxes.SalesTax.Length == 0)
			{
				Logger.WriteToLog(TraceLevel.Info,"SalesTax not found.");
				return null;
			}
			salestaxFound = salestaxes.SalesTax[0];
            return salestaxFound;
		}
		
		/// <summary>
        /// Query on SalesTax object basis on the search criteria and company Id.
        /// </summary>
        /// <param name="context">Session information</param>
        /// <param name="realmId">Company, for which search query to be executed.</param>        
        /// <param name="searchQuery">Criteria for the search
		///</param>
        /// <returns>Returns Search result.</returns>
		
		public List<SalesTax> GetSalesTaxes(PlatformSessionContext context, string realmId, SalesTaxQuery searchQuery)
        {
			Logger.WriteToLog(TraceLevel.Info,"Realm Id: " +realmId);
			
			base.SetServiceTypeProperty(realmId,ref context);
			
			SalesTaxes searchSalesTaxes = null;

						IDSOperationContext operationContext = new IDSOperationContext(IDSResource.salestax,realmId);
			
			if(searchQuery != null)
			{
            	searchSalesTaxes = (SalesTaxes)base.GetResourcesForQuery(context,operationContext,searchQuery);
			}
			else
			{
				searchSalesTaxes = (SalesTaxes)base.GetResources(context, operationContext, typeof(SalesTaxes));
			}
                    
            if (searchSalesTaxes == null || searchSalesTaxes.SalesTax == null)
            {
				Logger.WriteToLog(TraceLevel.Info, "SalesTax not found.");
                return null;
            }
            return new List<SalesTax>(searchSalesTaxes.SalesTax);
        }

		/// <summary>
		/// Deletes a SalesTax under the specified realm. 
		/// </summary>
		/// <param name="context">PlatformSessionContext object with session info filled</param>
		/// <param name="realmId">Users RealmID</param>
		/// <param name="newSalesTax">SalesTax object to Delete</param>
		public void DeleteSalesTax(PlatformSessionContext context, string realmId, SalesTax newSalesTax)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmID: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			base.DeleteResource(context, realmId, newSalesTax, IDSResource.salestax);
		}

	}
}

