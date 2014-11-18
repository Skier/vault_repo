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
    null,
    r.ID,
    'Recorded',
    '',
    '',
    now()
from DOC_RECORD r
where r.id not in (select RECORD_ID from DOC_ATTACHMENT where record_id is not null);

insert into sys_version (version) values ('0.0.46');
