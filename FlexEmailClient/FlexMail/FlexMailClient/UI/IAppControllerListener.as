package UI
{
    import mx.collections.ArrayCollection;
    
    public interface IAppControllerListener
    {
        function OnNewMessagesReceipt(messages:Array):void;
        function OnMessagesDeleted(messages:Array):void;
        function OnReset():void;
    }
}