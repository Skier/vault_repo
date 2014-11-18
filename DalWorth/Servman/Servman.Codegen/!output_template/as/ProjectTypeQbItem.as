
package com.dalworth.servman.domain
{
    [Bindable]
    [RemoteClass(alias="Servman.Domain.ProjectTypeQbItem")]
    public class ProjectTypeQbItem
    {
        public function ProjectTypeQbItem()
        {
        }
      
        private var projectTypeId:int;
        public function get ProjectTypeId():int { return projectTypeId; }
        public function set ProjectTypeId(value:int):void 
        {
            projectTypeId = value;
        }
      
        private var qbItemLiistId:String;
        public function get QbItemLiistId():String { return qbItemLiistId; }
        public function set QbItemLiistId(value:String):void 
        {
            qbItemLiistId = value;
        }
      

        public function clone():ProjectTypeQbItem
        {
            var result:ProjectTypeQbItem = new ProjectTypeQbItem();
      
            result.ProjectTypeId = this.ProjectTypeId;
      
            result.QbItemLiistId = this.QbItemLiistId;
      
            return result;
        }

        public function updateFields(value:ProjectTypeQbItem):void 
        {
            if (value == null)
                value = new ProjectTypeQbItem();
      
            this.ProjectTypeId = value.ProjectTypeId;
      
            this.QbItemLiistId = value.QbItemLiistId;
      
        }
    }
}
      