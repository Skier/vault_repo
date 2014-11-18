package com.dalworth.servman.main.lead.convert.steps
{
	import com.dalworth.servman.main.lead.convert.ConvertLeadToJobModel;
	
	public interface IWizardStep
	{
		function get isValid():Boolean;
		function init(model:ConvertLeadToJobModel):void;
		function updateModel():void;
	}
}