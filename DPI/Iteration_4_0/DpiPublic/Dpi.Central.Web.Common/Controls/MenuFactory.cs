using System.Web;

namespace Dpi.Central.Web.Controls
{
	public class MenuFactory
	{
        private static MenuFactory _instance;

        static public MenuFactory GetInstance()
        {
            if (_instance == null) {
                lock (typeof(MenuFactory)) {
                    if (_instance == null) {
                        _instance = new MenuFactory();
                    }
                }
            }

            return _instance;
        }

        public Menu CreateMenu()
        {
            string applicationUrl = HttpContext.Current.Request.ApplicationPath;
            string accountSiteUrl = SiteMap.AccountSiteUrl;
            string publicSiteUrl = SiteMap.PublicSiteUrl;

            Menu menu = new Menu();

            menu.ID = "_menu";

            Menu.SubMenu subMenu = new Menu.SubMenu(
                "home", publicSiteUrl + "/index.aspx",
                applicationUrl + "/images/home_button.gif",
                applicationUrl + "/images/home_button_on.gif");
            menu.SubMenus.Add(subMenu);

            subMenu = new Menu.SubMenu(
                "products", publicSiteUrl + "/products.aspx", 
                applicationUrl + "/images/products_button.gif",
                applicationUrl + "/images/products_button_on.gif");
            subMenu.Items.Add(new Menu.SubMenuItem("Overview", publicSiteUrl + "/products.aspx"));
            subMenu.Items.Add(new Menu.SubMenuItem("Pre-Paid&nbsp;Home&nbsp;Phone&nbsp;Service", publicSiteUrl + "/pphp.aspx"));
            subMenu.Items.Add(new Menu.SubMenuItem("Pre-Paid&nbsp;Long&nbsp;Distance", publicSiteUrl + "/ppld.aspx"));
            subMenu.Items.Add(new Menu.SubMenuItem("Pre-Paid&nbsp;Cellular", publicSiteUrl + "/ppc.aspx"));
            subMenu.Items.Add(new Menu.SubMenuItem("Pre-Paid&nbsp;MasterCard", publicSiteUrl + "/ppmc.aspx"));
            subMenu.Items.Add(new Menu.SubMenuItem("Pre-Paid&nbsp;Internet", publicSiteUrl + "/ppi.aspx"));
            subMenu.Items.Add(new Menu.SubMenuItem("Bill&nbsp;Pay", publicSiteUrl + "/bp.aspx"));
            menu.SubMenus.Add(subMenu);

            subMenu = new Menu.SubMenu(
                "become_an_agent", publicSiteUrl + "/reseller.aspx",
                applicationUrl + "/images/reseller_button.gif",
                applicationUrl + "/images/reseller_button_on.gif");
            subMenu.Items.Add(new Menu.SubMenuItem("Overview", publicSiteUrl + "/reseller.aspx"));
            subMenu.Items.Add(new Menu.SubMenuItem("Benefits", publicSiteUrl + "/reseller2.aspx"));
            subMenu.Items.Add(new Menu.SubMenuItem("Sign&nbsp;Up&nbsp;Now", publicSiteUrl + "/agent_contact.aspx"));
            menu.SubMenus.Add(subMenu);

            subMenu = new Menu.SubMenu(
                "agent", "https://secure.dpiteleconnect.com/agents/",
                applicationUrl + "/images/ainfo_button.gif",
                applicationUrl + "/images/ainfo_button_on.gif");
            subMenu.Target = "_blank";
            menu.SubMenus.Add(subMenu);

            bool authenticated = HttpContext.Current.User != null && HttpContext.Current.User.Identity.IsAuthenticated;

            subMenu = new Menu.SubMenu(
                "account", accountSiteUrl + (authenticated ? "/account/summary.aspx" : "/account/login.aspx"),
                applicationUrl + "/images/account_button.gif",
                applicationUrl + "/images/account_button_on.gif");

            subMenu.Items.Add(new Menu.SubMenuItem("Sign&nbsp;Up&nbsp;Now", accountSiteUrl + "/signup.aspx"));
            subMenu.Items.Add(new Menu.SubMenuItem("Account&nbsp;Login", accountSiteUrl + "/account/login.aspx"));

            if (authenticated) {
                subMenu.Items.Add(new Menu.SubMenuItem("Account&nbsp;Summary", accountSiteUrl + "/account/summary.aspx"));
            }

            menu.SubMenus.Add(subMenu);

            subMenu = new Menu.SubMenu(
                "about_us", publicSiteUrl + "/about.aspx",
                applicationUrl + "/images/about_button.gif",
                applicationUrl + "/images/about_button_on.gif");
            subMenu.Items.Add(new Menu.SubMenuItem("About&nbsp;Us", publicSiteUrl + "/about.aspx"));
            subMenu.Items.Add(new Menu.SubMenuItem("Employment", publicSiteUrl + "/jobs.aspx"));
            menu.SubMenus.Add(subMenu);

            subMenu = new Menu.SubMenu(
                "contact_us", publicSiteUrl + "/contact.aspx",
                applicationUrl + "/images/contact_button.gif",
                applicationUrl + "/images/contact_button_on.gif");
            subMenu.Items.Add(new Menu.SubMenuItem("Contact&nbsp;dPi", publicSiteUrl + "/contact.aspx"));
            subMenu.Items.Add(new Menu.SubMenuItem("Find&nbsp;A&nbsp;Reseller&nbsp;Near&nbsp;You", publicSiteUrl + "/locations.aspx"));
            menu.SubMenus.Add(subMenu);

            return menu;
        }

		protected MenuFactory()
		{
		}
	}
}
