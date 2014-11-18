/**
 * Copyright(c) 2006 "Midnight Coders, LLC". All Rights Reserved.
 */

package Domain.Common
{
    [Bindable]
    [RemoteClass(alias="Weborb.Samples.Ftp.Entities.FtpFile")]
    public class FtpFile
    {
        public var Name:String;
        public var Size:int;
        public var FileDate:Date;
        public var Ext:String;
        public var Permission:String;
        public var Directory:FtpDirectory;
        public var IsDirectory:Boolean = false;
        public var IsParentLink:Boolean = false;
        
        public function FtpFile():void{
            IsDirectory = this is FtpDirectory;
        }
        
        public function getPath():String
        {
            var directory:FtpDirectory = Directory;
            var path:String = Name;
            
            while(directory != null)
            {
                path = directory.Name + "/" + path;
                directory = directory.Directory;
            }
            
            if (this is FtpDirectory) {
                path += "/"
            }
            
            return path;
        }
        
        public function SimpleClone():FtpFile
        {
        	var result:FtpFile = new FtpFile;
        	result.Name = this.Name;
        	result.IsDirectory = this.IsDirectory;
        	result.Size = this.Size;
        	
        	return result;
        	
        }
            
    }
}