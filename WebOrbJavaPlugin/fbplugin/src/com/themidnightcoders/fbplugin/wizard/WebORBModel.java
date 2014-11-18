package com.themidnightcoders.fbplugin.wizard;
/**
 * Model class containing the data for the WebORBWizard
 */


import org.eclipse.core.resources.IProject;

import com.themidnightcoders.DatabaseInfo;
import com.themidnightcoders.DatabaseInfoType;
import com.themidnightcoders.ServiceSoap;
import com.themidnightcoders.fbplugin.service.QueryDescription;

public class WebORBModel 
{
    public boolean toDisplay;
    public String connectionURL;    
    public ServiceSoap svcSoap;
    public IProject project;
    
    public DatabaseInfoType type;
    public String host;
    public String port;
    public String username;
    public String password;
    public String database;

    public String instanceId;
    public String[] columns;
    public QueryDescription query;
}
