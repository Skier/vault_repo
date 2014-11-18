alter table DOC_LEASE_TRACT drop column GEOMETRY_ID;

insert into sys_version (version) values ('0.0.32');
