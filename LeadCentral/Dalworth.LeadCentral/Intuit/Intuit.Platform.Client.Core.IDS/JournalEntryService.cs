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
	/// Provides Method to perform CRUD operations on JournalEntry Resource of QuickBooks
	/// </summary>
	public class JournalEntryService : IDSBaseService
	{

		/// <summary>
		/// Adds a JournalEntry under the specified realm. 
		/// </summary>
		/// <param name="context">PlatformSessionContext object with session info filled</param>
		/// <param name="realmId">Users RealmID</param>
		/// <param name="newJournalEntry">JournalEntry object to Add</param>
		/// <returns>Returns an updated version of the JournalEntry with updated IdType and sync token.</returns>
		public JournalEntry AddJournalEntry(PlatformSessionContext context, string realmId, JournalEntry newJournalEntry)
		{
			base.SetServiceTypeProperty(realmId,ref context);
			newJournalEntry = (JournalEntry)base.AddResource(context, realmId, newJournalEntry, IDSResource.journalentry);
			return newJournalEntry;
		}

		/// <summary>
        /// Returns a list of all JournalEntries under the specified Realm.
        /// </summary>
        /// <param name="context">PlatformSessionContext object with session info filled</param>
        /// <param name="realmId">Users RealmID</param>
        /// <returns>List of all JournalEntries</returns>
		public List<JournalEntry> FindAll(PlatformSessionContext context, string realmId)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmId: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			IDSResource resource = IDSResource.journalentry;
           
			JournalEntries listOfObjects = (JournalEntries)base.FindAll(context, realmId, resource,typeof(JournalEntries));
			if (listOfObjects != null && listOfObjects.JournalEntry != null)
            {
                return new List<JournalEntry>(listOfObjects.JournalEntry);
            }
            else
            {	
				Logger.WriteToLog(TraceLevel.Info,"JournalEntry not found.");
                return new List<JournalEntry>();
            }
		}

		/// <summary>
        /// Returns a JournalEntry based on the Id string.
        /// </summary>
        /// <param name="context">PlatformSessionContext object with session info filled</param>
        /// <param name="realmId">Users RealmID</param>
        /// <param name="journalentryIdToFind">JournalEntry Id</param>
        /// <returns>JournalEntry object with specified id</returns>
		public JournalEntry FindById(PlatformSessionContext context, string realmId, IdType journalentryIdToFind)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmId: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			JournalEntry journalentryFound = null;
			JournalEntries journalentries = (JournalEntries)base.FindById(context, realmId, journalentryIdToFind, IDSResource.journalentry, typeof(JournalEntries));
			if (journalentries.JournalEntry == null || journalentries.JournalEntry.Length == 0)
			{
				Logger.WriteToLog(TraceLevel.Info,"JournalEntry not found.");
				return null;
			}
			journalentryFound = journalentries.JournalEntry[0];
            return journalentryFound;
		}
		
		/// <summary>
        /// Query on JournalEntry object basis on the search criteria and company Id.
        /// </summary>
        /// <param name="context">Session information</param>
        /// <param name="realmId">Company, for which search query to be executed.</param>        
        /// <param name="searchQuery">Criteria for the search
		///</param>
        /// <returns>Returns Search result.</returns>
		
		public List<JournalEntry> GetJournalEntries(PlatformSessionContext context, string realmId, JournalEntryQuery searchQuery)
        {
			Logger.WriteToLog(TraceLevel.Info,"Realm Id: " +realmId);
			
			base.SetServiceTypeProperty(realmId,ref context);
			
			JournalEntries searchJournalEntries = null;

						IDSOperationContext operationContext = new IDSOperationContext(IDSResource.journalentry,realmId);
			
			if(searchQuery != null)
			{
            	searchJournalEntries = (JournalEntries)base.GetResourcesForQuery(context,operationContext,searchQuery);
			}
			else
			{
				searchJournalEntries = (JournalEntries)base.GetResources(context, operationContext, typeof(JournalEntries));
			}
                    
            if (searchJournalEntries == null || searchJournalEntries.JournalEntry == null)
            {
				Logger.WriteToLog(TraceLevel.Info, "JournalEntry not found.");
                return null;
            }
            return new List<JournalEntry>(searchJournalEntries.JournalEntry);
        }

		/// <summary>
		/// Deletes a JournalEntry under the specified realm. 
		/// </summary>
		/// <param name="context">PlatformSessionContext object with session info filled</param>
		/// <param name="realmId">Users RealmID</param>
		/// <param name="newJournalEntry">JournalEntry object to Delete</param>
		public void DeleteJournalEntry(PlatformSessionContext context, string realmId, JournalEntry newJournalEntry)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmID: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			base.DeleteResource(context, realmId, newJournalEntry, IDSResource.journalentry);
		}

	}
}

