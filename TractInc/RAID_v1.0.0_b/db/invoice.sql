declare @clientId int;
declare @date varchar(10);

declare @invoiceId int;

set @clientId = 1;
set @date = '06/01/2007';

insert  into Invoice (ClientId, ClientName, ClientAddress, ClientActive, Status, Notes, StartDate, TotalDailyAmt, DailyInvoiceAmt, OtherInvoiceAmt, TotalInvoiceAmt)
select  c.ClientId, c.ClientName, c.ClientAddress, c.ClientActive, 'NEW', '', @date, 0, 0, 0, sum(bi.Qty * bi.BillRate)
  from  Client c
        inner join AssetAssignment aa
                on aa.ClientId = c.ClientId
        inner join BillItem bi
                on bi.Status = 'CONFIRMED'
               and bi.AssetAssignmentId = aa.AssetAssignmentId
        inner join Bill b
                on b.StartDate = @date
 where  c.ClientId = @clientId;

set @invoiceId = scope_identity();

insert  into InvoiceItem (InvoiceItemTypeId, InvoiceId, BillItemId, AssetAssignmentId, InvoiceDate, Qty, InvoiceRate, Status, IsSelected)
select  bi.BillItemTypeId, @invoiceId, bi.BillItemId, bi.AssetAssignmentId, b.StartDate, bi.Qty, bi.BillRate, 'NEW', 0
  from  BillItem bi
        inner join AssetAssignment aa
                on aa.AssetAssignmentId = bi.AssetAssignmentId
        inner join Client c
                on c.ClientId = aa.ClientId
        inner join Bill b
                on b.StartDate = @date
               and b.BillId = bi.BillId
 where  c.ClientId = @clientId
   and  bi.Status = 'CONFIRMED'
