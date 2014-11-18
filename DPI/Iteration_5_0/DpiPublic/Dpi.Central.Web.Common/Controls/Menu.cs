using System.Collections;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace Dpi.Central.Web.Controls
{
    public class Menu : Control
    {
        #region Helper classes

        public class SubMenu
        {
            public string Url;
            public string ImageUrl;
            public string ImageOnUrl;
            public string Id;
            public string Target = "_self";
            public ArrayList Items = new ArrayList();

            public SubMenu(string id, string url, string imageUrl, string imageOnUrl)
            {
                Id = id;
                Url = url;
                ImageUrl = imageUrl;
                ImageOnUrl = imageOnUrl;
            }

            public string GetLoadingClientScript()
            {
                if (Items.Count == 0) {
                    return string.Empty;
                }

                StringBuilder sb = new StringBuilder();

                sb.Append(string.Format("\twindow.{0} = new Menu(\"root\",192,16,", Id));
                sb.Append("\"Arial, Helvetica, sans-serif\",10,\"#333333\",\"#FFFFFF\",");
                sb.Append("\"#DBDBDB\",\"#DB6C1D\",\"left\",\"middle\",1,0,500,-5,7,");
                sb.Append("true,true,true,0,false,false);\n");

                foreach (SubMenuItem item in Items) {
                    sb.Append(string.Format("\t{0}.addMenuItem(\"{1}\",\"window.open('{2}', '_self');\");\n", Id, item.Title, item.Url));
                }

                sb.Append(string.Format("\t{0}.fontWeight=\"bold\";\n", Id));
                sb.Append(string.Format("\t{0}.hideOnMouseOut=true;\n", Id));
                sb.Append(string.Format("\t{0}.bgColor='#9A9A9A';\n", Id));
                sb.Append(string.Format("\t{0}.menuBorder=1;\n", Id));
                sb.Append(string.Format("\t{0}.menuLiteBgColor='#DADADA';\n", Id));
                sb.Append(string.Format("\t{0}.menuBorderBgColor='#FBF9F8';\n\n", Id));

                return sb.ToString();
            }
        }

        public struct SubMenuItem
        {
            public string Url;
            public string Title;

            public SubMenuItem(string title, string url)
            {
                Url = url;
                Title = title;
            }
        }

        #endregion

        #region Constants

        private const string MENU_KEY = "MENU_KEY";
        private const string MENU_LIB_KEY = "MENU_LIB_KEY";
        private const string MENU_LIB_SRC = "<SCRIPT language=\"JavaScript\" type=\"text/javascript\" src=\"{0}/script/mm_menu.js\"></script>";

        #endregion

        #region Fields

        internal ArrayList SubMenus = new ArrayList();

        #endregion

        #region Protected Methods

        protected override void CreateChildControls() 
        {
            ControlHelper.NeedDhtmlUtils();

            if (!Page.IsClientScriptBlockRegistered(MENU_LIB_KEY)) {
                string src = string.Format(MENU_LIB_SRC, HttpContext.Current.Request.ApplicationPath);
                this.Page.RegisterClientScriptBlock(MENU_LIB_KEY, src);
            }

            if (!Page.IsClientScriptBlockRegistered(MENU_KEY)) {
                string menuScript = GetClientScript();
                this.Page.RegisterClientScriptBlock(MENU_KEY, menuScript);
            }

            HtmlTable cxtTable = CreateCxtTable();

            base.Controls.Add(cxtTable);
        }

        protected override void Render(HtmlTextWriter writer) 
        {
            if (Site != null && Site.DesignMode) {
                writer.RenderBeginTag(HtmlTextWriterTag.Span);

                for (int i = 1; i <= 5; i++) {
                    if (i != 1) {
                        writer.Write("&nbsp|&nbsp");
                    }

                    writer.RenderBeginTag(HtmlTextWriterTag.A);
                    writer.Write("Menu Item " + i.ToString());
                    writer.RenderEndTag(); // A
                }

                writer.RenderEndTag(); // Span
            } else {
                base.Render (writer);
            }
        }

        #endregion

        #region Implementations

        private string GetClientScript()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("<script type=\"text/javascript\">\n<!--\n");

            // LoadMenu function
            sb.Append("function mmLoadMenus()\n{\n\tif (window.mm_menu_become_an_agent)\n\t\treturn;\n\n");
            
            SubMenu loadingSubMenu = null;
            foreach (SubMenu menu in SubMenus) {
                sb.Append(menu.GetLoadingClientScript());
                if (loadingSubMenu == null && menu.Items.Count > 0) {
                    loadingSubMenu = menu;
                }
            }
            
            sb.Append("\t");
            sb.Append(loadingSubMenu.Id);
            sb.Append(".writeMenus();\n}");

            // PushHandler function
            sb.Append("pushHandler(window, \"load\", function(e)\n{\n\tMM_preloadImages(");
            
            foreach (SubMenu menu in SubMenus) {
                sb.Append("\n\t\t'");
                sb.Append(menu.ImageOnUrl);
                sb.Append("',");
            }

            sb.Remove(sb.Length - 1, 1);
            sb.Append(");\n});\n");

            // Invocation LoadMenu function
            sb.Append("mmLoadMenus();\n//-->\n</script>");

            return sb.ToString();
        }

        private HtmlTable CreateCxtTable() 
        {
            HtmlTable cxtTable = new HtmlTable();

            cxtTable.CellPadding = 0;
            cxtTable.CellSpacing = 0;
            cxtTable.Border = 0;

            HtmlTableRow row = new HtmlTableRow();

            foreach (SubMenu subMenu in SubMenus) {
                HtmlAnchor anchor = new HtmlAnchor();
                anchor.HRef = subMenu.Url;
                anchor.Target = subMenu.Target;

                StringBuilder onmouseoverSb = new StringBuilder();
                StringBuilder onmouseoutSb = new StringBuilder();
                
                if (subMenu.Items.Count > 0) {
                    onmouseoverSb.Append("MM_showMenu(window.");
                    onmouseoverSb.Append(subMenu.Id);
                    onmouseoverSb.Append(",0,30,null,'");
                    onmouseoverSb.Append(subMenu.Id);
                    onmouseoverSb.Append("_Image');");

                    onmouseoutSb.Append("MM_startTimeout();");
                }

                onmouseoverSb.Append("MM_swapImage('");
                onmouseoverSb.Append(subMenu.Id);
                onmouseoverSb.Append("_Image','','");
                onmouseoverSb.Append(subMenu.ImageOnUrl);
                onmouseoverSb.Append("',1);");

                onmouseoutSb.Append("MM_swapImgRestore();");

                anchor.Attributes.Add("onmouseover", onmouseoverSb.ToString());
                anchor.Attributes.Add("onmouseout", onmouseoutSb.ToString());

                HtmlImage image = new HtmlImage();
                image.Src = subMenu.ImageUrl;
                image.ID = subMenu.Id + "_Image";
                image.Border = 0;
                image.Attributes.Add("name", image.ID);

                anchor.Controls.Add(image);

                HtmlTableCell subMenuCell = new HtmlTableCell();
                subMenuCell.Controls.Add(anchor);
                row.Cells.Add(subMenuCell);
            }

            // Add trail image.
            HtmlImage trailImage = new HtmlImage();
            trailImage.Src = "~/images/call_us.gif";
            trailImage.Border = 0;
            HtmlTableCell trailCell = new HtmlTableCell();
            trailCell.Controls.Add(trailImage);
            row.Cells.Add(trailCell);

            cxtTable.Rows.Add(row);

            return cxtTable;
        }

        #endregion
    }
}