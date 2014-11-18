package truetract.domain
{
import flash.events.EventDispatcher;

import mx.events.PropertyChangeEvent;
import mx.events.PropertyChangeEventKind;
import mx.collections.ArrayCollection;

[Bindable]
public class DrawingsFilter extends EventDispatcher implements IItemsFilter
{
    public var refName:String;

    public function DrawingsFilter()
    {
        reset();
    }

    public function isSpecified():Boolean
    {
        return (refName != null);
    }

    public function reset():void
    {
        refName = null;

        dispatchEvent(new PropertyChangeEvent(PropertyChangeEvent.PROPERTY_CHANGE, 
            true, false, PropertyChangeEventKind.UPDATE, "isSpecified"));
    }
    
    public function applyFilter(itemsList:ArrayCollection):void
    {
        if (!isSpecified()) return;

        for (var i:int = 0; i < itemsList.length; i++)
        {
            var tract:Tract = Tract(itemsList.getItemAt(i));

            if (refName && tract.RefName.indexOf(refName) == -1)
            {
                itemsList.removeItemAt(i);
                break;
            }
        }
    }
    
    public function getFilterParams():Object
    {
        var params:Object = new Object();
        
        if (refName != null) {
            params["Ref.name"] = refName;
        }
        
        return params;
    }
}
}