package com.llsvc.domain
{
	import com.llsvc.domain.vo.expencetypeVO;

	[Bindable]
	public class ExpenceType extends expencetypeVO
	{
		public var isLoading:Boolean;
		public var isLoaded:Boolean;
		public var isDirty:Boolean;
		
		public var defaultItem:DefaultExpenceType;
		public var user:User;
		
		public function ExpenceType()
		{
			super();
		}
		
		public function get defaultItemStr():String 
		{
			if (defaultItem != null) 
			{
				return defaultItem.itemname;
			} else 
			{
				return "n/a";
			}
		}
		
		public function updateFields(value:expencetypeVO):void 
		{
			if (value == null)
				value = new expencetypeVO(); 
			
			this.expencetypeid = value.expencetypeid;
			this.userid = value.userid;
			this.itemname = value.itemname;
			this.defaultrate = value.defaultrate;
			this.basedon = value.basedon;
		}
		
		public function toVO():expencetypeVO 
		{
			var result:expencetypeVO = new expencetypeVO()
			
			result.expencetypeid = this.expencetypeid;
			result.userid = this.userid;
			result.itemname = this.itemname;
			result.defaultrate = this.defaultrate;
			result.basedon = this.basedon;
			
			return result;
		}
		
	}
}