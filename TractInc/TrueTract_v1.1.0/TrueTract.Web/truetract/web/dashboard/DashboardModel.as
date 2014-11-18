package truetract.web.dashboard
{
    import flash.events.Event;
    
    import mx.collections.ArrayCollection;
    import mx.collections.ICollectionView;
    import mx.controls.Menu;
    import mx.events.FlexEvent;
    import mx.events.ListEvent;
    import mx.events.MenuEvent;
    
    import truetract.domain.UserDocumentsGroup;
    import truetract.domain.UserDrawingsGroup;
    import truetract.domain.UserGroup;
    
    [Bindable]
    public class DashboardModel
    {
        public static const ALL_DOCUMENTS_GROUP_NAME:String = "All Documents";
        public static const MY_DOCUMENTS_GROUP_NAME:String = "My Documents";
        public static const MY_DRAWINGS_GROUP_NAME:String = "My Drawings";
        public static const RECENT_DOCUMENTS_GROUP_NAME:String = "Recent Documents";

        public function DashboardModel()
        {
        }

        public var userGroupList:ArrayCollection;

        public var selectedGroup:UserGroup;

        public var plotterMode:Boolean = false;

        public function reset():void
        {
            var g:UserGroup;

            userGroupList = new ArrayCollection();

            g = UserDocumentsGroup.createSystemGroup(ALL_DOCUMENTS_GROUP_NAME);
            g.isLoaded = true;
            userGroupList.addItem(g);
            
            g = UserDocumentsGroup.createSystemGroup(MY_DOCUMENTS_GROUP_NAME);
            userGroupList.addItem(g);

            g = UserDrawingsGroup.createSystemGroup(MY_DRAWINGS_GROUP_NAME);
            userGroupList.addItem(g);

            g = UserDocumentsGroup.createSystemGroup(RECENT_DOCUMENTS_GROUP_NAME);
            userGroupList.addItem(g);
        }
        
        public function get myDocumentsGroup():UserDocumentsGroup 
        {
            return UserDocumentsGroup(getGroupByName(MY_DOCUMENTS_GROUP_NAME));
        }

        public function get myDrawingsGroup():UserDrawingsGroup
        {
            return UserDrawingsGroup(getGroupByName(MY_DRAWINGS_GROUP_NAME));
        }
        
        public function getGroupByName(groupName:String):UserGroup
        {
            var result:UserGroup;
            
            for each (var g:UserGroup in userGroupList)
            {
                if (g.groupName == groupName)
                    result = g;
            }

            return result;
        }
    }
}