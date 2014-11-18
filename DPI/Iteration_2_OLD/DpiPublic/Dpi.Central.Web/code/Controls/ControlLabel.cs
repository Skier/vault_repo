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
        #region Constants

        private const string FOR_ATTRIBUTE = "for";

        #endregion Constants

        #region Fields

        private string _targetId = String.Empty;
        private string _fieldName = String.Empty;
        private Control _target = null;
        private bool _showRequiredMark = false;

        #endregion Fields

        #region Methods

        protected override void OnInit(EventArgs e) {
            base.OnInit(e);
            ControlHelper.ChangeControlTag(this, HtmlTextWriterTag.Label);
        }

        protected override void OnLoad(EventArgs e) {
            base.OnLoad(e);
            UpdateTarget();
        }

        private void UpdateTarget() {
            if (Parent == null) {
                _target = null;
            } else {
                _target = Parent.FindControl(_targetId);
            }
            if (_target != null) {
                Attributes[FOR_ATTRIBUTE] = _target.ClientID;
                ILabelable lb = _target as ILabelable;
                if (lb != null) {
                    lb.Label = this;
                }
            } else {
                Attributes.Remove(FOR_ATTRIBUTE);
            }
        }

        protected override void Render(HtmlTextWriter output) {
            if (_target != null) {
                Attributes[FOR_ATTRIBUTE] = _target.ClientID;
                WebControl wc = _target as WebControl;
                if (wc != null) {
                    Enabled = wc.Enabled;
                }
            }
            base.AddAttributesToRender(output);
            RenderBeginTag(output);
            output.Write(Text);
            if (_target != null) {
                IRequired req = _target as IRequired;
                if ((req != null && req.IsRequired) || _showRequiredMark) {
                    ControlHelper.RenderRequiredSymbol(output);
                }
            } else if (_showRequiredMark) {
                ControlHelper.RenderRequiredSymbol(output);
            }
            RenderEndTag(output);
        }

        #endregion Methods

        #region Properties

        [Category("Appearance")]
        [DefaultValue(false)]
        [Bindable(false)]
        public bool ShowRequiredMark {
            get { return _showRequiredMark; }
            set { _showRequiredMark = value; }
        }

        [Category("Behavior")]
        [DefaultValue("")]
        [Bindable(false)]
        [TypeConverter(typeof (ControlIDConverter))]
        public string Target {
            get { return _targetId; }
            set {
                if (_targetId == value) {
                    return;
                }
                ILabelable lb = _target as ILabelable;
                if (lb != null) {
                    lb.Label = null;
                }
                _targetId = (value == null ? String.Empty : value);
                UpdateTarget();
            }
        }

        [Bindable(false)]
        [Browsable(false)]
        public Control TargetControl {
            get { return _target; }
        }

        [Category("Data")]
        [DefaultValue("")]
        public string FieldName {
            get {
                if (_fieldName.Length == 0) {
                    _fieldName = Text.LastIndexOf(',') > 0 ? Text.Substring(0, Text.LastIndexOf(',')) : Text;
                }
                return _fieldName;
            }
            set {
                if (value == null) {
                    value = string.Empty;
                }
                _fieldName = value;
            }
        }

        #endregion Properties
    }
}