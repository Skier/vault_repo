drop view DOC_LEASE_TRACT_ORPHAN_VIEW;

create view DOC_LEASE_TRACT_ORPHAN_VIEW as
select 
    l.id as lease_id,
    l.lease_num,
    l.prosp_name,
    (select name from doc_actor where is_giver=true and doc_id=l.id limit 1) as lessor,
    (select name from doc_actor where is_giver=false and doc_id=l.id limit 1) as lessee,
    l.effect_date as eff_date,
    l.effect_date + cast(cast(l.term as text) || ' month' as interval) as exp_date,
    l.term,
    cast(l.bonus_rate as float),
    cast(l.bonus_amt as float),
    l.is_paid_up,
    g.note,
    g.changed as created,
    g.the_geom
from geo_geometry g
    inner join doc_lease l on g.ref_id=l.id
where g.status=1
;

insert into sys_version (version) values ('0.0.33');
