
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
using Intuit.Platform.Client.Core.IDS;

namespace Intuit.Platform.Client.Core.IDS
{
    /// <summary>
    /// A list of Domain entities supported by IDS.  Used by the REST infrastructure
    /// to construct the relevant calls.
    /// </summary>
    public enum IDSResource
    {
		/// <summary>
        /// user Resource
        /// </summary>
        user,
		/// <summary>
        /// account Resource
        /// </summary>
        account,
		/// <summary>
        /// accounts Resource
        /// </summary>
        accounts,
		/// <summary>
        /// bill Resource
        /// </summary>
        bill,
		/// <summary>
        /// bills Resource
        /// </summary>
        bills,
		/// <summary>
        /// billpayment Resource
        /// </summary>
        billpayment,
		/// <summary>
        /// billpayments Resource
        /// </summary>
        billpayments,
		/// <summary>
        /// check Resource
        /// </summary>
        check,
		/// <summary>
        /// checks Resource
        /// </summary>
        checks,
		/// <summary>
        /// creditcardcharge Resource
        /// </summary>
        creditcardcharge,
		/// <summary>
        /// creditcardcharges Resource
        /// </summary>
        creditcardcharges,
		/// <summary>
        /// customer Resource
        /// </summary>
        customer,
		/// <summary>
        /// customers Resource
        /// </summary>
        customers,
		/// <summary>
        /// employee Resource
        /// </summary>
        employee,
		/// <summary>
        /// employees Resource
        /// </summary>
        employees,
		/// <summary>
        /// estimate Resource
        /// </summary>
        estimate,
		/// <summary>
        /// estimates Resource
        /// </summary>
        estimates,
		/// <summary>
        /// invoice Resource
        /// </summary>
        invoice,
		/// <summary>
        /// invoices Resource
        /// </summary>
        invoices,
		/// <summary>
        /// item Resource
        /// </summary>
        item,
		/// <summary>
        /// items Resource
        /// </summary>
        items,
		/// <summary>
        /// payment Resource
        /// </summary>
        payment,
		/// <summary>
        /// payments Resource
        /// </summary>
        payments,
		/// <summary>
        /// paymentmethod Resource
        /// </summary>
        paymentmethod,
		/// <summary>
        /// paymentmethods Resource
        /// </summary>
        paymentmethods,
		/// <summary>
        /// salesterm Resource
        /// </summary>
        salesterm,
		/// <summary>
        /// salesterms Resource
        /// </summary>
        salesterms,
		/// <summary>
        /// salesreceipt Resource
        /// </summary>
        salesreceipt,
		/// <summary>
        /// salesreceipts Resource
        /// </summary>
        salesreceipts,
		/// <summary>
        /// vendor Resource
        /// </summary>
        vendor,
		/// <summary>
        /// vendors Resource
        /// </summary>
        vendors,
		/// <summary>
        /// cashpurchase Resource
        /// </summary>
        cashpurchase,
		/// <summary>
        /// cashpurchases Resource
        /// </summary>
        cashpurchases,
		/// <summary>
        /// billpaymentcreditcard Resource
        /// </summary>
        billpaymentcreditcard,
		/// <summary>
        /// bomcomponent Resource
        /// </summary>
        bomcomponent,
		/// <summary>
        /// charge Resource
        /// </summary>
        charge,
		/// <summary>
        /// class Resource
        /// </summary>
		Class,//cannot write Class enum member in all lower case as class is keyword of .net
		/// <summary>
        /// creditcardcredit Resource
        /// </summary>
        creditcardcredit,
		/// <summary>
        /// creditcardrefund Resource
        /// </summary>
        creditcardrefund,
		/// <summary>
        /// creditmemo Resource
        /// </summary>
        creditmemo,
		/// <summary>
        /// customermsg Resource
        /// </summary>
        customermsg,
		/// <summary>
        /// customertype Resource
        /// </summary>
        customertype,
		/// <summary>
        /// deposit Resource
        /// </summary>
        deposit,
		/// <summary>
        /// discount Resource
        /// </summary>
        discount,
		/// <summary>
        /// fixedasset Resource
        /// </summary>
        fixedasset,
		/// <summary>
        /// inventoryadjustment Resource
        /// </summary>
        inventoryadjustment,
		/// <summary>
        /// itemconsolidated Resource
        /// </summary>
        itemconsolidated,
		/// <summary>
        /// itemreceipt Resource
        /// </summary>
        itemreceipt,
		/// <summary>
        /// salestaxgroup Resource
        /// </summary>
        salestaxgroup,
		/// <summary>
        /// job Resource
        /// </summary>
        job,
		/// <summary>
        /// jobtype Resource
        /// </summary>
        jobtype,
		/// <summary>
        /// journalentry Resource
        /// </summary>
        journalentry,
		/// <summary>
        /// othername Resource
        /// </summary>
        othername,
		/// <summary>
        /// payrollitem Resource
        /// </summary>
        payrollitem,
		/// <summary>
        /// purchaseorder Resource
        /// </summary>
        purchaseorder,
		/// <summary>
        /// salesorder Resource
        /// </summary>
        salesorder,
		/// <summary>
        /// salesrep Resource
        /// </summary>
        salesrep,
		/// <summary>
        /// salestax Resource
        /// </summary>
        salestax,
		/// <summary>
        /// salestaxcode Resource
        /// </summary>
        salestaxcode,
		/// <summary>
        /// salestaxpaymentcheck Resource
        /// </summary>
        salestaxpaymentcheck,
		/// <summary>
        /// shipmethod Resource
        /// </summary>
        shipmethod,
		/// <summary>
        /// syncactivityrequest Resource
        /// </summary>
        syncactivityrequest,
		/// <summary>
        /// timeactivity Resource
        /// </summary>
        timeactivity,
		/// <summary>
        /// uom Resource
        /// </summary>
        uom,
		/// <summary>
        /// vendortype Resource
        /// </summary>
        vendortype,
		/// <summary>
        /// vendorcredit Resource
        /// </summary>
        vendorcredit,
		/// <summary>
        /// preferences Resource
        /// </summary>
        preferences,
		/// <summary>
        /// accountbalances Resource
        /// </summary>
        accountbalances,
		/// <summary>
        /// balancesheet Resource
        /// </summary>
        balancesheet,
		/// <summary>
        /// customerswhooweme Resource
        /// </summary>
        customerswhooweme,
		/// <summary>
        /// advancedreport Resource
        /// </summary>
        advancedreport,
		/// <summary>
        /// incomebreakdown Resource
        /// </summary>
        incomebreakdown,
		/// <summary>
        /// profitandloss Resource
        /// </summary>
        profitandloss,
		/// <summary>
        /// salessummary Resource
        /// </summary>
        salessummary,
		/// <summary>
        /// topcustomersbysales Resource
        /// </summary>
        topcustomersbysales,
		/// <summary>
        /// none Resource
        /// </summary>
        none,
		/// <summary>
        /// company Resource
        /// </summary>
        company,
	}
}
