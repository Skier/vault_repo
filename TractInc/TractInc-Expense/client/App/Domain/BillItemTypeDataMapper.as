
      package App.Domain
      {
        import App.Domain.Codegen.*;
        import weborb.data.ActiveCollection;
        
	      public dynamic class BillItemTypeDataMapper extends _BillItemTypeDataMapper
	      {
	      	
	      	public function getAll():ActiveCollection {
	      		return ActiveRecords.BillItemType.findByDeleted(false);
	      	}

	      }
      }
    