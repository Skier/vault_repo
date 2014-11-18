package truetract.web
{
	import flash.events.EventDispatcher;

	public class AppEventDispatcher extends EventDispatcher
	{

	    //--------------------------------------------------------------------------
	    //
	    //  Singleton stuff
	    //
	    //--------------------------------------------------------------------------
	    
	    private static var _instance : AppEventDispatcher;
	    public static function get instance() : AppEventDispatcher
	    {
	        if ( _instance == null )
	            _instance = new AppEventDispatcher(new SingletonEnforcer);
	            
	        return _instance;
	    }
	
	    public function AppEventDispatcher(singletonEnforcer:SingletonEnforcer) 
	    {
	    }

	}
}

class SingletonEnforcer {}