package com.llsvc.domain
{
	import com.llsvc.domain.vo.invoiceitemattachmentVO;
	
	import mx.binding.utils.ChangeWatcher;
	
	[Bindable]
	public class InvoiceItemAttachment extends invoiceitemattachmentVO
	{
		public var isLoading:Boolean;
		public var isLoaded:Boolean;
		public var isDirty:Boolean;
		
		public var invoiceItem:InvoiceItem;
		
		public function InvoiceItemAttachment()
		{
			super();
		}
		
		private var _file:File;
		public function get file():File { return _file; }
		public function set file(value:File):void 
		{
			this._file = value;
			if (file != null) 
			{
				fileName = file.origfilename;
				ChangeWatcher.watch(file, "origfilename", origNameChangeHandler);
			}
		}

		public var fileName:String;

		private function origNameChangeHandler(e:*):void 
		{
			fileName = file.origfilename;
		} 
		
		public function updateFields(value:invoiceitemattachmentVO):void 
		{
			if (value == null)
				value = new invoiceitemattachmentVO(); 
			
			this.invoiceitemattachmentid = value.invoiceitemattachmentid;
			this.invoiceitemid = value.invoiceitemid;
			this.fileid = value.fileid;
		}
		
		public function toVO():invoiceitemattachmentVO 
		{
			var result:invoiceitemattachmentVO = new invoiceitemattachmentVO();
			
			result.invoiceitemattachmentid = this.invoiceitemattachmentid;
			result.invoiceitemid = this.invoiceitemid;
			result.fileid = this.fileid;
			
			return result;
		}
	
	}
}