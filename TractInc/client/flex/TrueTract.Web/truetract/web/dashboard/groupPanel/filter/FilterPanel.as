package truetract.web.dashboard.groupPanel.filter
{
import flash.display.DisplayObject;

import mx.containers.TitleWindow;

import truetract.domain.IItemsFilter;
import truetract.domain.DictionaryRegistry;
import mx.managers.PopUpManager;

[Event(name="applyFilter", type="flash.events.Event")]
[Event(name="resetFilter", type="flash.events.Event")]

public class FilterPanel extends TitleWindow
{
    [Bindable] protected var dictionary:DictionaryRegistry = DictionaryRegistry.getInstance();

    private var _filter:IItemsFilter;
    [Bindable] public function get filter():IItemsFilter { return _filter; }
    public function set filter(value:IItemsFilter):void
    {
        _filter = value;
    }

    protected function close():void
    {
        PopUpManager.removePopUp(this);
    }
}
}