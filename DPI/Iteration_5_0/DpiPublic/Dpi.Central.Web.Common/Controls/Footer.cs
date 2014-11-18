using System.Collections;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Dpi.Central.Web.Controls
{
    /// <summary>
    /// The class represents Footer control that is used in all pages of dPi public site.
    /// TODO: move fields to the ViewState
    /// TODO: add i18n support
    /// </summary>
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:Footer runat=server></{0}:Footer>")]
    public class Footer : WebControl
    {
        #region Helper classes

        private struct HyperLinkData
        {
            private string _text;
            private string _navigationUrl;
            private string _target;

            public HyperLinkData(string text, string navigationUrl) : this(text, navigationUrl, "_self")
            {
            }

            public HyperLinkData(string text, string navigationUrl, string target)
            {
                _text = text;
                _navigationUrl = navigationUrl;
                _target = target;
            }

            public string Text
            {
                get { return _text; }
            }

            public string NavigationUrl
            {
                get { return _navigationUrl; }
            }

            public string Target
            {
                get { return _target; }
            }
        }

        #endregion

        public Footer() : base(HtmlTextWriterTag.Div)
        {
            
        }

        #region Protected Methods

        /// <summary>
        /// Render this control to the output parameter specified.
        /// </summary>
        /// <param name="writer"> The HTML writer to write out to </param>
        protected override void RenderContents(HtmlTextWriter writer)
        {
            ArrayList linksData = GetLinksData();
            for (int i = 0; i < linksData.Count; i++) {
                if (i != 0 && i != 6) {
                    writer.Write("&nbsp|&nbsp");
                }

                HyperLinkData linkData = (HyperLinkData) linksData[i];

                writer.AddAttribute(HtmlTextWriterAttribute.Href, linkData.NavigationUrl);
                writer.AddAttribute(HtmlTextWriterAttribute.Target, linkData.Target);
                writer.RenderBeginTag(HtmlTextWriterTag.A);
                writer.Write(linkData.Text);
                writer.RenderEndTag(); // A

                if (i == 5) {
                    writer.Write("<br>Copyright &copy; 2007 dPi TeleConnect, LLC.&nbsp; All Rights Reserved.<br>");
                }
            }
        }

        #endregion

        #region Private Methods

        private ArrayList GetLinksData()
        {
            string publicSiteUrl = SiteMap.PublicSiteUrl;

            ArrayList links = new ArrayList(8);

            links.Add(new HyperLinkData("Home", publicSiteUrl + "/index.aspx"));
            links.Add(new HyperLinkData("Products", publicSiteUrl + "/products.aspx"));
            links.Add(new HyperLinkData("Become a Reseller", publicSiteUrl + "/reseller.aspx"));
            links.Add(new HyperLinkData("Agent Info", "https://secure.dpiteleconnect.com/agents/", "_blank"));
            links.Add(new HyperLinkData("About Us", publicSiteUrl + "/about.aspx"));
            links.Add(new HyperLinkData("Contact Us", publicSiteUrl + "/contact.aspx"));
            links.Add(new HyperLinkData("Terms and Conditions", publicSiteUrl + "/legal.aspx"));
            links.Add(new HyperLinkData("Webmaster", "mailto:webmaster@dpiteleconnect.com"));

            return links;
        }

        #endregion
    }
}