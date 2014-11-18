using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

namespace MobileTech.Windows.UI.Inventory.Load.Equipment
{
    public partial class InventoryLoadEquipmentView : InventoryLoadItemView
    {
        public InventoryLoadEquipmentView()
        {
            InitializeComponent();
        }

        protected override InventoryLoadItemModel CreateModelInstance()
        {
            return new InventoryLoadEquipmentModel();
        }

        protected override void OnApplyUIResources(CultureInfo cultureInfo)
        {
            base.OnApplyUIResources(cultureInfo);

            Resources.Culture = cultureInfo;

            Text = Resources.Title;
        }
    }
}