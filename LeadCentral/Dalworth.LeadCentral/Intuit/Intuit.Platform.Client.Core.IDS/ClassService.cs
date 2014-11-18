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
	/// Provides Method to perform CRUD operations on Class Resource of QuickBooks
	/// </summary>
	public class ClassService : IDSBaseService
	{

		/// <summary>
		/// Adds a Class under the specified realm. 
		/// </summary>
		/// <param name="context">PlatformSessionContext object with session info filled</param>
		/// <param name="realmId">Users RealmID</param>
		/// <param name="newClass">Class object to Add</param>
		/// <returns>Returns an updated version of the Class with updated IdType and sync token.</returns>
		public Class AddClass(PlatformSessionContext context, string realmId, Class newClass)
		{
			base.SetServiceTypeProperty(realmId,ref context);
			newClass = (Class)base.AddResource(context, realmId, newClass, IDSResource.Class);
			return newClass;
		}

		/// <summary>
        /// Returns a list of all Classes under the specified Realm.
        /// </summary>
        /// <param name="context">PlatformSessionContext object with session info filled</param>
        /// <param name="realmId">Users RealmID</param>
        /// <returns>List of all Classes</returns>
		public List<Class> FindAll(PlatformSessionContext context, string realmId)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmId: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			IDSResource resource = IDSResource.Class;
           
			Classes listOfObjects = (Classes)base.FindAll(context, realmId, resource,typeof(Classes));
			if (listOfObjects != null && listOfObjects.Class != null)
            {
                return new List<Class>(listOfObjects.Class);
            }
            else
            {	
				Logger.WriteToLog(TraceLevel.Info,"Class not found.");
                return new List<Class>();
            }
		}

		/// <summary>
        /// Returns a Class based on the Id string.
        /// </summary>
        /// <param name="context">PlatformSessionContext object with session info filled</param>
        /// <param name="realmId">Users RealmID</param>
        /// <param name="classIdToFind">Class Id</param>
        /// <returns>Class object with specified id</returns>
		public Class FindById(PlatformSessionContext context, string realmId, IdType classIdToFind)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmId: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			Class classFound = null;
			Classes classes = (Classes)base.FindById(context, realmId, classIdToFind, IDSResource.Class, typeof(Classes));
			if (classes.Class == null || classes.Class.Length == 0)
			{
				Logger.WriteToLog(TraceLevel.Info,"Class not found.");
				return null;
			}
			classFound = classes.Class[0];
            return classFound;
		}
		
		/// <summary>
        /// Query on Class object basis on the search criteria and company Id.
        /// </summary>
        /// <param name="context">Session information</param>
        /// <param name="realmId">Company, for which search query to be executed.</param>        
        /// <param name="searchQuery">Criteria for the search
		///</param>
        /// <returns>Returns Search result.</returns>
		
		public List<Class> GetClasses(PlatformSessionContext context, string realmId, ClassQuery searchQuery)
        {
			Logger.WriteToLog(TraceLevel.Info,"Realm Id: " +realmId);
			
			base.SetServiceTypeProperty(realmId,ref context);
			
			Classes searchClasses = null;

						IDSOperationContext operationContext = new IDSOperationContext(IDSResource.Class,realmId);
			
			if(searchQuery != null)
			{
            	searchClasses = (Classes)base.GetResourcesForQuery(context,operationContext,searchQuery);
			}
			else
			{
				searchClasses = (Classes)base.GetResources(context, operationContext, typeof(Classes));
			}
                    
            if (searchClasses == null || searchClasses.Class == null)
            {
				Logger.WriteToLog(TraceLevel.Info, "Class not found.");
                return null;
            }
            return new List<Class>(searchClasses.Class);
        }

		/// <summary>
		/// Deletes a Class under the specified realm. 
		/// </summary>
		/// <param name="context">PlatformSessionContext object with session info filled</param>
		/// <param name="realmId">Users RealmID</param>
		/// <param name="newClass">Class object to Delete</param>
		public void DeleteClass(PlatformSessionContext context, string realmId, Class newClass)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmID: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			base.DeleteResource(context, realmId, newClass, IDSResource.Class);
		}

	}
}

