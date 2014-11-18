package AerSysCo.UI.Catalog.Categories
{
    import mx.controls.treeClasses.ITreeDataDescriptor;
    import mx.collections.*;
    import AerSysCo.UI.Models.ModelUI;
    import AerSysCo.UI.Models.CategoryUI;

    public class CategoryTreeDataDescriptor implements ITreeDataDescriptor
    {
        public function getData(node:Object, model:Object=null):Object
        {
            return node;
        }
        
        public function hasChildren(node:Object, model:Object=null):Boolean
        {
        	if (node == null || null != (node as ModelUI))
        		return false;
        	else 	
				return (CategoryUI(node).children.length > 0);
        }
        
        public function addChildAt(parent:Object, newChild:Object, index:int, model:Object=null):Boolean
        {
            return false;
        }
        
        public function isBranch(node:Object, model:Object=null):Boolean
        {
        	return (null == node as ModelUI) && (CategoryUI(node).children.length > 0); 

//        	return (!Category(node).isLeaf());
        }
        
        public function removeChildAt(parent:Object, child:Object, index:int, model:Object=null):Boolean
        {
            return false;
        }
        
        public function getChildren(node:Object, model:Object=null):ICollectionView
        {
            var result:ArrayCollection = new ArrayCollection();
        	var parent:CategoryUI = node as CategoryUI;
            if ( null != parent ) {
            for each(var o:Object in parent.children)
            {
                result.addItem(o);
            }
            if (result.length > 0) 
            {
            	return result;
            } else 
            {
            	return null;
            }
            } else {
            	return null;
            }
        }
    }
}
