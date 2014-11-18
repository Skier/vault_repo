package truetract.web.dashboard
{
import mx.controls.Tree;
import mx.controls.listClasses.IListItemRenderer;
import mx.controls.treeClasses.TreeListData;
import mx.core.ClassFactory;
import mx.events.DragEvent;

public class UserGroupTree extends Tree
{
    [Bindable] public var showDisclosureIcon:Boolean = true;
    
    public function UserGroupTree()
    {
        itemRenderer = new ClassFactory(UserGroupTreeIR);
    }

    override protected function initListData(item:Object, treeListData:TreeListData):void
    {
        super.initListData(item, treeListData);

        if (!showDisclosureIcon)
            treeListData.disclosureIcon = null;
    }

    override public function showDropFeedback(event:DragEvent):void
    {
        var itemIndex:int = calculateDropIndex(event);
        var item:IListItemRenderer = indexToItemRenderer(itemIndex);

        if (item)
        {
            var uid:String = itemToUID(item.data);
            drawItem(visibleData[uid], isItemSelected(item.data), true, uid == caretUID);
        }
    }
}
}