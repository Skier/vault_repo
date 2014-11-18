using System;
using System.Collections.Generic;
using System.Text;

namespace MobileTech.Windows.UI
{
    public interface IView
    {
        void BindData(Object data);

        IApplication App {get;set;}

        void InitModel();
    }
}
