using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text;

namespace MobileTech.Windows.UI
{
	public class WaitCursor:IDisposable
	{
		bool m_disposed;

		public WaitCursor()
		{
			Cursor.Current = Cursors.WaitCursor;
			Cursor.Show();
		}

		#region IDisposable Members

		public void Dispose()
		{
			if (!m_disposed)
			{
				m_disposed = true;

				Cursor.Current = Cursors.Default;
				Cursor.Hide();

				GC.SuppressFinalize(this);
			}
		}

		#endregion
    }

}
