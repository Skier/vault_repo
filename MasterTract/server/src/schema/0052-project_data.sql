insert into ORG_PROJECT (
    ID,
    NAME)
select 
    nextval('ORG_PROJECT_SQC'),
    a.PROSP_NAME
from (select distinct PROSP_NAME from DOC_LEASE where PROSP_NAME is not null) a
;

insert into DOC_PROJECT (
    ID,
    DOC_ID,
    PROJECT_ID)
select 
    nextval('DOC_PROJECT_SQC'),
    l.ID,
    p.ID
from DOC_LEASE l 
    inner join ORG_PROJECT p on l.PROSP_NAME=p.NAME
;

insert into sys_version (version) values ('0.0.52');
