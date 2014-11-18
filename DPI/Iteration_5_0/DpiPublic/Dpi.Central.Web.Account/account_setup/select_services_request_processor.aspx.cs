using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Web;
using System.Xml;
using DPI.Interfaces;

namespace Dpi.Central.Web.Account.AccountSetup
{
    public class SelectServicesRequestProcessor : BaseAccountSetupPage
    {
        #region Web Form Designer generated code

        protected override void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Load += new EventHandler(this.OnPageLoad);
        }

        #endregion

        #region OnPageLoad

        private void OnPageLoad(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                string operationType = Request["Operation"];
                string productId = Request["ServiceId"];

                Response.Clear();
                Response.Cache.SetCacheability(HttpCacheability.NoCache);

                string xml = GetXml(productId, operationType);
                xml = xml.Replace("utf-16", "utf-8");
                WriteLog("OutputXML - " + xml);

                Response.Clear();
                Response.ContentType = "text/xml";

                Response.Write(xml);
                Response.End();
            } else {
                Response.Clear();
                Response.End();
            }
        }

        #endregion

        #region GetXml

        private string GetXml(string productId, string operationType)
        {
            //Prepare modification list
            ArrayList wasSelectedList = new ArrayList();
            foreach (IProdPrice prod in Model.GetServices()) {
                if (prod.ProdSelState == ProdSelectionState.Selected) {
                    wasSelectedList.Add(true);
                } else {
                    wasSelectedList.Add(false);
                }
            }

            IProdPrice[] products;
            if (operationType == "Add") {
                products = Model.AddService(int.Parse(productId));
            } else {
                products = Model.RemoveService(int.Parse(productId));
            }

            ArrayList removedProducts = new ArrayList();
            ArrayList addedProducts = new ArrayList();

            for (int i = 0; i < products.Length; i++) {
                if ((bool) wasSelectedList[i]
                    && products[i].ProdSelState != ProdSelectionState.Selected) {
                    removedProducts.Add(products[i]);
                }

                if (!(bool) wasSelectedList[i]
                    && products[i].ProdSelState == ProdSelectionState.Selected
                    && products[i].UnitPrice != decimal.Zero) {
                    addedProducts.Add(products[i]);
                }
            }

            //Create document                                    	        	        
            XmlDocument document = new XmlDocument();
            XmlElement root = document.CreateElement("root");
            document.AppendChild(root);

            XmlElement featuresStatus = document.CreateElement("FeaturesStatus");
            root.AppendChild(featuresStatus);

            foreach (IProdPrice prod in products) {
                XmlElement feature = document.CreateElement("Feature");
                featuresStatus.AppendChild(feature);

                XmlElement id = document.CreateElement("Id");
                XmlElement isChecked = document.CreateElement("IsChecked");
                XmlElement isEnabled = document.CreateElement("IsEnabled");

                feature.AppendChild(id);
                feature.AppendChild(isChecked);
                feature.AppendChild(isEnabled);

                id.InnerText = prod.ProdId.ToString();
                isChecked.InnerText = (prod.ProdSelState == ProdSelectionState.Selected).ToString().ToLower();
                isEnabled.InnerText = Model.IsProductEnabledOnUI(prod.ProdId).ToString().ToLower();
            }

            XmlElement removedFeatures = document.CreateElement("RemovedFeatures");
            root.AppendChild(removedFeatures);

            foreach (IProdPrice prod in removedProducts) {
                XmlElement feature = document.CreateElement("Feature");
                removedFeatures.AppendChild(feature);

                XmlElement id = document.CreateElement("Id");
                feature.AppendChild(id);

                id.InnerText = prod.ProdId.ToString();
            }

            XmlElement addedFeatures = document.CreateElement("AddedFeatures");
            root.AppendChild(addedFeatures);

            foreach (IProdPrice prod in addedProducts) {
                XmlElement feature = document.CreateElement("Feature");
                addedFeatures.AppendChild(feature);

                XmlElement id = document.CreateElement("Id");
                XmlElement name = document.CreateElement("Name");
                XmlElement price = document.CreateElement("Price");
                XmlElement isRemoveAllowed = document.CreateElement("IsRemoveAllowed");
                XmlElement prefix = document.CreateElement("Prefix");
                XmlElement nameCssClass = document.CreateElement("NameCssClass");
                XmlElement priceCssClass = document.CreateElement("PriceCssClass");
                XmlElement removeCssClass = document.CreateElement("RemoveCssClass");

                feature.AppendChild(id);
                feature.AppendChild(name);
                feature.AppendChild(price);
                feature.AppendChild(isRemoveAllowed);
                feature.AppendChild(prefix);
                feature.AppendChild(nameCssClass);
                feature.AppendChild(priceCssClass);
                feature.AppendChild(removeCssClass);

                FeaturesTable.DataItem featureData = new FeaturesTable.DataItem(prod);

                id.InnerText = featureData.Id.ToString();
                name.InnerText = featureData.Name;
                price.InnerText = featureData.Price;
                isRemoveAllowed.InnerText = Model.IsProductEnabledOnUI(prod.ProdId).ToString().ToLower();
                prefix.InnerText = featureData.Prefix;
                nameCssClass.InnerText = featureData.NameCssClass;
                priceCssClass.InnerText = featureData.PriceCssClass;
                removeCssClass.InnerText = featureData.RemoveCssClass;
            }

            XmlElement featuresTotalNode = document.CreateElement("FeaturesTotal");
            root.AppendChild(featuresTotalNode);

            XmlElement grandTotalNode = document.CreateElement("GrandTotal");
            root.AppendChild(grandTotalNode);

            decimal quoteTotal = Model.GetQuoteTotal();
            decimal grandTotal = quoteTotal + Model.Info.Package.Package.UnitPrice;

            featuresTotalNode.InnerText = "Total Upgrades: " + quoteTotal.ToString("C");
            grandTotalNode.InnerText = "Grand Total: " + grandTotal.ToString("C");

            StringBuilder stringBuilder = new StringBuilder();
            StringWriter writer = new StringWriter(stringBuilder);

            document.Save(writer);
            return stringBuilder.ToString();
        }

        #endregion
    }
}