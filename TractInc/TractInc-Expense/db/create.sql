create table Afe(
    AFE         varchar(20)  not null,
    ClientId    int          not null,
    AFEName     varchar(100) not null,
    AFEStatus   varchar(10)  not null,
    Deleted     bit          not null,
    primary key(AFE)
)

create table AfeStatus(
    AFEStatus   varchar(10) not null,
    primary key(AFEStatus)
)

create table Asset(
    AssetId         int identity    not null,
    [Type]          varchar(10)     not null,
    ChiefAssetId    int             not null,
    BusinessName    varchar(45)     not null,
    FirstName       varchar(45)     not null,
    MiddleName      varchar(45)     not null,
    LastName        varchar(45)     not null,
    SSN             varchar(9)      not null,
    Deleted         bit             not null,
    primary key(AssetId)
)

create table AssetAssignment(
    AssetAssignmentId   int identity    not null,
    AFE                 varchar(20)     not null,
    SubAFE              varchar(120)    not null,
    AssetId             int             not null,
    Deleted             bit             not null,
    primary key(AssetAssignmentId)
)

create table AssetType(
    [Type]  varchar(10)     not null,
    primary key([Type])
)

create table Bill(
    BillId          int identity    not null,
    Status          varchar(10)     not null,
    Notes           varchar(255)    not null,
    StartDate       varchar(10)     not null,
    AssetId         int             not null,
    TotalDailyBill  int             not null,
    DailyBillAmt    numeric(19, 3)  not null,
    OtherBillAmt    numeric(19, 3)  not null,
    TotalBillAmt    numeric(19, 3)  not null,
    primary key(BillId)
)

create table Invoice(
    InvoiceId       int identity    not null,
    InvoiceNumber   varchar(50)     not null,
    ClientId        int             not null,
    ClientName      varchar(45)     not null,
    ClientAddress   varchar(255)    not null,
    ClientActive    bit             not null,
    Status          varchar(10)     not null,
    Notes           varchar(255)        null,
    StartDate       varchar(10)     not null,
    TotalDailyAmt   int             not null,
    DailyInvoiceAmt numeric(19, 3)  not null,
    OtherInvoiceAmt numeric(19, 3)  not null,
    TotalInvoiceAmt numeric(19, 3)  not null,
    primary key(InvoiceId)
)

create table InvoiceStatus(
    Status  varchar(10)     not null,
    primary key(Status)
)

create table BillStatus(
    Status  varchar(10)     not null,
    primary key(Status)
)

create table BillItemType(
    BillItemTypeId      int identity    not null,
    InvoiceItemTypeId   int             not null,
    [Name]              varchar(20)         null,
    IsCountable         bit                 null,
    IsPresetRate        bit                 null,
    IsSingle            bit                 null,
    IsAttachRequired    bit                 null,
    Deleted             bit             not null,
    primary key(BillItemTypeId)
)

create table InvoiceItemType(
    InvoiceItemTypeId   int identity    not null,
    [Name]              varchar(20)         null,
    IsCountable         bit                 null,
    IsPresetRate        bit                 null,
    IsSingle            bit                 null,
    Deleted             bit             not null,
    primary key(InvoiceItemTypeId)
)

create table RateByAssignment(
    RateByAssignmentId  int identity    not null,
    AssetAssignmentId   int             not null,
    BillItemTypeId      int             not null,
    BillRate            numeric(19, 3)  not null,
    InvoiceRate         numeric(19, 3)  not null,
    ShouldNotExceedRate bit             not null default 0,
    Deleted             bit             not null,
    primary key(RateByAssignmentId)
)

create table BillItem(
    BillItemId              int identity    not null,
    BillItemTypeId          int             not null,
    BillId                  int             not null,
    AssetAssignmentId       int             not null,
    BillingDate             varchar(10)     not null,
    Qty                     int                 null,
    BillRate                numeric(19, 3)      null,
    Status                  varchar(10)     not null,
    Notes                   varchar(255)        null,
    BillItemCompositionId   int                 null,
    primary key(BillItemId)
)

create table WorkLog(
    WorkLogId   int identity    not null,
    BillItemId  int             not null,
    LogMessage  varchar(4000)       null,
    primary key(WorkLogId)
)

create table BillItemComposition (
    BillItemCompositionId   int identity    not null,
    BillId                  int             not null,
    BillItemTypeId          int             not null,
    Amount                  numeric(19, 3)  not null,
    Description             varchar(50)     not null,
    primary key(BillItemCompositionId)
)

create table InvoiceItem (
    InvoiceItemId int identity NOT NULL,
    InvoiceItemTypeId int not null,
    InvoiceId int not null, -- fk to invoice
    BillItemId int, -- FK to billitem
    AssetAssignmentId int NOT NULL,
    InvoiceDate varchar(10) NOT NULL,
    Qty int,
    InvoiceRate numeric(19, 3),
    Status varchar(10) NOT NULL,
    Notes varchar(255),
    IsSelected bit not null,
    primary key(InvoiceItemId)
)

create table BillItemAttachment (
    BillItemAttachmentId int identity not null,
    BillItemId int not null,
    FileName varchar(350) not null,
    OriginalFileName varchar(255) not null,
    primary key(BillItemAttachmentId)
)

create table BillItemStatus (
    Status varchar(10) NOT NULL,
    primary key(Status)
)

create table InvoiceItemStatus (
    Status varchar(10) NOT NULL,
    primary key(Status)
)

create table Client (
    ClientId int identity NOT NULL,
    ClientName varchar(45) NOT NULL,
    ClientAddress varchar(255) NOT NULL,
    Active bit NOT NULL,
    Deleted bit NOT NULL,
    primary key(ClientId)
)

create table DefaultBillRate (
    DefaultBillRateId int identity not null,
    AssetId int not null,
    BillItemTypeId int not null,
    BillRate numeric(19, 3) not null,
    primary key(DefaultBillRateId)
)

create table DefaultInvoiceRate (
    DefaultInvoiceRateId int identity not null,
    ClientId int not null,
    InvoiceItemTypeId int not null,
    InvoiceRate numeric(19, 3) not null,
    primary key(DefaultInvoiceRateId)
)

create table SubAfe (
    SubAFE       varchar(120) NOT NULL,
    AFE          varchar(20)  NOT NULL,
    SubAFEStatus varchar(10)  NOT NULL,
    ShortName    varchar(20)  NOT NULL,
    Deleted      bit          NOT NULL,
    [Temporary]  bit          NOT NULL default 0,
    primary key(SubAFE)
)

create table SubAfeStatus (
    SubAFEStatus varchar(10) NOT NULL,
    primary key(SubAFEStatus)
)

create table SyncLog (
    SyncLogId   int identity NOT NULL,
    AssetId     int NOT NULL,
    DeviceId    varchar(50) NOT NULL,
    primary key(SyncLogId)
)

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
    Deleted         bit         not null
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
    primary key(NoteId)
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
        on delete no action

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
        on delete no action,
    constraint FK_BillItem_BillItemComposition foreign key(BillItemCompositionId) references BillItemComposition(BillItemCompositionId)
        on update cascade
        on delete cascade

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

alter table DefaultInvoiceRate add
    constraint FK_DefaultInvoiceRate_Client foreign key(ClientId) references Client(ClientId)
        on update cascade
        on delete cascade,
    constraint FK_DefaultInvoiceRate_InvoiceItemType foreign key(InvoiceItemTypeId) references InvoiceItemType(InvoiceItemTypeId)
        on update cascade
        on delete cascade,
    constraint FK_DefaultInvoiceRate_ClientId_InvoiceItemTypeId unique(ClientId, InvoiceItemTypeId)

alter table DefaultBillRate add
    constraint FK_DefaultBillRate_Asset foreign key(AssetId) references Asset(AssetId)
        on update cascade
        on delete cascade,
    constraint FK_DefaultBillRate_BillItemType foreign key(BillItemTypeId) references BillItemType(BillItemTypeId)
        on update cascade
        on delete cascade,
    constraint FK_DefaultBillRate_AssetId_BillItemTypeId unique(AssetId, BillItemTypeId)

alter table BillItemComposition add
    constraint FK_BillItemComposition_Bill foreign key(BillId) references Bill(BillId)
        on update no action
        on delete no action,
    constraint FK_BillItemComposition_BillItemType foreign key(BillItemTypeId) references BillItemType(BillItemTypeId)
        on update no action
        on delete no action

alter table WorkLog add
    constraint FK_WorkLog_BillItem foreign key(BillItemId) references BillItem(BillItemId)
        on update cascade
        on delete cascade

alter table [User] add
    constraint AK_User_Login unique (Login)

