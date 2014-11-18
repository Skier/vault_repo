INSERT INTO dbo.DocumentType ([Name], [GiverRequired], [ReceiverRequired], [GiverRoleName], [ReceiverRoleName])

    SELECT [TYPE of DOCS],
            case when Giver is null then 0 else 1 end,
            case when Receiver is null then 0 else 1 end,
            Giver, Receiver
      FROM OPENROWSET('Microsoft.Jet.OLEDB.4.0', 
                      'Excel 8.0;DATABASE=c:\!flex\Types_of_Documents.xls',
                      'Select * from [Sheet1$]')

