create sequence DOC_SURFACE_TRACT_HISTORY_SQC;

create table DOC_SURFACE_TRACT_HISTORY (
  ID            int         not null,
  TRACT_ID      int         not null,
  GEOMETRY_ID   int         not null,
  USER_ID       int             null,
  USER_NAME     text            null,
  CREATED       timestamp   not null,
  NOTE          text            null
);

alter table DOC_SURFACE_TRACT_HISTORY
   add constraint PK_DOC_SURFACE_TRACT_HISTORY primary key (ID);

alter table DOC_SURFACE_TRACT_HISTORY add constraint FK_DSTH_DOC_SURFACE_TRACT foreign key(TRACT_ID) 
    references DOC_SURFACE_TRACT(ID);

alter table DOC_SURFACE_TRACT_HISTORY add constraint FK_DSTH_GEO_GEOMETRY foreign key(GEOMETRY_ID) 
    references GEO_GEOMETRY(ID);

alter table DOC_SURFACE_TRACT_HISTORY add constraint FK_DSTH_SYS_USER foreign key(USER_ID) 
    references SYS_USER(ID);

insert into sys_version (version) values ('0.0.73');
