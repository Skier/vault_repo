/*****************************************************************************/ 
/* Iteration 2 DB modification script                                        */
/*****************************************************************************/

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

-- CustData table

UPDATE [CustData]
SET [WebPassword] = '0' + [WebPassword]
WHERE [WebPassword] IS NOT NULL
GO

-- spStoreLocation_Get_States

CREATE PROCEDURE [spStoreLocation_Get_States]
AS
    SELECT DISTINCT [State]
    FROM [StoreLocation]
    ORDER BY [State]
GO

-- spStoreLocation_Get_CitiesByState

CREATE PROCEDURE [dbo].[spStoreLocation_Get_CitiesByState]
    @State CHAR(2)
AS
    SELECT DISTINCT [City]
    FROM [StoreLocation]
    WHERE [State] = @State AND [City] IS NOT NULL
    ORDER BY [City]
GO

-- spStoreLocation_Get_AllByStateAndCity

CREATE PROCEDURE [dbo].[spStoreLocation_Get_AllByStateAndCity]
    @State CHAR(2),
    @City  VARCHAR(50)
AS
    SELECT 
        [StoreCode],
        [StoreClass],
        [Name],
        [StoreNumber],
        [Address],
        [City],
        [State],
        [Zip],
        [Phone],
        [Fax],
        [Manager],
        [Active],
        [ActiveDate],
        [PriceCode],
        [Wireless_PriceCode],
        [Notes],
        [AddLocInf],
        [TermDate],
        [Status],
        [Ilec],
        [DMA],
        [CorpID],
        [Type],
        [Internet_Channel_ID],
        [LocalService],
        [Wireless],
        [Internet],
        [SmartConnect],
        [NET_FlatRate],
        [SC_FlatRate],
        [LS_FlatRate],
        [Divisional_Manager],
        [OverrideDebCardProd],
        [DebitCard],
        [OvrdRestProd],
        [IsNarrowPrinter],
        [Satellite],
        [ShowSource],
        [DpiWireless]
    FROM [StoreLocation]
    WHERE [State] = @State AND [City] = @City
    ORDER BY [Name]
GO

-- sp_AccountCheckPTP

CREATE PROCEDURE [sp_AccountCheckPTP]
	@AccNumber INT,
    @DueDate DATETIME
AS
    SELECT COUNT(1)
    FROM Account_PTP
    WHERE AccNumber = @AccNumber AND PTP_Date > @DueDate        
GO

-- sp_AccountGetPTP

CREATE PROCEDURE [sp_AccountGetPTP]
	@AccNumber INT,
    @DueDate DATETIME
AS
    SELECT PTP_Amount, PTP_Date, Date_Created
    FROM Account_PTP
    WHERE AccNumber = @AccNumber AND PTP_Date > @DueDate
    ORDER BY Account_PTP_ID       
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO