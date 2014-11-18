create sequence SYS_CLIENT_SQC;

create table SYS_CLIENT (
  ID            int         not null,
  NAME          text        not null,
  IS_ACTIVE     bool        not null
);

alter table SYS_CLIENT
   add constraint PK_SYS_CLIENT primary key (ID);

insert into sys_version (version) values ('0.0.54');
