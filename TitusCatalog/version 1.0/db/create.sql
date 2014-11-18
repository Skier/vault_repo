create table [Document](
    [DocumentId]  int identity    not null,
    [Path]        varchar(200)    not null,
    [Name]        varchar(200)        null,
    [Code]        varchar(2)          null,
    primary key([DocumentId])
)

create table [DocumentPage](
    [DocumentPageId]  int identity    not null,
    [DocumentId]      int             not null,
    [PageNumber]      int             not null,
    [ScreenshotURL]   varchar(200)        null,
    [Width]           int             not null,
    [Height]          int             not null,
    primary key([DocumentPageId])
)

create table [DocumentToken](
    [DocumentTokenId] int identity    not null,
    [DocumentPageId]  int             not null,
    [Top]           int             not null,
    [Left]          int             not null,
    [Width]         int             not null,
    [Height]        int             not null,
    [Text]            varchar(200)    not null,
    primary key([DocumentTokenId])
)

create table [Note](
    [NoteId]            int identity    not null,
    [UserId]            int             not null default 1,
    [DocumentPageId]    int             not null,
    [Top]               int             not null,
    [Left]              int             not null,
    [Width]             int             not null,
    [Height]            int             not null,
    [NoteText]          varchar(200)    not null,
    primary key([NoteId])
)

create table [DocumentPageModel](
    [DocumentPageModelId]   int identity    not null,
    [DocumentPageId]        int             not null,
    [ModelId]               bigint          not null,
    primary key([DocumentPageModelId])
)

create table [DocumentPageModelItem](
    [DocumentPageModelItemId]   int identity    not null,
    [DocumentPageId]            int             not null,
    [ModelItemId]               bigint          not null,
    primary key([DocumentPageModelItemId])
)

alter table [DocumentToken] add
    constraint FK_DocumentToken_DocumentPage foreign key([DocumentPageId]) references [DocumentPage]([DocumentPageId])
        on update cascade
        on delete cascade

alter table [DocumentPage] add
    constraint FK_DocumentPage_Document foreign key([DocumentId]) references [Document]([DocumentId])
        on update cascade
        on delete cascade

alter table [Note] add
    constraint FK_Note_DocumentPage foreign key([DocumentPageId]) references [DocumentPage]([DocumentPageId])
        on update cascade
        on delete cascade

go

create view EC_Menu_ModelItems as (
    select  cat1.name 'cat1', CAT1.ID 'CATID', cat2.name AS 'cat2', cat2.id AS 'cat2ID' , prod.name 'cat3',prod.ID 'cat3ID',model.name, MODEL.ID 'modelID'  , model.mfgcode , model.[description], jimage.image_id
    from    jnctbrandcat a
            inner join tblcategory cat1 on cat1.[id] = a.category_id and cat1.active = 1
            inner join jnctcatsubcat jntcat1 on jntcat1.category_id = cat1.[id]
            inner join tblcategory cat2 on cat2.[id]= jntcat1.subcategory_id
            inner join jnctcatprod catprod on catprod.category_id = cat2.id
            inner join tblproduct prod on prod.id = catprod.product_id
            inner join jnctprodmodel prodmod on prodmod.product_id = prod.id
            inner join tblmodel model on model.id = prodmod.model_id
            inner join jnctimage jimage on jimage.objid =model.id and lower(objtype) = 'model'
    where lower(a.brand_code) = 'tts'and CAT1.ID not IN (37,117) --('FAN COILS', 'AIR HANDLERS')
union all
    select  cat1.name 'cat1', CAT1.ID 'CATID', '' AS 'cat2', '' AS 'cat2ID' , prod.name 'cat3',prod.ID 'cat3ID',model.name, MODEL.ID 'modelID' , model.mfgcode , model.[description], jimage.image_id
    from    jnctbrandcat a
            inner join tblcategory cat1 on cat1.[id] = a.category_id and cat1.active = 1
            --inner join jnctcatsubcat jntcat1 on jntcat1.category_id = cat1.[id]
            --inner join tblcategory cat2 on cat2.[id]= jntcat1.subcategory_id
            inner join jnctcatprod catprod on catprod.category_id = cat1.id
            inner join tblproduct prod on prod.id = catprod.product_id
            inner join jnctprodmodel prodmod on prodmod.product_id = prod.id
            inner join tblmodel model on model.id = prodmod.model_id
            inner join jnctimage jimage on jimage.objid =model.id and lower(objtype) = 'model'
    where lower(a.brand_code) = 'tts' AND CAT1.ID IN (37,117) --('FAN COILS', 'AIR HANDLERS')
)

go
