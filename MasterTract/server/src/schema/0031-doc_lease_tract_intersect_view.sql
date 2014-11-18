drop view DOC_LEASE_TRACT_INTERSECT_VIEW;

create view DOC_LEASE_TRACT_INTERSECT_VIEW as
select 
    lt.id as id,
    cast(lt.lease_interest as float) as lt_int,
    ltl.lease_num as lt_lease_num,
    'TWP:' || lt.township || ' RNG:' || lt.range || ' SEC:' || lt.section || ' DESC:' || lt.tract as lt_descr,
    lta.id as lta_id,
    cast(lta.lease_interest as float) as lta_int,
    ltal.lease_num as lta_lease_num,
    'TWP:' || lta.township || ' RNG:' || lta.range || ' SEC:' || lta.section || ' DESC:' || lta.tract as lta_descr,
--    geometrytype(multi(intersection(g.the_geom, ga.the_geom))) as intr_type,
--    intersects(g.the_geom, ga.the_geom) as intrs,
    multi(intersection(g.the_geom, ga.the_geom)) as the_geom
from doc_lease_tract lt
    inner join (select h.tract_id, max(h.id) as id 
                from doc_lease_tract_history h 
                group by h.tract_id) hm on hm.tract_id=lt.id
    inner join doc_lease_tract_history hist on hist.id=hm.id
    inner join geo_geometry g on hist.geometry_id = g.id
    inner join doc_lease ltl on ltl.id=lt.lease_id
    inner join doc_lease_tract lta on lt.id<lta.id
    inner join (select h.tract_id, max(h.id) as id 
                from doc_lease_tract_history h 
                group by h.tract_id) hma on hma.tract_id=lta.id
    inner join doc_lease_tract_history hista on hista.id=hma.id
    inner join geo_geometry ga on hista.geometry_id=ga.id
    inner join doc_lease ltal on ltal.id=lta.lease_id
where intersects(g.the_geom, ga.the_geom)
  and area(intersection(g.the_geom, ga.the_geom)) > 0
  and 'GEOMETRYCOLLECTION' != geometrytype(multi(intersection(g.the_geom, ga.the_geom)))
;

insert into sys_version (version) values ('0.0.31');
