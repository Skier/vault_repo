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
	/// Provides Method to perform CRUD operations on Check Resource of QuickBooks
	/// </summary>
	public class CheckService : IDSBaseService
	{

		/// <summary>
		/// Adds a Check under the specified realm. 
		/// </summary>
		/// <param name="context">PlatformSessionContext object with session info filled</param>
		/// <param name="realmId">Users RealmID</param>
		/// <param name="newCheck">Check object to Add</param>
		/// <returns>Returns an updated version of the Check with updated IdType and sync token.</returns>
		public Check AddCheck(PlatformSessionContext context, string realmId, Check newCheck)
		{
			base.SetServiceTypeProperty(realmId,ref context);
			newCheck = (Check)base.AddResource(context, realmId, newCheck, IDSResource.check);
			return newCheck;
		}

		/// <summary>
        /// Returns a list of all Checks under the specified Realm.
        /// </summary>
        /// <param name="context">PlatformSessionContext object with session info filled</param>
        /// <param name="realmId">Users RealmID</param>
        /// <returns>List of all Checks</returns>
		public List<Check> FindAll(PlatformSessionContext context, string realmId)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmId: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			IDSResource resource = IDSResource.check;
           
            if (context.ServiceType == IntuitServicesType.QBO)
            {
                resource = IDSResource.checks;
            }
			Checks listOfObjects = (Checks)base.FindAll(context, realmId, resource,typeof(Checks));
			if (listOfObjects != null && listOfObjects.Check != null)
            {
                return new List<Check>(listOfObjects.Check);
            }
            else
            {	
				Logger.WriteToLog(TraceLevel.Info,"Check not found.");
                return new List<Check>();
            }
		}

		/// <summary>
        /// Returns a Check based on the Id string.
        /// </summary>
        /// <param name="context">PlatformSessionContext object with session info filled</param>
        /// <param name="realmId">Users RealmID</param>
        /// <param name="checkIdToFind">Check Id</param>
        /// <returns>Check object with specified id</returns>
		public Check FindById(PlatformSessionContext context, string realmId, IdType checkIdToFind)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmId: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			Check checkFound = null;
				
			switch (context.ServiceType)
			{
				case IntuitServicesType.QBD:
					Checks checks = (Checks)base.FindById(context, realmId, checkIdToFind, IDSResource.check, typeof(Checks));
					if (checks.Check == null || checks.Check.Length == 0)
					{
						Logger.WriteToLog(TraceLevel.Info,"Check not found.");
						return null;
					}
					checkFound = checks.Check[0];
					break;
				case IntuitServicesType.QBO:
					checkFound = (Check)base.FindById(context, realmId, checkIdToFind, IDSResource.check, typeof(Check));
					break;
			}
            return checkFound;
		}
		
		/// <summary>
        /// Query on Check object basis on the search criteria and company Id.
        /// </summary>
        /// <param name="context">Session information</param>
        /// <param name="realmId">Company, for which search query to be executed.</param>        
        /// <param name="searchQuery">Criteria for the search
		///</param>
        /// <returns>Returns Search result.</returns>
		
		public List<Check> GetChecks(PlatformSessionContext context, string realmId, QBQBOCheckQuery searchQuery)
        {
			Logger.WriteToLog(TraceLevel.Info,"Realm Id: " +realmId);
			
			base.SetServiceTypeProperty(realmId,ref context);
			
			Checks searchChecks = null;

						IDSOperationContext operationContext = new IDSOperationContext(IDSResource.check, realmId);
			if (context.ServiceType == IntuitServicesType.QBO)
            {
                operationContext = new IDSOperationContext(IDSResource.checks, realmId);
            }
           
			if(searchQuery != null)
			{
            	searchChecks = (Checks)base.GetResourcesForQuery(context,operationContext,searchQuery);
			}
			else
			{
				searchChecks = (Checks)base.GetResources(context, operationContext, typeof(Checks));
			}
                    
            if (searchChecks == null || searchChecks.Check == null)
            {
				Logger.WriteToLog(TraceLevel.Info, "Check not found.");
                return null;
            }
            return new List<Check>(searchChecks.Check);
        }

		/// <summary>
		/// Updates a Check under the specified realm. 
		/// </summary>
		/// <param name="context">PlatformSessionContext object with session info filled</param>
		/// <param name="realmId">Users RealmID</param>
		/// <param name="newCheck">Check object to Update</param>
		/// <returns>Returns an updated version of the Check with updated IdType and sync token.</returns>
		#warning 'Check Update operation is supported by QBO'
		public Check UpdateCheck(PlatformSessionContext context, string realmId, Check newCheck)
		{
			base.SetServiceTypeProperty(realmId,ref context);
			newCheck = (Check)base.UpdateResource(context, realmId, newCheck, IDSResource.check);
			return newCheck;
		}

		/// <summary>
		/// Deletes a Check under the specified realm. 
		/// </summary>
		/// <param name="context">PlatformSessionContext object with session info filled</param>
		/// <param name="realmId">Users RealmID</param>
		/// <param name="newCheck">Check object to Delete</param>
		public void DeleteCheck(PlatformSessionContext context, string realmId, Check newCheck)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmID: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			base.DeleteResource(context, realmId, newCheck, IDSResource.check);
		}

	}
}

