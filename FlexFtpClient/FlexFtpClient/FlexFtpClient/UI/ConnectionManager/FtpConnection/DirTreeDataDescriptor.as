/**
 * Copyright(c) 2006 "Midnight Coders, LLC". All Rights Reserved.
 */

package UI.ConnectionManager.FtpConnection
{
    import mx.controls.treeClasses.ITreeDataDescriptor;
    import mx.collections.*;
    import Domain.Common.*;

    public class DirTreeDataDescriptor implements ITreeDataDescriptor
    {
        public function getData(node:Object, model:Object=null):Object
        {
            return node;
        }
        
        public function hasChildren(node:Object, model:Object=null):Boolean
        {
            var ftpDirectory:FtpDirectory = FtpDirectory(node);
            if (ftpDirectory.Files.length > 0){
                return true;
            } else {
                return false;
            }
        }
        
        public function addChildAt(parent:Object, newChild:Object, index:int, model:Object=null):Boolean
        {
            return false;
        }
        
        public function isBranch(node:Object, model:Object=null):Boolean
        {
            return true;
        }
        
        public function removeChildAt(parent:Object, child:Object, index:int, model:Object=null):Boolean
        {
            return false;
        }
        
        public function getChildren(node:Object, model:Object=null):ICollectionView
        {
            var arr:ArrayCollection = new ArrayCollection();
            
            var ftpDirectory:FtpDirectory = FtpDirectory(node);
            
            for each(var file:FtpFile in ftpDirectory.Files)
            {
                if(file.IsDirectory && !file.IsParentLink)
                    arr.addItem(file);
            }
            
            return arr;
        }
    }
}
