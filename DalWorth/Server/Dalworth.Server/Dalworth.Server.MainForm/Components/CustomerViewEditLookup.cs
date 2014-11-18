using Dalworth.Server.Domain;
using Dalworth.Server.MainForm.CustomerLookup;
using Dalworth.Server.Windows;

namespace Dalworth.Server.MainForm.Components
{
    public partial class CustomerViewEditLookup : CustomerViewEdit
    {
        public delegate void CustomerChangedHandler(Customer customer, Address address);
        public event CustomerChangedHandler CustomerChanged;


        #region Constructor

        public CustomerViewEditLookup()
        {
            InitializeComponent();
//            m_btnLookup.Click += OnLookupClick;
//            m_btnClear.Click += OnClearClick;
        }

        #endregion

        #region BaseLead

        private LeadWrapper m_baseLead;
        public LeadWrapper BaseLead
        {
            get { return m_baseLead; }
            set { m_baseLead = value; }
        }

        #endregion

        public override Customer Customer
        {
            set
            {
                base.Customer = value;
//                m_btnClear.Enabled = value != null;
            }
        }

        #region IsReadOnly

        private bool m_isReadOnly;
        public bool IsReadOnly
        {
            get { return m_isReadOnly; }
            set
            {
                m_isReadOnly = value;
                Enabled = !m_isReadOnly;
//                m_btnLookup.Enabled = !m_isReadOnly;
//                m_btnClear.Enabled = !m_isReadOnly;
            }
        }

        #endregion

        #region EditButtonText

        public string EditButtonText
        {
            get { return m_btnCustomerEdit.Text; }
            set { m_btnCustomerEdit.Text = value; }
        }

        #endregion

        #region ShowLookupDialog

        public void ShowLookupDialog()
        {
            CustomerAndAddress customer = null;

            if (Customer != null)
                customer = new CustomerAndAddress(Customer, Address);

            using (CustomerLookupController controller = Controller.Prepare<CustomerLookupController>(customer))
            {
                if (BaseLead != null)
                    controller.BaseLead = BaseLead;

                controller.Execute(false);
                if (controller.IsCustomerSelected)
                {
                    Customer = controller.Customer.Customer;
                    Address = controller.Customer.Address;
                    if (CustomerChanged != null)
                        CustomerChanged.Invoke(Customer, Address);
                }
            }
        }

        #endregion
    }
}

