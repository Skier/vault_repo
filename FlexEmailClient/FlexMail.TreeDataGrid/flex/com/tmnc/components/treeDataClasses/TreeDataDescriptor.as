package com.tmnc.components.treeDataClasses
{
import mx.collections.ArrayCollection;
import mx.controls.treeClasses.ITreeDataDescriptor;
import mx.graphics.Stroke;

/**
 *  The TreeDataDescriptor class provides an implementation for
 *  accessing and manipulating arrays of ITreeItem data.
**/

public class TreeDataDescriptor {

    private var itemsHash:Object = new Object();
    private var childrenHash:Object = new Object();
        
   public function addItemsToHash(itemList:Array):void{
       for (var i:int = 0; i < itemList.length; i++ ){
           addItem(itemList[i]);
       }
       
       refreshTree();
   }

   public function removeItemsFromHash(itemList:Array):void{
       for (var i:int = 0; i < itemList.length; i++ ){
           removeItem(itemList[i]);
       }
       
       refreshTree();
   }

	public function isBranchExpanded(item:ITreeItem):Boolean {
	    
	    var topItem:ITreeItem;
	            	    
	    var parentItem:ITreeItem = itemsHash[item.parentId];
	    if (!parentItem) //item has no parent
	        return true;
	            	    
	    while (parentItem){
	        topItem = parentItem;
	        parentItem = itemsHash[topItem.parentId];
	    }
	    
	    return topItem.isOpened;
	}
	
	public function isParentExpanded(item:ITreeItem):Boolean {
	    	    
	    if (item.parentId == "") // root item
	        return true;
	        
	    var parent:ITreeItem = itemsHash[item.parentId];    
	    if (!parent) // item has no parent
	        return true;
	    
        return parent.isOpened;
	}

    public function sortItems(model:ArrayCollection, fieldName:String, desc:Boolean = false):void{
        var topLevelItems:Array = [];

        //create array of the top level items (items in the top of branch)
        for each (var item:ITreeItem in itemsHash) {
            if (item.parentId.length == 0 || !itemsHash[item.parentId]){
                topLevelItems.push(item);
            }
        }

	    model.list.removeAll();
        sortLevel(model, topLevelItems, fieldName, desc);
    } 
        
    private function refreshTree():void {
             
        for each (var item:ITreeItem in itemsHash) {
            
            //if item has no parent - place it on the top of tree 
            //and recursively refresh his branch
            if (item.parentId.length == 0 || !itemsHash[item.parentId]){
                item.depthLevel = 0;
                refreshBranch(item);
            }

        }
    }

    private function refreshBranch(branchItem:ITreeItem):void{
        var itemChildren:Array = childrenHash[branchItem.itemId];
        
        if (itemChildren){
            branchItem.hasChildren = true;

            //if item has children go recursively thru them for setting depthLevel 
            //and hasChildren props.
            for each (var child:ITreeItem in itemChildren){
                child.depthLevel = branchItem.depthLevel + 1;
                refreshBranch(child);
            }
            
        } else {
            branchItem.hasChildren = false;
        }
    }
   
   private function addItem(item:ITreeItem):void {
        //add item to itemsHash
        itemsHash[item.itemId] = item;
        
        //add to parent Children
        if (!childrenHash[item.parentId])
            childrenHash[item.parentId] = [];
        
        var parentChildren:Array = childrenHash[item.parentId];
        parentChildren.push(item);
        
/*         //check is item has children
        var itemChildren:Array = childrenHash[item.itemId];
        if (itemChildren){
            item.hasChildren = true;
        }
 */    }
    
    private function removeItem(item:ITreeItem):void {
        //delete from itemsHash
        delete itemsHash[item.itemId];

        //delete from childsHash
        var parentChildren:Array = childrenHash[item.parentId];
        if (parentChildren){
            var itemIndex:int = parentChildren.indexOf(item);
            parentChildren.splice(itemIndex, 1);
            
            if (parentChildren.length == 0){
                delete childrenHash[item.parentId];
            }
        }
    }
    
    
    private function sortLevel(model:ArrayCollection, levelItems:Array, sortFieldName:String, desc:Boolean):void {
        if (!levelItems || levelItems.length == 0){
            return;
        }
        
        if (desc){
            levelItems.sortOn(sortFieldName, Array.DESCENDING);
        } else {
            levelItems.sortOn(sortFieldName);                    
        }
        
        for (var i:int=0; i < levelItems.length; i++){
             model.addItem(levelItems[i]);
             sortLevel(model, childrenHash[levelItems[i].itemId], sortFieldName, desc);
        }
    }

	public function treeFilterFunction(item:Object):Boolean {
	    var treeItem:ITreeItem = ITreeItem(item);
	    
        var result:Boolean = isBranchExpanded(treeItem);
        
        if (result){
            result = isParentExpanded(treeItem);
        }
        
        return result;
	}
}
}