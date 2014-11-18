create table [TT_TractTextObject]
(
[TractTextObjectId] [int] NOT NULL IDENTITY(1, 1),
[TractId] [int] NOT NULL,
[Text] [nvarchar] (4000) NOT NULL,
[Easting] [numeric] (18, 2) NOT NULL,
[Northing] [numeric] (18, 2) NOT NULL,
[Rotation] [numeric] (18, 2) NOT NULL
)

set identity_insert [TT_TractTextObject] on
    insert into [TT_TractTextObject] ( TractTExtObjectId, TractId, [Text], Easting, Northing, Rotation )
    select TractTExtObjectId, TractId, [Text], Easting, Northing, Rotation 
        from TractTextObject
set identity_insert [TT_TractTextObject] off

ALTER TABLE [TT_TractTextObject] ADD CONSTRAINT [PK_TT_TractTextObject] PRIMARY KEY CLUSTERED  ([TractTextObjectId])

CREATE TABLE [TT_TractCall]
(
[TractCallId] [int] NOT NULL IDENTITY(1, 1),
[TractId] [int] NOT NULL,
[Type] [varchar] (15) NOT NULL,
[Params] [varchar] (200) NOT NULL,
[Order] [int] NOT NULL,
[CreatedByMouse] [bit] NOT NULL
)

set identity_insert [TT_TractCall] on
    insert into [TT_TractCall] ( TractCallId, TractId, [Type], Params, [Order], CreatedByMouse )
    select  TractCallId, TractId, [Type], Params, [Order], CreatedByMouse  
        from TractCall
set identity_insert [TT_TractCall] off

ALTER TABLE [TT_TractCall] ADD CONSTRAINT [PK_TT_TractCall] PRIMARY KEY CLUSTERED  ([TractCallId])

CREATE TABLE [TT_Tract]
(
[TractId] [int] NOT NULL IDENTITY(1, 1),
[Easting] [int] NOT NULL,
[Northing] [int] NOT NULL,
[RefName] [varchar] (100)  NOT NULL,
[CreatedBy] [int] NOT NULL,
[DocID] [int] NULL,
[CalledAC] [numeric] (18, 2) NOT NULL,
[UnitId] [int] NOT NULL,
[DocIdUnique] AS (isnull([DocId],(-1))),
[UniqueId] [varchar] (50)  NULL
)

set identity_insert [TT_Tract] on
    insert into [TT_Tract] ( TractId, Easting, Northing, RefName, CreatedBy, DocID, CalledAC, UnitId, 
        UniqueId )
    select TractId, Easting, Northing, RefName, CreatedBy, DocID, CalledAC, UnitId, 
        UniqueId
        from Tract
set identity_insert [TT_Tract] off

ALTER TABLE [TT_Tract] ADD CONSTRAINT [PK_TT_Tract] PRIMARY KEY CLUSTERED  ([TractId])

CREATE TABLE [TT_DocumentReference]
(
[DocumentReferenceId] [int] NOT NULL IDENTITY(1, 1),
[DocumentId] [int] NOT NULL,
[ReferenceId] [int] NULL,
[Description] [varchar] (350) NULL,
[State] [int] NULL,
[County] [int] NULL,
[DocTypeId] [int] NULL,
[DocumentNo] [varchar] (50) NULL,
[Volume] [varchar] (50) NULL,
[Page] [varchar] (50) NULL
)

set identity_insert [TT_DocumentReference] on
    insert into [TT_DocumentReference] (
[DocumentReferenceId],
[DocumentId],
[ReferenceId],
[Description],
[State],
[County],
[DocTypeId],
[DocumentNo],
[Volume],
[Page]
    ) select 
[DocumentReferenceId],
[DocumentId],
[ReferenceId],
[Description],
[State],
[County],
[DocTypeId],
[DocumentNo],
[Volume],
[Page]
    from DocumentReference
set identity_insert [TT_DocumentReference] off

ALTER TABLE [TT_DocumentReference] ADD CONSTRAINT [PK_TT_DocumentReference] PRIMARY KEY CLUSTERED  ([DocumentReferenceId])

CREATE TABLE [TT_DocumentAttachment]
(
[DocumentAttachmentId] [int] NOT NULL IDENTITY(1, 1),
[DocumentAttachmentTypeId] [int] NOT NULL,
[DocumentId] [int] NOT NULL,
[FileId] [int] NOT NULL
)

set identity_insert [TT_DocumentAttachment] on
    insert into [TT_DocumentAttachment] (
[DocumentAttachmentId],
[DocumentAttachmentTypeId],
[DocumentId],
[FileId]
    ) select 
[DocumentAttachmentId],
[DocumentAttachmentTypeId],
[DocumentId],
[FileId]
    from DocumentAttachment
set identity_insert [TT_DocumentAttachment] off

ALTER TABLE [TT_DocumentAttachment] ADD CONSTRAINT [PK_TT_DocumentAttachment] PRIMARY KEY CLUSTERED  ([DocumentAttachmentId])

CREATE TABLE [TT_Participant]
(
[ParticipantID] [int] NOT NULL IDENTITY(1, 1),
[DocID] [int] NULL,
[AsNamed] [varchar] (350) NULL,
[FirstName] [varchar] (50) NULL,
[MiddleName] [varchar] (50) NULL,
[LastName] [varchar] (50) NULL,
[IsSeller] [bit] NOT NULL
)

set identity_insert [TT_Participant] on
    insert into [TT_Participant] (
[ParticipantID],
[DocID],
[AsNamed],
[FirstName],
[MiddleName],
[LastName],
[IsSeller]
    ) select 
[ParticipantID],
[DocID],
[AsNamed],
[FirstName],
[MiddleName],
[LastName],
[IsSeller]
    from Participant
set identity_insert [TT_Participant] off

ALTER TABLE [TT_Participant] ADD CONSTRAINT [PK_TT_Participant] PRIMARY KEY CLUSTERED  ([ParticipantID])

CREATE TABLE [TT_Document]
(
[DocID] [int] NOT NULL IDENTITY(1, 1),
[IsPublic] [bit] NOT NULL,
[DocTypeId] [int] NOT NULL,
[Volume] [varchar] (50) NULL,
[Page] [varchar] (50) NULL,
[DocumentNo] [varchar] (50) NULL,
[County] [int] NOT NULL,
[State] [int] NOT NULL,
[ResearchNote] [varchar] (350) NULL,
[ImageLink] [varchar] (350)  NULL,
[DocPlace] AS (case when [documentNo] IS NULL OR len([documentNo])=(0) then (('Vol: '+[Volume])+', Pg:')+[Page] else 'DocNo: '+[documentNo] end),
[IsActive] [bit] NOT NULL CONSTRAINT [DF__TT_Document__IsActive] DEFAULT ((1)),
[DocBranchUid] [uniqueidentifier] NOT NULL CONSTRAINT [DF__TT_Document__DocBranch] DEFAULT (newid()),
[Filed] [datetime] NULL,
[Signed] [datetime] NULL,
[Created] [datetime] NOT NULL,
[CreatedBy] [int] NOT NULL,
[PreviousVersion] [int] NULL,
[DocumentStatusId] [int] NULL,
[LockedBy] [int] NULL
)

set identity_insert [TT_Document] on
    insert into [TT_Document] (
[DocID],
[IsPublic],
[DocTypeId],
[Volume],
[Page],
[DocumentNo],
[County],
[State],
[ResearchNote],
[ImageLink],
[IsActive],
[DocBranchUid],
[Filed],
[Signed],
[Created],
[CreatedBy],
[PreviousVersion],
[DocumentStatusId],
[LockedBy]
    ) select 
[DocID],
[IsPublic],
[DocTypeId],
[Volume],
[Page],
[DocumentNo],
[County],
[State],
[ResearchNote],
[ImageLink],
[IsActive],
[DocBranchUid],
[Filed],
[Signed],
[Created],
[CreatedBy],
[PreviousVersion],
[DocumentStatusId],
[LockedBy]
    from Document
set identity_insert [TT_Document] off

ALTER TABLE [TT_Document] ADD CONSTRAINT [PK_TT_Document] PRIMARY KEY CLUSTERED  ([DocID])

CREATE TABLE [TT_ProjectTab]
(
[ProjectTabId] [int] NOT NULL IDENTITY(1, 1),
[ProjectId] [int] NOT NULL,
[Name] [varchar] (50) NOT NULL,
[Label] [varchar] (50) NULL,
[TabOrder] [int] NOT NULL CONSTRAINT [DF_TT_ProjectTab_TabOrder] DEFAULT ((0))
)

set identity_insert [TT_ProjectTab] on
    insert into [TT_ProjectTab] (
[ProjectTabId],
[ProjectId],
[Name],
[Label],
[TabOrder]
    ) select 
[ProjectTabId],
[ProjectId],
[Name],
[Label],
[TabOrder]
    from ProjectTab
set identity_insert [TT_ProjectTab] off

ALTER TABLE [TT_ProjectTab] ADD CONSTRAINT [PK__TT_ProjectTab] PRIMARY KEY CLUSTERED  ([ProjectTabId])

CREATE TABLE [TT_User]
(
[UserId] [int] NOT NULL IDENTITY(1, 1),
[Login] [varchar] (50) NOT NULL,
[FirstName] [varchar] (50) NOT NULL,
[LastName] [varchar] (50) NOT NULL,
[PhoneNumber] [varchar] (50) NOT NULL,
[Password] [varchar] (50) NOT NULL,
[Email] [varchar] (50)  NOT NULL,
[IsActive] [bit] NOT NULL CONSTRAINT [DF__TT_User__IsActive] DEFAULT ((1)),
[HackingAttempts] [int] NOT NULL CONSTRAINT [DF__TT_User__HackingAtt] DEFAULT ((0)),
[NewTracts] [int] NOT NULL CONSTRAINT [DF__TT_User__NewTracts] DEFAULT ((0)),
[DefaultSite] [varchar] (250) NOT NULL CONSTRAINT [DF_TT_User_DefaultSite] DEFAULT ('Barnett Shale'),
[AssetId] [int] NULL,
[ClientId] [int] NULL
)

set identity_insert [TT_User] on
    insert into [TT_User] (
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
    ) select 
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
    from [User]
set identity_insert [TT_User] off

ALTER TABLE [TT_User] ADD CONSTRAINT [PK_TT_User] PRIMARY KEY CLUSTERED  ([UserId])

CREATE TABLE [TT_State]
(
[StateId] [int] NOT NULL,
[Name] [varchar] (50) NULL,
[StateFips] [varchar] (2) NULL,
[StateAbbr] [varchar] (2) NULL
)

    insert into [TT_State] (
[StateId],
[Name],
[StateFips],
[StateAbbr]
    ) select 
[StateId],
[Name],
[StateFips],
[StateAbbr]
    from [State]

ALTER TABLE [TT_State] ADD CONSTRAINT [PK_TT_State] PRIMARY KEY CLUSTERED  ([StateId])

CREATE TABLE [TT_Address]
(
[AddressId] [int] NOT NULL IDENTITY(1, 1),
[Address1] [varchar] (50) NOT NULL,
[Address2] [varchar] (50) NULL,
[City] [varchar] (50) NULL,
[State] [int] NULL,
[Zip] [varchar] (10) NULL
)

set identity_insert [TT_Address] on
    insert into [TT_Address] (
[AddressId],
[Address1],
[Address2],
[City],
[State],
[Zip]
    ) select 
[AddressId],
[Address1],
[Address2],
[City],
[State],
[Zip]
    from Address
set identity_insert [TT_Address] off

ALTER TABLE [TT_Address] ADD CONSTRAINT [PK_TT_Address] PRIMARY KEY CLUSTERED  ([AddressId])

CREATE TABLE [TT_Asset]
(
[AssetId] [int] NOT NULL IDENTITY(1, 1),
[AssetName] [varchar] (250) NOT NULL
)

set identity_insert [TT_Asset] on
    insert into [TT_Asset] (
[AssetId],
[AssetName]
    ) select 
[AssetId],
[AssetName]
    from Asset
set identity_insert [TT_Asset] off

ALTER TABLE [TT_Asset] ADD CONSTRAINT [PK__TT_Asset] PRIMARY KEY CLUSTERED  ([AssetId])

CREATE TABLE [TT_AssetAssignment]
(
[AssetAssignmentId] [int] NOT NULL IDENTITY(1, 1),
[AssetId] [int] NOT NULL,
[ProjectId] [int] NOT NULL,
[StartDate] [datetime] NOT NULL,
[EndDate] [datetime] NULL
)

set identity_insert [TT_AssetAssignment] on
    insert into [TT_AssetAssignment] (
[AssetAssignmentId],
[AssetId],
[ProjectId],
[StartDate],
[EndDate]
    ) select 
[AssetAssignmentId],
[AssetId],
[ProjectId],
[StartDate],
[EndDate]
    from AssetAssignment
set identity_insert [TT_AssetAssignment] off

ALTER TABLE [TT_AssetAssignment] ADD CONSTRAINT [PK__TT_AssetAssignment] PRIMARY KEY CLUSTERED  ([AssetAssignmentId])

CREATE TABLE [TT_Project]
(
[ProjectId] [int] NOT NULL IDENTITY(1, 1),
[Name] [varchar] (50) NOT NULL,
[ShortName] [varchar] (25) NOT NULL,
[ClientId] [int] NOT NULL,
[ClientAccountId] [int] NOT NULL,
[Description] [varchar] (250) NOT NULL,
[Status] [varchar] (50) NOT NULL,
[Changed] [datetime] NULL,
[ChangedBy] [int] NULL
)

set identity_insert [TT_Project] on
    insert into [TT_Project] (
[ProjectId],
[Name],
[ShortName],
[ClientId],
[ClientAccountId],
[Description],
[Status],
[Changed],
[ChangedBy]
    ) select 
[ProjectId],
[Name],
[ShortName],
[ClientId],
[ClientAccountId],
[Description],
[Status],
[Changed],
[ChangedBy]
    from Project
set identity_insert [TT_Project] off

ALTER TABLE [TT_Project] ADD CONSTRAINT [PK__TT_Project] PRIMARY KEY CLUSTERED  ([ProjectId])

CREATE TABLE [TT_Client]
(
[ClientId] [int] NOT NULL IDENTITY(1, 1),
[Name] [varchar] (50) NOT NULL
)

set identity_insert [TT_Client] on
    insert into [TT_Client] (
[ClientId],
[Name]
    ) select 
[ClientId],
[Name]
    from Client
set identity_insert [TT_Client] off

ALTER TABLE [TT_Client] ADD CONSTRAINT [PK__TT_Client] PRIMARY KEY CLUSTERED  ([ClientId])

CREATE TABLE [TT_ClientAccount]
(
[ClientAccountId] [int] NOT NULL IDENTITY(1, 1),
[ClientId] [int] NOT NULL,
[Code] [varchar] (50) NOT NULL,
[Name] [varchar] (50) NOT NULL
)

set identity_insert [TT_ClientAccount] on
    insert into [TT_ClientAccount] (
[ClientAccountId],
[ClientId],
[Code],
[Name]
    ) select 
[ClientAccountId],
[ClientId],
[Code],
[Name]
    from ClientAccount
set identity_insert [TT_ClientAccount] off

ALTER TABLE [TT_ClientAccount] ADD CONSTRAINT [PK__TT_ClientAccount] PRIMARY KEY CLUSTERED  ([ClientAccountId])

CREATE TABLE [TT_County]
(
[CountyId] [int] NOT NULL IDENTITY(1, 1),
[Name] [varchar] (50)  NULL,
[StateId] [int] NOT NULL,
[StateName] [varchar] (50) NULL,
[StateFips] [varchar] (2) NULL,
[CountyFips] [varchar] (3) NULL,
[Fips] [varchar] (5) NULL
)

set identity_insert [TT_County] on
    insert into [TT_County] (
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
    from County
set identity_insert [TT_County] off

ALTER TABLE [TT_County] ADD CONSTRAINT [PK_TT_County] PRIMARY KEY CLUSTERED  ([CountyId])

CREATE TABLE [TT_DocumentStatus]
(
[DocumentStatusId] [int] NOT NULL,
[StatusName] [varchar] (50) NOT NULL
)

    insert into [TT_DocumentStatus] (
[DocumentStatusId],
[StatusName]
    ) select 
[DocumentStatusId],
[StatusName]
    from DocumentStatus

ALTER TABLE [TT_DocumentStatus] ADD CONSTRAINT [PK_TT_DocumentStatus] PRIMARY KEY CLUSTERED  ([DocumentStatusId])

CREATE TABLE [TT_DocumentType]
(
[DocTypeID] [int] NOT NULL IDENTITY(1, 1),
[Name] [varchar] (50) NULL,
[GiverRequired] [bit] NOT NULL,
[ReceiverRequired] [bit] NOT NULL,
[GiverRoleName] [varchar] (50) NULL,
[ReceiverRoleName] [varchar] (50) NULL
)

set identity_insert [TT_DocumentType] on
    insert into [TT_DocumentType] (
[DocTypeID],
[Name],
[GiverRequired],
[ReceiverRequired],
[GiverRoleName],
[ReceiverRoleName]
    ) select 
[DocTypeID],
[Name],
[GiverRequired],
[ReceiverRequired],
[GiverRoleName],
[ReceiverRoleName]
    from DocumentType
set identity_insert [TT_DocumentType] off

ALTER TABLE [TT_DocumentType] ADD CONSTRAINT [PK_TT_DocumentType] PRIMARY KEY CLUSTERED  ([DocTypeID])

CREATE TABLE [TT_DocumentAttachmentType]
(
[DocumentAttachmentTypeId] [int] NOT NULL IDENTITY(1, 1),
[Name] [varchar] (50) NOT NULL
)

set identity_insert [TT_DocumentAttachmentType] on
    insert into [TT_DocumentAttachmentType] (
[DocumentAttachmentTypeId],
[Name]
    ) select 
[DocumentAttachmentTypeId],
[Name]
    from DocumentAttachmentType
set identity_insert [TT_DocumentAttachmentType] off

ALTER TABLE [TT_DocumentAttachmentType] ADD CONSTRAINT [PK_TT_DocumentAttachmentType] PRIMARY KEY CLUSTERED  ([DocumentAttachmentTypeId])

CREATE TABLE [TT_File]
(
[FileId] [int] NOT NULL IDENTITY(1, 1),
[FileName] [varchar] (250) NOT NULL,
[FileUrl] [varchar] (250) NOT NULL,
[FilePath] [varchar] (250) NOT NULL,
[CreatedBy] [int] NOT NULL,
[Created] [datetime] NOT NULL,
[Description] [varchar] (250) NOT NULL
)

set identity_insert [TT_File] on
    insert into [TT_File] (
[FileId],
[FileName],
[FileUrl],
[FilePath],
[CreatedBy],
[Created],
[Description]
    ) select 
[FileId],
[FileName],
[FileUrl],
[FilePath],
[CreatedBy],
[Created],
[Description]
    from [File]
set identity_insert [TT_File] off

ALTER TABLE [TT_File] ADD CONSTRAINT [PK__TT_File] PRIMARY KEY CLUSTERED  ([FileId])


CREATE TABLE [TT_Group]
(
[GroupId] [int] NOT NULL IDENTITY(1, 1),
[GroupName] [varchar] (50) NOT NULL
)

set identity_insert [TT_Group] on
    insert into [TT_Group] (
[GroupId],
[GroupName]
    ) select 
[GroupId],
[GroupName]
    from [Group]
set identity_insert [TT_Group] off

ALTER TABLE [TT_Group] ADD CONSTRAINT [PK_TT_Group] PRIMARY KEY CLUSTERED  ([GroupId])

CREATE TABLE [TT_GroupItem]
(
[GroupItemId] [int] NOT NULL IDENTITY(1, 1),
[GroupId] [int] NOT NULL,
[DocBranchUid] [uniqueidentifier] NOT NULL
)

set identity_insert [TT_GroupItem] on
    insert into [TT_GroupItem] (
[GroupItemId],
[GroupId],
[DocBranchUid]
    ) select 
[GroupItemId],
[GroupId],
[DocBranchUid]
    from [GroupItem]
set identity_insert [TT_GroupItem] off

ALTER TABLE [TT_GroupItem] ADD CONSTRAINT [PK_TT_GroupItem] PRIMARY KEY CLUSTERED  ([GroupItemId])

CREATE UNIQUE NONCLUSTERED INDEX [TT_UK_GroupId_DocBranchUid] ON [TT_GroupItem] ([GroupId], [DocBranchUid])

CREATE TABLE [TT_TermUnit]
(
[TermUnitId] [int] NOT NULL IDENTITY(1, 1),
[Name] [varchar] (50) NOT NULL
)

set identity_insert [TT_TermUnit] on
    insert into [TT_TermUnit] (
[TermUnitId],
[Name]
    ) select 
[TermUnitId],
[Name]
    from [TermUnit]
set identity_insert [TT_TermUnit] off

ALTER TABLE [TT_TermUnit] ADD CONSTRAINT [PK_TT_TermUnit] PRIMARY KEY CLUSTERED  ([TermUnitId])

CREATE TABLE [TT_Lease]
(
[LeaseId] [int] NOT NULL IDENTITY(1, 1),
[LCN] [varchar] (50) NULL,
[DocumentNumber] [varchar] (50) NULL,
[Volume] [varchar] (50) NULL,
[PAGE] [varchar] (50) NULL,
[LeaseeName] [varchar] (50) NULL,
[AssigneeName] [varchar] (50) NULL,
[LeassorName] [varchar] (50) NULL,
[AssignorName] [varchar] (50) NULL,
[StateFips] [varchar] (2) NULL,
[CountyFips] [varchar] (5) NULL,
[UnitDepth] [numeric] (18, 0) NULL,
[FromDepth] [numeric] (18, 0) NULL,
[FromFrom] [numeric] (18, 0) NULL,
[ToDepth] [numeric] (18, 0) NULL,
[ToFrom] [numeric] (18, 0) NULL,
[WorkInt] [varchar] (50) NULL,
[OrrInt] [varchar] (50) NULL,
[NetAcres] [numeric] (18, 0) NULL,
[GrossAcres] [numeric] (18, 0) NULL,
[NriAssign] [varchar] (50) NULL,
[RcdDate] [datetime] NULL,
[Term] [numeric] (18, 0) NULL,
[TermUnitId] [int] NULL,
[HBR] [bit] NOT NULL,
[Encumbrances] [bit] NOT NULL,
[EffDate] [datetime] NULL,
[PughClause] [bit] NOT NULL,
[DepthLimitation] [bit] NOT NULL,
[ShutInClau] [bit] NOT NULL,
[PoolingClau] [bit] NOT NULL,
[MinimumPmt] [numeric] (18, 0) NULL,
[Author] [int] NULL,
[Status] [varchar] (10) NULL
)

set identity_insert [TT_Lease] on
    insert into [TT_Lease] (
[LeaseId],
[LCN],
[DocumentNumber],
[Volume],
[PAGE],
[LeaseeName],
[AssigneeName],
[LeassorName],
[AssignorName],
[StateFips],
[CountyFips],
[UnitDepth],
[FromDepth],
[FromFrom],
[ToDepth],
[ToFrom],
[WorkInt],
[OrrInt],
[NetAcres],
[GrossAcres],
[NriAssign],
[RcdDate],
[Term],
[TermUnitId],
[HBR],
[Encumbrances],
[EffDate],
[PughClause],
[DepthLimitation],
[ShutInClau],
[PoolingClau],
[MinimumPmt],
[Author],
[Status]
    ) select 
[LeaseId],
[LCN],
[DocumentNumber],
[Volume],
[PAGE],
[LeaseeName],
[AssigneeName],
[LeassorName],
[AssignorName],
[StateFips],
[CountyFips],
[UnitDepth],
[FromDepth],
[FromFrom],
[ToDepth],
[ToFrom],
[WorkInt],
[OrrInt],
[NetAcres],
[GrossAcres],
[NriAssign],
[RcdDate],
[Term],
[TermUnitId],
[HBR],
[Encumbrances],
[EffDate],
[PughClause],
[DepthLimitation],
[ShutInClau],
[PoolingClau],
[MinimumPmt],
[Author],
[Status]
    from [Lease]
set identity_insert [TT_Lease] off

ALTER TABLE [TT_Lease] ADD CONSTRAINT [PK_TT_Lease] PRIMARY KEY CLUSTERED  ([LeaseId])

CREATE TABLE [TT_LeaseEditHistory]
(
[EditHistoryId] [int] NOT NULL IDENTITY(1, 1),
[UserId] [int] NOT NULL,
[LeaseId] [int] NOT NULL,
[DateEdited] [datetime] NOT NULL,
[Status] [varchar] (10) NULL
)

set identity_insert [TT_LeaseEditHistory] on
    insert into [TT_LeaseEditHistory] (
[EditHistoryId],
[UserId],
[LeaseId],
[DateEdited],
[Status]
    ) select 
[EditHistoryId],
[UserId],
[LeaseId],
[DateEdited],
[Status]
    from [LeaseEditHistory]
set identity_insert [TT_LeaseEditHistory] off

ALTER TABLE [TT_LeaseEditHistory] ADD CONSTRAINT [PK_TT_LeaseEditHistory] PRIMARY KEY CLUSTERED  ([EditHistoryId])

CREATE TABLE [TT_Module]
(
[ModuleId] [int] NOT NULL IDENTITY(1, 1),
[Description] [varchar] (250) NOT NULL
)

set identity_insert [TT_Module] on
    insert into [TT_Module] (
[ModuleId],
[Description]
    ) select 
[ModuleId],
[Description]
    from [Module]
set identity_insert [TT_Module] off

ALTER TABLE [TT_Module] ADD CONSTRAINT [PK_TT_Module] PRIMARY KEY CLUSTERED  ([ModuleId])

CREATE TABLE [TT_Permission]
(
[PermissionId] [int] NOT NULL IDENTITY(1, 1),
[ModuleId] [int] NOT NULL,
[Description] [varchar] (250) NULL,
[Code] [varchar] (50) NOT NULL
)

set identity_insert [TT_Permission] on
    insert into [TT_Permission] (
[PermissionId],
[ModuleId],
[Description],
[Code]
    ) select 
[PermissionId],
[ModuleId],
[Description],
[Code]
    from [Permission]
set identity_insert [TT_Permission] off

ALTER TABLE [TT_Permission] ADD CONSTRAINT [PK_TT_Permission] PRIMARY KEY CLUSTERED  ([PermissionId])

CREATE TABLE [TT_PermissionAssignment]
(
[PermissionAssignmentId] [int] NOT NULL IDENTITY(1, 1),
[PermissionId] [int] NOT NULL,
[RoleId] [int] NOT NULL
)

set identity_insert [TT_PermissionAssignment] on
    insert into [TT_PermissionAssignment] (
[PermissionAssignmentId],
[PermissionId],
[RoleId]
    ) select 
[PermissionAssignmentId],
[PermissionId],
[RoleId]
    from [PermissionAssignment]
set identity_insert [TT_PermissionAssignment] off

ALTER TABLE [TT_PermissionAssignment] ADD CONSTRAINT [PK_TT_PermissionAssignment] PRIMARY KEY CLUSTERED  ([PermissionAssignmentId])

CREATE TABLE [TT_Role]
(
[RoleId] [int] NOT NULL IDENTITY(1, 1),
[Name] [varchar] (50) NOT NULL
)

set identity_insert [TT_Role] on
    insert into [TT_Role] (
[RoleId],
[Name]
    ) select 
[RoleId],
[Name]
    from [Role]
set identity_insert [TT_Role] off

ALTER TABLE [TT_Role] ADD CONSTRAINT [PK_TT_Role] PRIMARY KEY CLUSTERED  ([RoleId])

CREATE TABLE [TT_ProjectStatus]
(
[StatusName] [varchar] (50) NOT NULL
)

    insert into [TT_ProjectStatus] (
[StatusName]
    ) select 
[StatusName]
    from [ProjectStatus]

ALTER TABLE [TT_ProjectStatus] ADD CONSTRAINT [PK_TT_ProjectStatus] PRIMARY KEY CLUSTERED  ([StatusName])

CREATE TABLE [TT_ProjectAttachment]
(
[ProjectAttachmentId] [int] NOT NULL IDENTITY(1, 1),
[ProjectId] [int] NOT NULL,
[ProjectAttachmentTypeId] [int] NOT NULL,
[FileId] [int] NOT NULL
)

set identity_insert [TT_ProjectAttachment] on
    insert into [TT_ProjectAttachment] (
[ProjectAttachmentId],
[ProjectId],
[ProjectAttachmentTypeId],
[FileId]
    ) select 
[ProjectAttachmentId],
[ProjectId],
[ProjectAttachmentTypeId],
[FileId]
    from [ProjectAttachment]
set identity_insert [TT_ProjectAttachment] off

ALTER TABLE [TT_ProjectAttachment] ADD CONSTRAINT [PK_TT_ProjectAttachment] PRIMARY KEY CLUSTERED  ([ProjectAttachmentId])

CREATE TABLE [TT_ProjectAttachmentType]
(
[ProjectAttachmentTypeId] [int] NOT NULL IDENTITY(1, 1),
[Name] [varchar] (50) NOT NULL,
[Description] [varchar] (250) NOT NULL
)

set identity_insert [TT_ProjectAttachmentType] on
    insert into [TT_ProjectAttachmentType] (
[ProjectAttachmentTypeId],
[Name],
[Description]
    ) select 
[ProjectAttachmentTypeId],
[Name],
[Description]
    from [ProjectAttachmentType]
set identity_insert [TT_ProjectAttachmentType] off

ALTER TABLE [TT_ProjectAttachmentType] ADD CONSTRAINT [PK__TT_ProjectAttachmentType] PRIMARY KEY CLUSTERED  ([ProjectAttachmentTypeId])

CREATE TABLE [TT_ProjectAttachmentInfo]
(
[ProjectAttachmentInfoId] [int] NOT NULL IDENTITY(1, 1),
[ProjectAttachmentId] [int] NOT NULL,
[Code] [varchar] (50) NOT NULL,
[Value] [varchar] (250) NOT NULL
)

set identity_insert [TT_ProjectAttachmentInfo] on
    insert into [TT_ProjectAttachmentInfo] (
[ProjectAttachmentInfoId],
[ProjectAttachmentId],
[Code],
[Value]
    ) select 
[ProjectAttachmentInfoId],
[ProjectAttachmentId],
[Code],
[Value]
    from [ProjectAttachmentInfo]
set identity_insert [TT_ProjectAttachmentInfo] off

ALTER TABLE [TT_ProjectAttachmentInfo] ADD CONSTRAINT [PK__TT_ProjectAttachmentInfo] PRIMARY KEY CLUSTERED  ([ProjectAttachmentInfoId])

CREATE TABLE [TT_ProjectTabContact]
(
[ProjectTabContactId] [int] NOT NULL IDENTITY(1, 1),
[ProjectTabId] [int] NOT NULL,
[ContactType] [varchar] (50) NULL,
[ContactName] [varchar] (50) NULL,
[FirstName] [varchar] (50) NULL,
[MiddleName] [varchar] (50) NULL,
[LastName] [varchar] (50) NULL,
[EntityRelationship] [varchar] (50) NULL,
[PhysicalAddress] [int] NULL,
[MailingAddress] [int] NULL,
[PhoneNumber] [varchar] (30) NULL,
[Email] [varchar] (50) NULL,
[IsActive] [bit] NULL,
[IsEntity] [bit] NULL
)

set identity_insert [TT_ProjectTabContact] on
    insert into [TT_ProjectTabContact] (
[ProjectTabContactId],
[ProjectTabId],
[ContactType],
[ContactName],
[FirstName],
[MiddleName],
[LastName],
[EntityRelationship],
[PhysicalAddress],
[MailingAddress],
[PhoneNumber],
[Email],
[IsActive],
[IsEntity]
    ) select 
[ProjectTabContactId],
[ProjectTabId],
[ContactType],
[ContactName],
[FirstName],
[MiddleName],
[LastName],
[EntityRelationship],
[PhysicalAddress],
[MailingAddress],
[PhoneNumber],
[Email],
[IsActive],
[IsEntity]
    from [ProjectTabContact]
set identity_insert [TT_ProjectTabContact] off

ALTER TABLE [TT_ProjectTabContact] ADD CONSTRAINT [PK_TT_ProjectTabContact] PRIMARY KEY CLUSTERED  ([ProjectTabContactId])

CREATE TABLE [TT_ProjectTabDocument]
(
[ProjectTabDocumentId] [int] NOT NULL IDENTITY(1, 1),
[ProjectTabId] [int] NOT NULL,
[DocumentId] [int] NOT NULL,
[Description] [varchar] (250) NOT NULL,
[Remarks] [varchar] (250) NOT NULL,
[IsActive] [bit] NOT NULL
)

set identity_insert [TT_ProjectTabDocument] on
    insert into [TT_ProjectTabDocument] (
[ProjectTabDocumentId],
[ProjectTabId],
[DocumentId],
[Description],
[Remarks],
[IsActive]
    ) select 
[ProjectTabDocumentId],
[ProjectTabId],
[DocumentId],
[Description],
[Remarks],
[IsActive]
    from [ProjectTabDocument]
set identity_insert [TT_ProjectTabDocument] off

ALTER TABLE [TT_ProjectTabDocument] ADD CONSTRAINT [PK_TT_ProjectTabDocume] PRIMARY KEY CLUSTERED  ([ProjectTabDocumentId])

CREATE TABLE [TT_ProjectTabDocumentTract]
(
[ProjectTabDocumentTractId] [int] NOT NULL IDENTITY(1, 1),
[ProjectTabDocumentId] [int] NOT NULL,
[TractId] [int] NOT NULL
)

set identity_insert [TT_ProjectTabDocumentTract] on
    insert into [TT_ProjectTabDocumentTract] (
[ProjectTabDocumentTractId],
[ProjectTabDocumentId],
[TractId]
    ) select 
[ProjectTabDocumentTractId],
[ProjectTabDocumentId],
[TractId]
    from [ProjectTabDocumentTract]
set identity_insert [TT_ProjectTabDocumentTract] off

ALTER TABLE [TT_ProjectTabDocumentTract] ADD CONSTRAINT [PK_TT_ProjectTabDocumentTract] PRIMARY KEY CLUSTERED  ([ProjectTabDocumentTractId])

CREATE TABLE [TT_GroupUser]
(
[GroupUserId] [int] NOT NULL IDENTITY(1, 1),
[GroupId] [int] NOT NULL,
[UserId] [int] NOT NULL
)

set identity_insert [TT_GroupUser] on
    insert into [TT_GroupUser] (
[GroupUserId],
[GroupId],
[UserId]
    ) select 
[GroupUserId],
[GroupId],
[UserId]
    from [GroupUser]
set identity_insert [TT_GroupUser] off

ALTER TABLE [TT_GroupUser] ADD CONSTRAINT [PK_TT_GroupUsers] PRIMARY KEY CLUSTERED  ([GroupUserId])

CREATE TABLE [TT_UserRole]
(
[UserRoleId] [int] NOT NULL IDENTITY(1, 1),
[UserId] [int] NOT NULL,
[RoleId] [int] NOT NULL
)

set identity_insert [TT_UserRole] on
    insert into [TT_UserRole] (
[UserRoleId],
[UserId],
[RoleId]
    ) select 
[UserRoleId],
[UserId],
[RoleId]
    from [UserRole]
set identity_insert [TT_UserRole] off

ALTER TABLE [TT_UserRole] ADD CONSTRAINT [PK_TT_UserRole] PRIMARY KEY CLUSTERED  ([UserRoleId])

CREATE TABLE [TT_HistoryLog]
(
[HistoryLogId] [int] NOT NULL IDENTITY(1, 1),
[SourceTableId] [int] NOT NULL,
[BackupTableId] [int] NOT NULL,
[SourceItemId] [int] NOT NULL,
[BackupItemId] [int] NOT NULL,
[ItemVersion] [int] NOT NULL,
[LogDate] [datetime] NOT NULL,
[UserId] [int] NOT NULL,
[Description] [varchar] (50) NOT NULL
)

set identity_insert [TT_HistoryLog] on
    insert into [TT_HistoryLog] (
[HistoryLogId],
[SourceTableId],
[BackupTableId],
[SourceItemId],
[BackupItemId],
[ItemVersion],
[LogDate],
[UserId],
[Description]
    ) select 
[HistoryLogId],
[SourceTableId],
[BackupTableId],
[SourceItemId],
[BackupItemId],
[ItemVersion],
[LogDate],
[UserId],
[Description]
    from [HistoryLog]
set identity_insert [TT_HistoryLog] off

ALTER TABLE [TT_HistoryLog] ADD CONSTRAINT [PK_TT_HistoryLog] PRIMARY KEY CLUSTERED  ([HistoryLogId])

CREATE TABLE [TT_Unit]
(
[UnitId] [int] NOT NULL IDENTITY(1, 1),
[Name] [varchar] (50) NULL,
[AcresRate] [float] NOT NULL
)

set identity_insert [TT_Unit] on
    insert into [TT_Unit] (
[UnitId],
[Name],
[AcresRate]
    ) select 
[UnitId],
[Name],
[AcresRate]
    from [Unit]
set identity_insert [TT_Unit] off

ALTER TABLE [TT_Unit] ADD CONSTRAINT [PK_TT_Unit] PRIMARY KEY CLUSTERED  ([UnitId])
go

CREATE PROCEDURE [sp_TT_CopyDocument]
    @docId int, @userId int
AS
BEGIN
        SET NOCOUNT ON;

    BEGIN TRY

        BEGIN TRAN

        declare @copiedDocId int, @tractId int, @newTractId int

        INSERT INTO [TT_Document]
                   ([IsPublic],[DocTypeId],[Volume],[Page],[DocumentNo],[County],[State],[Filed],[Signed]
                   ,[ResearchNote],[ImageLink],[CreatedBy],[Created],[IsActive],[DocBranchUid],[PreviousVersion])
        SELECT [IsPublic],[DocTypeId],[Volume],[Page],[DocumentNo],[County],[State],[Filed],[Signed]
                   ,[ResearchNote],[ImageLink],@userId, [Created], 1, [DocBranchUid], @docId
          FROM [TT_Document]
         WHERE [DocId] = @docId

        SELECT @copiedDocId = @@IDENTITY

        UPDATE [TT_Document] 
           SET [IsActive] = 0
         WHERE [DocId] = @docId

        INSERT INTO [TT_Participant]
                   ([DocID],[AsNamed],[FirstName],[MiddleName],[LastName],[IsSeller])
        SELECT @copiedDocId,[AsNamed],[FirstName],[MiddleName],[LastName],[IsSeller]
          FROM [TT_Participant]
         WHERE [DocId] = @docId

        INSERT INTO [TT_DocumentAttachment]
                ([DocumentAttachmentTypeId], [DocumentId], [FileId])
        SELECT [DocumentAttachmentTypeId], @copiedDocId, [FileId]
          FROM [TT_DocumentAttachment]
         WHERE [DocumentId] = @docId

        INSERT INTO [TT_DocumentReference]
                ([DocumentId], [ReferenceId], [Description], [State], [County], [DocTypeId], [DocumentNo], [Volume], [Page])
        SELECT @copiedDocId, [ReferenceId], [Description], [State], [County], [DocTypeId], [DocumentNo], [Volume], [Page]
          FROM [TT_DocumentReference]
         WHERE [DocumentId] = @docId

        DECLARE tract_cursor cursor FORWARD_ONLY FOR (
            SELECT [TractId]
              FROM [TT_Tract]
             WHERE [DocId] = @docId
        )

        OPEN tract_cursor
        FETCH NEXT FROM tract_cursor into @tractId

        WHILE @@FETCH_STATUS = 0
        BEGIN

            INSERT INTO [TT_Tract] ([Easting],[Northing],[RefName],[CreatedBy],[DocID],[CalledAC],[UnitId], [UniqueId])
            SELECT [Easting],[Northing],[RefName],[CreatedBy],@copiedDocId,[CalledAC],[UnitId], [UniqueId]
              FROM [TT_Tract]
             WHERE TractId = @tractId

            SET @newTractId = @@IDENTITY

            INSERT INTO [TT_TractCall] ([TractId], [Type], [Params], [Order], CreatedByMouse)
            SELECT @newTractId, [Type], [Params], [Order], CreatedByMouse
              FROM [TT_TractCall] where TractId = @tractId

            INSERT INTO [TT_TractTextObject] ([TractId], [Text], [Easting], [Northing], [Rotation])
            SELECT @newTractId, [Text], [Easting], [Northing], [Rotation]
              FROM [TT_TractTextObject] where TractId = @tractId

            FETCH NEXT FROM tract_cursor into @TractId
        END

        CLOSE tract_cursor
        DEALLOCATE tract_cursor

        COMMIT TRAN
        RETURN @copiedDocId
    END TRY
    BEGIN CATCH

        -- Print error information. 
        PRINT 'Error ' + CONVERT(varchar(50), ERROR_NUMBER()) +
              ', Severity ' + CONVERT(varchar(5), ERROR_SEVERITY()) +
              ', State ' + CONVERT(varchar(5), ERROR_STATE()) + 
              ', Procedure ' + ISNULL(ERROR_PROCEDURE(), '-') + 
              ', Line ' + CONVERT(varchar(5), ERROR_LINE());
        PRINT ERROR_MESSAGE();

        IF XACT_STATE() <> 0
        BEGIN
            ROLLBACK TRANSACTION;
        END

    END CATCH

END
go

ALTER TABLE [TT_GroupUser] ADD CONSTRAINT [TT_UK_GroupId_UserID] UNIQUE NONCLUSTERED  ([GroupId], [UserId])

ALTER TABLE [TT_ProjectTabDocument] ADD CONSTRAINT [TT_UK_ProjectTabDocument_ProjectTabId_DocumentID] UNIQUE NONCLUSTERED  ([ProjectTabId], [DocumentId])

ALTER TABLE [TT_Tract] ADD CONSTRAINT [TT_ck_refNameDocId] UNIQUE NONCLUSTERED  ([RefName], [DocIdUnique])

ALTER TABLE [TT_ProjectTabContact] ADD
CONSTRAINT [FK_TT_ProjectTabContact_Address] FOREIGN KEY ([PhysicalAddress]) REFERENCES [TT_Address] ([AddressId]),
CONSTRAINT [FK_TT_ProjectTabContact_Address1] FOREIGN KEY ([MailingAddress]) REFERENCES [TT_Address] ([AddressId]),
CONSTRAINT [FK_TT_ProjectTabContact_ProjectTab] FOREIGN KEY ([ProjectTabId]) REFERENCES [TT_ProjectTab] ([ProjectTabId]) ON DELETE CASCADE ON UPDATE CASCADE

ALTER TABLE [TT_Address] ADD
CONSTRAINT [FK_TT_Address_State] FOREIGN KEY ([State]) REFERENCES [TT_State] ([StateId])

ALTER TABLE [TT_AssetAssignment] ADD
CONSTRAINT [FK_TT_AssetAssignment_Asset] FOREIGN KEY ([AssetId]) REFERENCES [TT_Asset] ([AssetId]),
CONSTRAINT [FK_TT_AssetAssignment_Project] FOREIGN KEY ([ProjectId]) REFERENCES [TT_Project] ([ProjectId]) ON DELETE CASCADE

ALTER TABLE [TT_User] ADD
CONSTRAINT [FK_TT_User_Asset] FOREIGN KEY ([AssetId]) REFERENCES [TT_Asset] ([AssetId]),
CONSTRAINT [FK_TT_User_Client] FOREIGN KEY ([ClientId]) REFERENCES [TT_Client] ([ClientId])

ALTER TABLE [TT_ClientAccount] ADD
CONSTRAINT [FK_TT_ClientAccount_Client] FOREIGN KEY ([ClientId]) REFERENCES [TT_Client] ([ClientId]) ON DELETE CASCADE

ALTER TABLE [TT_Project] ADD
CONSTRAINT [FK_TT_Project_Client] FOREIGN KEY ([ClientId]) REFERENCES [TT_Client] ([ClientId]),
CONSTRAINT [FK_TT_Project_ClientAccount] FOREIGN KEY ([ClientAccountId]) REFERENCES [TT_ClientAccount] ([ClientAccountId]),
CONSTRAINT [FK_TT_Project_ProjectStatus] FOREIGN KEY ([Status]) REFERENCES [TT_ProjectStatus] ([StatusName]),
CONSTRAINT [FK_TT_Project_User] FOREIGN KEY ([ChangedBy]) REFERENCES [TT_User] ([UserId])

ALTER TABLE [TT_DocumentReference] ADD
CONSTRAINT [FK_TT_DocumentReference_County] FOREIGN KEY ([County]) REFERENCES [TT_County] ([CountyId]),
CONSTRAINT [FK_TT_DocumentReference_Document] FOREIGN KEY ([DocumentId]) REFERENCES [TT_Document] ([DocID]),
CONSTRAINT [FK_TT_DocumentReference_Document1] FOREIGN KEY ([ReferenceId]) REFERENCES [TT_Document] ([DocID]),
CONSTRAINT [FK_TT_DocumentReference_State] FOREIGN KEY ([State]) REFERENCES [TT_State] ([StateId]),
CONSTRAINT [FK_TT_DocumentReference_DocumentType] FOREIGN KEY ([DocTypeId]) REFERENCES [TT_DocumentType] ([DocTypeID])

ALTER TABLE [TT_County] ADD
CONSTRAINT [FK_TT_County_State1] FOREIGN KEY ([StateId]) REFERENCES [TT_State] ([StateId])

ALTER TABLE [TT_DocumentAttachment] ADD
CONSTRAINT [FK_TT_DocumentAttachment_Document] FOREIGN KEY ([DocumentId]) REFERENCES [TT_Document] ([DocID]),
CONSTRAINT [FK_TT_DocumentAttachment_DocumentAttachmentType] FOREIGN KEY ([DocumentAttachmentTypeId]) REFERENCES [TT_DocumentAttachmentType] ([DocumentAttachmentTypeId]),
CONSTRAINT [FK_TT_DocumentAttachment_File] FOREIGN KEY ([FileId]) REFERENCES [TT_File] ([FileId]) ON DELETE CASCADE

ALTER TABLE [TT_Participant] ADD
CONSTRAINT [FK_TT_Participant_Document] FOREIGN KEY ([DocID]) REFERENCES [TT_Document] ([DocID]) ON DELETE CASCADE

ALTER TABLE [TT_ProjectTabDocument] ADD
CONSTRAINT [FK_TT_ProjectTabDocument_Document] FOREIGN KEY ([DocumentId]) REFERENCES [TT_Document] ([DocID]),
CONSTRAINT [FK_TT_ProjectTabDocument_ProjectTab] FOREIGN KEY ([ProjectTabId]) REFERENCES [TT_ProjectTab] ([ProjectTabId]) ON DELETE CASCADE

ALTER TABLE [TT_Tract] ADD
CONSTRAINT [FK_TT_Tract_Document] FOREIGN KEY ([DocID]) REFERENCES [TT_Document] ([DocID]) ON DELETE CASCADE,
CONSTRAINT [FK_TT_Tract_User] FOREIGN KEY ([CreatedBy]) REFERENCES [TT_User] ([UserId])

ALTER TABLE [TT_Document] ADD
CONSTRAINT [FK_TT_Document_DocumentType] FOREIGN KEY ([DocTypeId]) REFERENCES [TT_DocumentType] ([DocTypeID]),
CONSTRAINT [FK_TT_Document_User] FOREIGN KEY ([CreatedBy]) REFERENCES [TT_User] ([UserId]),
CONSTRAINT [FK_TT_Document_DocumentStatus] FOREIGN KEY ([DocumentStatusId]) REFERENCES [TT_DocumentStatus] ([DocumentStatusId]),
CONSTRAINT [FK_TT_Document_User1] FOREIGN KEY ([LockedBy]) REFERENCES [TT_User] ([UserId])

ALTER TABLE [TT_ProjectAttachment] ADD
CONSTRAINT [FK_TT_ProjectAttachment_File] FOREIGN KEY ([FileId]) REFERENCES [TT_File] ([FileId]) ON DELETE CASCADE,
CONSTRAINT [FK_TT_ProjectAttachment_Project] FOREIGN KEY ([ProjectId]) REFERENCES [TT_Project] ([ProjectId]) ON DELETE CASCADE,
CONSTRAINT [FK_TT_ProjectAttachment_ProjectAttachmentType] FOREIGN KEY ([ProjectAttachmentTypeId]) REFERENCES [TT_ProjectAttachmentType] ([ProjectAttachmentTypeId])

ALTER TABLE [TT_File] ADD
CONSTRAINT [FK_TT_File_User] FOREIGN KEY ([CreatedBy]) REFERENCES [TT_User] ([UserId])

ALTER TABLE [TT_GroupItem] ADD
CONSTRAINT [FK_TT_GroupItem_Group] FOREIGN KEY ([GroupId]) REFERENCES [TT_Group] ([GroupId]) ON DELETE CASCADE

ALTER TABLE [TT_GroupUser] ADD
CONSTRAINT [FK_TT_UserGroup_Group] FOREIGN KEY ([GroupId]) REFERENCES [TT_Group] ([GroupId]) ON DELETE CASCADE,
CONSTRAINT [FK_TT_UserGroup_User] FOREIGN KEY ([UserId]) REFERENCES [TT_User] ([UserId]) ON DELETE CASCADE

ALTER TABLE [TT_LeaseEditHistory] ADD
CONSTRAINT [FK_TT_LeaseEditHistory_Lease] FOREIGN KEY ([LeaseId]) REFERENCES [TT_Lease] ([LeaseId]) ON DELETE CASCADE,
CONSTRAINT [FK_TT_LeaseEditHistory_User] FOREIGN KEY ([UserId]) REFERENCES [TT_User] ([UserId]) ON DELETE CASCADE

ALTER TABLE [TT_Lease] ADD
CONSTRAINT [FK_TT_Lease_TermUnit] FOREIGN KEY ([TermUnitId]) REFERENCES [TT_TermUnit] ([TermUnitId])

ALTER TABLE [TT_Permission] ADD
CONSTRAINT [FK_TT_Permission_Module1] FOREIGN KEY ([ModuleId]) REFERENCES [TT_Module] ([ModuleId]) ON DELETE CASCADE

ALTER TABLE [TT_PermissionAssignment] ADD
CONSTRAINT [FK_TT_PermissionAssignment_Permission] FOREIGN KEY ([PermissionId]) REFERENCES [TT_Permission] ([PermissionId]) ON DELETE CASCADE,
CONSTRAINT [FK_TT_PermissionAssignment_Role] FOREIGN KEY ([RoleId]) REFERENCES [TT_Role] ([RoleId]) ON DELETE CASCADE

ALTER TABLE [TT_ProjectTab] ADD
CONSTRAINT [FK_TT_ProjectTab_Project] FOREIGN KEY ([ProjectId]) REFERENCES [TT_Project] ([ProjectId]) ON DELETE CASCADE

ALTER TABLE [TT_ProjectAttachmentInfo] ADD
CONSTRAINT [FK_TT_ProjectAttachmentInfo_ProjectAttachment] FOREIGN KEY ([ProjectAttachmentId]) REFERENCES [TT_ProjectAttachment] ([ProjectAttachmentId]) ON DELETE CASCADE

ALTER TABLE [TT_ProjectTabDocumentTract] ADD
CONSTRAINT [FK_TT_ProjectTabDocumentTract_ProjectTabDocument] FOREIGN KEY ([ProjectTabDocumentId]) REFERENCES [TT_ProjectTabDocument] ([ProjectTabDocumentId]),
CONSTRAINT [FK_TT_ProjectTabDocumentTract_Tract] FOREIGN KEY ([TractId]) REFERENCES [TT_Tract] ([TractId])

ALTER TABLE [TT_UserRole] ADD
CONSTRAINT [FK_TT_UserRole_Role] FOREIGN KEY ([RoleId]) REFERENCES [TT_Role] ([RoleId]) ON DELETE CASCADE ON UPDATE CASCADE,
CONSTRAINT [FK_TT_UserRole_User] FOREIGN KEY ([UserId]) REFERENCES [TT_User] ([UserId]) ON DELETE CASCADE ON UPDATE CASCADE

ALTER TABLE [TT_TractCall] ADD
CONSTRAINT [FK_TT_TractCalls_Tract] FOREIGN KEY ([TractId]) REFERENCES [TT_Tract] ([TractId]) ON DELETE CASCADE

ALTER TABLE [TT_TractTextObject] ADD
CONSTRAINT [FK_TT_TractTextObjects_Tract] FOREIGN KEY ([TractId]) REFERENCES [TT_Tract] ([TractId]) ON DELETE CASCADE

---
drop TABLE [TractTextObject]
drop TABLE [TractCall]
drop TABLE [ProjectTabDocumentTract]
drop TABLE [Tract]
drop TABLE [DocumentReference]
drop TABLE [DocumentAttachment]
drop TABLE [Participant]
drop TABLE [ProjectTabDocument]
drop TABLE [DocumentAttachment_Old]
drop TABLE [Document]
drop TABLE [DocumentStatus]
drop TABLE [DocumentType]
drop TABLE [DocumentAttachmentType]
drop TABLE [ProjectTabContact]
drop TABLE [ProjectTab]
drop TABLE [AssetAssignment]
drop TABLE [ProjectAttachmentInfo]
drop TABLE [ProjectAttachment]
drop TABLE [Project]
drop TABLE [File]
drop TABLE [GroupUser]
drop TABLE [LeaseEditHistory]
drop TABLE [UserRole]
drop TABLE [User]
drop TABLE [Address]
drop TABLE [County]
drop TABLE [State]
drop TABLE [Asset]
drop TABLE [ClientAccount]
drop TABLE [Client]
drop TABLE [GroupItem]
drop TABLE [Group]
drop TABLE [Lease]
drop TABLE [TermUnit]
drop TABLE [PermissionAssignment]
drop TABLE [Permission]
drop TABLE [Module]
drop TABLE [Role]
drop TABLE [ProjectStatus]
drop TABLE [ProjectAttachmentType]
drop TABLE [HistoryLog]
drop TABLE [Unit]
go

drop procedure sp_CopyDocument
go

