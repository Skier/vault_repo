package com.llsvc.server.framework.data;

import java.io.Serializable;
import java.util.List;

import com.llsvc.server.entity.UserEntity;

public class DesktopPackage
    implements Serializable 
{
    public UserEntity user;

    public List moduleList;
}
