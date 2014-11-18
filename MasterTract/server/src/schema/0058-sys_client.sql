alter table SYS_CLIENT add ABBR text null;

update SYS_CLIENT set ABBR='';

alter table SYS_CLIENT alter column ABBR set not null;

insert into sys_version (version) values ('0.0.58');
