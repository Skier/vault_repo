using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace SmartSchedule
{
    public class BindingListEx<T> : BindingList<T>
    {
        public delegate void BeforeRemoveHandler(int index);
        public event BeforeRemoveHandler BeforeRemove;

        protected override void RemoveItem(int index)
        {
            if (BeforeRemove != null)
                BeforeRemove.Invoke(index);
            base.RemoveItem(index);
        }


        protected override void ClearItems()
        {
            if (BeforeRemove != null)
            {
                for (int i = 0; i < Items.Count; i++)
                    BeforeRemove.Invoke(i);
            }
            
            base.ClearItems();
        }
    }
}
