alter table ORG_PROJECT add CLIENT_ID int null;

alter table ORG_PROJECT add constraint FK_ORG_PROJECT_SYS_CLIENT foreign key(CLIENT_ID) 
    references SYS_CLIENT(ID);

insert into SYS_CLIENT(ID, NAME, IS_ACTIVE) values (-1, 'FC', true);

update ORG_PROJECT set CLIENT_ID=-1;

alter table ORG_PROJECT alter column CLIENT_ID set not null;

insert into sys_version (version) values ('0.0.55');
