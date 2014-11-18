package UI.Management.ServiceBrowser
{
	[RemoteClass(alias="Weborb.Management.ServiceBrowser.ServiceDataTypeContainer")]
	public class ServiceDataTypeContainer extends ServiceNode
	{
		[Bindable]
		public var DataType:ServiceDataType;
	}
}