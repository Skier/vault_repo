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
	/// Provides Method to perform CRUD operations on Vendor Resource of QuickBooks
	/// </summary>
	public class VendorService : IDSBaseService
	{

		/// <summary>
		/// Adds a Vendor under the specified realm. 
		/// </summary>
		/// <param name="context">PlatformSessionContext object with session info filled</param>
		/// <param name="realmId">Users RealmID</param>
		/// <param name="newVendor">Vendor object to Add</param>
		/// <returns>Returns an updated version of the Vendor with updated IdType and sync token.</returns>
		public Vendor AddVendor(PlatformSessionContext context, string realmId, Vendor newVendor)
		{
			base.SetServiceTypeProperty(realmId,ref context);
			newVendor = (Vendor)base.AddResource(context, realmId, newVendor, IDSResource.vendor);
			return newVendor;
		}

		/// <summary>
        /// Returns a list of all Vendors under the specified Realm.
        /// </summary>
        /// <param name="context">PlatformSessionContext object with session info filled</param>
        /// <param name="realmId">Users RealmID</param>
        /// <returns>List of all Vendors</returns>
		public List<Vendor> FindAll(PlatformSessionContext context, string realmId)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmId: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			IDSResource resource = IDSResource.vendor;
           
            if (context.ServiceType == IntuitServicesType.QBO)
            {
                resource = IDSResource.vendors;
            }
			Vendors listOfObjects = (Vendors)base.FindAll(context, realmId, resource,typeof(Vendors));
			if (listOfObjects != null && listOfObjects.Vendor != null)
            {
                return new List<Vendor>(listOfObjects.Vendor);
            }
            else
            {	
				Logger.WriteToLog(TraceLevel.Info,"Vendor not found.");
                return new List<Vendor>();
            }
		}

		/// <summary>
        /// Returns a Vendor based on the Id string.
        /// </summary>
        /// <param name="context">PlatformSessionContext object with session info filled</param>
        /// <param name="realmId">Users RealmID</param>
        /// <param name="vendorIdToFind">Vendor Id</param>
        /// <returns>Vendor object with specified id</returns>
		public Vendor FindById(PlatformSessionContext context, string realmId, IdType vendorIdToFind)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmId: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			Vendor vendorFound = null;
				
			switch (context.ServiceType)
			{
				case IntuitServicesType.QBD:
					Vendors vendors = (Vendors)base.FindById(context, realmId, vendorIdToFind, IDSResource.vendor, typeof(Vendors));
					if (vendors.Vendor == null || vendors.Vendor.Length == 0)
					{
						Logger.WriteToLog(TraceLevel.Info,"Vendor not found.");
						return null;
					}
					vendorFound = vendors.Vendor[0];
					break;
				case IntuitServicesType.QBO:
					vendorFound = (Vendor)base.FindById(context, realmId, vendorIdToFind, IDSResource.vendor, typeof(Vendor));
					break;
			}
            return vendorFound;
		}
		
		/// <summary>
        /// Query on Vendor object basis on the search criteria and company Id.
        /// </summary>
        /// <param name="context">Session information</param>
        /// <param name="realmId">Company, for which search query to be executed.</param>        
        /// <param name="searchQuery">Criteria for the search
		///</param>
        /// <returns>Returns Search result.</returns>
		
		public List<Vendor> GetVendors(PlatformSessionContext context, string realmId, QBQBOVendorQuery searchQuery)
        {
			Logger.WriteToLog(TraceLevel.Info,"Realm Id: " +realmId);
			
			base.SetServiceTypeProperty(realmId,ref context);
			
			Vendors searchVendors = null;

						IDSOperationContext operationContext = new IDSOperationContext(IDSResource.vendor, realmId);
			if (context.ServiceType == IntuitServicesType.QBO)
            {
                operationContext = new IDSOperationContext(IDSResource.vendors, realmId);
            }
           
			if(searchQuery != null)
			{
            	searchVendors = (Vendors)base.GetResourcesForQuery(context,operationContext,searchQuery);
			}
			else
			{
				searchVendors = (Vendors)base.GetResources(context, operationContext, typeof(Vendors));
			}
                    
            if (searchVendors == null || searchVendors.Vendor == null)
            {
				Logger.WriteToLog(TraceLevel.Info, "Vendor not found.");
                return null;
            }
            return new List<Vendor>(searchVendors.Vendor);
        }

		/// <summary>
		/// Updates a Vendor under the specified realm. 
		/// </summary>
		/// <param name="context">PlatformSessionContext object with session info filled</param>
		/// <param name="realmId">Users RealmID</param>
		/// <param name="newVendor">Vendor object to Update</param>
		/// <returns>Returns an updated version of the Vendor with updated IdType and sync token.</returns>
		public Vendor UpdateVendor(PlatformSessionContext context, string realmId, Vendor newVendor)
		{
			base.SetServiceTypeProperty(realmId,ref context);
			newVendor = (Vendor)base.UpdateResource(context, realmId, newVendor, IDSResource.vendor);
			return newVendor;
		}

		/// <summary>
		/// Deletes a Vendor under the specified realm. 
		/// </summary>
		/// <param name="context">PlatformSessionContext object with session info filled</param>
		/// <param name="realmId">Users RealmID</param>
		/// <param name="newVendor">Vendor object to Delete</param>
		public void DeleteVendor(PlatformSessionContext context, string realmId, Vendor newVendor)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmID: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			base.DeleteResource(context, realmId, newVendor, IDSResource.vendor);
		}

		/// <summary>
		/// Reverts a Vendor under the specified realm. 
		/// </summary>
		/// <param name="context">PlatformSessionContext object with session info filled</param>
		/// <param name="realmId">Users RealmID</param>
		/// <param name="newVendor">Vendor object to Revert</param>
		/// <returns>Returns an updated version of the Vendor with updated IdType and sync token.</returns>
		#warning 'Vendor Revert operation is supported by QB'
		public Vendor RevertVendor(PlatformSessionContext context, string realmId, Vendor newVendor)
		{
			base.SetServiceTypeProperty(realmId,ref context);
			newVendor = (Vendor)base.RevertResource(context, realmId, newVendor, IDSResource.vendor);
			return newVendor;
		}

	}
}

