using System;
using System.Collections.Generic;
using System.Text;
using MobileTech.Domain;
using MobileTech.ServiceLayer;
using MobileTech.Data;
using MobileTech.Windows.UI.SelectItem;
using MobileTech.Windows.UI.Odometer;

namespace MobileTech.Windows.UI.CustomerOperations
{
	public class CustomerOperationsModel:IModel,ISelectItemListener,IOdometerModel
	{

        public event ProductTableModelQuantityChangedHandler QuantityChanged; 

		RouteScheduleTableModel m_customers;
		CustomerVisit m_customerVisit;

		//ProductTableModel m_invoice;
		ProductTableModel m_invoiceItemEntry;

		RouteScheduleQueue m_routeScheduleQueue;

        Dictionary<CustomerTransactionTypeEnum, CustomerTransaction> m_transactions;

		public CustomerOperationsModel()
		{
            m_transactions = new Dictionary<CustomerTransactionTypeEnum, CustomerTransaction>();
		}

		#region Customers

		private void LoadCustomers()
		{
            List<RouteScheduleQueue> list = RouteScheduleQueue.FindCurrent();

			m_customers = new RouteScheduleTableModel(list);
		}

		public RouteScheduleTableModel Customers
		{
			get
			{
				if (m_customers == null)
					LoadCustomers();

				return m_customers;
			}
			set
			{
				m_customers = value;
			}
		}



		public RouteScheduleQueue RouteScheduleQueue
		{
			get { return m_routeScheduleQueue; }
			set { m_routeScheduleQueue = value; }
		}

		#endregion

		#region Invoice

        /*
		public ProductTableModel Invoice
		{
			get
			{

				if (m_invoice == null)
				{
					List<CustomerTransactionDetail> items = new List<CustomerTransactionDetail>();

					if (m_transactions.ContainsKey(CustomerTransactionTypeEnum.Sales))
					{
                        CustomerTransactionDetail.Find(
                            m_transactions[CustomerTransactionTypeEnum.Sales], false, items);
					}

                    m_invoice = new ProductTableModel(items, true);
				}

				return m_invoice;
			}
			set
			{
				m_invoice = value;
			}
		}*/

		public ProductTableModel InvoiceItemEntry
		{
			get
			{

                if (m_invoiceItemEntry == null)
				{
					List<CustomerTransactionDetail> items = new List<CustomerTransactionDetail>();

                    CustomerTransactionDetail.Find(m_transactions[CustomerTransactionTypeEnum.Sales], items);

                    m_invoiceItemEntry = new ProductTableModel(items, false);

                    m_invoiceItemEntry.QuantityChanged += new ProductTableModelQuantityChangedHandler(OnQuantityChanged);
				}


				return m_invoiceItemEntry;
			}
			set
			{
                if (value == null
                    && m_invoiceItemEntry != null)
                {
                    m_invoiceItemEntry.QuantityChanged -= new ProductTableModelQuantityChangedHandler(OnQuantityChanged);
                }

				m_invoiceItemEntry = value;
			}
		}

        void OnQuantityChanged(CustomerTransactionDetail item, int oldQuantity)
        {
            if (QuantityChanged != null)
            {
                QuantityChanged.Invoke(item, oldQuantity);
            }
        }

   
		public void LoadInvoice()
		{
			if (!m_transactions.ContainsKey(CustomerTransactionTypeEnum.Sales))
			{
                CustomerTransaction transaction = CustomerTransaction.Find(
					CustomerTransactionTypeEnum.Sales, m_customerVisit);

				if (transaction == null)
				{
					transaction = CustomerTransaction.Prepare(
						Route.FindCurrent(),
						m_customerVisit,
                        CustomerTransactionTypeEnum.Sales);

                    Counter.Assign(transaction.BusinessTransaction);

                    BusinessTransaction.Insert(transaction.BusinessTransaction);

					transaction.BusinessTransactionId = transaction.BusinessTransaction.BusinessTransactionId;

                    CustomerTransaction.Insert(transaction);
				}

                m_transactions.Add(CustomerTransactionTypeEnum.Sales, transaction);
			}
		}

		public void SaveInvoice()
		{
            CustomerTransaction transaction = m_transactions[CustomerTransactionTypeEnum.Sales];

            CustomerTransactionDetail.Clear(transaction);

			int itemsCount = 0;

			for (int i = 0; i < InvoiceItemEntry.GetRowCount(); i++)
			{
				if (InvoiceItemEntry.GetObject(i).Quantity < 1)
					continue;

				++itemsCount;

				CustomerTransactionDetail customerTransactionDetail = InvoiceItemEntry.GetObject(i);

                CustomerTransactionDetail.Insert(customerTransactionDetail);


			}


			if(itemsCount == 0)
			{
                CustomerTransaction.Delete(transaction);
				BusinessTransaction.Delete(transaction.BusinessTransaction);

                m_transactions.Remove(CustomerTransactionTypeEnum.Sales);
			}

			InvoiceItemEntry = null;

		}

		#endregion

		#region Transactions

		public void Begin()
		{
			Database.Begin();
			m_customerVisit = new CustomerVisit();

            Route route = Route.FindCurrent();

            m_customerVisit.CustomerId = m_routeScheduleQueue.Customer.CustomerId;
			m_customerVisit.DateStarted = DateTime.Now;
            m_customerVisit.SessionId = Session.FindCurrent().SessionId;
			m_customerVisit.RouteNumber = route.RouteNumber;
			m_customerVisit.LocationId = route.LocationId;

            Counter.Assign(m_customerVisit);

			CustomerVisit.Insert(m_customerVisit);
		}

		public void Commit()
		{

            m_routeScheduleQueue.Status = RouteScheduleQueueStatusEnum.Serviced;

			RouteScheduleQueue.Update(m_routeScheduleQueue);



			foreach (CustomerTransaction transaction in m_transactions.Values)
			{
                transaction.BusinessTransaction.DateCommited = DateTime.Now;
                transaction.BusinessTransaction.Status = BusinessTransactionStatusEnum.Commited;

                BusinessTransaction.Update(transaction.BusinessTransaction);
			}


            for (int i = 0; i < InvoiceItemEntry.GetRowCount(); i++)
            {

               CustomerTransactionDetail detail = InvoiceItemEntry.GetObject(i);
               RouteInventory routeInventory = new RouteInventory();
               routeInventory.Item = detail.Item;
               routeInventory.SessionId = detail.SessionId;
               routeInventory.StorageType = StorageTypeEnum.Store;

               routeInventory.AssignInventoryPeriodIndex();

               routeInventory.ModifySale(detail.Quantity);
            }

			m_customerVisit.DateFinished = DateTime.Now;

			CustomerVisit.Update(m_customerVisit);

			Database.Commit();

			CleanUp();
		}

		public void Rollback()
		{
			Database.Rollback();
			CleanUp();
		}

		public bool IsTransactionExists
		{
			get
			{
				return m_transactions.Count > 0;
			}
		}
		#endregion

		public void AssignDocumentNumbers()
		{
			foreach (CustomerTransaction transaction in m_transactions.Values)
			{
				BusinessTransaction.AssignDocumentNumber(transaction.BusinessTransaction);
			}
		}

		public void CleanUp()
		{
			m_routeScheduleQueue = null;
			//m_invoice = null;
			InvoiceItemEntry = null;
			m_transactions.Clear();
		}


        #region IModel Members

        public void Init()
        {

        }

        #endregion

        #region ISelectItemListener Members

        public int GetQuantity(Item item)
        {
            for (int i = 0; i < m_invoiceItemEntry.GetRowCount(); i++)
            {
                CustomerTransactionDetail customerTransactionDetail =
                    (CustomerTransactionDetail)m_invoiceItemEntry.GetObject(i);

                if (customerTransactionDetail.ItemNumber.Equals(item.ItemNumber))
                {
                    return customerTransactionDetail.Quantity;
                }
            }

            return 0;
        }

        public void SetQuantity(Item item, int quantity)
        {
            for (int i = 0; i < m_invoiceItemEntry.GetRowCount(); i++)
            {
                CustomerTransactionDetail customerTransactionDetail =
                    (CustomerTransactionDetail)m_invoiceItemEntry.GetObject(i);

                if (customerTransactionDetail.ItemNumber.Equals(item.ItemNumber))
                {
                    m_invoiceItemEntry.SetValueAt(quantity, i, 1);

                    return;
                }
            }

            CustomerTransactionDetail newItem = new CustomerTransactionDetail(m_transactions[CustomerTransactionTypeEnum.Sales],item);

            int invetoryQuantity = 0;

            try
            {
                invetoryQuantity = RouteInventory.FindBy(item, StorageTypeEnum.Store).TruckQty;
            }
            catch (DataNotFoundException) { }

            newItem.InventoryQuantity = invetoryQuantity;

            int index = m_invoiceItemEntry.Add(newItem);

            m_invoiceItemEntry.SetValueAt(quantity, index, 1);

        }

        public ItemTypeEnum GetItemType()
        {
            return ItemTypeEnum.Product;
        }

        #endregion

        #region IOdometerModel Members

        int m_odometerReading;

        public int OdometerReading
        {
            get
            {
                return m_odometerReading;
            }
            set
            {
                m_odometerReading = value;
            }
        }

        #endregion
    }
}
