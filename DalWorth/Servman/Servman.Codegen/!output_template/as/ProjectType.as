
package com.dalworth.servman.domain
{
    [Bindable]
    [RemoteClass(alias="Servman.Domain.ProjectType")]
    public class ProjectType
    {
        public function ProjectType()
        {
        }
      
        private var id:int;
        public function get Id():int { return id; }
        public function set Id(value:int):void 
        {
            id = value;
        }
      
        private var name:String;
        public function get Name():String { return name; }
        public function set Name(value:String):void 
        {
            name = value;
        }
      

        public function clone():ProjectType
        {
            var result:ProjectType = new ProjectType();
      
            result.Id = this.Id;
      
            result.Name = this.Name;
      
            return result;
        }

        public function updateFields(value:ProjectType):void 
        {
            if (value == null)
                value = new ProjectType();
      
            this.Id = value.Id;
      
            this.Name = value.Name;
      
        }
    }
}
      