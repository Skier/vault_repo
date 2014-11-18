using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using Dalworth.Server.Data;
using Dalworth.Server.Domain;
using Dalworth.Server.Domain.package;
using Dalworth.Server.SDK;
using Dalworth.Server.Servman.Domain;
using Dalworth.Server.Servman.Domain.intermediate;
using Address=Dalworth.Server.Domain.Address;
using Customer=Dalworth.Server.Domain.Customer;
using CustomerTypeEnum=Dalworth.Server.Servman.Domain.intermediate.CustomerTypeEnum;
using Task=Dalworth.Server.Domain.Task;

namespace Dalworth.Server.Servman.Win32.MainForm
{
    public class MainFormModel
    {        
        #region ExportData

//        public void ExportData(string folder, int fileNumber)
//        {
//            string[] fileCandidates = Directory.GetFiles(folder, "*" + fileNumber + "*");
//            if (fileCandidates == null || fileCandidates.Length == 0)
//            {
//                MessageBox.Show("Xml file not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                return;
//            }
//
//            string fileName = fileCandidates[0];
//            if (!fileName.ToLower().Contains("insert") && !fileName.ToLower().Contains("update"))
//            {
//                MessageBox.Show("Operation is not specified. Please name file NN_Insert_XX or NN_Update_XX", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                return;
//            }
//
//            bool isInsert = fileName.ToLower().Contains("insert");
//
//
//            XmlSerializer serializer = new XmlSerializer(typeof(Order));
//            Order order = (Order) serializer.Deserialize(new StreamReader(fileName));
//
//            if (isInsert)
//            {
//                ServmanExportModel.InsertOrder(order);
//                Directory.CreateDirectory(folder + "out\\");
//                StreamWriter writer = File.CreateText(folder + "out\\" + fileName.Replace(folder, string.Empty) + ".txt");
//                writer.WriteLine("Ticket Number: " + order.TicketNumber);
//                writer.WriteLine("Customer ID: " + order.Customer.ID);
//                writer.Close();
//            }                
//            else
//                ServmanExportModel.UpdateOrder(order);
//
//            MessageBox.Show("Data export has completed successfully", "Operation succeeded", MessageBoxButtons.OK,
//                    MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
//        }

        #endregion

        #region Initial entiry preset

//            Order order = new Order();
//            order.Customer = new Domain.intermediate.Customer();
//            order.Customer.Address.AreaId = "D/FW";
//            order.Customer.Address.Block = "7016";
//            order.Customer.Address.Prefix = string.Empty;
//            order.Customer.Address.Street = "Randall";
//            order.Customer.Address.Suffux = "Way";
//            order.Customer.Address.Unit = string.Empty;
//            order.Customer.Address.Address2 = string.Empty;
//            order.Customer.Address.City = "Plano";
//            order.Customer.Address.State = "TX";
//            order.Customer.Address.Zip = "75025";
//            order.Customer.Address.Zip4 = string.Empty;
//            order.Customer.Address.Grid = "H";
//            order.Customer.Address.Page = "553";
//
//            order.Customer.ID = 
//            order.Customer.CustomerType = CustomerTypeEnum.Residential;
//            order.Customer.Name = "KALASHNIKOV, SERGII";            
//            order.Customer.HomePhone = "11111111";
//            order.Customer.BusinessPhone = "2222222";
////            order.Customer.LastContact = new DateTime(1899, 12, 30); // this date is like null date in FoxPro
////            order.Customer.LastService = new DateTime(1899, 12, 30);
////            order.Customer.LastAddressChange = new DateTime(1899, 12, 30);
//            order.Customer.EmailAddress = string.Empty;
//            //order.Customer.HasVisitedWebsite;
//
//
//            //order.AlternativeAddress = order.Customer.Address;
//            order.OrderType = OrderTypeEnum.RugPickup;
//            order.OrderStatus = OrderStatusEnum.Pending;
//            //order.TicketNumber
//            order.ContactName = "Sergei";
//            order.ServiceDate = DateTime.Now;
//            order.TimeSchedule = "SECTOR";
//            //order.TechnicianId = "108"; set only when assigned
//            order.Cost = 150;
//            //order.ReceivedAmount
//            order.UserId = "032";
//            order.AdvertisingSourceId = "000067";
//            //order.DateTimeFirstCall = DateTime.Now;
//            //order.DateSchedule = DateTime.Now.AddDays(2);
//            //order.DateTimeDispatch
//            //order.DateTimeCompleted
//            //order.DateTimeArrived
//            order.PrintedNote = "Printed note";
//            //order.TimeEstimateComplete
//            //order.IsConfirmed
//            order.DispatchNote = "dispatch note";
//            order.MapBook = string.Empty;
//            order.PerformedByUser = "025";
//
//            SyncModel.InsertOrder(order);

        #endregion

        
    }
}
