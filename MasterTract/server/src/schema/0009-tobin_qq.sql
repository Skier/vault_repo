alter table tobin_qq add external_id int null;
update tobin_qq set external_id=gid;
create index TOBIN_QQ_EXTERNAL_ID_IDX on TOBIN_QQ(EXTERNAL_ID);
