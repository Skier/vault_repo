using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

namespace MobileTech.Windows.UI.Inventory.Load.Damage
{
    public partial class InventoryLoadDamageView : InventoryLoadItemView
    {
        public InventoryLoadDamageView()
        {
            InitializeComponent();
        }

        protected override InventoryLoadItemModel CreateModelInstance()
        {
            return new InventoryLoadDamageModel();
        }

        protected override void OnApplyUIResources(CultureInfo cultureInfo)
        {
            base.OnApplyUIResources(cultureInfo);

            Resources.Culture = cultureInfo;

            Text = Resources.Title;
        }
    }
}