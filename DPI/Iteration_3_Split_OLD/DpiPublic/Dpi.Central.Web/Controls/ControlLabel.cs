using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Dpi.Central.Web.Controls
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:ControlLabel runat=server></{0}:ControlLabel>")]
    [ToolboxItemFilter("Dpi.Central.Web.Controls", ToolboxItemFilterType.Require)]
    public class ControlLabel : Label
    {
        #region Fields

        private WebControl _associatedControl = null;

        #endregion Fields

        #region Methods

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            ControlHelper.ChangeControlTag(this, HtmlTextWriterTag.Label);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            _associatedControl = AssociatedControl;
        }

        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            bool bakEnabled = Enabled;
            if (AssociatedControl != null) {
                Enabled = AssociatedControl.Enabled;
            }

            try {
                base.AddAttributesToRender(writer);
            } catch (Exception ex) {
                if (Site == null || !Site.DesignMode) {
                    throw ex;
                }
            }
            Enabled = bakEnabled;
        }

        protected override void RenderContents(HtmlTextWriter writer)
        {
            base.RenderContents(writer);
            if (AssociatedControl != null) {
                IRequired req = AssociatedControl as IRequired;
                if ((req != null && req.IsRequired) || ShowRequiredMark) {
                    ControlHelper.RenderRequiredSymbol(writer);
                }
            } else if (ShowRequiredMark) {
                ControlHelper.RenderRequiredSymbol(writer);
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            if (SimpleText && AssociatedControl == null) {
                RenderContents(writer);
            } else {
                base.Render(writer);
            }
        }

        #endregion Methods

        #region Properties

        [Category("Appearance")]
        [DefaultValue(false)]
        [Bindable(false)]
        public bool ShowRequiredMark
        {
            get
            {
                object o = ViewState["ShowRequiredMark"];
                if (o == null) {
                    return false;
                }
                return (bool) o;
            }
            set { ViewState["ShowRequiredMark"] = value; }
        }

        [Category("Appearance")]
        [DefaultValue(false)]
        [Bindable(false)]
        [Description("Suppresses rendering of text wrapping tags")]
        public bool SimpleText
        {
            get
            {
                object o = ViewState["SimpleText"];
                if (o == null) {
                    return false;
                }
                return (bool) o;
            }
            set { ViewState["SimpleText"] = value; }
        }

        /*
         * WARNING: If you cannot compile the code because "there is no AssociatedControlID in the base class."
         *          you should update your .NET Framework with Microsoft .NET Framework 1.1 Service Pack 1
         *          http://www.microsoft.com/downloads/details.aspx?displaylang=en&FamilyID=A8F5654F-088E-40B2-BBDB-A83353618B38
         */

        [Category("Behavior")]
        [DefaultValue("")]
        [Bindable(false)]
        [TypeConverter(typeof (ControlIDConverter))]
        public override string AssociatedControlID
        {
            get { return base.AssociatedControlID; }
            set
            {
                if (value == base.AssociatedControlID) {
                    return;
                }

                if (_associatedControl != null) {
                    ILabelable lb = _associatedControl as ILabelable;
                    if (lb != null) {
                        lb.Label = null;
                    }
                    _associatedControl = null;
                }

                base.AssociatedControlID = value;
            }
        }

        [Bindable(false)]
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public WebControl AssociatedControl
        {
            get
            {
                string id = AssociatedControlID;
                if (_associatedControl == null && id != null && id.Length > 0) {
                    _associatedControl = FindControl(id) as WebControl;
                    if (_associatedControl == null) {
                        /*throw new Exception(
                        string.Format(
                            "Unable to find the control with id '{0}' that is associated with the Label '{1}'",
                            id,
                            ID));*/
                    } else {
                        ILabelable lb = _associatedControl as ILabelable;
                        if (lb != null) {
                            lb.Label = this;
                            // TODO: notify _associatedControl to re-draw it in design-mode
                        }
                    }
                }
                return _associatedControl;
            }
        }

        public override bool Enabled
        {
            get { return base.Enabled; }
            set
            {
                if (AssociatedControl != null) {
                    base.Enabled = AssociatedControl.Enabled;
                } else {
                    base.Enabled = value;
                }
            }
        }

        [Category("Data")]
        [DefaultValue("")]
        public string FieldName
        {
            get
            {
                object o = ViewState["FieldName"];
                string fld = o != null ? (string) o : string.Empty;
                if (fld.Length == 0) {
                    int pos = Text.LastIndexOf(',');
                    fld = pos > 0 ? Text.Substring(0, pos) : Text;
                }
                return fld;
            }
            set
            {
                if (value != null && value.Length > 0) {
                    ViewState["FieldName"] = value;
                }
            }
        }

        #endregion Properties
    }
}