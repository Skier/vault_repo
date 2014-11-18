package TractInc.modules
{
	import TractInc.Domain.User;
	import TractInc.Domain.Module;

	public interface ITractModuleInfo
	{
		function init(user:User, module:Module):void;
		function modeChanged(mode:String):void;
		function logout():Boolean;
	}
}
