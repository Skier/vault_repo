using System.Collections;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using DPI.Interfaces;

namespace Dpi.Central.Web.Account.AccountSetup
{
    public class ProductsTable : Table
    {
        private const int NUMBER_OF_COLUMNS = 3;
        private const string NOTE_CELL_CSS_CLASS = "midgray_small";
        //private const string NOTE_TEXT = "Click on the feature/service name to view a description";
        private const string NOTE_MARK_IMAGE_URL = "../images/asterisk.gif";

        private AccountSetupModel _model;

        /// <summary>
        /// Key - group name; value - list of subclasses names in the group.
        /// </summary>
        private IDictionary _groupToSubClassesMap;

        public ProductsTable(AccountSetupModel model)
        {
            _model = model;
            
            SetTableProperties();

            CreateTableData();

            bool isAltColor = false;

            foreach (string group in _groupToSubClassesMap.Keys) {
                if (!_model.IsGroupEnabled(group)) {
                    continue;
                }

                CreateGroupHeaderRow(group);

                IDictionary subClassToProductsMap = (IDictionary) _groupToSubClassesMap[group];
                foreach (string subClass in subClassToProductsMap.Keys) {
                    IList productsInSubClass = (IList) subClassToProductsMap[subClass];
                    foreach (IProdPrice product in productsInSubClass) {
                        if (_model.IsProductVisibleInList(product)) {
                            CreateProductRow(product, isAltColor);
                            isAltColor = !isAltColor;
                        }
                    }
                }
            }

            CreateNoteRow();
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

        private void CreateTableData()
        {
            IProdPrice[] products = _model.GetServices();

            _groupToSubClassesMap = new Hashtable();

            foreach (IProdPrice product in products) {
                string group = product.ProdType;
                string subClass = product.ProdSubclass;

                if (!_groupToSubClassesMap.Contains(group)) {
                    _groupToSubClassesMap.Add(group, new Hashtable());
                }

                IDictionary subClassesToProductsMap = (IDictionary) _groupToSubClassesMap[group];
                if (!subClassesToProductsMap.Contains(subClass)) {
                    subClassesToProductsMap.Add(subClass, new ArrayList());
                }

                IList productsInSubClass = (IList) subClassesToProductsMap[subClass];
                if (!productsInSubClass.Contains(product)) {
                    productsInSubClass.Add(product);
                }
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

        private void CreateProductRow(IProdPrice product, bool isAltColor)
        {
            TableRow firstRow = new TableRow();
            TableRow secondRow = new TableRow();

            secondRow.BackColor = firstRow.BackColor = isAltColor ? Color.WhiteSmoke : Color.White;

            TableCell descriptionCell = CreateProductDescriptionCell(product);
            TableCell nameCell = CreateProductNameCell(product, descriptionCell.Controls[0]);
            TableCell checkBoxCell = CreateCheckBoxCell(product);
            TableCell priceCell = CreatePriceCell(product);

            firstRow.Cells.Add(checkBoxCell);
            firstRow.Cells.Add(nameCell);
            firstRow.Cells.Add(priceCell);
            secondRow.Cells.Add(descriptionCell);

            this.Rows.Add(firstRow);
            this.Rows.Add(secondRow);
        }

        private void CreateNoteRow()
        {
            TableRow noteRow = new TableRow();

            TableCell noteCell = CreateNoteCell();

            noteRow.Cells.Add(noteCell);

            this.Rows.Add(noteRow);
        }

        private TableCell CreateCheckBoxCell(IProdPrice product)
        {
            TableCell cell = new TableCell();

            CheckBox checkBox;

            if (IsMultipleProductsInSubClass(product)) {
                checkBox = new RadioButton();
                ((RadioButton) checkBox).GroupName = product.ProdSubclass;
            } else {
                checkBox = new CheckBox();
            }

            checkBox.ID = product.ProdId.ToString();
            checkBox.Enabled = _model.IsProductEnabledOnUI(product.ProdId);
            checkBox.Attributes.Add("onclick", "javascript:OnServiceChanged('" + checkBox.ID + "')");

            if (product.ProdSelState == ProdSelectionState.Selected) {
                checkBox.Checked = true;
            } else if (product.ProdSelState == ProdSelectionState.Available) {
                checkBox.Checked = false;
            }

            cell.Controls.Add(checkBox);

            return cell;
        }

        private TableCell CreateProductNameCell(IProdPrice product, Control descriptionContainer)
        {
            TableCell cell = new TableCell();

            cell.Wrap = false;

            HtmlAnchor anchor = new HtmlAnchor();

            anchor.ID = "prod_name_" + product.ProdId;
            anchor.HRef = "javascript:void(0)";
            anchor.InnerHtml = product.BillText;
            anchor.Attributes.Add("class", "underline");
            anchor.Attributes.Add("onclick", "ToggleProductDescription('" + descriptionContainer.ClientID + "', 0.5);");

            if ((product.StartServMon == 2) && (product.UnitPrice > 0m)) {
                anchor.InnerHtml += " (First month free)";
            }

            if ((product.StartServMon == 3) && (product.UnitPrice > 0m)) {
                anchor.InnerHtml += " (Two months free)";
            }

            if ((product.StartServMon == 4) && (product.UnitPrice > 0m)) {
                anchor.InnerHtml += " (Three months free)";
            }

            if ((product.StartServMon > 4) && (product.UnitPrice > 0m)) {
                anchor.InnerHtml += string.Format(" ({0} months free)", product.StartServMon);
            }

            HtmlImage noteMark = new HtmlImage();

            noteMark.Src = NOTE_MARK_IMAGE_URL;

            cell.Controls.Add(anchor);
            cell.Controls.Add(noteMark);

            return cell;
        }

        private TableCell CreateProductDescriptionCell(IProdPrice product)
        {
            TableCell cell = new TableCell();

            cell.ColumnSpan = NUMBER_OF_COLUMNS;
            cell.CssClass = "midgray_normal";

            HtmlGenericControl container = new HtmlGenericControl("div");

            container.ID = "prod_desc_" + product.ProdId;
            container.Style.Add("display", "none");
            container.InnerHtml = product.Description;

            cell.Controls.Add(container);

            return cell;
        }

        private TableCell CreatePriceCell(IProdPrice product)
        {
            TableCell cell = new TableCell();

            cell.Text = product.UnitPrice.ToString("C");
            cell.HorizontalAlign = HorizontalAlign.Right;

            return cell;
        }

        private TableCell CreateNoteCell()
        {
            TableCell cell = new TableCell();

            cell.ColumnSpan = NUMBER_OF_COLUMNS;
            cell.CssClass = NOTE_CELL_CSS_CLASS;
            /*
            HtmlImage noteMark = new HtmlImage();

            noteMark.Src = NOTE_MARK_IMAGE_URL;

            cell.Controls.Add(noteMark);
            cell.Controls.Add(new LiteralControl(NOTE_TEXT));
            */
            return cell;
        }
        
        private bool IsMultipleProductsInSubClass(IProdPrice product) 
        {
            string group = product.ProdType, subClass = product.ProdSubclass;

            if (_groupToSubClassesMap.Contains(group)) {
                IDictionary subClassToProductsMap = (IDictionary) _groupToSubClassesMap[group];
                if (subClassToProductsMap.Contains(subClass)) {
                    return ((IList) subClassToProductsMap[subClass]).Count > 1;
                }
            }

            return false;
        }
    }
}