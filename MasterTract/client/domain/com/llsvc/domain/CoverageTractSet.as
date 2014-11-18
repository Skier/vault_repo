package com.llsvc.domain
{
import mx.collections.ArrayCollection;

[Bindable]
[RemoteClass(alias="com.llsvc.server.entity.CoverageTractSetEntity")]
public class CoverageTractSet
{
    public var id:int;
    public var coverageTract:CoverageTract;
    public var externalId:int;
}
}
