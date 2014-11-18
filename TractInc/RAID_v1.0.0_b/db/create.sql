CREATE TABLE Afe (
    AFE varchar(10) NOT NULL,
    ClientId int NOT NULL,
    AFEName varchar(45) NOT NULL,
    AFEStatus varchar(10) NOT NULL,
    Deleted bit NOT NULL,
    PRIMARY KEY  (AFE)
)
go

CREATE TABLE AfeStatus (
    AFEStatus varchar(10) NOT NULL,
    PRIMARY KEY  (AFEStatus)
)
go

CREATE TABLE Asset (
    AssetId int identity NOT NULL,
    [Type] varchar(10) NOT NULL,
    ChiefAssetId int NOT NULL,
    BusinessName varchar(45) NOT NULL,
    FirstName varchar(45) NOT NULL,
    MiddleName varchar(45) NOT NULL,
    LastName varchar(45) NOT NULL,
    SSN varchar(9) NOT NULL,
    Deleted bit NOT NULL,
    PRIMARY KEY  (AssetId)
)
go

CREATE TABLE AssetAssignment (
    AssetAssignmentId int identity NOT NULL,
    AFE varchar(10) NOT NULL,
    SubAFE varchar(50) NOT NULL,
    AssetId int NOT NULL,
    Deleted bit NOT NULL,
    PRIMARY KEY  (AssetAssignmentId)
)
go

CREATE TABLE AssetType (
    [Type] varchar(10) NOT NULL,
    PRIMARY KEY ([Type])
)
go

CREATE TABLE Bill (
    BillId int identity NOT NULL,
    Status varchar(10) NOT NULL,
    Notes varchar(255) NOT NULL,
    StartDate varchar(10) NOT NULL,
    AssetId int NOT NULL,
    TotalDailyBill int not null,
    DailyBillAmt numeric(19,3) not null,
    OtherBillAmt numeric(19,3) not null,
    TotalBillAmt numeric(19,3) not null,
    PRIMARY KEY  (BillId)
)


CREATE TABLE Invoice (
    InvoiceId int identity NOT NULL,
    InvoiceNumber varchar(50) NULL,
    ClientId int NOT NULL,
    ClientName varchar(45) NOT NULL,
    ClientAddress varchar(255) NOT NULL,
    ClientActive bit NOT NULL,
    Status varchar(10) NOT NULL,
    Notes varchar(255),
    StartDate varchar(10) NOT NULL,
    TotalDailyAmt int not null,
    DailyInvoiceAmt numeric(19,3) not null,
    OtherInvoiceAmt numeric(19,3) not null,
    TotalInvoiceAmt numeric(19,3) not null,
    PRIMARY KEY  (InvoiceId)
)


CREATE TABLE InvoiceStatus (
    Status varchar(10) NOT NULL,
    PRIMARY KEY  (Status)
)

CREATE TABLE BillStatus (
    Status varchar(10) NOT NULL,
    PRIMARY KEY  (Status)
)
go

create table BillItemType(
    BillItemTypeId int identity NOT NULL,
    InvoiceItemTypeId int NOT NULL,
    [Name] varchar(20),
    IsCountable bit,
    IsPresetRate bit,
    IsSingle bit,
    IsAttachRequired bit,
    Deleted bit NOT NULL,
    primary key (BillItemTypeId)
)

create table InvoiceItemType(
    InvoiceItemTypeId int identity NOT NULL,
    [Name] varchar(20),
    IsCountable bit,
    IsPresetRate bit,
    IsSingle bit,
    Deleted bit NOT NULL,
    primary key (InvoiceItemTypeId)
)

create table RateByAssignment(
    RateByAssignmentId int not null identity,
    AssetAssignmentId int not null,
    BillItemTypeId int not null,
    BillRate numeric(19,3) not null,
    InvoiceRate numeric(19,3) not null,
    ShouldNotExceedRate bit NOT NULL default 0,
    Deleted bit NOT NULL,
    primary key (RateByAssignmentId)
)

CREATE TABLE BillItem (
    BillItemId int identity NOT NULL,
    BillItemTypeId int not null,
    BillId int NOT NULL,
    AssetAssignmentId int NOT NULL,
    BillingDate varchar(10) NOT NULL,
    Qty int,
    BillRate numeric(19,3),
    Status varchar(10) NOT NULL,
    Notes varchar(255),
    PRIMARY KEY  (BillItemId)
)

CREATE TABLE InvoiceItem (
    InvoiceItemId int identity NOT NULL,
    InvoiceItemTypeId int not null,
    InvoiceId int not null, -- fk to invoice
    BillItemId int, -- FK to billitem
    AssetAssignmentId int NOT NULL,
    InvoiceDate varchar(10) NOT NULL,
    Qty int,
    InvoiceRate numeric(19,3),
    Status varchar(10) NOT NULL,
    Notes varchar(255),
    IsSelected bit not null,
    PRIMARY KEY  (InvoiceItemId)
)

create table BillItemAttachment (
    BillItemAttachmentId int identity not null,
    BillItemId int not null,
    FileName varchar(350) not null,
    OriginalFileName varchar(255) not null,
    primary key (BillItemAttachmentId)
)

CREATE TABLE BillItemStatus (
    Status varchar(10) NOT NULL,
    PRIMARY KEY  (Status)
)
go

CREATE TABLE InvoiceItemStatus (
    Status varchar(10) NOT NULL,
    PRIMARY KEY  (Status)
)
go

CREATE TABLE Client (
    ClientId int identity NOT NULL,
    ClientName varchar(45) NOT NULL,
    ClientAddress varchar(255) NOT NULL,
    Active bit NOT NULL,
    Deleted bit NOT NULL,
    PRIMARY KEY  (ClientId)
)
go


CREATE TABLE SubAfe (
    SubAFE       varchar(50) NOT NULL,
    AFE          varchar(10) NOT NULL,
    SubAFEStatus varchar(10) NOT NULL,
    ShortName    varchar(10) NOT NULL,
    Deleted      bit         NOT NULL,
    [Temporary]  bit         NOT NULL default 0,
    PRIMARY KEY  (SubAFE)
)
go

CREATE TABLE SubAfeStatus (
    SubAFEStatus varchar(10) NOT NULL,
    PRIMARY KEY  (SubAFEStatus)
)
go

CREATE TABLE SyncLog (
    SyncLogId   int identity NOT NULL,
    AssetId     int NOT NULL,
    DeviceId    varchar(50) NOT NULL,
    PRIMARY KEY  (SyncLogId)
)
go

create table Module(
    ModuleId    int          not null identity constraint PK_Module primary key,
    Description varchar(250) collate Ukrainian_CI_AS not null
)

create table Permission(
    PermissionId int          not null identity constraint PK_Permission primary key,
    ModuleId     int          not null,
    Description  varchar(250) collate Ukrainian_CI_AS,
    Code         varchar(50)  collate Ukrainian_CI_AS not null
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

create table [User](
    UserId          int         not null identity constraint PK_User primary key,
    Login           varchar(50) not null,
    Password        varchar(50) not null,
    Email           varchar(50) not null,
    IsActive        bit         not null default (1),
    HackingAttempts int         not null default (0),
    Deleted       bit         not null
)

create table UserRole(
    UserRoleId int not null identity constraint PK_UserPermissionGroup primary key,
    UserId     int not null,
    RoleId     int not null
)

create table UserAsset(
    UserAssetId int         not null identity constraint PK_UserAsset primary key,
    UserId      int         not null,
    AssetId     int         not null,
    Deleted bit NOT NULL
)

create table Message(
    MessageId       int           not null identity constraint PK_Message primary key,
    SenderUserId    int           not null,
    ReceiverUserId  int           not null,
    Posted          datetime      not null,
    Subject         varchar(250)  not null,
    Body            varchar(1000) not null,
    IsRead          bit           not null default (0)
)

create table Note(
    NoteId        int identity not null,
    RelatedItemId int          not null,
    ItemType      varchar(50)  not null,
    SenderId      int          not null,
    Posted        datetime     not null,
    NoteText      varchar(255) not null,
    primary key (NoteId)
)

alter table Message add
    constraint FK_Message_SenderUser foreign key(SenderUserId) references [User](UserId)
        on update no action
        on delete no action,
    constraint FK_Message_ReceiverUser foreign key(ReceiverUserId) references [User](UserId)
        on update no action
        on delete no action

alter table UserRole add
    constraint FK_UserRole_Role foreign key(RoleId) references Role(RoleId)
        on update cascade
        on delete cascade,
    constraint FK_UserRole_User foreign key(UserId) references [User](UserId)
        on update cascade
        on delete cascade

alter table PermissionAssignment add
    constraint FK_PermissionAssignment_Permission foreign key(PermissionId) references Permission(PermissionId)
        on update cascade
        on delete cascade,
    constraint FK_PermissionAssignment_Role foreign key(RoleId) references Role(RoleId)
        on update cascade
        on delete cascade

alter table Permission add
    constraint FK_Permission_Module foreign key(ModuleId) references Module(ModuleId)
        on update cascade
        on delete cascade

alter table UserAsset add
    constraint FK_UserAsset_User foreign key(UserId) references [User](UserId)
        on update cascade
        on delete cascade,
    constraint FK_UserAsset_Asset foreign key(AssetId) references Asset(AssetId)
        on update cascade
        on delete cascade

alter table Asset add
    constraint FK_Asset_AssetType foreign key([Type]) references AssetType([Type])
        on update no action
        on delete no action

alter table AssetAssignment add
    constraint FK_AssetAssignment_Asset foreign key(AssetId) references Asset(AssetId)
        on update cascade
        on delete cascade,
    constraint FK_AssetAssignment_Afe foreign key(AFE) references Afe(AFE)
        on update no action
        on delete no action,
    constraint FK_AssetAssignment_SubAfe foreign key(SubAFE) references SubAfe(SubAFE)
        on update no action
        on delete no action,
    constraint FK_AssetAssignment_Afe_SubAfe_AssetId unique(AFE, SubAFE, AssetId)

alter table Afe add
    constraint FK_Afe_Client foreign key(ClientId) references Client(ClientId)
        on update cascade
        on delete cascade,
    constraint FK_Afe_AfeStatus foreign key(AFEStatus) references AfeStatus(AFEStatus)
        on update no action
        on delete no action,
    constraint FK_Afe_Afe_ClientId unique(Afe, ClientId)

alter table SubAfe add
    constraint FK_SubAfe_Afe foreign key(AFE) references Afe(AFE)
        on update cascade
        on delete cascade,
    constraint FK_SubAfe_SubAfeStatus foreign key(SubAFEStatus) references SubAfeStatus(SubAFEStatus)
        on update no action
        on delete no action

alter table RateByAssignment add
    constraint FK_RateByAssignment_AssetAssignment foreign key(AssetAssignmentId) references AssetAssignment(AssetAssignmentId)
        on update cascade
        on delete cascade,
    constraint FK_RateByAssignment_BillItemType foreign key(BillItemTypeId) references BillItemType(BillItemTypeId)
        on update cascade
        on delete cascade,
    constraint FK_RateByAssignment_BillItemType_AssetAssignment unique(BillItemTypeId, AssetAssignmentId)

alter table BillItem add
    constraint FK_BillItem_BillItemStatus foreign key(Status) references BillItemStatus(Status)
        on update no action
        on delete no action,
    constraint FK_BillItem_BillItemType foreign key(BillItemTypeId) references BillItemType(BillItemTypeId)
        on update no action
        on delete no action,
    constraint FK_BillItem_Bill foreign key(BillId) references Bill(BillId)
        on update cascade
        on delete cascade,
    constraint FK_BillItem_AssetAssignment foreign key(AssetAssignmentId) references AssetAssignment(AssetAssignmentId)
        on update no action
        on delete no action

alter table Bill add
    constraint FK_Bill_BillStatus foreign key(Status) references BillStatus(Status)
        on update cascade
        on delete cascade,
    constraint FK_Bill_Asset foreign key(AssetId) references Asset(AssetId)
        on update no action
        on delete no action

alter table Invoice add
    constraint FK_Invoice_InvoiceStatus foreign key(Status) references InvoiceStatus(Status)
        on update no action
        on delete no action,
    constraint FK_Invoice_Client foreign key(ClientId) references Client(ClientId)
        on update no action
        on delete no action

alter table BillItemType add
    constraint FK_BillItemType_InvoiceItemType foreign key(InvoiceItemTypeId) references InvoiceItemType(InvoiceItemTypeId)
        on update no action
        on delete no action

alter table InvoiceItem add
    constraint FK_InvoiceItem_InvoiceItemStatus foreign key(Status) references InvoiceItemStatus(Status)
        on update no action
        on delete no action,
    constraint FK_InvoiceItem_InvoiceItemType foreign key(InvoiceItemTypeId) references InvoiceItemType(InvoiceItemTypeId)
        on update no action
        on delete no action,
    constraint FK_InvoiceItem_Invoice foreign key(InvoiceId) references Invoice(InvoiceId)
        on update cascade
        on delete cascade,
    constraint FK_InvoiceItem_AssetAssignment foreign key(AssetAssignmentId) references AssetAssignment(AssetAssignmentId)
        on update no action
        on delete no action,
    constraint FK_InvoiceItem_BillItem foreign key(BillItemId) references BillItem(BillItemId)
        on update no action
        on delete no action

alter table BillItemAttachment add
    constraint FK_BillItemAttachment_BillItem foreign key(BillItemId) references BillItem(BillItemId)
        on update cascade
        on delete cascade

alter table Note add
    constraint FK_Note_User foreign key(SenderId) references [User](UserId)
        on update cascade
        on delete cascade

go

