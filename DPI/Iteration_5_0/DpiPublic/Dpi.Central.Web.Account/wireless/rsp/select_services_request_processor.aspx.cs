using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Web;
using System.Xml;
using DPI.Interfaces;

namespace Dpi.Central.Web.Account.Wireless.Processes.Rsp
{
    public class SelectServicesRequestProcessor : RechargeServicePlanBasePage
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
            IWireless_Products[] optionalProducts = Model.GetOptionalProducts(Model.SelectedMainProduct.Vendor_Name);
            IWireless_Products currentProduct = null;
            foreach (IWireless_Products product in optionalProducts)
            {
                if (product.Wireless_product_id.ToString() == productId)
                {
                    currentProduct = product;
                    break;
                }
            }
                        
            ArrayList removedProducts = new ArrayList();
            ArrayList addedProducts = new ArrayList();            
            
            if (operationType == "Add") {
                Model.SelectedOptionalProducts.Add(currentProduct);
                addedProducts.Add(currentProduct);
            } else {
                foreach (IWireless_Products product in Model.SelectedOptionalProducts)
                {
                    if (product.Wireless_product_id == currentProduct.Wireless_product_id)
                    {
                        Model.SelectedOptionalProducts.Remove(product);
                        removedProducts.Add(currentProduct);
                        break;
                    }
                }                                                
            }
            
            //Create document                                    	        	        
            XmlDocument document = new XmlDocument();
            XmlElement root = document.CreateElement("root");
            document.AppendChild(root);
                        
            XmlElement removedFeatures = document.CreateElement("RemovedFeatures");
            root.AppendChild(removedFeatures);

            foreach (IWireless_Products prod in removedProducts) {
                XmlElement feature = document.CreateElement("Feature");
                removedFeatures.AppendChild(feature);

                XmlElement id = document.CreateElement("Id");
                feature.AppendChild(id);

                id.InnerText = prod.Wireless_product_id.ToString();
            }

            XmlElement addedFeatures = document.CreateElement("AddedFeatures");
            root.AppendChild(addedFeatures);

            foreach (IWireless_Products prod in addedProducts) {
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
                
                id.InnerText = prod.Wireless_product_id.ToString();
                name.InnerText = prod.Product_name;
                price.InnerText = prod.Price.ToString("C");
                isRemoveAllowed.InnerText = "true";
                prefix.InnerText = "feature_row_";
                nameCssClass.InnerText = "midgray_normal";
                priceCssClass.InnerText = "feature_price";
                removeCssClass.InnerText = "subitems";
            }

            IWireless_Products[] combinableProducts = ProductCompositionResolver.GetCombinableProducts(Map, Model.SelectedMainProduct, (IWireless_Products[])Model.SelectedOptionalProducts.ToArray(typeof(IWireless_Products)));
            
            XmlElement combinableProductsElement = document.CreateElement("CombinableProducts");
            root.AppendChild(combinableProductsElement);

            foreach (IWireless_Products product in combinableProducts) {
                XmlElement combinableProductElement = document.CreateElement("CombinableProduct");
                combinableProductsElement.AppendChild(combinableProductElement);

                XmlElement id = document.CreateElement("Id");
                combinableProductElement.AppendChild(id);

                id.InnerText = product.Wireless_product_id.ToString();
            }

            XmlElement optionalProductsElement = document.CreateElement("OptionalProducts");
            root.AppendChild(optionalProductsElement);

            foreach (IWireless_Products product in optionalProducts) {
                XmlElement optionalProductElement = document.CreateElement("OptionalProduct");
                optionalProductsElement.AppendChild(optionalProductElement);

                XmlElement id = document.CreateElement("Id");
                optionalProductElement.AppendChild(id);

                id.InnerText = product.Wireless_product_id.ToString();
            }

            bool finalProductExists = Model.CheckFinalProductExistence();

            XmlElement finalProductExistsElement = document.CreateElement("FinalProductExists");
            finalProductExistsElement.InnerText = finalProductExists.ToString().ToLower();
            root.AppendChild(finalProductExistsElement);

            if (!finalProductExists) {
                IWireless_Products[] minimalCombinableProducts = ProductCompositionResolver.GetMinimalCombinableProducts(Map, Model.SelectedMainProduct, (IWireless_Products[])Model.SelectedOptionalProducts.ToArray(typeof(IWireless_Products)));

                XmlElement minimalCombinableProductsElement = document.CreateElement("MinimalCombinableProducts");
                root.AppendChild(minimalCombinableProductsElement);

                foreach (IWireless_Products product in minimalCombinableProducts) {
                    XmlElement minimalCombinableProductElement = document.CreateElement("MinimalCombinableProduct");
                    minimalCombinableProductsElement.AppendChild(minimalCombinableProductElement);

                    XmlElement id = document.CreateElement("Id");
                    minimalCombinableProductElement.AppendChild(id);

                    id.InnerText = product.Wireless_product_id.ToString();
                }
            }

            bool isPinAvailable = finalProductExists && Model.IsPinAvailable();
            XmlElement isPinAvailableElement = document.CreateElement("IsPinAvailable");
            isPinAvailableElement.InnerText = isPinAvailable.ToString().ToLower();
            root.AppendChild(isPinAvailableElement);

            XmlElement featuresTotalNode = document.CreateElement("FeaturesTotal");
            root.AppendChild(featuresTotalNode);

            XmlElement grandTotalNode = document.CreateElement("GrandTotal");
            root.AppendChild(grandTotalNode);

            decimal quoteTotal = decimal.Zero;
            foreach (IWireless_Products product in Model.SelectedOptionalProducts)
            {
                quoteTotal += product.Price;
            }
            
            decimal grandTotal = quoteTotal + Model.SelectedMainProduct.Price;

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