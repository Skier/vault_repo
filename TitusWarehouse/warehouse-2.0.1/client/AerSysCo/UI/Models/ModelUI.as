package AerSysCo.UI.Models
{
	import AerSysCo.Server.Model;
	
	[Bindable]
	public class ModelUI
	{
	    public var modelId:int;
	    public var brandId:int;
	    public var categoryId:int;
	    public var modelName:String;
	    public var isActive:Boolean;
	    
	    public function get name():String 
	    {
	    	return modelName;
	    }
	    
	    public function populateFromModel(value:Model):void 
	    {
	    	this.modelId = value.modelId;
	    	this.brandId = value.brandId;
	    	this.categoryId = value.categoryId;
	    	this.modelName = value.modelName;
	    	this.isActive = value.isActive;
	    }
	    
	    public function toModel():Model 
	    {
	    	var result:Model = new Model();
	    	
	    	result.modelId = this.modelId;
	    	result.brandId = this.brandId;
	    	result.categoryId = this.categoryId;
	    	result.modelName = this.modelName;
	    	result.isActive = this.isActive;
	    	result.dateCreated = new Date();
	    	result.lastUpdateDate = new Date();
	    	
	    	return result;
	    }
	}
}