using System;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.UI;

namespace Dpi.Central.Web.Controls
{
    [DefaultProperty("Text"), ToolboxData("<{0}:Panel runat=server></{0}:Panel>")]
    public class Panel : System.Web.UI.WebControls.Panel
    {
        #region Constants

        private const string BEGIN_TAG_KEY = "BEGIN_TAG";
        private const string END_TAG_KEY = "END_TAG";
        private const string TEMPLATE_KEY = "PANEL_TEMPLATE";
        private const string TEMPLATE_RESOURCE_NAME = "Dpi.Central.Web.Controls.PanelTemplate.html";
        private const string CONTENT = "%content%";

        #endregion

        #region Properties

        private string BeginTag
        {
            get
            {
                object value = ViewState[BEGIN_TAG_KEY];

                if (value == null) {
                    InstantiateFromTemplate();
                    return (string)ViewState[BEGIN_TAG_KEY];
                }

                return (string) value;
            }

            set { ViewState[BEGIN_TAG_KEY] = value; }
        }

        private string EndTag
        {
            get
            {
                object value = ViewState[END_TAG_KEY];

                if (value == null) {
                    InstantiateFromTemplate();
                    return (string)ViewState[END_TAG_KEY];
                }

                return (string) value;
            }

            set { ViewState[END_TAG_KEY] = value; }
        }
        
        #endregion

        #region Override Methods

        public override void RenderBeginTag(HtmlTextWriter writer)
        {
            base.RenderBeginTag(writer);
            writer.Write(BeginTag);
        }

        public override void RenderEndTag(HtmlTextWriter writer)
        {
            writer.Write(EndTag);
            base.RenderEndTag(writer);
        }

        #endregion

        #region Private Methods

        private void InstantiateFromTemplate()
        {
            string template = (string)Context.Application[TEMPLATE_KEY];

            if (template == null) {
                lock (typeof(Panel)) {
                    template = (string)Context.Application[TEMPLATE_KEY];

                    if (template == null) {
                        template = LoadTemplate();
                        template = ResolveApplicationPath(template);

                        Context.Application[TEMPLATE_KEY] = template;
                    }
                }
            }

            int cxtIndex = template.IndexOf(CONTENT);
            if (cxtIndex == -1) {
                throw new FormatException(CONTENT + " placeholder not found in a panel template.");
            }

            BeginTag = template.Substring(0, cxtIndex);
            EndTag = template.Substring(cxtIndex + CONTENT.Length, template.Length - cxtIndex - CONTENT.Length);
        }

        private string LoadTemplate()
        {
            Assembly myAssembly = typeof (Panel).Assembly;

            using (Stream stream = myAssembly.GetManifestResourceStream(TEMPLATE_RESOURCE_NAME)) {
                if (stream == null) {
                    throw new ApplicationException(TEMPLATE_RESOURCE_NAME + " resource not found in " + myAssembly.GetName() + ".");
                }

                byte[] buffer = new byte[stream.Length];

                for (int offset = 0; offset != buffer.Length; ) {
                    int count = buffer.Length - offset > 255 ? 255 : buffer.Length - offset;
                    offset += stream.Read(buffer, offset, count);
                }

                UTF8Encoding encoding = new UTF8Encoding();
                string template = encoding.GetString(buffer);

                return template;
            }
        }

        private string ResolveApplicationPath(string template)
        {
            string relAppPath = HttpRuntime.AppDomainAppVirtualPath;
            
            if (!relAppPath.EndsWith("/")) {
                relAppPath += "/";
            }

            return template.Replace("~/", relAppPath);
        }

        #endregion
    }
}