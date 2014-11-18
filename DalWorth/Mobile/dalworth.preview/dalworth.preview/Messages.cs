using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using dalworth.domain;
using dalworth.preview.Properties;
using hobson.controls;
using MobileTech.Windows.UI.Controls;

namespace dalworth.preview
{
    public partial class Messages : BaseForm
    {
        public Messages()
        {
            InitializeComponent();
        }

        protected override void OnFormLoad(EventArgs e)
        {
        }

        private void OnBackClick(object sender, EventArgs e)
        {
            Close();            
        }

        private void OnNextClick(object sender, EventArgs e)
        {
            LoadEquipment loadEquipment = new LoadEquipment();
            ShowForm(loadEquipment);
            if (Model.AppPoint == ApplicationPoint.StartDayDone)
                Close();                        
        }
    }
}