package truetract.domain
{
	import flash.utils.ByteArray;
	
	public class DictionaryRegistry
	{
		[Embed(source="/data/States.xml",mimeType="application/octet-stream")]
    	private static const statesAndCountiesFile:Class;
    	
    	[Embed(source="/data/DocumentTypes.xml",mimeType="application/octet-stream")] 
    	private static const documentTypesFile:Class;
    	
    	[Embed(source="/data/Units.xml",mimeType="application/octet-stream")] 
    	private static const unitsFile:Class;

    	[Embed(source="/data/DocumentAttachmentTypes.xml",mimeType="application/octet-stream")] 
    	private static const documentAttachmentTypesFile:Class;

        private static var _instance : DictionaryRegistry;
        
        public static function getInstance() : DictionaryRegistry
        {
            if ( _instance == null )
                _instance = new DictionaryRegistry( arguments.callee );
                
            return _instance;
        }

        public function DictionaryRegistry( caller : Function = null ) 
        {
            if(caller != DictionaryRegistry.getInstance)
            {
                throw new Error ("DictionaryRegistry is a singleton class, use getInstance() instead");
            }
            
            if (DictionaryRegistry._instance != null)
            {
                throw new Error( "Only one DictionaryRegistry instance should be instantiated" ); 
            }
            
            loadXMLs();
        }

		[Bindable] public var statesAndCounties:XML;
		[Bindable] public var documentTypes:XML;
		[Bindable] public var units:XML;
        [Bindable] public var docAttachmentTypes:XML;

        public function getDocumentAttachmentType(documentAttachmentTypeId:Number):XMLList
        {
            return docAttachmentTypes.DocumentAttachmentType.(@Id == documentAttachmentTypeId);
        }

        public function getDocumentType(documentTypeId:Number):XMLList
        {
            return documentTypes.DocumentType.(@DocTypeID == documentTypeId);
        }

        public function getState(stateId:Number):XMLList
        {
            return statesAndCounties.state.(@StateId == stateId);
        }

        public function getCountyName(stateId:Number, countyId:Number):String
        {
            return statesAndCounties.state.(@StateId == stateId).county.(@CountyId == countyId).@Name;
        }

        public function getUnitName(unitId:Number):String
        {
            return units.Unit.(@UnitId == unitId).@Name;
        }

        private function loadXMLs():void
        {
		    var ba:ByteArray;
		    
		    ba = new statesAndCountiesFile() as ByteArray;
        	statesAndCounties = new XML(ba.readUTFBytes(ba.length));

		    ba = new documentTypesFile() as ByteArray;
        	documentTypes = new XML(ba.readUTFBytes(ba.length));

		    ba = new unitsFile() as ByteArray;
        	units = new XML(ba.readUTFBytes(ba.length));

		    ba = new documentAttachmentTypesFile() as ByteArray;
        	docAttachmentTypes = new XML(ba.readUTFBytes(ba.length));
        }
        
	}
}