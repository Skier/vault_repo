insert into DOC_ATTACHMENT (
    ID,
    DOC_ID,
    FILE_ID,
    RECORD_ID,
    TYPE,
    NAME,
    MEMO,
    MEMO_DATE
) select
    nextval('DOC_ATTACHMENT_SQC'),
    r.DOC_ID,
    ra.FILE_ID,
    r.ID,
    'Recorded',
    '',
    coalesce(ra.NOTE, ''),
    now()
from DOC_RECORD r
    inner join DOC_RECORD_ATTACHMENT ra on r.ID=ra.RECORD_ID;

drop table DOC_RECORD_ATTACHMENT;

insert into sys_version (version) values ('0.0.39');
