using System;
using System.Collections;
using System.Web;
using Dpi.Central.Web.Controls;

namespace Dpi.Central.Web.Account.Wireless.Processes.Rwa
{
    public class ReplenishWirelessAccountProcessMapProvider : IProcessMapProvider
    {
        private const int NODE_COUNT = 4;

        // Process map node indexes NI_
        private const int NI_CUSTOMER_INFO = 0;
        private const int NI_ORDER_SUMMARY = 1;
        private const int NI_PAYMENT = 2;
        private const int NI_RECEIPT = 3;

        // Process map node names PMN_
        private const string PMNN_CUSTOMER_INFO = "Customer Info";
        private const string PMNN_ORDER_SUMMARY = "Order Summary";
        private const string PMNN_PAYMENT = "Make Payment";
        private const string PMNN_RECEIPT = "Receipt";

        public string[] Nodes
        {
            get
            {
                object value = HttpContext.Current.Application["RwaNodes"];

                if (value == null) {
                    string[] steps = new string[NODE_COUNT];

                    steps[NI_CUSTOMER_INFO] = PMNN_CUSTOMER_INFO;
                    steps[NI_ORDER_SUMMARY] = PMNN_ORDER_SUMMARY;
                    steps[NI_PAYMENT] = PMNN_PAYMENT;
                    steps[NI_RECEIPT] = PMNN_RECEIPT;

                    HttpContext.Current.Application["RwaNodes"] = value = steps;
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
                Hashtable nodes = (Hashtable) HttpContext.Current.Application["RwaNodeIndexes"];

                if (nodes == null) {
                    nodes = new Hashtable();

                    nodes.Add(new Uri(SiteMap.RWA_CUSTOMER_INFO_URL).AbsolutePath, NI_CUSTOMER_INFO);
                    nodes.Add(new Uri(SiteMap.RWA_ORDER_SUMMARY_URL).AbsolutePath, NI_ORDER_SUMMARY);
                    nodes.Add(new Uri(SiteMap.RWA_PAYMENT_URL).AbsolutePath, NI_PAYMENT);
                    nodes.Add(new Uri(SiteMap.RWA_RECEIPT_URL).AbsolutePath, NI_RECEIPT);

                    HttpContext.Current.Application["RwaNodeIndexes"] = nodes;
                }

                return nodes;
            }
        }
    }
}