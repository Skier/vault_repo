package com.dalworth.leadCentral.lead.convert.steps
{
	import com.dalworth.leadCentral.lead.convert.ConvertLeadToJobModel;
	
	public interface IWizardStep
	{
		function get isValid():Boolean;
		function init(model:ConvertLeadToJobModel):void;
		function updateModel():void;
	}
}