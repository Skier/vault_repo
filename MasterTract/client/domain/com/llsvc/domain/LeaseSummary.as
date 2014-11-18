package com.llsvc.domain
{
import mx.collections.ArrayCollection;

[Bindable]
[RemoteClass(alias="com.llsvc.server.doc.LeaseSummary")]
public class LeaseSummary
{
    public var grossAc:Number;
    public var netAc:Number;
    public var interest:Number;
    public var leaseBurden:Number;
    public var leaseNri:Number;
    public var wi:Number;
    public var additionalBurden:Number;
    public var nri:Number;
    public var net:Number;
    public var leaseNames:ArrayCollection;

	public function LeaseSummary()
	{
		leaseNames = new ArrayCollection();
	}

}
}