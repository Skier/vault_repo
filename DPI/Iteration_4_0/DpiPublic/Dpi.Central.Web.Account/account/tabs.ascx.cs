using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace Dpi.Central.Web.Account
{

    #region TabsStateInfo

    public class TabsStateInfo
    {
        private int m_selectedIndex;
        private bool m_tab1Enabled;
        private bool m_tab2Enabled;
        private bool m_tab3Enabled;
        private bool m_tab4Enabled;
        private bool m_tab5Enabled;
        private bool m_tab6Enabled;

        public TabsStateInfo()
        {
            m_selectedIndex = -1;
            m_tab1Enabled = true;
            m_tab2Enabled = true;
            m_tab3Enabled = true;
            m_tab4Enabled = true;
            m_tab5Enabled = true;
            m_tab6Enabled = true;
        }

        public int SelectedIndex
        {
            get { return m_selectedIndex; }
            set { m_selectedIndex = value; }
        }
        
        public bool Tab1Enabled
        {
            get { return m_tab1Enabled; }
            set { m_tab1Enabled = value; }
        }

        public bool Tab2Enabled
        {
            get { return m_tab2Enabled; }
            set { m_tab2Enabled = value; }
        }

        public bool Tab3Enabled
        {
            get { return m_tab3Enabled; }
            set { m_tab3Enabled = value; }
        }

        public bool Tab4Enabled
        {
            get { return m_tab4Enabled; }
            set { m_tab4Enabled = value; }
        }

        public bool Tab5Enabled
        {
            get { return m_tab5Enabled; }
            set { m_tab5Enabled = value; }
        }
        
        public bool Tab6Enabled
        {
            get { return m_tab6Enabled; }
            set { m_tab6Enabled = value; }
        }
    }

    #endregion
    
	public class Tabs : UserControl
	{
	    protected ImageButton m_btnPressEnterStub;
        protected ImageButton m_btn01;
        protected ImageButton m_btn02;
        protected ImageButton m_btn03;
        protected ImageButton m_btn04;
        protected ImageButton m_btn05;	                    
        protected ImageButton m_btn06;	                    

	    #region State

	    public TabsStateInfo State
	    {
	        get
	        {
	            if (Session["AccountTabs_State"] == null)
	                Session["AccountTabs_State"] = new TabsStateInfo();
	                            
	            return (TabsStateInfo)Session["AccountTabs_State"];	            
	        }
	    }

	    #endregion
	    	    
		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.Load += new System.EventHandler(this.OnPageLoad);
            m_btn01.Click += new ImageClickEventHandler(OnButton1Click);
            m_btn02.Click += new ImageClickEventHandler(OnButton2Click);
            m_btn03.Click += new ImageClickEventHandler(OnButton3Click);
            m_btn04.Click += new ImageClickEventHandler(OnButton4Click);
            m_btn05.Click += new ImageClickEventHandler(OnButton5Click);		    
            m_btn06.Click += new ImageClickEventHandler(OnButton6Click);		    
        }
		#endregion

	    #region OnPageLoad

	    private void OnPageLoad(object sender, EventArgs e) {
	        
            m_btn01.ImageUrl = GetImageUrl(0, false, State.Tab1Enabled);
            m_btn02.ImageUrl = GetImageUrl(1, false, State.Tab2Enabled);
            m_btn03.ImageUrl = GetImageUrl(2, false, State.Tab3Enabled);
            m_btn04.ImageUrl = GetImageUrl(3, false, State.Tab4Enabled);
            m_btn05.ImageUrl = GetImageUrl(4, false, State.Tab5Enabled);
            m_btn06.ImageUrl = GetImageUrl(5, false, State.Tab6Enabled);
	        	        
            if (State.SelectedIndex == 0) {                
                m_btn01.ImageUrl = GetImageUrl(0, true, State.Tab1Enabled);
                m_btn02.ImageUrl = GetImageUrl(0, false, true, State.Tab2Enabled);
                
            }                
            else if (State.SelectedIndex == 1) {
                m_btn02.ImageUrl = GetImageUrl(1, true, State.Tab2Enabled);
                m_btn01.ImageUrl = GetImageUrl(1, true, false, State.Tab1Enabled);
                m_btn03.ImageUrl = GetImageUrl(1, false, true, State.Tab3Enabled);
            }
            else if (State.SelectedIndex == 2) {
                m_btn03.ImageUrl = GetImageUrl(2, true, State.Tab3Enabled);
                m_btn02.ImageUrl = GetImageUrl(2, true, false, State.Tab2Enabled);
                m_btn04.ImageUrl = GetImageUrl(2, false, true, State.Tab4Enabled);

            }
            else if (State.SelectedIndex == 3) {
                m_btn04.ImageUrl = GetImageUrl(3, true, State.Tab4Enabled);
                m_btn03.ImageUrl = GetImageUrl(3, true, false, State.Tab3Enabled);
                m_btn05.ImageUrl = GetImageUrl(3, false, true, State.Tab5Enabled);
                
            }
            else if (State.SelectedIndex == 4) {
                m_btn05.ImageUrl = GetImageUrl(4, true, State.Tab5Enabled);
                m_btn04.ImageUrl = GetImageUrl(4, true, false, State.Tab4Enabled);
                m_btn06.ImageUrl = GetImageUrl(4, false, true, State.Tab6Enabled);
                
            }	        	        	        
            else if (State.SelectedIndex == 5) {
                m_btn06.ImageUrl = GetImageUrl(5, true, State.Tab6Enabled);
                m_btn05.ImageUrl = GetImageUrl(5, true, false, State.Tab5Enabled);
            }
	        	        	        
            if (State.Tab1Enabled)
                m_btn01.Enabled = true;
            else 
                m_btn01.Enabled = false;
	        
            if (State.Tab2Enabled)
                m_btn02.Enabled = true;
            else 
                m_btn02.Enabled = false;

            if (State.Tab3Enabled)
                m_btn03.Enabled = true;
            else 
                m_btn03.Enabled = false;

	        if (State.Tab4Enabled)
                m_btn04.Enabled = true;
            else 
                m_btn04.Enabled = false;
	        
            if (State.Tab5Enabled)
                m_btn05.Enabled = true;
            else 
                m_btn05.Enabled = false;	        	        			
	        
            if (State.Tab6Enabled)
                m_btn06.Enabled = true;
            else 
                m_btn06.Enabled = false;	        	        			
	    }

	    #endregion

	    #region GetImageUrl

	    private string GetImageUrl(int tabNumber, bool isOn, bool isEnabled)
	    {
	        return GetImageUrl(tabNumber, isOn, false, false, isEnabled);
	    }
	    
	    private string GetImageUrl(int tabNumber, bool isLeftOff, bool isRightOff, bool isEnabled)
	    {
	        return GetImageUrl(tabNumber, false, isLeftOff, isRightOff, isEnabled);
	    }	    
	    
	    private string GetImageUrl(int tabNumber, bool isOn, bool isLeftOff, bool isRightOff, bool isEnabled)
	    {
	        string rv;
	        
	        if (isLeftOff)
	        {
	            rv = SiteMap.ACCOUNT_IMAGES_URL + "tab0" + (tabNumber - 1) + "_off_0" + tabNumber;
	        }	            	        
	        else if (isRightOff)
	        {
	            rv = SiteMap.ACCOUNT_IMAGES_URL + "tab0" + (tabNumber + 1) + "_off_0" + tabNumber;
	        }	            
	        else
	        {
	            rv = SiteMap.ACCOUNT_IMAGES_URL + "tab0" + tabNumber;
	            
	            if (isOn)
	                rv += "_on";
	            else
	                rv += "_off";	        	            
	        }	
	        
	        if (!isEnabled && !isOn)
	            rv += "_disabled";
	        
	        rv += ".png";
	        
	        return rv;
	    }

	    #endregion

	    #region OnClick

	    private void OnButton1Click(object sender, ImageClickEventArgs e) {
	        if (!m_btn01.Enabled)
	            return;
	        
	        State.SelectedIndex = 0;
	        Response.Redirect(SiteMap.ACCOUNT_SUMMARY_URL);
	    }
	    
	    private void OnButton2Click(object sender, ImageClickEventArgs e) {
	        if (!m_btn02.Enabled)
	            return;
	        
	        State.SelectedIndex = 1;
	        Response.Redirect(SiteMap.ACCOUNT_SETTINGS_URL);
	    }
	    
	    private void OnButton3Click(object sender, ImageClickEventArgs e) {
            if (!m_btn03.Enabled)
                return;
	        
	        State.SelectedIndex = 2;
	        Response.Redirect(SiteMap.ORDER_STATUS_URL);
	    }
	    
	    private void OnButton4Click(object sender, ImageClickEventArgs e) {
            if (!m_btn04.Enabled)
                return;
	        
            State.SelectedIndex = 3;
	        Response.Redirect(SiteMap.PAYMENT_SELECTION_URL);
	    }
	    
	    private void OnButton5Click(object sender, ImageClickEventArgs e) {
            if (!m_btn05.Enabled)
                return;

	        State.SelectedIndex = 4;
	        Response.Redirect(SiteMap.PROMISE_TO_PAY_URL);
	    }
	    
	    private void OnButton6Click(object sender, ImageClickEventArgs e) {
            if (!m_btn06.Enabled)
                return;

	        State.SelectedIndex = 5;
	        Response.Redirect(SiteMap.REC_PAYMENTS_URL);
	    }

	    #endregion
	}
}
