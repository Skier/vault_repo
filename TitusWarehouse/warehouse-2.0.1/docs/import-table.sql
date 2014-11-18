-- drop table tmp_titus;

create table tmp_titus (
    cat1_id varchar(10),
    cat1 varchar(200),
    cat2_id varchar(10),
    cat2 varchar(200),
    cat3_id varchar(10),
    cat3 varchar(200),
    desk_id varchar(10),
    desk varchar(200),
    img  varchar(200), 
    model varchar(200), 
    sku   varchar(200)
);


create table tmp_titus_items (
    sku varchar(100), 
    desk varchar(100), 
    price varchar(100), 
    plant varchar(100), 
    model varchar(100), 
    conf varchar(100), 
    cnt  varchar(100)    
);