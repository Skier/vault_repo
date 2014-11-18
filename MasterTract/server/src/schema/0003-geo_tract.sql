create sequence GEO_TRACT_SQC;

alter table GEO_TRACT add  TWP_DIR       text        not null;
alter table GEO_TRACT add    RNG_DIR       text        not null;
alter table GEO_TRACT add    MERIDIAN      text        not null;
alter table GEO_TRACT add    LOT           text        null;

