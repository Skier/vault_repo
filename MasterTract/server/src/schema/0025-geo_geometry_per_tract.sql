delete from GEO_GEOMETRY where status=-1;

insert into GEO_GEOMETRY (
    REF_ID,
    STATUS,
    THE_GEOM)
select
    a.lt_id,
    a.status,
    multi(geomunion(a.the_geom))
from (
select 
    ltq.lt_id,
    -1 as status,
    (dump(g.THE_GEOM)).geom as the_geom
from DOC_LEASE_TRACT_QQ ltq
    inner join GEO_TRACT gt on ltq.gt_id=gt.id
    inner join GEO_GEOMETRY g on gt.geometry_id=g.id
) a group by a.lt_id, a.status;

update DOC_LEASE_TRACT t set 
    GEOMETRY_ID = (select g.ID from GEO_GEOMETRY g where g.STATUS=-1 and g.REF_ID=t.ID);

insert into sys_version (version) values ('0.0.25');
