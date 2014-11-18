alter table GEO_GEOMETRY add REF_ID int null;

insert into GEO_GEOMETRY (
    REF_ID,
    STATUS,
    THE_GEOM)
select 
    ID,
    0,
    THE_GEOM
from GEO_TRACT;

create index GEO_GEOMETRY_REF_ID_IDX on GEO_GEOMETRY(REF_ID);

update GEO_TRACT t set 
    GEOMETRY_ID = (select g.ID from GEO_GEOMETRY g where g.REF_ID=t.ID);

drop view DOC_LEASE_TRACT_QQ_VIEW;

select DropGeometryColumn('','geo_tract','the_geom');

alter table GEO_TRACT drop column STATE_ID;

insert into sys_version (version) values ('0.0.20');
