alter table doc_lease_tract add surf_owner bool null;

update doc_lease_tract set surf_owner = false;

alter table doc_lease_tract alter column surf_owner set not null;

insert into sys_version (version) values ('0.0.61');
