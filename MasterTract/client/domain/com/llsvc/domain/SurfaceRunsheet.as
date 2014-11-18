package com.llsvc.domain
{
import flash.events.EventDispatcher;

import mx.binding.utils.ChangeWatcher;
import mx.collections.ArrayCollection;
import mx.formatters.CurrencyFormatter;

[Bindable]
[RemoteClass(alias="com.llsvc.server.entity.SurfaceRunsheetEntity")]
public class SurfaceRunsheet
{
    public var document:Document;
    public var instrument:String;
    public var no:String;
    public var dod:Date;
    public var dor:Date;
    public var description:String;
    public var remarks:String;
}
}
