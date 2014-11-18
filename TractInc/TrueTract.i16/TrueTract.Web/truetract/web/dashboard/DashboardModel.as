package truetract.web.dashboard
{
    import flash.events.Event;
    
    import mx.collections.ArrayCollection;
    import mx.collections.ICollectionView;
    import mx.controls.Menu;
    import mx.events.FlexEvent;
    import mx.events.ListEvent;
    import mx.events.MenuEvent;
    
    import truetract.domain.UserGroup;
    
    [Bindable]
    public class DashboardModel
    {
        public static const ALL_ITEMS_GROUP_NAME:String = "All Items";
        public static const MY_ITEMS_GROUP_NAME:String = "My Items";
        public static const RECENT_ITEMS_GROUP_NAME:String = "Recent Items";

        public function DashboardModel()
        {
            folderListMenu.labelField="groupName";
            folderListMenu.addEventListener(Event.RENDER, function(event:Event):void {
                ICollectionView(folderListMenu.dataProvider).refresh();
            });
        }

        public var allItemsGroup:UserGroup;
        public var myItemsGroup:UserGroup;
        public var recentItemsGroup:UserGroup;

        public var userGroupList:ArrayCollection;

        public var selectedGroup:UserGroup;

        public var plotterMode:Boolean = false;

        public var folderListMenu:Menu = new Menu();

        public function reset():void
        {
            myItemsGroup = UserGroup.createSystemGroup(MY_ITEMS_GROUP_NAME);
            recentItemsGroup = UserGroup.createSystemGroup(RECENT_ITEMS_GROUP_NAME);

            allItemsGroup = UserGroup.createSystemGroup(ALL_ITEMS_GROUP_NAME);
            allItemsGroup.children = new ArrayCollection();
            allItemsGroup.groupDocuments = [];
            allItemsGroup.groupDrawings = [];
            allItemsGroup.isLoaded = true;
            allItemsGroup.children.addItem(myItemsGroup);
            allItemsGroup.children.addItem(recentItemsGroup);

            userGroupList = new ArrayCollection();
            userGroupList.addItem(allItemsGroup);

            var folderListProvider:ArrayCollection = new ArrayCollection(allItemsGroup.children.source);
            folderListProvider.filterFunction = folderListMenu_filterFunction;
            folderListMenu.dataProvider = folderListProvider;
        }

        private function folderListMenu_filterFunction(item:*):Boolean
        {
            return item.systemGroup == false;
        }
    }
}