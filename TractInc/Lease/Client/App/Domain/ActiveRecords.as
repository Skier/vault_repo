
      package App.Domain
      {
        public final class ActiveRecords
        {
          

            public static function get User():UserDataMapper
            {
              return DataMapperRegistry.Instance.User;
            }
          

            public static function get UserRole():UserRoleDataMapper
            {
              return DataMapperRegistry.Instance.UserRole;
            }
          

            public static function get County():CountyDataMapper
            {
              return DataMapperRegistry.Instance.County;
            }
          

            public static function get Lease():LeaseDataMapper
            {
              return DataMapperRegistry.Instance.Lease;
            }
          

            public static function get State():StateDataMapper
            {
              return DataMapperRegistry.Instance.State;
            }
          

            public static function get LeaseEditHistory():LeaseEditHistoryDataMapper
            {
              return DataMapperRegistry.Instance.LeaseEditHistory;
            }
          

            public static function get Module():ModuleDataMapper
            {
              return DataMapperRegistry.Instance.Module;
            }
          

            public static function get Permission():PermissionDataMapper
            {
              return DataMapperRegistry.Instance.Permission;
            }
          

            public static function get PermissionAssignment():PermissionAssignmentDataMapper
            {
              return DataMapperRegistry.Instance.PermissionAssignment;
            }
          

            public static function get Role():RoleDataMapper
            {
              return DataMapperRegistry.Instance.Role;
            }
          

            public static function get TermUnit():TermUnitDataMapper
            {
              return DataMapperRegistry.Instance.TermUnit;
            }
          
        }
      }
    