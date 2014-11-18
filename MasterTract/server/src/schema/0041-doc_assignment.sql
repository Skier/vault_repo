create table DOC_ASSIGNMENT(
  ID           int            not null,
  ROYALTY      decimal(32,16) not null,
  ASSIGN_DATE  date           not null,
  EFFECT_DATE  date           not null,
  IS_FULL_LEASE_SET bool      not null,
  DEPTH_SEV    text               null,
  BURDENS      text               null
);

alter table DOC_ASSIGNMENT
   add constraint PK_DOC_ASSIGNMENT primary key (ID);

alter table DOC_ASSIGNMENT add constraint FK_DOC_ASSIGNMENT_DOC_DOCUMENT foreign key(ID) 
    references DOC_DOCUMENT(ID);

insert into sys_version (version) values ('0.0.41');
