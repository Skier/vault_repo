package
{
	import Domain.Employee;
	public class AppController
	{
		private const LOGIN_STATE:int = 0;
		private const MAIN_DASH:int = 1;
		
		[Bindable]
        public var WorkflowState:Number = LOGIN_STATE;
        [Bindable]
        public var CurrentEmployee:Employee;
        
        public function showMainDash():void{
        	WorkflowState = MAIN_DASH;
        }
	}
}