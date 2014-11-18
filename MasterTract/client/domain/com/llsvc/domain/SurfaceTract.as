package com.llsvc.domain
{
import flash.events.EventDispatcher;

import mx.binding.utils.ChangeWatcher;
import mx.collections.ArrayCollection;
import mx.formatters.CurrencyFormatter;

[Bindable]
[RemoteClass(alias="com.llsvc.server.entity.SurfaceTractEntity")]
public class SurfaceTract
{
    public var document:Document;
    public var glink:String;
    public var no:String;
    public var acres:Number = 0;
    public var owners:ArrayCollection;
}
}
