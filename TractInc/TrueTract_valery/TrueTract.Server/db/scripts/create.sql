/*
Script created by SQL Compare version 6.2.1 from Red Gate Software Ltd at 04.01.2008 13:36:41
Run this script on (local).ttt to make it the same as (local).TractInc
Please back up your database before running this script
*/
SET NUMERIC_ROUNDABORT OFF
GO
SET ANSI_PADDING, ANSI_WARNINGS, CONCAT_NULL_YIELDS_NULL, ARITHABORT, QUOTED_IDENTIFIER, ANSI_NULLS ON
GO
IF EXISTS (SELECT * FROM tempdb..sysobjects WHERE id=OBJECT_ID('tempdb..#tmpErrors')) DROP TABLE #tmpErrors
GO
CREATE TABLE #tmpErrors (Error int)
GO
SET XACT_ABORT ON
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO
CREATE USER [root] WITHOUT LOGIN WITH DEFAULT_SCHEMA=[dbo]
GO
GRANT ALTER TO [root]
GRANT ALTER ANY ASYMMETRIC KEY TO [root]
GRANT ALTER ANY APPLICATION ROLE TO [root]
GRANT ALTER ANY ASSEMBLY TO [root]
GRANT ALTER ANY CERTIFICATE TO [root]
GRANT ALTER ANY DATASPACE TO [root]
GRANT ALTER ANY DATABASE EVENT NOTIFICATION TO [root]
GRANT ALTER ANY FULLTEXT CATALOG TO [root]
GRANT ALTER ANY MESSAGE TYPE TO [root]
GRANT ALTER ANY ROLE TO [root]
GRANT ALTER ANY ROUTE TO [root]
GRANT ALTER ANY REMOTE SERVICE BINDING TO [root]
GRANT ALTER ANY CONTRACT TO [root]
GRANT ALTER ANY SYMMETRIC KEY TO [root]
GRANT ALTER ANY SCHEMA TO [root]
GRANT ALTER ANY SERVICE TO [root]
GRANT ALTER ANY DATABASE DDL TRIGGER TO [root]
GRANT ALTER ANY USER TO [root]
GRANT AUTHENTICATE TO [root]
GRANT BACKUP DATABASE TO [root]
GRANT BACKUP LOG TO [root]
GRANT CONTROL TO [root]
GRANT CONNECT TO [root]
GRANT CONNECT REPLICATION TO [root]
GRANT CHECKPOINT TO [root]
GRANT CREATE AGGREGATE TO [root]
GRANT CREATE ASYMMETRIC KEY TO [root]
GRANT CREATE ASSEMBLY TO [root]
GRANT CREATE CERTIFICATE TO [root]
GRANT CREATE DEFAULT TO [root]
GRANT CREATE DATABASE DDL EVENT NOTIFICATION TO [root]
GRANT CREATE FUNCTION TO [root]
GRANT CREATE FULLTEXT CATALOG TO [root]
GRANT CREATE MESSAGE TYPE TO [root]
GRANT CREATE PROCEDURE TO [root]
GRANT CREATE QUEUE TO [root]
GRANT CREATE ROLE TO [root]
GRANT CREATE ROUTE TO [root]
GRANT CREATE RULE TO [root]
GRANT CREATE REMOTE SERVICE BINDING TO [root]
GRANT CREATE CONTRACT TO [root]
GRANT CREATE SYMMETRIC KEY TO [root]
GRANT CREATE SCHEMA TO [root]
GRANT CREATE SYNONYM TO [root]
GRANT CREATE SERVICE TO [root]
GRANT CREATE TABLE TO [root]
GRANT CREATE TYPE TO [root]
GRANT CREATE VIEW TO [root]
GRANT CREATE XML SCHEMA COLLECTION TO [root]
GRANT DELETE TO [root]
GRANT EXECUTE TO [root]
GRANT INSERT TO [root]
GRANT REFERENCES TO [root]
GRANT SELECT TO [root]
GRANT SHOWPLAN TO [root]
GRANT SUBSCRIBE QUERY NOTIFICATIONS TO [root]
GRANT TAKE OWNERSHIP TO [root]
GRANT UPDATE TO [root]
GRANT VIEW DEFINITION TO [root]
GRANT VIEW DATABASE STATE TO [root]
GO
PRINT N'Altering members of role db_owner'
GO
EXEC sp_addrolemember N'db_owner', N'root'
GO
BEGIN TRANSACTION
GO
PRINT N'Creating [dbo].[TractTextObject]'
GO
CREATE TABLE [dbo].[TractTextObject]
(
[TractTextObjectId] [int] NOT NULL IDENTITY(1, 1),
[TractId] [int] NOT NULL,
[Text] [nvarchar] (4000) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Easting] [numeric] (18, 2) NOT NULL,
[Northing] [numeric] (18, 2) NOT NULL,
[Rotation] [numeric] (18, 2) NOT NULL
)

GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK_TractTextObject] on [dbo].[TractTextObject]'
GO
ALTER TABLE [dbo].[TractTextObject] ADD CONSTRAINT [PK_TractTextObject] PRIMARY KEY CLUSTERED  ([TractTextObjectId])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[TractCall]'
GO
CREATE TABLE [dbo].[TractCall]
(
[TractCallId] [int] NOT NULL IDENTITY(1, 1),
[TractId] [int] NOT NULL,
[Type] [varchar] (15) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Params] [varchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Order] [int] NOT NULL,
[CreatedByMouse] [bit] NOT NULL
)

GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK_TractCall] on [dbo].[TractCall]'
GO
ALTER TABLE [dbo].[TractCall] ADD CONSTRAINT [PK_TractCall] PRIMARY KEY CLUSTERED  ([TractCallId])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[Tract]'
GO
CREATE TABLE [dbo].[Tract]
(
[TractId] [int] NOT NULL IDENTITY(1, 1),
[Easting] [int] NOT NULL,
[Northing] [int] NOT NULL,
[RefName] [varchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedBy] [int] NOT NULL,
[DocID] [int] NULL,
[CalledAC] [numeric] (18, 2) NOT NULL,
[UnitId] [int] NOT NULL,
[DocIdUnique] AS (isnull([DocId],(-1))),
[UniqueId] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
)

GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK_Tract] on [dbo].[Tract]'
GO
ALTER TABLE [dbo].[Tract] ADD CONSTRAINT [PK_Tract] PRIMARY KEY CLUSTERED  ([TractId])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[DocumentReference]'
GO
CREATE TABLE [dbo].[DocumentReference]
(
[DocumentReferenceId] [int] NOT NULL IDENTITY(1, 1),
[DocumentId] [int] NOT NULL,
[ReferenceId] [int] NULL,
[Description] [varchar] (350) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[State] [int] NULL,
[County] [int] NULL,
[DocTypeId] [int] NULL,
[DocumentNo] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Volume] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Page] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
)

GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK_DocumentReference] on [dbo].[DocumentReference]'
GO
ALTER TABLE [dbo].[DocumentReference] ADD CONSTRAINT [PK_DocumentReference] PRIMARY KEY CLUSTERED  ([DocumentReferenceId])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[DocumentAttachment]'
GO
CREATE TABLE [dbo].[DocumentAttachment]
(
[DocumentAttachmentId] [int] NOT NULL IDENTITY(1, 1),
[DocumentAttachmentTypeId] [int] NOT NULL,
[DocumentId] [int] NOT NULL,
[FileId] [int] NOT NULL
)

GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK_DocumentAttachment] on [dbo].[DocumentAttachment]'
GO
ALTER TABLE [dbo].[DocumentAttachment] ADD CONSTRAINT [PK_DocumentAttachment] PRIMARY KEY CLUSTERED  ([DocumentAttachmentId])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[Participant]'
GO
CREATE TABLE [dbo].[Participant]
(
[ParticipantID] [int] NOT NULL IDENTITY(1, 1),
[DocID] [int] NULL,
[AsNamed] [varchar] (350) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[FirstName] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[MiddleName] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[LastName] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[IsSeller] [bit] NOT NULL
)

GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK_Participant] on [dbo].[Participant]'
GO
ALTER TABLE [dbo].[Participant] ADD CONSTRAINT [PK_Participant] PRIMARY KEY CLUSTERED  ([ParticipantID])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating index [IX_AsNamed] on [dbo].[Participant]'
GO
CREATE NONCLUSTERED INDEX [IX_AsNamed] ON [dbo].[Participant] ([AsNamed])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[Document]'
GO
CREATE TABLE [dbo].[Document]
(
[DocID] [int] NOT NULL IDENTITY(1, 1),
[IsPublic] [bit] NOT NULL,
[DocTypeId] [int] NOT NULL,
[Volume] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Page] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[DocumentNo] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[County] [int] NOT NULL,
[State] [int] NOT NULL,
[ResearchNote] [varchar] (350) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[ImageLink] [varchar] (350) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[DocPlace] AS (case when [documentNo] IS NULL OR len([documentNo])=(0) then (('Vol: '+[Volume])+', Pg:')+[Page] else 'DocNo: '+[documentNo] end),
[IsActive] [bit] NOT NULL CONSTRAINT [DF__Document__IsActi__44FF419A] DEFAULT ((1)),
[DocBranchUid] [uniqueidentifier] NOT NULL CONSTRAINT [DF__Document__DocBra__45F365D3] DEFAULT (newid()),
[Filed] [datetime] NULL,
[Signed] [datetime] NULL,
[Created] [datetime] NOT NULL,
[CreatedBy] [int] NOT NULL,
[PreviousVersion] [int] NULL,
[DocumentStatusId] [int] NULL,
[LockedBy] [int] NULL
)

GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK_Document] on [dbo].[Document]'
GO
ALTER TABLE [dbo].[Document] ADD CONSTRAINT [PK_Document] PRIMARY KEY CLUSTERED  ([DocID])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating index [IX_DocBranchUid] on [dbo].[Document]'
GO
CREATE NONCLUSTERED INDEX [IX_DocBranchUid] ON [dbo].[Document] ([DocBranchUid])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[ProjectTab]'
GO
CREATE TABLE [dbo].[ProjectTab]
(
[ProjectTabId] [int] NOT NULL IDENTITY(1, 1),
[ProjectId] [int] NOT NULL,
[Name] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Label] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[TabOrder] [int] NOT NULL CONSTRAINT [DF_ProjectTab_TabOrder] DEFAULT ((0))
)

GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK__ProjectTab__0F624AF8] on [dbo].[ProjectTab]'
GO
ALTER TABLE [dbo].[ProjectTab] ADD CONSTRAINT [PK__ProjectTab__0F624AF8] PRIMARY KEY CLUSTERED  ([ProjectTabId])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[User]'
GO
CREATE TABLE [dbo].[User]
(
[UserId] [int] NOT NULL IDENTITY(1, 1),
[Login] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[FirstName] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[LastName] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[PhoneNumber] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Password] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Email] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[IsActive] [bit] NOT NULL CONSTRAINT [DF__User__IsActive__1367E606] DEFAULT ((1)),
[HackingAttempts] [int] NOT NULL CONSTRAINT [DF__User__HackingAtt__145C0A3F] DEFAULT ((0)),
[NewTracts] [int] NOT NULL CONSTRAINT [DF__User__NewTracts__15502E78] DEFAULT ((0)),
[DefaultSite] [varchar] (250) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_User_DefaultSite] DEFAULT ('Barnett Shale'),
[AssetId] [int] NULL,
[ClientId] [int] NULL
)

GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK_User] on [dbo].[User]'
GO
ALTER TABLE [dbo].[User] ADD CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED  ([UserId])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[State]'
GO
CREATE TABLE [dbo].[State]
(
[StateId] [int] NOT NULL,
[Name] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[StateFips] [varchar] (2) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[StateAbbr] [varchar] (2) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
)

GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK_State] on [dbo].[State]'
GO
ALTER TABLE [dbo].[State] ADD CONSTRAINT [PK_State] PRIMARY KEY CLUSTERED  ([StateId])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[Address]'
GO
CREATE TABLE [dbo].[Address]
(
[AddressId] [int] NOT NULL IDENTITY(1, 1),
[Address1] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Address2] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[City] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[State] [int] NULL,
[Zip] [varchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
)

GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK_Address] on [dbo].[Address]'
GO
ALTER TABLE [dbo].[Address] ADD CONSTRAINT [PK_Address] PRIMARY KEY CLUSTERED  ([AddressId])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[Asset]'
GO
CREATE TABLE [dbo].[Asset]
(
[AssetId] [int] NOT NULL IDENTITY(1, 1),
[AssetName] [varchar] (250) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
)

GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK__Asset__76969D2E] on [dbo].[Asset]'
GO
ALTER TABLE [dbo].[Asset] ADD CONSTRAINT [PK__Asset__76969D2E] PRIMARY KEY CLUSTERED  ([AssetId])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[AssetAssignment]'
GO
CREATE TABLE [dbo].[AssetAssignment]
(
[AssetAssignmentId] [int] NOT NULL IDENTITY(1, 1),
[AssetId] [int] NOT NULL,
[ProjectId] [int] NOT NULL,
[StartDate] [datetime] NOT NULL,
[EndDate] [datetime] NULL
)

GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK__AssetAssignment__787EE5A0] on [dbo].[AssetAssignment]'
GO
ALTER TABLE [dbo].[AssetAssignment] ADD CONSTRAINT [PK__AssetAssignment__787EE5A0] PRIMARY KEY CLUSTERED  ([AssetAssignmentId])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[Project]'
GO
CREATE TABLE [dbo].[Project]
(
[ProjectId] [int] NOT NULL IDENTITY(1, 1),
[Name] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ShortName] [varchar] (25) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ClientId] [int] NOT NULL,
[ClientAccountId] [int] NOT NULL,
[Description] [varchar] (250) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Status] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Changed] [datetime] NULL,
[ChangedBy] [int] NULL
)

GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK__Project__619B8048] on [dbo].[Project]'
GO
ALTER TABLE [dbo].[Project] ADD CONSTRAINT [PK__Project__619B8048] PRIMARY KEY CLUSTERED  ([ProjectId])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[Client]'
GO
CREATE TABLE [dbo].[Client]
(
[ClientId] [int] NOT NULL IDENTITY(1, 1),
[Name] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
)

GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK__Client__5AEE82B9] on [dbo].[Client]'
GO
ALTER TABLE [dbo].[Client] ADD CONSTRAINT [PK__Client__5AEE82B9] PRIMARY KEY CLUSTERED  ([ClientId])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[ClientAccount]'
GO
CREATE TABLE [dbo].[ClientAccount]
(
[ClientAccountId] [int] NOT NULL IDENTITY(1, 1),
[ClientId] [int] NOT NULL,
[Code] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Name] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
)

GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK__ClientAccount__5CD6CB2B] on [dbo].[ClientAccount]'
GO
ALTER TABLE [dbo].[ClientAccount] ADD CONSTRAINT [PK__ClientAccount__5CD6CB2B] PRIMARY KEY CLUSTERED  ([ClientAccountId])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[County]'
GO
CREATE TABLE [dbo].[County]
(
[CountyId] [int] NOT NULL IDENTITY(1, 1),
[Name] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[StateId] [int] NOT NULL,
[StateName] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[StateFips] [varchar] (2) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[CountyFips] [varchar] (3) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Fips] [varchar] (5) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
)

GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK_County] on [dbo].[County]'
GO
ALTER TABLE [dbo].[County] ADD CONSTRAINT [PK_County] PRIMARY KEY CLUSTERED  ([CountyId])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[DocumentStatus]'
GO
CREATE TABLE [dbo].[DocumentStatus]
(
[DocumentStatusId] [int] NOT NULL,
[StatusName] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
)

GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK_DocumentStatus] on [dbo].[DocumentStatus]'
GO
ALTER TABLE [dbo].[DocumentStatus] ADD CONSTRAINT [PK_DocumentStatus] PRIMARY KEY CLUSTERED  ([DocumentStatusId])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[DocumentType]'
GO
CREATE TABLE [dbo].[DocumentType]
(
[DocTypeID] [int] NOT NULL IDENTITY(1, 1),
[Name] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[GiverRequired] [bit] NOT NULL,
[ReceiverRequired] [bit] NOT NULL,
[GiverRoleName] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[ReceiverRoleName] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
)

GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK_DocumentType] on [dbo].[DocumentType]'
GO
ALTER TABLE [dbo].[DocumentType] ADD CONSTRAINT [PK_DocumentType] PRIMARY KEY CLUSTERED  ([DocTypeID])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[DocumentAttachment_Old]'
GO
CREATE TABLE [dbo].[DocumentAttachment_Old]
(
[DocumentAttachmentId] [int] NOT NULL IDENTITY(1, 1),
[DocId] [int] NOT NULL,
[FileName] [varchar] (350) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[OriginalFileName] [varchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
)

GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK_DocumentAttachment_Old] on [dbo].[DocumentAttachment_Old]'
GO
ALTER TABLE [dbo].[DocumentAttachment_Old] ADD CONSTRAINT [PK_DocumentAttachment_Old] PRIMARY KEY CLUSTERED  ([DocumentAttachmentId])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[DocumentAttachmentType]'
GO
CREATE TABLE [dbo].[DocumentAttachmentType]
(
[DocumentAttachmentTypeId] [int] NOT NULL IDENTITY(1, 1),
[Name] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
)

GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK_DocumentAttachmentType] on [dbo].[DocumentAttachmentType]'
GO
ALTER TABLE [dbo].[DocumentAttachmentType] ADD CONSTRAINT [PK_DocumentAttachmentType] PRIMARY KEY CLUSTERED  ([DocumentAttachmentTypeId])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[File]'
GO
CREATE TABLE [dbo].[File]
(
[FileId] [int] NOT NULL IDENTITY(1, 1),
[FileName] [varchar] (250) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[FileUrl] [varchar] (250) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[FilePath] [varchar] (250) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatedBy] [int] NOT NULL,
[Created] [datetime] NOT NULL,
[Description] [varchar] (250) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
)

GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK__File__66603565] on [dbo].[File]'
GO
ALTER TABLE [dbo].[File] ADD CONSTRAINT [PK__File__66603565] PRIMARY KEY CLUSTERED  ([FileId])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[Group]'
GO
CREATE TABLE [dbo].[Group]
(
[GroupId] [int] NOT NULL IDENTITY(1, 1),
[GroupName] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
)

GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK_Group] on [dbo].[Group]'
GO
ALTER TABLE [dbo].[Group] ADD CONSTRAINT [PK_Group] PRIMARY KEY CLUSTERED  ([GroupId])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[GroupItem]'
GO
CREATE TABLE [dbo].[GroupItem]
(
[GroupItemId] [int] NOT NULL IDENTITY(1, 1),
[GroupId] [int] NOT NULL,
[DocBranchUid] [uniqueidentifier] NOT NULL
)

GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK_GroupItem] on [dbo].[GroupItem]'
GO
ALTER TABLE [dbo].[GroupItem] ADD CONSTRAINT [PK_GroupItem] PRIMARY KEY CLUSTERED  ([GroupItemId])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating index [UK_GroupId_DocBranchUid] on [dbo].[GroupItem]'
GO
CREATE UNIQUE NONCLUSTERED INDEX [UK_GroupId_DocBranchUid] ON [dbo].[GroupItem] ([GroupId], [DocBranchUid])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[TermUnit]'
GO
CREATE TABLE [dbo].[TermUnit]
(
[TermUnitId] [int] NOT NULL IDENTITY(1, 1),
[Name] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
)

GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK_TermUnit] on [dbo].[TermUnit]'
GO
ALTER TABLE [dbo].[TermUnit] ADD CONSTRAINT [PK_TermUnit] PRIMARY KEY CLUSTERED  ([TermUnitId])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[Lease]'
GO
CREATE TABLE [dbo].[Lease]
(
[LeaseId] [int] NOT NULL IDENTITY(1, 1),
[LCN] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[DocumentNumber] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Volume] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[PAGE] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[LeaseeName] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[AssigneeName] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[LeassorName] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[AssignorName] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[StateFips] [varchar] (2) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[CountyFips] [varchar] (5) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[UnitDepth] [numeric] (18, 0) NULL,
[FromDepth] [numeric] (18, 0) NULL,
[FromFrom] [numeric] (18, 0) NULL,
[ToDepth] [numeric] (18, 0) NULL,
[ToFrom] [numeric] (18, 0) NULL,
[WorkInt] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[OrrInt] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[NetAcres] [numeric] (18, 0) NULL,
[GrossAcres] [numeric] (18, 0) NULL,
[NriAssign] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
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
[Status] [varchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
)

GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK_Lease] on [dbo].[Lease]'
GO
ALTER TABLE [dbo].[Lease] ADD CONSTRAINT [PK_Lease] PRIMARY KEY CLUSTERED  ([LeaseId])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[LeaseEditHistory]'
GO
CREATE TABLE [dbo].[LeaseEditHistory]
(
[EditHistoryId] [int] NOT NULL IDENTITY(1, 1),
[UserId] [int] NOT NULL,
[LeaseId] [int] NOT NULL,
[DateEdited] [datetime] NOT NULL,
[Status] [varchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
)

GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK_LeaseEditHistory] on [dbo].[LeaseEditHistory]'
GO
ALTER TABLE [dbo].[LeaseEditHistory] ADD CONSTRAINT [PK_LeaseEditHistory] PRIMARY KEY CLUSTERED  ([EditHistoryId])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[Module]'
GO
CREATE TABLE [dbo].[Module]
(
[ModuleId] [int] NOT NULL IDENTITY(1, 1),
[Description] [varchar] (250) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
)

GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK_Module] on [dbo].[Module]'
GO
ALTER TABLE [dbo].[Module] ADD CONSTRAINT [PK_Module] PRIMARY KEY CLUSTERED  ([ModuleId])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[Permission]'
GO
CREATE TABLE [dbo].[Permission]
(
[PermissionId] [int] NOT NULL IDENTITY(1, 1),
[ModuleId] [int] NOT NULL,
[Description] [varchar] (250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Code] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
)

GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK_Permission] on [dbo].[Permission]'
GO
ALTER TABLE [dbo].[Permission] ADD CONSTRAINT [PK_Permission] PRIMARY KEY CLUSTERED  ([PermissionId])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[PermissionAssignment]'
GO
CREATE TABLE [dbo].[PermissionAssignment]
(
[PermissionAssignmentId] [int] NOT NULL IDENTITY(1, 1),
[PermissionId] [int] NOT NULL,
[RoleId] [int] NOT NULL
)

GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK_PermissionPermissionGroup] on [dbo].[PermissionAssignment]'
GO
ALTER TABLE [dbo].[PermissionAssignment] ADD CONSTRAINT [PK_PermissionPermissionGroup] PRIMARY KEY CLUSTERED  ([PermissionAssignmentId])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[Role]'
GO
CREATE TABLE [dbo].[Role]
(
[RoleId] [int] NOT NULL IDENTITY(1, 1),
[Name] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
)

GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK_PermissionGroup] on [dbo].[Role]'
GO
ALTER TABLE [dbo].[Role] ADD CONSTRAINT [PK_PermissionGroup] PRIMARY KEY CLUSTERED  ([RoleId])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[ProjectStatus]'
GO
CREATE TABLE [dbo].[ProjectStatus]
(
[StatusName] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
)

GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK_ProjectStatus] on [dbo].[ProjectStatus]'
GO
ALTER TABLE [dbo].[ProjectStatus] ADD CONSTRAINT [PK_ProjectStatus] PRIMARY KEY CLUSTERED  ([StatusName])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[ProjectAttachment]'
GO
CREATE TABLE [dbo].[ProjectAttachment]
(
[ProjectAttachmentId] [int] NOT NULL IDENTITY(1, 1),
[ProjectId] [int] NOT NULL,
[ProjectAttachmentTypeId] [int] NOT NULL,
[FileId] [int] NOT NULL
)

GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK__ProjectAttachmen__6B24EA82] on [dbo].[ProjectAttachment]'
GO
ALTER TABLE [dbo].[ProjectAttachment] ADD CONSTRAINT [PK__ProjectAttachmen__6B24EA82] PRIMARY KEY CLUSTERED  ([ProjectAttachmentId])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[ProjectAttachmentType]'
GO
CREATE TABLE [dbo].[ProjectAttachmentType]
(
[ProjectAttachmentTypeId] [int] NOT NULL IDENTITY(1, 1),
[Name] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Description] [varchar] (250) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
)

GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK__ProjectAttachmen__693CA210] on [dbo].[ProjectAttachmentType]'
GO
ALTER TABLE [dbo].[ProjectAttachmentType] ADD CONSTRAINT [PK__ProjectAttachmen__693CA210] PRIMARY KEY CLUSTERED  ([ProjectAttachmentTypeId])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[ProjectAttachmentInfo]'
GO
CREATE TABLE [dbo].[ProjectAttachmentInfo]
(
[ProjectAttachmentInfoId] [int] NOT NULL IDENTITY(1, 1),
[ProjectAttachmentId] [int] NOT NULL,
[Code] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Value] [varchar] (250) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
)

GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK__ProjectAttachmen__7E37BEF6] on [dbo].[ProjectAttachmentInfo]'
GO
ALTER TABLE [dbo].[ProjectAttachmentInfo] ADD CONSTRAINT [PK__ProjectAttachmen__7E37BEF6] PRIMARY KEY CLUSTERED  ([ProjectAttachmentInfoId])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[ProjectTabContact]'
GO
CREATE TABLE [dbo].[ProjectTabContact]
(
[ProjectTabContactId] [int] NOT NULL IDENTITY(1, 1),
[ProjectTabId] [int] NOT NULL,
[ContactType] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[ContactName] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[FirstName] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[MiddleName] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[LastName] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[EntityRelationship] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[PhysicalAddress] [int] NULL,
[MailingAddress] [int] NULL,
[PhoneNumber] [varchar] (30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Email] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[IsActive] [bit] NULL,
[IsEntity] [bit] NULL
)

GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK_ProjectTabContact] on [dbo].[ProjectTabContact]'
GO
ALTER TABLE [dbo].[ProjectTabContact] ADD CONSTRAINT [PK_ProjectTabContact] PRIMARY KEY CLUSTERED  ([ProjectTabContactId])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[ProjectTabDocument]'
GO
CREATE TABLE [dbo].[ProjectTabDocument]
(
[ProjectTabDocumentId] [int] NOT NULL IDENTITY(1, 1),
[ProjectTabId] [int] NOT NULL,
[DocumentId] [int] NOT NULL,
[Description] [varchar] (250) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Remarks] [varchar] (250) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[IsActive] [bit] NOT NULL
)

GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK__ProjectTabDocume__123EB7A3] on [dbo].[ProjectTabDocument]'
GO
ALTER TABLE [dbo].[ProjectTabDocument] ADD CONSTRAINT [PK__ProjectTabDocume__123EB7A3] PRIMARY KEY CLUSTERED  ([ProjectTabDocumentId])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[ProjectTabDocumentTract]'
GO
CREATE TABLE [dbo].[ProjectTabDocumentTract]
(
[ProjectTabDocumentTractId] [int] NOT NULL IDENTITY(1, 1),
[ProjectTabDocumentId] [int] NOT NULL,
[TractId] [int] NOT NULL
)

GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK_ProjectTabDocumentTract] on [dbo].[ProjectTabDocumentTract]'
GO
ALTER TABLE [dbo].[ProjectTabDocumentTract] ADD CONSTRAINT [PK_ProjectTabDocumentTract] PRIMARY KEY CLUSTERED  ([ProjectTabDocumentTractId])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[GroupUser]'
GO
CREATE TABLE [dbo].[GroupUser]
(
[GroupUserId] [int] NOT NULL IDENTITY(1, 1),
[GroupId] [int] NOT NULL,
[UserId] [int] NOT NULL
)

GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK_GroupUsers] on [dbo].[GroupUser]'
GO
ALTER TABLE [dbo].[GroupUser] ADD CONSTRAINT [PK_GroupUsers] PRIMARY KEY CLUSTERED  ([GroupUserId])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[UserRole]'
GO
CREATE TABLE [dbo].[UserRole]
(
[UserRoleId] [int] NOT NULL IDENTITY(1, 1),
[UserId] [int] NOT NULL,
[RoleId] [int] NOT NULL
)

GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK_UserPermissionGroup] on [dbo].[UserRole]'
GO
ALTER TABLE [dbo].[UserRole] ADD CONSTRAINT [PK_UserPermissionGroup] PRIMARY KEY CLUSTERED  ([UserRoleId])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[sp_CopyDocument]'
GO








-- =============================================
-- Create date: 2007/08/08
-- Description: Copyes Document and his related Participants and Tracts
-- =============================================
CREATE PROCEDURE [dbo].[sp_CopyDocument]
    @docId int, @userId int
AS
BEGIN
        SET NOCOUNT ON;

    BEGIN TRY

        BEGIN TRAN

        declare @copiedDocId int, @tractId int, @newTractId int

        INSERT INTO [Document]
                   ([IsPublic],[DocTypeId],[Volume],[Page],[DocumentNo],[County],[State],[Filed],[Signed]
                   ,[ResearchNote],[ImageLink],[CreatedBy],[Created],[IsActive],[DocBranchUid],[PreviousVersion])
        SELECT [IsPublic],[DocTypeId],[Volume],[Page],[DocumentNo],[County],[State],[Filed],[Signed]
                   ,[ResearchNote],[ImageLink],@userId, [Created], 1, [DocBranchUid], @docId
          FROM [Document]
         WHERE [DocId] = @docId

        SELECT @copiedDocId = @@IDENTITY

        UPDATE [Document] 
           SET [IsActive] = 0
         WHERE [DocId] = @docId

        INSERT INTO [Participant]
                   ([DocID],[AsNamed],[FirstName],[MiddleName],[LastName],[IsSeller])
        SELECT @copiedDocId,[AsNamed],[FirstName],[MiddleName],[LastName],[IsSeller]
          FROM [Participant]
         WHERE [DocId] = @docId

        INSERT INTO [DocumentAttachment]
                ([DocumentAttachmentTypeId], [DocumentId], [FileId])
        SELECT [DocumentAttachmentTypeId], @copiedDocId, [FileId]
          FROM [DocumentAttachment]
         WHERE [DocumentId] = @docId

        INSERT INTO [DocumentReference]
                ([DocumentId], [ReferenceId], [Description], [State], [County], [DocTypeId], [DocumentNo], [Volume], [Page])
        SELECT @copiedDocId, [ReferenceId], [Description], [State], [County], [DocTypeId], [DocumentNo], [Volume], [Page]
          FROM [DocumentReference]
         WHERE [DocumentId] = @docId

        DECLARE tract_cursor cursor FORWARD_ONLY FOR (
            SELECT [TractId]
              FROM [Tract]
             WHERE [DocId] = @docId
        )

        OPEN tract_cursor
        FETCH NEXT FROM tract_cursor into @tractId

        WHILE @@FETCH_STATUS = 0
        BEGIN

            INSERT INTO [Tract] ([Easting],[Northing],[RefName],[CreatedBy],[DocID],[CalledAC],[UnitId], [UniqueId])
            SELECT [Easting],[Northing],[RefName],[CreatedBy],@copiedDocId,[CalledAC],[UnitId], [UniqueId]
              FROM [Tract]
             WHERE TractId = @tractId

            SET @newTractId = @@IDENTITY

            INSERT INTO [TractCall] ([TractId], [Type], [Params], [Order], CreatedByMouse)
            SELECT @newTractId, [Type], [Params], [Order], CreatedByMouse
              FROM [TractCall] where TractId = @tractId

            INSERT INTO [TractTextObject] ([TractId], [Text], [Easting], [Northing], [Rotation])
            SELECT @newTractId, [Text], [Easting], [Northing], [Rotation]
              FROM [TractTextObject] where TractId = @tractId

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









GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[HistoryLog]'
GO
CREATE TABLE [dbo].[HistoryLog]
(
[HistoryLogId] [int] NOT NULL IDENTITY(1, 1),
[SourceTableId] [int] NOT NULL,
[BackupTableId] [int] NOT NULL,
[SourceItemId] [int] NOT NULL,
[BackupItemId] [int] NOT NULL,
[ItemVersion] [int] NOT NULL,
[LogDate] [datetime] NOT NULL,
[UserId] [int] NOT NULL,
[Description] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
)

GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK_HistoryLog] on [dbo].[HistoryLog]'
GO
ALTER TABLE [dbo].[HistoryLog] ADD CONSTRAINT [PK_HistoryLog] PRIMARY KEY CLUSTERED  ([HistoryLogId])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[Unit]'
GO
CREATE TABLE [dbo].[Unit]
(
[UnitId] [int] NOT NULL IDENTITY(1, 1),
[Name] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[AcresRate] [float] NOT NULL
)

GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK_Unit] on [dbo].[Unit]'
GO
ALTER TABLE [dbo].[Unit] ADD CONSTRAINT [PK_Unit] PRIMARY KEY CLUSTERED  ([UnitId])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding constraints to [dbo].[GroupUser]'
GO
ALTER TABLE [dbo].[GroupUser] ADD CONSTRAINT [UK_GroupId_UserID] UNIQUE NONCLUSTERED  ([GroupId], [UserId])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding constraints to [dbo].[ProjectTabDocument]'
GO
ALTER TABLE [dbo].[ProjectTabDocument] ADD CONSTRAINT [UK_ProjectTabDocument_ProjectTabId_DocumentID] UNIQUE NONCLUSTERED  ([ProjectTabId], [DocumentId])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding constraints to [dbo].[Tract]'
GO
ALTER TABLE [dbo].[Tract] ADD CONSTRAINT [ck_refNameDocId] UNIQUE NONCLUSTERED  ([RefName], [DocIdUnique])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[ProjectTabContact]'
GO
ALTER TABLE [dbo].[ProjectTabContact] ADD
CONSTRAINT [FK_ProjectTabContact_Address] FOREIGN KEY ([PhysicalAddress]) REFERENCES [dbo].[Address] ([AddressId]),
CONSTRAINT [FK_ProjectTabContact_Address1] FOREIGN KEY ([MailingAddress]) REFERENCES [dbo].[Address] ([AddressId]),
CONSTRAINT [FK_ProjectTabContact_ProjectTab] FOREIGN KEY ([ProjectTabId]) REFERENCES [dbo].[ProjectTab] ([ProjectTabId]) ON DELETE CASCADE ON UPDATE CASCADE
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[Address]'
GO
ALTER TABLE [dbo].[Address] ADD
CONSTRAINT [FK_Address_State] FOREIGN KEY ([State]) REFERENCES [dbo].[State] ([StateId])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[AssetAssignment]'
GO
ALTER TABLE [dbo].[AssetAssignment] ADD
CONSTRAINT [FK_AssetAssignment_Asset] FOREIGN KEY ([AssetId]) REFERENCES [dbo].[Asset] ([AssetId]),
CONSTRAINT [FK_AssetAssignment_Project] FOREIGN KEY ([ProjectId]) REFERENCES [dbo].[Project] ([ProjectId]) ON DELETE CASCADE
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[User]'
GO
ALTER TABLE [dbo].[User] ADD
CONSTRAINT [FK_User_Asset] FOREIGN KEY ([AssetId]) REFERENCES [dbo].[Asset] ([AssetId]),
CONSTRAINT [FK_User_Client] FOREIGN KEY ([ClientId]) REFERENCES [dbo].[Client] ([ClientId])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[ClientAccount]'
GO
ALTER TABLE [dbo].[ClientAccount] ADD
CONSTRAINT [FK_ClientAccount_Client] FOREIGN KEY ([ClientId]) REFERENCES [dbo].[Client] ([ClientId]) ON DELETE CASCADE
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[Project]'
GO
ALTER TABLE [dbo].[Project] ADD
CONSTRAINT [FK_Project_Client] FOREIGN KEY ([ClientId]) REFERENCES [dbo].[Client] ([ClientId]),
CONSTRAINT [FK_Project_ClientAccount] FOREIGN KEY ([ClientAccountId]) REFERENCES [dbo].[ClientAccount] ([ClientAccountId]),
CONSTRAINT [FK_Project_ProjectStatus] FOREIGN KEY ([Status]) REFERENCES [dbo].[ProjectStatus] ([StatusName]),
CONSTRAINT [FK_Project_User] FOREIGN KEY ([ChangedBy]) REFERENCES [dbo].[User] ([UserId])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[DocumentReference]'
GO
ALTER TABLE [dbo].[DocumentReference] ADD
CONSTRAINT [FK_DocumentReference_County] FOREIGN KEY ([County]) REFERENCES [dbo].[County] ([CountyId]),
CONSTRAINT [FK_DocumentReference_Document] FOREIGN KEY ([DocumentId]) REFERENCES [dbo].[Document] ([DocID]),
CONSTRAINT [FK_DocumentReference_Document1] FOREIGN KEY ([ReferenceId]) REFERENCES [dbo].[Document] ([DocID]),
CONSTRAINT [FK_DocumentReference_State] FOREIGN KEY ([State]) REFERENCES [dbo].[State] ([StateId]),
CONSTRAINT [FK_DocumentReference_DocumentType] FOREIGN KEY ([DocTypeId]) REFERENCES [dbo].[DocumentType] ([DocTypeID])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[County]'
GO
ALTER TABLE [dbo].[County] ADD
CONSTRAINT [FK_County_State1] FOREIGN KEY ([StateId]) REFERENCES [dbo].[State] ([StateId])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[DocumentAttachment]'
GO
ALTER TABLE [dbo].[DocumentAttachment] ADD
CONSTRAINT [FK_DocumentAttachment_Document] FOREIGN KEY ([DocumentId]) REFERENCES [dbo].[Document] ([DocID]),
CONSTRAINT [FK_DocumentAttachment_DocumentAttachmentType] FOREIGN KEY ([DocumentAttachmentTypeId]) REFERENCES [dbo].[DocumentAttachmentType] ([DocumentAttachmentTypeId]),
CONSTRAINT [FK_DocumentAttachment_File] FOREIGN KEY ([FileId]) REFERENCES [dbo].[File] ([FileId]) ON DELETE CASCADE
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[DocumentAttachment_Old]'
GO
ALTER TABLE [dbo].[DocumentAttachment_Old] ADD
CONSTRAINT [FK_DocumentAttachment_Document_Old] FOREIGN KEY ([DocId]) REFERENCES [dbo].[Document] ([DocID]) ON DELETE CASCADE ON UPDATE CASCADE
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[Participant]'
GO
ALTER TABLE [dbo].[Participant] ADD
CONSTRAINT [FK_Participant_Document] FOREIGN KEY ([DocID]) REFERENCES [dbo].[Document] ([DocID]) ON DELETE CASCADE
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[ProjectTabDocument]'
GO
ALTER TABLE [dbo].[ProjectTabDocument] ADD
CONSTRAINT [FK_ProjectTabDocument_Document] FOREIGN KEY ([DocumentId]) REFERENCES [dbo].[Document] ([DocID]),
CONSTRAINT [FK_ProjectTabDocument_ProjectTab] FOREIGN KEY ([ProjectTabId]) REFERENCES [dbo].[ProjectTab] ([ProjectTabId]) ON DELETE CASCADE
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[Tract]'
GO
ALTER TABLE [dbo].[Tract] ADD
CONSTRAINT [FK_Tract_Document] FOREIGN KEY ([DocID]) REFERENCES [dbo].[Document] ([DocID]) ON DELETE CASCADE,
CONSTRAINT [FK_Tract_User] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[User] ([UserId])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[Document]'
GO
ALTER TABLE [dbo].[Document] ADD
CONSTRAINT [FK_Document_DocumentType] FOREIGN KEY ([DocTypeId]) REFERENCES [dbo].[DocumentType] ([DocTypeID]),
CONSTRAINT [FK_Document_User] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[User] ([UserId]),
CONSTRAINT [FK_Document_DocumentStatus] FOREIGN KEY ([DocumentStatusId]) REFERENCES [dbo].[DocumentStatus] ([DocumentStatusId]),
CONSTRAINT [FK_Document_User1] FOREIGN KEY ([LockedBy]) REFERENCES [dbo].[User] ([UserId])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[ProjectAttachment]'
GO
ALTER TABLE [dbo].[ProjectAttachment] ADD
CONSTRAINT [FK_ProjectAttachment_File] FOREIGN KEY ([FileId]) REFERENCES [dbo].[File] ([FileId]) ON DELETE CASCADE,
CONSTRAINT [FK_ProjectAttachment_Project] FOREIGN KEY ([ProjectId]) REFERENCES [dbo].[Project] ([ProjectId]) ON DELETE CASCADE,
CONSTRAINT [FK_ProjectAttachment_ProjectAttachmentType] FOREIGN KEY ([ProjectAttachmentTypeId]) REFERENCES [dbo].[ProjectAttachmentType] ([ProjectAttachmentTypeId])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[File]'
GO
ALTER TABLE [dbo].[File] ADD
CONSTRAINT [FK_File_User] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[User] ([UserId])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[GroupItem]'
GO
ALTER TABLE [dbo].[GroupItem] ADD
CONSTRAINT [FK_GroupItem_Group] FOREIGN KEY ([GroupId]) REFERENCES [dbo].[Group] ([GroupId]) ON DELETE CASCADE
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[GroupUser]'
GO
ALTER TABLE [dbo].[GroupUser] ADD
CONSTRAINT [FK_UserGroup_Group] FOREIGN KEY ([GroupId]) REFERENCES [dbo].[Group] ([GroupId]) ON DELETE CASCADE,
CONSTRAINT [FK_UserGroup_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([UserId]) ON DELETE CASCADE
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[LeaseEditHistory]'
GO
ALTER TABLE [dbo].[LeaseEditHistory] ADD
CONSTRAINT [FK_LeaseEditHistory_Lease] FOREIGN KEY ([LeaseId]) REFERENCES [dbo].[Lease] ([LeaseId]) ON DELETE CASCADE,
CONSTRAINT [FK_LeaseEditHistory_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([UserId]) ON DELETE CASCADE
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[Lease]'
GO
ALTER TABLE [dbo].[Lease] ADD
CONSTRAINT [FK_Lease_TermUnit] FOREIGN KEY ([TermUnitId]) REFERENCES [dbo].[TermUnit] ([TermUnitId])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[Permission]'
GO
ALTER TABLE [dbo].[Permission] ADD
CONSTRAINT [FK_Permission_Module1] FOREIGN KEY ([ModuleId]) REFERENCES [dbo].[Module] ([ModuleId]) ON DELETE CASCADE
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[PermissionAssignment]'
GO
ALTER TABLE [dbo].[PermissionAssignment] ADD
CONSTRAINT [FK_PermissionAssignment_Permission] FOREIGN KEY ([PermissionId]) REFERENCES [dbo].[Permission] ([PermissionId]) ON DELETE CASCADE,
CONSTRAINT [FK_PermissionAssignment_Role] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Role] ([RoleId]) ON DELETE CASCADE
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[ProjectTab]'
GO
ALTER TABLE [dbo].[ProjectTab] ADD
CONSTRAINT [FK_ProjectTab_Project] FOREIGN KEY ([ProjectId]) REFERENCES [dbo].[Project] ([ProjectId]) ON DELETE CASCADE
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[ProjectAttachmentInfo]'
GO
ALTER TABLE [dbo].[ProjectAttachmentInfo] ADD
CONSTRAINT [FK_ProjectAttachmentInfo_ProjectAttachment] FOREIGN KEY ([ProjectAttachmentId]) REFERENCES [dbo].[ProjectAttachment] ([ProjectAttachmentId]) ON DELETE CASCADE
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[ProjectTabDocumentTract]'
GO
ALTER TABLE [dbo].[ProjectTabDocumentTract] ADD
CONSTRAINT [FK_ProjectTabDocumentTract_ProjectTabDocument] FOREIGN KEY ([ProjectTabDocumentId]) REFERENCES [dbo].[ProjectTabDocument] ([ProjectTabDocumentId]),
CONSTRAINT [FK_ProjectTabDocumentTract_Tract] FOREIGN KEY ([TractId]) REFERENCES [dbo].[Tract] ([TractId])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[UserRole]'
GO
ALTER TABLE [dbo].[UserRole] ADD
CONSTRAINT [FK_UserRole_Role] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Role] ([RoleId]) ON DELETE CASCADE ON UPDATE CASCADE,
CONSTRAINT [FK_UserRole_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([UserId]) ON DELETE CASCADE ON UPDATE CASCADE
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[TractCall]'
GO
ALTER TABLE [dbo].[TractCall] ADD
CONSTRAINT [FK_TractCalls_Tract] FOREIGN KEY ([TractId]) REFERENCES [dbo].[Tract] ([TractId]) ON DELETE CASCADE
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[TractTextObject]'
GO
ALTER TABLE [dbo].[TractTextObject] ADD
CONSTRAINT [FK_TractTextObjects_Tract] FOREIGN KEY ([TractId]) REFERENCES [dbo].[Tract] ([TractId]) ON DELETE CASCADE
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
GRANT ALTER TO [root]
GRANT ALTER ANY ASYMMETRIC KEY TO [root]
GRANT ALTER ANY APPLICATION ROLE TO [root]
GRANT ALTER ANY ASSEMBLY TO [root]
GRANT ALTER ANY CERTIFICATE TO [root]
GRANT ALTER ANY DATASPACE TO [root]
GRANT ALTER ANY DATABASE EVENT NOTIFICATION TO [root]
GRANT ALTER ANY FULLTEXT CATALOG TO [root]
GRANT ALTER ANY MESSAGE TYPE TO [root]
GRANT ALTER ANY ROLE TO [root]
GRANT ALTER ANY ROUTE TO [root]
GRANT ALTER ANY REMOTE SERVICE BINDING TO [root]
GRANT ALTER ANY CONTRACT TO [root]
GRANT ALTER ANY SYMMETRIC KEY TO [root]
GRANT ALTER ANY SCHEMA TO [root]
GRANT ALTER ANY SERVICE TO [root]
GRANT ALTER ANY DATABASE DDL TRIGGER TO [root]
GRANT ALTER ANY USER TO [root]
GRANT AUTHENTICATE TO [root]
GRANT BACKUP DATABASE TO [root]
GRANT BACKUP LOG TO [root]
GRANT CONTROL TO [root]
GRANT CONNECT TO [root]
GRANT CONNECT REPLICATION TO [root]
GRANT CHECKPOINT TO [root]
GRANT CREATE AGGREGATE TO [root]
GRANT CREATE ASYMMETRIC KEY TO [root]
GRANT CREATE ASSEMBLY TO [root]
GRANT CREATE CERTIFICATE TO [root]
GRANT CREATE DEFAULT TO [root]
GRANT CREATE DATABASE DDL EVENT NOTIFICATION TO [root]
GRANT CREATE FUNCTION TO [root]
GRANT CREATE FULLTEXT CATALOG TO [root]
GRANT CREATE MESSAGE TYPE TO [root]
GRANT CREATE PROCEDURE TO [root]
GRANT CREATE QUEUE TO [root]
GRANT CREATE ROLE TO [root]
GRANT CREATE ROUTE TO [root]
GRANT CREATE RULE TO [root]
GRANT CREATE REMOTE SERVICE BINDING TO [root]
GRANT CREATE CONTRACT TO [root]
GRANT CREATE SYMMETRIC KEY TO [root]
GRANT CREATE SCHEMA TO [root]
GRANT CREATE SYNONYM TO [root]
GRANT CREATE SERVICE TO [root]
GRANT CREATE TABLE TO [root]
GRANT CREATE TYPE TO [root]
GRANT CREATE VIEW TO [root]
GRANT CREATE XML SCHEMA COLLECTION TO [root]
GRANT DELETE TO [root]
GRANT EXECUTE TO [root]
GRANT INSERT TO [root]
GRANT REFERENCES TO [root]
GRANT SELECT TO [root]
GRANT SHOWPLAN TO [root]
GRANT SUBSCRIBE QUERY NOTIFICATIONS TO [root]
GRANT TAKE OWNERSHIP TO [root]
GRANT UPDATE TO [root]
GRANT VIEW DEFINITION TO [root]
GRANT VIEW DATABASE STATE TO [root]
IF EXISTS (SELECT * FROM #tmpErrors) ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT>0 BEGIN
PRINT 'The database update succeeded'
COMMIT TRANSACTION
END
ELSE PRINT 'The database update failed'
GO
DROP TABLE #tmpErrors
GO