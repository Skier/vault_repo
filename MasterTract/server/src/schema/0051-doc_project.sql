create sequence DOC_PROJECT_SQC;

create table DOC_PROJECT (
  ID            int         not null,
  DOC_ID        int         not null,
  PROJECT_ID    int         not null
);

alter table DOC_PROJECT
   add constraint PK_DOC_PROJECT primary key (ID);

alter table DOC_PROJECT add constraint FK_DOC_PROJECT_DOC_DOCUMENT foreign key(DOC_ID) 
    references DOC_DOCUMENT(ID);

alter table DOC_PROJECT add constraint FK_DOC_PROJECT_ORG_PROJECT foreign key(PROJECT_ID) 
    references ORG_PROJECT(ID);

insert into sys_version (version) values ('0.0.51');
