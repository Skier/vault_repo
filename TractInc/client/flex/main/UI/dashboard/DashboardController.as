package UI.dashboard
{
    import mx.utils.Base64Encoder;
    import mx.events.MenuEvent;
	import TractInc.Domain.User;
	import mx.rpc.Responder;
	import UI.AppModel;
	import mx.rpc.events.ResultEvent;
	import TractInc.Domain.packages.DashboardPackage;
	import mx.controls.Alert;
	import mx.rpc.events.FaultEvent;
	import TractInc.modules.TractModuleLoader;
	import mx.events.ModuleEvent;
	import flash.events.Event;
	import TractInc.modules.ITractModuleInfo;
	import TractInc.Domain.storage.ITractStorage;
	import TractInc.Domain.Module;
	import mx.modules.ModuleManager;
	import TractInc.modules.TractModule;
	import flash.events.EventDispatcher;
	import flash.events.MouseEvent;
	import common.events.LogoutEvent;
	import mx.collections.ArrayCollection;
	import mx.controls.LinkButton;
	import mx.controls.Button;
    import mx.managers.PopUpManager;
    import sjd.containers.ResizeWindow;
    import mx.effects.Resize;
    import flash.net.URLRequest;
    
	[Bindable]
	public class DashboardController extends EventDispatcher
	{
		public var view:DashboardView;
		public var model:DashboardModel = new DashboardModel();
		
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
			
			view.runningModulesBar.removeAllChildren();
			
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
			var storage:ITractStorage = AppModel.storage;
			storage.getDashboardPackage(user, responder);
		}
		
		private function encodeSession(user:User):String {
            var be:Base64Encoder = new Base64Encoder();
            var now:Date = new Date();
            be.encode(user.UserId + "-" + user.Password + "-" + now.toString());
            var encodedData:String = be.flush();
            return encodedData;		    
		}
		
		private function loadModel_onResultHandler(event:ResultEvent):void 
		{
			trace("DashboradController.loadModel_onResultHandler: called 1.");
			var dashPack:DashboardPackage = event.result as DashboardPackage;
			var user:User = dashPack.user;
		    for each (var module:Module in dashPack.ModuleList) {
		        // SubstituentModule
		        if ( 2 == module.ModuleTypeId ) {
        		    var sessionId:String = encodeSession(user);
//        		    var ur:URLRequest = new URLRequest(module.Url + "?debug=true&sessionId=" + sessionId);
        		    var ur:URLRequest = new URLRequest(module.Url + "?sessionId=" + sessionId);
        		    flash.net.navigateToURL(ur, "_top");
        		    return;
		        }
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
            var item:Object = event.item;
            var module:Module = item.module as Module;
//            var window:ResizeWindow = null;
            var window:ModuleWrapper = null;
            trace("DashboardController.runModule: moduleId=" + module.ModuleId.toString());
            
	        if ( 3 == module.ModuleTypeId ) {
    		    var sessionId:String = encodeSession(model.currentUser);
    		    var ur:URLRequest = new URLRequest(module.Url + "?debug=true&sessionId=" + sessionId);
//    		    var ur:URLRequest = new URLRequest(module.Url + "?sessionId=" + sessionId);
    		    flash.net.navigateToURL(ur, "_blank");
	        } else {
            
            var loader:TractModuleLoader = model.getRunningModule(module);
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
                var moduleId:String = module.ModuleId.toString();
                trace("DashboardController.runModule: moduleId=" + moduleId);			
            
                window = UI.dashboard.ModuleManager.getModule(moduleId).getWindow();
                view.vsDynamicModules.selectedChild = window;
                UI.dashboard.ModuleManager.highlightModule(module.ModuleId.toString());
/*                
		        window.visible = true;
                PopUpManager.bringToFront(window);
*/                
            }
            }
            
        }
        
		private function startModuleOnResultHandler(event:ResultEvent):void 
		{
            var loader:TractModuleLoader = event.result as TractModuleLoader;
            var module:Module = loader.moduleInfo;
            
			var lbn:Button = new Button();
			view.runningModulesBar.addChild(lbn);
			lbn.label = module.Description;
			lbn.data = module.ModuleId.toString();
			lbn.addEventListener(MouseEvent.CLICK, switchModuleEventHandler);

            var window:ModuleWrapper = new ModuleWrapper();
            window.init(loader, this);
            window.data = module.ModuleId.toString();
            view.vsDynamicModules.addChild(window);
            
            UI.dashboard.ModuleManager.registerModule(module.ModuleId.toString(),
                    lbn, loader, window);
            view.vsDynamicModules.selectedChild = window;
            UI.dashboard.ModuleManager.highlightModule(module.ModuleId.toString());
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
                    = UI.dashboard.ModuleManager.getModule(moduleId).getWindow();
            view.vsDynamicModules.selectedChild = window;
            UI.dashboard.ModuleManager.highlightModule(moduleId);
/*                    
		    window.visible = true;
            PopUpManager.bringToFront(window);
*/            
		}
	
		public function closeModule(wrapper:ModuleWrapper):void 
		{
		    var moduleId:String = wrapper.data as String;
		    var windowModule:UI.dashboard.ModuleManager = 
		            UI.dashboard.ModuleManager.getModule(moduleId);
            
            model.stopModule(windowModule.getLoader().moduleInfo);
            view.runningModulesBar.removeChild(windowModule.getTaskbarButton());
            view.vsDynamicModules.removeChild(wrapper);
            UI.dashboard.ModuleManager.unregisterModule(moduleId);
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