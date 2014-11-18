package com.llsvc.startup.desktop
{
import mx.utils.Base64Encoder;
import mx.events.MenuEvent;
import mx.rpc.Responder;
import mx.rpc.events.ResultEvent;
import mx.controls.Alert;
import mx.rpc.events.FaultEvent;
import mx.events.ModuleEvent;
import flash.events.Event;
import mx.modules.ModuleManager;
import flash.events.EventDispatcher;
import flash.events.MouseEvent;
import mx.collections.ArrayCollection;
import mx.controls.LinkButton;
import mx.controls.Button;
import mx.managers.PopUpManager;
//import sjd.containers.ResizeWindow;
import mx.effects.Resize;
import flash.net.URLRequest;

import com.llsvc.domain.Module;
import com.llsvc.domain.User;
import com.llsvc.module.IModule;
import com.llsvc.module.ModuleImpl;
import com.llsvc.module.ModuleImplLoader;
import com.llsvc.startup.data.DesktopPackage;
import com.llsvc.startup.events.LogoutEvent;
import com.llsvc.startup.MainModel;
import com.llsvc.framework.storage.IStorage;
import com.llsvc.framework.storage.Storage;

[Bindable]
public class DesktopController 
    extends EventDispatcher
{
    public var view:DesktopView;
    public var model:DesktopModel = new DesktopModel();
    
    public function init(user:User):void 
    {
        if (model.modules) {
            model.modules.removeAll();
        }
        loadModel(user);
    }
    
    public function btnLogout_clickHandler():void 
    {
        var canLogout:Boolean = true;
        
//        view.runningModulesBar.removeAllChildren();
        
        view.vsDynamicModules.removeAllChildren();
/*          
        for each(var mm:UI.dashboard.ModuleManager in UI.dashboard.ModuleManager.getModules()) {
            PopUpManager.removePopUp(mm.getWindow());
        }
*/          
        model.shutdown();
        
        if (canLogout) {
            view.dispatchEvent(new LogoutEvent(LogoutEvent.LOGOUT_EVENT));
        }
    }

    private function loadModel(user:User):void 
    {
        model.currentUser = user;
        var responder:Responder = new Responder(loadModel_onResultHandler, loadModel_onFaultHandler);
        Storage.instance.getPackage(user, responder);
    }
    
    private function encodeSession(user:Object):String {
        var be:Base64Encoder = new Base64Encoder();
        var now:Date = new Date();
        be.encode(user.UserId + "-" + user.Password + "-" + now.toString());
        var encodedData:String = be.flush();
        return encodedData;         
    }
    
    private function loadModel_onResultHandler(event:ResultEvent):void 
    {
        trace("DashboradController.loadModel_onResultHandler: called 1.");
        var dashPack:DesktopPackage = event.result as DesktopPackage;
        var user:User = dashPack.user;
//      Alert.show("--->1");        
//        Alert.show("DesktopControoler.loadModel_onResultHandler: module.count=" + dashPack.moduleList.length);
        for each (var module:Module in dashPack.moduleList) {
            // SubstituentModule
//            Alert.show("DesktopControoler.loadModel_onResultHandler: module.moduleTypeId=" + module.moduleTypeId);
            if ( 2 == module.moduleTypeId ) {
                var sessionId:String = encodeSession(user);
//                  var ur:URLRequest = new URLRequest(module.url + "?debug=true&sessionId=" + sessionId);
                var ur:URLRequest = new URLRequest(module.url + "?sessionId=" + sessionId);
                flash.net.navigateToURL(ur, "_top");
                return;
            }
            // to do: fix it, preload for flex sdk 3
            model.startModule(module);
        }
        trace("DashboradController.loadModel_onResultHandler: init dashboard.");
        model.init(dashPack);
        view.taskBar.visible = true;
    }
    
    private function loadModel_onFaultHandler(event:FaultEvent):void 
    {
        Alert.show(event.fault.message);

    }
    
    public function runModule(event:MenuEvent):void 
    {
    	//-------------------------
    	view.generalMap.visible = !view.generalMap.visible;
    	view.vsStaticModules.visible = !view.vsStaticModules.visible;
    	return;
    	//-------------------------
    	
    	view.generalMap.visible = false;
        var item:Object = event.item;
        var module:Module = item.module as Module;
//            var window:ResizeWindow = null;
        var window:ModuleWrapper = null;
        trace("DashboardController.runModule: moduleId=" + module.id.toString());
        
        if ( 3 == module.moduleTypeId ) {
            var sessionId:String = encodeSession(model.currentUser);
            var ur:URLRequest = new URLRequest(module.url + "?debug=true&sessionId=" + sessionId);
//              var ur:URLRequest = new URLRequest(module.Url + "?sessionId=" + sessionId);
            flash.net.navigateToURL(ur, "_blank");
        } else {
        
        var loader:ModuleImplLoader = model.getRunningModule(module);
        if ( null == loader ) {
            var responder:Responder = new Responder(startModuleOnResultHandler, 
                    startModuleOnFaultHandler);
            model.startModule2(module, responder);
/*
            loader = model.startModule(module);
            var lbn:Button = new Button();
            view.runningModulesBar.addChild(lbn);
            lbn.label = module.Description;
            lbn.data = module.ModuleId.toString();
            lbn.addEventListener(MouseEvent.CLICK, switchModuleEventHandler);

//                window = ResizeWindow(PopUpManager.createPopUp(view.desktop, ResizeWindow, false));
            window = new ModuleWrapper();
            window.init(loader, this);
            window.data = module.ModuleId.toString();
            view.vsDynamicModules.addChild(window);
*/                
/*                
            window.addChild(loader);
            window.showWindowButtons = true;
            window.addEventListener("closeWindow", closeModuleEventHandler);
            window.addEventListener("maxWindow", maximizeModuleEventHandler);
            window.addEventListener("minWindow", minimizeModuleEventHandler);
            window.setStyle("hideEffect", view.minimizeEffect);
            window.setStyle("showEffect", view.appearEffect);
            window.x = 100;
            window.y = 100;
            //PopUpManager.centerPopUp(window);
*/                        
/*                
            UI.dashboard.ModuleManager.registerModule(module.ModuleId.toString(),
                    lbn, loader, window);
            view.vsDynamicModules.selectedChild = window;
            UI.dashboard.ModuleManager.highlightModule(module.ModuleId.toString());
*/                
        } else {
            var moduleId:String = module.id.toString();
            trace("DashboardController.runModule: moduleId=" + moduleId);           
        
            window = com.llsvc.startup.desktop.ModuleManager.getModule(moduleId).getWindow();
            view.vsDynamicModules.selectedChild = window;
            com.llsvc.startup.desktop.ModuleManager.highlightModule(module.id.toString());
/*                
            window.visible = true;
            PopUpManager.bringToFront(window);
*/                
        }
        }
        
    }
    
    private function startModuleOnResultHandler(event:ResultEvent):void 
    {
        var loader:ModuleImplLoader = event.result as ModuleImplLoader;
        var module:Module = loader.moduleInfo as Module;
        
        var lbn:Button = new Button();
//        view.runningModulesBar.addChild(lbn);
        lbn.label = module.description;
        lbn.data = module.id.toString();
        lbn.addEventListener(MouseEvent.CLICK, switchModuleEventHandler);

        var window:ModuleWrapper = new ModuleWrapper();
        window.init(loader, this);
        window.data = module.id.toString();
        view.vsDynamicModules.addChild(window);
        
        com.llsvc.startup.desktop.ModuleManager.registerModule(module.id.toString(),
                lbn, loader, window);
        view.vsDynamicModules.selectedChild = window;
        com.llsvc.startup.desktop.ModuleManager.highlightModule(module.id.toString());
    }
    
    private function startModuleOnFaultHandler(event:FaultEvent):void 
    {
        Alert.show(event.fault.message);
    }
    
/*      
    public function stopModule(dbm:DashboardModule) : void
    {
        trace("DashboardController.stopModule: called for dashboard module=" + dbm.name);           
        var loader:TractModuleLoader = dbm.getTractLoader();
        model.stopModule(loader.moduleInfo);
        view.runningModulesBar.removeChild(dbm.getCaptionTab());
        view.vsDynamicModules.removeChild(dbm);
    }
    private function resolveDashboradModule(loader:TractModuleLoader) : DashboardModule
    {
        var result:DashboardModule = view.vsDynamicModules.getChildByName(
                loader.moduleInfo.ModuleId.toString()) as DashboardModule;
        return result;
    }
*/      
    
    private function switchModuleEventHandler(event:Event):void 
    {
        var button:Button = event.target as Button;
        trace("DashboardController.switchModule: button=" + button);            
        
        var moduleId:String = button.data as String;
        trace("DashboardController.switchModule: moduleId=" + moduleId);            

        var window:ModuleWrapper 
                = com.llsvc.startup.desktop.ModuleManager.getModule(moduleId).getWindow();
        view.vsDynamicModules.selectedChild = window;
        com.llsvc.startup.desktop.ModuleManager.highlightModule(moduleId);
/*                    
        window.visible = true;
        PopUpManager.bringToFront(window);
*/            
    }

    public function closeModule(wrapper:ModuleWrapper):void 
    {
        var moduleId:String = wrapper.data as String;
        var windowModule:com.llsvc.startup.desktop.ModuleManager = 
                com.llsvc.startup.desktop.ModuleManager.getModule(moduleId);
        
        model.stopModule(windowModule.getLoader().moduleInfo as Module);
//        view.runningModulesBar.removeChild(windowModule.getTaskbarButton());
        view.vsDynamicModules.removeChild(wrapper);
        com.llsvc.startup.desktop.ModuleManager.unregisterModule(moduleId);
        this.view.generalMap.visible = (0 == view.vsDynamicModules.getChildren().length);
    }
/*      
    private function closeModuleEventHandler(event:Event):void 
    {
        var window:ResizeWindow = event.currentTarget as ResizeWindow;
        var moduleId:String = window.data as String;
        var windowModule:UI.dashboard.ModuleManager = 
                UI.dashboard.ModuleManager.getModule(moduleId);
        
        model.stopModule(windowModule.getLoader().moduleInfo);
        view.runningModulesBar.removeChild(windowModule.getTaskbarButton());
        PopUpManager.removePopUp(window);
        UI.dashboard.ModuleManager.unregisterModule(moduleId);
    }
*/
/*
    private function maximizeModuleEventHandler(event:Event):void 
    {
        var window:ResizeWindow = event.currentTarget as ResizeWindow;
        if ( window.width != view.desktop.width || window.height != view.desktop.height ) {
            window.x = 0;
            window.y = view.desktop.y;
            window.width = view.desktop.width;
            window.height = view.desktop.height;
        }
    }

    private function minimizeModuleEventHandler(event:Event):void 
    {
        var window:ResizeWindow = event.currentTarget as ResizeWindow;
        window.visible = false;
    }
*/
}
}
