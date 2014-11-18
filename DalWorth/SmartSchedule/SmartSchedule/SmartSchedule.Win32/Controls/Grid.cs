using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SmartSchedule.Win32.Controls
{
    public class Grid : DevExpress.XtraGrid.GridControl
    {
        private bool m_isReadOnly;
        public bool IsReadOnly
        {
            get { return m_isReadOnly; }
            set { m_isReadOnly = value; }
        }

        protected override void OnDoubleClick(EventArgs ev)
        {
            if (m_isReadOnly)
                return;

            base.OnDoubleClick(ev);
        }

        protected override void OnDragDrop(DragEventArgs e)
        {
            if (m_isReadOnly)
                return;

            base.OnDragDrop(e);
        }

        protected override void OnDragOver(DragEventArgs e)
        {
            if (m_isReadOnly)
                return;

            base.OnDragOver(e);
        }
    }
}
