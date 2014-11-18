using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Dpi.Central.Web.Controls
{
    [DefaultProperty("Tabs")]
    [ToolboxData("<{0}:TabControl runat=server></{0}:TabControl>")]
    [ParseChildren(true), PersistChildren(false)]
    public class TabControl : WebControl, IPostBackEventHandler
    {
        #region Events

        /// <summary>
        /// Occurs when a different tab is selected in the tab 
        /// control between posts to the server.
        /// </summary>
        [Category("Action")]
        public event EventHandler SelectedIndexChanged;

        #endregion

        #region Member variables

        /// <summary>
        /// Contains a collection of <see cref="Tab"/> objects.
        /// </summary>
        private TabCollection _tabs = new TabCollection();

        #endregion

        #region Constructors

        public TabControl() : base(HtmlTextWriterTag.Div)
        {
        }

        #endregion

        #region Event raisers

        /// <summary>
        /// Raises the <see cref="SelectedIndexChanged"/> event of the <code>TabControls</code>. 
        /// This allows you to create a custom handler for the event.
        /// </summary>
        /// <param name="args">A <see cref="System.EventArgs"/> that contains the event data.</param>
        protected virtual void OnSelectedIndexChanged(EventArgs args)
        {
            if (null != SelectedIndexChanged) {
                SelectedIndexChanged(this, args);
            }
        }

        #endregion

        #region Properties

        [Editor("System.ComponentModel.Design.CollectionEditor, System.Design", typeof (UITypeEditor)), PersistenceMode(PersistenceMode.InnerProperty), DefaultValue(null), MergableProperty(false)]
        public TabCollection Tabs
        {
            get { return _tabs; }
        }

        [Bindable(true), DefaultValue(0), Category("Behavior")]
        public int SelectedIndex
        {
            get
            {
                object value = ViewState["SelectedIndex"];
                if (null == value) {
                    return 0;
                }

                return (int) value;
            }

            set
            {
                ViewState["SelectedIndex"] = value;
                ChildControlsCreated = false;
            }
        }

        [Category("Appearance")]
        public string NoteLine1
        {
            get 
            {
                object value = ViewState["NoteLine1"];
                if (null == value) {
                    return string.Empty;
                }

                return (string) value;
            }

            set {
                ViewState["NoteLine1"] = value;
                ChildControlsCreated = false;
            }
        }

        [Category("Appearance")]
        public string NoteLine2 
        {
            get {
                object value = ViewState["NoteLine2"];
                if (null == value) {
                    return string.Empty;
                }

                return (string) value;
            }

            set {
                ViewState["NoteLine2"] = value;
                ChildControlsCreated = false;
            }
        }

        #endregion

        #region IPostBackEventHandler

        public void RaisePostBackEvent(string eventArgument)
        {
            int selectedIndex = Convert.ToInt32(Page.Request.Form["__EVENTARGUMENT"]);

            if (selectedIndex != SelectedIndex) {
                SelectedIndex = selectedIndex;

                OnSelectedIndexChanged(EventArgs.Empty);
            }
        }

        #endregion

        #region Override methods

        protected override void Render(HtmlTextWriter writer) 
        {
            if (Site != null && Site.DesignMode == true) {
                this.EnsureChildControls();
            }

            base.Render (writer);
        }

        protected override void CreateChildControls()
        {
            Controls.Clear();

            HtmlGenericControl ul = new HtmlGenericControl(HtmlTextWriterTag.Ul.ToString());

            for (int i = 0; i < Tabs.Count; i++) {
                Tab tab = Tabs[i];

                HtmlAnchor anchor = new HtmlAnchor();
                anchor.HRef = Page.GetPostBackClientHyperlink(this, i.ToString());
                anchor.InnerText = tab.Title;

                HtmlGenericControl li = new HtmlGenericControl(HtmlTextWriterTag.Li.ToString());
            
                if (i == SelectedIndex) {
                    li.Attributes.Add("class", "selected");
                    anchor.HRef = string.Empty;
                }

                li.Controls.Add(anchor);
                ul.Controls.Add(li);
            }

            Controls.Add(ul);

            HtmlGenericControl div = new HtmlGenericControl(HtmlTextWriterTag.Div.ToString());
            div.Attributes.Add("class", "note");
            div.InnerHtml = NoteLine1 + "<br>" + NoteLine2;

            Controls.Add(div);
        }

        #endregion
    }
}