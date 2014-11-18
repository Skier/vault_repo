alter table geo_tract add GEOMETRY_ID int null;

insert into sys_version (version) values ('0.0.18');
