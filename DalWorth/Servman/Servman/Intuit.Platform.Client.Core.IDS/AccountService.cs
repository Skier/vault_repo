 
 


 
 


 
 



 
 



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
	/// Provides Method to perform CRUD operations on Account Resource of QuickBooks
	/// </summary>
	public class AccountService : IDSBaseService
	{

		/// <summary>
		/// Adds a Account under the specified realm. 
		/// </summary>
		/// <param name="context">PlatformSessionContext object with session info filled</param>
		/// <param name="realmId">Users RealmID</param>
		/// <param name="newAccount">Account object to Add</param>
		/// <returns>Returns an updated version of the Account with updated IdType and sync token.</returns>
		public Account AddAccount(PlatformSessionContext context, string realmId, Account newAccount)
		{
			base.SetServiceTypeProperty(realmId,ref context);
			newAccount = (Account)base.AddResource(context, realmId, newAccount, IDSResource.account);
			return newAccount;
		}

		/// <summary>
        /// Returns a list of all Accounts under the specified Realm.
        /// </summary>
        /// <param name="context">PlatformSessionContext object with session info filled</param>
        /// <param name="realmId">Users RealmID</param>
        /// <returns>List of all Accounts</returns>
		public List<Account> FindAll(PlatformSessionContext context, string realmId)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmId: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			IDSResource resource = IDSResource.account;
           
            if (context.ServiceType == IntuitServicesType.QBO)
            {
                resource = IDSResource.accounts;
            }
			Accounts listOfObjects = (Accounts)base.FindAll(context, realmId, resource,typeof(Accounts));
			if (listOfObjects != null && listOfObjects.Account != null)
            {
                return new List<Account>(listOfObjects.Account);
            }
            else
            {	
				Logger.WriteToLog(TraceLevel.Info,"Account not found.");
                return new List<Account>();
            }
		}

		/// <summary>
        /// Returns a Account based on the Id string.
        /// </summary>
        /// <param name="context">PlatformSessionContext object with session info filled</param>
        /// <param name="realmId">Users RealmID</param>
        /// <param name="accountIdToFind">Account Id</param>
        /// <returns>Account object with specified id</returns>
		public Account FindById(PlatformSessionContext context, string realmId, IdType accountIdToFind)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmId: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			Account accountFound = null;
				
			switch (context.ServiceType)
			{
				case IntuitServicesType.QBD:
					Accounts accounts = (Accounts)base.FindById(context, realmId, accountIdToFind, IDSResource.account, typeof(Accounts));
					if (accounts.Account == null || accounts.Account.Length == 0)
					{
						Logger.WriteToLog(TraceLevel.Info,"Account not found.");
						return null;
					}
					accountFound = accounts.Account[0];
					break;
				case IntuitServicesType.QBO:
					accountFound = (Account)base.FindById(context, realmId, accountIdToFind, IDSResource.account, typeof(Account));
					break;
			}
            return accountFound;
		}
		
		/// <summary>
        /// Query on Account object basis on the search criteria and company Id.
        /// </summary>
        /// <param name="context">Session information</param>
        /// <param name="realmId">Company, for which search query to be executed.</param>        
        /// <param name="searchQuery">Criteria for the search
		///</param>
        /// <returns>Returns Search result.</returns>
		
		public List<Account> GetAccounts(PlatformSessionContext context, string realmId, QBQBOAccountQuery searchQuery)
        {
			Logger.WriteToLog(TraceLevel.Info,"Realm Id: " +realmId);
			
			base.SetServiceTypeProperty(realmId,ref context);
			
			Accounts searchAccounts = null;

						IDSOperationContext operationContext = new IDSOperationContext(IDSResource.account, realmId);
			if (context.ServiceType == IntuitServicesType.QBO)
            {
                operationContext = new IDSOperationContext(IDSResource.accounts, realmId);
            }
           
			if(searchQuery != null)
			{
            	searchAccounts = (Accounts)base.GetResourcesForQuery(context,operationContext,searchQuery);
			}
			else
			{
				searchAccounts = (Accounts)base.GetResources(context, operationContext, typeof(Accounts));
			}
                    
            if (searchAccounts == null || searchAccounts.Account == null)
            {
				Logger.WriteToLog(TraceLevel.Info, "Account not found.");
                return null;
            }
            return new List<Account>(searchAccounts.Account);
        }

		/// <summary>
		/// Updates a Account under the specified realm. 
		/// </summary>
		/// <param name="context">PlatformSessionContext object with session info filled</param>
		/// <param name="realmId">Users RealmID</param>
		/// <param name="newAccount">Account object to Update</param>
		/// <returns>Returns an updated version of the Account with updated IdType and sync token.</returns>
		public Account UpdateAccount(PlatformSessionContext context, string realmId, Account newAccount)
		{
			base.SetServiceTypeProperty(realmId,ref context);
			newAccount = (Account)base.UpdateResource(context, realmId, newAccount, IDSResource.account);
			return newAccount;
		}

		/// <summary>
		/// Deletes a Account under the specified realm. 
		/// </summary>
		/// <param name="context">PlatformSessionContext object with session info filled</param>
		/// <param name="realmId">Users RealmID</param>
		/// <param name="newAccount">Account object to Delete</param>
		public void DeleteAccount(PlatformSessionContext context, string realmId, Account newAccount)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmID: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			base.DeleteResource(context, realmId, newAccount, IDSResource.account);
		}

		/// <summary>
		/// Reverts a Account under the specified realm. 
		/// </summary>
		/// <param name="context">PlatformSessionContext object with session info filled</param>
		/// <param name="realmId">Users RealmID</param>
		/// <param name="newAccount">Account object to Revert</param>
		/// <returns>Returns an updated version of the Account with updated IdType and sync token.</returns>
		#warning 'Account Revert operation is supported by QB'
		public Account RevertAccount(PlatformSessionContext context, string realmId, Account newAccount)
		{
			base.SetServiceTypeProperty(realmId,ref context);
			newAccount = (Account)base.RevertResource(context, realmId, newAccount, IDSResource.account);
			return newAccount;
		}

	}
}

