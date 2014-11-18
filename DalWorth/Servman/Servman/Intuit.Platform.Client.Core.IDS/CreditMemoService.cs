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
	/// Provides Method to perform CRUD operations on CreditMemo Resource of QuickBooks
	/// </summary>
	public class CreditMemoService : IDSBaseService
	{

		/// <summary>
		/// Adds a CreditMemo under the specified realm. 
		/// </summary>
		/// <param name="context">PlatformSessionContext object with session info filled</param>
		/// <param name="realmId">Users RealmID</param>
		/// <param name="newCreditMemo">CreditMemo object to Add</param>
		/// <returns>Returns an updated version of the CreditMemo with updated IdType and sync token.</returns>
		public CreditMemo AddCreditMemo(PlatformSessionContext context, string realmId, CreditMemo newCreditMemo)
		{
			base.SetServiceTypeProperty(realmId,ref context);
			newCreditMemo = (CreditMemo)base.AddResource(context, realmId, newCreditMemo, IDSResource.creditmemo);
			return newCreditMemo;
		}

		/// <summary>
        /// Returns a list of all CreditMemos under the specified Realm.
        /// </summary>
        /// <param name="context">PlatformSessionContext object with session info filled</param>
        /// <param name="realmId">Users RealmID</param>
        /// <returns>List of all CreditMemos</returns>
		public List<CreditMemo> FindAll(PlatformSessionContext context, string realmId)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmId: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			IDSResource resource = IDSResource.creditmemo;
           
			CreditMemos listOfObjects = (CreditMemos)base.FindAll(context, realmId, resource,typeof(CreditMemos));
			if (listOfObjects != null && listOfObjects.CreditMemo != null)
            {
                return new List<CreditMemo>(listOfObjects.CreditMemo);
            }
            else
            {	
				Logger.WriteToLog(TraceLevel.Info,"CreditMemo not found.");
                return new List<CreditMemo>();
            }
		}

		/// <summary>
        /// Returns a CreditMemo based on the Id string.
        /// </summary>
        /// <param name="context">PlatformSessionContext object with session info filled</param>
        /// <param name="realmId">Users RealmID</param>
        /// <param name="creditmemoIdToFind">CreditMemo Id</param>
        /// <returns>CreditMemo object with specified id</returns>
		public CreditMemo FindById(PlatformSessionContext context, string realmId, IdType creditmemoIdToFind)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmId: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			CreditMemo creditmemoFound = null;
			CreditMemos creditmemos = (CreditMemos)base.FindById(context, realmId, creditmemoIdToFind, IDSResource.creditmemo, typeof(CreditMemos));
			if (creditmemos.CreditMemo == null || creditmemos.CreditMemo.Length == 0)
			{
				Logger.WriteToLog(TraceLevel.Info,"CreditMemo not found.");
				return null;
			}
			creditmemoFound = creditmemos.CreditMemo[0];
            return creditmemoFound;
		}
		
		/// <summary>
        /// Query on CreditMemo object basis on the search criteria and company Id.
        /// </summary>
        /// <param name="context">Session information</param>
        /// <param name="realmId">Company, for which search query to be executed.</param>        
        /// <param name="searchQuery">Criteria for the search
		///</param>
        /// <returns>Returns Search result.</returns>
		
		public List<CreditMemo> GetCreditMemos(PlatformSessionContext context, string realmId, CreditMemoQuery searchQuery)
        {
			Logger.WriteToLog(TraceLevel.Info,"Realm Id: " +realmId);
			
			base.SetServiceTypeProperty(realmId,ref context);
			
			CreditMemos searchCreditMemos = null;

						IDSOperationContext operationContext = new IDSOperationContext(IDSResource.creditmemo,realmId);
			
			if(searchQuery != null)
			{
            	searchCreditMemos = (CreditMemos)base.GetResourcesForQuery(context,operationContext,searchQuery);
			}
			else
			{
				searchCreditMemos = (CreditMemos)base.GetResources(context, operationContext, typeof(CreditMemos));
			}
                    
            if (searchCreditMemos == null || searchCreditMemos.CreditMemo == null)
            {
				Logger.WriteToLog(TraceLevel.Info, "CreditMemo not found.");
                return null;
            }
            return new List<CreditMemo>(searchCreditMemos.CreditMemo);
        }

		/// <summary>
		/// Deletes a CreditMemo under the specified realm. 
		/// </summary>
		/// <param name="context">PlatformSessionContext object with session info filled</param>
		/// <param name="realmId">Users RealmID</param>
		/// <param name="newCreditMemo">CreditMemo object to Delete</param>
		public void DeleteCreditMemo(PlatformSessionContext context, string realmId, CreditMemo newCreditMemo)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmID: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			base.DeleteResource(context, realmId, newCreditMemo, IDSResource.creditmemo);
		}

	}
}

