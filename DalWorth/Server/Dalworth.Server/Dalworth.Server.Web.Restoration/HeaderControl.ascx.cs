using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using Dalworth.Server.Web.Common;
using Dalworth.Server.Domain;

namespace Dalworth.Server.Web.Restoration
{
    public partial class HeaderControl : System.Web.UI.UserControl
    {
       

        private int m_selectedMenu;
        public int SelectedMenu
        {
            set { m_selectedMenu = value; }
        }

        private List<DalworthPage.HeaderMenuItem> m_menuItems;
        public List<DalworthPage.HeaderMenuItem> MenuItems
        {
            set { m_menuItems = value; }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            DalworthPage page = (DalworthPage)Page;

            water_fire.Attributes.Add("src", page.GetFullUrl("img/header.png"));
            //call_now.Attributes.Add("src", page.GetFullUrl("img/call-now.png"));

            string list = String.Empty;
            string listTemplage = @"<li {0}><a href=""{1}"" {2}>{3}</a></li>";

            foreach (DalworthPage.HeaderMenuItem menuItem in m_menuItems)
            {
                list += string.Format(listTemplage, menuItem.IsHighlighted ? "class=\"some\"" : string.Empty,
                    menuItem.Article != null ? page.GetFullUrl(menuItem.Article.Url) : "#",
                    menuItem.isSelected ? "class=\"current\"" : string.Empty, menuItem.Name);

            }
            m_menu.Text = list;
        }



    }
}