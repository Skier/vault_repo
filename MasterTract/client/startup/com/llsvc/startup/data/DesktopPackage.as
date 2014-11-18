package com.llsvc.startup.data
{
import mx.collections.ArrayCollection;

import com.llsvc.domain.User;

[Bindable]
[RemoteClass(alias="com.llsvc.server.framework.data.DesktopPackage")]
public class DesktopPackage
{
    public var user:User;
    
    public var moduleList:ArrayCollection;
}
}
