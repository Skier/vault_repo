package tractIncStaffManager.team.assignment
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
    import TractInc.Domain.TeamAssignment;
    import TractInc.Domain.Asset;
    import TractInc.Domain.Project;
    import tractInc.domain.packages.StaffManagerPackage;
    import tractInc.domain.storage.IStaffManagerStorage;
    import tractInc.domain.storage.StaffManagerStorage;
    import tractIncStaffManager.StaffManagerController;
    import tractIncStaffManager.team.TeamController;
    
    [Bindable]
    public class TeamAssignmentController
    {
        private static var instance:TeamAssignmentController = null;
        
        public static function getInstance():TeamAssignmentController
        {
            return instance;
        }

        public var team:Team = null;  
        public var parentController:TeamController = null;  
        public var view:TeamAssignmentView = null;
        private var editView:EditView = null;
        
        public function TeamAssignmentController():void 
        {
            instance = this;    
        }

        public function init(cp:Team, pc:TeamController):void 
        {
            team = cp;
            parentController = pc;
            if ( null != cp ) {
                view.assignmentDataGrid.dataProvider = new ArrayCollection(cp.TeamAssignmentList);
            }
        }
        
        public function getProjectById(id:int):Project
        {
            for each (var a:Project in StaffManagerController.getInstance().model.staffManagerPackage.ProjectList) {
                if ( id == a.ProjectId ) {
                    return a;
                }
            }
            return null;    
        }
            
        public function getAssetById(id:int):Asset
        {
            for each (var i:Asset in StaffManagerController.getInstance().model.staffManagerPackage.AssetList) {
                if ( id == i.AssetId ) {
                    return i;
                }
            }
            return null;    
        }

        public function openTeamAssignment(teamAssignment:TeamAssignment):void
        {
            editView = EditView.open(this, teamAssignment, true);
        }
        
        public function addMemberButtonOnClickHandler(event:Event):void 
        {
            editView = EditView.open(this, null, true);
        }

        public function projectLabelFunction(item:Object, column:DataGridColumn):String
        {
            var assignment:TeamAssignment = item as TeamAssignment;
            var project:Project = this.getProjectById(assignment.ProjectId);
            return project.ProjectName;
        }
        
        public function nameLabelFunction(item:Object, column:DataGridColumn):String
        {
            var assignment:TeamAssignment = item as TeamAssignment;
            var asset:Asset = this.getAssetById(assignment.LeadAssetId);
            return asset.AssetName;
        }
        
        public function dateLabelFunction(item:Object, column:DataGridColumn):String
        {
            var assignment:TeamAssignment = item as TeamAssignment;
            if ( "Start Date" == column.headerText ) {
                return StaffManagerController.getInstance().getDateFormater().format(assignment.StartDate);
            } else {
                return StaffManagerController.getInstance().getDateFormater().format(assignment.EndDate);
            }
        }
        
        public function saveTeamAssignment(teamAssignment:TeamAssignment):void 
        {
            teamAssignment.TeamId = team.TeamId;
            
            var responder:Responder = new Responder(
                    saveTeamAssignmentResultHandler, 
                    saveTeamAssignmentFaultHandler);
                    
            StaffManagerController.getInstance().model.isBusy = true;
            StaffManagerController.getInstance().storage.saveTeamAssignment(teamAssignment, responder);
        }
        
        public function removeTeamAssignment(teamAssignment:TeamAssignment):void
        {
            var responder:Responder = new Responder(
                    removeTeamAssignmentResultHandler, 
                    removeTeamAssignmentFaultHandler);

            StaffManagerController.getInstance().model.isBusy = true;
            StaffManagerController.getInstance().storage.removeTeamAssignment(teamAssignment, responder);
        }
        
        public function reloadTeamAssignmentList():void
        {
            var responder:Responder = new Responder(
                    reloadTeamAssignmentListResultHandler, 
                    reloadTeamAssignmentListFaultHandler);

            StaffManagerController.getInstance().model.isBusy = true;
            StaffManagerController.getInstance().storage.getTeamAssignmentList(team.TeamId, responder);
        }
        
        private function saveTeamAssignmentResultHandler(event:ResultEvent):void 
        {
            StaffManagerController.getInstance().model.isBusy = false;
            editView.close();
            reloadTeamAssignmentList();
        }
        
        private function saveTeamAssignmentFaultHandler(event:FaultEvent):void 
        {
            StaffManagerController.getInstance().model.isBusy = false;
            Alert.show(event.fault.faultString);
        }

        private function removeTeamAssignmentResultHandler(event:ResultEvent):void 
        {
            StaffManagerController.getInstance().model.isBusy = false;
            reloadTeamAssignmentList();
        }
        
        private function removeTeamAssignmentFaultHandler(event:FaultEvent):void 
        {
            StaffManagerController.getInstance().model.isBusy = false;
            Alert.show(event.fault.faultString);
        }

        private function reloadTeamAssignmentListResultHandler(event:ResultEvent):void 
        {
            StaffManagerController.getInstance().model.isBusy = false;
            team.MemberList = event.result as Array;
            view.assignmentDataGrid.dataProvider = new ArrayCollection(team.MemberList);
        }
        
        private function reloadTeamAssignmentListFaultHandler(event:FaultEvent):void 
        {
            StaffManagerController.getInstance().model.isBusy = false;
            Alert.show(event.fault.faultString);
        }
    }
}
