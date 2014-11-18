using System.Web.UI.HtmlControls;
using Dpi.Central.Web.Controls;

namespace Dpi.Central.Web.Account
{
    public class BaseImageAccountPage : BaseAccountPage
    {
        #region Protected Methods

        protected override void InitLayout()
        {
            HtmlTable cxtTable = new HtmlTable();

            cxtTable.CellPadding = 0;
            cxtTable.CellSpacing = 0;
            cxtTable.Border = 0;
            cxtTable.Height = "100%";

            HtmlTableCell cell = CreateCell(cxtTable, "page_header");
            cell.ColSpan = 2;
            cell.Controls.Add(CreateImage(HEADER_IMG_PATH));
            cell.Controls.Add(Menu);

            HtmlTableRow row = new HtmlTableRow();
            cell = CreateCell(row, "page_left_side_bar_cxt");
            cell.Controls.Add(CreateImage("~/images/about_side_top.jpg"));
            cell = CreateCell(row, "page_cxt");
            cell.Controls.Add(CreateImage("~/images/ppc_top.jpg"));
            HtmlForm form = ControlHelper.GetHtmlForm();
            cell.Controls.Add(form);
            cxtTable.Rows.Add(row);

            row = new HtmlTableRow();
            cell = CreateCell(row, "page_left_side_bar_footer");
            cell.Controls.Add(CreateImage("~/images/about_side_bottom.jpg"));
            cell = CreateCell(row, "page_footer");
            cell.Controls.Add(new Footer());
            cxtTable.Rows.Add(row);

            int cxtTableIndex = Controls.IndexOf(form);
            Controls.AddAt(cxtTableIndex, cxtTable);
        }

        protected override void AddErrorControlToPage() 
        {
            Form.Controls.AddAt(0, ErrorControl);   
        }

        #endregion
    }
}