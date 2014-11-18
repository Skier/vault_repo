using System;
using System.Windows.Forms;

namespace Dalworth.Server.MainForm.Accounting
{
    public partial class QbAddress : UserControl
    {
        #region HeaderText

        private string m_headerText;
        public string HeaderText
        {
            get { return m_headerText; }
            set { m_headerText = value; }
        }

        #endregion 

        #region Address1

        private string m_address1;
        public string Address1
        {
            get
            {
                return m_address1;
            }
            set
            {
                m_address1 = value;
            }
        }

        #endregion

        #region Address2

        private string m_address2;
        public string Address2
        {
            get
            {
                return m_address2;
            }
            set
            {
                m_address2 = value;
            }
        }

        #endregion

        #region City

        private string m_city;
        public string City
        {
            get 
            {
                return m_city;   
            }
            set
            {
                m_city = value;
            }
        }

        #endregion 

        #region State

        private string m_state;
        public string State
        {
            get
            {
                return m_state;
            }
            set
            {
                m_state = value;
            }
        }
        
        #endregion

        #region Zip

        private string m_zip;
        public string Zip
        {
            get
            {
                
                return m_zip;
            }
            set
            {
                m_zip = value;
            }
        }

        #endregion 

        #region Constructor

        public QbAddress()
        {
            InitializeComponent();
            m_txtAddress1.Validating += OnValidating;
            m_txtAddress2.Validating += OnValidating;
            m_txtCity.Validating += OnValidating;
            m_txtState.Validating += OnValidating;
            m_txtZip.Validating += OnValidating;
        }

        #endregion 

        #region Initialize

        public void Initialize()
        {
            m_grpAddress.Text = HeaderText;
            LoadDataToUI();
        }

        #endregion 

        #region OnLoad

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            m_grpAddress.Text = HeaderText;
        }

        #endregion;

        #region HasErrors

        public bool HasErrors
        {
            get { return m_errorProvider.HasErrors; }
        }

        #endregion 

        #region ClearErrors

        public void ClearErrors ()
        {
            m_errorProvider.ClearErrors();
        }

        #endregion 

        #region LoadData To From UI

        private void LoadDataToUI()
        {
            m_txtAddress1.Text = Address1;
            m_txtAddress2.Text = Address2;
            m_txtCity.Text = City;
            m_txtState.Text = State;
            m_txtZip.Text = Zip;
        }

        public void LoadDataFromUI()
        {
            Address1 = m_txtAddress1.Text;
            Address2 = m_txtAddress2.Text;
            City = m_txtCity.Text;
            State = m_txtState.Text;
            Zip = m_txtZip.Text;
        }

        #endregion

        #region OnValidating

        private void OnValidating(object sender, EventArgs e)
        {
            ValidateOptinalField(m_txtAddress1, 41);
            ValidateOptinalField(m_txtAddress2, 41);
            ValidateOptinalField(m_txtCity, 31);
            ValidateOptinalField(m_txtState, 21);
            ValidateOptinalField(m_txtZip, 13);

        }

        #endregion

        #region ValidateOptinalField

        private void ValidateOptinalField(System.Windows.Forms.Control control, int maxSize)
        {
            if (!string.IsNullOrEmpty(control.Text) && control.Text.Length > maxSize)
            {
                m_errorProvider.SetError(control, "Too Long");
            }
        }

        #endregion 
    }
}


