package truetract.web.dashboard
{
    import mx.collections.ArrayCollection;
    import mx.controls.Menu;
    import mx.events.FlexEvent;
    import mx.events.MenuEvent;
    import mx.collections.ICollectionView;
    import flash.events.Event;
    import mx.events.ListEvent;
    
    [Bindable]
    public class DashboardModel
    {
        public static const MY_ITEMS_GROUP:String = "My Items";
        public static const RECENT_ITEMS_GROUP:String = "Recent Items";

        public function DashboardModel()
        {
            folderListMenu.labelField="groupName";
            folderListMenu.addEventListener(Event.RENDER, function(event:Event):void {
                ICollectionView(folderListMenu.dataProvider).refresh();
            });
        }

        public var myItemsGroup:UserGroup;
        public var recentItemsGroup:UserGroup;

        public var userGroupList:ArrayCollection;

        public var selectedGroup:UserGroup;

        public var plotterMode:Boolean = false;

        public var folderListMenu:Menu = new Menu();

        public function reset():void
        {
            myItemsGroup = UserGroup.createSystemGroup(MY_ITEMS_GROUP);
            recentItemsGroup = UserGroup.createSystemGroup(RECENT_ITEMS_GROUP);

            userGroupList = new ArrayCollection();
            userGroupList.addItem(myItemsGroup);
            userGroupList.addItem(recentItemsGroup);

            var folderListProvider:ArrayCollection = new ArrayCollection(userGroupList.source);
            folderListProvider.filterFunction = folderListMenu_filterFunction; 
            folderListMenu.dataProvider = folderListProvider;
        }

        private function folderListMenu_filterFunction(item:*):Boolean
        {
            return item != selectedGroup && !item.systemGroup;
        }
    }
}