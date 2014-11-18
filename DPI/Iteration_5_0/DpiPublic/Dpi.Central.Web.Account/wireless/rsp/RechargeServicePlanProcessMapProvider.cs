using System;
using System.Collections;
using System.Web;
using Dpi.Central.Web.Controls;

namespace Dpi.Central.Web.Account.Wireless.Processes.Rsp
{
    public class RechargeServicePlanProcessMapProvider : IProcessMapProvider
    {
        private const int NODE_COUNT = 5;

        // Process map node indexes NI_
        private const int NI_SELECT_PLAN = 0;
        private const int NI_SELECT_PRODUCTS = 1;
        private const int NI_ORDER_SUMMARY = 2;
        private const int NI_MAKE_PAYMENT = 3;
        private const int NI_RECEIPT = 4;

        // Process map node names PMN_
        private const string PMNN_SELECT_PLAN = "Select Plan";
        private const string PMNN_SELECT_PRODUCTS = "Select Products";
        private const string PMNN_ORDER_SUMMARY = "Order Summary";
        private const string PMNN_MAKE_PAYMENT = "Make Payment";
        private const string PMNN_RECEIPT = "Receipt";

        public string[] Nodes
        {
            get
            {
                object value = HttpContext.Current.Application["RdpNodes"];

                if (value == null) {
                    string[] steps = new string[NODE_COUNT];

                    steps[NI_SELECT_PLAN] = PMNN_SELECT_PLAN;
                    steps[NI_SELECT_PRODUCTS] = PMNN_SELECT_PRODUCTS;
                    steps[NI_ORDER_SUMMARY] = PMNN_ORDER_SUMMARY;
                    steps[NI_MAKE_PAYMENT] = PMNN_MAKE_PAYMENT;
                    steps[NI_RECEIPT] = PMNN_RECEIPT;

                    HttpContext.Current.Application["RdpNodes"] = value = steps;
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
                Hashtable nodes = (Hashtable) HttpContext.Current.Application["RdpNodeIndexes"];

                if (nodes == null) {
                    nodes = new Hashtable();

                    nodes.Add(new Uri(SiteMap.RDP_SELECT_PLAN_URL).AbsolutePath, NI_SELECT_PLAN);
                    nodes.Add(new Uri(SiteMap.RDP_SELECT_PRODUCTS_URL).AbsolutePath, NI_SELECT_PRODUCTS);
                    nodes.Add(new Uri(SiteMap.RDP_ORDER_SUMMARY_URL).AbsolutePath, NI_ORDER_SUMMARY);
                    nodes.Add(new Uri(SiteMap.RDP_PAY_CHECK_URL).AbsolutePath, NI_MAKE_PAYMENT);
                    nodes.Add(new Uri(SiteMap.RDP_PAY_CREDIT_CARD_URL).AbsolutePath, NI_MAKE_PAYMENT);
                    nodes.Add(new Uri(SiteMap.RDP_RECEIPT_URL).AbsolutePath, NI_RECEIPT);

                    HttpContext.Current.Application["RdpNodeIndexes"] = nodes;
                }

                return nodes;
            }
        }
    }
}