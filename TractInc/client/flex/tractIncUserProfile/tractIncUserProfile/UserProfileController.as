package tractIncUserProfile
{
    import flash.events.Event;
	import mx.events.ItemClickEvent;
    import mx.rpc.Responder;
    import mx.rpc.events.ResultEvent;
    import mx.rpc.events.FaultEvent;
    import mx.controls.Alert;
	import mx.collections.ArrayCollection;
    
    import TractInc.Domain.User;
    import tractInc.domain.packages.UserProfilePackage;
    import tractInc.domain.storage.IUserProfileStorage;
    import tractInc.domain.storage.UserProfileStorage;
    import TractInc.Domain.Person;
    
    [Bindable]
    public class UserProfileController
    {
        public var tabData:Array = [
            {label:"Personal Info", data:"PersonalInfo"},
            {label:"Preferences", data:"Preferences"},
            {label:"Change Password", data:"ChangePassword"}
        ];
        
        public var model:UserProfileModel = null;
        public var storage:IUserProfileStorage = null;
        public var view:UserProfileView = null;
        
        public function init(user:User):void 
        {
            model = new UserProfileModel();
            storage = UserProfileStorage.instance;
            
            var responder:Responder = new Responder(getUserProfilePackage_onResultHandler, 
                    getUserProfilePackage_onFaultHandler);
            model.isBusy = true;
            storage.getUserProfilePackage(user.UserId, responder);
        }
        
        public function logout():Boolean 
        {
            if (model.isBusy) {
                Alert.show("User profile service is running");
                return false;
            }
            
            if (isDirty) {
                Alert.show("User profile changes not saved");
                return false;
            }
            
            return true;
        }
        
        public function tabChanged(event:ItemClickEvent):void 
        {
            view.tabStack.selectedIndex = event.index;
        }
        
        private function get isDirty():Boolean 
        {
/*            
            if (view.txtFirstName.text != model.user.FirstName) {
                return true;
            }

            if (view.txtLastName.text != model.user.LastName) {
                return true;
            }

            if (view.txtEmail.text != model.user.Email) {
                return true;
            }

            if (view.txtPhone.text != model.user.PhoneNumber) {
                return true;
            }
*/            
            return false;
        }

        private function resetForm():void 
        {
            view.txtFirstName.text = model.userProfilePackage.person.FirstName;
            view.txtLastName.text = model.userProfilePackage.person.LastName;
            view.txtMiddleName.text = model.userProfilePackage.person.MiddleName;
            view.txtEmail.text = model.userProfilePackage.person.Email;
            view.txtPhone.text = model.userProfilePackage.person.PhoneNumber;
            view.txtDefaultSite.text = model.userProfilePackage.userPreference.DefaultSite;
            view.txtNewTracts.text = model.userProfilePackage.userPreference.NewTracts.toString();
            view.txtOldPassword.text = "";
            view.txtNewPassword.text = "";
            view.txtConfirmPassword.text = "";
            
            if ( !model.userProfilePackage.canEdit ) {
                view.txtFirstName.enabled = false;
                view.txtLastName.enabled = false;
                view.txtMiddleName.enabled = false;
                view.txtEmail.enabled = false;
                view.txtPhone.enabled = false;
/*                
                view.txtOldPassword.enabled = false;
                view.txtNewPassword.enabled = false;
                view.txtConfirmPassword.enabled = false;
*/                
            }
            
        }
        
        private function saveUser(user:User):void 
        {
            var responder:Responder = new Responder(saveUser_onResultHandler, saveUser_onFaultHandler);

            model.isBusy = true;

//            AppModel.storage.saveUser(model.user, responder);
        }
        
        private function getUserProfilePackage_onResultHandler(event:ResultEvent):void 
        {
            model.isBusy = false;
            model.init(event.result as UserProfilePackage);

            resetForm();
        }
        
        private function getUserProfilePackage_onFaultHandler(event:FaultEvent):void 
        {
            model.isBusy = false;
            Alert.show(event.fault.message);
        }

        public function btnSave_clickHandler():void 
        {
            var responder:Responder = null;
            
            if (!view.isValid()) {
                return;
            }
            
            if ( 0 == view.tabBar.selectedIndex && model.userProfilePackage.canEdit ) {
                var person:Person = model.userProfilePackage.person;
                person.FirstName = view.txtFirstName.text;
                person.LastName = view.txtLastName.text;
                person.MiddleName = view.txtMiddleName.text;
                person.Email = view.txtEmail.text;
                person.PhoneNumber = view.txtPhone.text;
                
                responder = new Responder(savePersonResultHandler, 
                        savePersonFaultHandler);
                model.isBusy = true;
                storage.savePerson(person, responder);
            }
            
            if ( 2 == view.tabBar.selectedIndex ) {
                var newPassword:String = view.txtNewPassword.text;
                var confirmPassword:String = view.txtConfirmPassword.text;
                if ( newPassword != confirmPassword ) {
                    Alert.show("Password and Confirmation does not match.");
                } else {
                    responder = new Responder(changePasswordResultHandler, 
                            changePasswordFaultHandler);
                    model.isBusy = true;
                    storage.changePassword(model.userProfilePackage.user.UserId,
                            view.txtOldPassword.text,
                            newPassword, responder);
                }
            }
        }
        
        public function btnReset_clickHandler():void 
        {
            resetForm();
        }
        
        private function savePersonResultHandler(event:ResultEvent):void 
        {
            model.isBusy = false;
            resetForm();
            Alert.show("User Information is changed sucessfully.");
        }
        
        private function savePersonFaultHandler(event:FaultEvent):void 
        {
            model.isBusy = false;
            Alert.show(event.fault.faultString);
        }

        private function changePasswordResultHandler(event:ResultEvent):void 
        {
            model.isBusy = false;
            resetForm();
            Alert.show("Password is changed sucessfully.");
        }
        
        private function changePasswordFaultHandler(event:FaultEvent):void 
        {
            model.isBusy = false;
            Alert.show(event.fault.faultString);
        }

        private function saveUser_onResultHandler(event:ResultEvent):void 
        {
            model.isBusy = false;
//            model.user = event.result as User;
        }
        
        private function saveUser_onFaultHandler(event:FaultEvent):void 
        {
            model.isBusy = false;
            Alert.show(event.fault.faultString);
        }
        
    }
}