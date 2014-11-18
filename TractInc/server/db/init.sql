begin tran

delete from [PermissionAssignment]
delete from [UserRole]
delete from [Permission]
delete from [Module]
delete from [ModuleType]
delete from [Role]
delete from [UserPreference]
delete from [User]
delete from [Person]

set identity_insert [ModuleType] on
    insert into [ModuleType] ( ModuleTypeId, TypeName ) 
        values ( 1, 'LoadableModule')
    insert into [ModuleType] ( ModuleTypeId, TypeName ) 
        values ( 2, 'SubstituentModule')
    insert into [ModuleType] ( ModuleTypeId, TypeName ) 
        values ( 3, 'ExternalModule')
set identity_insert [ModuleType] off

set identity_insert [Module] on
    insert into [Module] ( ModuleId, ModuleTypeId, ShortName, Description, Url) 
        values ( -2, 2, 'ExternalModule', 'External Module', 'tractIncExternalModule.html')
    insert into [Module] ( ModuleId, ModuleTypeId, ShortName, Description, Url) 
        values ( -1, 1, 'DemoModule', 'Demo Module', 'tractIncDemoModule.swf')
    insert into [Module] ( ModuleId, ModuleTypeId, ShortName, Description, Url) 
        values ( 1, 1, 'UserProfile', 'User Profile', 'tractIncUserProfile.swf')
    insert into [Module] ( ModuleId, ModuleTypeId, ShortName, Description, Url) 
        values ( 2, 1, 'UserManager', 'User Manager', 'tractIncUserManager.swf')
    insert into [Module] ( ModuleId, ModuleTypeId, ShortName, Description, Url) 
        values ( 3, 1, 'ClientManager', 'Client Management', 'tractIncClientManager.swf')
    insert into [Module] ( ModuleId, ModuleTypeId, ShortName, Description, Url) 
        values ( 4, 1, 'ContractManager', 'Contract Management', 'tractIncContractManager.swf')
    insert into [Module] ( ModuleId, ModuleTypeId, ShortName, Description, Url) 
        values ( 5, 1, 'ProjectManager', 'Project Management', 'tractIncProjectManager.swf')
    insert into [Module] ( ModuleId, ModuleTypeId, ShortName, Description, Url) 
        values ( 6, 1, 'StaffManager', 'Staff Management', 'tractIncStaffManager.swf')
    insert into [Module] ( ModuleId, ModuleTypeId, ShortName, Description, Url) 
        values ( 7, 3, 'TrueTractModule', 'True Tract', 'trueTractModule.html')
    insert into [Module] ( ModuleId, ModuleTypeId, ShortName, Description, Url) 
        values ( 8, 2, 'ClientViewModule', 'Client View', 'tractIncClientApp.html')
set identity_insert [Module] off

set identity_insert [Permission] on
    insert into [Permission] ( PermissionId, ModuleId, Code, Description ) 
        values ( -2, -2, 'ExternalModuleRun', 'Execute External Module' )
    insert into [Permission] ( PermissionId, ModuleId, Code, Description ) 
        values ( -1, -1, 'DemoModuleRun', 'Execute Demo Module' )
    insert into [Permission] ( PermissionId, ModuleId, Code, Description ) 
        values ( 1, 1, 'ProfileRun', 'Execute User Profile Module' )
    insert into [Permission] ( PermissionId, ModuleId, Code, Description ) 
        values ( 2, 1, 'ProfileView', 'View User Profile' )
    insert into [Permission] ( PermissionId, ModuleId, Code, Description ) 
        values ( 3, 1, 'ProfileEdit', 'Edit User Profile' )
    insert into [Permission] ( PermissionId, ModuleId, Code, Description ) 
        values ( 4, 2, 'UserManagerRun', 'Execute User Manager Module' )
    insert into [Permission] ( PermissionId, ModuleId, Code, Description ) 
        values ( 5, 2, 'UserManagerManageRoles', 'Manage Roles' )
    insert into [Permission] ( PermissionId, ModuleId, Code, Description ) 
        values ( 6, 2, 'UserManagerManageUsers', 'Manage Users' )
    insert into [Permission] ( PermissionId, ModuleId, Code, Description ) 
        values ( 7, 3, 'ClientManagerRun', 'Execute Client Management Module' )
    insert into [Permission] ( PermissionId, ModuleId, Code, Description ) 
        values ( 8, 4, 'ContractManagerRun', 'Execute Contract Management Module' )
    insert into [Permission] ( PermissionId, ModuleId, Code, Description ) 
        values ( 9, 5, 'ProjectManagerRun', 'Execute Project Management Module' )
    insert into [Permission] ( PermissionId, ModuleId, Code, Description ) 
        values ( 10, 5, 'ProjectManagerAssignAccount', 'Assign Account To Project' )
    insert into [Permission] ( PermissionId, ModuleId, Code, Description ) 
        values ( 11, 6, 'StaffManagerRun', 'Execute Staff Management Module' )
    insert into [Permission] ( PermissionId, ModuleId, Code, Description ) 
        values ( 12, 7, 'TrueTractRun', 'Truct Tract Module' )
    insert into [Permission] ( PermissionId, ModuleId, Code, Description ) 
        values ( 13, 8, 'ClientViewRun', 'Run Client View' )
set identity_insert [Permission] off

set identity_insert [Role] on
    insert into [Role] ( RoleId, Name ) 
        values ( 1, 'Initial Role' )
    insert into [Role] ( RoleId, Name ) 
        values ( 2, 'Superuser' )
set identity_insert [Role] off

set identity_insert [PermissionAssignment] on
    insert into [PermissionAssignment] ( PermissionAssignmentId, PermissionId, RoleId ) 
        values ( -1, -1, 1 )
    insert into [PermissionAssignment] ( PermissionAssignmentId, PermissionId, RoleId ) 
        values ( 1, 1, 1 )
    insert into [PermissionAssignment] ( PermissionAssignmentId, PermissionId, RoleId ) 
        values ( 2, 2, 1 )
    insert into [PermissionAssignment] ( PermissionAssignmentId, PermissionId, RoleId ) 
        values ( 3, 4, 2 )
    insert into [PermissionAssignment] ( PermissionAssignmentId, PermissionId, RoleId ) 
        values ( 4, 5, 2 )
    insert into [PermissionAssignment] ( PermissionAssignmentId, PermissionId, RoleId ) 
        values ( 5, 6, 2 )
set identity_insert [PermissionAssignment] off

set identity_insert [ContractStatus] on
    insert into [ContractStatus] ( ContractStatusId, StatusName ) 
        values ( 1, 'Active' )
    insert into [ContractStatus] ( ContractStatusId, StatusName ) 
        values ( 2, 'Inactive' )
set identity_insert [ContractStatus] off

set identity_insert InvoiceItemType on
    insert into InvoiceItemType (InvoiceItemTypeId, TypeName, IsCountable, IsPresetRate, IsSingle, IsAttachRequired) values(1, 'Daily Billing',  1, 1, 1, 0);
    insert into InvoiceItemType (InvoiceItemTypeId, TypeName, IsCountable, IsPresetRate, IsSingle, IsAttachRequired) values(2, 'Miles', 1, 1, 1, 0);
    insert into InvoiceItemType (InvoiceItemTypeId, TypeName, IsCountable, IsPresetRate, IsSingle, IsAttachRequired) values(3, 'Lodging',        0, 0, 1, 1);
    insert into InvoiceItemType (InvoiceItemTypeId, TypeName, IsCountable, IsPresetRate, IsSingle, IsAttachRequired) values(4, 'Meals',          0, 0, 1, 0);
    insert into InvoiceItemType (InvoiceItemTypeId, TypeName, IsCountable, IsPresetRate, IsSingle, IsAttachRequired) values(5, 'Phone',          0, 0, 1, 0);
    insert into InvoiceItemType (InvoiceItemTypeId, TypeName, IsCountable, IsPresetRate, IsSingle, IsAttachRequired) values(6, 'Copies',         0, 0, 0, 0);
    insert into InvoiceItemType (InvoiceItemTypeId, TypeName, IsCountable, IsPresetRate, IsSingle, IsAttachRequired) values(7, 'Filing Fees',    0, 0, 0, 0);
    insert into InvoiceItemType (InvoiceItemTypeId, TypeName, IsCountable, IsPresetRate, IsSingle, IsAttachRequired) values(8, 'Travel',    0, 0, 0, 0);
    insert into InvoiceItemType (InvoiceItemTypeId, TypeName, IsCountable, IsPresetRate, IsSingle, IsAttachRequired) values(9, 'Postage',    0, 0, 0, 0);
    insert into InvoiceItemType (InvoiceItemTypeId, TypeName, IsCountable, IsPresetRate, IsSingle, IsAttachRequired) values(10, 'Fax',    0, 0, 0, 0);
    insert into InvoiceItemType (InvoiceItemTypeId, TypeName, IsCountable, IsPresetRate, IsSingle, IsAttachRequired) values(11, 'Abstractor',    0, 0, 0, 0);
    insert into InvoiceItemType (InvoiceItemTypeId, TypeName, IsCountable, IsPresetRate, IsSingle, IsAttachRequired) values(12, 'Notary',    0, 0, 0, 0);
    insert into InvoiceItemType (InvoiceItemTypeId, TypeName, IsCountable, IsPresetRate, IsSingle, IsAttachRequired) values(13, 'Photo',    0, 0, 0, 0);
    insert into InvoiceItemType (InvoiceItemTypeId, TypeName, IsCountable, IsPresetRate, IsSingle, IsAttachRequired) values(14, 'Other',    0, 0, 0, 0);
    insert into InvoiceItemType (InvoiceItemTypeId, TypeName, IsCountable, IsPresetRate, IsSingle, IsAttachRequired) values(15, 'Other Fees',    0, 0, 0, 0);
    insert into InvoiceItemType (InvoiceItemTypeId, TypeName, IsCountable, IsPresetRate, IsSingle, IsAttachRequired) values(16, 'Managing',    0, 0, 0, 0);
set identity_insert InvoiceItemType off

set identity_insert [ProjectStatus] on
    insert into [ProjectStatus] ( ProjectStatusId, StatusName ) 
        values ( 1, 'Active' )
    insert into [ProjectStatus] ( ProjectStatusId, StatusName ) 
        values ( 2, 'Inactive' )
    insert into [ProjectStatus] ( ProjectStatusId, StatusName ) 
        values ( 3, 'Complete' )
set identity_insert [ProjectStatus] off

set identity_insert [AccountType] on
    insert into [AccountType] ( AccountTypeId, TypeName ) 
        values ( 1, 'Active' )
    insert into [AccountType] ( AccountTypeId, TypeName ) 
        values ( 2, 'Passive' )
    insert into [AccountType] ( AccountTypeId, TypeName ) 
        values ( 3, 'Active-Passive' )
set identity_insert [AccountType] off

set identity_insert BillItemType on
    insert into BillItemType (BillItemTypeId, InvoiceItemTypeId, TypeName) values(1, 1, 'Daily Billing');
    insert into BillItemType (BillItemTypeId, InvoiceItemTypeId, TypeName) values(2, 2, 'Miles');
    insert into BillItemType (BillItemTypeId, InvoiceItemTypeId, TypeName) values(3, 3, 'Lodging');
    insert into BillItemType (BillItemTypeId, InvoiceItemTypeId, TypeName) values(4, 4, 'Meals');
    insert into BillItemType (BillItemTypeId, InvoiceItemTypeId, TypeName) values(5, 5, 'Phone');
    insert into BillItemType (BillItemTypeId, InvoiceItemTypeId, TypeName) values(6, 6, 'Copies');
    insert into BillItemType (BillItemTypeId, InvoiceItemTypeId, TypeName) values(7, 7, 'Filing Fees');
    insert into BillItemType (BillItemTypeId, InvoiceItemTypeId, TypeName) values(8, 8, 'Travel');
    insert into BillItemType (BillItemTypeId, InvoiceItemTypeId, TypeName) values(9, 9, 'Postage');
    insert into BillItemType (BillItemTypeId, InvoiceItemTypeId, TypeName) values(10, 10, 'Fax');
    insert into BillItemType (BillItemTypeId, InvoiceItemTypeId, TypeName) values(11, 11, 'Abstractor');
    insert into BillItemType (BillItemTypeId, InvoiceItemTypeId, TypeName) values(12, 12, 'Notary');
    insert into BillItemType (BillItemTypeId, InvoiceItemTypeId, TypeName) values(13, 13, 'Photo');
    insert into BillItemType (BillItemTypeId, InvoiceItemTypeId, TypeName) values(14, 14, 'Other');
    insert into BillItemType (BillItemTypeId, InvoiceItemTypeId, TypeName) values(15, 15, 'Other Fees');
set identity_insert BillItemType off

set identity_insert [AssetType] on
    insert into [AssetType] ( AssetTypeId, TypeName ) 
        values ( 1, 'Landman' )
    insert into [AssetType] ( AssetTypeId, TypeName ) 
        values ( 2, 'Digger' )
set identity_insert [AssetType] off


commit