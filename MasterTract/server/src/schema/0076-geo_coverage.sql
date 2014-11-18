create sequence GEO_COVERAGE_TRACT_SQC;

create table GEO_COVERAGE_TRACT (
ID      int not null,
TYPE    int not null,
NAME    text null,
STATE_ID int null,
COUNTY_ID int null,
TOWNSHIP text null,
TDIR    text null,
RANGE   text null,
RDIR    text null,
MERIDIAN int null,
SECTION text null,
TRACT_DESC text null,
AC decimal(32,16) not null
);

alter table GEO_COVERAGE_TRACT
   add constraint PK_GEO_COVERAGE_TRACT primary key (ID);

alter table GEO_COVERAGE_TRACT add constraint FK_GEO_COVERAGE_TRACT_GEO_STATE foreign key(STATE_ID) 
    references GEO_STATE(ID);

alter table GEO_COVERAGE_TRACT add constraint FK_GEO_COVERAGE_TRACT_GEO_COUNTY foreign key(COUNTY_ID) 
    references GEO_COUNTY(ID);

SELECT AddGeometryColumn('','geo_coverage_tract','the_geom','4267','MULTIPOLYGON',2);

create sequence GEO_COVERAGE_SET_SQC;

create table GEO_COVERAGE_SET (
    ID      int         not null,
    LT_ID   int         not null,
    CT_ID   int         not null
);

alter table GEO_COVERAGE_SET
   add constraint PK_GEO_COVERAGE_SET primary key (ID);

alter table GEO_COVERAGE_SET add constraint FK_GEO_COVERAGE_SET_DOC_LEASE_TRACT foreign key(LT_ID) 
    references DOC_LEASE_TRACT(ID);

alter table GEO_COVERAGE_SET add constraint FK_GEO_COVERAGE_SET_GEO_COVERAGE_TRACT foreign key(CT_ID) 
    references GEO_COVERAGE_TRACT(ID) on delete cascade;

ALTER TABLE GEO_COVERAGE_TRACT ADD CONSTRAINT geo_coverage_geometry_valid_check CHECK (isvalid(the_geom));

ALTER TABLE GEO_GEOMETRY ADD CONSTRAINT geo_geometry_geometry_valid_check CHECK (isvalid(the_geom));

alter table GEO_COVERAGE_SET add constraint U_GEO_COVERAGE_TO_LEASE_TRACT unique(LT_ID, CT_ID);

alter table GEO_COVERAGE_TRACT add constraint U_GEO_COVERAGE_TRACT_NAME unique(TYPE, NAME);

create view GEO_COVERAGE_TRACT_VIEW as
select 
    ct.id as coverage_id,
    ct.ac,
    (select count(*) from geo_coverage_set where ct_id=ct.id) as count,
    ct.the_geom
from geo_coverage_tract ct
;

SELECT UpdateGeometrySRID('geo_geometry','the_geom','4267');

insert into sys_version (version) values ('0.0.76');
