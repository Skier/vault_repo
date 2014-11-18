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
	/// Provides Method to perform CRUD operations on Estimate Resource of QuickBooks
	/// </summary>
	public class EstimateService : IDSBaseService
	{

		/// <summary>
		/// Adds a Estimate under the specified realm. 
		/// </summary>
		/// <param name="context">PlatformSessionContext object with session info filled</param>
		/// <param name="realmId">Users RealmID</param>
		/// <param name="newEstimate">Estimate object to Add</param>
		/// <returns>Returns an updated version of the Estimate with updated IdType and sync token.</returns>
		public Estimate AddEstimate(PlatformSessionContext context, string realmId, Estimate newEstimate)
		{
			base.SetServiceTypeProperty(realmId,ref context);
			newEstimate = (Estimate)base.AddResource(context, realmId, newEstimate, IDSResource.estimate);
			return newEstimate;
		}

		/// <summary>
        /// Returns a list of all Estimates under the specified Realm.
        /// </summary>
        /// <param name="context">PlatformSessionContext object with session info filled</param>
        /// <param name="realmId">Users RealmID</param>
        /// <returns>List of all Estimates</returns>
		public List<Estimate> FindAll(PlatformSessionContext context, string realmId)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmId: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			IDSResource resource = IDSResource.estimate;
           
            if (context.ServiceType == IntuitServicesType.QBO)
            {
                resource = IDSResource.estimates;
            }
			Estimates listOfObjects = (Estimates)base.FindAll(context, realmId, resource,typeof(Estimates));
			if (listOfObjects != null && listOfObjects.Estimate != null)
            {
                return new List<Estimate>(listOfObjects.Estimate);
            }
            else
            {	
				Logger.WriteToLog(TraceLevel.Info,"Estimate not found.");
                return new List<Estimate>();
            }
		}

		/// <summary>
        /// Returns a Estimate based on the Id string.
        /// </summary>
        /// <param name="context">PlatformSessionContext object with session info filled</param>
        /// <param name="realmId">Users RealmID</param>
        /// <param name="estimateIdToFind">Estimate Id</param>
        /// <returns>Estimate object with specified id</returns>
		public Estimate FindById(PlatformSessionContext context, string realmId, IdType estimateIdToFind)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmId: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			Estimate estimateFound = null;
				
			switch (context.ServiceType)
			{
				case IntuitServicesType.QBD:
					Estimates estimates = (Estimates)base.FindById(context, realmId, estimateIdToFind, IDSResource.estimate, typeof(Estimates));
					if (estimates.Estimate == null || estimates.Estimate.Length == 0)
					{
						Logger.WriteToLog(TraceLevel.Info,"Estimate not found.");
						return null;
					}
					estimateFound = estimates.Estimate[0];
					break;
				case IntuitServicesType.QBO:
					estimateFound = (Estimate)base.FindById(context, realmId, estimateIdToFind, IDSResource.estimate, typeof(Estimate));
					break;
			}
            return estimateFound;
		}
		
		/// <summary>
        /// Query on Estimate object basis on the search criteria and company Id.
        /// </summary>
        /// <param name="context">Session information</param>
        /// <param name="realmId">Company, for which search query to be executed.</param>        
        /// <param name="searchQuery">Criteria for the search
		///</param>
        /// <returns>Returns Search result.</returns>
		
		public List<Estimate> GetEstimates(PlatformSessionContext context, string realmId, QBQBOEstimateQuery searchQuery)
        {
			Logger.WriteToLog(TraceLevel.Info,"Realm Id: " +realmId);
			
			base.SetServiceTypeProperty(realmId,ref context);
			
			Estimates searchEstimates = null;

						IDSOperationContext operationContext = new IDSOperationContext(IDSResource.estimate, realmId);
			if (context.ServiceType == IntuitServicesType.QBO)
            {
                operationContext = new IDSOperationContext(IDSResource.estimates, realmId);
            }
           
			if(searchQuery != null)
			{
            	searchEstimates = (Estimates)base.GetResourcesForQuery(context,operationContext,searchQuery);
			}
			else
			{
				searchEstimates = (Estimates)base.GetResources(context, operationContext, typeof(Estimates));
			}
                    
            if (searchEstimates == null || searchEstimates.Estimate == null)
            {
				Logger.WriteToLog(TraceLevel.Info, "Estimate not found.");
                return null;
            }
            return new List<Estimate>(searchEstimates.Estimate);
        }

		/// <summary>
		/// Updates a Estimate under the specified realm. 
		/// </summary>
		/// <param name="context">PlatformSessionContext object with session info filled</param>
		/// <param name="realmId">Users RealmID</param>
		/// <param name="newEstimate">Estimate object to Update</param>
		/// <returns>Returns an updated version of the Estimate with updated IdType and sync token.</returns>
		public Estimate UpdateEstimate(PlatformSessionContext context, string realmId, Estimate newEstimate)
		{
			base.SetServiceTypeProperty(realmId,ref context);
			newEstimate = (Estimate)base.UpdateResource(context, realmId, newEstimate, IDSResource.estimate);
			return newEstimate;
		}

		/// <summary>
		/// Deletes a Estimate under the specified realm. 
		/// </summary>
		/// <param name="context">PlatformSessionContext object with session info filled</param>
		/// <param name="realmId">Users RealmID</param>
		/// <param name="newEstimate">Estimate object to Delete</param>
		public void DeleteEstimate(PlatformSessionContext context, string realmId, Estimate newEstimate)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmID: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			base.DeleteResource(context, realmId, newEstimate, IDSResource.estimate);
		}

		/// <summary>
		/// Reverts a Estimate under the specified realm. 
		/// </summary>
		/// <param name="context">PlatformSessionContext object with session info filled</param>
		/// <param name="realmId">Users RealmID</param>
		/// <param name="newEstimate">Estimate object to Revert</param>
		/// <returns>Returns an updated version of the Estimate with updated IdType and sync token.</returns>
		#warning 'Estimate Revert operation is supported by QB'
		public Estimate RevertEstimate(PlatformSessionContext context, string realmId, Estimate newEstimate)
		{
			base.SetServiceTypeProperty(realmId,ref context);
			newEstimate = (Estimate)base.RevertResource(context, realmId, newEstimate, IDSResource.estimate);
			return newEstimate;
		}

	}
}

