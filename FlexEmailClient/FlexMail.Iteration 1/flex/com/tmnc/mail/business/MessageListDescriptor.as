package com.tmnc.mail.business
{

import mx.collections.ArrayCollection;
import flash.utils.Dictionary;
import mx.collections.Sort;
import mx.collections.SortField;
import com.tmnc.mail.view.MainView;
import com.tmnc.mail.vo.MessageInfo;
import com.tmnc.mail.view.helpers.ViewHelper;

public class MessageListDescriptor {

    private const DEFAULT_SORT_FIELD:String = "messageSentSortValue";

    [Bindable]
    public var treeList:ArrayCollection;
    public var useThreadView:Boolean = true;
    public var messagesCount:int = 0;
    
    //all messages in plain view. The hash key is Uid field
    private var _messagesUidMap:Object = new Object();
    
    //messages wich have own MessageId. Need for quick access to parent message.
    private var _messagesIdMap:Object = new Object();
        
    private var _lastSortDataField:String = DEFAULT_SORT_FIELD;
    private var _lastSortDirectionDesc:Boolean = true;
    
    public function MessageListDescriptor():void {
        treeList = new ArrayCollection();
        treeList.filterFunction = filterFunction;
    }

    public function rebuildTree():void {
        //need for restoring group state (opened/closed)
        var oldGroupsUidMap:Object = getGroupUIDMap();
        
        treeList.source.splice(0);
        
        for each(var message:MessageInfo in _messagesUidMap){
            
            var group:MessageTreeGroup = null;
            var topGroupMessage:MessageInfo = null;
            
            topGroupMessage = findMostTopBranchMessage(message);
            group = getGroupByUid(topGroupMessage.Uid);
            
            if (!group){
                if (topGroupMessage){
                    group = new MessageTreeGroup(topGroupMessage);                    
                } else {
                    group = new MessageTreeGroup(message);
                }

                treeList.addItem(group);                
            }
            
            treeList.addItem(new MessageTreeItem(message, group));
            if (oldGroupsUidMap[message.Uid]){
                group.isOpened = oldGroupsUidMap[message.Uid].isOpened;                
            }
        }
    }

    public function getMessagesUIDArray():Array {
        var result:Array = [];
        
        for (var uid:String in _messagesUidMap){
            result.push(uid);
        }
        
        return result;
    }
    
    public function addItems(items:Array):void {
        var lastSelectedItem:* = ViewHelper.SelectedMessageTreeItem;
        
        for each (var message:MessageInfo in items){
            
            _messagesUidMap[message.Uid] = message;
            if (message.MessageId){
                _messagesIdMap[message.MessageId] = message;
            }

            messagesCount++;            
        }
        
        rebuildTree();
        
        refreshProvider();
        
        restoreLastGridPosition(lastSelectedItem);
    }

    private function restoreLastGridPosition(lastSelectedItem:*):void {
        var result:*;
        
        if (!lastSelectedItem){
            return;
        }

        var correspondingItem:MessageTreeItem = getItemByUid(lastSelectedItem.message.Uid);
        
        if (lastSelectedItem is MessageTreeGroup){
            correspondingItem.parent.isOpened = lastSelectedItem.isOpened;
            ViewHelper.SelectedMessageTreeItem = correspondingItem.parent;
        } else {
            correspondingItem.parent.isOpened = true;
            ViewHelper.SelectedMessageTreeItem = correspondingItem;
        }
    }   
    
    public function deleteItems(messageList:Array):void {
        
        for each (var message:MessageInfo in messageList){
            delete _messagesUidMap[message.Uid];
            
            if (message.MessageId){
                delete _messagesIdMap[message.MessageId];
            }
            
            messagesCount--;
        }
        
        rebuildTree();
        
        refreshProvider();
    }
    
    public function refreshProvider():void {
        sortTree(_lastSortDataField, _lastSortDirectionDesc);        
    }

    public function sortTree(dataField:String, descDirection:Boolean):void {
        _lastSortDataField = dataField;
        _lastSortDirectionDesc = descDirection;
        
        if (treeList.source.length == 0){
            return;
        }
        
        var sortOptions:Number = Array.CASEINSENSITIVE;
        if (treeList.source[0][dataField] is Number) sortOptions |= Array.NUMERIC;
        if (descDirection) sortOptions |= Array.DESCENDING;
        
        if (!useThreadView){
            treeList.source.sortOn(dataField, sortOptions);
            treeList.refresh();
            return;
        }
        
        var groups:Array = getGroupArray();

        groups.sortOn(dataField, sortOptions);        
                        
        treeList.source.splice(0);
        treeList.refresh();
        
        for each(var group:MessageTreeGroup in groups){
            group.items.sortOn(dataField, sortOptions);
            
            treeList.addItem(group);
            
            for (var i:int=0; i<group.items.length;i++){
                treeList.addItem(group.items[i]);
            }
        }

        treeList.refresh();        
    }

    private function findMostTopBranchMessage(message:MessageInfo):MessageInfo {
        var result:MessageInfo = message;
        
        while (result.InReplyTo && _messagesIdMap[result.InReplyTo]){
            result = _messagesIdMap[result.InReplyTo];
        }

        return result;
    }
    
    private function getGroupByUid(uid:String):MessageTreeGroup {
        
        for each(var o:Object in treeList.source){
            if (o.message.Uid == uid){
                return (o is MessageTreeGroup) 
                    ? MessageTreeGroup(o) : MessageTreeGroup(o.parent);
            }
        }

        return null;
    }

    private function getItemByUid(uid:String):MessageTreeItem {
        for each(var o:Object in treeList.source){
            if (o.message.Uid == uid && !(o is MessageTreeGroup)){
                return MessageTreeItem(o);
            }
        }

        return null;
    }
            
    private function filterFunction(o:Object):Boolean {

        if (useThreadView){
            return (o.parent) ? o.parent.isOpened : true;
        } else {
            return !(o is MessageTreeGroup);
        }
    }
    
    private function getGroupArray():Array {
        var result:Array = [];
        
        for each(var o:Object in treeList.source){
            if (o is MessageTreeGroup) 
                result.push(o);
        }
        
        return result;
    }

    private function getGroupUIDMap():Object {
        var result:Object = new Object();
        
        for each(var o:Object in treeList.source){
            if (o is MessageTreeGroup) 
                result[o.message.Uid] = o;
        }
        
        return result;
    }    
}
}