drop view DOC_LEASE_TRACT_PROJECT_VIEW;

create view DOC_LEASE_TRACT_PROJECT_VIEW as
select 
    p.name as proj_name,
    g.the_geom
from doc_lease_tract lt
    inner join doc_lease_tract_history hist on hist.tract_id=lt.id
    inner join geo_geometry g on hist.geometry_id = g.id
    inner join doc_lease l on lt.lease_id=l.id
    inner join doc_project dp on dp.doc_id=l.id
    inner join org_project p on p.id=dp.project_id
;

insert into sys_version (version) values ('0.0.60');
