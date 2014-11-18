set xact_abort on
go

begin transaction

if exists (select * from dbo.sysobjects where id = object_id(N'[TractCallTypes]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [TractCallTypes]

if exists (select * from dbo.sysobjects where id = object_id(N'[UserTractHistory]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [UserTractHistory]

if exists (select * from dbo.sysobjects where id = object_id(N'[Participant]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [Participant]

if exists (select * from dbo.sysobjects where id = object_id(N'[HistoryLog]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [HistoryLog]

if exists (select * from dbo.sysobjects where id = object_id(N'[TractTextObjectsBackup]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [TractTextObjectsBackup]

if exists (select * from dbo.sysobjects where id = object_id(N'[TractCallsBackup]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [TractCallsBackup]

if exists (select * from dbo.sysobjects where id = object_id(N'[TractBackup]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [TractBackup]

if exists (select * from dbo.sysobjects where id = object_id(N'[County]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [County]

if exists (select * from dbo.sysobjects where id = object_id(N'[State]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [State]

if exists (select * from dbo.sysobjects where id = object_id(N'[LeaseEditHistory]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [LeaseEditHistory]

if exists (select * from dbo.sysobjects where id = object_id(N'[Lease]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [Lease]

if exists (select * from dbo.sysobjects where id = object_id(N'[TermUnit]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [TermUnit]

if exists (select * from dbo.sysobjects where id = object_id(N'[TractTextObjects]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [TractTextObjects]

if exists (select * from dbo.sysobjects where id = object_id(N'[TractCalls]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [TractCalls]

if exists (select * from dbo.sysobjects where id = object_id(N'[Tract]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [Tract]

if exists (select * from dbo.sysobjects where id = object_id(N'[PermissionAssignment]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [PermissionAssignment]

if exists (select * from dbo.sysobjects where id = object_id(N'[Permission]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [Permission]

if exists (select * from dbo.sysobjects where id = object_id(N'[UserRole]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [UserRole]

if exists (select * from dbo.sysobjects where id = object_id(N'[Module]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [Module]

if exists (select * from dbo.sysobjects where id = object_id(N'[Role]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [Role]

if exists (select * from dbo.sysobjects where id = object_id(N'[Document]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [Document]

if exists (select * from dbo.sysobjects where id = object_id(N'[User]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [User]

if exists (select * from dbo.sysobjects where id = object_id(N'[DocumentType]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [DocumentType]

if exists (select * from dbo.sysobjects where id = object_id(N'[Unit]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [Unit]

create table Document (
    DocID           int          not null identity constraint PK_Document primary key,
    IsPublic        bit          not null,
    DocTypeId       int          not null,
    Volume          varchar (50) null,
    Page            varchar (50) null,
    DocumentNo      varchar (50) null,
    County          int          not null,
    State           int          not null,
    DateFiledYear   int,
    DateFiledMonth  int,
    DateFiledDay    int,
    DateSignedYear  int,
    DateSignedMonth int,
    DateSignedDay   int,
    ResearchNote    varchar (350) null,
    ImageLink       varchar (350) null,
    CreatedBy       int not null,
    DateModified    datetime not null,
    DocPlace        as 
        (case 
            when documentNo is null or len(documentNo) = 0 then 'Vol: ' + Volume + ', Pg:' + Page 
            else 'DocNo: ' + documentNo
        end)

    constraint ck_documentKeyFields unique ([State], [County], [DocTypeId], DocPlace)
)

create table DocumentType (
    DocTypeID        int          not null identity constraint PK_DocumentType primary key,
    Name             varchar (50),
    GiverRequired    bit not null,
    ReceiverRequired bit not null,
    GiverRoleName    varchar (50) null,
    ReceiverRoleName varchar (50) null 
) 

CREATE TABLE [dbo].[Participant](
 [ParticipantID] [int] IDENTITY(1,1) NOT NULL constraint PK_Participant primary key,
 [DocID] [int] NULL,
 [AsNamed] [varchar] (350) NULL,
 [FirstName] [varchar](50) NULL,
 [MiddleName] [varchar](50) NULL,
 [LastName] [varchar](50) NULL,
 [IsSeller] [bit] NOT NULL 
)

create table Module(
  ModuleId    int          not null identity constraint PK_Module primary key,
  Description varchar(250) not null
)

create table Permission(
  PermissionId int          not null identity constraint PK_Permission primary key,
  ModuleId     int          not null,
  Description  varchar(250) ,
  Code         varchar(50)  not null
)

create table PermissionAssignment(
  PermissionAssignmentId int not null identity constraint PK_PermissionPermissionGroup primary key,
  PermissionId           int not null,
  RoleId                 int not null
)

create table Role(
  RoleId int         not null identity constraint PK_PermissionGroup primary key,
  [Name] varchar(50) not null
)

create table Tract(
  TractId     int              not null identity constraint PK_Tract primary key,
  Easting     int              not null,
  Northing    int              not null,
  RefName     varchar(100)     not null,
  CreatedBy   int              not null,
  IsDeleted   bit              not null constraint DF_Tract_IsDeleted default (0),
  DocID       int              null,
  CalledAC    numeric(18,2)    not null,
  UnitId      int              not null,
  DocIdUnique as (isnull(DocId, -1)),
  timestamp                    not null,
  constraint ck_refNameDocId   unique ([RefName], [DocIdUnique])
)

create table TractCalls(
  TractCallId    int          not null identity constraint PK_TractCalls primary key,
  TractId        int          not null,
  CallType       varchar(15)  not null,
  CallDBValue    varchar(200) not null,
  CallOrder      int          not null,
  CreatedByMouse bit          not null
)

create table TractTextObjects(
  TractTextObjectId    int            not null identity constraint PK_TractTextObjects primary key,
  TractId              int            not null,
  [Text]               nvarchar(4000) not null,
  Easting              numeric(18,2)  not null,
  Northing             numeric(18,2)  not null,
  Rotation             numeric(18,2)  not null
)

create table [User](
  UserId          int         not null identity constraint PK_User primary key,
  Login           varchar(50) not null,
  FirstName       varchar(50) not null,
  LastName        varchar(50) not null,
  PhoneNumber     varchar(50) not null,
  Password        varchar(50) not null,
  Email           varchar(50) not null,
  IsActive        bit         not null default (1),
  HackingAttempts int         not null default (0),
  NewTracts       int         not null default (0)
)

create table [UserTractHistory] (
  UserTractHistoryId int not null identity constraint PK_UserTractHistory primary key,
  UserId int not null,
  TractId int not null,
  AccessDate datetime not null,
)

create table UserRole(
  UserRoleId int not null identity constraint PK_UserPermissionGroup primary key,
  UserId     int not null,
  RoleId     int not null
)

create table Unit (
    UnitId  int          not null identity constraint PK_Unit primary key,
    Name    varchar (50)  
)

create table County(
  CountyId   int         not null identity constraint PK_County primary key,
  [Name]     varchar(50),
  StateId    int         not null,
  StateName  varchar(50),
  StateFips  varchar(2),
  CountyFips varchar(3),
  Fips       varchar(5)
)

create table Lease(
  LeaseId         int            not null identity constraint PK_Lease primary key,
  LCN             varchar(50),
  DocumentNumber  varchar(50),
  Volume          varchar(50),
  PAGE            varchar(50),
  LeaseeName      varchar(50),
  AssigneeName    varchar(50),
  LeassorName     varchar(50),
  AssignorName    varchar(50),
  StateFips       varchar(2),
  CountyFips      varchar(5),
  UnitDepth       numeric(18,0),
  FromDepth       numeric(18,0),
  FromFrom        numeric(18,0),
  ToDepth         numeric(18,0),
  ToFrom          numeric(18,0),
  WorkInt         varchar(50),
  OrrInt          varchar(50),
  NetAcres        numeric(18,0),
  GrossAcres      numeric(18,0),
  NriAssign       varchar(50),
  RcdDate         datetime,
  Term            numeric(18,0),
  TermUnitId      int,
  HBR             bit            not null,
  Encumbrances    bit            not null,
  EffDate         datetime,
  PughClause      bit            not null,
  DepthLimitation bit            not null,
  ShutInClau      bit            not null,
  PoolingClau     bit            not null,
  MinimumPmt      numeric(18,0),
  Author          int,
  Status          varchar(10)
)

create table LeaseEditHistory(
  EditHistoryId int      not null identity constraint PK_LeaseEditHistory primary key,
  UserId        int      not null,
  LeaseId       int      not null,
  DateEdited    datetime not null,
  Status        varchar(10)
)

create table State(
  StateId   int         not null constraint PK_State primary key,
  [Name]    varchar(50),
  StateFips varchar(2),
  StateAbbr varchar(2)
)

create table TermUnit(
  TermUnitId    int         not null identity constraint PK_TermUnit primary key,
  Name          varchar(50) not null
)

create table HistoryLog(
  HistoryLogId  int         not null identity constraint PK_HistoryLog primary key,
  SourceTableId int         not null,
  BackupTableId int         not null,
  SourceItemId  int         not null,
  BackupItemId  int         not null,
  ItemVersion   int         not null,
  LogDate       datetime    not null,
  UserId        int         not null,
  Description   varchar(50) not null
)

create table TractBackup(
  TractId     int              not null identity constraint PK_TractBackup primary key,
  Easting     int              not null,
  Northing    int              not null,
  RefName     varchar(100)     not null,
  CreatedBy   int              not null,
  IsDeleted   bit              not null,
  DocID       int              null,
  CalledAC    numeric(18,2)    not null,
  UnitId      int              not null
)

create table TractCallsBackup(
  TractCallId    int          not null identity constraint PK_TractCallsBackup primary key,
  TractId        int          not null,
  CallType       varchar(15)  not null,
  CallDBValue    varchar(200) not null,
  CallOrder      int          not null,
  CreatedByMouse bit          not null
)

create table TractTextObjectsBackup(
  TractTextObjectId    int            not null identity constraint PK_TractTextObjectsBackup primary key,
  TractId              int            not null,
  [Text]               nvarchar(4000) not null,
  Easting              numeric(18,2)  not null,
  Northing             numeric(18,2)  not null,
  Rotation             int            not null
)

create table DocumentAttachment (
  DocumentAttachmentId int          not null identity constraint PK_DocumentAttachment primary key,
  DocId                int          not null,
  FileName             varchar(350) not null,
  OriginalFileName     varchar(255) not null
)

alter table UserRole add
  constraint FK_UserRole_Role foreign key(RoleId) references Role(RoleId) on delete cascade,
  constraint FK_UserRole_User foreign key(UserId) references [User](UserId) on delete cascade

alter table Tract add
  constraint FK_Tract_User foreign key(CreatedBy) references [User](UserId),
  constraint FK_Tract_Document foreign key(DocId) references [Document](DocId) on delete cascade

alter table UserTractHistory add
  constraint FK_UserTractHistory_User foreign key(UserId) references [User] (UserId)  on delete cascade,
  constraint FK_UserTractHistory_Tract foreign key(TractId) references [Tract] (TractId)

alter table Document add
  constraint FK_Document_DocumentType foreign key(DocTypeId) references [DocumentType](DocTypeId) on delete cascade,
  constraint FK_Document_User foreign key (CreatedBy) references [User](UserId)

alter table TractCalls add
  constraint FK_TractCalls_Tract foreign key(TractId) references Tract(TractId) on delete cascade

alter table TractTextObjects add
  constraint FK_TractTextObjects_Tract foreign key(TractId) references Tract(TractId) on delete cascade

alter table TractCallsBackup add
  constraint FK_TractCallsBackup_TractBackup foreign key(TractId) references TractBackup(TractId) on delete cascade

alter table TractTextObjectsBackup add
  constraint FK_TractTextObjectsBackup_TractBackup foreign key(TractId) references TractBackup(TractId) on delete cascade

alter table PermissionAssignment add
  constraint FK_PermissionAssignment_Permission foreign key(PermissionId) references Permission(PermissionId) on delete cascade,
  constraint FK_PermissionAssignment_Role foreign key(RoleId) references Role(RoleId) on delete cascade

alter table Permission add
  constraint FK_Permission_Module1 foreign key(ModuleId) references Module(ModuleId) on delete cascade

alter table Lease add
  constraint FK_Lease_TermUnit foreign key(TermUnitId) references TermUnit(TermUnitId)

alter table LeaseEditHistory add
  constraint FK_LeaseEditHistory_Lease foreign key(LeaseId) references Lease(LeaseId) on delete cascade,
  constraint FK_LeaseEditHistory_User foreign key(UserId) references [User](UserId) on delete cascade

alter table County add
  constraint FK_County_State1 foreign key(StateId) references State(StateId)

alter table Participant add
  constraint FK_Participant_Document foreign key(DocID) references [Document](DocID) on delete cascade

alter table DocumentAttachment add
  constraint FK_DocumentAttachment_Document foreign key(DocId) references Document(DocId) on update cascade on delete cascade

commit
go



