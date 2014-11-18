package truetract.domain
{
    import mx.collections.ArrayCollection;

    import truetract.domain.Document;
    
    [Bindable]
    public class UserGroup
    {
        public var groupId:int;
        public var groupName:String;
        public var systemGroup:Boolean = false;
        public var isLoaded:Boolean = false;
        public var filter:IItemsFilter;

        /**
        * Array and ArrayCollection of group Items. 
        * Collection is used for binding. Array for WebOrb.
        */
        private var _groupItemsList:ArrayCollection = new ArrayCollection();
        public function get groupItemsList():ArrayCollection {
            return _groupItemsList;
        };

        private var _groupItems:Array;
        public function get groupItems():Array { return _groupItems; }
        public function set groupItems(value:Array):void 
        { 
            _groupItems = value;
            _groupItemsList.source = value;
        }

        public function applyFilter():void
        {
            if (!filter || groupItems.length == 0) {
                return;
            }

            filter.applyFilter(groupItemsList);
        }

    }
}