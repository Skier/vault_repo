using System;

using DPI.Interfaces;

namespace DPI.Components
{
	public class OperationMessenger
	{
		public static event EventHandler RefreshData;
		public static event EventHandler Restart;
		public static event EventHandler Stop;

		public static void OnRefreshData()
		{
			if (RefreshData != null)
				RefreshData(null, EventArgs.Empty);
		}
		public static void OnRestart()
		{
			if (Restart != null)
				Restart(null, EventArgs.Empty);
		}
		public static void OnStop()
		{
			if (Stop != null)
				Stop(null, EventArgs.Empty);
		}
	}
}