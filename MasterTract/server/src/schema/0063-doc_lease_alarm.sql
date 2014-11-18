create sequence DOC_LEASE_ALARM_SQC;

create table DOC_LEASE_ALARM (
    ID          int         not null,
    LEASE_ID    int         not null,
    CLAUSE_ID   int             null,
    ALARM_DATE  date        not null,
    IS_ACTIVE   bool        not null
);

alter table DOC_LEASE_ALARM
   add constraint PK_DOC_LEASE_ALARM primary key (ID);

alter table DOC_LEASE_ALARM add constraint FK_DOC_LEASE_ALARM_DOC_LEASE foreign key(LEASE_ID) 
    references DOC_LEASE(ID);

alter table DOC_LEASE_ALARM add constraint FK_DOC_LEASE_ALARM_DOC_LEASE_CLAUSE foreign key(CLAUSE_ID) 
    references DOC_LEASE_CLAUSE2(ID);

insert into sys_version (version) values ('0.0.63');
