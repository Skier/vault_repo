using System;
using System.Collections;
using System.Web;
using Dpi.Central.Web.Controls;

namespace Dpi.Central.Web.Account.AccountSetup
{
    public class AccountSetupProcessMapProvider : IProcessMapProvider
    {
        private const int NODE_COUNT = 7;

        // Process map node indexes NI_
        private const int NI_SELECT_PROVIDER = 0;
        private const int NI_SELECT_PACKAGE = 1;
        private const int NI_SELECT_SERVICES = 2;
        private const int NI_ORDER_SUMMARY = 3;
        private const int NI_SERVICE_ADDRESS = 4;
        private const int NI_MAKE_PAYMENT = 5;
        private const int NI_ACCOUNT_SUMMARY = 6;

        // Process map node names PMN_
        private const string PMNN_SELECT_PROVIDER = "Select Provider";
        private const string PMNN_SELECT_PACKAGE = "Select Package";
        private const string PMNN_SELECT_SERVICES = "Select Services";
        private const string PMNN_ORDER_SUMMARY = "Order Summary";
        private const string PMNN_SERVICE_ADDRESS = "Address / Customer Info";
        private const string PMNN_MAKE_PAYMENT = "Make Payment";
        private const string PMNN_ACCOUNT_SUMMARY = "Account Summary";

        public string[] Nodes
        {
            get
            {
                object value = HttpContext.Current.Application["AccountSetupNodes"];

                if (value == null) {
                    string[] steps = new string[NODE_COUNT];

                    steps[NI_SELECT_PROVIDER] = PMNN_SELECT_PROVIDER;
                    steps[NI_SELECT_PACKAGE] = PMNN_SELECT_PACKAGE;
                    steps[NI_SELECT_SERVICES] = PMNN_SELECT_SERVICES;
                    steps[NI_ORDER_SUMMARY] = PMNN_ORDER_SUMMARY;
                    steps[NI_SERVICE_ADDRESS] = PMNN_SERVICE_ADDRESS;
                    steps[NI_MAKE_PAYMENT] = PMNN_MAKE_PAYMENT;
                    steps[NI_ACCOUNT_SUMMARY] = PMNN_ACCOUNT_SUMMARY;

                    HttpContext.Current.Application["AccountSetupNodes"] = value = steps;
                }

                return (string[]) value;
            }
        }

        public int CurrentNodeIndex
        {
            get
            {
                string path = HttpContext.Current.Request.Url.AbsolutePath;

                if (UrlToNodeIndexMap.Contains(path)) {
                    return (int) UrlToNodeIndexMap[path];
                }

                throw new ApplicationException("Process map can not find node for the path: " + path + ".");
            }
        }

        private static Hashtable UrlToNodeIndexMap
        {
            get
            {
                Hashtable nodes = (Hashtable) HttpContext.Current.Application["AccountSetupNodeIndexes"];

                if (nodes == null) {
                    nodes = new Hashtable();

                    nodes.Add(new Uri(SiteMap.NEW_ACC_SELECT_PROVIDER_URL).AbsolutePath, NI_SELECT_PROVIDER);
                    nodes.Add(new Uri(SiteMap.NEW_ACC_SELECT_PACKAGE_URL).AbsolutePath, NI_SELECT_PACKAGE);
                    nodes.Add(new Uri(SiteMap.NEW_ACC_SELECT_SERVICES_URL).AbsolutePath, NI_SELECT_SERVICES);
                    nodes.Add(new Uri(SiteMap.NEW_ACC_ORDER_SUMMARY_URL).AbsolutePath, NI_ORDER_SUMMARY);
                    nodes.Add(new Uri(SiteMap.NEW_ACC_TPV_AGREEMENT_URL).AbsolutePath, NI_ORDER_SUMMARY);
                    nodes.Add(new Uri(SiteMap.NEW_ACC_TPV_DISAGREEMENT_URL).AbsolutePath, NI_ORDER_SUMMARY);
                    nodes.Add(new Uri(SiteMap.NEW_ACC_SERVICE_ADDRESS_URL).AbsolutePath, NI_SERVICE_ADDRESS);
                    nodes.Add(new Uri(SiteMap.NEW_ACC_PAY_CREDIT_CARD_URL).AbsolutePath, NI_MAKE_PAYMENT);
                    nodes.Add(new Uri(SiteMap.NEW_ACC_PAY_CHECK_URL).AbsolutePath, NI_MAKE_PAYMENT);
                    nodes.Add(new Uri(SiteMap.NEW_ACC_SUMMARY_URL).AbsolutePath, NI_ACCOUNT_SUMMARY);

                    HttpContext.Current.Application["AccountSetupNodeIndexes"] = nodes;
                }

                return nodes;
            }
        }
    }
}