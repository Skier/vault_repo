insert into Inventory (WarehouseId, ItemId, Qty, DateCreated, CreatedByUser, LastUpdateDate) 
select WarehouseId, ItemId, ItemId, getdate(), 'import', getdate() 
  from Item, Warehouse;

