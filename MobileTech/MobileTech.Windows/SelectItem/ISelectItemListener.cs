using System;
using System.Collections.Generic;
using System.Text;
using MobileTech.Domain;

namespace MobileTech.Windows.UI.SelectItem
{
    public interface ISelectItemListener
    {
        int GetQuantity(Item item);

        void SetQuantity(Item item, int quantity);

        ItemTypeEnum GetItemType();
    }
}
