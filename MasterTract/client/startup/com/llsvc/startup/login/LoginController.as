package com.llsvc.startup.login
{
import flash.events.EventDispatcher;

import mx.controls.Alert;

import mx.rpc.Responder;
import mx.rpc.events.FaultEvent;
import mx.rpc.events.InvokeEvent;
import mx.rpc.events.ResultEvent;
import mx.rpc.remoting.mxml.RemoteObject;

import com.llsvc.startup.events.LoginCompleteEvent;
import com.llsvc.startup.events.LoginFaultEvent;

import com.llsvc.domain.Person;
import com.llsvc.startup.MainController;
import com.llsvc.framework.storage.IStorage;
import com.llsvc.framework.storage.Storage;

[Bindable]
public class LoginController extends EventDispatcher
{
    public var serviceIsBusy:Boolean = false;
    public var view:LoginView = null;
    public var registerView:RegisterView = null;
    
    public function LoginController(view:LoginView):void 
    {
        this.view = view;
    }

    public function autoLogin(userName:String, userPassword:String):void 
    {
        view.username.text = userName;
        view.password.text = userPassword;
        doLogin();          
    }

    public function doLogin():void 
    {
/*    	
        if (!view.loginFormValidator.validate(true))
            return;
*/
        var userLogin:String = view.username.text;
        var userPassword:String = view.password.text;

        var responder:Responder = new Responder(login_onResultHandler, login_onFaultHandler);
        Storage.instance.login(userLogin, userPassword, responder);
    }
    
    public function setInitFocus():void 
    {
        view.focusManager.setFocus(view.username);
    }

    public function doRegisterView():void 
    {
        registerView = RegisterView.open(view, true);
        registerView.setController(this);
    }

    public function doRegister(person:Person, login:String, password:String):void 
    {
        var responder:Responder = new Responder(onRegisterResultHandler, 
        		onRegisterFaultHandler);
        Storage.instance.register(person, login, password, responder);
    }

    public function restorePassword():void 
    {
/*    	
        if (!view.restoreFormValidator.validate(true)) {
            return;
        }
*/        
        var responder:Responder = new Responder(restorePassword_onResultHandler, restorePassword_onFaultHandler);
        Storage.instance.restorePassword(view.username.text, responder);
    }
    
    private function login_onResultHandler(event:ResultEvent):void 
    {
        serviceIsBusy = false;
        view.dispatchEvent(new LoginCompleteEvent(
        		LoginCompleteEvent.LOGIN_COMPLETE, event.result as Object));
    }
    
    private function login_onFaultHandler(event:FaultEvent):void 
    {
        serviceIsBusy = false;
        view.dispatchEvent(new LoginFaultEvent(LoginFaultEvent.LOGIN_FAULT, event.fault));
    }

    private function onRegisterResultHandler(event:ResultEvent):void 
    {
        serviceIsBusy = false;
        Alert.show("You have sucessifully registered, please login.");
        registerView.close();
    }
    
    private function onRegisterFaultHandler(event:FaultEvent):void 
    {
        serviceIsBusy = false;
        Alert.show(event.fault.faultString);
    }
    
    private function restorePassword_onResultHandler(event:ResultEvent):void 
    {
        serviceIsBusy = false;
        var isSucces:Boolean = event.result as Boolean;

        if (isSucces) {
            view.loginMode = true;
            Alert.show("Password was sent to your email");
        } else {
            Alert.show("User with current login not found");
        }
    }
    
    private function restorePassword_onFaultHandler(event:FaultEvent):void 
    {
        serviceIsBusy = false;
        Alert.show(event.fault.faultString);
    }
}
}
