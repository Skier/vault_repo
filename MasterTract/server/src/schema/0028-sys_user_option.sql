create sequence SYS_USER_OPTION_SQC;

create table SYS_USER_OPTION (
  ID            int         not null,
  USER_ID       int         not null,
  OPT_KEY       text        not null,
  OPT_VAL       text        not null
);

alter table SYS_USER_OPTION
   add constraint PK_SYS_USER_OPTION primary key (ID);

alter table SYS_USER_OPTION add constraint FK_SYS_USER_OPTION_SYS_USER foreign key(USER_ID) 
    references SYS_USER(ID);

insert into sys_version (version) values ('0.0.28');
