
      package App.Domain
      {
        public final class ActiveRecords
        {
          

            public static function get AfeStatus():AfeStatusDataMapper
            {
              return DataMapperRegistry.Instance.AfeStatus;
            }
          

            public static function get Client():ClientDataMapper
            {
              return DataMapperRegistry.Instance.Client;
            }
          

            public static function get AssetType():AssetTypeDataMapper
            {
              return DataMapperRegistry.Instance.AssetType;
            }
          

            public static function get Asset():AssetDataMapper
            {
              return DataMapperRegistry.Instance.Asset;
            }
          

            public static function get Afe():AfeDataMapper
            {
              return DataMapperRegistry.Instance.Afe;
            }
          

            public static function get BillStatus():BillStatusDataMapper
            {
              return DataMapperRegistry.Instance.BillStatus;
            }
          

            public static function get BillItemStatus():BillItemStatusDataMapper
            {
              return DataMapperRegistry.Instance.BillItemStatus;
            }
          

            public static function get InvoiceItemType():InvoiceItemTypeDataMapper
            {
              return DataMapperRegistry.Instance.InvoiceItemType;
            }
          

            public static function get Bill():BillDataMapper
            {
              return DataMapperRegistry.Instance.Bill;
            }
          

            public static function get SubAfeStatus():SubAfeStatusDataMapper
            {
              return DataMapperRegistry.Instance.SubAfeStatus;
            }
          

            public static function get BillItemType():BillItemTypeDataMapper
            {
              return DataMapperRegistry.Instance.BillItemType;
            }
          

            public static function get SubAfe():SubAfeDataMapper
            {
              return DataMapperRegistry.Instance.SubAfe;
            }
          

            public static function get InvoiceStatus():InvoiceStatusDataMapper
            {
              return DataMapperRegistry.Instance.InvoiceStatus;
            }
          

            public static function get InvoiceItemStatus():InvoiceItemStatusDataMapper
            {
              return DataMapperRegistry.Instance.InvoiceItemStatus;
            }
          

            public static function get Invoice():InvoiceDataMapper
            {
              return DataMapperRegistry.Instance.Invoice;
            }
          

            public static function get AssetAssignment():AssetAssignmentDataMapper
            {
              return DataMapperRegistry.Instance.AssetAssignment;
            }
          

            public static function get BillItem():BillItemDataMapper
            {
              return DataMapperRegistry.Instance.BillItem;
            }
          

            public static function get User():UserDataMapper
            {
              return DataMapperRegistry.Instance.User;
            }
          

            public static function get Module():ModuleDataMapper
            {
              return DataMapperRegistry.Instance.Module;
            }
          

            public static function get Note():NoteDataMapper
            {
              return DataMapperRegistry.Instance.Note;
            }
          

            public static function get Permission():PermissionDataMapper
            {
              return DataMapperRegistry.Instance.Permission;
            }
          

            public static function get Role():RoleDataMapper
            {
              return DataMapperRegistry.Instance.Role;
            }
          

            public static function get RateByAssignment():RateByAssignmentDataMapper
            {
              return DataMapperRegistry.Instance.RateByAssignment;
            }
          

            public static function get PermissionAssignment():PermissionAssignmentDataMapper
            {
              return DataMapperRegistry.Instance.PermissionAssignment;
            }
          

            public static function get BillItemAttachment():BillItemAttachmentDataMapper
            {
              return DataMapperRegistry.Instance.BillItemAttachment;
            }
          

            public static function get InvoiceItem():InvoiceItemDataMapper
            {
              return DataMapperRegistry.Instance.InvoiceItem;
            }
          

            public static function get SyncLog():SyncLogDataMapper
            {
              return DataMapperRegistry.Instance.SyncLog;
            }
          

            public static function get Message():MessageDataMapper
            {
              return DataMapperRegistry.Instance.Message;
            }
          

            public static function get UserAsset():UserAssetDataMapper
            {
              return DataMapperRegistry.Instance.UserAsset;
            }
          

            public static function get UserRole():UserRoleDataMapper
            {
              return DataMapperRegistry.Instance.UserRole;
            }
          
        }
      }
    