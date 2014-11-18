package com.llsvc.client.lm
{
import mx.collections.ArrayCollection;

import com.llsvc.domain.User;

[Bindable]
[RemoteClass(alias="com.llsvc.server.doc.DocumentPackage")]
public class DocumentPackage
{
    public var stateList:ArrayCollection;
    public var projectList:ArrayCollection;
    public var documentTypeList:ArrayCollection;
    public var documentStatusList:ArrayCollection;
}
}
