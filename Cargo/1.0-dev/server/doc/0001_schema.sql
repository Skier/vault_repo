/*==============================================================*/
/* Database name:  PhysicalDataModel_1                          */
/* DBMS name:      PostgreSQL 7                                 */
/* Created on:     2007-04-25 16:21:16                          */
/*==============================================================*/

create sequence CARGO_LOG_SQC;

create sequence CARGO_NETWORK_SQC;

create sequence CARGO_VEHICLE_SQC;

create sequence CARGO_NETWORK_VEHICLE_SQC;

create sequence CARGO_USER_SQC;


/*==============================================================*/
/* Table: CARGO_USER                                            */
/*==============================================================*/
create table CARGO_USER (
ID                   INTEGER              not null,
USER_NAME            VARCHAR(30)          not null,
PASSWORD             VARCHAR(30)          not null
);

alter table CARGO_USER
   add constraint PK_CARGO_USER primary key (ID);
alter table CARGO_USER
   add constraint AK_CARGO_USER unique (USER_NAME);


/*==============================================================*/
/* Table: CARGO_ROLE                                            */
/*==============================================================*/

create view CARGO_ROLE as select user_name, 'tomcat' as user_role from CARGO_USER;


/*==============================================================*/
/* Table: CARGO_LOG                                             */
/*==============================================================*/
create table CARGO_LOG (
ID                   INT8                 not null,
NETWORK_ID           INTEGER              not null,
VEHICLE_ID          INTEGER              not null,
LOG_TIME             TIMESTAMP WITH TIME ZONE not null,
LOG_LEVEL            INTEGER              not null 
      constraint CKC_LOG_LEVEL_CARGO_LO check (LOG_LEVEL between '0' and '5'),
LOG_TEXT             TEXT                 not null
);

alter table CARGO_LOG
   add constraint PK_CARGO_LOG primary key (ID);

/*==============================================================*/
/* Table: CARGO_NETWORK                                         */
/*==============================================================*/
create table CARGO_NETWORK (
ID                   INTEGER              not null,
NAME                 TEXT                 not null
);

alter table CARGO_NETWORK
   add constraint PK_CARGO_NETWORK primary key (ID);

/*==============================================================*/
/* Table: CARGO_NETWORK_LOG                                     */
/*==============================================================*/
create table CARGO_NETWORK_LOG (
ID                   INT8                 not null,
IS_JOIN              BOOL                 not null,
IS_LEAVE             BOOL                 not null,
IS_AUTHORIZED        BOOL                 not null
);

alter table CARGO_NETWORK_LOG
   add constraint PK_CARGO_NETWORK_LOG primary key (ID);

/*==============================================================*/
/* Table: CARGO_STATUS_LOG                                      */
/*==============================================================*/
create table CARGO_STATUS_LOG (
ID                   INT8                 not null,
LATITUDE             TEXT                 not null,
LONGITUDE            TEXT                 not null,
STATUS               TEXT                 not null,
TEMPERATURE          TEXT                 not null,
HUMIDITY             TEXT                 not null
);

alter table CARGO_STATUS_LOG
   add constraint PK_CARGO_STATUS_LOG primary key (ID);

/*==============================================================*/
/* Table: CARGO_TRIP_LOG                                        */
/*==============================================================*/
create table CARGO_TRIP_LOG (
ID                   INT8                 not null,
IS_START             BOOL                 not null,
ORIGIN               TEXT                 not null,
DESTINATION          TEXT                 not null,
CONTENTS             TEXT                 not null
);

alter table CARGO_TRIP_LOG
   add constraint PK_CARGO_TRIP_LOG primary key (ID);

/*==============================================================*/
/* Table: CARGO_VEHICLE                                        */
/*==============================================================*/
create table CARGO_VEHICLE (
ID                   INTEGER              not null,
ITEM_ID              TEXT                 not null,
ITEM_OWNER           TEXT                 not null,
NAME                 TEXT                 not null
);

alter table CARGO_VEHICLE
   add constraint PK_CARGO_VEHICLE primary key (ID);
   
alter table CARGO_VEHICLE
   add constraint AK_KEY_CARGO_VEHICLE unique (ITEM_ID);
   

/*==============================================================*/
/* Table: CARGO_NETWORK_VEHICLE                                 */
/*==============================================================*/
create table CARGO_NETWORK_VEHICLE (
ID                   INT8                 not null,
VEHICLE_ID          INTEGER              not null,
NETWORK_ID           INTEGER              not null
);

alter table CARGO_NETWORK_VEHICLE
   add constraint PK_CARGO_NETWORK_VEHICLE primary key (ID);
   
alter table CARGO_NETWORK_VEHICLE
   add constraint AK_CARGO_NETWORK_VEHICLE UNIQUE (VEHICLE_ID, NETWORK_ID);

alter table CARGO_LOG
   add constraint FK_CARGO_LO_REFERENCE_CARGO_NE foreign key (NETWORK_ID)
      references CARGO_NETWORK (ID)
      on delete restrict on update restrict;

alter table CARGO_LOG
   add constraint FK_CARGO_LO_REFERENCE_CARGO_VE foreign key (VEHICLE_ID)
      references CARGO_VEHICLE (ID)
      on delete restrict on update restrict;

alter table CARGO_NETWORK_LOG
   add constraint FK_CARGO_NE_REFERENCE_CARGO_LO foreign key (ID)
      references CARGO_LOG (ID)
      on delete restrict on update restrict;

alter table CARGO_STATUS_LOG
   add constraint FK_CARGO_ST_REFERENCE_CARGO_LO foreign key (ID)
      references CARGO_LOG (ID)
      on delete restrict on update restrict;

alter table CARGO_TRIP_LOG
   add constraint FK_CARGO_TR_REFERENCE_CARGO_LO foreign key (ID)
      references CARGO_LOG (ID)
      on delete restrict on update restrict;

alter table CARGO_NETWORK_VEHICLE
   add constraint FK_CARG_NET_REFERENCE_CARGO_VE foreign key (VEHICLE_ID)
      references CARGO_VEHICLE (ID)
      on delete restrict on update restrict;

alter table CARGO_NETWORK_VEHICLE
   add constraint FK_CARG_NET_REFERENCE_CARGO_NE foreign key (NETWORK_ID)
      references CARGO_NETWORK (ID)
      on delete restrict on update restrict;

