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
	/// Provides Method to perform CRUD operations on JobType Resource of QuickBooks
	/// </summary>
	public class JobTypeService : IDSBaseService
	{

		/// <summary>
        /// Returns a list of all JobTypes under the specified Realm.
        /// </summary>
        /// <param name="context">PlatformSessionContext object with session info filled</param>
        /// <param name="realmId">Users RealmID</param>
        /// <returns>List of all JobTypes</returns>
		public List<JobType> FindAll(PlatformSessionContext context, string realmId)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmId: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			IDSResource resource = IDSResource.jobtype;
           
			JobTypes listOfObjects = (JobTypes)base.FindAll(context, realmId, resource,typeof(JobTypes));
			if (listOfObjects != null && listOfObjects.JobType != null)
            {
                return new List<JobType>(listOfObjects.JobType);
            }
            else
            {	
				Logger.WriteToLog(TraceLevel.Info,"JobType not found.");
                return new List<JobType>();
            }
		}

		/// <summary>
        /// Returns a JobType based on the Id string.
        /// </summary>
        /// <param name="context">PlatformSessionContext object with session info filled</param>
        /// <param name="realmId">Users RealmID</param>
        /// <param name="jobtypeIdToFind">JobType Id</param>
        /// <returns>JobType object with specified id</returns>
		public JobType FindById(PlatformSessionContext context, string realmId, IdType jobtypeIdToFind)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmId: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			JobType jobtypeFound = null;
			JobTypes jobtypes = (JobTypes)base.FindById(context, realmId, jobtypeIdToFind, IDSResource.jobtype, typeof(JobTypes));
			if (jobtypes.JobType == null || jobtypes.JobType.Length == 0)
			{
				Logger.WriteToLog(TraceLevel.Info,"JobType not found.");
				return null;
			}
			jobtypeFound = jobtypes.JobType[0];
            return jobtypeFound;
		}
		
		/// <summary>
        /// Query on JobType object basis on the search criteria and company Id.
        /// </summary>
        /// <param name="context">Session information</param>
        /// <param name="realmId">Company, for which search query to be executed.</param>        
        /// <param name="searchQuery">Criteria for the search
		///</param>
        /// <returns>Returns Search result.</returns>
		
		public List<JobType> GetJobTypes(PlatformSessionContext context, string realmId, JobTypeQuery searchQuery)
        {
			Logger.WriteToLog(TraceLevel.Info,"Realm Id: " +realmId);
			
			base.SetServiceTypeProperty(realmId,ref context);
			
			JobTypes searchJobTypes = null;

						IDSOperationContext operationContext = new IDSOperationContext(IDSResource.jobtype,realmId);
			
			if(searchQuery != null)
			{
            	searchJobTypes = (JobTypes)base.GetResourcesForQuery(context,operationContext,searchQuery);
			}
			else
			{
				searchJobTypes = (JobTypes)base.GetResources(context, operationContext, typeof(JobTypes));
			}
                    
            if (searchJobTypes == null || searchJobTypes.JobType == null)
            {
				Logger.WriteToLog(TraceLevel.Info, "JobType not found.");
                return null;
            }
            return new List<JobType>(searchJobTypes.JobType);
        }

	}
}

