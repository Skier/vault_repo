create table [EC_Catalog](
    [CatalogId] int identity not null,
    [Code]      varchar(3)       null,
    [Name]      varchar(50)      null,
    primary key([CatalogId])
)

create table [EC_Section](
    [SectionId]       int identity not null,
    [CatalogId]       int          not null,
    [StartPageNumber] int          not null,
    [PagesTotal]      int          not null,
    [SectionPrefix]   varchar(10)      null,
    [PdfPath]         varchar(200)     null,
    [Sort]            int          not null,
    primary key([SectionId])
)

create table [EC_SectionPage](
    [SectionPageId]     int identity not null,
    [SectionId]         int          not null,
    [SectionPageNumber] int          not null,
    [ScreenshotURL]     varchar(200)     null,
    [Width]             int          not null,
    [Height]            int          not null,
    primary key([SectionPageId])
)

create table [EC_SectionPageToken](
    [SectionPageTokenId] int identity not null,
    [SectionPageId]      int          not null,
    [Top]                int          not null,
    [Left]               int          not null,
    [Width]              int          not null,
    [Height]             int          not null,
    [Text]               varchar(200) not null,
    primary key([SectionPageTokenId])
)

create table [EC_SectionItem](
    [SectionItemId] int identity not null,
    [SectionPageId] int          not null,
    [ModelId]       int          not null,
    [CatalogLevel]  int          not null,
    primary key([SectionItemId])
)

alter table [EC_SectionPageToken] add
    constraint FK_EC_SectionPageToken_SectionPage foreign key([SectionPageId]) references [EC_SectionPage]([SectionPageId])
        on update cascade
        on delete cascade

alter table [EC_SectionPage] add
    constraint FK_EC_SectionPage_Section foreign key([SectionId]) references [EC_Section]([SectionId])
        on update cascade
        on delete cascade

alter table [EC_Section] add
    constraint FK_EC_Section_Catalog foreign key([CatalogId]) references [EC_Catalog]([CatalogId])
        on update cascade
        on delete cascade

alter table [EC_SectionItem] add
    constraint FK_EC_SectionItem_SectionPage foreign key([SectionPageId]) references [EC_SectionPage]([SectionPageId])
        on update cascade
        on delete cascade

go

create view EC_TTS_Menu_ModelItems as (
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
