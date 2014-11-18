package truetract.domain
{
    import mx.collections.ArrayCollection;
    import mx.states.State;
    
    import truetract.plotter.domain.Document;
    
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

        public var documentFilter:DocumentFilter = new DocumentFilter();

        /**
        * Array and ArrayCollection of group Documents. 
        * Collection is used for binding. Array for WebOrb.
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
        * Array and ArrayCollection of group Drawings. 
        * Collection is used for binding. Array for WebOrb.
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
        
        public function applyFilter():void
        {
            var filter:DocumentFilter = documentFilter;

            if (documentFilter && documentFilter.isSpecified())
            {
                for (var i:int = 0; i < groupDocumentsList.length; i++)
                {
                    var doc:Document = groupDocumentsList[i];

                    if (   (filter.stateId > 0 && doc.State != filter.stateId)
                        || (filter.countyId > 0 && doc.County != filter.countyId)
                        || (filter.docTypeId > 0 && doc.DocTypeId != filter.docTypeId)
                        || (filter.docNumber && doc.DocumentNo != filter.docNumber)
                        || (filter.volume && doc.Volume != filter.volume)
                        || (filter.page && doc.Volume != filter.volume)
                        || (filter.seller && doc.Seller.AsNamed.indexOf(filter.seller) == -1)
                        || (filter.buyer && doc.Buyer.AsNamed.indexOf(filter.buyer) == -1)
                        || (filter.createdRange && !filter.createdRange.inRange(doc.Created))
                        || (filter.filedRange && !filter.filedRange.inRange(doc.DateFiled))
                        || (filter.signedRange && !filter.signedRange.inRange(doc.DateSigned)))
                    {
                        groupDocumentsList.removeItemAt(i);
                        break;
                    }
                }
            }
        }
    }
}