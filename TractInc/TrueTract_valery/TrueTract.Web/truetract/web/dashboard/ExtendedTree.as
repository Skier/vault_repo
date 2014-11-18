package truetract.web.dashboard
{
import mx.controls.Tree;
import mx.controls.listClasses.IListItemRenderer;
import mx.controls.treeClasses.TreeListData;
import mx.core.ClassFactory;
import mx.core.EventPriority;
import mx.events.DragEvent;

public class ExtendedTree extends Tree
{
    [Bindable] public var showDisclosureIcon:Boolean = true;
    [Bindable] public var useExtendedDropFeetback:Boolean = true;

    public function ExtendedTree()
    {
        addEventListener(DragEvent.DRAG_OVER, myDragOverHandler, false, 
            EventPriority.DEFAULT_HANDLER);
    }

    override protected function initListData(item:Object, treeListData:TreeListData):void
    {
        super.initListData(item, treeListData);

        if (!showDisclosureIcon)
            treeListData.disclosureIcon = null;
    }

    override public function showDropFeedback(event:DragEvent):void
    {
        if (useExtendedDropFeetback)
        {
            var dropIndex:int = calculateDropIndex(event);
            var item:IListItemRenderer = indexToItemRenderer(dropIndex);
    
            if (item)
            {
                var uid:String = itemToUID(item.data);
                drawItem(visibleData[uid], isItemSelected(item.data), true, uid == caretUID);
            }
        } 
        else
        {
            super.showDropFeedback(event);
        }
    }

    protected function myDragOverHandler(event:DragEvent):void
    {
        var dropIndex:int = calculateDropIndex(event);
        var item:IListItemRenderer = indexToItemRenderer(dropIndex);

        if (item && dataDescriptor.hasChildren(item.data))
        {
            expandItem(item.data, true, true);
        }
    }
}
}