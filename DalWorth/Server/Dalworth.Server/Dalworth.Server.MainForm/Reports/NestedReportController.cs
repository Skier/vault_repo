using System;
using System.Collections.Generic;
using System.Text;
using Dalworth.Server.Windows;

namespace Dalworth.Server.MainForm.Reports
{
    public abstract class NestedReportController<TModel, TView> : NestedController<TModel, TView>
        where TView : BaseControl, new()
        where TModel: new()
    {
        public abstract bool IsPreviewImplemented();
        public abstract void OnPreview();

        public abstract bool IsPrintImplemented();
        public abstract void OnPrint();

        public abstract bool IsXlsExportImplemented();
        public abstract void ExportXls(string path);        
    }
}
