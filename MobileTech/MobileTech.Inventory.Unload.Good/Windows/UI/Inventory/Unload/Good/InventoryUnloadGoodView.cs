using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MobileTech.Domain;

namespace MobileTech.Windows.UI.Inventory.Unload.Good
{
    public partial class InventoryUnloadGoodView : InventoryUnloadItemView
    {
        protected override InventoryUnloadItemModel CreateModelInstance()
        {
            return new InventoryUnloadGoodModel();
        }

        protected override void OnApplyUIResources(System.Globalization.CultureInfo cultureInfo)
        {
            base.OnApplyUIResources(cultureInfo);

            Resources.Culture = cultureInfo;

            Text = Resources.Title;
        }
    }
}