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
	/// Provides Method to perform CRUD operations on Preferences Resource of QuickBooks
	/// </summary>
	public class PreferencesService : IDSBaseService
	{

		/// <summary>
        /// Returns a list of all CompanyPreferences under the specified Realm.
        /// </summary>
        /// <param name="context">PlatformSessionContext object with session info filled</param>
        /// <param name="realmId">Users RealmID</param>
        /// <returns>List of all CompanyPreferences</returns>
		public List<Preferences> FindAll(PlatformSessionContext context, string realmId)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmId: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			IDSResource resource = IDSResource.preferences;
           
			CompanyPreferences listOfObjects = (CompanyPreferences)base.FindAll(context, realmId, resource,typeof(CompanyPreferences));
			if (listOfObjects != null && listOfObjects.Preferences != null)
            {
				List<Preferences> PreferencesList = new List<Preferences>();
				PreferencesList.Add(listOfObjects.Preferences);
				return PreferencesList;
            }
            else
            {	
				Logger.WriteToLog(TraceLevel.Info,"Preferences not found.");
                return new List<Preferences>();
            }
		}

	}
}

