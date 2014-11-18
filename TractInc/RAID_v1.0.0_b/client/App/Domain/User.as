package App.Domain
{
	import App.Domain.Codegen.*;
	
	[Bindable]
	[RemoteClass(alias="TractInc.Expense.Domain.User")]
	public dynamic class User extends _User
	{
		
		public static const DEFAULT_HACKING_ATTEMPTS:int = 5;
		
	}
}
    