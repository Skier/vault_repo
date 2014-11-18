/**
 * Copyright(c) 2006 "Midnight Coders, LLC". All Rights Reserved.
 */

package Domain.Common
{
    import mx.collections.ArrayCollection;
    import mx.events.PropertyChangeEvent;
    import mx.core.Application;
    import mx.events.PropertyChangeEventKind;
    
    [RemoteClass(alias="Weborb.Samples.Ftp.Entities.FtpDirectory")]
    [Bindable]
    public class FtpDirectory extends FtpFile
    {

        private var m_files:ArrayCollection = new ArrayCollection();
        
        public function FtpDirectory(){}

		public function get Files():ArrayCollection {
			return m_files;
		}
        
        public function set Files(value:ArrayCollection):void{
            
            var newFiles:ArrayCollection = new ArrayCollection;
            
            if (this.Directory != null) {
                newFiles.addItem(ParentLink());
            }
            
            for each (var file:FtpFile in value) {
            	file.Directory = this;
            	var f:FtpFile = GetLocalCopy(file);
                if (f != null && f.IsDirectory)
                	newFiles.addItem(f);
                else
                    newFiles.addItem(file);
            }
            
            m_files.removeAll();
            
            for each (file in newFiles){
            	m_files.addItem(file);
            }

        }
        
        public static function Create(name:String, parent:FtpDirectory=null):FtpDirectory{

        	var result:FtpDirectory = new FtpDirectory();

        	result.Name = name;
        	result.IsDirectory = true;
        	result.Directory = parent;

        	if (parent != null) {
        		parent.Files.addItem(result);
        	}

			return result;        

        }
        
        private function GetLocalCopy(file:FtpFile):FtpFile{
            
            for each (var f:FtpFile in m_files) {
                if (f.Name == file.Name) {
                    return f;
                }
            }
            
            return null;
        }
        
        private function ParentLink():FtpFile{
            var parentLink:FtpFile = new FtpFile();
            parentLink.IsDirectory = true;
            parentLink.IsParentLink = true;
            parentLink.Name = "..";
            parentLink.Directory = this;
            parentLink.FileDate = null;        

            return parentLink;
        }

    }
}