alter table DOC_LEASE_TRACT add STATE_ID int null;
alter table DOC_LEASE_TRACT add COUNTY_ID int null;

insert into sys_version (version) values ('0.0.69');
