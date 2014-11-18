using System.Collections;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using DPI.Interfaces;

namespace Dpi.Central.Web.Account.Wireless.Processes.Rsp
{
    public class ProductsTable : Table
    {
        private const int NUMBER_OF_COLUMNS = 3;

        ArrayList pairs = new ArrayList();

        public ProductsTable(IWireless_Products[] optionalProducts, ArrayList selectedProducts, IWireless_Products[] combinableProducts)
        {
            SetTableProperties();

            CreateTableData(optionalProducts);

            bool isAltColor = false;

            CreateGroupHeaderRow("Optional Products");
            
            Hashtable checkedProducts = new Hashtable();
            foreach (IWireless_Products product in selectedProducts) {
                checkedProducts.Add(product.Wireless_product_id, product);
            }

            Hashtable enabledProducts = new Hashtable(combinableProducts.Length);
            foreach (IWireless_Products product in combinableProducts) {
                enabledProducts.Add(product.Wireless_product_id, product);
            }

            foreach (IWireless_Products product in optionalProducts) {
                bool @checked = checkedProducts.ContainsKey(product.Wireless_product_id);
                bool @enabled = @checked || enabledProducts.ContainsKey(product.Wireless_product_id);

                CreateProductRow(product, isAltColor, @checked, @enabled);

                isAltColor = !isAltColor;                                   
            }            
        }

        private void SetTableProperties()
        {
            this.BorderColor = Color.Gainsboro;
            this.BorderWidth = 0;
            this.CellPadding = 0;
            this.CellSpacing = 0;
            this.GridLines = GridLines.Both;
            this.Width = Unit.Percentage(100.0);
        }

        private void CreateTableData(IWireless_Products[] optionalProducts)
        {
            foreach (IWireless_Products product in optionalProducts) {
                pairs.Add(new Pair(product.Product_name, product.Price));
            }
        }

        private void CreateGroupHeaderRow(string groupName)
        {
            TableRow row = new TableRow();

            row.BackColor = Color.Chocolate;

            TableCell headerCell = new TableCell();

            headerCell.ForeColor = Color.White;
            headerCell.ColumnSpan = NUMBER_OF_COLUMNS;
            headerCell.Text = groupName;
            headerCell.HorizontalAlign = HorizontalAlign.Center;

            row.Cells.Add(headerCell);

            this.Rows.Add(row);
        }

        private void CreateProductRow(IWireless_Products product, bool isAltColor, bool isChecked, bool enabled)
        {
            TableRow firstRow = new TableRow();
            
            firstRow.BackColor = isAltColor ? Color.WhiteSmoke : Color.White;

            TableCell nameCell = CreateProductNameCell(product);
            TableCell checkBoxCell = CreateCheckBoxCell(product, isChecked, enabled);
            TableCell priceCell = CreatePriceCell(product.Price);

            firstRow.Cells.Add(checkBoxCell);
            firstRow.Cells.Add(nameCell);
            firstRow.Cells.Add(priceCell);
            
            this.Rows.Add(firstRow);
        }

        private TableCell CreateCheckBoxCell(IWireless_Products product, bool isChecked, bool enabled)
        {
            TableCell cell = new TableCell();
            
            CheckBox checkBox = new CheckBox();

            checkBox.ID = product.Wireless_product_id.ToString();
            checkBox.Checked = isChecked;
            checkBox.Enabled = enabled;
            checkBox.Attributes.Add("onclick", "javascript:OnServiceChanged('" + checkBox.ID + "')");

            cell.Controls.Add(checkBox);

            return cell;
        }

        private TableCell CreateProductNameCell(IWireless_Products product)
        {
            TableCell cell = new TableCell();
            cell.Wrap = false;

            HtmlGenericControl productName = new HtmlGenericControl(HtmlTextWriterTag.Span.ToString());
            productName.ID = "span_pn_" + product.Wireless_product_id;
            productName.InnerText = product.Product_name;

            HtmlImage asterisk = new HtmlImage();
            asterisk.ID = "img_ast_pn_" + product.Wireless_product_id;
            asterisk.Src = "../../images/asterisk.gif";
            asterisk.Style.Add("display", "none");

            cell.Controls.Add(productName);
            cell.Controls.Add(asterisk);

            return cell;
        }

        private TableCell CreatePriceCell(decimal price)
        {
            TableCell cell = new TableCell();

            cell.Text = price.ToString("C");
            cell.HorizontalAlign = HorizontalAlign.Right;

            return cell;
        }
    }
}