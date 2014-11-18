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
	/// Provides Method to perform CRUD operations on Job Resource of QuickBooks
	/// </summary>
	public class JobService : IDSBaseService
	{

		/// <summary>
		/// Adds a Job under the specified realm. 
		/// </summary>
		/// <param name="context">PlatformSessionContext object with session info filled</param>
		/// <param name="realmId">Users RealmID</param>
		/// <param name="newJob">Job object to Add</param>
		/// <returns>Returns an updated version of the Job with updated IdType and sync token.</returns>
		public Job AddJob(PlatformSessionContext context, string realmId, Job newJob)
		{
			base.SetServiceTypeProperty(realmId,ref context);
			newJob = (Job)base.AddResource(context, realmId, newJob, IDSResource.job);
			return newJob;
		}

		/// <summary>
        /// Returns a list of all Jobs under the specified Realm.
        /// </summary>
        /// <param name="context">PlatformSessionContext object with session info filled</param>
        /// <param name="realmId">Users RealmID</param>
        /// <returns>List of all Jobs</returns>
		public List<Job> FindAll(PlatformSessionContext context, string realmId)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmId: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			IDSResource resource = IDSResource.job;
           
			Jobs listOfObjects = (Jobs)base.FindAll(context, realmId, resource,typeof(Jobs));
			if (listOfObjects != null && listOfObjects.Job != null)
            {
                return new List<Job>(listOfObjects.Job);
            }
            else
            {	
				Logger.WriteToLog(TraceLevel.Info,"Job not found.");
                return new List<Job>();
            }
		}

		/// <summary>
        /// Returns a Job based on the Id string.
        /// </summary>
        /// <param name="context">PlatformSessionContext object with session info filled</param>
        /// <param name="realmId">Users RealmID</param>
        /// <param name="jobIdToFind">Job Id</param>
        /// <returns>Job object with specified id</returns>
		public Job FindById(PlatformSessionContext context, string realmId, IdType jobIdToFind)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmId: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			Job jobFound = null;
			Jobs jobs = (Jobs)base.FindById(context, realmId, jobIdToFind, IDSResource.job, typeof(Jobs));
			if (jobs.Job == null || jobs.Job.Length == 0)
			{
				Logger.WriteToLog(TraceLevel.Info,"Job not found.");
				return null;
			}
			jobFound = jobs.Job[0];
            return jobFound;
		}
		
		/// <summary>
        /// Query on Job object basis on the search criteria and company Id.
        /// </summary>
        /// <param name="context">Session information</param>
        /// <param name="realmId">Company, for which search query to be executed.</param>        
        /// <param name="searchQuery">Criteria for the search
		///</param>
        /// <returns>Returns Search result.</returns>
		
		public List<Job> GetJobs(PlatformSessionContext context, string realmId, JobQuery searchQuery)
        {
			Logger.WriteToLog(TraceLevel.Info,"Realm Id: " +realmId);
			
			base.SetServiceTypeProperty(realmId,ref context);
			
			Jobs searchJobs = null;

						IDSOperationContext operationContext = new IDSOperationContext(IDSResource.job,realmId);
			
			if(searchQuery != null)
			{
            	searchJobs = (Jobs)base.GetResourcesForQuery(context,operationContext,searchQuery);
			}
			else
			{
				searchJobs = (Jobs)base.GetResources(context, operationContext, typeof(Jobs));
			}
                    
            if (searchJobs == null || searchJobs.Job == null)
            {
				Logger.WriteToLog(TraceLevel.Info, "Job not found.");
                return null;
            }
            return new List<Job>(searchJobs.Job);
        }

		/// <summary>
		/// Updates a Job under the specified realm. 
		/// </summary>
		/// <param name="context">PlatformSessionContext object with session info filled</param>
		/// <param name="realmId">Users RealmID</param>
		/// <param name="newJob">Job object to Update</param>
		/// <returns>Returns an updated version of the Job with updated IdType and sync token.</returns>
		public Job UpdateJob(PlatformSessionContext context, string realmId, Job newJob)
		{
			base.SetServiceTypeProperty(realmId,ref context);
			newJob = (Job)base.UpdateResource(context, realmId, newJob, IDSResource.job);
			return newJob;
		}

		/// <summary>
		/// Deletes a Job under the specified realm. 
		/// </summary>
		/// <param name="context">PlatformSessionContext object with session info filled</param>
		/// <param name="realmId">Users RealmID</param>
		/// <param name="newJob">Job object to Delete</param>
		public void DeleteJob(PlatformSessionContext context, string realmId, Job newJob)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmID: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			base.DeleteResource(context, realmId, newJob, IDSResource.job);
		}

		/// <summary>
		/// Reverts a Job under the specified realm. 
		/// </summary>
		/// <param name="context">PlatformSessionContext object with session info filled</param>
		/// <param name="realmId">Users RealmID</param>
		/// <param name="newJob">Job object to Revert</param>
		/// <returns>Returns an updated version of the Job with updated IdType and sync token.</returns>
		public Job RevertJob(PlatformSessionContext context, string realmId, Job newJob)
		{
			base.SetServiceTypeProperty(realmId,ref context);
			newJob = (Job)base.RevertResource(context, realmId, newJob, IDSResource.job);
			return newJob;
		}

	}
}

