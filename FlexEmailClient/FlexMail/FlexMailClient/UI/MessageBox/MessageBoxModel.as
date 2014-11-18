package UI.MessageBox
{
    import mx.collections.ArrayCollection;
    import Domain.Message;
    import UI.MessageBox.MessageTree.MessageTreeCollection;
    import mx.collections.SortField;
    import mx.collections.Sort;
    
    [Bindable]
    public class MessageBoxModel
    {
        public var Messages:MessageTreeCollection;
        
        public var CurrentMessage:Message;
        
        public function Reset():void 
        {
            CurrentMessage = null;
            ResetMessageCollection();
        }
        
        private function ResetMessageCollection():void
        {
            Messages = new MessageTreeCollection();

            //Default sorting by Message Sent Date. In desc order.
            var sf:SortField = new SortField("messageSentSortValue", false, true, true);
            Messages.sort = new Sort();
            Messages.sort.fields = [sf];
        }
    }
}