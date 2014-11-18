package com.llsvc.startup.desktop
{
import com.llsvc.domain.Module;
import com.llsvc.domain.User;
import com.llsvc.module.ModuleImpl;
import com.llsvc.module.ModuleImplLoader;
import com.llsvc.startup.data.DesktopPackage;

import flash.events.TimerEvent;
import flash.utils.Timer;

import mx.collections.ArrayCollection;
import mx.events.ModuleEvent;
import mx.modules.ModuleManager;
import mx.rpc.Responder;
import mx.rpc.events.FaultEvent;
import mx.rpc.events.ResultEvent;
import mx.managers.CursorManager;

[Bindable]
public class DesktopModel
{
    public static const PING_DELAY:int = 10000;

    public var dPackage:DesktopPackage;

    public var menuItems:ArrayCollection = new ArrayCollection();
    public var menuData:ArrayCollection = new ArrayCollection([
        {label:"Start", children:menuItems}
    ]);
    
    public var currentUser:User;
    public var modules:ArrayCollection;
    
    // Hash ModuleId -> Loader
    private var runningModules:Object = new Object();
    private var runningMode:String = ModuleImpl.MODE_OFFLINE;
    private var pingTimer:Timer = null;
    
    public function init(dashPack:DesktopPackage):void 
    {
        menuItems.removeAll();
        dPackage = dashPack;
        
        if (dashPack != null) {
            currentUser = dashPack.user;
            modules = dashPack.moduleList;
            
            for each (var module:Module in modules) {
                trace("DashboardModel.init: module: " + module.id.toString() + " " + module.description);
                var item:Object = new Object();
                menuItems.addItem(item);
                item.label = module.description;
                item.module = module;
            }
            
        }
        
        // start pingTimer
/*        
        pingTimer = new Timer(DesktopModel.PING_DELAY, 0);
        pingTimer.addEventListener(TimerEvent.TIMER, pingTimerOnTimerHandler);
        pingTimer.start();
*/        
    }
    
    public function getRunningModule(module:Module) : ModuleImplLoader
    {
        var key:String = module.id.toString();
        var loader:ModuleImplLoader = runningModules[key] as ModuleImplLoader;
        return loader;
    }
    
    public function startModule(module:Module) : ModuleImplLoader
    {
        trace("DashboardModel.startModule: starting module " + module.id.toString() + " ...");            
//        CursorManager.setBusyCursor();
        var loader:ModuleImplLoader = new ModuleImplLoader();
//        loader.addEventListener(ModuleEvent.READY, moduleLoaderModuleLoadResultHandler);
//        loader.addEventListener(ModuleEvent.ERROR, moduleLoaderModuleLoadErrorHandler);
        loader.moduleInfo = module;
        loader.load();
        loader.loadModule();
        return loader;
    }

    public function startModule2(module:Module, responder:Responder):void
    {
        trace("DashboardModel.startModule: starting module " + module.id.toString() + " ...");            
        CursorManager.setBusyCursor();
        var loader:ModuleImplLoader = new ModuleImplLoader();
        loader.addEventListener(ModuleEvent.ERROR, moduleLoaderModuleLoadErrorHandler);
        loader.addEventListener(ModuleEvent.READY, moduleLoaderModuleLoadResultHandler);
		loader.id = module.id.toString();
        loader.data = responder;
        loader.moduleInfo = module;
        loader.load();
//        loader.addEventListener("ready", moduleLoaderModuleLoadResultHandler);
		
        loader.loadModule();
    }
    
    public function stopModule(module:Module) : void
    {
        var key:String = module.id.toString();
        var loader:ModuleImplLoader = getRunningModule(module);
        unloadModule(loader);
        runningModules[key] = null;
    }

    public function shutdown() : void
    {
        trace("DashboardModel.shutdown: called.");          
        for(var key:String in runningModules) {
            var loader:ModuleImplLoader = runningModules[key] as ModuleImplLoader;
            trace("DashboardModel.shutdown: loader=" + loader);         
            if ( null != loader ) {
                unloadModule(loader);
            }
            runningModules[key] = null;
            
        }
    }

    private function unloadModule(loader:ModuleImplLoader): void
    {
        var mod:ModuleImpl = loader.child as ModuleImpl;
        mod.logout();
        loader.unloadModule();
    }
/*    
    private function moduleLoaderModuleLoadSetupHandler(event:ModuleEvent):void 
    {
        trace("DesktopModel.moduleLoaderModuleLoadSetupHandler: event=" + event);           
        
        var loader:ModuleImplLoader = event.target as ModuleImplLoader;
        trace("DesktopModel.moduleLoaderModuleLoadSetupHandler: loader=" + loader);
        
        loader.addEventListener(ModuleEvent.READY, moduleLoaderModuleLoadResultHandler);
        loader.loadModule();
    }
*/
    private function moduleLoaderModuleLoadResultHandler(event:ModuleEvent):void 
    {
        trace("DashboardModel.tractModuleLoaderModuleLoadResultHandler: event=" + event);           
        CursorManager.removeBusyCursor();
        
        var loader:ModuleImplLoader = event.target as ModuleImplLoader;
        trace("DashboardModel.tractModuleLoaderModuleLoadResultHandler: loader=" + loader);         
        
        var mod:ModuleImpl = loader.child as ModuleImpl;
        trace("DashboardModel.tractModuleLoaderModuleLoadResultHandler: binary module=" + mod);         
        
        mod.init(currentUser, loader.moduleInfo);
        trace("DashboardModel.tractModuleLoaderModuleLoadResultHandler: binary module init done.");         
        
        var key:String = loader.moduleInfo.id.toString();
        runningModules[key] = loader;
        trace("DashboardModel.tractModuleLoaderModuleLoadResultHandler: marked as running.");           
        
        var responder:Responder = loader.data as Responder;
        var re:ResultEvent = new ResultEvent(ResultEvent.RESULT, false, true, loader);
        responder.result(re);
    }
    
    private function moduleLoaderModuleLoadErrorHandler(event:ModuleEvent) : void 
    {
        trace("DashboardModel.tractModuleLoaderModuleLoadErrorHandler: module loading is failed. " + event.toString());         
        CursorManager.removeBusyCursor();
        var loader:ModuleImplLoader = event.target as ModuleImplLoader;
        trace("DashboardModel.tractModuleLoaderModuleLoadFaultHandler: loader=" + loader);          
        var responder:Responder = loader.data as Responder;
        responder.fault("Cannot load module.");
    }
    
    private function pingTimerOnTimerHandler(event:TimerEvent) : void 
    {
        trace("DashboardModel.pingTimerOnTimerHandler: event=" + event.toString());         
        var responder:Responder = new Responder(pingResultHandler, pingFaultHandler);
/*        
        var storage:ITractStorage = RemoteStorage.instance;
        storage.ping(responder);
*/        
    }

    private function notifyRunningModules() : void {
        for(var key:String in runningModules) {
            var loader:ModuleImplLoader = runningModules[key] as ModuleImplLoader;
            trace("DashboardModel.notifyRunningModules: loader=" + loader);         
        
            if ( null != loader ) {
                var mod:ModuleImpl = loader.child as ModuleImpl;
                trace("DashboardModel.notifyRunningModules: binary module=" + mod);         
        
                mod.modeChanged(runningMode);
            }
        }            
    }   
        
    private function pingResultHandler(event:ResultEvent) : void 
    {
        //trace("DashboardModel.pingResultHandler: event=" + event.toString());         
        if ( runningMode == ModuleImpl.MODE_OFFLINE ) {
            runningMode = ModuleImpl.MODE_ONLINE;
            notifyRunningModules();
        }
    }
    
    private function pingFaultHandler(event:FaultEvent) : void 
    {
        //trace("DashboardModel.pingFaultHandler: event=" + event.toString());          
        if ( runningMode == ModuleImpl.MODE_ONLINE ) {
            runningMode = ModuleImpl.MODE_OFFLINE;
            notifyRunningModules();
        }
    }
    
}
}
