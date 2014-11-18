package com.llsvc.domain
{
import mx.collections.ArrayCollection;

[Bindable]
[RemoteClass(alias="com.llsvc.server.entity.LeaseFilterEntity")]
public class LeaseFilter
{
    public var state:State;
    public var county:County;
    public var volume:String;
    public var page:String;

    public var leaseName:String;

    public var leaseFromDate:Date;
    public var leaseToDate:Date;

    public var effectiveFromDate:Date;
    public var effectiveToDate:Date;

	public var townshipRange:String;
    
}
}
