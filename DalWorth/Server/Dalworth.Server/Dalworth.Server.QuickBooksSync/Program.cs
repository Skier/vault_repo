using System;
using System.IO;
using System.Collections.Generic;
using Dalworth.Server.SDK;
using Dalworth.Server.QuickBooks;
using Dalworth.Server.Domain;

namespace Dalworth.Server.QuickBooksSync
{
    public delegate void AskContinue();

    class Program
    {
        
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Quickbooks sync tool");
                Console.WriteLine( "1 - FindMissing Customers");
                Console.WriteLine("2 - RefreshBaseData");
                Console.WriteLine("3 - Syncronize Data");
                Console.WriteLine("4 - Process Requests");
                return;
            }

            int selection = int.Parse(args[0]);

            Host.LogFileWriter = File.AppendText(Host.GetPath("log.txt"));
            Configuration.RemoveConfigReadOnlyAttribute();
            Configuration.LoadGlobalConfiguration(false);

            QbSync qbSync = new QbSync();

            qbSync.Connect();
            try
            {
                switch(selection)
                {
                    case 1:
                        qbSync.FindMissingCustomers();
                        break;
                    case 2:
                        qbSync.RefreshBaseData();
                        break;
                    case 3:
                        qbSync.SyncronizeData();
                        break;
                    case 4:
                        //qbSync.ProcessRequests(CanProceed, SelectSimilarCustomer);
                        break;
                    case 5:
                        qbSync.FindCustomerListIds(args[1]);
                        break;
                }
            }
            catch (Exception ex)
            {
                Host.Trace("Main", ex.ToString());
            }
            finally
            {
                qbSync.Disconnect();
            }

            Host.Trace("Main", "DONE!!");
            return;
        }

        private static bool CanProceed()
        {
            Console.WriteLine("Would you like to continue?");
            string response = Console.ReadLine();
            if (response == "NO")
                return false;

            return true;
        }

        private static int SelectSimilarCustomer(QbCustomer newCustomer, List<QbCustomer> similarCustomers)
        {
            Console.WriteLine("New Customer:" + newCustomer.Name);
            Console.WriteLine("            :" + newCustomer.Phone1);
            Console.WriteLine("            :" + newCustomer.BillingAddressAddr1);
            Console.WriteLine("            :" + newCustomer.BillingAddressAddr2);
            Console.WriteLine("            :" + newCustomer.BillingAddressCity);
            Console.WriteLine("            :" + newCustomer.BillingAddressState);
            Console.WriteLine("            :" + newCustomer.ShippingAddressPostalCode);

            Console.WriteLine("Similar Customers:");
                                                                
            for (int i= 0; i < similarCustomers.Count; i++)
            {
                 QbCustomer similarQbCustomer = similarCustomers[i];

                 Console.WriteLine("Select : " + i);
                 Console.WriteLine("            :" + similarQbCustomer.Name);
                 Console.WriteLine("            :" + similarQbCustomer.Phone1);
                 Console.WriteLine("            :" + similarQbCustomer.BillingAddressAddr1);
                 Console.WriteLine("            :" + similarQbCustomer.BillingAddressAddr2);
                 Console.WriteLine("            :" + similarQbCustomer.BillingAddressCity);
                 Console.WriteLine("            :" + similarQbCustomer.BillingAddressState);
                 Console.WriteLine("            :" + similarQbCustomer.ShippingAddressPostalCode);
            }

            Console.WriteLine("Select -1 to create new customer");
            string answer = Console.ReadLine();

            int selection;
            if (string.IsNullOrEmpty(answer))
                selection = -1;
            else
                selection = int.Parse(answer);

            return selection;
        }
    }
}
