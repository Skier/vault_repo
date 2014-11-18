update Invoice
set    InvoiceNumber = ''
where  InvoiceNumber is null

alter table Invoice
    alter column InvoiceNumber varchar(50) not null


