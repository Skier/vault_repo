update BillItemType
set    IsAttachRequired = 1
where  BillItemTypeId <> 1
and    BillItemTypeId <> 2
