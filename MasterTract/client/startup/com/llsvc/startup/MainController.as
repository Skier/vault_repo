package com.llsvc.startup
{
import mx.controls.Alert;
import com.llsvc.framework.util.ExceptionHelper;
import com.llsvc.startup.events.LoginCompleteEvent;
import com.llsvc.startup.events.LoginFaultEvent;
import com.llsvc.startup.events.LogoutEvent;
//    import TractInc.Domain.storage.RemoteStorage;

[Bindable]
public class MainController
{
    public var view:MainView;
    public var model:MainModel = new MainModel();
    
    public function MainController() {}
    
    public function onLoginCompleteHandler(event:LoginCompleteEvent):void 
    {
        model.user = event.user;
        setAppWorkflowState(MainModel.VIEW_DESKTOP);
    }
    
    public function onLoginFailedHandler(event:LoginFaultEvent):void 
    {
        Alert.show(ExceptionHelper.parseFaultString(event.fault.faultString));
    }

    public function onLogoutHandler(event:LogoutEvent):void 
    {
        model.user = null;
        setAppWorkflowState(MainModel.VIEW_LOGIN);
    }
    
    public function setAppWorkflowState(state:int): void {
        switch (state) {
            
            case MainModel.VIEW_LOGIN:
                view.mainViewStack.selectedChild = view.viewLogin;
                break;
            
            case MainModel.VIEW_DESKTOP:
                view.mainViewStack.selectedChild = view.viewDesktop;
                view.viewDesktop.init(model.user);
                break;
            
            default:
                throw new Error("Workflow state is invalid");
        }
    }

}

}
