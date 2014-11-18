delete InvoiceItem
delete Invoice
delete Note
delete [Message]
delete BillItem

update Bill set Status='NEW', TotalDailyBill = 0, DailyBillAmt = 0, OtherBillAmt = 0, TotalBillAmt = 0
update Bill set Status='CONFIRMED' where substring(StartDate, 1, 2) < '08'
