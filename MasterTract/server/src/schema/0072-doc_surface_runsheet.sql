create table DOC_SURFACE_RUNSHEET(
  ID           int            not null,
  NO           text           not null,
  INSTRUMENT   text           not null,
  DOD          date           not null,
  DOR          date           not null,
  DESCR        text               null,
  REMARKS      text               null
);

alter table DOC_SURFACE_RUNSHEET
   add constraint PK_DOC_SURFACE_RUNSHEET primary key (ID);

alter table DOC_SURFACE_RUNSHEET add constraint FK_DOC_SURF_RUNSHEET_DOC_DOCUMENT foreign key(ID) 
    references DOC_DOCUMENT(ID);

insert into sys_version (version) values ('0.0.72');
