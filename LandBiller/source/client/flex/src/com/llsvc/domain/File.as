package com.llsvc.domain
{
	import com.llsvc.domain.vo.fileVO;
	
	[Bindable]
	public class File extends fileVO
	{
		public var isLoading:Boolean;
		public var isLoaded:Boolean;
		public var isDirty:Boolean;
		
		public function File()
		{
			super();
		}

		public function updateFields(value:fileVO):void 
		{
			if (value == null)
				value = new fileVO(); 
			
			this.fileid = value.fileid;
			this.origfilename = value.origfilename;
			this.storagekey = value.storagekey;
			this.userid = value.userid;
			this.note = value.note;
		}
		
		public function toVO():fileVO 
		{
			var result:fileVO = new fileVO();
			
			result.fileid = this.fileid;
			result.origfilename = this.origfilename;
			result.storagekey = this.storagekey;
			result.userid = this.userid;
			result.note = this.note;
			
			return result;
		}
		
	}
}