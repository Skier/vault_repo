using System.Collections;
using System.Web.UI.WebControls;
using DPI.Interfaces;

namespace Dpi.Central.Web.Account.Wireless.Processes.Rsp
{
	public class SelectedProductsTable : Table
	{
        public class DataItem
        {
            private const string PREFIX = "feature_row_";
            private const string NAME_CSS_CLASS = "midgray_normal";
            private const string PRICE_CSS_CLASS = "feature_price";
            private const string REMOVE_CSS_CLASS = "subitems";

            public readonly string Prefix;
            public readonly int Id;
            public readonly string Name;
            public readonly string NameCssClass;
            public readonly string Price;
            public readonly string PriceCssClass;
            public readonly string RemoveCssClass;

            public DataItem(int id, string name, decimal price)
            {
                Prefix = PREFIX;
                Id = id;
                Name = name;
                NameCssClass = NAME_CSS_CLASS;
                Price = price.ToString("C");
                PriceCssClass = PRICE_CSS_CLASS;
                RemoveCssClass = REMOVE_CSS_CLASS;
            }
        }

        private IList _dataItems;

		public SelectedProductsTable(ArrayList products)
		{
            CreateTableData(products);
		    CreateStubRow();

            foreach (DataItem item in _dataItems) {
                CreateFeatureRow(item);
            }
		}

	    private void CreateStubRow()
	    {
            TableRow row = new TableRow();
            row.ID = "stub01";

            TableCell nameCell = new TableCell();            
            nameCell.Width = Unit.Percentage(100);
            nameCell.CssClass = "midgray_normal";

            TableCell removeCell = new TableCell();
            removeCell.CssClass = "subitems";

            TableCell priceCell = new TableCell();
            priceCell.CssClass = "feature_price";

            row.Cells.Add(nameCell);
            row.Cells.Add(removeCell);
            row.Cells.Add(priceCell);

            Rows.Add(row);	        
	    }
	    
        private void CreateFeatureRow(DataItem item)
        {
            TableRow row = new TableRow();
            row.ID = item.Prefix + item.Id;

            TableCell nameCell = new TableCell();
            nameCell.Text = item.Name;
            nameCell.Width = Unit.Percentage(100);
            nameCell.CssClass = item.NameCssClass;

            TableCell removeCell = new TableCell();
            removeCell.CssClass = item.RemoveCssClass;

//            if (_model.IsProductEnabledOnUI(item.Id)) {
                removeCell.Text = string.Format("<a href=\"javascript:RemoveRow('{0}')\">remove</a>", item.Id);
//            }

            TableCell priceCell = new TableCell();
            priceCell.Text = item.Price;
            priceCell.CssClass = item.PriceCssClass;

            row.Cells.Add(nameCell);
            row.Cells.Add(removeCell);
            row.Cells.Add(priceCell);

            this.Rows.Add(row);
        }

        private void CreateTableData(ArrayList products)
        {
            _dataItems = new ArrayList();

            foreach (IWireless_Products product in products)
            {
                _dataItems.Add(new DataItem(product.Wireless_product_id, 
                    product.Product_name, product.Price));
            }
        }

//        /// <summary>
//        /// TODO: SR - move to the model.
//        /// </summary>
//        /// <param name="product"></param>
//        /// <returns></returns>
//        private bool IsAvailableInFeaturesTable(IProdPrice product)
//        {
//            return product.ProdType != "Local Service" && product.ProdSelState == ProdSelectionState.Selected && product.UnitPrice != decimal.Zero;
//        }
	}
}
