using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MobileTech.Windows.UI.Inventory.Unload.Equipment
{
    public partial class InventoryUnloadEquipmentView : InventoryUnloadItemView
    {
        public InventoryUnloadEquipmentView()
        {
            InitializeComponent();
        }

        protected override InventoryUnloadItemModel CreateModelInstance()
        {
            return new InventoryUnloadEquipmentModel();
        }

        protected override void OnApplyUIResources(System.Globalization.CultureInfo cultureInfo)
        {
            base.OnApplyUIResources(cultureInfo);

            Resources.Culture = cultureInfo;

            Text = Resources.Title;
        }
    }
}