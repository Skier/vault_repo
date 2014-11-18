using System;
using System.Collections;
using System.Collections.Specialized;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using AspPage = System.Web.UI.Page;

namespace Dpi.Central.Web.Controls
{
    public class ControlHelper
    {
        #region Helper classes

        private class PageClientScripts : IEnumerable
        {
            private class ClientScriptItem
            {
                public readonly string Value;
                public readonly bool IsStartup;

                public ClientScriptItem(string value, bool isStartup)
                {
                    Value = value;
                    IsStartup = isStartup;
                }
            }

            private const string SCRIPT_INIT_START =
                "\n<SCRIPT language=\"JavaScript\" type=\"text/javascript\" id=\"pageInit\">\n<!--\n";

            private const string SCRIPT_STARTUP_START =
                "\n<SCRIPT language=\"JavaScript\" type=\"text/javascript\" id=\"pageStartup\">\n<!--\n";

            private const string SCRIPT_STOP = "//-->\n</SCRIPT>\n";
            private const string INIT_KEY = "PageInitScripts";
            private const string STARTUP_KEY = "PageStartupScripts";
            private const int LOADED_CONTROL_STATE = 4;

            private static readonly FieldInfo _registeredClientScriptBlocksField;
            private static readonly FieldInfo _registeredClientStartupScriptsField;
            private static FieldInfo _controlStateField;
            private static readonly IDictionary _clientScriptsByPages = Hashtable.Synchronized(new Hashtable());

            private AspPage _page;
            private IDictionary _list = new HybridDictionary();
            private IDictionary _initScripts;
            private IDictionary _startupScripts;
            private bool _registeredOnPage;
            private bool _initBlockChanged;
            private bool _startupBlockChanged;

            static PageClientScripts()
            {
                _registeredClientScriptBlocksField =
                    typeof (AspPage).GetField(
                        "_registeredClientScriptBlocks", BindingFlags.NonPublic | BindingFlags.Instance);
                _registeredClientStartupScriptsField =
                    typeof (AspPage).GetField(
                        "_registeredClientStartupScripts", BindingFlags.NonPublic | BindingFlags.Instance);
                if (_registeredClientScriptBlocksField == null || _registeredClientStartupScriptsField == null) {
                    throw new Exception("Cannot find script block fields in Page class");
                }
            }

            private PageClientScripts(AspPage page)
            {
                _page = page;
            }

            public static PageClientScripts GetForPage(AspPage page)
            {
                PageClientScripts result = (PageClientScripts) _clientScriptsByPages[page];
                if (result == null) {
                    result = new PageClientScripts(page);
                    if (IsPageLoaded(page)) {
                        result._registeredOnPage = true;
                    } else {
                        page.PreRender += new EventHandler(result.PagePreRender);
                        page.Unload += new EventHandler(result.PageUnload);
                    }
                    _clientScriptsByPages[page] = result;
                }
                return result;
            }

            private static bool IsPageLoaded(AspPage page)
            {
                if (_controlStateField == null) {
                    _controlStateField =
                        typeof (Control).GetField("_controlState", BindingFlags.Instance | BindingFlags.NonPublic);
                    if (_controlStateField == null) {
                        throw new Exception("Cannot get field _controlState on Control type");
                    }
                }
                object value = _controlStateField.GetValue(page);
                return ((int) value) >= LOADED_CONTROL_STATE;
            }

            public void Add(string key, string script, bool isStartup)
            {
                if (script == null) {
                    throw new ArgumentNullException("script");
                }
                script = script.Trim();
                if (script.Length == 0) {
                    throw new ArgumentException("Empty script", "script");
                }
                _list[key] = new ClientScriptItem(script, isStartup);
                if (_registeredOnPage) {
                    if (isStartup) {
                        _startupBlockChanged = true;
                    } else {
                        _initBlockChanged = true;
                    }
                    PagePreRender(null, null);
                }
            }

            public bool IsScriptAdded(string key)
            {
                return _list.Contains(key);
            }

            private void PagePreRender(object sender, EventArgs ea)
            {
                if (_list.Count == 0) {
                    return;
                }
                StringBuilder sbInit = new StringBuilder(SCRIPT_INIT_START, _list.Count*100);
                StringBuilder sbStartup = new StringBuilder(SCRIPT_STARTUP_START, _list.Count*100);
                foreach (ClientScriptItem item in this) {
                    StringBuilder sb = item.IsStartup ? sbStartup : sbInit;
                    sb.Append(item.Value);
                    sb.Append("\n");
                }
                if (sbInit.Length > SCRIPT_INIT_START.Length && (!_registeredOnPage || _initBlockChanged)) {
                    if (_registeredOnPage && CheckInitScripts()) {
                        _initScripts.Remove(INIT_KEY);
                    }
                    sbInit.Append(SCRIPT_STOP);
                    _page.RegisterClientScriptBlock(INIT_KEY, sbInit.ToString());
                }
                if (sbStartup.Length > SCRIPT_STARTUP_START.Length && (!_registeredOnPage || _startupBlockChanged)) {
                    if (_registeredOnPage && CheckStartupScripts()) {
                        _startupScripts.Remove(STARTUP_KEY);
                    }
                    sbStartup.Append(SCRIPT_STOP);
                    _page.RegisterStartupScript(STARTUP_KEY, sbStartup.ToString());
                }
                _registeredOnPage = true;
                _initBlockChanged = _startupBlockChanged = false;
            }

            private bool CheckInitScripts()
            {
                if (_initScripts == null) {
                    _initScripts = (IDictionary) _registeredClientScriptBlocksField.GetValue(_page);
                }
                return _initScripts != null;
            }

            private bool CheckStartupScripts()
            {
                if (_startupScripts == null) {
                    _startupScripts = (IDictionary) _registeredClientStartupScriptsField.GetValue(_page);
                }
                return _startupScripts != null;
            }

            private void PageUnload(object sender, EventArgs ea)
            {
                _clientScriptsByPages.Remove(sender);
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return _list.Values.GetEnumerator();
            }
        }

        #endregion

        #region Constants

        public const string REQUIRE_SYMBOL = "*";
        public const string REQUIRE_CLASS = "requireIndicator";
        public const string ERROR_MESSAGE_CSS_CLASS = "inplaceerror";
        public const string DHTML_UTILS_KEY = "DHTML_UTILS";

        public const string DHTML_UTILS_SRC =
            "<script type=\"text/javascript\" src=\"{0}script/dhtmlutils.js\"></script>\n\r";

        public const bool DEFAULT_PERFORM_VALIDATION = false;

        private const string NO_CACHE_META =
            @"<META http-equiv=""expires"" content=""0"">
<META http-equiv=""pragma"" content=""no-cache"">
<META http-equiv=""cache-control"" content=""no-store, no-cache, must-revalidate, post-check=0, pre-check=0"">";

        private const string EMULATE_IE_KEY = "EMULATE_IE";

        private const string EMULATE_IE_SRC =
            "<SCRIPT language=\"JavaScript\" type=\"text/javascript\">\n<!--\nemulateIE();\n// -->\n</script>";

        #endregion

        #region Fields

        private static Regex time12Regex;
        private static FieldInfo _tagKeyFieldInfo;
        private static FieldInfo _tagNameFieldInfo;
        private static FieldInfo _ñontrolCollection_readOnlyErrorMsg;
        private static PropertyInfo _aspPageFormProperty;
        private static HttpBrowserCapabilities _realBrowser;

        #endregion

        #region Constructors

        private ControlHelper()
        {
        }

        #endregion Constructors

        #region Methods

        #region Rendering

        public static void RenderRequiredSymbol(HtmlTextWriter output)
        {
            output.AddAttribute(HtmlTextWriterAttribute.Class, REQUIRE_CLASS);
            output.RenderBeginTag(HtmlTextWriterTag.Span);
            output.Write(REQUIRE_SYMBOL);
            output.RenderEndTag();
        }

        public static void RenderErrorMessage(IValidatedControl validatedControl, HtmlTextWriter output)
        {
            if (validatedControl.PerformValidation && validatedControl.ShowErrorMessage && !validatedControl.IsValid) {
                Label err = new Label();
                err.Text = validatedControl.ErrorMessage;
                if (IsStringNotEmpty(validatedControl.ErrorMessageCssClass)) {
                    err.CssClass = validatedControl.ErrorMessageCssClass;
                }
                output.Write("<BR/>");
                err.RenderControl(output);
            }
        }

        public static void RenderNoCacheMeta(HtmlTextWriter output)
        {
            output.WriteLine(NO_CACHE_META);
        }

        #endregion Rendering

        #region Control properties

        public static void ChangeControlTag(WebControl control, HtmlTextWriterTag tag)
        {
            if (_tagKeyFieldInfo == null) {
                _tagKeyFieldInfo = typeof (WebControl).GetField(
                    "tagKey", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                _tagNameFieldInfo = typeof (WebControl).GetField(
                    "tagName", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                if (_tagKeyFieldInfo == null) {
                    throw new FieldAccessException("Field tagKey not found in WebControl class");
                }
                if (_tagNameFieldInfo == null) {
                    throw new FieldAccessException("Field tagName not found in WebControl class");
                }
            }
            _tagKeyFieldInfo.SetValue(control, tag);
            if (tag != HtmlTextWriterTag.Unknown) {
                _tagNameFieldInfo.SetValue(
                    control,
                    Enum.Format(
                        typeof (HtmlTextWriterTag), tag, "G").ToLower(CultureInfo.InvariantCulture));
            } else {
                _tagNameFieldInfo.SetValue(control, null);
            }
        }

        public static void CopyControlProperties(WebControl from, WebControl to)
        {
            foreach (string attribName in from.Attributes.Keys) {
                to.Attributes[attribName] = from.Attributes[attribName];
            }
            foreach (string styleName in from.Style.Keys) {
                to.Style[styleName] = from.Style[styleName];
            }
            to.Font.CopyFrom(from.Font);
            to.CssClass = from.CssClass;
            to.BackColor = from.BackColor;
            to.ForeColor = from.ForeColor;
            to.BorderStyle = from.BorderStyle;
            to.BorderWidth = from.BorderWidth;
            to.BorderColor = from.BorderColor;
            to.Height = from.Height;
            to.Width = from.Width;
            from.ControlStyle.CopyFrom(from.ControlStyle);
            to.CopyBaseAttributes(from);
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

        #endregion Control properties

        #region Validation

        public static void AppendErrorMessage(StringBuilder result, string message, params object[] args)
        {
            if (!IsStringNotEmpty(message)) {
                return;
            }
            if (result.Length > 0) {
                result.Append(Environment.NewLine);
            }
            try {
                result.AppendFormat(message, args);
            } catch (FormatException ex) {
                throw new ApplicationException(
                    string.Format(
                        "Invalid message or parameters. Message: \"{0}\"; Parameter count: {1}",
                        message,
                        args == null ? 0 : args.Length),
                    ex);
            }
        }

        public static bool CallEachValidateHandler(object sender, ValidateEventHandler[] handlers, StringBuilder errors)
        {
            bool result = true;
            ValidateEventArgs ea = new ValidateEventArgs();
            foreach (ValidateEventHandler handler in handlers) {
                ea.IsValid = true;
                handler(sender, ea);
                if (!ea.IsValid) {
                    result = false;
                    AppendErrorMessage(errors, ea.ErrorMessage);
                }
            }
            return result;
        }

        #endregion Validation

        #region String handling

        public static bool IsStringNotEmpty(string value)
        {
            return value != null && value.Length > 0;
        }

        public static TimeSpan ParseAmPmTime(string st)
        {
            if (time12Regex == null) {
                lock (typeof (ControlHelper)) {
                    if (time12Regex == null) {
                        time12Regex = new Regex(
                            @"^(?<hours>\d{1,2})\:(?<minutes>\d\d)(\:(?<seconds>\d\d))?\s{0,3}(?<ampm>(am)|(pm))$",
                            RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnoreCase);
                    }
                }
            }
            try {
                Match match = time12Regex.Match(st);
                if (!match.Success) {
                    throw new Exception("String not matches the pattern");
                }
                int h = Convert.ToInt32(match.Groups["hours"].Value);
                int m = Convert.ToInt32(match.Groups["minutes"].Value);
                string ss = match.Groups["seconds"].Value;
                int s = ss.Length == 0 ? 0 : Convert.ToInt32(ss);
                string ampm = match.Groups["ampm"].Value.ToUpper();
                switch (ampm) {
                    case "AM":
                        if (h == 12) {
                            h = 0;
                        }
                        break;
                    case "PM":
                        if (h < 12) {
                            h += 12;
                        }
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(string.Format("Invalid AM/PM specifier \"{0}\"", ampm));
                }
                if (h > 23) {
                    throw new ArgumentOutOfRangeException(string.Format("Invalid hour value ({0})", h));
                }
                if (m > 59) {
                    throw new ArgumentOutOfRangeException(string.Format("Invalid minute value ({0})", m));
                }
                if (s > 59) {
                    throw new ArgumentOutOfRangeException(string.Format("Invalid second value ({0})", s));
                }
                return new TimeSpan(0, h, m, s);
            } catch (Exception ex) {
                throw new ArgumentException(
                    string.Format(
                        "Invalid time string \"{0}\". Currently 12-hour time format is used",
                        st),
                    ex);
            }
        }

        #endregion String handling

        #region Client scripts

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

        public static void EmulateIE()
        {
            NeedDhtmlUtils();
            AspPage page = GetCurrentPage();
            if (!page.IsClientScriptBlockRegistered(EMULATE_IE_KEY)) {
                page.RegisterClientScriptBlock(EMULATE_IE_KEY, EMULATE_IE_SRC);
            }
        }

        public static bool IsClientScriptAdded(AspPage page, string key)
        {
            PageClientScripts scripts = PageClientScripts.GetForPage(page);
            return scripts.IsScriptAdded(key);
        }

        public static void AddClientScript(AspPage page, string key, string script)
        {
            AddClientScript(page, key, script, false);
        }

        public static void AddClientScript(AspPage page, string key, string script, bool isStartup)
        {
            PageClientScripts scripts = PageClientScripts.GetForPage(page);
            scripts.Add(key, script, isStartup);
        }

        #endregion Client scripts

        #region HTTP Context

        public static void EnforceClientTarget(string clientTarget)
        {
            AspPage page = GetCurrentPage();
            _realBrowser = HttpContext.Current.Request.Browser;
            page.ClientTarget = clientTarget;
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

        public static string GetVirtualDir()
        {
            HttpContext context = HttpContext.Current;
            string cachedVirtualDir = (string) context.Items["cachedVirtualDir"];
            if (cachedVirtualDir == null) {
                string rawUrl = context.Request.RawUrl;
                context.Items["cachedVirtualDir"] = cachedVirtualDir =
                                                    rawUrl.Substring(0, rawUrl.IndexOf("/", 1) + 1);
            }
            return cachedVirtualDir;
        }

        #endregion HTTP Context

        #endregion

        #region Properties

        public static HttpBrowserCapabilities RealBrowser
        {
            get
            {
                if (_realBrowser == null) {
                    return HttpContext.Current.Request.Browser;
                }
                return _realBrowser;
            }
        }

        #endregion Properties
    }
}