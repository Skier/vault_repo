drop view DOC_LEASE_TRACT_QQ_VIEW;
drop view DOC_LEASE_TRACT_QQ_VIEW2;
drop view DOC_LEASE_TRACT_VIEW;
drop view DOC_LEASE_TRACT_FULL_VIEW;

alter table DOC_ATTACHMENT add TYPE        text        null;
alter table DOC_ATTACHMENT add NAME        text        null;
alter table DOC_ATTACHMENT add MEMO        text        null;
alter table DOC_ATTACHMENT add MEMO_DATE   date        null;
alter table DOC_ATTACHMENT add COR_DATE    date        null;
alter table DOC_ATTACHMENT add COR_FROM    text        null;
alter table DOC_ATTACHMENT add COR_TO      text        null;
alter table DOC_ATTACHMENT add RECORD_ID   int         null;

update DOC_ATTACHMENT set TYPE=DESCR, NAME='', MEMO=coalesce(NOTE, ''), MEMO_DATE=now();

alter table DOC_ATTACHMENT drop column DESCR;
alter table DOC_ATTACHMENT drop column NOTE;

alter table DOC_ATTACHMENT alter column TYPE set not null;
alter table DOC_ATTACHMENT alter column NAME set not null;
alter table DOC_ATTACHMENT alter column MEMO set not null;
alter table DOC_ATTACHMENT alter column MEMO_DATE set not null;

insert into sys_version (version) values ('0.0.38');
