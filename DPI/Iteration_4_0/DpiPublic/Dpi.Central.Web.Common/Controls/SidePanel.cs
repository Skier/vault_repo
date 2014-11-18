using System.IO;
using System.Web.UI;

namespace Dpi.Central.Web.Controls
{
    public class SidePanel : Control
    {
        protected override void Render(HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "0");
            writer.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "0");
            writer.AddAttribute(HtmlTextWriterAttribute.Border, "0");
            writer.AddAttribute(HtmlTextWriterAttribute.Height, "100%");
            writer.RenderBeginTag(HtmlTextWriterTag.Table);

            writer.RenderBeginTag(HtmlTextWriterTag.Tr);

            writer.AddAttribute(HtmlTextWriterAttribute.Class, "aboutSideTop");
            writer.RenderBeginTag(HtmlTextWriterTag.Td);

            writer.AddAttribute(HtmlTextWriterAttribute.Src, ImageUrl + "about_side_top.jpg");
            writer.RenderBeginTag(HtmlTextWriterTag.Img);

            writer.RenderEndTag(); // Img
            writer.RenderEndTag(); // Td
            writer.RenderEndTag(); // Tr

            writer.RenderBeginTag(HtmlTextWriterTag.Tr);

            writer.AddAttribute(HtmlTextWriterAttribute.Class, "aboutSideBottom");
            writer.RenderBeginTag(HtmlTextWriterTag.Td);

            writer.AddAttribute(HtmlTextWriterAttribute.Src, ImageUrl + "about_side_bottom.jpg");
            writer.RenderBeginTag(HtmlTextWriterTag.Img);

            writer.RenderEndTag(); // Img
            writer.RenderEndTag(); // Td
            writer.RenderEndTag(); // Tr

            writer.RenderEndTag(); // Table
        }

        private string ImageUrl
        {
            get
            {
                if (Site != null && Site.DesignMode) {
                    if (Directory.Exists("images")) {
                        return "images/";
                    } else if (Directory.Exists("../images")) {
                        return "../images/";
                    } else {
                        return string.Empty;
                    }
                } else {
                    return SiteMap.AccountSiteUrl + "/images/";
                }
            }
        }
    }
}