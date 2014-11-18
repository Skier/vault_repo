alter table SYS_USER add IS_ADMIN bool null;

alter table SYS_USER add CLIENT_ID int null;

alter table SYS_USER add constraint FK_SYS_USER_SYS_CLIENT foreign key(CLIENT_ID) 
    references SYS_CLIENT(ID);

update SYS_USER set IS_ADMIN=false, CLIENT_ID=-1;

alter table SYS_USER alter column IS_ADMIN set not null;

insert into sys_version (version) values ('0.0.56');
