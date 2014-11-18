package src.deedplotter.domain.dictionary
{
	import flash.utils.ByteArray;
	
	public class DictionaryRegistry
	{
		[Embed(source="States.xml",mimeType="application/octet-stream")]
    	private static const statesAndCountiesFile:Class;
    	
    	[Embed(source="DocumentTypes.xml",mimeType="application/octet-stream")] 
    	private static const documentTypesFile:Class;
    	
    	[Embed(source="Units.xml",mimeType="application/octet-stream")] 
    	private static const unitsFile:Class;

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

        public function getDocumentType(documentTypeId:Number):XMLList
        {
            return documentTypes.DocumentType.(@DocTypeID == documentTypeId);
        }

        public function getStateName(stateId:Number):String
        {
            return statesAndCounties.state.(@StateId == stateId).@Name;
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
        }
        
	}
}