create sequence LL_StateSqc;
create table LL_State(
  StateId   int         not null DEFAULT nextval('LL_StateSqc'::regclass),
  Name      varchar(50) not null,
  StateFips varchar(2)  not null,
  StateAbbr varchar(2)  not null
);

alter table LL_State
   add constraint PK_LL_State primary key (StateId);

create sequence LL_CountySqc;
create table LL_County(
  CountyId   int          not null DEFAULT nextval('LL_CountySqc'::regclass),
  Name       varchar(150) not null,
  StateId    int          not null,
  StateFips  varchar(2)   not null,
  CountyFips varchar(3)   not null,
  FullFips   varchar(5)   not null
);

alter table LL_County
   add constraint PK_LL_County primary key (CountyId);

alter table LL_County add
  constraint FK_LL_County_LL_State foreign key(StateId) references LL_State(StateId);

create sequence LL_AddressSqc;
create table LL_Address(
  AddressId int          not null DEFAULT nextval('LL_AddressSqc'::regclass),
  Address1  varchar(150) not null,
  Address2  varchar(150)     null,
  City      varchar(50)  not null,
  StateId   int          not null,
  Zip       varchar(50)      null
);

alter table LL_Address
   add constraint PK_LL_Address primary key (AddressId);

alter table LL_Address add
  constraint FK_LL_Address_LL_State foreign key(StateId) references LL_State(StateId);

create sequence LL_PersonSqc;
create table LL_Person(
  PersonId   int         not null DEFAULT nextval('LL_PersonSqc'::regclass),
  FirstName  varchar(50) not null,
  MiddleName varchar(50)     null,
  LastName   varchar(50) not null,
  Phone      varchar(50) not null,
  PhoneAlt   varchar(50)     null,
  AddressId  int
);

alter table LL_Person
   add constraint PK_LL_Person primary key (PersonId);

alter table LL_Person add
  constraint FK_LL_Person_LL_Address foreign key(AddressId) references LL_Address(AddressId) on update cascade on delete cascade;

create sequence LL_LoginSqc;
create table LL_Login(
  LoginId  int         not null DEFAULT nextval('LL_LoginSqc'::regclass),
  Username varchar(50) not null,
  Password varchar(50) not null,
  EMail    varchar(50) not null,
  PersonId int         not null
);

alter table LL_Login
   add constraint PK_LL_Login primary key (LoginId);

alter table LL_Login add
  constraint FK_LL_Login_LL_Person foreign key(PersonId) references LL_Person(PersonId);

create sequence LL_User_Sqc;
create table LL_User(
  UserId  int not null DEFAULT nextval('LL_User_Sqc'::regclass),
  LoginId int not null
);

alter table LL_User
   add constraint PK_LL_User primary key (UserId);

alter table LL_User add
  constraint FK_LL_User_LL_Login foreign key(LoginId) references LL_Login(LoginId);

create sequence LL_Client_Sqc;
create table LL_Client(
  ClientId    int          not null DEFAULT nextval('LL_Client_Sqc'::regclass),
  UserId      int          not null,
  PersonId    int          not null,
  Name        varchar(250) not null,
  Description varchar(250)
);

alter table LL_Client
   add constraint PK_LL_Client primary key (ClientId);

alter table LL_Client add
  constraint FK_LL_Client_LL_User foreign key(UserId) references LL_User(UserId);
  
alter table LL_Client add
  constraint FK_LL_Client_LL_Person foreign key(PersonId) references LL_Person(PersonId);

create sequence LL_Project_Sqc;
create table LL_Project(
  ProjectId   int           not null DEFAULT nextval('LL_Project_Sqc'::regclass),
  ClientId    int           not null,
  ProjectName varchar(50)   not null,
  Afe         varchar(50)       null,
  Description text              null,
  Status      varchar(50)   not null
);

alter table LL_Project
   add constraint PK_LL_Project primary key (ProjectId);

alter table LL_Project add
  constraint FK_LL_Project_LL_Client foreign key(ClientId) references LL_Client(ClientId);

create sequence LL_DefaultExpenceType_Sqc;
create table LL_DefaultExpenceType(
  DefaultExpenceTypeId int           not null DEFAULT nextval('LL_DefaultExpenceType_Sqc'::regclass),
  ItemName             varchar(50)   not null,
  DefaultRate          numeric(18,3) not null
);

alter table LL_DefaultExpenceType
   add constraint PK_LL_DefaultExpenceType primary key (DefaultExpenceTypeId);

create sequence LL_Invoice_Sqc;
create table LL_Invoice(
  InvoiceId   int      not null DEFAULT nextval('LL_Invoice_Sqc'::regclass),
  UserId      int      not null,
  ClientId    int      not null,
  InvoiceDate date     not null,
  StartDate   date         null,
  EndDate     date         null
);

alter table LL_Invoice
   add constraint PK_LL_Invoice primary key (InvoiceId);

alter table LL_Invoice add
  constraint FK_LL_Invoice_LL_Client foreign key(ClientId) references LL_Client(ClientId);

alter table LL_Invoice add
  constraint FK_LL_Invoice_LL_User foreign key(UserId) references LL_User(UserId);

create sequence LL_ExpenceType_Sqc;
create table LL_ExpenceType(
  ExpenceTypeId  int           not null DEFAULT nextval('LL_ExpenceType_Sqc'::regclass),
  BasedOn        int               null,
  UserId         int           not null,
  ItemName       varchar(50)   not null,
  DefaultRate    numeric(18,3) not null
);

alter table LL_ExpenceType
   add constraint PK_LL_ExpenceType primary key (ExpenceTypeId);

create sequence LL_InvoiceItem_Sqc;
create table LL_InvoiceItem(
  InvoiceItemId int           not null DEFAULT nextval('LL_InvoiceItem_Sqc'::regclass),
  InvoiceId     int           not null,
  ProjectId     int           not null,
  ExpenceTypeId int           not null,
  ItemDate      date          not null,
  Quantity      numeric(18,3) not null,
  Rate          numeric(18,3) not null,
  Adjustment    numeric(18,3) not null,
  Status        int           not null
);

alter table LL_InvoiceItem
   add constraint PK_LL_InvoiceItem primary key (InvoiceItemId);

alter table LL_InvoiceItem add
  constraint FK_LL_InvoiceItem_LL_ExpenceType foreign key(ExpenceTypeId) references LL_ExpenceType(ExpenceTypeId);

alter table LL_InvoiceItem add
  constraint FK_LL_InvoiceItem_LL_Project foreign key(ProjectId) references LL_Project(ProjectId);

create sequence LL_Note_Sqc;
create table LL_Note(
  NoteId    int  not null DEFAULT nextval('LL_Note_Sqc'::regclass),
  UserId    int  not null,
  InvoiceId int  not null,
  NoteDate  date not null,
  NoteText  text not null
);

alter table LL_Note
   add constraint PK_LL_Note primary key (NoteId);

alter table LL_Note add
  constraint FK_LL_Note_LL_Invoice foreign key(InvoiceId) references LL_Invoice(InvoiceId);

alter table LL_Note add
  constraint FK_LL_Note_LL_User foreign key(UserId) references LL_User(UserId);

