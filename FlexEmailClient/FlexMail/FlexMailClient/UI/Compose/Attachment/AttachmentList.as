package UI.Compose.Attachment
{
    import mx.containers.Canvas;
    import flash.utils.Dictionary;
    import flash.net.URLRequest;
    import flash.net.FileReference;
    import mx.effects.Move;
    import mx.events.EffectEvent;
    import UI.Compose.Attachment.AttachmentItem.AttachmentItemView;
    import flash.net.FileReferenceList;
    import flash.events.Event;
    
    public class AttachmentList extends Canvas
    {

        [Bindable]
        public var TotalSize:int = 0;
        
        public var UploadRequestURL:URLRequest;
        
        public var Items:Array = [];

        private var m_playingEffects:Dictionary;

        private var m_fileRefList:FileReferenceList;

        public function AttachmentList():void
        {
            m_fileRefList = new FileReferenceList();
            m_fileRefList.addEventListener(Event.SELECT, SelectHandler);
            
            m_playingEffects = new Dictionary(true);
        }
        

        public function AddAttachment():void 
        {
            m_fileRefList.browse();
        }
        
        private function SelectHandler(event:Event):void
        {
            var file:FileReference;
            var selectedFileArray:Array = m_fileRefList.fileList;

            for (var i:uint = 0; i < selectedFileArray.length; i++) {
                file = FileReference(selectedFileArray[i]);
                AddItem(file);
            }
                
            CalculateTotalSize();
        }

        private function AddItem(file:FileReference):void
        {
            if (IsFileDuplicate(file.name))
                return;

            var itemView:AttachmentItemView = new AttachmentItemView();
            itemView.addEventListener("removeAttachment", RemoveItemHandler);

            var index:int = Items.length;
            Items[index] = itemView;
            addChild(itemView);
            layoutItems(index, true);

            itemView.Controller.uploadFile(file, UploadRequestURL);
        }
        
        public function get HasBusyItems():Boolean
        {
            for (var i:int = 0; i < Items.length; i++)
                if (Items[i].Controller.IsUploadInProgress)
                    return true;
       
            return false;
        }
                 
        public function GetItems():Array
        {
            var result:Array = [];
            
            for (var i:int = 0; i <Items.length; i++) 
                result.push(Items[i].Controller.Model);
            
            return result;
        }
        
        public function RemoveAllItems():void
        {
            Items.length = 0;
            removeAllChildren();
        }
        
        private function RemoveItemHandler(event:Event):void 
        {
            var item:AttachmentItemView = event.target as AttachmentItemView;
            var index:int = IndexOf(item.uid);
            Items.splice(index, 1);
            removeChild(item);
            layoutItems(index);
            
            CalculateTotalSize();
        }

        private function CalculateTotalSize():void
        {
            TotalSize = 0;
            
            for (var i:int = 0; i < Items.length; i++) 
                TotalSize += Items[i].Controller.Model.Size;
                
        }
                 
        private function layoutItems(startIndex:int, scrollToBottom:Boolean=false):void 
        {
            var n:int = Items.length;
            var e:Move;
            
            for (var i:int = startIndex; i < n ; i++) 
            {
                var item:AttachmentItemView = Items[i];
                var yTo:Number = i * (item.height);
                
                if (m_playingEffects[item] == null) 
                {
                    e = new Move(item);
                    if (item.x == 0 && item.y == 0)
                    {
                        e.xFrom = NewItemStartX;
                        e.yFrom = NewItemStartY;
                    }
    
                    e.xTo = 0;
                    e.yTo = yTo;
                    m_playingEffects[item] = e;
                    
                    e.addEventListener(EffectEvent.EFFECT_END, 
                    
                        function(event:Event):void 
                        {
                           delete m_playingEffects[item];
                        }
                    );
                    
                    e.play();
                } 
                else 
                {
                    m_playingEffects[item].pause();
                    m_playingEffects[item].yTo = yTo;
                    m_playingEffects[item].play();
                }
            }
            
            if (scrollToBottom)
            {
                e.addEventListener(EffectEvent.EFFECT_END, 
                    function(event:Event):void 
                    {
                        validateNow();
                        verticalScrollPosition = maxVerticalScrollPosition;    
                    }
                );
            }
        }
        
        private function IsFileDuplicate(fileName:String):Boolean
        {
            var result:Boolean = false;
            
            for (var i:int = 0; i < Items.length; i++){
                if (Items[i].Controller.Model.Name == fileName)
                {
                    result = true;
                    break;
                }
            }
            
            return result;
        }
        
        private function IndexOf(uid:String):int 
        {
            var index:int = -1;
            
            for (var i:int = 0; i < Items.length; i++){
                if (Items[i].uid == uid)
                {
                    index = i;
                    break;
                }
            }

            return index;
        }     

        private const NewItemStartX:int = -100;
        private const NewItemStartY:int = -100;
        
    }
}