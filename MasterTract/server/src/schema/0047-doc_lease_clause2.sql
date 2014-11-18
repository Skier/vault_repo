create sequence DOC_LEASE_CLAUSE_SQC;

create table DOC_LEASE_CLAUSE2 (
  ID            int         not null,
  LEASE_ID      int         not null,
  CODE          text        not null,
  NAME          text            null,
  DESCR         text            null
);

alter table DOC_LEASE_CLAUSE2
   add constraint PK_DOC_LEASE_CLAUSE2 primary key (ID);

alter table DOC_LEASE_CLAUSE2 add constraint FK_DLC2_DOC_LEASE foreign key(LEASE_ID) 
    references DOC_LEASE(ID);

insert into sys_version (version) values ('0.0.47');
