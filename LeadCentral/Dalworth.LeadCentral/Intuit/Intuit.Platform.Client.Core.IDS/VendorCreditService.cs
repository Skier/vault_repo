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
	/// Provides Method to perform CRUD operations on VendorCredit Resource of QuickBooks
	/// </summary>
	public class VendorCreditService : IDSBaseService
	{

		/// <summary>
		/// Adds a VendorCredit under the specified realm. 
		/// </summary>
		/// <param name="context">PlatformSessionContext object with session info filled</param>
		/// <param name="realmId">Users RealmID</param>
		/// <param name="newVendorCredit">VendorCredit object to Add</param>
		/// <returns>Returns an updated version of the VendorCredit with updated IdType and sync token.</returns>
		public VendorCredit AddVendorCredit(PlatformSessionContext context, string realmId, VendorCredit newVendorCredit)
		{
			base.SetServiceTypeProperty(realmId,ref context);
			newVendorCredit = (VendorCredit)base.AddResource(context, realmId, newVendorCredit, IDSResource.vendorcredit);
			return newVendorCredit;
		}

		/// <summary>
        /// Returns a list of all VendorCredits under the specified Realm.
        /// </summary>
        /// <param name="context">PlatformSessionContext object with session info filled</param>
        /// <param name="realmId">Users RealmID</param>
        /// <returns>List of all VendorCredits</returns>
		public List<VendorCredit> FindAll(PlatformSessionContext context, string realmId)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmId: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			IDSResource resource = IDSResource.vendorcredit;
           
			VendorCredits listOfObjects = (VendorCredits)base.FindAll(context, realmId, resource,typeof(VendorCredits));
			if (listOfObjects != null && listOfObjects.VendorCredit != null)
            {
                return new List<VendorCredit>(listOfObjects.VendorCredit);
            }
            else
            {	
				Logger.WriteToLog(TraceLevel.Info,"VendorCredit not found.");
                return new List<VendorCredit>();
            }
		}

		/// <summary>
        /// Returns a VendorCredit based on the Id string.
        /// </summary>
        /// <param name="context">PlatformSessionContext object with session info filled</param>
        /// <param name="realmId">Users RealmID</param>
        /// <param name="vendorcreditIdToFind">VendorCredit Id</param>
        /// <returns>VendorCredit object with specified id</returns>
		public VendorCredit FindById(PlatformSessionContext context, string realmId, IdType vendorcreditIdToFind)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmId: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			VendorCredit vendorcreditFound = null;
			VendorCredits vendorcredits = (VendorCredits)base.FindById(context, realmId, vendorcreditIdToFind, IDSResource.vendorcredit, typeof(VendorCredits));
			if (vendorcredits.VendorCredit == null || vendorcredits.VendorCredit.Length == 0)
			{
				Logger.WriteToLog(TraceLevel.Info,"VendorCredit not found.");
				return null;
			}
			vendorcreditFound = vendorcredits.VendorCredit[0];
            return vendorcreditFound;
		}
		
		/// <summary>
        /// Query on VendorCredit object basis on the search criteria and company Id.
        /// </summary>
        /// <param name="context">Session information</param>
        /// <param name="realmId">Company, for which search query to be executed.</param>        
        /// <param name="searchQuery">Criteria for the search
		///</param>
        /// <returns>Returns Search result.</returns>
		
		public List<VendorCredit> GetVendorCredits(PlatformSessionContext context, string realmId, VendorCreditQuery searchQuery)
        {
			Logger.WriteToLog(TraceLevel.Info,"Realm Id: " +realmId);
			
			base.SetServiceTypeProperty(realmId,ref context);
			
			VendorCredits searchVendorCredits = null;

						IDSOperationContext operationContext = new IDSOperationContext(IDSResource.vendorcredit,realmId);
			
			if(searchQuery != null)
			{
            	searchVendorCredits = (VendorCredits)base.GetResourcesForQuery(context,operationContext,searchQuery);
			}
			else
			{
				searchVendorCredits = (VendorCredits)base.GetResources(context, operationContext, typeof(VendorCredits));
			}
                    
            if (searchVendorCredits == null || searchVendorCredits.VendorCredit == null)
            {
				Logger.WriteToLog(TraceLevel.Info, "VendorCredit not found.");
                return null;
            }
            return new List<VendorCredit>(searchVendorCredits.VendorCredit);
        }

		/// <summary>
		/// Deletes a VendorCredit under the specified realm. 
		/// </summary>
		/// <param name="context">PlatformSessionContext object with session info filled</param>
		/// <param name="realmId">Users RealmID</param>
		/// <param name="newVendorCredit">VendorCredit object to Delete</param>
		public void DeleteVendorCredit(PlatformSessionContext context, string realmId, VendorCredit newVendorCredit)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmID: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			base.DeleteResource(context, realmId, newVendorCredit, IDSResource.vendorcredit);
		}

	}
}

