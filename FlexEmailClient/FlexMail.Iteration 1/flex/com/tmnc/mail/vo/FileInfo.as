package com.tmnc.mail.vo
{

    import mx.formatters.NumberFormatter;
        
    [RemoteClass(alias="Weborb.Samples.Email.Entities.FileInfo")]
    [Bindable]
    public class FileInfo {
        
        public var Name:String;
        public var Url:String;
        public var Size:Number = 0;
        
        //need for Data Provider support
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