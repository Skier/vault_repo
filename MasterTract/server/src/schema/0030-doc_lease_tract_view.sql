drop view DOC_LEASE_TRACT_VIEW;

create view DOC_LEASE_TRACT_VIEW as
select 
    l.id as lease_id,
    lt.id as tract_id,
    l.lease_num,
    l.prosp_name,
    (select name from doc_actor where is_giver=true and doc_id=l.id limit 1) as lessor,
    (select name from doc_actor where is_giver=false and doc_id=l.id limit 1) as lessee,
    'TWP:' || lt.township || ' RNG:' || lt.range || ' SEC:' || lt.section || ' DESC:' || lt.tract as description,
    l.effect_date as eff_date,
    l.effect_date + cast(cast(l.term as text) || ' month' as interval) as exp_date,
    l.term,
    cast(l.bonus_rate as float),
    cast(l.bonus_amt as float),
    l.is_paid_up,
    cast(lt.gross_acres as float),
    cast(lt.net_acres as float),
    cast(lt.lease_interest as float) as interest,
    cast(lt.nri as float),
    cast(lt.cwi as float),
    coalesce((select f.orig_filename from sys_file f inner join doc_attachment da on f.id=da.file_id where da.doc_id=l.id and da.descr='ORIGINAL_DOC_FILE'), '-') as pdf_lease,
    '' as unitid,
    '' as ami,
    hist.id as version,
    g.the_geom
from doc_lease_tract lt
    inner join (select h.tract_id, max(h.id) as id 
                from doc_lease_tract_history h 
                group by h.tract_id) hm on hm.tract_id=lt.id
    inner join doc_lease_tract_history hist on hist.id=hm.id
    inner join geo_geometry g on hist.geometry_id = g.id
    inner join doc_lease l on lt.lease_id=l.id
;

insert into sys_version (version) values ('0.0.30');
