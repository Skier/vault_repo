package tractIncStaffManager.team.member
{
    import flash.events.Event;
    import mx.events.ItemClickEvent;
    import mx.rpc.Responder;
    import mx.rpc.events.ResultEvent;
    import mx.rpc.events.FaultEvent;
    import mx.controls.Alert;
    import mx.collections.ArrayCollection;
    import mx.controls.dataGridClasses.DataGridColumn;
    
    import TractInc.Domain.Team;
    import TractInc.Domain.TeamMember;
    import TractInc.Domain.Asset;
    import tractInc.domain.packages.StaffManagerPackage;
    import tractInc.domain.storage.IStaffManagerStorage;
    import tractInc.domain.storage.StaffManagerStorage;
    import tractIncStaffManager.StaffManagerController;
    import tractIncStaffManager.team.TeamController;
    
    [Bindable]
    public class TeamMemberController
    {
        private static var instance:TeamMemberController = null;
        
        public static function getInstance():TeamMemberController
        {
            return instance;
        }

        public var team:Team = null;  
        public var parentController:TeamController = null;  
        public var view:TeamMemberView = null;
        private var editView:EditView = null;
        
        public function TeamMemberController():void 
        {
            instance = this;    
        }

        public function init(cp:Team, pc:TeamController):void 
        {
            team = cp;
            parentController = pc;
            if ( null != cp ) {
                view.memberDataGrid.dataProvider = new ArrayCollection(cp.MemberList);
            }
        }
        
/*        
        public function getClientPersonById(id:int):Person
        {
            for each (var i:Person in teamPackage.TeamClient.PersonList) {
                if ( id == i.PersonId ) {
                    return i;
                }
            }
            return null;    
        }
*/

        public function openTeamMember(clientContact:TeamMember):void
        {
            editView = EditView.open(this, clientContact, true);
        }
        
        public function addMemberButtonOnClickHandler(event:Event):void 
        {
            editView = EditView.open(this, null, true);
        }

        public function nameLabelFunction(item:Object, column:DataGridColumn):String
        {
            var member:TeamMember = item as TeamMember;
            var asset:Asset = member.MemberAsset;
            return asset.AssetName;
        }
        
        public function dateLabelFunction(item:Object, column:DataGridColumn):String
        {
            var member:TeamMember = item as TeamMember;
            if ( "Start Date" == column.headerText ) {
                return StaffManagerController.getInstance().getDateFormater().format(member.StartDate);
            } else {
                return StaffManagerController.getInstance().getDateFormater().format(member.EndDate);
            }
        }
        
        public function saveTeamMember(clientContact:TeamMember):void 
        {
            clientContact.TeamId = team.TeamId;
            
            var responder:Responder = new Responder(
                    saveTeamMemberResultHandler, 
                    saveTeamMemberFaultHandler);
                    
            StaffManagerController.getInstance().model.isBusy = true;
            StaffManagerController.getInstance().storage.saveTeamMember(clientContact, responder);
        }
        
        public function removeTeamMember(clientContact:TeamMember):void
        {
            var responder:Responder = new Responder(
                    removeTeamMemberResultHandler, 
                    removeTeamMemberFaultHandler);

            StaffManagerController.getInstance().model.isBusy = true;
            StaffManagerController.getInstance().storage.removeTeamMember(clientContact, responder);
        }
        
        public function reloadTeamMemberList():void
        {
            var responder:Responder = new Responder(
                    reloadTeamMemberListResultHandler, 
                    reloadTeamMemberListFaultHandler);

            StaffManagerController.getInstance().model.isBusy = true;
            StaffManagerController.getInstance().storage.getTeamMemberList(team.TeamId, responder);
        }
        
        private function saveTeamMemberResultHandler(event:ResultEvent):void 
        {
            StaffManagerController.getInstance().model.isBusy = false;
            editView.close();
            reloadTeamMemberList();
        }
        
        private function saveTeamMemberFaultHandler(event:FaultEvent):void 
        {
            StaffManagerController.getInstance().model.isBusy = false;
            Alert.show(event.fault.faultString);
        }

        private function removeTeamMemberResultHandler(event:ResultEvent):void 
        {
            StaffManagerController.getInstance().model.isBusy = false;
            reloadTeamMemberList();
        }
        
        private function removeTeamMemberFaultHandler(event:FaultEvent):void 
        {
            StaffManagerController.getInstance().model.isBusy = false;
            Alert.show(event.fault.faultString);
        }

        private function reloadTeamMemberListResultHandler(event:ResultEvent):void 
        {
            StaffManagerController.getInstance().model.isBusy = false;
            team.MemberList = event.result as Array;
            view.memberDataGrid.dataProvider = new ArrayCollection(team.MemberList);
        }
        
        private function reloadTeamMemberListFaultHandler(event:FaultEvent):void 
        {
            StaffManagerController.getInstance().model.isBusy = false;
            Alert.show(event.fault.faultString);
        }
    }
}
