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
	/// Provides Method to perform CRUD operations on Charge Resource of QuickBooks
	/// </summary>
	public class ChargeService : IDSBaseService
	{

		/// <summary>
        /// Returns a list of all Charges under the specified Realm.
        /// </summary>
        /// <param name="context">PlatformSessionContext object with session info filled</param>
        /// <param name="realmId">Users RealmID</param>
        /// <returns>List of all Charges</returns>
		public List<Charge> FindAll(PlatformSessionContext context, string realmId)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmId: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			IDSResource resource = IDSResource.charge;
           
			Charges listOfObjects = (Charges)base.FindAll(context, realmId, resource,typeof(Charges));
			if (listOfObjects != null && listOfObjects.Charge != null)
            {
                return new List<Charge>(listOfObjects.Charge);
            }
            else
            {	
				Logger.WriteToLog(TraceLevel.Info,"Charge not found.");
                return new List<Charge>();
            }
		}

		/// <summary>
        /// Returns a Charge based on the Id string.
        /// </summary>
        /// <param name="context">PlatformSessionContext object with session info filled</param>
        /// <param name="realmId">Users RealmID</param>
        /// <param name="chargeIdToFind">Charge Id</param>
        /// <returns>Charge object with specified id</returns>
		public Charge FindById(PlatformSessionContext context, string realmId, IdType chargeIdToFind)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmId: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			Charge chargeFound = null;
			Charges charges = (Charges)base.FindById(context, realmId, chargeIdToFind, IDSResource.charge, typeof(Charges));
			if (charges.Charge == null || charges.Charge.Length == 0)
			{
				Logger.WriteToLog(TraceLevel.Info,"Charge not found.");
				return null;
			}
			chargeFound = charges.Charge[0];
            return chargeFound;
		}
		
		/// <summary>
        /// Query on Charge object basis on the search criteria and company Id.
        /// </summary>
        /// <param name="context">Session information</param>
        /// <param name="realmId">Company, for which search query to be executed.</param>        
        /// <param name="searchQuery">Criteria for the search
		///</param>
        /// <returns>Returns Search result.</returns>
		
		public List<Charge> GetCharges(PlatformSessionContext context, string realmId, ChargeQuery searchQuery)
        {
			Logger.WriteToLog(TraceLevel.Info,"Realm Id: " +realmId);
			
			base.SetServiceTypeProperty(realmId,ref context);
			
			Charges searchCharges = null;

						IDSOperationContext operationContext = new IDSOperationContext(IDSResource.charge,realmId);
			
			if(searchQuery != null)
			{
            	searchCharges = (Charges)base.GetResourcesForQuery(context,operationContext,searchQuery);
			}
			else
			{
				searchCharges = (Charges)base.GetResources(context, operationContext, typeof(Charges));
			}
                    
            if (searchCharges == null || searchCharges.Charge == null)
            {
				Logger.WriteToLog(TraceLevel.Info, "Charge not found.");
                return null;
            }
            return new List<Charge>(searchCharges.Charge);
        }

	}
}

