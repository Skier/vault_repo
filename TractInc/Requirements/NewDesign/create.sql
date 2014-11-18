set xact_abort on
go

begin transaction
go

create table AccessRestriction(
  AccessRestrictionId int not null constraint PK_AccessRestriction primary key,
  ClientId            int not null,
  DocumentId          int not null
)
go

alter table AccessRestriction add
  constraint FK_AccessRestriction_Client foreign key(ClientId) references Client(ClientId),
  constraint FK_AccessRestriction_Document foreign key(DocumentId) references Document(DocId)
go

create table Account(
  AccountId       int         not null constraint PK_Account primary key,
  AccountNumber   varchar(50),
  AccountName     varchar(50) not null,
  ParentAccountId int,
  AccountTypeId   int         not null,
  CompanyId       int         not null,
  ClientId        int         not null
)
go

alter table Account add
  constraint FK_Account_Account foreign key(ParentAccountId) references Account(AccountId),
  constraint FK_Account_AccountType foreign key(AccountTypeId) references AccountType(AccountTypeId),
  constraint FK_Account_Client foreign key(ClientId) references Client(ClientId),
  constraint FK_Account_Company foreign key(CompanyId) references Company(CompanyId)
go

create table AccountTransaction(
  AccountTransactionId int           not null constraint PK_AccountTransaction primary key,
  AccountId            int           not null,
  Date                 datetime      not null,
  ReferenceNumber      int           not null,
  Type                 int           not null,
  Number               int           not null,
  DebitAmount          numeric(18,3),
  CreditAmount         numeric(18,3),
  Balance              numeric(18,3)
)
go

alter table AccountTransaction add
  constraint FK_AccountTransaction_Account foreign key(AccountId) references Account(AccountId),
  constraint FK_AccountTransaction_JournalEntry foreign key(ReferenceNumber) references JournalEntry(ReferenceNumber)
go

create table AccountType(
  AccountTypeId int         not null constraint PK_AccouuntType primary key,
  TypeName      varchar(50) not null
)
go

create table Asset(
  AssetId     int not null constraint PK_Asset primary key,
  AssetTypeId int,
  CompanyId   int not null
)
go

alter table Asset add
  constraint FK_Asset_AssetType foreign key(AssetTypeId) references AssetType(AssetTypeId),
  constraint FK_Asset_Company foreign key(CompanyId) references Company(CompanyId)
go

create table AssetAssignment(
  AssetAssignmentId int      not null constraint PK_AssetAssignment primary key,
  ProjectId         int      not null,
  AssetContractId   int      not null,
  ChiefAssetId      int      not null,
  FieldManagerId    int      not null,
  StartDate         datetime not null,
  EndDate           datetime
)
go

alter table AssetAssignment add
  constraint FK_AssetAssignment_Asset foreign key(AssetContractId) references Asset(AssetId),
  constraint FK_AssetAssignment_Asset1 foreign key(ChiefAssetId) references Asset(AssetId),
  constraint FK_AssetAssignment_Asset2 foreign key(FieldManagerId) references Asset(AssetId),
  constraint FK_AssetAssignment_AssetContract foreign key(AssetContractId) references AssetContract(AssetContractId),
  constraint FK_AssetAssignment_Project foreign key(ProjectId) references Project(ProjectId)
go

create table AssetContract(
  AssetContractId int      not null constraint PK_AssetContract primary key,
  AssetId         int      not null,
  ContractId      int      not null,
  StartDate       datetime not null,
  EndDate         datetime
)
go

alter table AssetContract add
  constraint FK_AssetContract_Asset foreign key(AssetId) references Asset(AssetId),
  constraint FK_AssetContract_Contract foreign key(ContractId) references Contract(ContractId)
go

create table AssetContractRate(
  AssetContractRateId int           not null constraint PK_AssetContractRate primary key,
  AssetContractId     int           not null,
  BillItemTypeId      int           not null,
  Rate                numeric(19,3) not null
)
go

alter table AssetContractRate add
  constraint FK_AssetContractRate_AssetContract foreign key(AssetContractId) references AssetContract(AssetContractId),
  constraint FK_AssetContractRate_BillItemType foreign key(BillItemTypeId) references BillItemType(BillItemTypeId)
go

create table AssetType(
  AssetTypeId int         not null constraint PK_AssetType primary key,
  TypeName    varchar(50)
)
go

create table Bill(
  BillId       int           not null constraint PK_Bill primary key,
  AssetId      int           not null,
  StartDate    datetime      not null,
  EndDate      datetime      not null,
  BillStatusId int           not null,
  TotalDays    int           not null,
  DailyAmount  numeric(19,3) not null,
  OtherAmount  numeric(19,3) not null,
  TotalAmount  numeric(19,3) not null
)
go

alter table Bill add
  constraint FK_Bill_Asset foreign key(AssetId) references Asset(AssetId),
  constraint FK_Bill_BillStatus foreign key(BillStatusId) references BillStatus(BillStatusId)
go

create table BillDailyItemNote(
  BillDailyItemNoteId int not null constraint PK_BillDailyItemNote primary key,
  BillDailyItemId     int not null,
  NoteId              int not null
)
go

alter table BillDailyItemNote add
  constraint FK_BillDailyItemNote_BillProjectDayItem foreign key(BillDailyItemId) references BillProjectDayItem(BillProjectDayItemId),
  constraint FK_BillDailyItemNote_Note foreign key(NoteId) references Note(NoteId)
go

create table BillingType(
  BillingTypeId int         not null constraint PK_BillingType primary key,
  TypeName      varchar(50) not null
)
go

create table BillItem(
  BillItemId           int           not null constraint PK_BillItem primary key,
  BillProjectDayItemId int           not null,
  BillItemTypeId       int           not null,
  CompositionItemId    int,
  BillRate             numeric(19,3) not null,
  Quantity             numeric(18,3) not null,
  BillItemStatusId     int           not null
)
go

alter table BillItem add
  constraint FK_BillItem_BillItemStatus foreign key(BillItemStatusId) references BillItemStatus(BillItemStatusId),
  constraint FK_BillItem_BillItemType foreign key(BillItemTypeId) references BillItemType(BillItemTypeId),
  constraint FK_BillItem_BillProjectDayItem foreign key(BillProjectDayItemId) references BillProjectDayItem(BillProjectDayItemId),
  constraint FK_BillItem_CompositionItem foreign key(CompositionItemId) references CompositionItem(CompositionItemId)
go

create table BillItemAttachment(
  BillItemAttachmentId     int not null constraint PK_BillItemAttachment primary key,
  BillItemId               int not null,
  BillItemAttachmentTypeId int not null,
  FileId                   int not null
)
go

alter table BillItemAttachment add
  constraint FK_BillItemAttachment_BillItem foreign key(BillItemId) references BillItem(BillItemId),
  constraint FK_BillItemAttachment_BillItemAttachmentType foreign key(BillItemAttachmentTypeId) references BillItemAttachmentType(BillItemAttachmentTypeId),
  constraint FK_BillItemAttachment_File foreign key(FileId) references [File](FileId)
go

create table BillItemAttachmentType(
  BillItemAttachmentTypeId int          not null constraint PK_BillItemAttachmentType primary key,
  TypeName                 varchar(50)  not null,
  Description              varchar(350)
)
go

create table BillItemNote(
  BillItemNoteId int not null constraint PK_BillItemNote primary key,
  BillItemId     int not null,
  NoteId         int not null
)
go

alter table BillItemNote add
  constraint FK_BillItemNote_BillItem foreign key(BillItemId) references BillItem(BillItemId),
  constraint FK_BillItemNote_Note foreign key(NoteId) references Note(NoteId)
go

create table BillItemStatus(
  BillItemStatusId int         not null constraint PK_BillItemStatus primary key,
  StatusName       varchar(50) not null
)
go

create table BillItemType(
  BillItemTypeId    int         not null constraint PK_BillItemType primary key,
  TypeName          varchar(50) not null,
  InvoiceItemTypeId int         not null,
  IsCountable       bit         not null,
  IsPresetRate      bit         not null,
  IsSingle          bit         not null,
  IsAttachRequired  bit         not null
)
go

create table BillNote(
  BillNoteId int not null constraint PK_BillNote primary key,
  BillId     int not null,
  NoteId     int not null
)
go

alter table BillNote add
  constraint FK_BillNote_Bill foreign key(BillId) references Bill(BillId),
  constraint FK_BillNote_Note foreign key(NoteId) references Note(NoteId)
go

create table BillProjectDayItem(
  BillProjectDayItemId int           not null constraint PK_BillProjectDayItem primary key,
  BillId               int,
  AssetAssignmentId    int           not null,
  BillingDate          datetime      not null,
  DailyBillQty         numeric(18,3) not null,
  DailyBillRate        numeric(18,3) not null,
  BillItemStatusId     int           not null,
  Description          varchar(350)
)
go

alter table BillProjectDayItem add
  constraint FK_BillProjectDayItem_AssetAssignment foreign key(AssetAssignmentId) references AssetAssignment(AssetAssignmentId),
  constraint FK_BillProjectDayItem_Bill foreign key(BillId) references Bill(BillId),
  constraint FK_BillProjectDayItem_InvoiceProjectDayItem foreign key(BillItemStatusId) references InvoiceProjectDayItem(InvoiceProjectDayItemId)
go

create table BillStatus(
  BillStatusId int         not null constraint PK_BillStatus primary key,
  StatusName   varchar(50) not null
)
go

create table Client(
  ClientId      int          not null constraint PK_Client primary key,
  ClientName    varchar(50)  not null,
  ClientAddress varchar(250)
)
go

create table ClientContact(
  ClientContactId int      not null constraint PK_ClientContact primary key,
  PersonId        int      not null,
  ContractId      int      not null,
  StartDate       datetime not null,
  EndDate         datetime
)
go

alter table ClientContact add
  constraint FK_ClientContact_Contract foreign key(ContractId) references Contract(ContractId),
  constraint FK_ClientContact_Person foreign key(PersonId) references Person(PersonId)
go

create table Company(
  CompanyId        int         not null constraint PK_Company primary key,
  CompanyName      varchar(50) not null,
  BillingTypeId    int         not null,
  BillingStartDate datetime    not null
)
go

alter table Company add
  constraint FK_Company_BillingType foreign key(BillingTypeId) references BillingType(BillingTypeId)
go

create table CompanyContact(
  CompanyContactId int      not null constraint PK_CompanyContact primary key,
  ContractId       int      not null,
  PersonId         int      not null,
  StartDate        datetime not null,
  EndDate          datetime
)
go

alter table CompanyContact add
  constraint FK_CompanyContact_Contract foreign key(ContractId) references Contract(ContractId),
  constraint FK_CompanyContact_Person foreign key(PersonId) references Person(PersonId)
go

create table CompanyTeam(
  CompanyTeamId int         not null constraint PK_CompanyTeam primary key,
  TeamName      varchar(50) not null,
  CompanyId     int,
  ParentTeamId  int         not null,
  TeamManagerId int         not null
)
go

alter table CompanyTeam add
  constraint FK_CompanyTeam_Asset foreign key(TeamManagerId) references Asset(AssetId),
  constraint FK_CompanyTeam_Company foreign key(CompanyId) references Company(CompanyId),
  constraint FK_CompanyTeam_CompanyTeam foreign key(ParentTeamId) references CompanyTeam(CompanyTeamId)
go

create table CompanyTeamAsset(
  CompanyTeamAssetId int not null constraint PK_CompanyTeamAsset primary key,
  CompanyTeamId      int not null,
  AssetId            int not null
)
go

alter table CompanyTeamAsset add
  constraint FK_CompanyTeamAsset_Asset foreign key(AssetId) references Asset(AssetId),
  constraint FK_CompanyTeamAsset_CompanyTeam foreign key(CompanyTeamId) references CompanyTeam(CompanyTeamId)
go

create table CompositionItem(
  CompositionItemId int           not null constraint PK_CompositionItem primary key,
  BillItemTypeId    int           not null,
  BillId            int           not null,
  Amount            numeric(19,3) not null,
  Description       varchar(350)
)
go

alter table CompositionItem add
  constraint FK_CompositionItem_Bill foreign key(BillId) references Bill(BillId),
  constraint FK_CompositionItem_BillItemType foreign key(BillItemTypeId) references BillItemType(BillItemTypeId)
go

create table CompositionItemAttachment(
  CompositionItemAttachmentId int not null constraint PK_CompositionItemAttachment primary key,
  CompositionItemId           int not null,
  FileId                      int not null
)
go

alter table CompositionItemAttachment add
  constraint FK_CompositionItemAttachment_CompositionItem foreign key(CompositionItemId) references CompositionItem(CompositionItemId),
  constraint FK_CompositionItemAttachment_File foreign key(FileId) references [File](FileId)
go

create table Contract(
  ContractId       int         not null constraint PK_Contract primary key,
  ClientId         int         not null,
  CompanyId        int         not null,
  ContractName     varchar(50) not null,
  ContractStatusId int         not null,
  StartDate        datetime,
  EndDate          datetime
)
go

alter table Contract add
  constraint FK_Contract_Client foreign key(ClientId) references Client(ClientId),
  constraint FK_Contract_Company foreign key(CompanyId) references Company(CompanyId),
  constraint FK_Contract_ContractStatus foreign key(ContractStatusId) references ContractStatus(ContractStatusId)
go

create table ContractRate(
  InvoiceDefaultRateId int           not null constraint PK_InvoiceDefaultRate primary key,
  ContractId           int           not null,
  InvoiceItemTypeId    int           not null,
  Rate                 numeric(19,3) not null
)
go

alter table ContractRate add
  constraint FK_InvoiceDefaultRate_Contract foreign key(ContractId) references Contract(ContractId),
  constraint FK_InvoiceDefaultRate_InvoiceItemType foreign key(InvoiceItemTypeId) references InvoiceItemType(InvoiceItemTypeId)
go

create table ContractStatus(
  ContractStatusId int         not null constraint PK_ContractStatus primary key,
  StatusName       varchar(50) not null
)
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

alter table County add
  constraint FK_County_State foreign key(StateId) references State(StateId)
go

create table Document(
  DocId        int              not null constraint PK_Document primary key,
  DocTypeId    int              not null,
  State        int              not null,
  County       int              not null,
  Volume       varchar(50),
  Page         varchar(50),
  DocumentNo   varchar(50),
  ResearchNote varchar(350),
  ImageLink    varchar(350),
  CreatedBy    int              not null,
  IsActive     bit              not null default (1),
  DocBranchUid uniqueidentifier not null default (newid()),
  Filed        datetime         not null,
  Signed       datetime         not null,
  Created      datetime         not null,
  IsPublic     bit              not null
)
go

alter table Document add
  constraint FK_Document_County foreign key(County) references County(CountyId),
  constraint FK_Document_DocumentType foreign key(DocTypeId) references DocumentType(DocTypeId),
  constraint FK_Document_State foreign key(State) references State(StateId),
  constraint FK_Document_User foreign key(CreatedBy) references [User](UserId)
go

create index IX_DocBranchUid on Document(DocBranchUid)
go

create table DocumentAttachment(
  DocumentAttachmentId     int not null constraint PK_DocumentAttachment primary key,
  DocumentId               int not null,
  DocumentAttachmentTypeId int not null,
  FileId                   int not null
)
go

alter table DocumentAttachment add
  constraint FK_DocumentAttachment_Document foreign key(DocumentId) references Document(DocId),
  constraint FK_DocumentAttachment_DocumentAttachmentType foreign key(DocumentAttachmentTypeId) references DocumentAttachmentType(DocumentAttachmentTypeId),
  constraint FK_DocumentAttachment_File foreign key(FileId) references [File](FileId)
go

create table DocumentAttachmentType(
  DocumentAttachmentTypeId int          not null constraint PK_DocumentAttachmentType primary key,
  [Name]                   varchar(50)  not null,
  Description              varchar(350)
)
go

create table DocumentReference(
  DcoumentReferenceId int not null constraint PK_DocumentReference primary key,
  DocumentId          int not null,
  ReferenceId         int not null
)
go

alter table DocumentReference add
  constraint FK_DocumentReference_Document foreign key(DocumentId) references Document(DocId),
  constraint FK_DocumentReference_Document1 foreign key(ReferenceId) references Document(DocId)
go

create table DocumentType(
  DocTypeId        int         not null constraint PK_DocumentType primary key,
  [Name]           varchar(50) not null,
  GiverRequired    bit         not null,
  ReceiverRequired bit         not null,
  GiverRoleName    varchar(50),
  ReceiverRoleName varchar(50)
)
go

create table [File](
  FileId        int          not null constraint PK_File primary key,
  FileName      varchar(350) not null,
  FileUrl       varchar(350) not null,
  FileLocalPath varchar(350) not null,
  Description   varchar(350),
  UserId        int          not null
)
go

alter table [File] add
  constraint FK_File_User foreign key(UserId) references [User](UserId)
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

alter table GroupItem add
  constraint FK_GroupItem_Group foreign key(GroupId) references [Group](GroupId)
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

alter table GroupUser add
  constraint FK_GroupUser_Group foreign key(GroupId) references [Group](GroupId),
  constraint FK_GroupUser_User foreign key(UserId) references [User](UserId)
go

create table Invoice(
  InvoiceId       int           not null constraint PK_Invoice primary key,
  CompanyId       int           not null,
  ClientId        int           not null,
  InvoiceStatusId int           not null,
  InvoiceNumber   varchar(50),
  InvoiceDate     datetime,
  ClientName      varchar(50),
  ClientAddress   varchar(50),
  Days            int,
  DailyAmount     numeric(19,3),
  OtherAmount     numeric(19,3),
  TotalAmount     numeric(19,3)
)
go

alter table Invoice add
  constraint FK_Invoice_Client foreign key(ClientId) references Client(ClientId),
  constraint FK_Invoice_Company foreign key(CompanyId) references Company(CompanyId),
  constraint FK_Invoice_InvoiceStatus foreign key(InvoiceStatusId) references InvoiceStatus(InvoiceStatusId)
go

create table InvoiceDailyItemNote(
  InvoiceDailyItemNoteId int not null constraint PK_InvoiceDailyItemNote primary key,
  InvoiceDailyItemId     int not null,
  NoteId                 int not null
)
go

alter table InvoiceDailyItemNote add
  constraint FK_InvoiceDailyItemNote_InvoiceProjectDayItem foreign key(InvoiceDailyItemId) references InvoiceProjectDayItem(InvoiceProjectDayItemId),
  constraint FK_InvoiceDailyItemNote_Note foreign key(NoteId) references Note(NoteId)
go

create table InvoiceItem(
  InvoiceItemId           int           not null constraint PK_InvoiceItem primary key,
  InvoiceProjectDayItemId int           not null,
  InvoiceItemTypeId       int           not null,
  BillItemId              int,
  InvoicingDate           datetime      not null,
  Quantity                int           not null,
  Rate                    numeric(19,3) not null,
  IsSelected              bit           not null,
  InvoiceItemStatusId     int           not null
)
go

alter table InvoiceItem add
  constraint FK_InvoiceItem_BillItem foreign key(BillItemId) references BillItem(BillItemId),
  constraint FK_InvoiceItem_InvoiceItemStatus foreign key(InvoiceItemStatusId) references InvoiceItemStatus(InvoiceItemStatusId),
  constraint FK_InvoiceItem_InvoiceItemType foreign key(InvoiceItemTypeId) references InvoiceItemType(InvoiceItemTypeId),
  constraint FK_InvoiceItem_InvoiceProjectDayItem foreign key(InvoiceProjectDayItemId) references InvoiceProjectDayItem(InvoiceProjectDayItemId)
go

create table InvoiceItemNote(
  InvoiceItemNoteId int not null constraint PK_InvoiceItemNote primary key,
  InvoiceItemId     int not null,
  NoteId            int not null
)
go

alter table InvoiceItemNote add
  constraint FK_InvoiceItemNote_InvoiceItem foreign key(InvoiceItemId) references InvoiceItem(InvoiceItemId),
  constraint FK_InvoiceItemNote_Note foreign key(NoteId) references Note(NoteId)
go

create table InvoiceItemStatus(
  InvoiceItemStatusId int         not null constraint PK_InvoiceItemStatus primary key,
  StatusName          varchar(50) not null
)
go

create table InvoiceItemType(
  InvoiceItemTypeId int         not null constraint PK_InvoiceItemType primary key,
  TypeName          varchar(50)
)
go

create table InvoiceNote(
  InvoiceNoteId int not null constraint PK_InvoiceNote primary key,
  InvoiceId     int not null,
  NoteId        int not null
)
go

alter table InvoiceNote add
  constraint FK_InvoiceNote_Invoice foreign key(InvoiceId) references Invoice(InvoiceId),
  constraint FK_InvoiceNote_Note foreign key(NoteId) references Note(NoteId)
go

create table InvoiceProjectDayItem(
  InvoiceProjectDayItemId int           not null constraint PK_InvoiceProjectDayItem primary key,
  InvoiceId               int           not null,
  AssetAssignmentId       int           not null,
  InvoicingDate           datetime      not null,
  DailyBillQty            numeric(18,3) not null,
  DailyBillRate           numeric(18,3) not null,
  StatusId                int           not null,
  BillProjectDayItemId    int,
  Description             varchar(350)
)
go

alter table InvoiceProjectDayItem add
  constraint FK_InvoiceProjectDayItem_AssetAssignment foreign key(AssetAssignmentId) references AssetAssignment(AssetAssignmentId),
  constraint FK_InvoiceProjectDayItem_Invoice foreign key(InvoiceId) references Invoice(InvoiceId)
go

create table InvoiceStatus(
  InvoiceStatusId int         not null constraint PK_InvoiceStatus primary key,
  StatusName      varchar(50) not null
)
go

create table JournalEntry(
  ReferenceNumber int         not null constraint PK_JournalEntry primary key,
  Date            datetime    not null,
  Type            int         not null,
  Number          varchar(50),
  [Name]          varchar(50)
)
go

create table JournalEntryTransaction(
  JournalEntryTransactionId int           not null constraint PK_JournalEntryTransaction primary key,
  ReferenceNumber           int           not null,
  AccountId                 int           not null,
  IsDebit                   bit           not null,
  Amount                    numeric(18,3) not null
)
go

alter table JournalEntryTransaction add
  constraint FK_JournalEntryTransaction_Account foreign key(AccountId) references Account(AccountId),
  constraint FK_JournalEntryTransaction_JournalEntry foreign key(ReferenceNumber) references JournalEntry(ReferenceNumber)
go

create table Module(
  ModuleId    int          not null identity constraint PK_Module primary key,
  ShortName   varchar(20),
  Description varchar(250),
  Url         varchar(250) not null,
  IsPopup     bit          not null default (0)
)
go

create table Note(
  NoteId   int          not null constraint PK_Note primary key,
  UserId   int          not null,
  Posted   datetime     not null,
  NoteText varchar(350) not null
)
go

create table Participant(
  ParticipantId int          not null constraint PK_Participant primary key,
  DocId         int          not null,
  AsNamed       varchar(350),
  FirstName     varchar(50),
  MiddleName    varchar(50),
  LastName      varchar(50),
  IsSeller      bit          not null
)
go

alter table Participant add
  constraint FK_Participant_Document foreign key(DocId) references Document(DocId)
go

create table Payment(
  PaymentId   int           not null constraint PK_Payment primary key,
  Type        int           not null,
  Number      varchar(50),
  Amount      numeric(18,3) not null,
  PaymentDate datetime      not null,
  ClientId    int           not null,
  CompanyId   int           not null
)
go

alter table Payment add
  constraint FK_Payment_Client foreign key(ClientId) references Client(ClientId),
  constraint FK_Payment_Company foreign key(CompanyId) references Company(CompanyId)
go

create table PaymentInvoice(
  PaymentInvoiceId int           not null constraint PK_PaymentInvoice primary key,
  PaymentId        int           not null,
  InvoiceId        int           not null,
  Amount           numeric(18,3) not null
)
go

alter table PaymentInvoice add
  constraint FK_PaymentInvoice_Invoice foreign key(InvoiceId) references Invoice(InvoiceId),
  constraint FK_PaymentInvoice_Payment foreign key(PaymentId) references Payment(PaymentId)
go

create table Permission(
  PermissionId int          not null identity constraint PK_Permission primary key,
  ModuleId     int          not null,
  Description  varchar(250),
  Code         varchar(50)  not null
)
go

alter table Permission add
  constraint FK_Permission_Module foreign key(ModuleId) references Module(ModuleId) on update cascade on delete cascade
go

create table PermissionAssignment(
  PermissionAssignmentId int not null identity constraint PK_PermissionPermissionGroup primary key,
  PermissionId           int not null,
  RoleId                 int not null
)
go

alter table PermissionAssignment add
  constraint FK_PermissionAssignment_Permission foreign key(PermissionId) references Permission(PermissionId) on update cascade on delete cascade,
  constraint FK_PermissionAssignment_Role foreign key(RoleId) references Role(RoleId) on update cascade on delete cascade
go

create table Person(
  PersonId   int         not null constraint PK_Person primary key,
  UserId     int,
  AssetId    int,
  FirstName  varchar(50),
  MiddleName varchar(50),
  LastName   varchar(50),
  Phone      varchar(50),
  Email      varchar(50),
  SSN        varchar(50)
)
go

alter table Person add
  constraint FK_Person_Asset foreign key(AssetId) references Asset(AssetId),
  constraint FK_Person_User foreign key(UserId) references [User](UserId)
go

create table Project(
  ProjectId       int           not null constraint PK_Project primary key,
  ContractId      int           not null,
  AccountId       int,
  ProjectName     varchar(50)   not null,
  ShortName       varbinary(50) not null,
  Description     varchar(350),
  CreatedBy       int           not null,
  ProjectStatusId int           not null
)
go

alter table Project add
  constraint FK_Project_Account foreign key(AccountId) references Account(AccountId),
  constraint FK_Project_Contract foreign key(ContractId) references Contract(ContractId),
  constraint FK_Project_ProjectStatus foreign key(ProjectStatusId) references ProjectStatus(ProjectStatusId)
go

create table ProjectAttachment(
  ProjectAttachmentId     int not null constraint PK_ProjectAttachment primary key,
  ProjectId               int not null,
  ProjectAttachmentTypeId int not null,
  FileId                  int not null
)
go

alter table ProjectAttachment add
  constraint FK_ProjectAttachment_File foreign key(FileId) references [File](FileId),
  constraint FK_ProjectAttachment_Project foreign key(ProjectId) references Project(ProjectId),
  constraint FK_ProjectAttachment_ProjectAttachmentType foreign key(ProjectAttachmentTypeId) references ProjectAttachmentType(ProjectAttachmentTypeId)
go

create table ProjectAttachmentInfo(
  ProjectAttachmentInfoId int         not null constraint PK_ProjectAttachmentInfo primary key,
  ProjectAttachmentId     int         not null,
  Code                    varchar(50) not null,
  Value                   varchar(50) not null
)
go

alter table ProjectAttachmentInfo add
  constraint FK_ProjectAttachmentInfo_ProjectAttachment foreign key(ProjectAttachmentId) references ProjectAttachment(ProjectAttachmentId)
go

create table ProjectAttachmentType(
  ProjectAttachmentTypeId int          not null constraint PK_ProjectAttachmentType primary key,
  [Name]                  varchar(50)  not null,
  Description             varchar(350)
)
go

create table ProjectStatus(
  ProjectStatusId int         not null constraint PK_ProjectStatus primary key,
  StatusName      varchar(50) not null
)
go

create table ProjectTab(
  ProjectTabId int          not null constraint PK_ProjectTab primary key,
  ProjectId    int,
  [Name]       varchar(50),
  Description  varchar(350)
)
go

alter table ProjectTab add
  constraint FK_ProjectTab_Project foreign key(ProjectId) references Project(ProjectId)
go

create table ProjectTabDocument(
  ProjectTabDocumentId int          not null constraint PK_ProjectTabDocument primary key,
  ProjectTabId         int          not null,
  DocumentId           int          not null,
  Description          varchar(350)
)
go

alter table ProjectTabDocument add
  constraint FK_ProjectTabDocument_Document foreign key(DocumentId) references Document(DocId),
  constraint FK_ProjectTabDocument_ProjectTab foreign key(ProjectTabId) references ProjectTab(ProjectTabId)
go

create table ProjectTeam(
  ProjectTeamId int      not null constraint PK_ProjectTeam primary key,
  ProjectId     int      not null,
  TeamId        int      not null,
  StartDate     datetime not null,
  EndDate       datetime
)
go

alter table ProjectTeam add
  constraint FK_ProjectTeam_Project foreign key(ProjectId) references Project(ProjectId),
  constraint FK_ProjectTeam_Team foreign key(TeamId) references Team(TeamId)
go

create table Role(
  RoleId int         not null identity constraint PK_PermissionGroup primary key,
  [Name] varchar(50) not null
)
go

create table State(
  StateId   int         not null constraint PK_State primary key,
  [Name]    varchar(50),
  StateFips varchar(2),
  StateAbbr varchar(2)
)
go

create table Team(
  TeamId   int         not null constraint PK_Team primary key,
  TeamName varchar(50) not null
)
go

create table TeamMember(
  TeamMemberId int      not null constraint PK_TeamAssignment primary key,
  TeamId       int      not null,
  AssetId      int      not null,
  ChiefAssetId int      not null,
  StartDate    datetime not null,
  EndDate      datetime
)
go

alter table TeamMember add
  constraint FK_TeamMember_Asset foreign key(AssetId) references Asset(AssetId),
  constraint FK_TeamMember_Asset1 foreign key(ChiefAssetId) references Asset(AssetId),
  constraint FK_TeamMember_Team foreign key(TeamId) references Team(TeamId)
go

create table Tract(
  TractId     int           not null identity constraint PK_Tract primary key,
  Easting     int           not null,
  Northing    int           not null,
  RefName     varchar(100)  not null,
  CreatedBy   int           not null,
  IsDeleted   bit           not null constraint DF_Tract_IsDeleted default (0),
  DocID       int,
  CalledAC    numeric(18,2) not null,
  UnitId      int           not null,
  DocIdUnique  as (isnull([DocId],(-1))),
  timestamp   timestamp     not null,

  constraint ck_refNameDocId unique(RefName,DocIdUnique)
)
go

alter table Tract add
  constraint FK_Tract_Document foreign key(DocID) references Document(DocId),
  constraint FK_Tract_Unit foreign key(UnitId) references Unit(UnitId)
go

create table TractCalls(
  TractCallId    int          not null identity constraint PK_TractCalls primary key,
  TractId        int          not null,
  CallType       varchar(15)  not null,
  CallDBValue    varchar(200) not null,
  CallOrder      int          not null,
  CreatedByMouse bit          not null
)
go

alter table TractCalls add
  constraint FK_TractCalls_Tract foreign key(TractId) references Tract(TractId)
go

create table TractTextObjects(
  TractTextObjectId int            not null identity constraint PK_TractTextObjects primary key,
  TractId           int            not null,
  Text              nvarchar(4000) not null,
  Easting           numeric(18,2)  not null,
  Northing          numeric(18,2)  not null,
  Rotation          numeric(18,2)  not null
)
go

alter table TractTextObjects add
  constraint FK_TractTextObjects_Tract foreign key(TractId) references Tract(TractId)
go

create table Unit(
  UnitId    int         not null identity constraint PK_Unit primary key,
  [Name]    varchar(50) not null,
  AcresRate float       not null
)
go

create table [User](
  UserId          int         not null identity constraint PK_User primary key,
  Login           varchar(50) not null,
  Password        varchar(50) not null,
  DefaultSite     varchar(50) not null,
  IsActive        bit         not null default (1),
  HackingAttempts int         not null default (0),
  NewTracts       int         not null default (0)
)
go

create table UserRole(
  UserRoleId int not null identity constraint PK_UserPermissionGroup primary key,
  UserId     int not null,
  RoleId     int not null
)
go

alter table UserRole add
  constraint FK_UserRole_Role foreign key(RoleId) references Role(RoleId) on update cascade on delete cascade,
  constraint FK_UserRole_User foreign key(UserId) references [User](UserId) on update cascade on delete cascade
go

commit
go


