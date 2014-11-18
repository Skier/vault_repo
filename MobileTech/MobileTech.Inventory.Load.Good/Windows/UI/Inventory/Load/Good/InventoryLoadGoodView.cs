using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

namespace MobileTech.Windows.UI.Inventory.Load.Good
{
    public partial class InventoryLoadGoodView : InventoryLoadItemView
    {
        public InventoryLoadGoodView()
        {
            InitializeComponent();
        }

        protected override InventoryLoadItemModel CreateModelInstance()
        {
            return new InventoryLoadGoodModel();
        }

        protected override void OnApplyUIResources(CultureInfo cultureInfo)
        {
            base.OnApplyUIResources(cultureInfo);

            Resources.Culture = cultureInfo;

            Text = Resources.Title;
        }
    }
}