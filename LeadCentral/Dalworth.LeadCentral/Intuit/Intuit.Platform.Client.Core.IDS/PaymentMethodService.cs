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
	/// Provides Method to perform CRUD operations on PaymentMethod Resource of QuickBooks
	/// </summary>
	public class PaymentMethodService : IDSBaseService
	{

		/// <summary>
		/// Adds a PaymentMethod under the specified realm. 
		/// </summary>
		/// <param name="context">PlatformSessionContext object with session info filled</param>
		/// <param name="realmId">Users RealmID</param>
		/// <param name="newPaymentMethod">PaymentMethod object to Add</param>
		/// <returns>Returns an updated version of the PaymentMethod with updated IdType and sync token.</returns>
		public PaymentMethod AddPaymentMethod(PlatformSessionContext context, string realmId, PaymentMethod newPaymentMethod)
		{
			base.SetServiceTypeProperty(realmId,ref context);
			newPaymentMethod = (PaymentMethod)base.AddResource(context, realmId, newPaymentMethod, IDSResource.paymentmethod);
			return newPaymentMethod;
		}

		/// <summary>
        /// Returns a list of all PaymentMethods under the specified Realm.
        /// </summary>
        /// <param name="context">PlatformSessionContext object with session info filled</param>
        /// <param name="realmId">Users RealmID</param>
        /// <returns>List of all PaymentMethods</returns>
		public List<PaymentMethod> FindAll(PlatformSessionContext context, string realmId)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmId: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			IDSResource resource = IDSResource.paymentmethod;
           
            if (context.ServiceType == IntuitServicesType.QBO)
            {
                resource = IDSResource.paymentmethods;
            }
			PaymentMethods listOfObjects = (PaymentMethods)base.FindAll(context, realmId, resource,typeof(PaymentMethods));
			if (listOfObjects != null && listOfObjects.PaymentMethod != null)
            {
                return new List<PaymentMethod>(listOfObjects.PaymentMethod);
            }
            else
            {	
				Logger.WriteToLog(TraceLevel.Info,"PaymentMethod not found.");
                return new List<PaymentMethod>();
            }
		}

		/// <summary>
        /// Returns a PaymentMethod based on the Id string.
        /// </summary>
        /// <param name="context">PlatformSessionContext object with session info filled</param>
        /// <param name="realmId">Users RealmID</param>
        /// <param name="paymentmethodIdToFind">PaymentMethod Id</param>
        /// <returns>PaymentMethod object with specified id</returns>
		public PaymentMethod FindById(PlatformSessionContext context, string realmId, IdType paymentmethodIdToFind)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmId: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			PaymentMethod paymentmethodFound = null;
				
			switch (context.ServiceType)
			{
				case IntuitServicesType.QBD:
					PaymentMethods paymentmethods = (PaymentMethods)base.FindById(context, realmId, paymentmethodIdToFind, IDSResource.paymentmethod, typeof(PaymentMethods));
					if (paymentmethods.PaymentMethod == null || paymentmethods.PaymentMethod.Length == 0)
					{
						Logger.WriteToLog(TraceLevel.Info,"PaymentMethod not found.");
						return null;
					}
					paymentmethodFound = paymentmethods.PaymentMethod[0];
					break;
				case IntuitServicesType.QBO:
					paymentmethodFound = (PaymentMethod)base.FindById(context, realmId, paymentmethodIdToFind, IDSResource.paymentmethod, typeof(PaymentMethod));
					break;
			}
            return paymentmethodFound;
		}
		
		/// <summary>
        /// Query on PaymentMethod object basis on the search criteria and company Id.
        /// </summary>
        /// <param name="context">Session information</param>
        /// <param name="realmId">Company, for which search query to be executed.</param>        
        /// <param name="searchQuery">Criteria for the search
		///</param>
        /// <returns>Returns Search result.</returns>
		
		public List<PaymentMethod> GetPaymentMethods(PlatformSessionContext context, string realmId, QBQBOPaymentMethodQuery searchQuery)
        {
			Logger.WriteToLog(TraceLevel.Info,"Realm Id: " +realmId);
			
			base.SetServiceTypeProperty(realmId,ref context);
			
			PaymentMethods searchPaymentMethods = null;

						IDSOperationContext operationContext = new IDSOperationContext(IDSResource.paymentmethod, realmId);
			if (context.ServiceType == IntuitServicesType.QBO)
            {
                operationContext = new IDSOperationContext(IDSResource.paymentmethods, realmId);
            }
           
			if(searchQuery != null)
			{
            	searchPaymentMethods = (PaymentMethods)base.GetResourcesForQuery(context,operationContext,searchQuery);
			}
			else
			{
				searchPaymentMethods = (PaymentMethods)base.GetResources(context, operationContext, typeof(PaymentMethods));
			}
                    
            if (searchPaymentMethods == null || searchPaymentMethods.PaymentMethod == null)
            {
				Logger.WriteToLog(TraceLevel.Info, "PaymentMethod not found.");
                return null;
            }
            return new List<PaymentMethod>(searchPaymentMethods.PaymentMethod);
        }

		/// <summary>
		/// Updates a PaymentMethod under the specified realm. 
		/// </summary>
		/// <param name="context">PlatformSessionContext object with session info filled</param>
		/// <param name="realmId">Users RealmID</param>
		/// <param name="newPaymentMethod">PaymentMethod object to Update</param>
		/// <returns>Returns an updated version of the PaymentMethod with updated IdType and sync token.</returns>
		#warning 'PaymentMethod Update operation is supported by QBO'
		public PaymentMethod UpdatePaymentMethod(PlatformSessionContext context, string realmId, PaymentMethod newPaymentMethod)
		{
			base.SetServiceTypeProperty(realmId,ref context);
			newPaymentMethod = (PaymentMethod)base.UpdateResource(context, realmId, newPaymentMethod, IDSResource.paymentmethod);
			return newPaymentMethod;
		}

		/// <summary>
		/// Deletes a PaymentMethod under the specified realm. 
		/// </summary>
		/// <param name="context">PlatformSessionContext object with session info filled</param>
		/// <param name="realmId">Users RealmID</param>
		/// <param name="newPaymentMethod">PaymentMethod object to Delete</param>
		public void DeletePaymentMethod(PlatformSessionContext context, string realmId, PaymentMethod newPaymentMethod)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmID: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			base.DeleteResource(context, realmId, newPaymentMethod, IDSResource.paymentmethod);
		}

	}
}

