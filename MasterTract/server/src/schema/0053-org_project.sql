alter table ORG_PROJECT add IS_ACTIVE bool null;

update ORG_PROJECT set IS_ACTIVE=true;

alter table ORG_PROJECT alter column IS_ACTIVE set not null;

insert into sys_version (version) values ('0.0.53');
