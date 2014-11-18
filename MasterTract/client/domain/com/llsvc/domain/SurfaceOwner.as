package com.llsvc.domain
{
import flash.events.EventDispatcher;

import mx.binding.utils.ChangeWatcher;
import mx.collections.ArrayCollection;
import mx.formatters.CurrencyFormatter;

[Bindable]
[RemoteClass(alias="com.llsvc.server.entity.SurfaceOwnerEntity")]
public class SurfaceOwner
{
    public var id:int;
    public var tract:SurfaceTract;
    public var actor:DocumentActor;
    public var interest:Number;
    public var isOwner:Boolean;
    public var note:String;
}
}
