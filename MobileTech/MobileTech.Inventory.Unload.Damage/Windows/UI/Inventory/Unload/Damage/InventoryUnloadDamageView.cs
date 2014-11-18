using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MobileTech.Windows.UI.Inventory.Unload.Damage
{
    public partial class InventoryUnloadDamageView : InventoryUnloadItemView
    {
        public InventoryUnloadDamageView()
        {
            InitializeComponent();
        }

        protected override InventoryUnloadItemModel CreateModelInstance()
        {
            return new InventoryUnloadDamageModel();
        }

        protected override void OnApplyUIResources(System.Globalization.CultureInfo cultureInfo)
        {
            base.OnApplyUIResources(cultureInfo);

            Resources.Culture = cultureInfo;

            Text = Resources.Title;
        }
    }
}