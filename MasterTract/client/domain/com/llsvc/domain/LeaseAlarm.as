package com.llsvc.domain
{
[Bindable]
[RemoteClass(alias="com.llsvc.server.entity.LeaseAlarmEntity")]
public class LeaseAlarm
{
	
    public var id:int;
    public var lease:Lease;
    public var clause:LeaseClause;
    public var alarmDate:Date;
    public var isActive:Boolean;

}
}