create sequence DOC_SURFACE_OWNER_SQC;

create table DOC_SURFACE_OWNER (
  ID            int         not null,
  DOC_ID        int         not null,
  ACTOR_ID      int         not null,
  INTR          decimal(32,16)  null,
  IS_OWNER      bool        not null,
  NOTE          text            null
);

alter table DOC_SURFACE_OWNER
   add constraint PK_DOC_SURFACE_OWNER primary key (ID);

alter table DOC_SURFACE_OWNER add constraint FK_DSO_DOC_SURFACE_TRACT foreign key(DOC_ID) 
    references DOC_SURFACE_TRACT(ID);

alter table DOC_SURFACE_OWNER add constraint FK_DSO_DOC_ACTOR foreign key(ACTOR_ID) 
    references DOC_ACTOR(ID);

insert into sys_version (version) values ('0.0.74');
