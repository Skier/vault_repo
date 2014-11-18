create sequence GEO_LAYER_SQC;

create table GEO_LAYER (
  ID            int         not null,
  NAME          text        not null,
  DESCRIPTION   text            null,
  IS_ACTIVE     bool        not null,
  IS_PUBLIC     bool        not null
);

alter table GEO_LAYER
   add constraint PK_GEO_LAYER primary key (ID);

insert into sys_version (version) values ('0.0.70');
