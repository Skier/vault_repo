create table CARGO_TRACE (
    CARGO_ID    varchar(15) not null,
    TIME_STAMP  timestamp   not null,
    LATITUDE    varchar(10) not null, 
    LONGITUDE   varchar(10) not null, 
    primary key (CARGO_ID, TIME_STAMP)  
);
