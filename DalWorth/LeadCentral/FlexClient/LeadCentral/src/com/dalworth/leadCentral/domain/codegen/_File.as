
package com.dalworth.leadCentral.domain.codegen
{
    import com.dalworth.leadCentral.domain.*;
    
    [Bindable]
    public class _File implements IDomainEntity
    {
        public function _File()
        {
        }
      
        private var id:int;
        public function get Id():int { return id; }
        public function set Id(value:int):void 
        {
            id = value;
        }
      
        private var storageKey:String;
        public function get StorageKey():String { return storageKey; }
        public function set StorageKey(value:String):void 
        {
            storageKey = value;
        }
      
        private var originalFileName:String;
        public function get OriginalFileName():String { return originalFileName; }
        public function set OriginalFileName(value:String):void 
        {
            originalFileName = value;
        }
      
        private var fileType:String;
        public function get FileType():String { return fileType; }
        public function set FileType(value:String):void 
        {
            fileType = value;
        }
      
        private var fileSize:Number;
        public function get FileSize():Number { return fileSize; }
        public function set FileSize(value:Number):void 
        {
            fileSize = value;
        }
      

        public function prepareToSend():File
        {
            var result:File = new File();
      
            result.Id = this.Id;
      
            result.StorageKey = this.StorageKey;
      
            result.OriginalFileName = this.OriginalFileName;
      
            result.FileType = this.FileType;
      
            result.FileSize = this.FileSize;
      
            return result;
        }

        public function applyFields(value:Object):void 
        {
            if (value == null)
                value = new File();
      
            this.Id = value["Id"];
      
            this.StorageKey = value["StorageKey"];
      
            this.OriginalFileName = value["OriginalFileName"];
      
            this.FileType = value["FileType"];
      
            this.FileSize = value["FileSize"];
      
        }
    }
}
      