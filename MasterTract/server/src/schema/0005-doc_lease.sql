create sequence DOC_LEASE_LEASE_NUM_SQC start 331000;

alter table DOC_LEASE add LEASE_NUM int null default nextval('DOC_LEASE_LEASE_NUM_SQC');
alter table DOC_LEASE add LOC_ID text null;

