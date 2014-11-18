
     package App.Domain.Codegen
     {
      
        import App.Domain.CountyDataMapper;
      
        import App.Domain.LeaseDataMapper;
      
        import App.Domain.UserDataMapper;
      
        import App.Domain.LeaseEditHistoryDataMapper;
      
        import App.Domain.StateDataMapper;
      
        import App.Domain.TermUnitDataMapper;
      
        import App.Domain.UserRoleDataMapper;
      
        import App.Domain.ModuleDataMapper;
      
        import App.Domain.PermissionDataMapper;
      
        import App.Domain.PermissionAssignmentDataMapper;
      
        import App.Domain.RoleDataMapper;
      
       public class _DataMapperRegistry
       {
        

          private var m_countyDataMapper:CountyDataMapper;

          public function get County():CountyDataMapper
          {
            if(m_countyDataMapper == null )
              m_countyDataMapper = new CountyDataMapper();
              
            return m_countyDataMapper;
          }
        

          private var m_leaseDataMapper:LeaseDataMapper;

          public function get Lease():LeaseDataMapper
          {
            if(m_leaseDataMapper == null )
              m_leaseDataMapper = new LeaseDataMapper();
              
            return m_leaseDataMapper;
          }
        

          private var m_userDataMapper:UserDataMapper;

          public function get User():UserDataMapper
          {
            if(m_userDataMapper == null )
              m_userDataMapper = new UserDataMapper();
              
            return m_userDataMapper;
          }
        

          private var m_leaseEditHistoryDataMapper:LeaseEditHistoryDataMapper;

          public function get LeaseEditHistory():LeaseEditHistoryDataMapper
          {
            if(m_leaseEditHistoryDataMapper == null )
              m_leaseEditHistoryDataMapper = new LeaseEditHistoryDataMapper();
              
            return m_leaseEditHistoryDataMapper;
          }
        

          private var m_stateDataMapper:StateDataMapper;

          public function get State():StateDataMapper
          {
            if(m_stateDataMapper == null )
              m_stateDataMapper = new StateDataMapper();
              
            return m_stateDataMapper;
          }
        

          private var m_termUnitDataMapper:TermUnitDataMapper;

          public function get TermUnit():TermUnitDataMapper
          {
            if(m_termUnitDataMapper == null )
              m_termUnitDataMapper = new TermUnitDataMapper();
              
            return m_termUnitDataMapper;
          }
        

          private var m_userRoleDataMapper:UserRoleDataMapper;

          public function get UserRole():UserRoleDataMapper
          {
            if(m_userRoleDataMapper == null )
              m_userRoleDataMapper = new UserRoleDataMapper();
              
            return m_userRoleDataMapper;
          }
        

          private var m_moduleDataMapper:ModuleDataMapper;

          public function get Module():ModuleDataMapper
          {
            if(m_moduleDataMapper == null )
              m_moduleDataMapper = new ModuleDataMapper();
              
            return m_moduleDataMapper;
          }
        

          private var m_permissionDataMapper:PermissionDataMapper;

          public function get Permission():PermissionDataMapper
          {
            if(m_permissionDataMapper == null )
              m_permissionDataMapper = new PermissionDataMapper();
              
            return m_permissionDataMapper;
          }
        

          private var m_permissionAssignmentDataMapper:PermissionAssignmentDataMapper;

          public function get PermissionAssignment():PermissionAssignmentDataMapper
          {
            if(m_permissionAssignmentDataMapper == null )
              m_permissionAssignmentDataMapper = new PermissionAssignmentDataMapper();
              
            return m_permissionAssignmentDataMapper;
          }
        

          private var m_roleDataMapper:RoleDataMapper;

          public function get Role():RoleDataMapper
          {
            if(m_roleDataMapper == null )
              m_roleDataMapper = new RoleDataMapper();
              
            return m_roleDataMapper;
          }
            
        }
      }
    