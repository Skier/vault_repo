drop table TT_PermissionAssignment
drop table TT_Permission
drop table TT_Module
drop table TT_UserRole
drop table TT_Role
drop table TT_ProjectStatus
go

insert into [Company](
    CompanyName
) values (
    'Our Company'
)    
go

set identity_insert [Client] on
    insert into [Client] (
ClientId,
ClientName,
ClientAddress
    ) select 
[ClientId],
[Name],
null
    from [TT_Client]
set identity_insert [Client] off
go

set identity_insert [Person] on
    insert into [Person] (
PersonId,
ClientId,
CompanyId,
FirstName,
MiddleName,
LastName,
PhoneNumber,
Email,
SSN
    ) select 
[UserId],
[ClientId],
null, -- no company
[FirstName],
'',
[LastName],
[PhoneNumber],
[Email],
''
    from [TT_User]
set identity_insert [Person] off
go

set identity_insert [User] on
    insert into [User] (
UserId,
PersonId,
Login,
Password,
IsActive,
HackingAttempts
    ) select 
[UserId],
[UserId],
[Login],
[Password],
[IsActive],
[HackingAttempts]
    from [TT_User]
set identity_insert [User] off
go

set identity_insert [UserPreference] on
    insert into [UserPreference] (
UserPereferenceId,
UserId,
DefaultSite,
NewTracts
    ) select 
[UserId],
[UserId],
[DefaultSite],
[NewTracts]
    from [TT_User]
set identity_insert [UserPreference] off
go


CREATE VIEW [TTV_User]
(
[UserId],
[Login],
[FirstName],
[LastName],
[PhoneNumber],
[Password],
[Email],
[IsActive],
[HackingAttempts],
[NewTracts],
[DefaultSite],
[AssetId],
[ClientId]
) as
select
u.UserId,
u.Login,
p.FirstName,
p.LastName,
p.PhoneNumber,
u.Password,
p.Email,
u.IsActive,
u.HackingAttempts,
up.NewTracts,
up.DefaultSite,
null,
p.ClientId
from [User] u
    inner join Person p on u.PersonId = p.PersonId
    inner join UserPreference up on u.UserId = up.UserId
go

set identity_insert [Asset] on
    insert into [Asset] (
AssetId, 
AssetTypeId,
CompanyId,
PersonId,
AssetName
    ) select 
[AssetId],
1,
1,
(select TT_User.UserId from TT_User where TT_User.AssetId=AssetId),
[AssetName]
    from [TT_Asset]
set identity_insert [Asset] off
go

alter table TT_Project drop CONSTRAINT [FK_TT_Project_User]
ALTER TABLE [TT_Project] DROP CONSTRAINT [FK_TT_Project_Client]
go

ALTER TABLE [TT_Tract] DROP CONSTRAINT [FK_TT_Tract_User]
ALTER TABLE [TT_Tract] ADD
CONSTRAINT [FK_TT_Tract_User] FOREIGN KEY ([CreatedBy]) REFERENCES [User] ([UserId])

ALTER TABLE [TT_Document] DROP CONSTRAINT [FK_TT_Document_User]
ALTER TABLE [TT_Document] ADD
CONSTRAINT [FK_Document_User] FOREIGN KEY ([CreatedBy]) REFERENCES [User] ([UserId])

ALTER TABLE [TT_Document] DROP CONSTRAINT [FK_TT_Document_User1]
ALTER TABLE [TT_Document] ADD
CONSTRAINT [FK_Document_User1] FOREIGN KEY ([LockedBy]) REFERENCES [User] ([UserId])

ALTER TABLE [TT_File] DROP CONSTRAINT [FK_TT_File_User]
ALTER TABLE [TT_File] ADD
CONSTRAINT [FK_File_User] FOREIGN KEY ([CreatedBy]) REFERENCES [User] ([UserId])

ALTER TABLE [TT_GroupUser] DROP CONSTRAINT [FK_TT_UserGroup_User]
ALTER TABLE [TT_GroupUser] ADD
CONSTRAINT [FK_UserGroup_User] FOREIGN KEY ([UserId]) REFERENCES [User] ([UserId]) ON DELETE CASCADE

ALTER TABLE [TT_LeaseEditHistory] DROP CONSTRAINT [FK_TT_LeaseEditHistory_User]
ALTER TABLE [TT_LeaseEditHistory] ADD
CONSTRAINT [FK_LeaseEditHistory_User] FOREIGN KEY ([UserId]) REFERENCES [User] ([UserId]) ON DELETE CASCADE

drop table TT_User
go

ALTER TABLE [TT_AssetAssignment] DROP CONSTRAINT [FK_TT_AssetAssignment_Asset]
ALTER TABLE [TT_AssetAssignment] ADD
CONSTRAINT [FK_AssetAssignment_Asset] FOREIGN KEY ([AssetId]) REFERENCES [Asset] ([AssetId])

drop table TT_Asset
go

ALTER TABLE [TT_ClientAccount] DROP CONSTRAINT [FK_TT_ClientAccount_Client]
ALTER TABLE [TT_ClientAccount] ADD
CONSTRAINT [FK_ClientAccount_Client] FOREIGN KEY ([ClientId]) REFERENCES [Client] ([ClientId]) ON DELETE CASCADE

drop table TT_Client
go

    insert into [State] (
[StateId],
[Name],
[StateFips],
[StateAbbr]
    ) select 
[StateId],
[Name],
[StateFips],
[StateAbbr]
    from [TT_State]
go

set identity_insert [County] on
    insert into [County] (
[CountyId],
[Name],
[StateId],
[StateName],
[StateFips],
[CountyFips],
[Fips]
    ) select 
[CountyId],
[Name],
[StateId],
[StateName],
[StateFips],
[CountyFips],
[Fips]
    from TT_County
set identity_insert [County] off
go

ALTER TABLE [TT_DocumentReference] DROP CONSTRAINT [FK_TT_DocumentReference_County]
ALTER TABLE [TT_DocumentReference] ADD
CONSTRAINT [FK_DocumentReference_County] FOREIGN KEY ([County]) REFERENCES [County] ([CountyId])

drop table TT_County
go

ALTER TABLE [TT_Address] DROP CONSTRAINT [FK_TT_Address_State]
ALTER TABLE [TT_Address] ADD
CONSTRAINT [FK_Address_State] FOREIGN KEY ([State]) REFERENCES [State] ([StateId])
ALTER TABLE [TT_DocumentReference] DROP CONSTRAINT [FK_TT_DocumentReference_State]
ALTER TABLE [TT_DocumentReference] ADD
CONSTRAINT [FK_DocumentReference_State] FOREIGN KEY ([State]) REFERENCES [State] ([StateId])

drop table TT_State
go

set identity_insert [Contract] on
    insert into [Contract] (
ContractId,
ClientId,
CompanyId,
ContractStatusId,
ContractName,
StartDate,
EndDate
    ) select 
[ProjectId],
[ClientId],
1, -- our company
1, -- active
[Name],
getdate(),
getdate()
    from TT_Project
set identity_insert [Contract] off

set identity_insert [Project] on
    insert into [Project] (
ProjectId,
ContractId,
AccountId,
ProjectStatusId,
ProjectName,
ShortName,
Description,
CreatedBy
    ) select 
[ProjectId],
[ProjectId],
null,
1, --active
[Name],
[ShortName],
[Description],
isnull([ChangedBy], '')
    from TT_Project
set identity_insert [Project] off

ALTER TABLE [TT_AssetAssignment] DROP CONSTRAINT [FK_TT_AssetAssignment_Project]
ALTER TABLE [TT_AssetAssignment] ADD
CONSTRAINT [FK_AssetAssignment_Project] FOREIGN KEY ([ProjectId]) REFERENCES [Project] ([ProjectId]) ON DELETE CASCADE
ALTER TABLE [TT_ProjectAttachment] DROP CONSTRAINT [FK_TT_ProjectAttachment_Project]
ALTER TABLE [TT_ProjectAttachment] ADD
CONSTRAINT [FK_ProjectAttachment_Project] FOREIGN KEY ([ProjectId]) REFERENCES [Project] ([ProjectId]) ON DELETE CASCADE
ALTER TABLE [TT_ProjectTab] DROP CONSTRAINT [FK_TT_ProjectTab_Project]
ALTER TABLE [TT_ProjectTab] ADD
CONSTRAINT [FK_ProjectTab_Project] FOREIGN KEY ([ProjectId]) REFERENCES [Project] ([ProjectId]) ON DELETE CASCADE

drop table TT_Project
go
drop table TT_ProjectStatus
go

insert into UserRole (
    UserId,
    RoleId
) select
    UserId,
    1
from [User]
go

---
set identity_insert [Team] on
    insert into [Team] (
TeamId, 
CompanyId,
ParentTeamId,
TeamName
    ) select 
ProjectId,
1, -- our company
null, -- no parent
ProjectName + ' Team'
    from Project
set identity_insert [Team] off

set identity_insert [TeamMember] on
    insert into [TeamMember] (
TeamMemberId,
TeamId,
AssetId,
StartDate,
EndDate
    ) select 
AssetAssignmentId,
ProjectId,
AssetId,
StartDate,
EndDate
    from TT_AssetAssignment
set identity_insert [TeamMember] off

set identity_insert [TeamAssignment] on
    insert into [TeamAssignment] (
TeamAssignmentId, 
TeamId,
ProjectId,
LeadAssetId,
StartDate,
EndDate
    ) select 
AssetAssignmentId,
ProjectId,
ProjectId,
AssetId,
StartDate,
EndDate
    from TT_AssetAssignment
set identity_insert [TeamAssignment] off

drop table TT_AssetAssignment

set identity_insert [Account] on
    insert into [Account] (
AccountId,
AccountName,
ParentAccountId,
AccountTypeId,
CompanyId,
ClientId
    ) select 
ClientAccountId,
Code,
null, 
1, --active
1, --our company,
ClientId
    from TT_ClientAccount
set identity_insert [Account] off

drop table TT_ClientAccount
