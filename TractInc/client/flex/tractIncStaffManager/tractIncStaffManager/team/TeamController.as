package tractIncStaffManager.team
{
    import flash.events.Event;
    import mx.events.ItemClickEvent;
    import mx.rpc.Responder;
    import mx.rpc.events.ResultEvent;
    import mx.rpc.events.FaultEvent;
    import mx.controls.Alert;
    import mx.controls.dataGridClasses.DataGridColumn;
    import mx.collections.ArrayCollection;
    
    import TractInc.Domain.Team;
    import TractInc.Domain.TeamMember;
    import TractInc.Domain.Asset;
    import tractInc.domain.packages.StaffManagerPackage;
    import tractInc.domain.storage.IStaffManagerStorage;
    import tractInc.domain.storage.StaffManagerStorage;
    import tractIncStaffManager.StaffManagerController;
    
    [Bindable]
    public class TeamController
    {
        private static var instance:TeamController = null;
        
        public static function getInstance():TeamController
        {
            return instance;
        }

        public var parentController:StaffManagerController = null;  
        public var view:TeamView = null;
        private var editView:EditView = null;
        
        public function TeamController():void 
        {
            instance = this;    
        }

        public function init(pc:StaffManagerController):void 
        {
            parentController = pc;
            
            var root:Object = new Object();
            loadNode(0, root);
            
            view.masterDataGrid.dataProvider = root;
        }

        private function loadNode(parentId:int, node:Object):void {
            for each(var a:Team in StaffManagerController.getInstance().model.staffManagerPackage.TeamList) {
                if ( parentId == a.ParentTeamId ) {
                    if ( null == node.children ) {
                        node.children = new ArrayCollection();
                    }
                    var n:Object = new Object();
                    node.children.addItem(n);
                    n.data = a;
                    n.label = a.TeamName;
                    loadNode(a.TeamId, n);
                }
            }
        }
        
        public function masterGridOnClickHandler(event:Event):void
        {
            var t:Team = view.masterDataGrid.selectedItem.data as Team;
            view.detailDataGrid.dataProvider = t.MemberList;
        }
            
        public function assetNameLabelFunction(item:Object, column:DataGridColumn):String
        {
            var member:TeamMember = item as TeamMember;
            var asset:Asset = member.MemberAsset;
            return asset.AssetName;
        }
        
        public function openTeam(team:Team):void
        {
            editView = EditView.open(this, team, true);
        }
        
        public function addButtonOnClickHandler(event:Event):void 
        {
            editView = EditView.open(this, null, true);
        }

        public function saveTeam(account:Team):void 
        {
            var responder:Responder = new Responder(
                    saveTeamResultHandler, 
                    saveTeamFaultHandler);

            parentController.model.isBusy = true;
            parentController.storage.saveTeam(account, responder);
        }
        
        public function removeTeam(account:Team):void
        {
            var responder:Responder = new Responder(
                    removeTeamResultHandler, 
                    removeTeamFaultHandler);

            parentController.model.isBusy = true;
            parentController.storage.removeTeam(account, responder);
        }
        
        private function saveTeamResultHandler(event:ResultEvent):void 
        {
            parentController.model.isBusy = false;
            editView.close();
            parentController.reloadTeamList();
        }
        
        private function saveTeamFaultHandler(event:FaultEvent):void 
        {
            parentController.model.isBusy = false;
            Alert.show(event.fault.faultString);
        }
        
        private function removeTeamResultHandler(event:ResultEvent):void 
        {
            parentController.model.isBusy = false;
            parentController.reloadTeamList();
        }
        
        private function removeTeamFaultHandler(event:FaultEvent):void 
        {
            parentController.model.isBusy = false;
            Alert.show(event.fault.faultString);
        }
    }
}
