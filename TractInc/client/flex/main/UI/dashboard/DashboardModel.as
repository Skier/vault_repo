package UI.dashboard
{
	import flash.utils.Timer;
	import flash.events.TimerEvent;
	
	import mx.rpc.Responder;
    import mx.rpc.events.FaultEvent;
    import mx.rpc.events.ResultEvent;
	import mx.collections.ArrayCollection;
	import mx.events.ModuleEvent;
    import mx.managers.CursorManager;

	import TractInc.Domain.User;
	import TractInc.Domain.Module;
	import TractInc.modules.TractModule;
	import TractInc.modules.TractModuleLoader;
	import TractInc.Domain.packages.DashboardPackage;
	import TractInc.Domain.storage.ITractStorage;
	import TractInc.Domain.storage.RemoteStorage;
	
	[Bindable]
	public class DashboardModel
	{
		public static const PING_DELAY:int = 10000;
		
		public var dashPackage:DashboardPackage;

        public var menuItems:ArrayCollection = new ArrayCollection();
        public var menuData:ArrayCollection = new ArrayCollection([
            {label:"Start", children:menuItems}
        ]);
		
		public var currentUser:User;
		public var modules:ArrayCollection;
        
        // Hash ModuleId -> Loader
        private var runningModules:Object = new Object();
        private var runningMode:String = TractModule.MODE_OFFLINE;
		private var pingTimer:Timer = null;
        
		public function init(dashPack:DashboardPackage):void 
		{
		    menuItems.removeAll();
			dashPackage = dashPack;
			
			if (dashPack != null) {
				currentUser = dashPack.user;
				modules = dashPack.Modules;
				
			    for each (var module:Module in modules) {
				    trace("DashboardModel.init: module: " + module.ModuleId.toString() + " " + module.Description);
                    var item:Object = new Object();
                    menuItems.addItem(item);
                    item.label = module.Description;
                    item.module = module;
                }
                
			}
			
            // start pingTimer
		    pingTimer = new Timer(DashboardModel.PING_DELAY, 0);
            pingTimer.addEventListener(TimerEvent.TIMER, pingTimerOnTimerHandler);
			pingTimer.start();
		}
		
		public function getRunningModule(module:Module) : TractModuleLoader
		{
		    var key:String = module.ModuleId.toString();
		    var loader:TractModuleLoader = runningModules[key] as TractModuleLoader;
		    return loader;
		}
		
		public function startModule(module:Module) : TractModuleLoader
		{
            trace("DashboardModel.startModule: starting module " + module.ModuleId.toString() + " ...");			
            CursorManager.setBusyCursor();
		    var loader:TractModuleLoader = new TractModuleLoader();
  			loader.addEventListener(ModuleEvent.READY, tractModuleLoaderModuleLoadResultHandler);
   			loader.addEventListener(ModuleEvent.ERROR, tractModuleLoaderModuleLoadErrorHandler);
			loader.moduleInfo = module;
            loader.load();
            loader.loadModule();
		    return loader;
		}
		
		public function startModule2(module:Module, responder:Responder):void
		{
            trace("DashboardModel.startModule: starting module " + module.ModuleId.toString() + " ...");			
            CursorManager.setBusyCursor();
		    var loader:TractModuleLoader = new TractModuleLoader();
  			loader.addEventListener(ModuleEvent.READY, tractModuleLoaderModuleLoadResultHandler);
   			loader.addEventListener(ModuleEvent.ERROR, tractModuleLoaderModuleLoadErrorHandler);
   			loader.data = responder;
			loader.moduleInfo = module;
            loader.load();
            loader.loadModule();
		}
		
		public function stopModule(module:Module) : void
		{
		    var key:String = module.ModuleId.toString();
		    var loader:TractModuleLoader = getRunningModule(module);
		    unloadModule(loader);
		    runningModules[key] = null;
		}

        public function shutdown() : void
        {
            trace("DashboardModel.shutdown: called.");			
            for(var key:String in runningModules) {
                var loader:TractModuleLoader = runningModules[key] as TractModuleLoader;
                trace("DashboardModel.shutdown: loader=" + loader);			
                if ( null != loader ) {
                    unloadModule(loader);
                }
		        runningModules[key] = null;
		        
            }
        }

        private function unloadModule(loader:TractModuleLoader): void
        {
            var mod:TractModule = loader.child as TractModule;
            mod.logout();
		    loader.unloadModule();
		}
		
		private function tractModuleLoaderModuleLoadResultHandler(event:ModuleEvent):void 
		{
            trace("DashboardModel.tractModuleLoaderModuleLoadResultHandler: event=" + event);			
            CursorManager.removeBusyCursor();
            
            var loader:TractModuleLoader = event.target as TractModuleLoader;
            trace("DashboardModel.tractModuleLoaderModuleLoadResultHandler: loader=" + loader);			
            
            var mod:TractModule = loader.child as TractModule;
            trace("DashboardModel.tractModuleLoaderModuleLoadResultHandler: binary module=" + mod);			
            
            mod.init(currentUser, loader.moduleInfo);
            trace("DashboardModel.tractModuleLoaderModuleLoadResultHandler: binary module init done.");			
            
		    var key:String = loader.moduleInfo.ModuleId.toString();
	        runningModules[key] = loader;
            trace("DashboardModel.tractModuleLoaderModuleLoadResultHandler: marked as running.");			
            
            var responder:Responder = loader.data as Responder;
            var re:ResultEvent = new ResultEvent(ResultEvent.RESULT, false, true, loader);
            responder.result(re);
		}
		
		private function tractModuleLoaderModuleLoadErrorHandler(event:ModuleEvent) : void 
		{
            trace("DashboardModel.tractModuleLoaderModuleLoadErrorHandler: module loading is failed. " + event.toString());			
            CursorManager.removeBusyCursor();
            var loader:TractModuleLoader = event.target as TractModuleLoader;
            trace("DashboardModel.tractModuleLoaderModuleLoadFaultHandler: loader=" + loader);			
            var responder:Responder = loader.data as Responder;
            responder.fault("Cannot load module.");
		}
		
		private function pingTimerOnTimerHandler(event:TimerEvent) : void 
		{
            trace("DashboardModel.pingTimerOnTimerHandler: event=" + event.toString());			
			var responder:Responder = new Responder(pingResultHandler, pingFaultHandler);
			var storage:ITractStorage = RemoteStorage.instance;
			storage.ping(responder);
		}

        private function notifyRunningModules() : void {
            for(var key:String in runningModules) {
                var loader:TractModuleLoader = runningModules[key] as TractModuleLoader;
                trace("DashboardModel.notifyRunningModules: loader=" + loader);			
            
                if ( null != loader ) {
                    var mod:TractModule = loader.child as TractModule;
                    trace("DashboardModel.notifyRunningModules: binary module=" + mod);			
            
                    mod.modeChanged(runningMode);
                }
            }            
        }	
        	
		private function pingResultHandler(event:ResultEvent) : void 
		{
            //trace("DashboardModel.pingResultHandler: event=" + event.toString());			
            if ( runningMode == TractModule.MODE_OFFLINE ) {
                runningMode = TractModule.MODE_ONLINE;
                notifyRunningModules();
            }
		}
		
		private function pingFaultHandler(event:FaultEvent) : void 
		{
            //trace("DashboardModel.pingFaultHandler: event=" + event.toString());			
            if ( runningMode == TractModule.MODE_ONLINE ) {
                runningMode = TractModule.MODE_OFFLINE;
                notifyRunningModules();
            }
		}
		
	}
}