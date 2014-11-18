package truetract.web    
{
import mx.collections.ArrayCollection;

import truetract.domain.Tract;
import truetract.domain.User;
import truetract.domain.Module;

[Bindable]
public class AppModel
{
    public static const WORKFLOW_STATE_LOGOUT:int = 0;
    public static const WORKFLOW_STATE_LOGIN:int = 1;
    public static const WORKFLOW_STATE_CLIENT:int = 2;

    private static var _instance : AppModel;
    
    public static function getInstance() : AppModel
    {
        if ( _instance == null )
            _instance = new AppModel( arguments.callee );
            
        return _instance;
    }

    public function AppModel( caller : Function = null ) 
    {
        if(caller != AppModel.getInstance)
        {
            throw new Error ("AppModel is a singleton class, use getInstance() instead");
        }
        
        if (AppModel._instance != null)
        {
            throw new Error( "Only one AppModel instance should be instantiated" ); 
        }
    }

    public var user:User;

 	public var userModuleList:ArrayCollection; //temporary
    public var isScopeMappingAllowed:Boolean = false; //temporary

    public var workflowState:Number;

    public function reset():void 
    {
        user              = null;
 		userModuleList    = null;
        workflowState     = 0;
    }
}
}