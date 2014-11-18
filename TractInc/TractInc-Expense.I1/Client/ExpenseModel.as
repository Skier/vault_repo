package
{
    import flash.net.SharedObject;
    import mx.collections.ArrayCollection;
    import Domain.Asset;
    
    [Bindable]
    public class ExpenseModel
    {
        private const SHARED_OBJECT_NAME:String = "TractIncExpense";
        
        public var clientId:String;
        public var asset:Asset;
        public var isLoggedIn:Boolean = false;
        public var currentState:String = "Login";
        
        
        public var billItems:ArrayCollection = new ArrayCollection();
        public var newBillItems:ArrayCollection = new ArrayCollection();
        public var correctedBillItems:ArrayCollection = new ArrayCollection();
        
        
        
        public function ExpenseModel(){
            asset = new Asset(); /* this will have to happend during login"*/
            
    
        }
        public function saveToSharedObject():void {
            var sharedObject:SharedObject = SharedObject.getLocal(SHARED_OBJECT_NAME);
            sharedObject.data.clientId = clientId;
            sharedObject.data.billItems = this.billItems;
            sharedObject.flush();
            sharedObject.close();
        }
        
        public  function restoreFromSharedObject():void {
            
            var sharedObject:SharedObject = SharedObject.getLocal(SHARED_OBJECT_NAME);
            clientId = sharedObject.data.clientId;
            if (sharedObject.data.billItems != null){
                billItems = ArrayCollection(sharedObject.data.billItems);
            }
            else{
                billItems = new ArrayCollection();
            }
            sharedObject.close();
        }
        
        public function clearSharedObject():void{
            var sharedObject:SharedObject = SharedObject.getLocal(SHARED_OBJECT_NAME);
            sharedObject.clear();
            currentState = "Login";
            clientId = null;
            asset.AssetId = 0;
            isLoggedIn = false;
            billItems.removeAll();
            newBillItems.removeAll();
            correctedBillItems.removeAll();
            
            
            
        }
        
    }
}