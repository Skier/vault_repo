create sequence ORG_PROJECT_SQC;

create table ORG_PROJECT (
  ID            int         not null,
  NAME          text        not null
);

alter table ORG_PROJECT
   add constraint PK_ORG_PROJECT primary key (ID);

insert into sys_version (version) values ('0.0.50');
