create sequence DOC_REFERENCE_SQC;

create table DOC_REFERENCE (
  ID            int         not null,
  REFERRER_ID   int         not null,
  REFEREE_ID    int         not null
);

alter table DOC_REFERENCE
   add constraint PK_DOC_REFERENCE primary key (ID);

alter table DOC_REFERENCE add constraint FK_DOC_REFERENCE_DOC_DOCUMENT foreign key(REFERRER_ID) 
    references DOC_DOCUMENT(ID);

alter table DOC_REFERENCE add constraint FK_DOC_REFERENCE_DOC_DOCUMENT2 foreign key(REFEREE_ID) 
    references DOC_DOCUMENT(ID);

insert into sys_version (version) values ('0.0.37');
