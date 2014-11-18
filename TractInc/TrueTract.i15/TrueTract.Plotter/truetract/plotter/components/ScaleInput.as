package truetract.plotter.components
{
import flash.events.Event;

import mx.controls.Label;
import mx.controls.Text;
import mx.controls.TextInput;
import mx.controls.listClasses.BaseListData;
import mx.controls.listClasses.IDropInListItemRenderer;
import mx.events.PropertyChangeEvent;

import truetract.plotter.components.scaleInputClasses.ScaleEditor;
import truetract.plotter.components.scaleInputClasses.classes.IPEBase;
import truetract.plotter.utils.ScaleValue;

[Event(name="change", type="flash.events.Event")]
[Event(name="enter", type="mx.events.FlexEvent")]
[Event(name="textInput", type="flash.events.TextEvent")]
public class ScaleInput extends IPEBase
{
	public function ScaleInput():void
	{
		super();
		nonEditableControl = new Label();
		editableControl = new ScaleEditor();

		facadeEvents(editableControl,"change","enter","textInput","valueCommit");
	}
	
	override protected function commitEditedValue():Boolean
	{
	    var oldValue:ScaleValue = editor.Model;
	    
	    var result:Boolean = editor.Commit();
	    
	    if (result) 
	        Label(nonEditableControl).text = editor.Model.toString();

        dispatchEvent(new Event(Event.CHANGE));
        
        return result;		
	}
    	
	public function set scaleValue(value:ScaleValue):void
	{
		editor.Model = value;
		
		Label(nonEditableControl).text = value.toString();
	}
	
	public function get scaleValue():ScaleValue 
	{
	    return editor.Model;
	}
	
	private function get editor():ScaleEditor 
	{
	    return ScaleEditor(editableControl);
	}
	
}
}