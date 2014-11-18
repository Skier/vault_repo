
package com.dalworth.servman.domain
{
    [Bindable]
    [RemoteClass(alias="Servman.Domain.ProjectTypeQbItem")]
    public class ProjectTypeQbItem
    {
        public function ProjectTypeQbItem()
        {
        }
      
        private var _projectTypeId:int;
        public function get projectTypeId():int { return _projectTypeId; }
        public function set projectTypeId(value:int):void 
        {
            _projectTypeId = value;
        }
      
        private var _qbItemLiistId:String;
        public function get qbItemLiistId():String { return _qbItemLiistId; }
        public function set qbItemLiistId(value:String):void 
        {
            _qbItemLiistId = value;
        }
      
    }
}
      