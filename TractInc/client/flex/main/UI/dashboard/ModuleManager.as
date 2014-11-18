package UI.dashboard
{
    
import mx.controls.Button;
import TractInc.modules.TractModuleLoader;

public class ModuleManager
{
    private static var modules:Object = new Object();
    
    private var tbButton:Button = null;
    private var moduleLoader:TractModuleLoader = null;
    private var moduleWindow:ModuleWrapper = null;
    
    public static function registerModule(moduleId:String, 
            button:Button, loader:TractModuleLoader, window:ModuleWrapper):void
    {
        modules[moduleId] = new ModuleManager(button, loader, window);
    }
    
    public static function unregisterModule(moduleId:String):void
    {
        modules[moduleId] = null;
    }
    
    public static function getModule(moduleId:String):ModuleManager
    {
        return modules[moduleId] as ModuleManager;
    }
    
    public static function highlightModule(moduleId:String):void
    {
/**/        
        for(var key:String in modules) {
            if ( null != modules[key] ) {
                var mod:ModuleManager = modules[key] as ModuleManager;
                if ( moduleId == key ) {
                    mod.getTaskbarButton().selected = true;
                } else {
                    mod.getTaskbarButton().selected = false;
                }
            }
        }            
/**/        
    }
    
    public static function getModules():Array
    {
        var result:Array = new Array();
        for(var key:String in modules) {
            if ( null != modules[key] ) {
                result.push(modules[key]);
            }
        }            
        return result;
    }
    
    public function ModuleManager(button:Button, 
            loader:TractModuleLoader, window:ModuleWrapper) : void
    {
        tbButton = button;
        moduleLoader = loader;
        moduleWindow = window;
    }
    
    public function getTaskbarButton():Button
    {
        return tbButton;
    }
    
    public function getLoader():TractModuleLoader
    {
        return moduleLoader;
    }
    
    public function getWindow():ModuleWrapper
    {
        return moduleWindow;
    }
    
}
	
}