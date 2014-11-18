using System;
using System.Collections.Generic;
using System.Text;
using MobileTech.Domain;


namespace MobileTech.Windows.UI.ItemSearch
{
    public interface IItemSearchListener
    {
        void SetItem(Item product);

        ItemTypeEnum GetItemType();
    }
}
