begin tran

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

if exists (select * from dbo.sysobjects where id = object_id(N'[User]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [User]

if exists (select * from dbo.sysobjects where id = object_id(N'[County]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [County]

if exists (select * from dbo.sysobjects where id = object_id(N'[State]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [State]

create table [Role](
    RoleId  int identity(1, 1)  not null,
    Name    varchar(50)         not null,
    constraint PK_PermissionGroup primary key (RoleId)
)

create table [State](
    StateId     int identity(1, 1)  not null,
    Name        varchar(50)             null,
    StateFips   varchar(2)              null,
    StateAbbr   varchar(2)              null,
    constraint PK_State primary key(StateId)
)

create table [AccountType](
    AccountTypeId   int identity(1, 1)  not null,
    TypeName        varchar(50)         not null,
    constraint PK_AccouuntType primary key(AccountTypeId)
)

create table [DocumentType](
    DocTypeId           int identity(1, 1)  not null,
    Name                varchar(50)         not null,
    GiverRequired       bit                 not null,
    ReceiverRequired    bit                 not null,
    GiverRoleName       varchar(50)             null,
    ReceiverRoleName    varchar(50)             null,
    constraint PK_DocumentType primary key(DocTypeId)
)

create table [Client](
    ClientId        int identity(1, 1)  not null,
    ClientName      varchar(50)         not null,
    ClientAddress   varchar(250)            null,
    constraint PK_Client primary key(ClientId)
)

create table [Unit](
    UnitId  int identity(1, 1)  not null,
    Name    varchar(50)             null,
    constraint PK_Unit primary key(UnitId)
)

create table [DocumentAttachmentType](
    DocumentAttachmentTypeId    int identity(1, 1)  not null,
    Name                        varchar(50)         not null,
    Description                 varchar(350)            null,
    constraint PK_DocumentAttachmentType primary key(DocumentAttachmentTypeId)
)

create table [AssetType](
    AssetTypeId int identity(1, 1)  not null,
    TypeName    varchar(50)             null,
    constraint PK_AssetType primary key(AssetTypeId)
)

create table [ProjectStatus](
    ProjectStatusId int identity(1, 1)  not null,
    StatusName      varchar(50)         not null,
    constraint PK_ProjectStatus primary key(ProjectStatusId)
)

create table [ProjectAttachmentType](
    ProjectAttachmentTypeId int identity(1, 1)  not null,
    Name                    varchar(50)         not null,
    Description             varchar(350)            null,
    constraint PK_ProjectAttachmentType primary key(ProjectAttachmentTypeId)
)

create table [Company](
    CompanyId   int identity(1, 1)  not null,
    CompanyName varchar(50)         not null,
    constraint PK_Company primary key(CompanyId)
)

create table [User](
    UserId          int identity(1, 1)  not null,
    Login           varchar(50)         not null,
    Password        varchar(50)         not null,
    DefaultSite     varchar(50)         not null,
    IsActive        bit                 not null default(1),
    HackingAttempts int                 not null default(0),
    NewTracts       int                 not null default(0),
    Email           varchar(50)             null,
    constraint PK_User primary key(UserId)
)

create table [BillItemType](
    BillItemTypeId      int identity(1, 1)  not null,
    TypeName            varchar(50)         not null,
    InvoiceItemTypeId   int                 not null,
    IsCountable         bit                 not null,
    IsPresetRate        bit                 not null,
    IsSingle            bit                 not null,
    IsAttachRequired    bit                 not null,
    constraint PK_BillItemType primary key(BillItemTypeId)
)

create table [BillStatus](
    BillStatusId    int identity(1, 1)  not null,
    StatusName      varchar(50)         not null,
    constraint PK_BillStatus primary key(BillStatusId)
)

create table [BillItemStatus](
    BillItemStatusId    int identity(1, 1)  not null,
    StatusName          varchar(50)         not null,
    constraint PK_BillItemStatus primary key(BillItemStatusId)
)

create table [InvoiceItemType](
    InvoiceItemTypeId   int identity(1, 1)  not null,
    TypeName            varchar(50)             null,
    constraint PK_InvoiceItemType primary key(InvoiceItemTypeId)
)

create table [InvoiceItemStatus](
    InvoiceItemStatusId int identity(1, 1)  not null,
    StatusName          varchar(50)         not null,
    constraint PK_InvoiceItemStatus primary key(InvoiceItemStatusId)
)

create table [Module](
    ModuleId    int identity(1, 1)  not null,
    ShortName   varchar(20)         not null,
    Description varchar(250)            null,
    Url         varchar(250)        not null,
    IsPopup     bit                 not null default(0),
    constraint PK_Module primary key(ModuleId)
)

create table [InvoiceStatus](
    InvoiceStatusId int identity(1, 1)  not null,
    StatusName      varchar(50)         not null,
    constraint PK_InvoiceStatus primary key(InvoiceStatusId)
)

create table [PermissionAssignment](
    PermissionAssignmentId  int identity(1, 1)  not null,
    PermissionId            int                 not null,
    RoleId                  int                 not null,
    constraint PK_PermissionPermissionGroup primary key(PermissionAssignmentId)
)

create table [UserRole](
    UserRoleId  int identity(1, 1)  not null,
    UserId      int                 not null,
    RoleId      int                 not null,
    constraint PK_UserPermissionGroup primary key(UserRoleId)
)

create table [Document](
    DocId           int identity(1, 1)  not null,
    DocTypeId       int                 not null,
    IsPublic        bit                 not null,
    State           int                 not null,
    County          int                 not null,
    Volume          varchar(50)             null,
    Page            varchar(50)             null,
    DocumentNo      varchar(50)             null,
    DateFiledYear   int                     null,
    DateFiledMonth  int                     null,
    DateFiledDay    int                     null,
    DateSignedYear  int                     null,
    DateSignedMonth int                     null,
    DateSignedDay   int                     null,
    ResearchNote    varchar(350)            null,
    ImageLink       varchar(350)            null,
    CreatedBy       int                 not null,
    DateModifyed    datetime            not null,
    DocPlace        varchar(50)             null,
    constraint PK_Document primary key(DocId)
)

create table [County](
    CountyId    int identity(1, 1)  not null,
    Name        varchar(50)             null,
    StateId     int                 not null,
    StateName   varchar(50)             null,
    StateFips   varchar(2)              null,
    CountyFips  varchar(3)              null,
    Fips        varchar(5)              null,
    constraint PK_County primary key(CountyId)
)

create table [Account](
    AccountId       int identity(1, 1)  not null,
    AccountName     varchar(50)         not null,
    ParentAccountId int                     null,
    AccountTypeId   int                 not null,
    CompanyId       int                 not null,
    constraint PK_Account primary key(AccountId)
)

create table [Participant](
    ParticipantId   int identity(1, 1)  not null,
    DocId           int                 not null,
    AsNamed         varchar(350)            null,
    FirstName       varchar(50)             null,
    MiddleName      varchar(50)             null,
    LastName        varchar(50)             null,
    IsSeller        bit                 not null,
    constraint PK_Participant primary key(ParticipantId)
)

create table [Tract](
    TractId     int identity(1, 1)  not null,
    Easting     int                 not null,
    Northing    int                 not null,
    RefName     varchar(100)        not null,
    CreatedBy   int                 not null,
    IsDeleted   bit                 not null default(0),
    DocID       int                     null,
    CalledAC    numeric(18, 2)      not null,
    UnitId      int                 not null,
    DocIdUnique as (isnull(DocId, (-1))),
    [timestamp] timestamp           not null,
    constraint PK_Tract primary key(TractId),
    constraint ck_refNameDocId unique(RefName, DocIdUnique)
)

create table [DocumentAttachment](
    DocumentAttachmentId        int identity(1, 1)  not null,
    DocumentId                  int                 not null,
    DocumentAttachmentTypeId    int                 not null,
    FileId                      int                 not null,
    constraint PK_DocumentAttachment primary key(DocumentAttachmentId)
)

create table [ProjectTabDocument](
    ProjectTabDocumentId    int identity(1, 1)  not null,
    ProjectTabId            int                 not null,
    DocumentId              int                 not null,
    Description             varchar(350)            null,
    constraint PK_ProjectTabDocument primary key(ProjectTabDocumentId)
)

create table [TractCalls](
    TractCallId     int identity(1, 1)  not null,
    TractId         int                 not null,
    CallType        varchar(15)         not null,
    CallDBValue     varchar(200)        not null,
    CallOrder       int                 not null,
    CreatedByMouse  bit                 not null,
    constraint PK_TractCalls primary key(TractCallId)
)

create table [TractTextObjects](
    TractTextObjectId   int identity(1, 1)  not null,
    TractId             int                 not null,
    Text                nvarchar(4000)      not null,
    Easting             numeric(18, 2)      not null,
    Northing            numeric(18, 2)      not null,
    Rotation            numeric(18, 2)      not null,
    constraint PK_TractTextObjects primary key(TractTextObjectId)
)

create table [Contract](
    ContractId      int identity(1, 1)  not null,
    ClientId        int                 not null,
    ContractName    varchar(50)         not null,
    Status          int                 not null,
    StartDate       datetime                null,
    EndDate         datetime                null,
    constraint PK_Contract primary key(ContractId)
)

create table [ClientContact](
    ClientContactId int identity(1, 1)  not null,
    PersonId        int                 not null,
    ContractId      int                 not null,
    constraint PK_ClientContact primary key(ClientContactId)
)

create table [Project](
    ProjectId       int identity(1, 1)  not null,
    ContractId      int                 not null,
    AccountId       int                     null,
    ProjectName     varchar(50)         not null,
    ShortName       varchar(50)         not null,
    Description     varchar(350)            null,
    CreatedBy       int                 not null,
    ProjectStatusId int                 not null,
    constraint PK_Project primary key(ProjectId)
)

create table [ContractDefaultRate](
    InvoiceDefaultRateId    int identity(1, 1)  not null,
    ContractId              int                 not null,
    InvoiceItemTypeId       int                 not null,
    Rate                    numeric(19, 3)      not null,
    constraint PK_InvoiceDefaultRate primary key(InvoiceDefaultRateId)
)

create table [ProjectAttachment](
    ProjectAttachmentId     int identity(1, 1)  not null,
    ProjectId               int                 not null,
    ProjectAttachmentTypeId int                 not null,
    FileId                  int                 not null,
    constraint PK_ProjectAttachment primary key(ProjectAttachmentId)
)

create table [Asset](
    AssetId         int identity(1, 1)  not null,
    AssetTypeId     int                     null,
    CrewChiefId     int                     null,
    FieldManagerId  int                     null,
    CompanyId       int                 not null,
    constraint PK_Asset primary key(AssetId)
)

create table [ProjectAttachmentInfo](
    ProjectAttachmentInfoId int identity(1, 1)  not null,
    ProjectAttachmentId     int                 not null,
    Code                    varchar(50)         not null,
    Value                   varchar(50)         not null,
    constraint PK_ProjectAttachmentInfo primary key(ProjectAttachmentInfoId)
)

create table [File](
    FileId          int identity(1, 1)  not null,
    FileName        varchar(350)        not null,
    FileUrl         varchar(350)        not null,
    FileLocalPath   varchar(350)        not null,
    Description     varchar(350)            null,
    UserId          int                 not null,
    constraint PK_File primary key(FileId)
)

create table [Person](
    PersonId    int identity(1, 1)  not null,
    UserId      int                     null,
    AssetId     int                     null,
    FirstName   varchar(50)             null,
    MiddleName  varchar(50)             null,
    LastName    varchar(50)             null,
    Phone       varchar(50)             null,
    SSN         varchar(50)             null,
    constraint PK_Person primary key(PersonId)
)

create table [AssetAssignment](
    AssetAssignmentId   int identity(1, 1)  not null,
    AssetId             int                 not null,
    ProjectId           int                 not null,
    ChiefAssetId        int                 not null,
    FieldManagerId      int                 not null,
    StartDate           datetime            not null,
    EndDate             datetime                null,
    constraint PK_AssetAssignment primary key(AssetAssignmentId)
)

create table [ProjectTab](
    ProjectTabId    int identity(1, 1)  not null,
    ProjectId       int                     null,
    Name            varchar(50)             null,
    Description     varchar(350)            null,
    constraint PK_ProjectTab primary key(ProjectTabId)
)

create table [AssetDefaultRate](
    AssetDefaultRateId  int identity(1, 1)  not null,
    AssetId             int                 not null,
    BillItemTypeId      int                 not null,
    Rate                numeric(19, 3)      not null,
    constraint PK_AssetDefaultRate primary key(AssetDefaultRateId)
)

create table [BillItem](
    BillItemId          int identity(1, 1)  not null,
    BillId              int                     null,
    BillItemTypeId      int                 not null,
    AssetAssignmentId   int                 not null,
    CompositionItemId   int                     null,
    BillingDate         datetime            not null,
    BillRate            numeric(19, 3)      not null,
    Quantity            int                 not null,
    BillItemStatusId    int                 not null,
    constraint PK_BillItem primary key(BillItemId)
)

create table [CompositionItem](
    CompositionItemId   int identity(1, 1)  not null,
    BillItemTypeId      int                 not null,
    BillId              int                 not null,
    Amount              numeric(19, 3)      not null,
    Description         varchar(350)            null,
    constraint PK_CompositionItem primary key(CompositionItemId)
)

create table [AssignmentRate](
    AssignmentRateId    int identity(1, 1)  not null,
    AssetAssignmentId   int                 not null,
    BillItemTypeId      int                 not null,
    InvoiceItemTypeId   int                 not null,
    BillRate            numeric(19, 3)      not null,
    InvoiceRate         numeric(19, 3)      not null,
    constraint PK_AssignmentRate primary key(AssignmentRateId)
)

create table [InvoiceItem](
    InvoiceItemId       int identity(1, 1)  not null,
    InvoiceId           int                 not null,
    InvoiceItemTypeId   int                 not null,
    AssetAssignmentId   int                 not null,
    BillItemId          int                     null,
    InvoiceItemDate     datetime            not null,
    Quantity            int                 not null,
    Rate                numeric(19, 3)      not null,
    IsSelected          bit                 not null,
    InvoiceItemStatusId int                 not null,
    constraint PK_InvoiceItem primary key(InvoiceItemId)
)

create table [Bill](
    BillId          int identity(1, 1)  not null,
    AssetId         int                 not null,
    StartDate       datetime            not null,
    EndDate         datetime            not null,
    TotalDays       int                 not null,
    DailyAmount     numeric(19, 3)      not null,
    OtherAmount     numeric(19, 3)      not null,
    TotalAmount     numeric(19, 3)      not null,
    BillStatusId    int                 not null,
    constraint PK_Bill primary key(BillId)
)

create table [Permission](
    PermissionId    int identity(1, 1)  not null,
    ModuleId        int                 not null,
    Description     varchar(250)            null,
    Code            varchar(50)         not null,
    constraint PK_Permission primary key(PermissionId)
)

create table [Invoice](
    InvoiceId       int identity(1, 1)  not null,
    ClientId        int                 not null,
    InvoiceStatusId int                 not null,
    InvoiceNumber   varchar(50)             null,
    InvoiceDate     datetime                null,
    ClientName      varchar(50)             null,
    ClientAddress   varchar(50)             null,
    Days            int                     null,
    DailyAmount     numeric(19, 3)          null,
    OtherAmount     numeric(19, 3)          null,
    TotalAmount     numeric(19, 3)          null,
    constraint PK_Invoice primary key(InvoiceId)
)

alter table [PermissionAssignment] add
    constraint FK_PermissionAssignment_Permission foreign key(PermissionId)
        references [Permission](PermissionId)
            on update cascade
            on delete cascade,
    constraint FK_PermissionAssignment_Role foreign key(RoleId)
        references [Role](RoleId)
            on update cascade
            on delete cascade

alter table [UserRole] add
    constraint FK_UserRole_Role foreign key(RoleId)
        references [Role](RoleId)
            on update cascade
            on delete cascade,
    constraint FK_UserRole_User foreign key(UserId)
        references [User](UserId)
            on update cascade
            on delete cascade

alter table [Document] add
    constraint FK_Document_County foreign key(County)
        references [County](CountyId),
    constraint FK_Document_DocumentType foreign key(DocTypeId)
        references [DocumentType](DocTypeId),
    constraint FK_Document_State foreign key(State)
        references [State](StateId),
    constraint FK_Document_User foreign key(CreatedBy)
        references [User](UserId)

alter table [County] add
    constraint FK_County_State foreign key(StateId)
        references [State](StateId)

alter table [Account] add
    constraint FK_Account_AccountType foreign key(AccountTypeId)
        references [AccountType](AccountTypeId),
    constraint FK_Account_Company foreign key(CompanyId)
        references [Company](CompanyId)

alter table [Participant] add
    constraint FK_Participant_Document foreign key(DocId)
        references [Document](DocId)

alter table [Tract] add
    constraint FK_Tract_Document foreign key(DocID)
        references [Document](DocId),
    constraint FK_Tract_Unit foreign key(UnitId)
        references [Unit](UnitId)

alter table [DocumentAttachment] add
    constraint FK_DocumentAttachment_Document foreign key(DocumentId)
        references [Document](DocId),
    constraint FK_DocumentAttachment_DocumentAttachmentType foreign key(DocumentAttachmentTypeId)
        references [DocumentAttachmentType](DocumentAttachmentTypeId),
    constraint FK_DocumentAttachment_File foreign key(FileId)
        references [File](FileId)

alter table [ProjectTabDocument] add
    constraint FK_ProjectTabDocument_Document foreign key(DocumentId)
        references [Document](DocId),
    constraint FK_ProjectTabDocument_ProjectTab foreign key(ProjectTabId)
        references [ProjectTab](ProjectTabId)

alter table [TractCalls] add
    constraint FK_TractCalls_Tract foreign key(TractId)
        references [Tract](TractId)

alter table [TractTextObjects] add
    constraint FK_TractTextObjects_Tract foreign key(TractId)
        references [Tract](TractId)

alter table [Contract] add
    constraint FK_Contract_Client foreign key(ClientId)
        references [Client](ClientId)

alter table [ClientContact] add
    constraint FK_ClientContact_Contract foreign key(ContractId)
        references [Contract](ContractId),
    constraint FK_ClientContact_Person foreign key(PersonId)
        references [Person](PersonId)

alter table [Project] add
    constraint FK_Project_Account foreign key(AccountId)
        references [Account](AccountId),
    constraint FK_Project_Contract foreign key(ContractId)
        references [Contract](ContractId),
    constraint FK_Project_ProjectStatus foreign key(ProjectStatusId)
        references [ProjectStatus](ProjectStatusId)

alter table [ContractDefaultRate] add
    constraint FK_InvoiceDefaultRate_Contract foreign key(ContractId)
        references [Contract](ContractId),
    constraint FK_InvoiceDefaultRate_InvoiceItemType foreign key(InvoiceItemTypeId)
        references [InvoiceItemType](InvoiceItemTypeId)

alter table [ProjectAttachment] add
    constraint FK_ProjectAttachment_File foreign key(FileId)
        references [File](FileId),
    constraint FK_ProjectAttachment_Project foreign key(ProjectId)
        references [Project](ProjectId),
    constraint FK_ProjectAttachment_ProjectAttachmentType foreign key(ProjectAttachmentTypeId)
        references [ProjectAttachmentType](ProjectAttachmentTypeId)

alter table [Asset] add
    constraint FK_Asset_Asset foreign key(CrewChiefId)
        references [Asset](AssetId),
    constraint FK_Asset_Asset1 foreign key(FieldManagerId)
        references [Asset](AssetId),
    constraint FK_Asset_AssetType foreign key(AssetTypeId)
        references [AssetType](AssetTypeId),
    constraint FK_Asset_Company foreign key(CompanyId)
        references [Company](CompanyId)

alter table [ProjectAttachmentInfo] add
    constraint FK_ProjectAttachmentInfo_ProjectAttachment foreign key(ProjectAttachmentId)
        references [ProjectAttachment](ProjectAttachmentId)

alter table [File] add
    constraint FK_File_User foreign key(UserId)
        references [User](UserId)

alter table [Person] add
    constraint FK_Person_Asset foreign key(AssetId)
        references [Asset](AssetId),
    constraint FK_Person_User foreign key(UserId)
        references [User](UserId)

alter table [AssetAssignment] add
    constraint FK_AssetAssignment_Asset foreign key(AssetId)
        references [Asset](AssetId),
    constraint FK_AssetAssignment_Asset1 foreign key(ChiefAssetId)
        references [Asset](AssetId),
    constraint FK_AssetAssignment_Asset2 foreign key(FieldManagerId)
        references [Asset](AssetId),
    constraint FK_AssetAssignment_Project foreign key(ProjectId)
        references [Project](ProjectId)

alter table [ProjectTab] add
    constraint FK_ProjectTab_Project foreign key(ProjectId)
        references [Project](ProjectId)

alter table [AssetDefaultRate] add
    constraint FK_AssetDefaultRate_Asset foreign key(AssetId)
        references [Asset](AssetId),
    constraint FK_AssetDefaultRate_BillItemType foreign key(BillItemTypeId)
        references [BillItemType](BillItemTypeId)

alter table [BillItem] add
    constraint FK_BillItem_AssetAssignment foreign key(AssetAssignmentId)
        references [AssetAssignment](AssetAssignmentId),
    constraint FK_BillItem_Bill foreign key(BillId)
        references [Bill](BillId),
    constraint FK_BillItem_BillItemType foreign key(BillItemTypeId)
        references [BillItemType](BillItemTypeId),
    constraint FK_BillItem_BillItemStatus foreign key(BillItemStatusId)
        references [BillItemStatus](BillItemStatusId)

alter table [CompositionItem] add
    constraint FK_CompositionItem_Bill foreign key(BillId)
        references [Bill](BillId),
    constraint FK_CompositionItem_BillItemType foreign key(BillItemTypeId)
        references [BillItemType](BillItemTypeId)

alter table [AssignmentRate] add
    constraint FK_AssignmentRate_AssetAssignment foreign key(AssetAssignmentId)
        references [AssetAssignment](AssetAssignmentId),
    constraint FK_AssignmentRate_BillItemType foreign key(BillItemTypeId)
        references [BillItemType](BillItemTypeId),
    constraint FK_AssignmentRate_InvoiceItemType foreign key(InvoiceItemTypeId)
        references [InvoiceItemType](InvoiceItemTypeId)

alter table [InvoiceItem] add
    constraint FK_InvoiceItem_AssetAssignment foreign key(AssetAssignmentId)
        references [AssetAssignment](AssetAssignmentId),
    constraint FK_InvoiceItem_BillItem foreign key(BillItemId)
        references [BillItem](BillItemId),
    constraint FK_InvoiceItem_Invoice foreign key(InvoiceId)
        references [Invoice](InvoiceId),
    constraint FK_InvoiceItem_InvoiceItemStatus foreign key(InvoiceItemStatusId)
        references [InvoiceItemStatus](InvoiceItemStatusId),
    constraint FK_InvoiceItem_InvoiceItemType foreign key(InvoiceItemTypeId)
        references [InvoiceItemType](InvoiceItemTypeId)

alter table [Bill] add
    constraint FK_Bill_Asset foreign key(AssetId)
        references [Asset](AssetId),
    constraint FK_Bill_BillStatus foreign key(BillStatusId)
        references [BillStatus](BillStatusId)

alter table [Permission] add
    constraint FK_Permission_Module foreign key(ModuleId)
        references [Module](ModuleId)
            on update cascade
            on delete cascade

alter table [Invoice] add
    constraint FK_Invoice_InvoiceStatus foreign key(InvoiceStatusId)
        references [InvoiceStatus](InvoiceStatusId)

commit

