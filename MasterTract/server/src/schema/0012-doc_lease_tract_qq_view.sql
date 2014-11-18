drop view DOC_LEASE_TRACT_QQ_VIEW;

create view DOC_LEASE_TRACT_QQ_VIEW as
select 
    l.id as lease_id,
    lt.id as tract_id,
    l.lease_num,
    (select name from doc_actor where is_giver=true and doc_id=l.id limit 1) as lessor,
    (select name from doc_actor where is_giver=false and doc_id=l.id limit 1) as lessee,
    'TWP:' || lt.township || ' RNG:' || lt.range || ' SEC:' || lt.section || ' DESC:' || lt.tract as description,
    l.effect_date as eff_date,
    l.effect_date + cast(cast(l.term as text) || ' month' as interval) as exp_date,
    l.term,
    cast(l.bonus_rate as float),
    cast(l.bonus_amt as float),
    l.is_paid_up,
    cast(l.gross_acres as float),
    cast(l.net_acres as float),
    cast(lt.nri as float),
    cast(lt.cwi as float),
    coalesce((select f.orig_filename from sys_file f inner join doc_attachment da on f.id=da.file_id where da.doc_id=l.id and da.descr='ORIGINAL_DOC_FILE'), '-') as pdf_lease,
    '' as unitid,
    '' as ami,
    q.the_geom
from tobin_qq q 
    inner join geo_tract t on q.gid=t.qq_id
    inner join doc_lease_tract_qq ltq on t.id=ltq.gt_id
    inner join doc_lease_tract lt on ltq.lt_id=lt.id
    inner join doc_lease l on lt.lease_id=l.id
;
