package com.tmnc.mail.business.messages
{

import com.tmnc.components.treeDataClasses.ITreeItem;
import com.tmnc.components.treeDataClasses.TreeDataDescriptor;

import mx.collections.ArrayCollection;

public class MessageListDescriptor {

    [Bindable]
    public var messageList:ArrayCollection;
    
    [Bindable]
    public var messageTreeItemList:ArrayCollection;

    private var _treeDescriptor:TreeDataDescriptor = new TreeDataDescriptor();
    private var _lastSortField:String;
    
    private var _threatViewActive:Boolean = false;
    
    public function MessageListDescriptor():void{
        messageList = new ArrayCollection();
        messageTreeItemList = new ArrayCollection();
        messageTreeItemList.filterFunction = filterFunction;
    }
    
    public function set threadViewActive(value:Boolean):void {
        _threatViewActive = value;

        if (value){
            messageTreeItemList.sort = null;
            sortTreeByField( (_lastSortField) ? _lastSortField : "itemId");
        }
    }
    
    public function get threadViewActive():Boolean{
        return _threatViewActive;
    }
    
    public function addItem(item:MessageInfo):void {
        addItems([item]);
    }
    
    public function addItems(messageItemList:Array):void{
        var treeItemList:Array = [];

        for each (var item:MessageInfo in messageItemList){
            messageList.addItem(item);
            
            var treeItem:MessageTreeItem = new MessageTreeItem(item);
            messageTreeItemList.addItem(treeItem);
            treeItemList.push(treeItem);
        }

        _treeDescriptor.addItemsToHash(treeItemList);

        if (_threatViewActive){
            sortTreeByField( (_lastSortField) ? _lastSortField : "itemId");            
        }
    }
    
    public function deleteItem(messageId:int):void {
        var i:int;
        
        for (i = 0; i < messageList.length; i++){
            if (messageList[i].Uid == messageId){
                messageList.removeItemAt(i);
                break;
            }
        }
        
    }

    public function deleteItems(itemList:Array):void{
        var treeItemList:Array = [];
        
        var itemIndex:int;
        var treeItemIndex:int;
        
        for (var i:int = 0; i < itemList.length; i++){
            if (itemList[i] is MessageInfo){
                //delete MessageInfo item
                itemIndex = messageList.getItemIndex(itemList[i]);
                if (itemIndex != -1){
                    messageList.removeItemAt(itemIndex);
                }
                
                //find MessageTreeItem, delete it and add to candidates for deleting from hash
               treeItemIndex = getIndexOfTreeItemById(itemList[i].Uid);
                if (treeItemIndex != -1){
                    treeItemList.push(messageTreeItemList.removeItemAt(treeItemIndex));
                }
                
            } else if (itemList[i] is MessageTreeItem){
                //find MessageInfo item and delete it
                itemIndex = getIndexOfItemById(itemList[i].itemId);
                if (itemIndex != -1){
                    messageList.removeItemAt(itemIndex);
                }
                
                //delete MessageTreeItem and add to candidates for deleting from hash
                treeItemIndex = messageTreeItemList.getItemIndex(itemList[i]);
                if (treeItemIndex != -1){
                    treeItemList.push(messageTreeItemList.removeItemAt(treeItemIndex));                    
                }

            }
        }

        _treeDescriptor.removeItemsFromHash(treeItemList);
        
        if (_threatViewActive){
            sortTreeByField( (_lastSortField) ? _lastSortField : "itemId");            
        }
    }

    public function sortTreeByField(fieldName:String, desc:Boolean = false):void{
        _treeDescriptor.sortItems(messageTreeItemList, fieldName, desc);
        _lastSortField = fieldName;
    }

    public function getMessageItemById(msgUid:String):MessageInfo {
        var itemIndex:int = getIndexOfItemById(msgUid);
        if (itemIndex != -1){
            return messageList.getItemAt(itemIndex) as MessageInfo;
        }
        
        return null;
    }

        private function filterFunction(item:Object):Boolean {
            if (!_threatViewActive){
                return true;
            }
            
        return _treeDescriptor.treeFilterFunction(item);
        }    
        
    private function getIndexOfTreeItemById(itemId:String):int{
        
        for (var i:int = 0; i < messageTreeItemList.length; i++){
            if (messageTreeItemList[i].itemId == itemId){
                return i;
            }
        }
        
        return -1;
    }

    private function getIndexOfItemById(itemId:String):int{
        
        for (var i:int = 0; i < messageList.length; i++){
            if (messageList[i].Uid == itemId){
                return i;
            }
        }
        
        return -1;
    }
    
}
}