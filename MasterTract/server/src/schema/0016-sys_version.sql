create table sys_version (
    deployed timestamp not null default now(),
    version text not null
);

insert into sys_version (version) values ('0.0.16');
