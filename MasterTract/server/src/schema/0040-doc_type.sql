alter table DOC_TYPE add KIND text null;

update DOC_TYPE set KIND='LEASE' where ID=1;
update DOC_TYPE set KIND='ASSIGNMENT' where ID=2;

alter table DOC_TYPE alter column KIND set not null;

insert into sys_version (version) values ('0.0.40');
