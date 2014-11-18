using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Dpi.Central.Web.Account.AccountSetup
{
    public class PackageDetails : UserControl
    {
        public delegate void PackageSelectHandler(object sender, ServicePackageInfo package);
        public event PackageSelectHandler Select;

        #region Web Form Designer generated code

        protected Table m_tblFeatures;
        protected Label m_lblPackageName;
        protected ImageButton m_btnSelect;
        protected System.Web.UI.WebControls.Label m_lblPrice;
        protected System.Web.UI.HtmlControls.HtmlGenericControl spnPpdNote;
        protected HtmlTable m_tblMain;

        protected override void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        ///		Required method for Designer support - do not modify
        ///		the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.m_btnSelect.Click += new System.Web.UI.ImageClickEventHandler(this.m_btnSelect_Click);
            this.Load += new System.EventHandler(this.OnPageLoad);

        }

        #endregion

        #region Properties

        private ServicePackageInfo m_package;

        public ServicePackageInfo Package
        {
            get { return m_package; }
            set { m_package = value; }
        }

        #endregion

        #region OnPageLoad

        private void OnPageLoad(object sender, EventArgs e)
        {
            if (m_package == null) {
                return;
            }

            if (m_package.PackageName != null) {
                m_lblPackageName.Text = m_package.PackageName;
            } else {
                m_lblPackageName.Text = "Unknown";
            }

            m_lblPrice.Text = m_package.Price.ToString("C");

            //Test	        	        
            //	        ArrayList myTest = new ArrayList(m_package.Features);
            //	        myTest.Add("                                        ");
            //	        myTest.Add("                                        ");
            //	        myTest.Add("                                        ");
            //	        m_package.Features = (string[]) myTest.ToArray(typeof (string));
            //Test

            if (m_package.Features != null) {
                foreach (string feature in m_package.Features) {
                    TableRow newRow = new TableRow();

                    TableCell cell1 = new TableCell();
                    cell1.VerticalAlign = VerticalAlign.Top;
                    cell1.Text = "<IMG SRC=\"../images/package_ci.gif\" ALIGN=\"MIDDLE\" WIDTH=\"11\" HEIGHT=\"14\" BORDER=\"0\">";

                    TableCell cell2 = new TableCell();
                    cell2.Text = "&nbsp;";

                    TableCell cell3 = new TableCell();
                    cell3.CssClass = "package_text";
                    cell3.Text = feature;

                    newRow.Cells.Add(cell1);
                    newRow.Cells.Add(cell2);
                    newRow.Cells.Add(cell3);

                    m_tblFeatures.Rows.Add(newRow);
                }
            }

            if (((BaseAccountSetupPage)Page).Model.IsProductLifeLine(m_package.Package)) {
                spnPpdNote.InnerHtml = "&nbsp;";
            }
        }

        #endregion

        #region OnSelectClick

        private void m_btnSelect_Click(object sender, ImageClickEventArgs e)
        {
            if (Select != null) {
                this.Select(this, m_package);
            }
        }

        #endregion
    }
}