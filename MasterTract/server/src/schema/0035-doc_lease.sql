alter table DOC_LEASE add VET bool null;
alter table DOC_LEASE add OPTIONS text null;
alter table DOC_LEASE add TERM_STATUS text null;

insert into sys_version (version) values ('0.0.35');
