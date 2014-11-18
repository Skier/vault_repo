package com.titus.catalog.model
{

[Bindable]
[RemoteClass(alias="Titus.ECatalog.Entity.SubmittalDataObject")]
public class Submittal
{
	
    public var FileCategory:String;

    public var FileCategoryId:int;

    public var Secure:int;

    public var FileType:String;

    public var FileTypeSort:int;

    public var FileId:int;

    public var FileName:String;
    
    public var isInCart:Boolean = false;

	public function Submittal()
	{
	}

}

}
