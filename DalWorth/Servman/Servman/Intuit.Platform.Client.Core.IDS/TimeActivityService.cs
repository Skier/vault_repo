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
	/// Provides Method to perform CRUD operations on TimeActivity Resource of QuickBooks
	/// </summary>
	public class TimeActivityService : IDSBaseService
	{

		/// <summary>
		/// Adds a TimeActivity under the specified realm. 
		/// </summary>
		/// <param name="context">PlatformSessionContext object with session info filled</param>
		/// <param name="realmId">Users RealmID</param>
		/// <param name="newTimeActivity">TimeActivity object to Add</param>
		/// <returns>Returns an updated version of the TimeActivity with updated IdType and sync token.</returns>
		public TimeActivity AddTimeActivity(PlatformSessionContext context, string realmId, TimeActivity newTimeActivity)
		{
			base.SetServiceTypeProperty(realmId,ref context);
			newTimeActivity = (TimeActivity)base.AddResource(context, realmId, newTimeActivity, IDSResource.timeactivity);
			return newTimeActivity;
		}

		/// <summary>
        /// Returns a list of all TimeActivities under the specified Realm.
        /// </summary>
        /// <param name="context">PlatformSessionContext object with session info filled</param>
        /// <param name="realmId">Users RealmID</param>
        /// <returns>List of all TimeActivities</returns>
		public List<TimeActivity> FindAll(PlatformSessionContext context, string realmId)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmId: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			IDSResource resource = IDSResource.timeactivity;
           
			TimeActivities listOfObjects = (TimeActivities)base.FindAll(context, realmId, resource,typeof(TimeActivities));
			if (listOfObjects != null && listOfObjects.TimeActivity != null)
            {
                return new List<TimeActivity>(listOfObjects.TimeActivity);
            }
            else
            {	
				Logger.WriteToLog(TraceLevel.Info,"TimeActivity not found.");
                return new List<TimeActivity>();
            }
		}

		/// <summary>
        /// Returns a TimeActivity based on the Id string.
        /// </summary>
        /// <param name="context">PlatformSessionContext object with session info filled</param>
        /// <param name="realmId">Users RealmID</param>
        /// <param name="timeactivityIdToFind">TimeActivity Id</param>
        /// <returns>TimeActivity object with specified id</returns>
		public TimeActivity FindById(PlatformSessionContext context, string realmId, IdType timeactivityIdToFind)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmId: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			TimeActivity timeactivityFound = null;
			TimeActivities timeactivities = (TimeActivities)base.FindById(context, realmId, timeactivityIdToFind, IDSResource.timeactivity, typeof(TimeActivities));
			if (timeactivities.TimeActivity == null || timeactivities.TimeActivity.Length == 0)
			{
				Logger.WriteToLog(TraceLevel.Info,"TimeActivity not found.");
				return null;
			}
			timeactivityFound = timeactivities.TimeActivity[0];
            return timeactivityFound;
		}
		
		/// <summary>
        /// Query on TimeActivity object basis on the search criteria and company Id.
        /// </summary>
        /// <param name="context">Session information</param>
        /// <param name="realmId">Company, for which search query to be executed.</param>        
        /// <param name="searchQuery">Criteria for the search
		///</param>
        /// <returns>Returns Search result.</returns>
		
		public List<TimeActivity> GetTimeActivities(PlatformSessionContext context, string realmId, TimeActivityQuery searchQuery)
        {
			Logger.WriteToLog(TraceLevel.Info,"Realm Id: " +realmId);
			
			base.SetServiceTypeProperty(realmId,ref context);
			
			TimeActivities searchTimeActivities = null;

						IDSOperationContext operationContext = new IDSOperationContext(IDSResource.timeactivity,realmId);
			
			if(searchQuery != null)
			{
            	searchTimeActivities = (TimeActivities)base.GetResourcesForQuery(context,operationContext,searchQuery);
			}
			else
			{
				searchTimeActivities = (TimeActivities)base.GetResources(context, operationContext, typeof(TimeActivities));
			}
                    
            if (searchTimeActivities == null || searchTimeActivities.TimeActivity == null)
            {
				Logger.WriteToLog(TraceLevel.Info, "TimeActivity not found.");
                return null;
            }
            return new List<TimeActivity>(searchTimeActivities.TimeActivity);
        }

		/// <summary>
		/// Updates a TimeActivity under the specified realm. 
		/// </summary>
		/// <param name="context">PlatformSessionContext object with session info filled</param>
		/// <param name="realmId">Users RealmID</param>
		/// <param name="newTimeActivity">TimeActivity object to Update</param>
		/// <returns>Returns an updated version of the TimeActivity with updated IdType and sync token.</returns>
		public TimeActivity UpdateTimeActivity(PlatformSessionContext context, string realmId, TimeActivity newTimeActivity)
		{
			base.SetServiceTypeProperty(realmId,ref context);
			newTimeActivity = (TimeActivity)base.UpdateResource(context, realmId, newTimeActivity, IDSResource.timeactivity);
			return newTimeActivity;
		}

		/// <summary>
		/// Deletes a TimeActivity under the specified realm. 
		/// </summary>
		/// <param name="context">PlatformSessionContext object with session info filled</param>
		/// <param name="realmId">Users RealmID</param>
		/// <param name="newTimeActivity">TimeActivity object to Delete</param>
		public void DeleteTimeActivity(PlatformSessionContext context, string realmId, TimeActivity newTimeActivity)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmID: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			base.DeleteResource(context, realmId, newTimeActivity, IDSResource.timeactivity);
		}

		/// <summary>
		/// Reverts a TimeActivity under the specified realm. 
		/// </summary>
		/// <param name="context">PlatformSessionContext object with session info filled</param>
		/// <param name="realmId">Users RealmID</param>
		/// <param name="newTimeActivity">TimeActivity object to Revert</param>
		/// <returns>Returns an updated version of the TimeActivity with updated IdType and sync token.</returns>
		public TimeActivity RevertTimeActivity(PlatformSessionContext context, string realmId, TimeActivity newTimeActivity)
		{
			base.SetServiceTypeProperty(realmId,ref context);
			newTimeActivity = (TimeActivity)base.RevertResource(context, realmId, newTimeActivity, IDSResource.timeactivity);
			return newTimeActivity;
		}

	}
}

