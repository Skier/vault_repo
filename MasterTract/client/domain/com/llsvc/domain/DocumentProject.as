package com.llsvc.domain
{
import mx.collections.ArrayCollection;

[Bindable]
[RemoteClass(alias="com.llsvc.server.entity.DocumentProjectEntity")]
public class DocumentProject
{
    public var id:int;      
    public var document:Document;
    public var project:Project;    

    public function get projectName():String 
    {
    	if (project != null) 
    	{
    		return project.name;
    	} else 
    	{
    		return "n/a";
    	}
    }  

}
}
