package
{

    import mx.collections.ArrayCollection;
    import App.Domain.*;
    import weborb.data.ActiveCollection;
    
    [Bindable]
    public class ExpenseModel
    {

        public const DIARY_VIEW: String = "diary_view";

		public var caption_text:String;
		
		public var bills:ActiveCollection;
		
    }

}
