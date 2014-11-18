using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using DPI.ClientComp;
using DPI.Components;
using DPI.Interfaces;
using DPI.Services;

namespace Dpi.Central.Web.Account.AccountSetup
{
    /// <summary>
    /// Summary description for select_services.
    /// </summary>
    public class SelectPackages : BaseAccountSetupPage 
    {
        private const string ROLE = "Agent";
        private const string STORE_CODE = "DPI-Web";
        private const string CRITERIA = "Local Conversion"; //Only if user wants to convert his phone
        
        protected System.Web.UI.WebControls.ImageButton btnPrevious;
        protected System.Web.UI.WebControls.Table m_tblPackages;       
        protected PackageDetails m_packageDetails0;
        protected PackageDetails m_packageDetails1;
        protected PackageDetails m_packageDetails2;
        protected PackageDetails m_packageDetails3;
        protected PackageDetails m_packageDetails4;
        protected PackageDetails m_packageDetails5;
        protected PackageDetails m_packageDetails6;
        protected PackageDetails m_packageDetails7;
        protected PackageDetails m_packageDetails8;
        protected PackageDetails m_packageDetails9;
        protected Dpi.Central.Web.Controls.Footer _footer;
        
        
        protected PackageDetails[] m_packageDetailList;
    
        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e) 
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            m_packageDetailList = new PackageDetails[10];
            m_packageDetailList[0] = m_packageDetails0;
            m_packageDetailList[1] = m_packageDetails1;
            m_packageDetailList[2] = m_packageDetails2;
            m_packageDetailList[3] = m_packageDetails3;
            m_packageDetailList[4] = m_packageDetails4;
            m_packageDetailList[5] = m_packageDetails5;
            m_packageDetailList[6] = m_packageDetails6;
            m_packageDetailList[7] = m_packageDetails7;
            m_packageDetailList[8] = m_packageDetails8;
            m_packageDetailList[9] = m_packageDetails9;
                        
            InitializeComponent();
            base.OnInit(e);
        }
		
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() 
        {    
            this.btnPrevious.Click += new System.Web.UI.ImageClickEventHandler(this.OnPreviousClick);
            this.Load += new System.EventHandler(this.OnPageLoad);

            foreach (PackageDetails packageDetails in m_packageDetailList)
            {
                packageDetails.Select += new PackageDetails.PackageSelectHandler(OnPackageSelect);
            }

        }

        #endregion

        #region OnPageLoad

        private void OnPageLoad(object sender, EventArgs e) {                     
            InitPackageControls();
        }

        #endregion

        #region OnPreviousClick

        private void OnPreviousClick(object sender, ImageClickEventArgs e) {
            Model.Info.IsDoNotResetModelOnFirstStep = true;
            Response.Redirect(SiteMap.NEW_ACC_SELECT_PROVIDER_URL);
        }

        #endregion

        #region Package helpers

        private decimal GetPackagePrice(string[][] matrix, IKeyVal[] discounts, IProdPrice[] products, int packageNumber)
        {
            IProdPrice thisPackage = GetPackage(matrix, products, packageNumber);

            foreach (IKeyVal keyVal in discounts)
            {
                if (thisPackage.ProdId.ToString() == keyVal.Key)
                {                     
                    decimal result = thisPackage.UnitPrice + decimal.Parse(keyVal.Val);
                    return result;
                }
            }
            
            return thisPackage.UnitPrice;
        }
        
        private int GetPackagesCount (string[][] matrix)
        {
            return matrix[0].Length - 1;
        }

        private string GetPackageName (string[][] matrix, int packageNumber) {
            return matrix[1][packageNumber + 1];
        }
        
        private IProdPrice GetPackage (string[][] matrix, IProdPrice[] products, int packageNumber) {
            foreach (IProdPrice product in products)
            {
                if (product.ProdId.ToString() == matrix[0][packageNumber + 1])
                    return product;
            }
            
            throw new Exception("No Product Found");
        }
        
        
        private string[] GetPackageFeatures (string[][] matrix, IProdPrice[] products, int packageNumber)
        {
            ArrayList features = new ArrayList();
            if (matrix[3][packageNumber + 1].Trim() != string.Empty)
                features.Add(matrix[3][packageNumber + 1] + " " + matrix[3][0]);
            
            if (matrix[4][packageNumber + 1].Trim() != string.Empty)
                features.Add(matrix[4][0] + " - " + matrix[4][packageNumber + 1]);
            
            for (int i = 5; i < matrix.Length; i++)
            {
                if (matrix[i][packageNumber + 1].Trim() == "true")
                {
                    features.Add(matrix[i][0]);
                }
            }

            IProdPrice thisProduct = GetPackage(matrix, products, packageNumber);
            features.Add("First Month Rate: " + thisProduct.UnitPrice.ToString("C"));
                        
            return (string[]) features.ToArray(typeof(string));
        }

        #endregion

        #region InitPackageControls

        private void InitPackageControls()
        {            
            IKeyVal[] discounts = ProdSvc.GetAllProdDiscounts(Map, OrderType.New.ToString());
            IProdPrice[] products;
            if (Model.Info.IsMoveExistingPhone)
                products = ProdSvc.GetTopProd(Map, Model.Info.Provider, Model.Info.Zip, ROLE, STORE_CODE, CRITERIA);
            else
                products = ProdSvc.GetTopProd(Map, Model.Info.Provider, Model.Info.Zip, ROLE, STORE_CODE, null);
            
            if (Model.Info.IsQualifyForLowIncomeNull || !Model.Info.IsQualifyForLowIncome)
            {
                ArrayList filteredProducts = new ArrayList();
                foreach (IProdPrice product in products)
                {                    
                    if (!Model.IsProductLifeLine(product))
                        filteredProducts.Add(product);                    
                }
                
                products = (IProdPrice[]) filteredProducts.ToArray(typeof (ProdPrice));
            }
                                    
            string[][] matrix = FeatureMatrixAdapter.GetMatrix(products);
            
            int packagesCount = GetPackagesCount(matrix);
            
            if (packagesCount > m_packageDetailList.Length)
                throw new Exception("Too many packages");
            
            if (packagesCount < m_packageDetailList.Length)
            {
                for (int i = packagesCount; i < m_packageDetailList.Length; i++) {
                    m_packageDetailList[i].Visible = false;
                }                
            }
            
            for (int i = 0; i <= packagesCount - 1; i++)
            {
                ServicePackageInfo package = new ServicePackageInfo();
                package.PackageName = GetPackageName(matrix, i);
                package.Price = GetPackagePrice(matrix, discounts, products, i);
                package.Features = GetPackageFeatures(matrix, products, i);                
                package.Package = GetPackage(matrix, products, i);
                
                m_packageDetailList[i].Package = package;                
            }
         
        }

        #endregion

        #region OnPackageSelect

        private void OnPackageSelect(object sender, ServicePackageInfo package) {
            Model.Info.Package = package;
            Model.ResetServiceList();
            Response.Redirect(SiteMap.NEW_ACC_SELECT_SERVICES_URL);            
        }

        #endregion

    }
}
