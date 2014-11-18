create sequence GEO_GEOMETRY_SQC;

create table GEO_GEOMETRY (
    ID          int                 not null default nextval('GEO_GEOMETRY_SQC'),
    STATUS      int                 not null,
    NOTE        text                    null,
    USER_ID     int                     null,
    CHANGED     timestamp           not null default now()
);

alter table GEO_GEOMETRY
   add constraint PK_GEO_GEOMETRY primary key (ID);

SELECT AddGeometryColumn('','geo_geometry','the_geom','-1','MULTIPOLYGON',2);

alter table GEO_TRACT add constraint FK_GEO_TRACT_GEO_GEOMETRY foreign key(GEOMETRY_ID) 
    references GEO_GEOMETRY(ID);

insert into sys_version (version) values ('0.0.19');
