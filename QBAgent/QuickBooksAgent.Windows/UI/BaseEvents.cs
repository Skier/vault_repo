using System;


namespace QuickBooksAgent.Windows.UI
{
	public delegate void SimpleEvent();
	public delegate void SelectObjectsEvent(object[] objects);
	public delegate void SelectIndexEvent(int index);
}