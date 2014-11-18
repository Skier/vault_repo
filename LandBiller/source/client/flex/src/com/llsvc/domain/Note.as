package com.llsvc.domain
{
	import com.llsvc.domain.vo.noteVO;

	[Bindable]
	public class Note extends noteVO
	{
		public var isLoading:Boolean;
		public var isLoaded:Boolean;
		public var isDirty:Boolean;
		
		public var user:User;
		public var invoice:Invoice; 
		
		public function Note()
		{
			super();
		}
		
		public function updateFields(value:noteVO):void 
		{
			if (value == null)
				value = new noteVO(); 
			
			this.noteid = value.noteid;
			this.userid = value.userid;
			this.invoiceid = value.invoiceid;
			this.notedate = value.notedate;
			this.notetext = value.notetext;
			this.notefrom = value.notefrom;
		}
		
		public function toVO():noteVO 
		{
			var result:noteVO = new noteVO();
			
			result.noteid = this.noteid;
			result.userid = this.userid;
			result.invoiceid = this.invoiceid;
			result.notedate = this.notedate;
			result.notetext = this.notetext;
			result.notefrom = this.notefrom;
			
			return result;
		}
		
	}
}