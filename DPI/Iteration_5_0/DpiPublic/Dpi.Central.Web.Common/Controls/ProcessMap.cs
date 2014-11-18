using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Dpi.Central.Web.Controls
{
    public interface IProcessMapProvider
    {
        string[] Nodes { get; }
        int CurrentNodeIndex { get; }
    }

    [DefaultProperty("Text")]
    [ToolboxData("<{0}:ProcessMap runat=server></{0}:ProcessMap>")]
    public class ProcessMap : WebControl
    {
        private IProcessMapProvider _provider;

        public ProcessMap() : base(HtmlTextWriterTag.Div)
        {
        }

        #region Properties

        [Browsable(false)]
        public IProcessMapProvider Provider
        {
            get { return _provider; }
            set { _provider = value; }
        }

        [Bindable(false)]
        [Category("Appearance")]
        [DefaultValue("")]
        public string CssClassPrevious
        {
            get
            {
                object cssClassPrevious = this.ViewState["CssClassPrevious"];

                if (cssClassPrevious != null) {
                    return (string) cssClassPrevious;
                }

                return string.Empty;
            }

            set { this.ViewState["CssClassPrevious"] = value; }
        }

        [Bindable(false)]
        [Category("Appearance")]
        [DefaultValue("")]
        public string CssClassNext
        {
            get
            {
                object cssClassNext = this.ViewState["CssClassNext"];

                if (cssClassNext != null) {
                    return (string) cssClassNext;
                }

                return string.Empty;
            }

            set { this.ViewState["CssClassNext"] = value; }
        }

        [Bindable(false)]
        [Category("Appearance")]
        [DefaultValue("")]
        public string CssClassCurrent
        {
            get
            {
                object cssClassCurrent = this.ViewState["CssClassCurrent"];

                if (cssClassCurrent != null) {
                    return (string) cssClassCurrent;
                }

                return string.Empty;
            }

            set { this.ViewState["CssClassCurrent"] = value; }
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Render this control to the output parameter specified.
        /// </summary>
        /// <param name="writer"> The HTML writer to write out to </param>
        protected override void RenderContents(HtmlTextWriter writer)
        {
            if (_provider == null) {
                return;
            }

            for (int i = 0; i < _provider.Nodes.Length; i++) {
                string node = _provider.Nodes[i];

                string cssClass;

                if (i < _provider.CurrentNodeIndex) {
                    cssClass = CssClassPrevious;
                } else if (i == _provider.CurrentNodeIndex) {
                    cssClass = CssClassCurrent;
                } else {
                    cssClass = CssClassNext;
                }

                writer.AddAttribute(HtmlTextWriterAttribute.Class, cssClass);
                writer.RenderBeginTag(HtmlTextWriterTag.Span);
                writer.Write(node);
                writer.RenderEndTag(); // Span

                if (i < _provider.Nodes.Length - 1) {
                    cssClass = i >= _provider.CurrentNodeIndex ? CssClassNext : CssClassPrevious;

                    writer.AddAttribute(HtmlTextWriterAttribute.Class, cssClass);
                    writer.RenderBeginTag(HtmlTextWriterTag.Span);
                    writer.Write("&nbsp>&nbsp");
                    writer.RenderEndTag(); // Span
                }
            }
        }

        #endregion
    }
}