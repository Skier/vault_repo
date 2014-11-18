drop view DOC_LEASE_TRACT_ERROR_VIEW;

create view DOC_LEASE_TRACT_ERROR_VIEW as
select 
    t.id as tract_id,
    t.lease_id,
    t.township,
    t.range,
    t.section,
    t.tract as description
from doc_lease_tract t
where t.note='NOT PARSED'
;
