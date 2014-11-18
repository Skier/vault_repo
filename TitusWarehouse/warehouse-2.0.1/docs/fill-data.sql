delete from tmp_titus where cat1 is null;

insert into Category (CategoryId, BrandId, ParentCategoryId, Name, CreatedByUser, DateCreated, LastUpdateDate) 
select distinct cast(cat1_id as integer), 1, null, cat1, 'import', getdate(), getdate() 
  from tmp_titus; 


insert into Category (CategoryId, BrandId, ParentCategoryId, Name, CreatedByUser, DateCreated, LastUpdateDate) 
select distinct cast(cat2_id as integer), 1, cast(cat1_id as integer), cat2, 'import', getdate(), getdate() 
  from tmp_titus; 


insert into Category (CategoryId, BrandId, ParentCategoryId, Name, CreatedByUser, DateCreated, LastUpdateDate) 
select distinct cast(cat3_id as integer), 1, cast(cat2_id as integer), cat3, 'import', getdate(), getdate() 
  from tmp_titus; 

insert into Model (ModelId, BrandId, CategoryId, ModelName, IsActive, DateCreated, CreatedByUser, LastUpdateDate)
select distinct cast(desk_id as integer), 1, cast(cat3_id as integer), model, 1, getdate(), 'import', getdate() 
  from tmp_titus;

 
insert into Item (SKU, Description, Width, Length, Height, Weight, QtyIncrement, IsActive, DateCreated, CreatedByUser, LastUpdateDate) 
select distinct SKU, desk, 1, 1, 1, 1, 1, 1, getdate(), 'import', getdate() 
  from tmp_titus; 



insert into ModelItem (ModelId, ItemId, Configuration, Price, IsActive, ImageURL, XMLBullerDescription, DateCreated, LastUpdateDate, CreatedByUser )
select distinct cast(desk_id as integer), (select ItemId from Item where Item.SKU = tmp_titus.SKU), 
       '123XYZ345', 23.34, 1, '', '',  getdate(), getdate(), 'import' 
  from tmp_titus;


update ModelItem 
   set configuration = coalesce(( select conf 
                           from tmp_titus_items 
                                inner join item on tmp_titus_items.sku = item.sku
                          where item.ItemId = ModelItem.ItemId ), '.') , 
       price = coalesce(( select cast(price as money)
                           from tmp_titus_items 
                                inner join item on tmp_titus_items.sku = item.sku
                          where item.ItemId = ModelItem.ItemId ), 1)

update Item 
   set description = coalesce(( select coalesce(desk, Item.description) 
                         from tmp_titus_items 
                         where Item.sku = tmp_titus_items.sku), '.');

insert into Inventory (WarehouseId, ItemId, Qty, DateCreated, CreatedByUser, LastUpdateDate) 
select 1, ItemId, cast(cnt as integer), getdate(), 'import', getdate() 
  from Item 
       inner join tmp_titus_items on Item.sku = tmp_titus_items.sku;

