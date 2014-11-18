package UI.MessageBox.MessageTree
{

import Domain.Message;

import UI.MessageBox.MessageBoxController;

import flash.utils.Dictionary;

import mx.collections.ArrayCollection;
import mx.collections.Sort;
import mx.collections.SortField;
import mx.collections.IList;
import mx.binding.utils.ChangeWatcher;
import mx.utils.ObjectUtil;
import mx.events.PropertyChangeEvent;
import mx.events.CollectionEvent;
import mx.events.CollectionEventKind;

/**
 * Relations between messages are building according the next rules :
 *
 * Message may have identificator : MessageId field. Can be null.
 * Message may have a related parent message where (parent.MessageId = message.InReplyTo).
 *
**/

public class MessageTreeCollection extends ArrayCollection {

    private const DEFAULT_SORT_FIELD:String = "messageSentSortValue";

    [Bindable]
    public var ShowMessageGroups:Boolean = true;
    
    //Hashtable of all messages where the key is Message Uid value.
    private var m_uidsMap:Object = new Object();
    
    //Hashtable of messages which have own MessageId where the key is Message Id value. 
    //Need for quick access to parent message.
    private var m_idsMap:Object = new Object();

    private var m_groups:Array;
    
    private var m_lastSortDataField:String = DEFAULT_SORT_FIELD;
    private var m_isLastSortDirectionDesc:Boolean = true;
    
    private var m_lastSortField:SortField;
    
    public function MessageTreeCollection():void 
    {
        super();
        filterFunction = ThreadsFilterFunction;
        
        ChangeWatcher.watch(this, "ShowMessageGroups", OnRepresentationChanged);
    }

    public function AddItems(items:Array):void 
    {
        for each (var message:Message in items) 
        {
            m_uidsMap[message.Uid] = message;
            
            if (message.MessageId)
                m_idsMap[message.MessageId] = message;
        }
        
        RecreateCollection();
        
        refresh();
    }
    
    private function RecreateCollection():void 
    {
        //need for restoring group state (opened/closed)
        var oldGroupsUidsMap:Object = GetGroupUidsMap();
        
        m_groups = [];
        
        source.splice(0);
        
        for each(var message:Message in m_uidsMap){
            
            var topMessageInGroup:Message = FindTopRelatedMessage(message);
            var group:MessageTreeGroup = FindMessageGroup(topMessageInGroup.Uid);
            
            if (!group)
            {
                group = new MessageTreeGroup(topMessageInGroup);
                addItem(group);
                
                m_groups.push(group);
            }
            
            addItem(new MessageTreeItem(message, group));
            
            if (oldGroupsUidsMap[message.Uid])
            {
                group.isOpened = oldGroupsUidsMap[message.Uid].isOpened;                
            }
        }
    }

    public function RemoveItems(messageList:Array):void 
    {
        for each (var message:Message in messageList)
        {
            delete m_uidsMap[message.Uid];

            if (message.MessageId)
            {
                delete m_idsMap[message.MessageId];
            }
        }
        
        RecreateCollection();
        
        refresh();
    }
    
    override public function refresh():Boolean
    {
        var result:Boolean;
        var inSortField:SortField = null;
        
        if (sort && sort.fields.length > 0) 
        {
            inSortField = sort.fields[0];
            SortCollection(MessageTreeItem.AdjustSortField(inSortField));
        }

        sort = null;
        result = super.refresh();
        
        //restore sort object
        if (inSortField)
        {
            sort = new Sort();
            sort.fields = [inSortField];
        }
        
        return result;
    }

    private function OnRepresentationChanged(event:PropertyChangeEvent):void
    {
        refresh();
    }
    
     public function SortCollection(sr:SortField):void {
        if (source.length == 0)
            return;
        
        var sortOptions:Number = 0;
        if (sr.caseInsensitive) sortOptions != Array.CASEINSENSITIVE;
        if (sr.numeric) sortOptions |= Array.NUMERIC;
        if (sr.descending) sortOptions |= Array.DESCENDING;
        
        if (!ShowMessageGroups)
        {
            source.sortOn(sr.name, sortOptions);
            return;
        }
        
        m_groups.sortOn(sr.name, sortOptions);
        
        source.splice(0);
        
        for each(var group:MessageTreeGroup in m_groups)
        {
            group.items.sortOn(sr.name, sortOptions);
            
            addItem(group);
            
            for (var i:int = 0; i < group.items.length; i++)
            {
                addItem(group.items[i]);
            }
        }
    }
    
    //Find the topest message in m_idsMap hastable that is parent of 
    private function FindTopRelatedMessage(message:Message):Message 
    {
        var result:Message = message;
        
        while (result.InReplyTo && m_idsMap[result.InReplyTo])
        {
            result = m_idsMap[result.InReplyTo];
        }
        
        return result;
    }
    
    private function FindMessageGroup(messageUid:String):MessageTreeGroup 
    {
        var result:MessageTreeGroup = null;
        
        for each( var o:Object in source )
        {
            if (o.message.Uid == messageUid)
            {
                result = (o is MessageTreeGroup) ? MessageTreeGroup(o) : MessageTreeGroup(o.parent);
            }
        }

        return result;
    }

    private function GetGroupUidsMap():Object 
    {
        var result:Object = new Object();
        
        for each(var o:Object in source)
        {
            if (o is MessageTreeGroup)
                result[o.message.Uid] = o;
        }
        
        return result;
    }    
    
    private function ThreadsFilterFunction(o:Object):Boolean 
    {
        if (ShowMessageGroups)
            return (o.parent) ? o.parent.isOpened : true;
        else
            return !(o is MessageTreeGroup);
    }
    
}
}