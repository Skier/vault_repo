alter table SYS_USER add IS_PM bool null;

update SYS_USER set IS_PM=false;

alter table SYS_USER alter column IS_PM set not null;

insert into sys_version (version) values ('0.0.57');
