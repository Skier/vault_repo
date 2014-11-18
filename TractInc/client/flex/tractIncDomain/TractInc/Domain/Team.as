package TractInc.Domain
{
    import mx.collections.ArrayCollection;
    
    [Bindable]
    [RemoteClass(alias="TractInc.Server.Domain.Team")]
    public class Team
    {
        public var TeamId:int;      
        public var CompanyId:int;      
        public var ParentTeamId:int;      
        public var TeamName:String;

        private var _cList:Array;
        public function get MemberList():Array { return _cList; }
        public function set MemberList(value:Array):void 
        { 
            _cList = value;
        }

        private var _taList:Array;
        public function get TeamAssignmentList():Array { return _taList; }
        public function set TeamAssignmentList(value:Array):void 
        { 
            _taList = value;
        }
    }
}