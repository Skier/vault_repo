using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace QuickBooksAgent.Windows.UI.DatabaseManager.Explorer
{
    public class ExplorerController:SingleFormController<ExplorerModel,ExplorerView>
    {
        protected override void OnModelInitialize(object[] data)
        {
            base.OnModelInitialize(data);

            Debug.Assert(data != null && data.Length > 0,
                "Less one parameter requred");

            Debug.Assert(data[0] is List<Object>,
                "First parameter must be List<Object>");


            Model.Init((List<Object>)data[0]);
        }


        public override void OnViewLoad()
        {
            base.OnViewLoad();

            View.m_table.BindModel(Model);
            View.m_table.Focus();
            View.m_table.Select(0);
        }
    }
}
