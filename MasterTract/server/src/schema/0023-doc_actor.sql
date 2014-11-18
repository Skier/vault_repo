alter table DOC_ACTOR alter column ADDRESS_ID drop not null;

insert into sys_version (version) values ('0.0.23');
