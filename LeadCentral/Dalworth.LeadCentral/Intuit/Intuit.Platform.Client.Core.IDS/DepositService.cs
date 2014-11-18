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
	/// Provides Method to perform CRUD operations on Deposit Resource of QuickBooks
	/// </summary>
	public class DepositService : IDSBaseService
	{

		/// <summary>
        /// Returns a list of all Deposits under the specified Realm.
        /// </summary>
        /// <param name="context">PlatformSessionContext object with session info filled</param>
        /// <param name="realmId">Users RealmID</param>
        /// <returns>List of all Deposits</returns>
		public List<Deposit> FindAll(PlatformSessionContext context, string realmId)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmId: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			IDSResource resource = IDSResource.deposit;
           
			Deposits listOfObjects = (Deposits)base.FindAll(context, realmId, resource,typeof(Deposits));
			if (listOfObjects != null && listOfObjects.Deposit != null)
            {
                return new List<Deposit>(listOfObjects.Deposit);
            }
            else
            {	
				Logger.WriteToLog(TraceLevel.Info,"Deposit not found.");
                return new List<Deposit>();
            }
		}

		/// <summary>
        /// Returns a Deposit based on the Id string.
        /// </summary>
        /// <param name="context">PlatformSessionContext object with session info filled</param>
        /// <param name="realmId">Users RealmID</param>
        /// <param name="depositIdToFind">Deposit Id</param>
        /// <returns>Deposit object with specified id</returns>
		public Deposit FindById(PlatformSessionContext context, string realmId, IdType depositIdToFind)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmId: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			Deposit depositFound = null;
			Deposits deposits = (Deposits)base.FindById(context, realmId, depositIdToFind, IDSResource.deposit, typeof(Deposits));
			if (deposits.Deposit == null || deposits.Deposit.Length == 0)
			{
				Logger.WriteToLog(TraceLevel.Info,"Deposit not found.");
				return null;
			}
			depositFound = deposits.Deposit[0];
            return depositFound;
		}
		
		/// <summary>
        /// Query on Deposit object basis on the search criteria and company Id.
        /// </summary>
        /// <param name="context">Session information</param>
        /// <param name="realmId">Company, for which search query to be executed.</param>        
        /// <param name="searchQuery">Criteria for the search
		///</param>
        /// <returns>Returns Search result.</returns>
		
		public List<Deposit> GetDeposits(PlatformSessionContext context, string realmId, DepositQuery searchQuery)
        {
			Logger.WriteToLog(TraceLevel.Info,"Realm Id: " +realmId);
			
			base.SetServiceTypeProperty(realmId,ref context);
			
			Deposits searchDeposits = null;

						IDSOperationContext operationContext = new IDSOperationContext(IDSResource.deposit,realmId);
			
			if(searchQuery != null)
			{
            	searchDeposits = (Deposits)base.GetResourcesForQuery(context,operationContext,searchQuery);
			}
			else
			{
				searchDeposits = (Deposits)base.GetResources(context, operationContext, typeof(Deposits));
			}
                    
            if (searchDeposits == null || searchDeposits.Deposit == null)
            {
				Logger.WriteToLog(TraceLevel.Info, "Deposit not found.");
                return null;
            }
            return new List<Deposit>(searchDeposits.Deposit);
        }

	}
}

