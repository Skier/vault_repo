create sequence DOC_ASSIGNMENT_TRACT_SQC;

create table DOC_ASSIGNMENT_TRACT(
  ID            int            not null,
  TRACT_ID      int            not null,
  ASSIGNMENT_ID int            not null
);

alter table DOC_ASSIGNMENT_TRACT
   add constraint PK_DOC_ASSIGNMENT_TRACT primary key (ID);

alter table DOC_ASSIGNMENT_TRACT add constraint FK_D_A_T_DOC_LEASE_TRACT foreign key(TRACT_ID) 
    references DOC_LEASE_TRACT(ID);

alter table DOC_ASSIGNMENT_TRACT add constraint FK_D_A_T_DOC_ASSIGNMENT foreign key(ASSIGNMENT_ID) 
    references DOC_ASSIGNMENT(ID);

insert into sys_version (version) values ('0.0.42');
