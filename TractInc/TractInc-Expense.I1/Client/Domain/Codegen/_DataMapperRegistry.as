
     package Domain.Codegen
     {
      
        import Domain.AfeDataMapper;
      
        import Domain.AfeStatusDataMapper;
      
        import Domain.AssetDataMapper;
      
        import Domain.AssetAssignmentDataMapper;
      
        import Domain.AssetTypeDataMapper;
      
        import Domain.BillDataMapper;
      
        import Domain.BillItemDataMapper;
      
        import Domain.BillItemStatusDataMapper;
      
        import Domain.BillStatusDataMapper;
      
        import Domain.ClientDataMapper;
      
        import Domain.RateDataMapper;
      
        import Domain.SubAfeDataMapper;
      
        import Domain.SubAfeStatusDataMapper;
      
        import Domain.SyncLogDataMapper;
      
       public class _DataMapperRegistry
       {
        

          private var m_afeDataMapper:AfeDataMapper;

          public function get Afe():AfeDataMapper
          {
            if(m_afeDataMapper == null )
              m_afeDataMapper = new AfeDataMapper();
              
            return m_afeDataMapper;
          }
        

          private var m_afeStatusDataMapper:AfeStatusDataMapper;

          public function get AfeStatus():AfeStatusDataMapper
          {
            if(m_afeStatusDataMapper == null )
              m_afeStatusDataMapper = new AfeStatusDataMapper();
              
            return m_afeStatusDataMapper;
          }
        

          private var m_assetDataMapper:AssetDataMapper;

          public function get Asset():AssetDataMapper
          {
            if(m_assetDataMapper == null )
              m_assetDataMapper = new AssetDataMapper();
              
            return m_assetDataMapper;
          }
        

          private var m_assetAssignmentDataMapper:AssetAssignmentDataMapper;

          public function get AssetAssignment():AssetAssignmentDataMapper
          {
            if(m_assetAssignmentDataMapper == null )
              m_assetAssignmentDataMapper = new AssetAssignmentDataMapper();
              
            return m_assetAssignmentDataMapper;
          }
        

          private var m_assetTypeDataMapper:AssetTypeDataMapper;

          public function get AssetType():AssetTypeDataMapper
          {
            if(m_assetTypeDataMapper == null )
              m_assetTypeDataMapper = new AssetTypeDataMapper();
              
            return m_assetTypeDataMapper;
          }
        

          private var m_billDataMapper:BillDataMapper;

          public function get Bill():BillDataMapper
          {
            if(m_billDataMapper == null )
              m_billDataMapper = new BillDataMapper();
              
            return m_billDataMapper;
          }
        

          private var m_billItemDataMapper:BillItemDataMapper;

          public function get BillItem():BillItemDataMapper
          {
            if(m_billItemDataMapper == null )
              m_billItemDataMapper = new BillItemDataMapper();
              
            return m_billItemDataMapper;
          }
        

          private var m_billItemStatusDataMapper:BillItemStatusDataMapper;

          public function get BillItemStatus():BillItemStatusDataMapper
          {
            if(m_billItemStatusDataMapper == null )
              m_billItemStatusDataMapper = new BillItemStatusDataMapper();
              
            return m_billItemStatusDataMapper;
          }
        

          private var m_billStatusDataMapper:BillStatusDataMapper;

          public function get BillStatus():BillStatusDataMapper
          {
            if(m_billStatusDataMapper == null )
              m_billStatusDataMapper = new BillStatusDataMapper();
              
            return m_billStatusDataMapper;
          }
        

          private var m_clientDataMapper:ClientDataMapper;

          public function get Client():ClientDataMapper
          {
            if(m_clientDataMapper == null )
              m_clientDataMapper = new ClientDataMapper();
              
            return m_clientDataMapper;
          }
        

          private var m_rateDataMapper:RateDataMapper;

          public function get Rate():RateDataMapper
          {
            if(m_rateDataMapper == null )
              m_rateDataMapper = new RateDataMapper();
              
            return m_rateDataMapper;
          }
        

          private var m_subAfeDataMapper:SubAfeDataMapper;

          public function get SubAfe():SubAfeDataMapper
          {
            if(m_subAfeDataMapper == null )
              m_subAfeDataMapper = new SubAfeDataMapper();
              
            return m_subAfeDataMapper;
          }
        

          private var m_subAfeStatusDataMapper:SubAfeStatusDataMapper;

          public function get SubAfeStatus():SubAfeStatusDataMapper
          {
            if(m_subAfeStatusDataMapper == null )
              m_subAfeStatusDataMapper = new SubAfeStatusDataMapper();
              
            return m_subAfeStatusDataMapper;
          }
        

          private var m_syncLogDataMapper:SyncLogDataMapper;

          public function get SyncLog():SyncLogDataMapper
          {
            if(m_syncLogDataMapper == null )
              m_syncLogDataMapper = new SyncLogDataMapper();
              
            return m_syncLogDataMapper;
          }
            
        }
      }
    