insert into CustomerPrice( CustomerId, ModelItemId, multiplier) 
select CustomerId, ModelItemId, 1 
  from Customer, ModelItem;