using System;
using System.Collections;

namespace Dpi.Central.Web.Controls
{
    /// <summary>
    /// Represents a collection of <see cref="Tab"/> objects in a <see cref="TabControl"/>.
    /// </summary>
    public class TabCollection : CollectionBase
    {
        public Tab this[int index]
        {
            get { return (Tab) List[index]; }
            set { List[index] = value; }
        }

        public int Add(Tab value)
        {
            return List.Add(value);
        }

        protected override void OnInsert(int index, Object value)
        {
            if (value.GetType() != typeof (Tab)) {
                throw new ArgumentException("value must be of type Tab.", "value");
            }
        }

        protected override void OnRemove(int index, Object value)
        {
            if (value.GetType() != typeof (Tab)) {
                throw new ArgumentException("value must be of type Tab.", "value");
            }
        }

        protected override void OnSet(int index, Object oldValue, Object newValue)
        {
            if (newValue.GetType() != typeof (Tab)) {
                throw new ArgumentException("newValue must be of type Tab.", "newValue");
            }
        }

        protected override void OnValidate(Object value)
        {
            if (value.GetType() != typeof (Tab)) {
                throw new ArgumentException("value must be of type Tab.");
            }
        }
    }
}