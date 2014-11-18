set xact_abort on
go

begin transaction
go

if exists (select * from dbo.sysobjects where id = object_id(N'[County]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [County]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[State]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [State]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[LeaseEditHistory]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [LeaseEditHistory]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[Lease]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [Lease]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[TermUnit]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [TermUnit]
GO

commit transaction
go


set xact_abort on
go

begin transaction
go

create table County(
  CountyId   int         not null identity constraint PK_County primary key,
  [Name]     varchar(50),
  StateId    int         not null,
  StateName  varchar(50),
  StateFips  varchar(2),
  CountyFips varchar(3),
  Fips       varchar(5)
)
go

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
go

create table LeaseEditHistory(
  EditHistoryId int      not null identity constraint PK_LeaseEditHistory primary key,
  UserId        int      not null,
  LeaseId       int      not null,
  DateEdited    datetime not null,
  Status        varchar(10)
)
go

create table State(
  StateId   int         not null constraint PK_State primary key,
  [Name]    varchar(50),
  StateFips varchar(2),
  StateAbbr varchar(2)
)
go

create table TermUnit(
  TermUnitId    int         not null identity constraint PK_TermUnit primary key,
  Name          varchar(50) not null
)
go

alter table Lease add
  constraint FK_Lease_TermUnit foreign key(TermUnitId) references TermUnit(TermUnitId)
go

alter table LeaseEditHistory add
  constraint FK_LeaseEditHistory_Lease foreign key(LeaseId) references Lease(LeaseId) on delete cascade,
  constraint FK_LeaseEditHistory_User foreign key(UserId) references [User](UserId) on delete cascade
go

alter table County add
  constraint FK_County_State1 foreign key(StateId) references State(StateId)
go

commit transaction
go
