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
	/// Provides Method to perform CRUD operations on Payment Resource of QuickBooks
	/// </summary>
	public class PaymentService : IDSBaseService
	{

		/// <summary>
		/// Adds a Payment under the specified realm. 
		/// </summary>
		/// <param name="context">PlatformSessionContext object with session info filled</param>
		/// <param name="realmId">Users RealmID</param>
		/// <param name="newPayment">Payment object to Add</param>
		/// <returns>Returns an updated version of the Payment with updated IdType and sync token.</returns>
		public Payment AddPayment(PlatformSessionContext context, string realmId, Payment newPayment)
		{
			base.SetServiceTypeProperty(realmId,ref context);
			newPayment = (Payment)base.AddResource(context, realmId, newPayment, IDSResource.payment);
			return newPayment;
		}

		/// <summary>
        /// Returns a list of all Payments under the specified Realm.
        /// </summary>
        /// <param name="context">PlatformSessionContext object with session info filled</param>
        /// <param name="realmId">Users RealmID</param>
        /// <returns>List of all Payments</returns>
		public List<Payment> FindAll(PlatformSessionContext context, string realmId)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmId: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			IDSResource resource = IDSResource.payment;
           
            if (context.ServiceType == IntuitServicesType.QBO)
            {
                resource = IDSResource.payments;
            }
			Payments listOfObjects = (Payments)base.FindAll(context, realmId, resource,typeof(Payments));
			if (listOfObjects != null && listOfObjects.Payment != null)
            {
                return new List<Payment>(listOfObjects.Payment);
            }
            else
            {	
				Logger.WriteToLog(TraceLevel.Info,"Payment not found.");
                return new List<Payment>();
            }
		}

		/// <summary>
        /// Returns a Payment based on the Id string.
        /// </summary>
        /// <param name="context">PlatformSessionContext object with session info filled</param>
        /// <param name="realmId">Users RealmID</param>
        /// <param name="paymentIdToFind">Payment Id</param>
        /// <returns>Payment object with specified id</returns>
		public Payment FindById(PlatformSessionContext context, string realmId, IdType paymentIdToFind)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmId: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			Payment paymentFound = null;
				
			switch (context.ServiceType)
			{
				case IntuitServicesType.QBD:
					Payments payments = (Payments)base.FindById(context, realmId, paymentIdToFind, IDSResource.payment, typeof(Payments));
					if (payments.Payment == null || payments.Payment.Length == 0)
					{
						Logger.WriteToLog(TraceLevel.Info,"Payment not found.");
						return null;
					}
					paymentFound = payments.Payment[0];
					break;
				case IntuitServicesType.QBO:
					paymentFound = (Payment)base.FindById(context, realmId, paymentIdToFind, IDSResource.payment, typeof(Payment));
					break;
			}
            return paymentFound;
		}
		
		/// <summary>
        /// Query on Payment object basis on the search criteria and company Id.
        /// </summary>
        /// <param name="context">Session information</param>
        /// <param name="realmId">Company, for which search query to be executed.</param>        
        /// <param name="searchQuery">Criteria for the search
		///</param>
        /// <returns>Returns Search result.</returns>
		
		public List<Payment> GetPayments(PlatformSessionContext context, string realmId, QBQBOPaymentQuery searchQuery)
        {
			Logger.WriteToLog(TraceLevel.Info,"Realm Id: " +realmId);
			
			base.SetServiceTypeProperty(realmId,ref context);
			
			Payments searchPayments = null;

						IDSOperationContext operationContext = new IDSOperationContext(IDSResource.payment, realmId);
			if (context.ServiceType == IntuitServicesType.QBO)
            {
                operationContext = new IDSOperationContext(IDSResource.payments, realmId);
            }
           
			if(searchQuery != null)
			{
            	searchPayments = (Payments)base.GetResourcesForQuery(context,operationContext,searchQuery);
			}
			else
			{
				searchPayments = (Payments)base.GetResources(context, operationContext, typeof(Payments));
			}
                    
            if (searchPayments == null || searchPayments.Payment == null)
            {
				Logger.WriteToLog(TraceLevel.Info, "Payment not found.");
                return null;
            }
            return new List<Payment>(searchPayments.Payment);
        }

		/// <summary>
		/// Updates a Payment under the specified realm. 
		/// </summary>
		/// <param name="context">PlatformSessionContext object with session info filled</param>
		/// <param name="realmId">Users RealmID</param>
		/// <param name="newPayment">Payment object to Update</param>
		/// <returns>Returns an updated version of the Payment with updated IdType and sync token.</returns>
		#warning 'Payment Update operation is supported by QBO'
		public Payment UpdatePayment(PlatformSessionContext context, string realmId, Payment newPayment)
		{
			base.SetServiceTypeProperty(realmId,ref context);
			newPayment = (Payment)base.UpdateResource(context, realmId, newPayment, IDSResource.payment);
			return newPayment;
		}

		/// <summary>
		/// Deletes a Payment under the specified realm. 
		/// </summary>
		/// <param name="context">PlatformSessionContext object with session info filled</param>
		/// <param name="realmId">Users RealmID</param>
		/// <param name="newPayment">Payment object to Delete</param>
		public void DeletePayment(PlatformSessionContext context, string realmId, Payment newPayment)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmID: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			base.DeleteResource(context, realmId, newPayment, IDSResource.payment);
		}

	}
}

