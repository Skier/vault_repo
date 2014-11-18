using System;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using AspPage = System.Web.UI.Page;

namespace Dpi.Central.Web.Controls
{
    public class ControlHelper
    {
        #region Constants

        private const string DHTML_UTILS_KEY = "DHTML_UTILS";
        private const string DHTML_UTILS_SRC = "<script type=\"text/javascript\" src=\"{0}script/dhtmlutils.js\"></script>";
        private const string DWC_UTILS_KEY = "DWC_UTILS_KEY";
        private const string DWC_UTILS_SRC = "<script type=\"text/javascript\" src=\"{0}/script/dwcutils.js\"></script>";
        private const string DWC_PROTOTYPE_KEY = "DWC_PROTOTYPE_KEY";
        private const string DWC_PROTOTYPE_SRC = "<SCRIPT src=\"{0}/script/prototype.js\" type=\"text/javascript\"></SCRIPT>";
        private const string DWC_SCRIPTACULOUS_KEY = "DWC_SCRIPTACULOUS_KEY";
        private const string DWC_SCRIPTACULOUS_SRC = "<SCRIPT src=\"{0}/script/scriptaculous.js\" type=\"text/javascript\"></SCRIPT>";

        #endregion

        #region Fields

        private static FieldInfo _ñontrolCollection_readOnlyErrorMsg;
        private static PropertyInfo _aspPageFormProperty;

        #endregion

        #region Constructors

        private ControlHelper()
        {
        }

        #endregion

        #region Methods

        public static void SetJumpForwardScript(TextBox from, int maxLength, TextBox to)
        {
            string[,] data = new string[,] {{"onkeydown", "SetJumpForwardDown"}, {"onkeyup", "SetJumpForwardUp"}};

            for (int i = 0; i <= data.GetUpperBound(0); i++) {
                string @event = data[i, 0];
                string function = data[i, 1];

                StringBuilder sb = new StringBuilder();

                sb.Append(from.Attributes[@event] != null ? from.Attributes[@event] : string.Empty);
                sb.Append(function);
                sb.Append("(this.value, ");
                sb.Append((maxLength - 1).ToString());
                sb.Append(", '");
                sb.Append(to.ClientID);
                sb.Append("');");

                from.Attributes.Add(@event, sb.ToString());
            }
        }

        public static void SetJumpBackwardScript(TextBox from, TextBox to) 
        {
            // Uncomment this line if you want to jump backward on keyup event.
            // string[,] data = new string[,] {{"onkeydown", "SetJumpBackward"}, {"onkeyup", "SetJumpBackward"}};

            string[,] data = new string[,] {{"onkeydown", "SetJumpBackward"}};

            for (int i = 0; i <= data.GetUpperBound(0); i++) {
                string @event = data[i, 0];
                string function = data[i, 1];

                StringBuilder sb = new StringBuilder();

                sb.Append(from.Attributes[@event] != null ? from.Attributes[@event] : string.Empty);
                sb.Append(function);
                sb.Append("(this.value, '");
                sb.Append(to.ClientID);
                sb.Append("');");

                from.Attributes.Add(@event, sb.ToString());
            }
        }

        public static HtmlForm GetHtmlForm()
        {
            if (_aspPageFormProperty == null) {
                _aspPageFormProperty = typeof (AspPage).GetProperty(
                    "Form", BindingFlags.Instance | BindingFlags.NonPublic);
            }
            AspPage page = GetCurrentPage();
            HtmlForm form = (HtmlForm) _aspPageFormProperty.GetValue(page, new object[0]);
            return form;
        }

        public static void NeedDhtmlUtils()
        {
            AspPage page = GetCurrentPage();

            if (!page.IsClientScriptBlockRegistered(DHTML_UTILS_KEY)) {
                string src = string.Format(DHTML_UTILS_SRC, GetVirtualDir());
                if (GetHtmlForm() == null) {
                    bool inserted = false;
                    if (page.Controls.Count > 0) {
                        LiteralControl ctl = (LiteralControl) page.Controls[0];
                        string[] tokens = {"</head>", "</HEAD>", "<body"};
                        int idx = -1;
                        for (int i = 0; i < tokens.Length && idx == -1; i++) {
                            idx = ctl.Text.IndexOf(tokens[i]);
                        }
                        if (idx != -1) {
                            ctl.Text = ctl.Text.Insert(idx, src);
                            inserted = true;
                        }
                    }
                    if (!inserted) {
                        if (_ñontrolCollection_readOnlyErrorMsg == null) {
                            _ñontrolCollection_readOnlyErrorMsg = page.Controls.GetType().GetField(
                                "_readOnlyErrorMsg", BindingFlags.Instance | BindingFlags.NonPublic);
                        }
                        string readOnlyErrorMsg = (string) _ñontrolCollection_readOnlyErrorMsg.GetValue(page.Controls);
                        _ñontrolCollection_readOnlyErrorMsg.SetValue(page.Controls, null);
                        try {
                            page.Controls.AddAt(0, new LiteralControl(src));
                        } finally {
                            _ñontrolCollection_readOnlyErrorMsg.SetValue(page.Controls, readOnlyErrorMsg);
                        }
                    }
                } else {
                    page.RegisterClientScriptBlock(DHTML_UTILS_KEY, src);
                }
            }
        }

        public static void NeedDwcUtils()
        {
            AspPage page = GetCurrentPage();

            if (!page.IsClientScriptBlockRegistered(DWC_UTILS_KEY)) {
                string src = string.Format(DWC_UTILS_SRC, HttpContext.Current.Request.ApplicationPath);
                page.RegisterClientScriptBlock(DWC_UTILS_KEY, src);
            }

            if (!page.IsClientScriptBlockRegistered(DWC_PROTOTYPE_KEY)) {
                string src = string.Format(DWC_PROTOTYPE_SRC, HttpContext.Current.Request.ApplicationPath);
                page.RegisterClientScriptBlock(DWC_PROTOTYPE_KEY, src);
            }

            if (!page.IsClientScriptBlockRegistered(DWC_SCRIPTACULOUS_KEY)) {
                string src = string.Format(DWC_SCRIPTACULOUS_SRC, HttpContext.Current.Request.ApplicationPath);
                page.RegisterClientScriptBlock(DWC_SCRIPTACULOUS_KEY, src);
            }
        }

        public static string GetVirtualDir()
        {
            HttpContext context = HttpContext.Current;
            string cachedVirtualDir = (string) context.Items["cachedVirtualDir"];
            if (cachedVirtualDir == null) {
                string rawUrl = context.Request.RawUrl;
                context.Items["cachedVirtualDir"] = cachedVirtualDir = rawUrl.Substring(0, rawUrl.IndexOf("/", 1) + 1);
            }
            return cachedVirtualDir;
        }

        private static AspPage GetCurrentPage() 
        {
            AspPage page = HttpContext.Current.Handler as AspPage;
            if (page == null) {
                throw new InvalidOperationException(
                    String.Format(
                    "Current IHttpHandler must be a {0} but was a {1}",
                    typeof (AspPage),
                    HttpContext.Current.Handler.GetType()));
            }
            return page;
        }

        #endregion
    }
}