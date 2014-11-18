using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Globalization;
using System.Text;
using System.Windows.Forms;

namespace Dalworth.Windows.ServiceVisit.DeclineVisit
{
    public partial class DeclineVisitView : BaseControl
    {
        public DeclineVisitView()
        {
            InitializeComponent();
        }


        protected override void ApplyUIResources(CultureInfo cultureInfo)
        {
            base.ApplyUIResources(cultureInfo);

            Text = "Decline Visit";
        }
    }
}
