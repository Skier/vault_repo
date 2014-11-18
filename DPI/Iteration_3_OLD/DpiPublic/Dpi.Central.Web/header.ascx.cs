using System;
using System.Collections;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Dpi.Central.Web.Controls;

namespace Dpi.Central.Web
{
    public class HeaderUserControl : UserControl
    {
        #region Helper Classes

        private class MenuBar
        {
            public ArrayList Menus = new ArrayList();

            public string GetClientScript() {
                StringBuilder sb = new StringBuilder();

                sb.Append("<script type=\"text/javascript\">\n<!--\n");

                // LoadMenu function
                sb.Append(
                    "function mmLoadMenus()\n{\n"
                    + "\tif (window.mm_menu_become_an_agent)\n\t\treturn;\n\n");
                Menu loadingMenu = null;
                foreach (Menu menu in Menus) {
                    sb.Append(menu.GetLoadingClientScript());
                    if (loadingMenu == null && menu.Items.Count > 0) {
                        loadingMenu = menu;
                    }
                }
                sb.Append("\t" + loadingMenu.Id + ".writeMenus();\n}");

                // PushHandler function
                sb.Append(
                    "pushHandler(window, \"load\", function(e)\n{\n"
                    + "\tMM_preloadImages(");
                foreach (Menu menu in Menus) {
                    sb.Append("\n\t\t'" + menu.ImageOnUrl + "',");
                }
                sb.Remove(sb.Length - 1, 1);
                sb.Append(");\n});\n");

                // Invocation LoadMenu function
                sb.Append("mmLoadMenus();\n");

                sb.Append("//-->\n</script>");

                return sb.ToString();
            }
        }

        private class Menu
        {
            public string Url;
            public string ImageUrl;
            public string ImageOnUrl;
            public string Id;
            public string Target = "_self";
            public ArrayList Items = new ArrayList();

            public Menu(string id, string url, string imageUrl, string imageOnUrl) {
                Id = id;
                Url = url;
                ImageUrl = imageUrl;
                ImageOnUrl = imageOnUrl;
            }

            public string GetLoadingClientScript() {
                if (Items.Count == 0) {
                    return string.Empty;
                }

                StringBuilder sb = new StringBuilder();
                sb.Append(
                    string.Format(
                        "\twindow.{0} = new Menu(\"root\",192,16,"
                        + "\"Arial, Helvetica, sans-serif\",10,\"#333333\",\"#FFFFFF\","
                        + "\"#DBDBDB\",\"#DB6C1D\",\"left\",\"middle\",1,0,500,-5,7,"
                        + "true,true,true,0,false,false);\n",
                        Id));

                foreach (MenuItem item in Items) {
                    sb.Append(
                        string.Format(
                            "\t{0}.addMenuItem(\"{1}\",\"window.open('{2}', '_self');\");\n",
                            Id,
                            item.Title,
                            item.Url));
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

        private struct MenuItem
        {
            public string Url;
            public string Title;

            public MenuItem(string title, string url) {
                Url = url;
                Title = title;
            }
        }

        #endregion

        #region Constants

        private const string MENU_KEY = "MENU_KEY";
        private const string MENU_LIB_KEY = "MENU_LIB_KEY";

        private const string MENU_LIB_SRC =
            "<SCRIPT language=\"JavaScript\" type=\"text/javascript\" src=\"{0}/script/mm_menu.js\"></script>";

        #endregion

        #region Web Form Designer generated code

        protected HtmlTable _contentTable;
        protected HtmlTableCell _buttonColumn;

        protected override void OnInit(EventArgs e) {
            InitializeComponent();
            base.OnInit(e);
        }

        protected override void OnLoad(EventArgs e) {
            ControlHelper.NeedDhtmlUtils();

            _buttonColumn.Attributes.Add(
                "background",
                Request.ApplicationPath + "/images/header.jpg");

            if (!Page.IsClientScriptBlockRegistered(MENU_LIB_KEY)) {
                string src = string.Format(MENU_LIB_SRC, Request.ApplicationPath);
                this.Page.RegisterClientScriptBlock(MENU_LIB_KEY, src);
            }

            MenuBar menuBar = BuildMenuBar();

            if (!Page.IsClientScriptBlockRegistered(MENU_KEY)) {
                string menuScript = menuBar.GetClientScript();
                this.Page.RegisterClientScriptBlock(MENU_KEY, menuScript);
            }

            HtmlTable menuBarTable = BuildMenuBarTable(menuBar);

            // Adding menuBarTable
            HtmlTableRow menuRow = new HtmlTableRow();
            HtmlTableCell menuCell = new HtmlTableCell();
            menuCell.Controls.Add(menuBarTable);
            menuRow.Cells.Add(menuCell);
            _contentTable.Rows.Add(menuRow);

            base.OnLoad(e);
        }

        private void InitializeComponent() {

        }
        
        #endregion

        #region Implementations

        private MenuBar BuildMenuBar() {
            MenuBar menuBar = new MenuBar();

            Menu menu = new Menu(
                "home",
                Request.ApplicationPath + "/index.aspx",
                Request.ApplicationPath + "/images/home_button.gif",
                Request.ApplicationPath + "/images/home_button_on.gif");
            menuBar.Menus.Add(menu);

            menu = new Menu(
                "products",
                Request.ApplicationPath + "/products.aspx",
                Request.ApplicationPath + "/images/products_button.gif",
                Request.ApplicationPath + "/images/products_button_on.gif");
            menu.Items.Add(new MenuItem("Overview", Request.ApplicationPath + "/products.aspx"));
            menu.Items.Add(
                new MenuItem("Pre-Paid&nbsp;Home&nbsp;Phone&nbsp;Service", Request.ApplicationPath + "/pphp.aspx"));
            menu.Items.Add(new MenuItem("Pre-Paid&nbsp;Long&nbsp;Distance", Request.ApplicationPath + "/ppld.aspx"));
            menu.Items.Add(new MenuItem("Pre-Paid&nbsp;Cellular", Request.ApplicationPath + "/ppc.aspx"));
            menu.Items.Add(new MenuItem("Pre-Paid&nbsp;MasterCard", Request.ApplicationPath + "/ppmc.aspx"));
            menu.Items.Add(new MenuItem("Pre-Paid&nbsp;Internet", Request.ApplicationPath + "/ppi.aspx"));
            menu.Items.Add(new MenuItem("Bill&nbsp;Pay", Request.ApplicationPath + "/bp.aspx"));
            menuBar.Menus.Add(menu);

            menu = new Menu(
                "become_an_agent",
                Request.ApplicationPath + "/reseller.aspx",
                Request.ApplicationPath + "/images/reseller_button.gif",
                Request.ApplicationPath + "/images/reseller_button_on.gif");
            menu.Items.Add(new MenuItem("Overview", Request.ApplicationPath + "/reseller.aspx"));
            menu.Items.Add(new MenuItem("Benefits", Request.ApplicationPath + "/reseller2.aspx"));
            menu.Items.Add(new MenuItem("Sign&nbsp;Up&nbsp;Now", Request.ApplicationPath + "/agent_contact.aspx"));
            menuBar.Menus.Add(menu);

            menu = new Menu(
                "agent",
                "https://secure.dpiteleconnect.com/agents/",
                Request.ApplicationPath + "/images/ainfo_button.gif",
                Request.ApplicationPath + "/images/ainfo_button_on.gif");
            menu.Target = "_blank";
            menuBar.Menus.Add(menu);

            bool auth = Context.User != null && Context.User.Identity.IsAuthenticated;

            menu =
                new Menu(
                    "account",
                    Request.ApplicationPath + (auth ? "/account/summary.aspx" : "/account/login.aspx"),
                    Request.ApplicationPath + "/images/account_button.gif",
                    Request.ApplicationPath + "/images/account_button_on.gif");

            menu.Items.Add(new MenuItem("Sign&nbsp;Up&nbsp;Now", Request.ApplicationPath + "/signup.aspx"));
            menu.Items.Add(new MenuItem("Account&nbsp;Login", Request.ApplicationPath + "/account/login.aspx"));

            if (auth) {
                menu.Items.Add(new MenuItem("Account&nbsp;Summary", Request.ApplicationPath + "/account/summary.aspx"));
            }

            menuBar.Menus.Add(menu);

            menu = new Menu(
                "about_us",
                Request.ApplicationPath + "/about.aspx",
                Request.ApplicationPath + "/images/about_button.gif",
                Request.ApplicationPath + "/images/about_button_on.gif");
            menu.Items.Add(new MenuItem("About&nbsp;Us", Request.ApplicationPath + "/about.aspx"));
            menu.Items.Add(new MenuItem("Employment", Request.ApplicationPath + "/jobs.aspx"));
            menuBar.Menus.Add(menu);

            menu = new Menu(
                "contact_us",
                Request.ApplicationPath + "/contact.aspx",
                Request.ApplicationPath + "/images/contact_button.gif",
                Request.ApplicationPath + "/images/contact_button_on.gif");
            menu.Items.Add(new MenuItem("Contact&nbsp;dPi", Request.ApplicationPath + "/contact.aspx"));
            menu.Items.Add(
                new MenuItem(
                    "Find&nbsp;A&nbsp;Reseller&nbsp;Near&nbsp;You", Request.ApplicationPath + "/locations.aspx"));
            menuBar.Menus.Add(menu);

            return menuBar;
        }

        private HtmlTable BuildMenuBarTable(MenuBar bar) {
            HtmlTable menuBarTable = new HtmlTable();

            menuBarTable.CellPadding = 0;
            menuBarTable.CellSpacing = 0;
            menuBarTable.Border = 0;

            HtmlTableRow row = new HtmlTableRow();

            foreach (Menu menu in bar.Menus) {
                HtmlTableCell cell = new HtmlTableCell();
                HtmlAnchor anch = new HtmlAnchor();

                anch.HRef = menu.Url;
                anch.Target = menu.Target;
                anch.Attributes.Add(
                    "onmouseover",
                    (menu.Items.Count > 0
                         ? "MM_showMenu(window." + menu.Id + ",0,30,null,'" + menu.Id + "_Image" + "');"
                         : string.Empty)
                    + "MM_swapImage('" + menu.Id + "_Image" + "','','" + menu.ImageOnUrl + "',1);");
                anch.Attributes.Add(
                    "onmouseout",
                    (menu.Items.Count > 0 ? "MM_startTimeout();" : string.Empty)
                    + "MM_swapImgRestore();");

                HtmlImage image = new HtmlImage();

                image.Src = menu.ImageUrl;
                image.ID = menu.Id + "_Image";
                image.Border = 0;
                image.Attributes.Add("name", image.ID);
                anch.Controls.Add(image);

                cell.Controls.Add(anch);

                row.Cells.Add(cell);
            }

            HtmlTableCell cell1 = new HtmlTableCell();
            HtmlImage image1 = new HtmlImage();
            image1.Src = Request.ApplicationPath + "/images/call_us.gif";
            image1.Border = 0;
            cell1.Controls.Add(image1);

            row.Cells.Add(cell1);

            menuBarTable.Rows.Add(row);

            return menuBarTable;
        }
        
        #endregion
    }
}