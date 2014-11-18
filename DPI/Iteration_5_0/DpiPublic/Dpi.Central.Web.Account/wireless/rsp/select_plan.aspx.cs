using System;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;
using DPI.Interfaces;

namespace Dpi.Central.Web.Account.Wireless.Processes.Rsp
{
    public class SelectPlanPage : RechargeServicePlanBasePage
    {
        protected Controls.Panel pnlPlan0;
        protected Controls.Panel pnlPlan1;
        protected Controls.Panel pnlPlan2;
        protected Controls.Panel pnlPlan3;
        protected Controls.Panel pnlPlan4;
        protected Controls.Panel pnlPlan5;
        protected Controls.Panel pnlPlan6;
        protected Controls.Panel pnlPlan7;
        protected Controls.Panel pnlPlan8;
                
        protected Label lblPlanCaption0;
        protected Label lblPlanCaption1;
        protected Label lblPlanCaption2;
        protected Label lblPlanCaption3;
        protected Label lblPlanCaption4;
        protected Label lblPlanCaption5;
        protected Label lblPlanCaption6;
        protected Label lblPlanCaption7;
        protected Label lblPlanCaption8;
        
        protected Table tblPlanBody0;
        protected Table tblPlanBody1;
        protected Table tblPlanBody2;
        protected Table tblPlanBody3;
        protected Table tblPlanBody4;
        protected Table tblPlanBody5;
        protected Table tblPlanBody6;
        protected Table tblPlanBody7;
        protected Table tblPlanBody8;
        protected Table tblPlanBody9;
        protected System.Web.UI.WebControls.ImageButton m_btnBack;
        
        private Table[] m_bodyTables = new Table[9];
        private Label[] m_captions = new Label[9];        
        private Panel[] m_panels = new Panel[9];
        
        #region Web Form Designer generated code

        protected override void OnInit(EventArgs e)
        {   
            InitArrays();    
            InitPlans();
            InitializeComponent();
            base.OnInit(e);                        
        }

        private void InitializeComponent()
        {
            this.Load += new System.EventHandler(this.SelectPlanPage_Load);
            m_btnBack.Click += new ImageClickEventHandler(OnBackClick);
        }

        #endregion                
        
        private void OnBackClick(object sender, ImageClickEventArgs e) {
            Response.Redirect(SiteMap.WRLS_SERVICE_INFO_URL);
        }
        

        private void SelectPlanPage_Load(object sender, EventArgs e)
        {                        
        }
        
        private void InitPlans()
        {
            for (int i = 0; i <= 8; i++) {
                m_panels[i].Visible = false;                
            }
            
            Hashtable plans = Model.GetUserPlans();
            int keyIndex = 0;
            foreach (string key in plans.Keys) {                    
                ShowPlan(keyIndex, key, (ArrayList) plans[key]);
                keyIndex++;
            }                                                                                       
        }
        
        private void InitArrays()
        {
            m_panels[0] = pnlPlan0;
            m_panels[1] = pnlPlan1;
            m_panels[2] = pnlPlan2;
            m_panels[3] = pnlPlan3;
            m_panels[4] = pnlPlan4;
            m_panels[5] = pnlPlan5;
            m_panels[6] = pnlPlan6;
            m_panels[7] = pnlPlan7;
            m_panels[8] = pnlPlan8;
            
            m_bodyTables[0] = tblPlanBody0;
            m_bodyTables[1] = tblPlanBody1;
            m_bodyTables[2] = tblPlanBody2;
            m_bodyTables[3] = tblPlanBody3;
            m_bodyTables[4] = tblPlanBody4;
            m_bodyTables[5] = tblPlanBody5;
            m_bodyTables[6] = tblPlanBody6;
            m_bodyTables[7] = tblPlanBody7;
            m_bodyTables[8] = tblPlanBody8;
            
            m_captions[0] = lblPlanCaption0;
            m_captions[1] = lblPlanCaption1;
            m_captions[2] = lblPlanCaption2;
            m_captions[3] = lblPlanCaption3;
            m_captions[4] = lblPlanCaption4;
            m_captions[5] = lblPlanCaption5;
            m_captions[6] = lblPlanCaption6;
            m_captions[7] = lblPlanCaption7;
            m_captions[8] = lblPlanCaption8;            
        }
                
        
        private void ShowPlan(int planIndex, string planName, ArrayList products)
        {
            m_panels[planIndex].Visible = true;
            m_captions[planIndex].Text = planName;
            Table table = m_bodyTables[planIndex];                                      
            
            foreach (IWireless_Products product in products)
            {
                TableRow row = new TableRow();
                
                TableCell productNameCell = new TableCell();                                
                LinkButton productLink = new LinkButton();                                
                productLink.CssClass = "underline";                
                productLink.CausesValidation = false; 
                productLink.ID = product.Wireless_product_id.ToString();
                productLink.Text = product.Product_name;                
                productLink.Click += new EventHandler(OnProductLinkClick);
                productNameCell.Controls.Add(productLink);
                
                TableCell productPriceCell = new TableCell();
                productPriceCell.Width = Unit.Percentage(10);
                productPriceCell.Text = product.Price.ToString("C");
                productPriceCell.HorizontalAlign = HorizontalAlign.Right;
                
                row.Cells.Add(productNameCell);
                row.Cells.Add(productPriceCell);
                table.Rows.Add(row);
            }
        }

        private void OnProductLinkClick(object sender, EventArgs e)
        {
            int wirelessProductId = int.Parse(((LinkButton) sender).ID);

            IWireless_Products[] mainProducts = Model.GetMainProducts();
            
            foreach (IWireless_Products product in mainProducts)
            {
                if (product.Wireless_product_id == wirelessProductId)
                {
                    Model.SelectedMainProduct = product;
                    break;
                }
            }
            
            IWireless_Products[] optionalProducts = Model.GetOptionalProducts(Model.SelectedMainProduct.Vendor_Name);
            Model.SelectedOptionalProducts = new ArrayList();
            if (optionalProducts.Length > 0)
            {
                Response.Redirect(SiteMap.RDP_SELECT_PRODUCTS_URL);
            } else
            {                
                Response.Redirect(SiteMap.RDP_ORDER_SUMMARY_URL);
            }
        }
    }
}