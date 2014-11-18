package truetract.web.dashboard
{
    import mx.collections.ArrayCollection;
    
    [Bindable]
    [RemoteClass(alias="TractInc.TrueTract.Entity.UserGroupInfo")]
    public class UserGroup
    {

        public static function createSystemGroup(groupName:String):UserGroup
        {
            var result:UserGroup = new UserGroup();
            result.groupName = groupName;
            result.systemGroup = true;
            
            return result;
        }

        public var groupId:int;

        public var groupName:String;

        public var systemGroup:Boolean;

        public var isLoaded:Boolean = false;

        public var children:ArrayCollection;

        /**
        * Array and ArrayCollection of group Documents. Collection for binding Array for WebOrb.
        */        
        private var _groupDocumentsList:ArrayCollection = new ArrayCollection();
        public function get groupDocumentsList():ArrayCollection {
            return _groupDocumentsList;
        };

        private var _groupDocuments:Array;
        public function get groupDocuments():Array { return _groupDocuments; }
        public function set groupDocuments(value:Array):void 
        { 
            _groupDocuments = value;
            _groupDocumentsList.source = value;
        }

        /**
        * Array and ArrayCollection of group Drawings. Collection for binding Array for WebOrb.
        */
        private var _groupDrawingsList:ArrayCollection = new ArrayCollection();
        public function get groupDrawingsList():ArrayCollection {
            return _groupDrawingsList;
        };

        private var _groupDrawings:Array;
        public function get groupDrawings():Array { return _groupDrawings; }
        public function set groupDrawings(value:Array):void 
        { 
            _groupDrawings = value;
            groupDrawingsList.source = value;
        }

    }
}