SELECT AddGeometryColumn('','geo_tract','the_geom','-1','MULTIPOLYGON',2);

update geo_tract set the_geom = (select the_geom from tobin_qq where gid=qq_id);
