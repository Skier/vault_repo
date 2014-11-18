using System.Collections;
using System.Web.UI.WebControls;
using DPI.Interfaces;

namespace Dpi.Central.Web.Account.AccountSetup
{
	public class FeaturesTable : Table
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

            public DataItem(IProdPrice product)
            {
                Prefix = PREFIX;
                Id = product.ProdId;
                Name = AccountSetupModel.GetBillText(product);
                NameCssClass = NAME_CSS_CLASS;
                Price = product.StartServMon != 1 ? 0m.ToString("C") : product.UnitPrice.ToString("C");
                PriceCssClass = PRICE_CSS_CLASS;
                RemoveCssClass = REMOVE_CSS_CLASS;
            }
        }

        private AccountSetupModel _model;
        private IList _dataItems;

		public FeaturesTable(AccountSetupModel model)
		{
            _model = model;

            CreateTableData();

            foreach (DataItem item in _dataItems) {
                CreateFeatureRow(item);
            }
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

            if (_model.IsProductEnabledOnUI(item.Id)) {
                removeCell.Text = string.Format("<a href=\"javascript:RemoveRow('{0}')\">remove</a>", item.Id);
            }

            TableCell priceCell = new TableCell();
            priceCell.Text = item.Price;
            priceCell.CssClass = item.PriceCssClass;

            row.Cells.Add(nameCell);
            row.Cells.Add(removeCell);
            row.Cells.Add(priceCell);

            this.Rows.Add(row);
        }

        private void CreateTableData()
        {
            _dataItems = new ArrayList();

            IProdPrice[] products = _model.GetServices();

            foreach (IProdPrice product in products) {
                if (IsAvailableInFeaturesTable(product)) {
                    DataItem item = new DataItem(product);
                    _dataItems.Add(item);
                }
            }
        }

        /// <summary>
        /// TODO: SR - move to the model.
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        private bool IsAvailableInFeaturesTable(IProdPrice product)
        {
            return product.ProdType != "Local Service" && product.ProdSelState == ProdSelectionState.Selected && product.UnitPrice != decimal.Zero;
        }
	}
}
