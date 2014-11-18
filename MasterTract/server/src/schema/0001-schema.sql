-- SYS_MODULE_TYPE
create sequence SYS_MODULE_TYPE_SQC;

create table SYS_MODULE_TYPE (
    ID          int                 not null,
    NAME        varchar(50)         not null
);

alter table SYS_MODULE_TYPE
   add constraint PK_SYS_MODULE_TYPE primary key (ID);

-- SYS_MODULE
create sequence SYS_MODULE_SQC;

create table SYS_MODULE (
    ID          int                 not null,
    TYPE_ID     int                 not null,
    NAME        varchar(20)         not null,
    DESCRIPTION varchar(250)            null,
    URL         varchar(250)        not null
);

alter table SYS_MODULE
   add constraint PK_SYS_MODULE primary key (ID);

alter table SYS_MODULE add constraint FK_SYS_MODULE_SYS_MODULE_TYPE foreign key(TYPE_ID) 
    references SYS_MODULE_TYPE(ID);

-- SYS_PERSON
create sequence SYS_PERSON_SQC;

create table SYS_PERSON (
    ID          int                 not null,
    FIRST_NAME  varchar(50)         not null,
    MIDDLE_NAME varchar(50)             null,
    LAST_NAME   varchar(50)         not null,
    P_PHONE     varchar(50)         not null,
    S_PHONE     varchar(50)             null,
    EMAIL       varchar(50)         not null,
    SSN         varchar(50)             null,
    BIRTH_DAY   date                not null
);

alter table SYS_PERSON
   add constraint PK_SYS_PERSON primary key (ID);

-- SYS_USER
create sequence SYS_USER_SQC;

create table SYS_USER (
    ID          int                 not null,
    PERSON_ID   int                 not null,
    LOGIN       varchar(50)         not null,
    PASSWORD    varchar(50)         not null,
    IS_ACTIVE   bool                not null,
    HACK_ATTEMPTS int               not null
);

alter table SYS_USER
   add constraint PK_SYS_USER primary key (ID);

alter table SYS_USER add constraint FK_SYS_USER_SYS_PERSON foreign key(PERSON_ID) 
    references SYS_PERSON(ID);

-- SYS_ROLE
create sequence SYS_ROLE_SQC;

create table SYS_ROLE (
    ID          int                 not null,
    NAME        varchar(50)         not null
);

alter table SYS_ROLE
   add constraint PK_SYS_ROLE primary key (ID);

-- SYS_USER_ROLE
create sequence SYS_USER_ROLE_SQC;

create table SYS_USER_ROLE (
    ID          int                 not null,
    USER_ID     int                 not null,
    ROLE_ID     int                 not null
);

alter table SYS_USER_ROLE
   add constraint PK_SYS_USER_ROLE primary key (ID);

alter table SYS_USER_ROLE add constraint FK_SYS_USER_ROLE_SYS_USER foreign key(USER_ID) 
    references SYS_USER(ID);

alter table SYS_USER_ROLE add constraint FK_SYS_USER_ROLE_SYS_ROLE foreign key(ROLE_ID)    
    references SYS_ROLE(ID);

-- SYS_FILE
create sequence SYS_FILE_SQC;

create table SYS_FILE(
  ID          int          not null,
  ORIG_FILENAME text       not null,
  MIME_TYPE   text         not null,
  STORAGE_KEY text         not null,
  USER_ID     int          not null,
  CHANGED     date         not null,
  NOTE        text             null
);

alter table SYS_FILE
   add constraint PK_SYS_FILE primary key (ID);

alter table SYS_FILE add constraint FK_SYS_FILE_SYS_USER foreign key(USER_ID) 
    references SYS_USER(ID) on delete restrict;

-- GEO_STATE
create sequence GEO_STATE_SQC;

create table GEO_STATE(
  ID        int         not null,
  NAME      varchar(50) not null,
  FIPS      varchar(2)  not null,
  ABBR      varchar(2)  not null
);

alter table GEO_STATE
   add constraint PK_GEO_STATE primary key (ID);

-- GEO_COUNTY
create sequence GEO_COUNTY_SQC;

create table GEO_COUNTY(
  ID        int         not null,
  STATE_ID  int         not null,
  NAME      varchar(50) not null,
  SFIPS     varchar(2)  not null,
  CFIPS     varchar(3)  not null,
  FIPS      varchar(5)  not null
);

alter table GEO_COUNTY
   add constraint PK_GEO_COUNTY primary key (ID);

alter table GEO_COUNTY add constraint FK_GEO_COUNTY_GEO_STATE foreign key(STATE_ID) 
    references GEO_STATE(ID);

-- GEO_TRACT
--create sequence GEO_TRACT_SQC;

create table GEO_TRACT(
  ID            int         not null,
  STATE_ID      int         not null,
  TOWNSHIP      text        not null,
--  TWP_DIR       text        not null,
  RANGE         text        not null,
--  RNG_DIR       text        not null,
--  MERIDIAN      text        not null,
  SECTION       text        not null,
  QQ            text        not null
);

alter table GEO_TRACT
   add constraint PK_GEO_TRACT primary key (ID);

alter table GEO_TRACT add constraint FK_GEO_TRACT_GEO_STATE foreign key(STATE_ID) 
    references GEO_STATE(ID);

-- GEO_ADDRESS
create sequence GEO_ADDRESS_SQC;

create table GEO_ADDRESS(
  ID        int          not null,
  STATE_ID  int          not null,
  ADDR1     text         not null,
  ADDR2     text             null,
  CITY      text         not null,
  ZIP       text         not null
);

alter table GEO_ADDRESS
   add constraint PK_GEO_ADDRESS primary key (ID);

alter table GEO_ADDRESS add constraint FK_GEO_ADDRESS_GEO_STATE foreign key(STATE_ID) 
    references GEO_STATE(ID);

-- DOC_TYPE
create sequence DOC_TYPE_SQC;

create table DOC_TYPE(
  ID          int          not null,
  NAME        varchar(50)  not null,
  GIVER_ROLE  text             null,
  RECV_ROLE   text             null
);

alter table DOC_TYPE
   add constraint PK_DOC_TYPE primary key (ID);

-- DOC_STATUS
create sequence DOC_STATUS_SQC;

create table DOC_STATUS(
  ID          int          not null,
  NAME        varchar(50)  not null
);

alter table DOC_STATUS
   add constraint PK_DOC_STATUS primary key (ID);

-- DOC_DOCUMENT
create sequence DOC_DOCUMENT_SQC;

create table DOC_DOCUMENT(
  ID          int          not null,
  TYPE_ID     int          not null,
  STATUS_ID   int          not null,
  USER_ID     int          not null,
  NOTE        text             null
);

alter table DOC_DOCUMENT
   add constraint PK_DOC_DOCUMENT primary key (ID);

alter table DOC_DOCUMENT add constraint FK_DOC_DOCUMENT_DOC_TYPE foreign key(TYPE_ID) 
    references DOC_TYPE(ID);

alter table DOC_DOCUMENT add constraint FK_DOC_DOCUMENT_DOC_STATUS foreign key(STATUS_ID) 
    references DOC_STATUS(ID);

alter table DOC_DOCUMENT add constraint FK_DOC_DOCUMENT_SYS_USER foreign key(USER_ID) 
    references SYS_USER(ID);

-- DOC_RECORD
create sequence DOC_RECORD_SQC;

create table DOC_RECORD(
  ID          int          not null,
  DOC_ID      int          not null,
  STATE_ID    int          not null,
  COUNTY_ID   int          not null,
  DOC_DATE    date         not null,
  DOC_NO      text             null,
  VOLUME      varchar(50)      null,
  PAGE        varchar(50)      null,
  IS_PUBLIC   bool         not null
);

alter table DOC_RECORD
   add constraint PK_DOC_RECORD primary key (ID);

alter table DOC_RECORD add constraint FK_DOC_RECORD_DOC_DOCUMENT foreign key(DOC_ID) 
    references DOC_DOCUMENT(ID);

alter table DOC_RECORD add constraint FK_DOC_RECORD_GEO_STATE foreign key(STATE_ID) 
    references GEO_STATE(ID);

alter table DOC_RECORD add constraint FK_DOC_RECORD_GEO_COUNTY foreign key(COUNTY_ID) 
    references GEO_COUNTY(ID);

-- DOC_ACTOR
create sequence DOC_ACTOR_SQC;

create table DOC_ACTOR(
  ID          int          not null,
  DOC_ID      int          not null,
  ADDRESS_ID  int          not null,
  NAME        text         not null,
  IS_GIVER    bool         not null,
  IS_COMPANY  bool         not null,
  TAXID       text             null
);

alter table DOC_ACTOR
   add constraint PK_DOC_ACTOR primary key (ID);

alter table DOC_ACTOR add constraint FK_DOC_ACTOR_DOC_DOCUMENT foreign key(DOC_ID) 
    references DOC_DOCUMENT(ID);

alter table DOC_ACTOR add constraint FK_DOC_ACTOR_GEO_ADDRESS foreign key(ADDRESS_ID) 
    references GEO_ADDRESS(ID) on delete restrict;


-- DOC_ACTOR_PHONE
create sequence DOC_ACTOR_PHONE_SQC;

create table DOC_ACTOR_PHONE(
  ID          int          not null,
  ACTOR_ID    int          not null,
  PHONE       text         not null,
  IS_PRIMARY  bool         not null
);

alter table DOC_ACTOR_PHONE
   add constraint PK_DOC_ACTOR_PHONE primary key (ID);

alter table DOC_ACTOR_PHONE add constraint FK_DOC_ACTOR_PHONE_DOC_ACTOR foreign key(ACTOR_ID) 
    references DOC_ACTOR(ID);

-- DOC_ATTACHMENT
create sequence DOC_ATTACHMENT_SQC;

create table DOC_ATTACHMENT(
  ID          int          not null,
  DOC_ID      int          not null,
  FILE_ID     int          not null,
  DESCR       text         not null,
  NOTE        text             null
);

alter table DOC_ATTACHMENT
   add constraint PK_DOC_ATTACHMENT primary key (ID);

alter table DOC_ATTACHMENT add constraint FK_DOC_ATTACHMENT_DOC_DOCUMENT foreign key(DOC_ID) 
    references DOC_DOCUMENT(ID);

alter table DOC_ATTACHMENT add constraint FK_DOC_ATTACHMENT_SYS_FILE foreign key(FILE_ID) 
    references SYS_FILE(ID);

-- DOC_RECORD_ATTACHMENT
create sequence DOC_RECORD_ATTACHMENT_SQC;

create table DOC_RECORD_ATTACHMENT(
  ID          int          not null,
  RECORD_ID   int          not null,
  FILE_ID     int          not null,
  DESCR       text         not null,
  NOTE        text             null
);

alter table DOC_RECORD_ATTACHMENT
   add constraint PK_DOC_RECORD_ATTACHMENT primary key (ID);

alter table DOC_RECORD_ATTACHMENT add constraint FK_DOC_RECORD_ATTACHMENT_DOC_RECORD foreign key(RECORD_ID) 
    references DOC_RECORD(ID);

alter table DOC_RECORD_ATTACHMENT add constraint FK_DOC_RECORD_ATTACHMENT_SYS_FILE foreign key(FILE_ID) 
    references SYS_FILE(ID);

-- DOC_LEASE
create table DOC_LEASE(
  ID          int           not null,
  PROSP_NAME  varchar(255)      null,
  LEASE_NAME  text          not null,
  LEASE_DATE  date          not null,
  EFFECT_DATE date          not null,
  TERM        int           not null,
  IS_PAID_UP  bool          not null,
  ROYALTY     decimal(17,5) not null,
  ROYALTY_INPUT text            null,
  BONUS_RATE  decimal(17,5) not null,
  BONUS_AMT   decimal(17,5) not null,
  GROSS_ACRES decimal(32,16) not null,
  NET_ACRES   decimal(32,16) not null,
  NOTE        text             null
);

alter table DOC_LEASE
   add constraint PK_DOC_LEASE primary key (ID);

alter table DOC_LEASE add constraint FK_DOC_LEASE_DOC_DOCUMENT foreign key(ID) 
    references DOC_DOCUMENT(ID);

-- DOC_LEASE_EXT
create sequence DOC_LEASE_EXT_SQC;

create table DOC_LEASE_EXT(
  ID          int           not null,
  TERM        int           not null,
  ROYALTY     decimal(17,5) not null,
  BONUS_RATE  decimal(17,5) not null,
  BONUS_AMT   decimal(17,5) not null,
  NOTE        text              null
);

alter table DOC_LEASE_EXT
   add constraint PK_DOC_LEASE_EXT primary key (ID);

-- DOC_LEASE_CLAUSE
create table DOC_LEASE_CLAUSE(
  ID          int          not null,
  DEPTH       bool             null,
  DAMAGE      bool             null,
  PUGH        bool             null,
  SHUT_IN_GAS bool             null,
  TAKE_GAS_ROY bool             null,
  SURFACE     bool             null,
  CONT_DRILL  bool             null,
  FAV_NAT     bool             null,
  OPT_TO_EXT  bool             null,
  EXT_ID      int              null,
  ASSIGNMENT  bool             null,
  PROD_PAYM   bool             null,
  POOL_PROV   bool             null,
  MIN_ROY_PAY bool             null,
  RENEWAL_OPT bool             null,
  HBP         bool             null,
  SPC_PROV    bool             null,
  LESSER_INT  bool             null,
  REWORK_DAYS bool             null,
  OTHER       bool             null,
  OTHER_DESC  text             null
);

alter table DOC_LEASE_CLAUSE add constraint FK_DOC_LEASE_CLAUSE_DOC_LEASE foreign key(ID) 
    references DOC_LEASE(ID);

alter table DOC_LEASE_CLAUSE add constraint FK_DOC_LEASE_CLAUSE_DOC_LEASE_EXT foreign key(EXT_ID) 
    references DOC_LEASE_EXT(ID) on delete restrict;

-- DOC_LEASE_TRACT
create sequence DOC_LEASE_TRACT_SQC;

create table DOC_LEASE_TRACT(
  ID          int          not null,
  LEASE_ID    int          not null,
  TOWNSHIP    text         not null,
  RANGE       text         not null,
  SECTION     text         not null,
  TRACT       text         not null,
  GROSS_ACRES decimal(32,16) not null,
  NET_ACRES decimal(32,16)       null,
  NOTE   text             null
);

alter table DOC_LEASE_TRACT
   add constraint PK_DOC_LEASE_TRACT primary key (ID);

alter table DOC_LEASE_TRACT add constraint FK_DOC_LEASE_TRACT_DOC_LEASE foreign key(LEASE_ID) 
    references DOC_LEASE(ID);

-- DOC_LEASE_TRACT_QQ
create sequence DOC_LEASE_TRACT_QQ_SQC;

create table DOC_LEASE_TRACT_QQ(
  ID       int          not null,
  LT_ID    int          not null,
  GT_ID    int          not null
);

alter table DOC_LEASE_TRACT_QQ
   add constraint PK_DOC_LEASE_TRACT_QQ primary key (ID);

alter table DOC_LEASE_TRACT_QQ add constraint FK_DOC_LEASE_TRACT_QQ_DOC_LEASE_TRACT foreign key(LT_ID) 
    references DOC_LEASE_TRACT(ID);

alter table DOC_LEASE_TRACT_QQ add constraint FK_DOC_LEASE_TRACT_QQ_GEO_TRACT foreign key(GT_ID) 
    references GEO_TRACT(ID);

-- DOC_LEASE_BREAK
create sequence DOC_LEASE_BREAK_SQC;

create table DOC_LEASE_BREAK(
  ID          int          not null,
  ACTOR_ID    int          not null,
  TRACT_ID    int          not null,
  INTR        decimal(32,16) not null,
  FROM_DEPTH  text             null,
  TO_DEPTH    text             null,
  PRODUCT     text             null,
  FORMATION   text             null
);

alter table DOC_LEASE_BREAK
   add constraint PK_DOC_LEASE_BREAK primary key (ID);

alter table DOC_LEASE_BREAK add constraint FK_DOC_LEASE_BREAK_DOC_LEASE_TRACT foreign key(TRACT_ID) 
    references DOC_LEASE_TRACT(ID);

alter table DOC_LEASE_BREAK add constraint FK_DOC_LEASE_BREAK_DOC_ACTOR foreign key(ACTOR_ID) 
    references DOC_ACTOR(ID);

-- DOC_LEASE_ASSIGN
create table DOC_LEASE_ASSIGN(
  ID           int            not null,
  ROYALTY      decimal(32,16) not null,
  ASSIGN_DATE  date           not null,
  EFFECT_DATE  date           not null,
  IS_FULL_LEASE_SET bool      not null
);

alter table DOC_LEASE_ASSIGN
   add constraint PK_DOC_LEASE_ASSIGN primary key (ID);

alter table DOC_LEASE_ASSIGN add constraint FK_DOC_LEASE_ASSIGN_DOC_DOCUMENT foreign key(ID) 
    references DOC_DOCUMENT(ID);

-- DOC_LEASE_ASSIGN_SET
create sequence DOC_LEASE_ASSIGN_SET_SQC;

create table DOC_LEASE_ASSIGN_SET(
  ID          int          not null,
  ASSIGN_ID   int          not null,
  LEASE_ID    int          not null,
  RECORDED    bool          not null
);

alter table DOC_LEASE_ASSIGN_SET
   add constraint PK_DOC_LEASE_ASSIGN_SET primary key (ID);

alter table DOC_LEASE_ASSIGN_SET add constraint FK_DOC_LEASE_ASSIGN_SET_DOC_LEASE_ASSIGN foreign key(ASSIGN_ID) 
    references DOC_LEASE_ASSIGN(ID);

alter table DOC_LEASE_ASSIGN_SET add constraint FK_DOC_LEASE_ASSIGN_SET_DOC_LEASE foreign key(LEASE_ID) 
    references DOC_LEASE(ID) on delete restrict;







