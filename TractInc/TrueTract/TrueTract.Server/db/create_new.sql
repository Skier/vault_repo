use tractinc
go

create table Asset(
  AssetId int not null identity primary key
)
go

create table AssetAssignment(
  AssetAssignmentId int not null identity primary key,
  AssetId           int not null,
  ProjectId         int not null
)
go

create table Client(
  ClientId int         not null identity primary key,
  Name     varchar(50) not null
)
go

create table ClientAccount(
  ClientAccountId int         not null identity primary key,
  ClientId        int         not null,
  Code            varchar(50) not null,
  Name            varchar(50) not null
)
go

create table County(
  CountyId   int         not null identity constraint PK_County primary key,
  Name       varchar(50),
  StateId    int         not null,
  StateName  varchar(50),
  StateFips  varchar(2),
  CountyFips varchar(3),
  Fips       varchar(5)
)
go

create table Document(
  DocID        int              not null identity constraint PK_Document primary key,
  IsPublic     bit              not null,
  DocTypeId    int              not null,
  Volume       varchar(50),
  Page         varchar(50),
  DocumentNo   varchar(50),
  County       int              not null,
  State        int              not null,
  ResearchNote varchar(350),
  ImageLink    varchar(350),
  DocPlace      as (case when [documentNo] IS NULL OR len([documentNo])=(0) then (('Vol: '+[Volume])+', Pg:')+[Page] else 'DocNo: '+[documentNo] end),
  IsActive     bit              not null,
  DocBranchUid uniqueidentifier not null,
  Filed        datetime,
  Signed       datetime,
  Created      datetime         not null,
  CreatedBy    int              not null
)
go

create index IX_DocBranchUid on Document(DocBranchUid)
go

create table DocumentAttachment(
  DocumentAttachmentId     int          not null identity constraint PK_DocumentAttachment primary key,
  DocumentAttachmentTypeId int          not null,
  DocumentId               int          not null,
  FileName                 varchar(250) not null,
  FileUrl                  varchar(250) not null,
  Description              varchar(250),
  FileId                   int          not null
)
go

create table DocumentAttachmentType(
  DocumentAttachmentTypeId int         not null identity constraint PK_DocumentAttachmentType primary key,
  Name                     varchar(50) not null
)
go

create table DocumentType(
  DocTypeID        int         not null identity constraint PK_DocumentType primary key,
  Name             varchar(50),
  GiverRequired    bit         not null,
  ReceiverRequired bit         not null,
  GiverRoleName    varchar(50),
  ReceiverRoleName varchar(50)
)
go

create table [File](
  FileId      int          not null identity primary key,
  FileName    varchar(250) not null,
  FileUrl     varchar(250) not null,
  FilePath    varchar(250) not null,
  CreatedBy   int          not null,
  Description varchar(250) not null
)
go

create table [Group](
  GroupId   int         not null identity constraint PK_Group primary key,
  GroupName varchar(50) not null
)
go

create table GroupItem(
  GroupItemId  int              not null identity constraint PK_GroupItem primary key,
  GroupId      int              not null,
  DocBranchUid uniqueidentifier not null
)
go

create unique index UK_GroupId_DocBranchUid on GroupItem(GroupId,DocBranchUid)
go

create table GroupUser(
  GroupUserId int not null identity constraint PK_GroupUsers primary key,
  GroupId     int not null,
  UserId      int not null,

  constraint UK_GroupId_UserID unique(GroupId,UserId)
)
go

create table Module(
  ModuleId    int          not null identity constraint PK_Module primary key,
  Description varchar(250) not null
)
go

create table Participant(
  ParticipantID int          not null identity constraint PK_Participant primary key,
  DocID         int,
  AsNamed       varchar(350),
  FirstName     varchar(50),
  MiddleName    varchar(50),
  LastName      varchar(50),
  IsSeller      bit          not null
)
go

create index IX_AsNamed on Participant(AsNamed)
go

create table Permission(
  PermissionId int          not null identity constraint PK_Permission primary key,
  ModuleId     int          not null,
  Description  varchar(250),
  Code         varchar(50)  not null
)
go

create table PermissionAssignment(
  PermissionAssignmentId int not null identity constraint PK_PermissionPermissionGroup primary key,
  PermissionId           int not null,
  RoleId                 int not null
)
go

create table Project(
  ProjectId       int          not null identity primary key,
  Name            varchar(50)  not null,
  ShortName       varchar(25)  not null,
  ClientId        int          not null,
  ClientAccountId int          not null,
  ProjectStatusId int          not null,
  Description     varchar(250) not null
)
go

create table ProjectAttachment(
  ProjectAttachmentId     int not null identity primary key,
  ProjectId               int not null,
  ProjectAttachmentTypeId int not null,
  FileId                  int not null
)
go

create table ProjectAttachmentType(
  ProjectAttachmentTypeId int          not null identity primary key,
  Name                    varchar(50)  not null,
  Description             varchar(250) not null
)
go

create table ProjectStatus(
  ProjectStatusId int         not null identity primary key,
  Name            varchar(50) not null
)
go

create table ProjectTab(
  ProjectTabId int          not null identity primary key,
  ProjectId    int          not null,
  Name         varchar(50)  not null,
  Description  varchar(250) not null
)
go

create table ProjectTabDocument(
  ProjectTabDocumentId int          not null identity primary key,
  ProjectTabId         int          not null,
  DocumentId           int          not null,
  Description          varchar(250) not null
)
go

create table Role(
  RoleId int         not null identity constraint PK_PermissionGroup primary key,
  Name   varchar(50) not null
)
go

create table State(
  StateId   int         not null constraint PK_State primary key,
  Name      varchar(50),
  StateFips varchar(2),
  StateAbbr varchar(2)
)
go

create table TermUnit(
  TermUnitId int         not null identity constraint PK_TermUnit primary key,
  Name       varchar(50) not null
)
go

create table Tract(
  TractId     int           not null identity constraint PK_Tract primary key,
  Easting     int           not null,
  Northing    int           not null,
  RefName     varchar(100)  not null,
  CreatedBy   int           not null,
  IsDeleted   bit           not null,
  DocID       int,
  CalledAC    numeric(18,2) not null,
  UnitId      int           not null,
  DocIdUnique  as (isnull([DocId],(-1))),

  constraint ck_refNameDocId unique(RefName,DocIdUnique)
)
go

create table TractCall(
  TractCallId    int          not null identity constraint PK_TractCall primary key,
  TractId        int          not null,
  Type           varchar(15)  not null,
  Params         varchar(200) not null,
  [Order]        int          not null,
  CreatedByMouse bit          not null
)
go

create table TractTextObject(
  TractTextObjectId int            not null identity constraint PK_TractTextObject primary key,
  TractId           int            not null,
  Text              nvarchar(4000) not null,
  Easting           numeric(18,2)  not null,
  Northing          numeric(18,2)  not null,
  Rotation          numeric(18,2)  not null
)
go

create table Unit(
  UnitId    int         not null identity constraint PK_Unit primary key,
  Name      varchar(50),
  AcresRate float
)
go

create table [User](
  UserId          int          not null identity constraint PK_User primary key,
  Login           varchar(50)  not null,
  FirstName       varchar(50)  not null,
  LastName        varchar(50)  not null,
  PhoneNumber     varchar(50)  not null,
  Password        varchar(50)  not null,
  Email           varchar(50)  not null,
  IsActive        bit          not null,
  HackingAttempts int          not null,
  DefaultSite     varchar(250) not null,
  AssetId         int
)
go

create table UserRole(
  UserRoleId int not null identity constraint PK_UserPermissionGroup primary key,
  UserId     int not null,
  RoleId     int not null
)
go

alter table AssetAssignment add
  constraint FK_AssetAssignment_Asset foreign key(AssetId) references Asset(AssetId),
  constraint FK_AssetAssignment_Project foreign key(ProjectId) references Project(ProjectId) on delete cascade
go

alter table ClientAccount add
  constraint FK_ClientAccount_Client foreign key(ClientId) references Client(ClientId) on delete cascade
go

alter table County add
  constraint FK_County_State1 foreign key(StateId) references State(StateId)
go

alter table Document add
  constraint FK_Document_DocumentType foreign key(DocTypeId) references DocumentType(DocTypeID) on delete cascade,
  constraint FK_Document_User foreign key(CreatedBy) references [User](UserId)
go

alter table DocumentAttachment add
  constraint FK_DocumentAttachment foreign key(FileId) references [File](FileId),
  constraint FK_DocumentAttachment_Document foreign key(DocumentId) references Document(DocID),
  constraint FK_DocumentAttachment_DocumentAttachmentType foreign key(DocumentAttachmentTypeId) references DocumentAttachmentType(DocumentAttachmentTypeId)
go

alter table [File] add
  constraint FK_File_User foreign key(CreatedBy) references [User](UserId)
go

alter table GroupItem add
  constraint FK_GroupItem_Group foreign key(GroupId) references [Group](GroupId) on delete cascade
go

alter table GroupUser add
  constraint FK_UserGroup_User foreign key(UserId) references [User](UserId) on delete cascade,
  constraint FK_UserGroup_Group foreign key(GroupId) references [Group](GroupId) on delete cascade
go

alter table Lease add
  constraint FK_Lease_TermUnit foreign key(TermUnitId) references TermUnit(TermUnitId)
go

alter table LeaseEditHistory add
  constraint FK_LeaseEditHistory_Lease foreign key(LeaseId) references Lease(LeaseId) on delete cascade,
  constraint FK_LeaseEditHistory_User foreign key(UserId) references [User](UserId) on delete cascade
go

alter table Participant add
  constraint FK_Participant_Document foreign key(DocID) references Document(DocID) on delete cascade
go

alter table Permission add
  constraint FK_Permission_Module1 foreign key(ModuleId) references Module(ModuleId) on delete cascade
go

alter table PermissionAssignment add
  constraint FK_PermissionAssignment_Permission foreign key(PermissionId) references Permission(PermissionId) on delete cascade,
  constraint FK_PermissionAssignment_Role foreign key(RoleId) references Role(RoleId) on delete cascade
go

alter table Project add
  constraint FK_Project_ClientAccount foreign key(ClientAccountId) references ClientAccount(ClientAccountId),
  constraint FK_Project_ProjectStatus foreign key(ProjectStatusId) references ProjectStatus(ProjectStatusId),
  constraint FK_Project_Client foreign key(ClientId) references Client(ClientId)
go

alter table ProjectAttachment add
  constraint FK_ProjectAttachment_Project foreign key(ProjectId) references Project(ProjectId) on delete cascade,
  constraint FK_Project_ProjectAttachmentType foreign key(ProjectAttachmentTypeId) references ProjectAttachmentType(ProjectAttachmentTypeId),
  constraint FK_Project_File foreign key(FileId) references [File](FileId)
go

alter table ProjectTab add
  constraint FK_ProjectTab_Project foreign key(ProjectId) references Project(ProjectId) on delete cascade
go

alter table ProjectTabDocument add
  constraint FK_ProjectTabDocument_ProjectTab foreign key(ProjectTabId) references ProjectTab(ProjectTabId) on delete cascade,
  constraint FK_ProjectTabDocument_Document foreign key(DocumentId) references Document(DocID)
go

alter table Tract add
  constraint FK_Tract_User foreign key(CreatedBy) references [User](UserId),
  constraint FK_Tract_Document foreign key(DocID) references Document(DocID) on delete cascade
go

alter table TractCall add
  constraint FK_TractCall_Tract foreign key(TractId) references Tract(TractId) on delete cascade
go

alter table TractTextObject add
  constraint FK_TractTextObject_Tract foreign key(TractId) references Tract(TractId) on delete cascade
go

alter table [User] add
  constraint FK_User_Asset foreign key(AssetId) references Asset(AssetId)
go

alter table UserRole add
  constraint FK_UserRole_Role foreign key(RoleId) references Role(RoleId) on delete cascade,
  constraint FK_UserRole_User foreign key(UserId) references [User](UserId) on delete cascade
go

-- =============================================
-- Author:              Vitaly Vengrov
-- Create date: 2007/08/08
-- Description: Copyes Document and his related Participants and Tracts
-- =============================================
create PROCEDURE sp_CopyDocument
    @docId int, @userId int
AS
BEGIN
        SET NOCOUNT ON;

    BEGIN TRY

        BEGIN TRAN

        declare @copiedDocId int, @tractId int, @newTractId int

--        UPDATE DOCUMENT 
--            SET IsActive = 0 
--         WHERE docId = @docId

        INSERT INTO [Document]
                   ([IsPublic],[DocTypeId],[Volume],[Page],[DocumentNo],[County],[State],[Filed],[Signed]
                   ,[ResearchNote],[ImageLink],[CreatedBy],[Created],[IsActive],[DocBranchUid])
        SELECT [IsPublic],[DocTypeId],[Volume],[Page],[DocumentNo],[County],[State],[Filed],[Signed]
                   ,[ResearchNote],[ImageLink],[CreatedBy], [Created], 0, [DocBranchUid]
          FROM [Document]
         WHERE [DocId] = @docId

        SELECT @copiedDocId = @@IDENTITY

        INSERT INTO [Participant]
                   ([DocID],[AsNamed],[FirstName],[MiddleName],[LastName],[IsSeller])
        SELECT @copiedDocId,[AsNamed],[FirstName],[MiddleName],[LastName],[IsSeller]
          FROM [Participant]
         WHERE [DocId] = @docId

        INSERT INTO [DocumentAttachment]
                ([DocumentAttachmentTypeId], [DocumentId], [FileName], [FileUrl], [Description])
        SELECT [DocumentAttachmentTypeId], @copiedDocId, [FileName], [FileUrl], [Description]
          FROM [DocumentAttachment]
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

            INSERT INTO [Tract] ([Easting],[Northing],[RefName],[CreatedBy],[IsDeleted],[DocID],[CalledAC],[UnitId])
            SELECT [Easting],[Northing],[RefName],[CreatedBy],[IsDeleted],@copiedDocId,[CalledAC],[UnitId]
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
go


