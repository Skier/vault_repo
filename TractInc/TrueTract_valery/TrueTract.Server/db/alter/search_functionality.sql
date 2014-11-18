set xact_abort on
go

begin transaction
go

if object_id('dbo.SearchItem') is null
create table SearchItem(
  SearchItemId int  not null identity constraint PK_SearchItem primary key,
  ItemTypeId   int  not null,
  ItemId       int  not null,
  ItemValue    text not null,
  ItemXmlValue text not null,

  constraint FK_SearchItem_ItemType_ItemId unique(ItemTypeId,ItemId)
)
go

alter table SearchItem add
  constraint FK_SearchItem_SearchItemType foreign key(ItemTypeId) references SearchItemType(ItemTypeId)
go

if object_id('dbo.SearchItemType') is null
create table SearchItemType(
  ItemTypeId int         not null identity constraint PK_SearchItemType primary key,
  TypeName   varchar(50) not null
)
go

commit
go


