create table DOC_SURFACE_TRACT(
  ID           int            not null,
  NO           text           not null,
  AC           decimal(32,16) not null,
  GLINK        text           not null,
  STATUS       text           not null
);

alter table DOC_SURFACE_TRACT
   add constraint PK_DOC_SURFACE_TRACT primary key (ID);

alter table DOC_SURFACE_TRACT add constraint FK_DOC_SURF_TRACT_DOC_DOCUMENT foreign key(ID) 
    references DOC_DOCUMENT(ID);

insert into sys_version (version) values ('0.0.71');
