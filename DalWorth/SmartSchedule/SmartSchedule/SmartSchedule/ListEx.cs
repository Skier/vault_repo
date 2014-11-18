using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchedule
{
    public class ListEx<T> : List<T>
    {
        public ListEx() {}
        public ListEx(IEnumerable<T> collection) : base(collection) {}

        #region Shuffle

        public void Shuffle()
        {
            Random random = new Random();

            Dictionary<T, int> map = new Dictionary<T, int>();
            foreach (T t in this)
                map.Add(t, random.Next());

            Sort(delegate(T x, T y) { return map[x].CompareTo(map[y]); });
        }

        #endregion
    }
}
