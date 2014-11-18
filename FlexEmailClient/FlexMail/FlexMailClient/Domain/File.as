package Domain
{
    [RemoteClass(alias="Weborb.Samples.Email.Entities.FileInfo")]
    [Bindable]
    public class File {
        
        public var Name:String;
        public var Url:String;
        public var Size:Number = 0;
		public var Text:String;
		
        public function get label():String{
            return Name + " (" + SizeDisplayValue + ")";
        }
                
        public function get SizeDisplayValue():String {
            if (Size > 1024){
                return Math.round(Size/1024) + " Kb";
            } else {
                return Size + " b";                
            }
        }
        
    }
}