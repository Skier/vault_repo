begin tran

--- 4
if exists (select * from dbo.sysobjects where id = object_id(N'[TeamAssignment]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [TeamAssignment]

if exists (select * from dbo.sysobjects where id = object_id(N'[TeamMember]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [TeamMember]

if exists (select * from dbo.sysobjects where id = object_id(N'[Team]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [Team]

if exists (select * from dbo.sysobjects where id = object_id(N'[AssetRate]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [AssetRate]

if exists (select * from dbo.sysobjects where id = object_id(N'[BillItemType]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [BillItemType]

if exists (select * from dbo.sysobjects where id = object_id(N'[Asset]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [Asset]

if exists (select * from dbo.sysobjects where id = object_id(N'[AssetType]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [AssetType]


---
if exists (select * from dbo.sysobjects where id = object_id(N'[County]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [County]

if exists (select * from dbo.sysobjects where id = object_id(N'[State]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [State]

---
---
---
if exists (select * from dbo.sysobjects where id = object_id(N'[Project]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [Project]

if exists (select * from dbo.sysobjects where id = object_id(N'[ProjectStatus]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [ProjectStatus]

if exists (select * from dbo.sysobjects where id = object_id(N'[ContractRate]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [ContractRate]

if exists (select * from dbo.sysobjects where id = object_id(N'[InvoiceItemType]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [InvoiceItemType]

if exists (select * from dbo.sysobjects where id = object_id(N'[CompanyContact]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [CompanyContact]

if exists (select * from dbo.sysobjects where id = object_id(N'[ClientContact]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [ClientContact]

if exists (select * from dbo.sysobjects where id = object_id(N'[Contract]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [Contract]

if exists (select * from dbo.sysobjects where id = object_id(N'[ContractStatus]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [ContractStatus]

if exists (select * from dbo.sysobjects where id = object_id(N'[Account]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [Account]

if exists (select * from dbo.sysobjects where id = object_id(N'[AccountType]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [AccountType]

--- 1
if exists (select * from dbo.sysobjects where id = object_id(N'[PermissionAssignment]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [PermissionAssignment]

if exists (select * from dbo.sysobjects where id = object_id(N'[Permission]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [Permission]

if exists (select * from dbo.sysobjects where id = object_id(N'[UserRole]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [UserRole]

if exists (select * from dbo.sysobjects where id = object_id(N'[Module]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [Module]

if exists (select * from dbo.sysobjects where id = object_id(N'[ModuleType]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [ModuleType]

if exists (select * from dbo.sysobjects where id = object_id(N'[Role]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [Role]

if exists (select * from dbo.sysobjects where id = object_id(N'[UserPreference]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [UserPreference]

if exists (select * from dbo.sysobjects where id = object_id(N'[User]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [User]

if exists (select * from dbo.sysobjects where id = object_id(N'[Person]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [Person]

if exists (select * from dbo.sysobjects where id = object_id(N'[ClientCompany]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [ClientCompany]

if exists (select * from dbo.sysobjects where id = object_id(N'[Client]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [Client]

if exists (select * from dbo.sysobjects where id = object_id(N'[Company]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [Company]


--- 1
create table ModuleType(
    ModuleTypeId   int identity(1, 1)  not null,
    TypeName        varchar(50)         not null,
    constraint PK_ModuleType primary key(ModuleTypeId)
)

create table Module(
  ModuleId     int          not null identity,  
  ModuleTypeId int          not null,
  ShortName   varchar(20)  null,
  Description varchar(250) null,
  Url         varchar(250) not null,
  constraint PK_Module primary key(ModuleId)
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

create table [Company](
    CompanyId   int identity(1, 1)  not null,
    CompanyName varchar(50)         not null,
    constraint PK_Company primary key(CompanyId)
)

create table [Client](
    ClientId        int identity(1, 1)  not null,
    ClientName      varchar(50)         not null,
    ClientAddress   varchar(250)            null,
    constraint PK_Client primary key(ClientId)
)

create table ClientCompany(
    ClientCompanyId int identity(1, 1) not null, 
    ClientId        int not null,
    CompanyId       int not null,
    constraint PK_ClientCompany primary key(ClientCompanyId)
)

create table [Person](
    PersonId    int identity(1, 1)  not null,
    ClientId    int                     null,
    CompanyId    int                    null,
    FirstName   varchar(50)             null,
    MiddleName  varchar(50)             null,
    LastName    varchar(50)             null,
    PhoneNumber varchar(50)             null,
    Email       varchar(50)         not null,
    SSN         varchar(50)             null,
    constraint PK_Person primary key(PersonId)
)

create table [User](
  UserId          int         not null identity constraint PK_User primary key,
  PersonId        int         not null,
  Login           varchar(50) not null,
  Password        varchar(50) not null,
  IsActive        bit         not null default (1),
  HackingAttempts int         not null default (0),
)


create table [UserPreference](
  UserPereferenceId          int         not null identity constraint PK_UserPreference primary key,
  UserId                     int         not null,
  DefaultSite                varchar(50) not null,
  NewTracts                  int         not null default (0)
)

create table UserRole(
  UserRoleId int not null identity constraint PK_UserPermissionGroup primary key,
  UserId     int not null,
  RoleId     int not null
)

---
---
create table [AccountType](
    AccountTypeId   int identity(1, 1)  not null,
    TypeName        varchar(50)         not null,
    constraint PK_AccouuntType primary key(AccountTypeId)
)

create table [Account](
    AccountId       int identity(1, 1)  not null,
    AccountName     varchar(50)         not null,
    ParentAccountId int                     null,
    AccountTypeId   int                 not null,
    CompanyId       int                 not null,
    ClientId        int                 not null,
    constraint PK_Account primary key(AccountId)
)

create table [ContractStatus](
    ContractStatusId   int identity(1, 1)  not null,
    StatusName        varchar(50)         not null,
    constraint PK_ContractStatus primary key(ContractStatusId)
)

create table [Contract](
    ContractId      int identity(1, 1)  not null,
    ClientId        int                 not null,
    CompanyId       int                 not null,
    ContractStatusId        int                 not null,
    ContractName    varchar(50)         not null,
    StartDate       datetime                null,
    EndDate         datetime                null,
    constraint PK_Contract primary key(ContractId)
)

create table [ClientContact](
    ClientContactId int identity(1, 1)  not null,
    PersonId        int                 not null,
    ContractId      int                 not null,
    StartDate       datetime            not null,
    EndDate         datetime                null,
    constraint PK_ClientContact primary key(ClientContactId)
)

create table [CompanyContact](
    CompanyContactId int identity(1, 1)  not null,
    PersonId        int                 not null,
    ContractId      int                 not null,
    StartDate       datetime            not null,
    EndDate         datetime                null,
    constraint PK_CompanyContact primary key(CompanyContactId)
)

create table [InvoiceItemType](
    InvoiceItemTypeId   int identity(1, 1)  not null,
    TypeName            varchar(50)         not null,
    IsCountable         bit                 not null,
    IsPresetRate        bit                 not null,
    IsSingle            bit                 not null,
    IsAttachRequired    bit                 not null,
    constraint PK_InvoiceItemType primary key(InvoiceItemTypeId)
)

create table [ContractRate](
    ContractRateId    int identity(1, 1)  not null,
    ContractId              int                 not null,
    InvoiceItemTypeId       int                 not null,
    Rate                    numeric(19, 3)      not null,
    constraint PK_InvoiceDefaultRate primary key(ContractRateId)
)

create table [ProjectStatus](
    ProjectStatusId     int identity(1, 1)  not null,
    StatusName          varchar(50)             null,
    constraint PK_ProjectStatus primary key(ProjectStatusId)
)

create table [Project](
    ProjectId       int identity(1, 1)  not null,
    ContractId      int                 not null,
    AccountId       int                     null,
    ProjectStatusId int                 not null,
    ProjectName     varchar(50)         not null,
    ShortName       varchar(50)         not null,
    Description     varchar(250)        not null,
    CreatedBy       varchar(50)         not null,
    constraint PK_Project primary key(ProjectId)
)

---
---
---
create table County(
  CountyId   int         not null identity constraint PK_County primary key,
  [Name]     varchar(50),
  StateId    int         not null,
  StateName  varchar(50),
  StateFips  varchar(2),
  CountyFips varchar(3),
  Fips       varchar(5)
)

create table State(
  StateId   int         not null constraint PK_State primary key,
  [Name]    varchar(50),
  StateFips varchar(2),
  StateAbbr varchar(2)
)

--- 4
create table AssetType(
  AssetTypeId int identity(1,1) not null,
  TypeName    varchar(50),
  constraint PK_AssetType primary key(AssetTypeId)
)

create table Asset(
  AssetId     int identity(1,1) not null, 
  AssetTypeId int not null,
  CompanyId   int not null,
  PersonId    int     null,
  AssetName   varchar(50) not null,
  constraint PK_Asset primary key(AssetId)
)

create table BillItemType(
  BillItemTypeId    int identity(1,1)   not null,
  InvoiceItemTypeId int                 not null,
  TypeName          varchar(50)         not null,
  constraint PK_BillItemType primary key(BillItemTypeId)
)

create table AssetRate(
  AssetRateId int identity(1,1) not null,
  AssetId             int           not null,
  BillItemTypeId      int           not null,
  ContractId          int               null,
  Rate                numeric(19,3) not null
  constraint PK_AssetRate primary key(AssetRateId)
)

create table Team(
  TeamId   int identity(1,1) not null, 
  CompanyId int not null,
  ParentTeamId int null,
  TeamName varchar(50) not null,
  constraint PK_Team primary key(TeamId)
)

create table TeamMember(
  TeamMemberId int identity(1,1) not null,
  TeamId       int      not null,
  AssetId      int      not null,
  StartDate    datetime not null,
  EndDate      datetime null,
  constraint PK_TeamMember primary key(TeamMemberId)
)

create table TeamAssignment(
  TeamAssignmentId int identity(1,1) not null, 
  TeamId            int      not null,
  ProjectId         int      not null,
  LeadAssetId       int      not null,
  StartDate         datetime not null,
  EndDate           datetime null,
  constraint PK_TeamAssignment primary key(TeamAssignmentId)
)

--- 1
alter table ClientCompany add
  constraint FK_ClientCompany_Client foreign key(ClientId) references Client(ClientId) on update cascade on delete cascade,
  constraint FK_ClientCompany_Company foreign key(CompanyId) references Company(CompanyId) on update cascade on delete cascade

alter table Person add
  constraint FK_Person_Client foreign key(ClientId) references Client(ClientId) on update cascade on delete cascade,
  constraint FK_Person_Company foreign key(CompanyId) references Company(CompanyId) on update cascade on delete cascade

alter table [User] add
  constraint FK_User_Person foreign key(PersonId) references Person(PersonId) on update cascade on delete cascade

alter table UserPreference add
  constraint FK_UserPreference_User foreign key(UserId) references [User](UserId) on update cascade on delete cascade

alter table UserRole add
  constraint FK_UserRole_Role foreign key(RoleId) references Role(RoleId) on update cascade on delete cascade,
  constraint FK_UserRole_User foreign key(UserId) references [User](UserId) on update cascade on delete cascade

alter table PermissionAssignment add
  constraint FK_PermissionAssignment_Permission foreign key(PermissionId) references Permission(PermissionId) on update cascade on delete cascade,
  constraint FK_PermissionAssignment_Role foreign key(RoleId) references Role(RoleId) on update cascade on delete cascade

alter table Permission add
  constraint FK_Permission_Module foreign key(ModuleId) references Module(ModuleId) on update cascade on delete cascade

alter table County add
  constraint FK_County_State foreign key(StateId) references State(StateId)

---
---
alter table [Account] add
    constraint FK_Account_AccountType foreign key(AccountTypeId)
        references [AccountType](AccountTypeId),
    constraint FK_Account_Company foreign key(CompanyId)
        references [Company](CompanyId),
    constraint FK_Account_Client foreign key(ClientId)
        references [Client](ClientId),
    constraint FK_Account_Account foreign key(ParentAccountId)
        references [Account](AccountId)

alter table [Contract] add
    constraint FK_Contract_ContractStatus foreign key(ContractStatusId)
        references [ContractStatus](ContractStatusId),
    constraint FK_Contract_Client foreign key(ClientId)
        references [Client](ClientId),
    constraint FK_Contract_Company foreign key(CompanyId)
        references [Company](CompanyId)

alter table [ClientContact] add
    constraint FK_ClientContact_Contract foreign key(ContractId)
        references [Contract](ContractId),
    constraint FK_ClientContact_Person foreign key(PersonId)
        references [Person](PersonId)

alter table [CompanyContact] add
    constraint FK_CompanyContact_Contract foreign key(ContractId)
        references [Contract](ContractId),
    constraint FK_CompanyContact_Person foreign key(PersonId)
        references [Person](PersonId)

alter table [ContractRate] add
    constraint FK_ContractRate_Contract foreign key(ContractId)
        references [Contract](ContractId),
    constraint FK_ContractRate_InvoiceItemType foreign key(InvoiceItemTypeId)
        references [InvoiceItemType](InvoiceItemTypeId),
    constraint FK_ContractRate_Unique1 unique (InvoiceItemTypeId,ContractId)

alter table [Project] add
    constraint FK_Project_Contract foreign key(ContractId)
        references [Contract](ContractId),
    constraint FK_Project_Account foreign key(AccountId)
        references [Account](AccountId),
    constraint FK_Project_ProjectStatus foreign key(ProjectStatusId)
        references [ProjectStatus](ProjectStatusId)

--- 4
alter table Asset add
  constraint FK_Asset_AssetType foreign key(AssetTypeId) references AssetType(AssetTypeId),
  constraint FK_Asset_Company foreign key(CompanyId) references Company(CompanyId),
  constraint FK_Asset_Person foreign key(PersonId) references Person(PersonId)

alter table BillItemType add
  constraint FK_BillItemType_InvoiceItemType foreign key(InvoiceItemTypeId) references InvoiceItemType(InvoiceItemTypeId)

alter table AssetRate add
  constraint FK_AssetRate_BillItemType foreign key(BillItemTypeId) references BillItemType(BillItemTypeId),
  constraint FK_AssetRate_Contract foreign key(ContractId) references Contract(ContractId),
  constraint FK_AssetRate_Asset foreign key(AssetId) references Asset(AssetId)

alter table Team add
  constraint FK_Team_Team foreign key(ParentTeamId) references Team(TeamId),
  constraint FK_Team_Company foreign key(CompanyId) references Company(CompanyId)

alter table TeamMember add
  constraint FK_TeamMember_Team foreign key(TeamId) references Team(TeamId),
  constraint FK_TeamMember_Asset foreign key(AssetId) references Asset(AssetId)

alter table TeamAssignment add
  constraint FK_TeamAssignment_Team foreign key(TeamId) references Team(TeamId),
  constraint FK_TeamAssignment_Asset foreign key(LeadAssetId) references Asset(AssetId),
  constraint FK_TeamAssignment_Project foreign key(ProjectId) references Project(ProjectId)

commit
go
