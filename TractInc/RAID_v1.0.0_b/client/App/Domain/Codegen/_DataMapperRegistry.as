
     package App.Domain.Codegen
     {
      
        import App.Domain.AfeStatusDataMapper;
      
        import App.Domain.ClientDataMapper;
      
        import App.Domain.AssetTypeDataMapper;
      
        import App.Domain.AssetDataMapper;
      
        import App.Domain.AfeDataMapper;
      
        import App.Domain.BillStatusDataMapper;
      
        import App.Domain.BillItemStatusDataMapper;
      
        import App.Domain.InvoiceItemTypeDataMapper;
      
        import App.Domain.BillDataMapper;
      
        import App.Domain.SubAfeStatusDataMapper;
      
        import App.Domain.BillItemTypeDataMapper;
      
        import App.Domain.SubAfeDataMapper;
      
        import App.Domain.InvoiceStatusDataMapper;
      
        import App.Domain.InvoiceItemStatusDataMapper;
      
        import App.Domain.InvoiceDataMapper;
      
        import App.Domain.AssetAssignmentDataMapper;
      
        import App.Domain.BillItemDataMapper;
      
        import App.Domain.UserDataMapper;
      
        import App.Domain.ModuleDataMapper;
      
        import App.Domain.NoteDataMapper;
      
        import App.Domain.PermissionDataMapper;
      
        import App.Domain.RoleDataMapper;
      
        import App.Domain.RateByAssignmentDataMapper;
      
        import App.Domain.PermissionAssignmentDataMapper;
      
        import App.Domain.BillItemAttachmentDataMapper;
      
        import App.Domain.InvoiceItemDataMapper;
      
        import App.Domain.SyncLogDataMapper;
      
        import App.Domain.MessageDataMapper;
      
        import App.Domain.UserAssetDataMapper;
      
        import App.Domain.UserRoleDataMapper;
      
       public class _DataMapperRegistry
       {
        

          private var m_afeStatusDataMapper:AfeStatusDataMapper;

          public function get AfeStatus():AfeStatusDataMapper
          {
            if(m_afeStatusDataMapper == null )
              m_afeStatusDataMapper = new AfeStatusDataMapper();
              
            return m_afeStatusDataMapper;
          }
        

          private var m_clientDataMapper:ClientDataMapper;

          public function get Client():ClientDataMapper
          {
            if(m_clientDataMapper == null )
              m_clientDataMapper = new ClientDataMapper();
              
            return m_clientDataMapper;
          }
        

          private var m_assetTypeDataMapper:AssetTypeDataMapper;

          public function get AssetType():AssetTypeDataMapper
          {
            if(m_assetTypeDataMapper == null )
              m_assetTypeDataMapper = new AssetTypeDataMapper();
              
            return m_assetTypeDataMapper;
          }
        

          private var m_assetDataMapper:AssetDataMapper;

          public function get Asset():AssetDataMapper
          {
            if(m_assetDataMapper == null )
              m_assetDataMapper = new AssetDataMapper();
              
            return m_assetDataMapper;
          }
        

          private var m_afeDataMapper:AfeDataMapper;

          public function get Afe():AfeDataMapper
          {
            if(m_afeDataMapper == null )
              m_afeDataMapper = new AfeDataMapper();
              
            return m_afeDataMapper;
          }
        

          private var m_billStatusDataMapper:BillStatusDataMapper;

          public function get BillStatus():BillStatusDataMapper
          {
            if(m_billStatusDataMapper == null )
              m_billStatusDataMapper = new BillStatusDataMapper();
              
            return m_billStatusDataMapper;
          }
        

          private var m_billItemStatusDataMapper:BillItemStatusDataMapper;

          public function get BillItemStatus():BillItemStatusDataMapper
          {
            if(m_billItemStatusDataMapper == null )
              m_billItemStatusDataMapper = new BillItemStatusDataMapper();
              
            return m_billItemStatusDataMapper;
          }
        

          private var m_invoiceItemTypeDataMapper:InvoiceItemTypeDataMapper;

          public function get InvoiceItemType():InvoiceItemTypeDataMapper
          {
            if(m_invoiceItemTypeDataMapper == null )
              m_invoiceItemTypeDataMapper = new InvoiceItemTypeDataMapper();
              
            return m_invoiceItemTypeDataMapper;
          }
        

          private var m_billDataMapper:BillDataMapper;

          public function get Bill():BillDataMapper
          {
            if(m_billDataMapper == null )
              m_billDataMapper = new BillDataMapper();
              
            return m_billDataMapper;
          }
        

          private var m_subAfeStatusDataMapper:SubAfeStatusDataMapper;

          public function get SubAfeStatus():SubAfeStatusDataMapper
          {
            if(m_subAfeStatusDataMapper == null )
              m_subAfeStatusDataMapper = new SubAfeStatusDataMapper();
              
            return m_subAfeStatusDataMapper;
          }
        

          private var m_billItemTypeDataMapper:BillItemTypeDataMapper;

          public function get BillItemType():BillItemTypeDataMapper
          {
            if(m_billItemTypeDataMapper == null )
              m_billItemTypeDataMapper = new BillItemTypeDataMapper();
              
            return m_billItemTypeDataMapper;
          }
        

          private var m_subAfeDataMapper:SubAfeDataMapper;

          public function get SubAfe():SubAfeDataMapper
          {
            if(m_subAfeDataMapper == null )
              m_subAfeDataMapper = new SubAfeDataMapper();
              
            return m_subAfeDataMapper;
          }
        

          private var m_invoiceStatusDataMapper:InvoiceStatusDataMapper;

          public function get InvoiceStatus():InvoiceStatusDataMapper
          {
            if(m_invoiceStatusDataMapper == null )
              m_invoiceStatusDataMapper = new InvoiceStatusDataMapper();
              
            return m_invoiceStatusDataMapper;
          }
        

          private var m_invoiceItemStatusDataMapper:InvoiceItemStatusDataMapper;

          public function get InvoiceItemStatus():InvoiceItemStatusDataMapper
          {
            if(m_invoiceItemStatusDataMapper == null )
              m_invoiceItemStatusDataMapper = new InvoiceItemStatusDataMapper();
              
            return m_invoiceItemStatusDataMapper;
          }
        

          private var m_invoiceDataMapper:InvoiceDataMapper;

          public function get Invoice():InvoiceDataMapper
          {
            if(m_invoiceDataMapper == null )
              m_invoiceDataMapper = new InvoiceDataMapper();
              
            return m_invoiceDataMapper;
          }
        

          private var m_assetAssignmentDataMapper:AssetAssignmentDataMapper;

          public function get AssetAssignment():AssetAssignmentDataMapper
          {
            if(m_assetAssignmentDataMapper == null )
              m_assetAssignmentDataMapper = new AssetAssignmentDataMapper();
              
            return m_assetAssignmentDataMapper;
          }
        

          private var m_billItemDataMapper:BillItemDataMapper;

          public function get BillItem():BillItemDataMapper
          {
            if(m_billItemDataMapper == null )
              m_billItemDataMapper = new BillItemDataMapper();
              
            return m_billItemDataMapper;
          }
        

          private var m_userDataMapper:UserDataMapper;

          public function get User():UserDataMapper
          {
            if(m_userDataMapper == null )
              m_userDataMapper = new UserDataMapper();
              
            return m_userDataMapper;
          }
        

          private var m_moduleDataMapper:ModuleDataMapper;

          public function get Module():ModuleDataMapper
          {
            if(m_moduleDataMapper == null )
              m_moduleDataMapper = new ModuleDataMapper();
              
            return m_moduleDataMapper;
          }
        

          private var m_noteDataMapper:NoteDataMapper;

          public function get Note():NoteDataMapper
          {
            if(m_noteDataMapper == null )
              m_noteDataMapper = new NoteDataMapper();
              
            return m_noteDataMapper;
          }
        

          private var m_permissionDataMapper:PermissionDataMapper;

          public function get Permission():PermissionDataMapper
          {
            if(m_permissionDataMapper == null )
              m_permissionDataMapper = new PermissionDataMapper();
              
            return m_permissionDataMapper;
          }
        

          private var m_roleDataMapper:RoleDataMapper;

          public function get Role():RoleDataMapper
          {
            if(m_roleDataMapper == null )
              m_roleDataMapper = new RoleDataMapper();
              
            return m_roleDataMapper;
          }
        

          private var m_rateByAssignmentDataMapper:RateByAssignmentDataMapper;

          public function get RateByAssignment():RateByAssignmentDataMapper
          {
            if(m_rateByAssignmentDataMapper == null )
              m_rateByAssignmentDataMapper = new RateByAssignmentDataMapper();
              
            return m_rateByAssignmentDataMapper;
          }
        

          private var m_permissionAssignmentDataMapper:PermissionAssignmentDataMapper;

          public function get PermissionAssignment():PermissionAssignmentDataMapper
          {
            if(m_permissionAssignmentDataMapper == null )
              m_permissionAssignmentDataMapper = new PermissionAssignmentDataMapper();
              
            return m_permissionAssignmentDataMapper;
          }
        

          private var m_billItemAttachmentDataMapper:BillItemAttachmentDataMapper;

          public function get BillItemAttachment():BillItemAttachmentDataMapper
          {
            if(m_billItemAttachmentDataMapper == null )
              m_billItemAttachmentDataMapper = new BillItemAttachmentDataMapper();
              
            return m_billItemAttachmentDataMapper;
          }
        

          private var m_invoiceItemDataMapper:InvoiceItemDataMapper;

          public function get InvoiceItem():InvoiceItemDataMapper
          {
            if(m_invoiceItemDataMapper == null )
              m_invoiceItemDataMapper = new InvoiceItemDataMapper();
              
            return m_invoiceItemDataMapper;
          }
        

          private var m_syncLogDataMapper:SyncLogDataMapper;

          public function get SyncLog():SyncLogDataMapper
          {
            if(m_syncLogDataMapper == null )
              m_syncLogDataMapper = new SyncLogDataMapper();
              
            return m_syncLogDataMapper;
          }
        

          private var m_messageDataMapper:MessageDataMapper;

          public function get Message():MessageDataMapper
          {
            if(m_messageDataMapper == null )
              m_messageDataMapper = new MessageDataMapper();
              
            return m_messageDataMapper;
          }
        

          private var m_userAssetDataMapper:UserAssetDataMapper;

          public function get UserAsset():UserAssetDataMapper
          {
            if(m_userAssetDataMapper == null )
              m_userAssetDataMapper = new UserAssetDataMapper();
              
            return m_userAssetDataMapper;
          }
        

          private var m_userRoleDataMapper:UserRoleDataMapper;

          public function get UserRole():UserRoleDataMapper
          {
            if(m_userRoleDataMapper == null )
              m_userRoleDataMapper = new UserRoleDataMapper();
              
            return m_userRoleDataMapper;
          }
            
        }
      }
    