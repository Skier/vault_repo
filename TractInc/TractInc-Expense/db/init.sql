delete from [Role];
delete from Permission;
delete from Module;
delete from SubAfeStatus;
delete from AfeStatus;
delete from BillStatus;
delete from BillItemStatus;
delete from BillItemType;
go

set identity_insert InvoiceItemType on
insert into InvoiceItemType (InvoiceItemTypeId, [Name], IsCountable, IsPresetRate, IsSingle, Deleted) values(1, 'Daily Billing', 1, 1, 1, 0);
insert into InvoiceItemType (InvoiceItemTypeId, [Name], IsCountable, IsPresetRate, IsSingle, Deleted) values(2, 'Miles',         1, 1, 1, 0);
insert into InvoiceItemType (InvoiceItemTypeId, [Name], IsCountable, IsPresetRate, IsSingle, Deleted) values(3, 'Lodging',       0, 0, 1, 0);
insert into InvoiceItemType (InvoiceItemTypeId, [Name], IsCountable, IsPresetRate, IsSingle, Deleted) values(4, 'Meals',         0, 0, 1, 0);
insert into InvoiceItemType (InvoiceItemTypeId, [Name], IsCountable, IsPresetRate, IsSingle, Deleted) values(5, 'Phone',         0, 0, 1, 0);
insert into InvoiceItemType (InvoiceItemTypeId, [Name], IsCountable, IsPresetRate, IsSingle, Deleted) values(6, 'Copies',        0, 0, 0, 0);
insert into InvoiceItemType (InvoiceItemTypeId, [Name], IsCountable, IsPresetRate, IsSingle, Deleted) values(7, 'Filing Fees',   0, 0, 0, 0);
insert into InvoiceItemType (InvoiceItemTypeId, [Name], IsCountable, IsPresetRate, IsSingle, Deleted) values(8, 'Travel',        0, 0, 0, 0);
insert into InvoiceItemType (InvoiceItemTypeId, [Name], IsCountable, IsPresetRate, IsSingle, Deleted) values(9, 'Postage',       0, 0, 0, 0);
insert into InvoiceItemType (InvoiceItemTypeId, [Name], IsCountable, IsPresetRate, IsSingle, Deleted) values(10, 'Fax',          0, 0, 0, 0);
insert into InvoiceItemType (InvoiceItemTypeId, [Name], IsCountable, IsPresetRate, IsSingle, Deleted) values(11, 'Abstractor',   0, 0, 0, 0);
insert into InvoiceItemType (InvoiceItemTypeId, [Name], IsCountable, IsPresetRate, IsSingle, Deleted) values(12, 'Notary',       0, 0, 0, 0);
insert into InvoiceItemType (InvoiceItemTypeId, [Name], IsCountable, IsPresetRate, IsSingle, Deleted) values(13, 'Photo',        0, 0, 0, 0);
insert into InvoiceItemType (InvoiceItemTypeId, [Name], IsCountable, IsPresetRate, IsSingle, Deleted) values(14, 'Other',        0, 0, 0, 0);
insert into InvoiceItemType (InvoiceItemTypeId, [Name], IsCountable, IsPresetRate, IsSingle, Deleted) values(15, 'Other Fees',   0, 0, 0, 0);
insert into InvoiceItemType (InvoiceItemTypeId, [Name], IsCountable, IsPresetRate, IsSingle, Deleted) values(16, 'Managing',     0, 0, 0, 0);
set identity_insert InvoiceItemType off
go

set identity_insert BillItemType on
insert into BillItemType (BillItemTypeId, InvoiceItemTypeId, [Name], IsCountable, IsPresetRate, IsSingle, IsAttachRequired, Deleted) values(1, 1, 'Daily Billing',  1, 1, 1, 0, 0);
insert into BillItemType (BillItemTypeId, InvoiceItemTypeId, [Name], IsCountable, IsPresetRate, IsSingle, IsAttachRequired, Deleted) values(2, 2, 'Miles',          1, 1, 1, 0, 0);
insert into BillItemType (BillItemTypeId, InvoiceItemTypeId, [Name], IsCountable, IsPresetRate, IsSingle, IsAttachRequired, Deleted) values(3, 3, 'Lodging',        0, 0, 1, 1, 0);
insert into BillItemType (BillItemTypeId, InvoiceItemTypeId, [Name], IsCountable, IsPresetRate, IsSingle, IsAttachRequired, Deleted) values(4, 4, 'Meals',          0, 0, 1, 1, 0);
insert into BillItemType (BillItemTypeId, InvoiceItemTypeId, [Name], IsCountable, IsPresetRate, IsSingle, IsAttachRequired, Deleted) values(5, 5, 'Phone',          0, 0, 1, 1, 0);
insert into BillItemType (BillItemTypeId, InvoiceItemTypeId, [Name], IsCountable, IsPresetRate, IsSingle, IsAttachRequired, Deleted) values(6, 6, 'Copies',         0, 0, 0, 1, 0);
insert into BillItemType (BillItemTypeId, InvoiceItemTypeId, [Name], IsCountable, IsPresetRate, IsSingle, IsAttachRequired, Deleted) values(7, 7, 'Filing Fees',    0, 0, 0, 1, 0);
insert into BillItemType (BillItemTypeId, InvoiceItemTypeId, [Name], IsCountable, IsPresetRate, IsSingle, IsAttachRequired, Deleted) values(8, 8, 'Travel',         0, 0, 0, 1, 0);
insert into BillItemType (BillItemTypeId, InvoiceItemTypeId, [Name], IsCountable, IsPresetRate, IsSingle, IsAttachRequired, Deleted) values(9, 9, 'Postage',        0, 0, 0, 1, 0);
insert into BillItemType (BillItemTypeId, InvoiceItemTypeId, [Name], IsCountable, IsPresetRate, IsSingle, IsAttachRequired, Deleted) values(10, 10, 'Fax',          0, 0, 0, 1, 0);
insert into BillItemType (BillItemTypeId, InvoiceItemTypeId, [Name], IsCountable, IsPresetRate, IsSingle, IsAttachRequired, Deleted) values(11, 11, 'Abstractor',   0, 0, 0, 1, 0);
insert into BillItemType (BillItemTypeId, InvoiceItemTypeId, [Name], IsCountable, IsPresetRate, IsSingle, IsAttachRequired, Deleted) values(12, 12, 'Notary',       0, 0, 0, 1, 0);
insert into BillItemType (BillItemTypeId, InvoiceItemTypeId, [Name], IsCountable, IsPresetRate, IsSingle, IsAttachRequired, Deleted) values(13, 13, 'Photo',        0, 0, 0, 1, 0);
insert into BillItemType (BillItemTypeId, InvoiceItemTypeId, [Name], IsCountable, IsPresetRate, IsSingle, IsAttachRequired, Deleted) values(14, 14, 'Other',        0, 0, 0, 1, 0);
insert into BillItemType (BillItemTypeId, InvoiceItemTypeId, [Name], IsCountable, IsPresetRate, IsSingle, IsAttachRequired, Deleted) values(15, 15, 'Other Fees',   0, 0, 0, 1, 0);
set identity_insert BillItemType off
go

insert into BillStatus values ('NEW');
insert into BillStatus values ('SUBMITTED');
insert into BillStatus values ('REJECTED');
insert into BillStatus values ('CHANGED');
insert into BillStatus values ('CORRECTED');
insert into BillStatus values ('APPROVED');
insert into BillStatus values ('CONFIRMED');
insert into BillStatus values ('DECLINED');
insert into BillStatus values ('SENT');
go

insert into BillItemStatus (Status) values ('NEW');
insert into BillItemStatus (Status) values ('SUBMITTED');
insert into BillItemStatus (Status) values ('REJECTED');
insert into BillItemStatus (Status) values ('CHANGED');
insert into BillItemStatus (Status) values ('CORRECTED');
insert into BillItemStatus (Status) values ('APPROVED');
insert into BillItemStatus (Status) values ('CONFIRMED');
insert into BillItemStatus (Status) values ('DECLINED');
insert into BillItemStatus (Status) values ('SENT');
go

insert into AfeStatus (AFEStatus) values ('ISSUED');
insert into AfeStatus (AFEStatus) values ('EXPIRED');
insert into AfeStatus (AFEStatus) values ('LOCKED');
insert into AfeStatus (AFEStatus) values ('UNLOCKED');
go

insert into SubAfeStatus (SubAFEStatus) values ('ISSUED');
insert into SubAfeStatus (SubAFEStatus) values ('EXPIRED');
insert into SubAfeStatus (SubAFEStatus) values ('LOCKED');
insert into SubAfeStatus (SubAFEStatus) values ('UNLOCKED');
go

insert into InvoiceStatus (Status) values ('NEW');
insert into InvoiceStatus (Status) values ('SUBMITTED');
insert into InvoiceStatus (Status) values ('PAID');
insert into InvoiceStatus (Status) values ('VOID');
go

insert into InvoiceItemStatus (Status) values ('NEW');
insert into InvoiceItemStatus (Status) values ('SUBMITTED');
insert into InvoiceItemStatus (Status) values ('PAID');
insert into InvoiceItemStatus (Status) values ('VOID');
go

set identity_insert [Module] on
insert into [Module] ( ModuleId, Description ) values (1, 'Expense Module')
set identity_insert [Module] off
go

set identity_insert [Permission] on
insert into [Permission] ( PermissionId, ModuleId, Description, Code ) values ( 1, 1, 'Log Bill Items', 'LOG_BILL_ITEMS' )
insert into [Permission] ( PermissionId, ModuleId, Description, Code ) values ( 2, 1, 'Manage Bills', 'MANAGE_BILLS' )
set identity_insert [Permission] off
go

set identity_insert [Role] on
insert into [Role] ( RoleId, Name ) values ( 1, 'Manager' )
insert into [Role] ( RoleId, Name ) values ( 2, 'Landman' )
insert into [Role] ( RoleId, Name ) values ( 3, 'Crew Chief' )
set identity_insert [Role] off
go

