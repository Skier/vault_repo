alter table DOC_LEASE add RENT_DUE_DATE date null;

insert into sys_version (version) values ('0.0.68');
