package TractInc.Domain
{
    [Bindable]
	[RemoteClass(alias="TractInc.Server.Domain.County")]
	public class County
	{
        public var CountyId:int;
        public var Name:String;
        public var StateId:int;
        public var StateName:String;
        public var StateFips:String;
        public var CountyFips:String;
        public var Fips:String;
	}
}