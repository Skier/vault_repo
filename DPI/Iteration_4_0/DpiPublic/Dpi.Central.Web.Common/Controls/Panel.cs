using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Windows.Forms.Design;

namespace Dpi.Central.Web.Controls
{
    /// <summary>
    /// TODO: refactor it and: add file into ApplicCach, add ~ support!!!, 
    /// </summary>
    [DefaultProperty("Text"), ToolboxData("<{0}:Panel runat=server></{0}:Panel>")]
    public class Panel : System.Web.UI.WebControls.Panel
    {
        internal const string LAYOUTFILE_PROPERTY_NAME = "LayoutFile";

        private const string CONTENT = "%content%";

        // TODO: mend it.
        private const string DEFAULT_LAYOUT_FILE = "D:\\Temp\\DefaultVagPanel.cxt";

        private static string DEFAULT_BEGIN_LAYOUT_TAGS = string.Empty;
        private static string DEFAULT_END_LAYOUT_TAGS = string.Empty;

        #region Properties

        [Bindable(true), Category("Layout"), DefaultValue(DEFAULT_LAYOUT_FILE)]
        [Description("The file with HTML layout of the panel.")]
        [Editor(typeof (FileNameEditor), typeof (UITypeEditor))]
        public virtual string LayoutFile
        {
            get
            {
                object value = ViewState["_LayoutFile"];
                if (null == value) {
                    return DEFAULT_LAYOUT_FILE;
                }

                return (string) value;
            }

            set
            {
                IsLayoutFileLoaded = (LayoutFile == value);
                ViewState["_LayoutFile"] = value;
            }
        }

        internal string BeginLayoutTags
        {
            get
            {
                if (UseDefaultLayout) {
                    return DEFAULT_BEGIN_LAYOUT_TAGS;
                }

                object value = ViewState["_BeginLayoutTags"];
                if (null == value) {
                    return string.Empty;
                }

                return (string) value;
            }
        }

        internal string EndLayoutTags
        {
            get
            {
                if (UseDefaultLayout) {
                    return DEFAULT_END_LAYOUT_TAGS;
                }

                object value = ViewState["_EndLayoutTags"];
                if (null == value) {
                    return string.Empty;
                }

                return (string) value;
            }
        }

        private bool IsLayoutFileLoaded
        {
            get
            {
                object value = ViewState["_IsLayoutFileLoaded"];
                if (null == value) {
                    return false;
                }

                return (bool) value;
            }

            set { ViewState["_IsLayoutFileLoaded"] = value; }
        }

        private bool UseDefaultLayout
        {
            get { return LayoutFile == DEFAULT_LAYOUT_FILE; }
        }

        #endregion

        #region Override Methods

        protected override void OnPreRender(EventArgs e)
        {
            this.LoadLayoutFile();
            base.OnPreRender(e);
        }

        public override void RenderBeginTag(HtmlTextWriter writer)
        {
            base.RenderBeginTag(writer);
            writer.Write(BeginLayoutTags);
        }

        public override void RenderEndTag(HtmlTextWriter writer)
        {
            writer.Write(EndLayoutTags);
            base.RenderEndTag(writer);
        }

        protected override void Render(HtmlTextWriter writer)
        {
            if (Site != null && Site.DesignMode) {
                base.EnsureChildControls();
                this.LoadLayoutFile();
            }

            base.Render(writer);
        }

        #endregion

        #region Internal Methods

        internal void LoadLayoutFile()
        {
            if (IsLayoutFileLoaded) {
                return;
            }

            string layoutFile = LayoutFile;

            if (!File.Exists(layoutFile)) {
                throw new FileNotFoundException(layoutFile + " file not found.");
            }

            using (StreamReader sr = File.OpenText(layoutFile)) {
                string layout = sr.ReadToEnd();

                //Get absolute application path
                string relAppPath = HttpRuntime.AppDomainAppVirtualPath;
                if(!relAppPath.EndsWith("/"))
                    relAppPath += "/";

                //Replace virtual paths w/ absolute path
                layout = layout.Replace("~/", relAppPath);

//                // TODO: Test it feature carefully
//                layout = layout.Replace("~", 
//                    HttpContext.Current.Request.Url.Scheme + Uri.SchemeDelimiter +
//                    HttpContext.Current.Request.Url.Authority + 
//                    HttpContext.Current.Request.ApplicationPath);

                int cxtIndex = layout.IndexOf(CONTENT);
                if (cxtIndex == -1) {
                    throw new FormatException(CONTENT + " placeholder not found.");
                }

                SetBeginLayoutTags(layout.Substring(0, cxtIndex));
                SetEndLayoutTags(layout.Substring(cxtIndex + CONTENT.Length,
                                                  layout.Length - cxtIndex - CONTENT.Length));
            }

            IsLayoutFileLoaded = true;
        }

        #endregion

        #region Private Methods

        private void SetBeginLayoutTags(string tags)
        {
            if (UseDefaultLayout) {
                DEFAULT_BEGIN_LAYOUT_TAGS = tags;
                ViewState["_BeginLayoutTags"] = null;
            } else {
                ViewState["_BeginLayoutTags"] = tags;
            }
        }

        private void SetEndLayoutTags(string tags)
        {
            if (UseDefaultLayout) {
                DEFAULT_END_LAYOUT_TAGS = tags;
                ViewState["_EndLayoutTags"] = null;
            } else {
                ViewState["_EndLayoutTags"] = tags;
            }
        }

        #endregion
    }
}