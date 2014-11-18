using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using hobson.controls;
using MobileTech.Windows.UI;

namespace dalworth.preview
{
    public partial class Tools : BaseForm
    {
        public Tools()
        {
            InitializeComponent();
        }

        protected override void OnFormLoad(EventArgs e)
        {
            m_btnCalculators.Picture = ImageKeys.Calc;
        }
    }
}