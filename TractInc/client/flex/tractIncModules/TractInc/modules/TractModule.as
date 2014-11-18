package TractInc.modules
{
	import TractInc.modules.ITractModuleInfo;
	import TractInc.Domain.*;
	import mx.modules.Module;

	public class TractModule extends Module implements ITractModuleInfo
	{
	    public static const MODE_ONLINE:String = "online";
	    public static const MODE_OFFLINE:String = "offline";
	    
		public function init(user:TractInc.Domain.User, module:TractInc.Domain.Module):void
		{
			throw new Error("not overriden yet!");
		}

        public function modeChanged(mode:String):void
        {
			// i'm not aware of mode changes
        }
        
		public function logout():Boolean
		{
			throw new Error("not overriden yet!");
		}

	}
}