package truetract.plotter.components
{
    import mx.controls.Tree;
    import mx.controls.treeClasses.TreeListData;

    public class ExtendedTree extends Tree
    {
        [Bindable] public var showDisclosureIcon:Boolean = true;
        
        override protected function initListData(item:Object, treeListData:TreeListData):void
        {
            super.initListData(item, treeListData);
    
            if (!showDisclosureIcon)
                treeListData.disclosureIcon = null;
        }
    }
}